Imports System.Data.SqlClient
Imports MAX.USPS
Imports RickProject.DB
Imports RickProject.Business

Public Class AddContact
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Config.LoginUserType) Then
            Response.Redirect("~/Login.aspx")
        End If
        If Not Page.IsPostBack Then
            BindAllCommunities()
            ' BindVMCategoryByGGID(Config.LoginUserType)
            BindVMCategoryByCommID(ddlCommunity.SelectedValue)
            txtPrimaryFirstName.Focus()
            If Not String.IsNullOrEmpty(Request.QueryString("cid")) Then
                divVMStatus.Style.Add("display", "")
                If (Request.UserAgent.Contains("Chrome")) Then
                    divVMCategory.Style.Add("margin-top", "5px")
                End If

                Dim customerId As Integer = Request.QueryString("cid")
                lblPageTitle.InnerText = "Edit Contact"
                hidCustomerId.Value = customerId
                LoadCustomer(customerId, Config.UserId)
                If txtPrimaryZip.Value.Trim = "99999" Then
                    regexPrimaryWorkPhone.Enabled = False
                    regexPrimaryMobilePhone.Enabled = False
                    regexSecondaryWorkPhone.Enabled = False
                    regexSecondaryMobilePhone.Enabled = False
                Else
                    regexPrimaryWorkPhone.Enabled = True
                    regexPrimaryMobilePhone.Enabled = True
                    regexSecondaryWorkPhone.Enabled = True
                    regexSecondaryMobilePhone.Enabled = True
                End If
                btnAddNewNote.Style.Add("display", "")
                divNotesDisplay.Style.Add("display", "")
                pnlAddNotes.Style.Add("display", "none")
                txtAddNotes.Height = "80"
            Else
                btnAddNewNote.Style.Add("display", "none")
                divNotesDisplay.Style.Add("display", "none")
                pnlAddNotes.Style.Add("display", "")
                If (Request.UserAgent.Contains("Chrome")) Then
                    txtAddNotes.Height = "112"
                Else
                    txtAddNotes.Height = "107"
                End If

            End If
            If ddlVMStatus.SelectedValue = "Not Started" Then
                ddlVMCategory.Enabled = True
            Else
                ddlVMCategory.Enabled = False
            End If
        End If
    End Sub

    Protected Sub Page_PreLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreLoad
        'CapitalizeNames()
    End Sub
    <System.Web.Services.WebMethod()>
    Public Shared Function LoadZip(ByVal zipCode As String) As String
        'major changes in this function to reflect allowing canadian zips
        'check version 102 for the logic that has been deleted
        zipCode = zipCode.ToUpper().Replace(" ", "")
        Dim success As Boolean = False, international As Boolean = False
        Dim city As String = ""
        Dim state As String = ""
        Dim countryCode As String = ""
        'If Regex.IsMatch(zipCode, "^[0-9]{5}$|^[a-zA-Z][0-9][a-zA-Z][ ]?[0-9][a-zA-Z][0-9]$") Then
        success = GetZip(zipCode, city, state, countryCode)
        'Else
        'End If
        Return city & "~" & state & "~" & countryCode
    End Function
    Public Sub BindAllCommunities()
        Dim objPriorityProcessor As New PriorityProcessor()
        Dim dt As New DataTable
        objPriorityProcessor.GetAllCommunitiesByGlobalGroup(Config.LoginUserType, Config.UserId, dt)
        ddlCommunity.DataSource = dt
        ddlCommunity.DataTextField = "CommName"
        ddlCommunity.DataValueField = "CommID"
        ddlCommunity.DataBind()
        ddlCommunity.Items.Insert(0, New ListItem("Please select", "0"))
        If ddlCommunity.Items.Count = 2 Then
            ddlCommunity.SelectedIndex = 1
        End If
    End Sub
    Public Shared Function GetZip(ByVal zip As String, ByRef city As String, ByRef state As String, ByRef countryCode As String) As Boolean
        zip = zip.Replace(" ", "")
        Dim isExists As Boolean = False
        If zip = "99999" Then
            city = "International"
            state = "XX"
            countryCode = ""
            isExists = True
        Else
            Dim strSQLGetZip As String = "" _
            + "SELECT City, State, CountryCode " _
            + "FROM Zipcodes " _
            + "WHERE(ZipCode = @zip)"
            Dim dt As New DataTable()
            Dim Db As DataBase = New DataBase
            Db.Init(strSQLGetZip, False)
            Db.AddParameter("@zip", zip, 50, SqlDbType.NVarChar, ParameterDirection.Input)
            If Not Db.Execute(dt) Then
                Db.Close()
                Return False
            End If
            Db.Close()
            If dt.Rows.Count = 0 Then
                city = ""
                state = ""
                countryCode = ""
            Else
                city = Convert.ToString(dt.Rows(0)(0))
                state = Convert.ToString(dt.Rows(0)(1))
                countryCode = Convert.ToString(dt.Rows(0)(2))
            End If

            'Using dbcon As New SqlConnection(Me.GetSqlConnection)
            '    dbcon.Open()
            '    Using cmdGetZip As New SqlCommand(strSQLGetZip, dbcon)
            '        cmdGetZip.Parameters.AddWithValue("@zip", zip)
            '        Using dr As SqlDataReader = cmdGetZip.ExecuteReader(CommandBehavior.SingleRow)
            '            If dr.HasRows Then
            '                dr.Read()
            '                If Not dr.IsDBNull(0) Then city = dr.GetString(0) Else city = ""
            '                If Not dr.IsDBNull(1) Then state = dr.GetString(1) Else state = ""
            '                If Not dr.IsDBNull(2) Then countryCode = dr.GetString(2) Else countryCode = ""
            '                dr.Close()
            '                isExists = True
            '            Else
            '                city = ""
            '                state = ""
            '                countryCode = ""
            '                dr.Close()
            '                'Check if zip exists on yahoo
            '                'isExists = cms.Utilities.CheckYahooGeoCoderZipDB(zip, city, state, countryCode, Me)
            '                isExists = cms.Utilities.GoogleZipCodeLookup(zip, city, state, countryCode, Me)
            '            End If
            '        End Using
            '    End Using
            'End Using
        End If
        Return isExists
    End Function
    <System.Web.Services.WebMethod()>
    Public Shared Function ValidateUSPS(ByVal zip As String, ByVal workAddress As String, ByVal state As String, ByVal city As String, ByVal countryCode As String) As String
        Dim result As String = "true"
        Try


            Dim tempWorkAddrress As String = workAddress
            If zip <> "99999" And zip <> "" And workAddress.Trim() <> "" Then
                If (String.IsNullOrEmpty(countryCode)) Then
                    GetZip(zip, city, state, countryCode)
                End If
                If countryCode.ToUpper() = "US" Then
                    Dim isValid As Boolean = ValidateAddressField(workAddress, zip, state)
                    If Not isValid Then
                        result = "false~Street Address is not a valid USPS address.<br/><br/>"
                        ' lblUSPSMessageError.Text = "Office Address is not a valid USPS address.<br/><br/>"
                        'tblHomeAddress.Visible = False
                        'lblUSPSMessageError.Visible = True
                    Else
                        If Not String.IsNullOrEmpty(workAddress) Then
                            If tempWorkAddrress.ToLower <> workAddress.ToLower Then
                                result = "invalid~" & workAddress
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
        Return result
    End Function
    Public Shared Function ValidateAddressField(ByRef street As String, ByRef zip As String, ByRef state As String) As Boolean
        Try
            Dim m As New USPSManager("613COMMU5206", True)
            Dim a As New Address()
            street = street.Replace("#", "")
            'city = city.Replace("Saint", "St")
            a.Address2 = street
            a.Zip = zip
            'a.State = state
            'a.City = city
            Dim validatedAddress As Address = m.ValidateAddress(a)
            street = CapitalizeName(validatedAddress.Address2)
            'city = CapitalizeName(validatedAddress.City)
            'state = CapitalizeName(validatedAddress.State)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Shared Function CapitalizeName(ByVal name As String) As String
        If IsEmpty(name) Then Return name
        If Char.IsLower(name.Chars(0)) Then
            Return StrConv(name, VbStrConv.ProperCase)
        ElseIf StrComp(name, UCase(name), vbBinaryCompare) = 0 Then
            Return StrConv(name, VbStrConv.ProperCase)
        End If
        Return name
    End Function
    Shared Function IsEmpty(ByVal text As String) As Boolean
        If text Is Nothing Then Return True
        If Trim(text).Length < 1 Then Return True
        Return False
    End Function
    Public Sub BindVMCategoryByGGID(ByVal GGID As Integer)
        Dim objMaster As New MasterProcessor()
        Dim dt As DataTable = New DataTable()
        objMaster.GetCategoryByGGID(GGID, dt)
        ddlVMCategory.Items.Clear()
        ddlVMCategory.DataSource = dt
        ddlVMCategory.DataTextField = "Type"
        ddlVMCategory.DataValueField = "VMCategoryId"
        ddlVMCategory.DataBind()
        'If dt.Rows.Count > 0 Then
        ddlVMCategory.Items.Insert(0, New ListItem("Please select", 0))
        'End If
    End Sub
    Public Sub BindVMCategoryByCommID(ByVal CommID As Integer)
        Dim objMaster As New MasterProcessor()
        Dim dt As DataTable = New DataTable()
        objMaster.GetCategoryByCommID(CommID, Config.UserId, dt)
        ddlVMCategory.Items.Clear()
        ddlVMCategory.DataSource = dt
        ddlVMCategory.DataTextField = "Type"
        ddlVMCategory.DataValueField = "VMCategoryId"
        ddlVMCategory.DataBind()
        'If dt.Rows.Count > 0 Then
        ddlVMCategory.Items.Insert(0, New ListItem("Please select", 0))
        'End If
    End Sub

    Protected Sub btnAddContact_Click(sender As Object, e As EventArgs)
        If Not Page.IsValid Then
            Exit Sub
        End If
        lblCustomerStatus.Style.Add("display", "none")
        Dim objCustomer As Customer = New Customer()
        Dim objCustomerProcessor As New CustomerProcessor()
        objCustomer.FirstName = CapitalizeName(txtPrimaryFirstName.Value)
        objCustomer.LastName = CapitalizeName(txtPrimaryLastName.Value)
        objCustomer.EmailId = txtPrimaryEmail.Value
        objCustomer.MobilePhone = txtPrimaryMobilePhone.Value
        objCustomer.WorkPhone = txtPrimaryWorkPhone.Value
        objCustomer.StreetAddress = CapitalizeName(txtPrimaryAddress.Value)
        objCustomer.State = txtPrimaryState.Value
        objCustomer.City = txtPrimaryCity.Value
        objCustomer.Fax = txtPrimaryZip.Value
        objCustomer.CountryCode = txtPrimaryCountry.Value
        objCustomer.SecondaryFirstName = CapitalizeName(txtSecondaryFirstName.Value)
        objCustomer.SecondaryLastName = CapitalizeName(txtSecondaryLastName.Value)
        objCustomer.SecondaryWorkPhone = txtSecondaryPhone.Value
        objCustomer.SecondaryCellPhone = txtSecondaryMobilePhone.Value
        objCustomer.SecondaryEmailID = txtSecondaryEmail.Value
        objCustomer.UserID = Config.UserId
        objCustomer.GGID = Config.LoginUserType
        objCustomer.Notes = CapitalizeName(txtAddNotes.Text.Trim)
        objCustomer.VMCategoryID = ddlVMCategory.SelectedValue
        objCustomer.IsActive = cbActive.Checked
        objCustomer.Community = ddlCommunity.SelectedValue
        Dim returnValue As Integer = 0
        objCustomer.ValueMapStatus = ddlVMStatus.SelectedValue
        If Not String.IsNullOrWhiteSpace(hidCustomerId.Value) Then
            objCustomer.CustomerID = hidCustomerId.Value
            If hidOldCategoryId.Value <> ddlVMCategory.SelectedValue Then
                Dim objPriorityProcessor As New PriorityProcessor()
                objPriorityProcessor.ResetCustomerPriorityChoices(hidCustomerId.Value)
            End If
            returnValue = objCustomerProcessor.UpdateCustomer(objCustomer)
        Else
            returnValue = objCustomerProcessor.InsertCustomer(objCustomer)
            If returnValue > 0 Then
                UserProcessor.InsertUserActivity("New Contact", "was created for ", returnValue, Config.UserId)
            End If

        End If

        lblCustomerStatus.Style.Add("display", "inline")
        If returnValue = -1 Then
            lblCustomerStatus.ForeColor = System.Drawing.Color.Red
            lblCustomerStatus.Text = "An error occured in inserting Cutomer. Please contact RickProject Team"
        ElseIf returnValue = -102 Then
            lblCustomerStatus.ForeColor = System.Drawing.Color.Red
            lblCustomerStatus.Text = "Contact already exists."
        ElseIf returnValue > 0 Then
            lblCustomerStatus.ForeColor = System.Drawing.Color.Blue
            lblCustomerStatus.Text = "Contact saved successfully."
            'If Request.QueryString("mode") = "overview" Then
            ' Response.Redirect("~/OverView.aspx?cid=" & returnValue)
            If Request.QueryString("mode") = "overview" Then
                Response.Redirect("~/OverView.aspx?cid=" & Request.QueryString("cid"))
            ElseIf Request.QueryString("mode") = "search" Then
                'Response.Redirect("~/SearchContact.aspx")
                Response.Redirect("~/OverView.aspx?cid=" & returnValue)
            ElseIf Request.QueryString("mode") = "home" Then
                Response.Redirect("~/Home.aspx")
            Else
                Response.Redirect("~/OverView.aspx?cid=" & returnValue)
            End If
            ' End If
            hidCustomerId.Value = returnValue
            LoadCustomer(hidCustomerId.Value, Config.UserId)

        End If
    End Sub
    Protected Sub btnCancelContact_Click(sender As Object, e As EventArgs)
        If Request.QueryString("mode") = "overview" Then
            Response.Redirect("~/OverView.aspx?cid=" & Request.QueryString("cid"))
        ElseIf Request.QueryString("mode") = "search" Then
            Response.Redirect("~/SearchContact.aspx")
        ElseIf Request.QueryString("mode") = "home" Then
            Response.Redirect("~/Home.aspx")
        End If
    End Sub
    Public Sub LoadCustomer(ByVal customerID As Integer, ByVal userID As Integer)
        Dim objCustomerProcessor As New CustomerProcessor()
        Dim dtCustomer As New DataTable
        objCustomerProcessor.GetContactInfoByUser(userID, customerID, dtCustomer)
        If dtCustomer.Rows.Count > 0 Then
            txtPrimaryFirstName.Value = CapitalizeName(Convert.ToString(dtCustomer.Rows(0)("FirstName")))
            txtPrimaryLastName.Value = CapitalizeName(Convert.ToString(dtCustomer.Rows(0)("LastName")))
            txtPrimaryWorkPhone.Value = FormatPhoneNumber(Convert.ToString(dtCustomer.Rows(0)("WorkPhone")))
            txtPrimaryMobilePhone.Value = FormatPhoneNumber(Convert.ToString(dtCustomer.Rows(0)("MobilePhone")))
            txtPrimaryEmail.Value = Convert.ToString(dtCustomer.Rows(0)("EmailId"))
            txtPrimaryAddress.Value = Convert.ToString(dtCustomer.Rows(0)("StreetAddress"))
            txtPrimaryState.Value = Convert.ToString(dtCustomer.Rows(0)("State"))
            txtPrimaryCity.Value = Convert.ToString(dtCustomer.Rows(0)("City"))
            txtPrimaryZip.Value = Convert.ToString(dtCustomer.Rows(0)("Fax"))
            txtPrimaryCountry.Value = Convert.ToString(dtCustomer.Rows(0)("CountryCode"))
            txtSecondaryFirstName.Value = CapitalizeName(Convert.ToString(dtCustomer.Rows(0)("SecondaryFirstName")))
            txtSecondaryLastName.Value = CapitalizeName(Convert.ToString(dtCustomer.Rows(0)("SecondaryLastName")))
            txtSecondaryEmail.Value = Convert.ToString(dtCustomer.Rows(0)("SecondaryEmailId"))
            txtSecondaryMobilePhone.Value = FormatPhoneNumber(Convert.ToString(dtCustomer.Rows(0)("SecondaryCellPhone")))
            txtSecondaryPhone.Value = FormatPhoneNumber(Convert.ToString(dtCustomer.Rows(0)("SecondaryWorkPhone")))
            txtNotes.Value = GetNotesByCustomer(Config.UserId, hidCustomerId.Value)
            cbActive.Checked = Convert.ToBoolean(dtCustomer.Rows(0)("Active"))

            hidOldCategoryId.Value = Convert.ToString(dtCustomer.Rows(0)("VMCategoryId"))

            If Convert.ToString(dtCustomer.Rows(0)("VMCustomerStatus")) = "Sold" Then
                ddlVMStatus.Items.Add(New ListItem("Sold", "Sold"))
                ddlVMStatus.Enabled = False
            End If
            ddlVMStatus.SelectedValue = Convert.ToString(dtCustomer.Rows(0)("VMCustomerStatus"))
            If String.IsNullOrEmpty(Convert.ToString(dtCustomer.Rows(0)("Community"))) Then
                ddlCommunity.SelectedIndex = -1
            Else
                ddlCommunity.SelectedValue = Convert.ToString(dtCustomer.Rows(0)("Community"))
            End If
            BindVMCategoryByCommID(ddlCommunity.SelectedValue)
            Try
                ddlVMCategory.SelectedValue = Convert.ToString(dtCustomer.Rows(0)("VMCategoryId"))
            Catch ex As Exception

            End Try

        End If
    End Sub
    Public Function FormatPhoneNumber(ByVal phoneStr As String) As String
        If Len(phoneStr) < 1 Then
            Return ""
        ElseIf Len(phoneStr) > 10 Then
            Return phoneStr
        End If
        'strip out all the nonnumerics - \D means non-digit characters (\d is digit)
        phoneStr = Regex.Replace(phoneStr, "\D", "")

        If Left(phoneStr, 1) = "1" And Len(phoneStr) > 10 Then
            phoneStr = Mid(phoneStr, 2)
        End If

        Dim testPNum As Decimal
        Try
            testPNum = CDec(Left(phoneStr, 10))
        Catch ex As System.Exception
            Return phoneStr
        End Try
        If Len(testPNum.ToString) < 10 Then Return phoneStr

        Dim formattedString As String = String.Format("{0:(###) ###-####}", testPNum)
        Dim suffix As String = Mid(phoneStr, 11).Trim
        If suffix.Length > 0 Then
            formattedString &= " x" & suffix
        End If
        Return formattedString
    End Function
    Public Function PhoneStorage(ByVal inputString As String) As String
        If Len(inputString) < 1 Then
            Return ""
        ElseIf Len(inputString) > 10 Then
            Return inputString
        End If

        Dim testStr As String = Regex.Replace(inputString, "\D", "")

        'strip off the leading 1 if they entered it
        If Left(testStr, 1) = "1" And Len(testStr) > 10 Then
            testStr = Mid(testStr, 2)
        End If
        Dim fPart As Decimal
        Try
            fPart = CDec(Left(testStr, 10))
        Catch ex As System.Exception
            Return inputString
        End Try
        testStr = fPart.ToString & " " & Mid(testStr, 11)
        testStr = testStr.Trim
        Return testStr
    End Function

    Protected Sub btnSaveNote_Click(sender As Object, e As EventArgs)
        If txtAddNotes.Text.Trim <> "" Then
            lblCustomerStatus.Style.Add("display", "none")
            Dim returnValue As Integer = 0
            Dim objCustomerProcessor As New CustomerProcessor()
            returnValue = objCustomerProcessor.InsertNotes(Config.UserId, hidCustomerId.Value, CapitalizeName(txtAddNotes.Text.Trim))
            If returnValue = -1 Then
                lblCustomerStatus.ForeColor = System.Drawing.Color.Red
                lblCustomerStatus.Text = "An error occured in inserting Notes. Please contact RickProject Team"
                lblCustomerStatus.Style.Add("display", "")
            ElseIf returnValue = -102 Then
                lblCustomerStatus.ForeColor = System.Drawing.Color.Red
                lblCustomerStatus.Text = "Notes already exists."
                lblCustomerStatus.Style.Add("display", "")
            ElseIf returnValue > 0 Then
                lblCustomerStatus.ForeColor = System.Drawing.Color.Blue
                lblCustomerStatus.Text = "Notes saved successfully."
                'If Request.QueryString("mode") = "overview" Then
                ' Response.Redirect("~/OverView.aspx?cid=" & returnValue)
                txtNotes.Value = GetNotesByCustomer(Config.UserId, hidCustomerId.Value)
                lblCustomerStatus.Style.Add("display", "")
            End If
        Else

        End If
    End Sub

    Public Function GetNotesByCustomer(ByVal userID As Integer, ByVal customerID As Integer) As String
        Dim GetNotes = ""
        Dim dt As New DataTable
        Dim objCustomerProcessor As New CustomerProcessor()
        objCustomerProcessor.GetNotesInfoByCustomer(userID, customerID, dt)
        If dt.Rows.Count > 0 Then
            For Each row As DataRow In dt.Rows
                Dim initials As String = Convert.ToString(row("initials")).ToLower()
                Dim dateRec As DateTime = Convert.ToDateTime(row("DateRecorded"))
                Dim prefix As String = String.Format("{0}-({1}) ", dateRec.ToString("M/d/yy"), initials)
                Dim note As String = Convert.ToString(row("Note"))
                If Not String.IsNullOrEmpty(note) Then
                    GetNotes &= prefix & CapitalizeName(note) & vbCrLf
                End If

            Next

        End If
        Return GetNotes
    End Function

    Protected Sub ddlCommunity_SelectedIndexChanged(sender As Object, e As EventArgs)
        BindVMCategoryByCommID(ddlCommunity.SelectedValue)
    End Sub

    'Protected Sub CapitalizeNames()
    '    Exit Sub
    '    txtPrimaryFirstName.Value = StrConv(txtPrimaryFirstName.Value.Trim(), VbStrConv.ProperCase)
    '    txtPrimaryLastName.Value = CapitalizeName(txtPrimaryLastName.Value.Trim())
    '    txtPrimaryEmail.Value = CapitalizeName(txtPrimaryEmail.Value.Trim())
    '    If StrComp(txtPrimaryLastName.Value, UCase(txtPrimaryLastName.Value), vbBinaryCompare) = 0 Then
    '        txtPrimaryLastName.Value = StrConv(txtPrimaryLastName.Value, VbStrConv.ProperCase)
    '    End If
    '    txtSecondaryFirstName.Value = StrConv(txtSecondaryFirstName.Value, VbStrConv.ProperCase)
    '    txtSecondaryEmail.Value = CapitalizeName(txtSecondaryEmail.Value.Trim())
    '    txtSecondaryLastName.Value = CapitalizeName(txtSecondaryLastName.Value.Trim())
    '    If StrComp(txtSecondaryLastName.Value, UCase(txtSecondaryLastName.Value), vbBinaryCompare) = 0 Then
    '        txtSecondaryLastName.Value = StrConv(txtSecondaryLastName.Value, VbStrConv.ProperCase)
    '    End If
    'End Sub
End Class