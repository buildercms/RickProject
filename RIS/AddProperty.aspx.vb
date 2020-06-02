Imports System.Globalization
Imports MAX.USPS
Imports RickProject.Business
Imports RickProject.DB
Imports SD = System.Drawing
Imports System.IO
Imports System.Drawing.Drawing2D

Public Class AddProperty
    Inherits System.Web.UI.Page
    Public Shared userId As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Config.LoginUserType) Then
            Response.Redirect("~/Login.aspx")
        End If
        divPropertyError.Visible = False
        If Not Page.IsPostBack Then
            userId = Config.UserId
            hidAssignProperty.Value = "0"
            LoadCustomer(Request.QueryString("cid"), Config.UserId)
            hidCustomerId.Value = Request.QueryString("cid").Trim()
            Dim objCustomerProcessor As New CustomerProcessor()
            Dim dt, dt1 As New DataTable()
            objCustomerProcessor.GetContactInfoByUser(Config.UserId, Request.QueryString("cid"), dt1)
            lblCustomerName.InnerText = dt1.Rows(0)("FirstName") & " " & dt1.Rows(0)("LastName")
            aOverView.HRef = "Overview.aspx?cid=" & Request.QueryString("cid")
            aCreateValueMap.HRef = "ValueMap.aspx?cid=" & Request.QueryString("cid")
            aCreateProperty.HRef = "AddProperty.aspx?cid=" & Request.QueryString("cid")
            aCompareDecide.HRef = "CompareDecide.aspx?cid=" & Request.QueryString("cid")
            BindValueProperty(Request.QueryString("cid"))
            btnAddProperty.Visible = True
            If Not Request.QueryString("type") Is Nothing Then
                divViewProperty.Style.Add("Visibility", "hidden")
                divAddProperty.Style.Add("Visibility", "visible")
                btnAddProperty.Visible = False
                divPropertyActions.Visible = True
                chkIncludeArchieve.Visible = False
                BindAllPropertiesByCustomerUser(Request.QueryString("cid"), Config.UserId)
                ddlProperties.Attributes.Add("onchange", "javascript: return TogglePropertyName('" & ddlProperties.ClientID & "','" & txtPropertyName.ClientID & "')")
                hidAssignProperty.Value = "0"
            Else
                If Not Request.QueryString("pid") Is Nothing Then
                    hidPropertyId.Value = Request.QueryString("pid")
                    LoadPropertyDetails(hidPropertyId.Value)
                    divViewProperty.Style.Add("Visibility", "hidden")
                    chkIncludeArchieve.Visible = False
                    divAddProperty.Style.Add("Visibility", "visible")
                    ddlProperties.Style.Add("display", "none")
                    txtPropertyName.Style.Add("display", "")
                    txtPropertyName.Focus()
                    divPropertyActions.Visible = True
                    lblPageTitle.InnerText = "Edit Property"
                    btnAddProperty.Visible = False
                    hidAssignProperty.Value = "0"
                    rfvddlProperties.Enabled = False

                Else
                    hidPropertyId.Value = String.Empty
                    Dim dtProperty As New DataTable()
                    dtProperty = LoadAllPropertyDetails(hidCustomerId.Value)
                    If dtProperty.Rows.Count = 0 Then
                        divViewProperty.Style.Add("Visibility", "hidden")
                        chkIncludeArchieve.Visible = False
                        divAddProperty.Style.Add("Visibility", "visible")
                        BindAllPropertiesByCustomerUser(Request.QueryString("cid"), Config.UserId)
                        txtPropertyName.Focus()
                        divPropertyActions.Visible = True
                        btnAddProperty.Visible = False
                        ddlProperties.Attributes.Add("onchange", "javascript: return TogglePropertyName('" & ddlProperties.ClientID & "','" & txtPropertyName.ClientID & "')")
                    Else
                        divViewProperty.Style.Add("Visibility", "visible")
                        chkIncludeArchieve.Visible = True
                        divAddProperty.Style.Add("Visibility", "hidden")
                        divPropertyActions.Visible = False
                        btnAddProperty.Visible = True
                    End If
                    Dim valueMapStatus As Boolean = GetValueMapStatus(hidCustomerId.Value)
                    If dtProperty.Rows.Count > 0 Then
                        Dim propertyBuilder As New StringBuilder()
                        For i As Integer = 0 To dtProperty.Rows.Count - 1
                            Dim objProcessor As New PropertyProcessor()
                            Dim dtPropertyValueScore As New DataSet
                            objProcessor.GetPropertyValueMapScoreById(dtProperty.Rows(i)("PropertyId"), hidCustomerId.Value, dtPropertyValueScore)
                            Dim accordianContent As String = System.IO.File.ReadAllText(Server.MapPath("~/CustomFiles/PropertyUI.html"))
                            accordianContent = accordianContent.Replace("$divid$", "divPriority" & dtProperty.Rows(i)("PropertyId"))
                            accordianContent = accordianContent.Replace("$propid$", dtProperty.Rows(i)("PropertyId"))
                            accordianContent = accordianContent.Replace("$id$", dtProperty.Rows(i)("PropertyId"))
                            accordianContent = accordianContent.Replace("collapseOne", "collapse" & dtProperty.Rows(i)("PropertyId"))
                            accordianContent = accordianContent.Replace("$PropertyName$", dtProperty.Rows(i)("PropertyName"))
                            accordianContent = accordianContent.Replace("Price$", Convert.ToInt32(dtProperty.Rows(i)("Price")).ToString("C", CultureInfo.CreateSpecificCulture("en-US")).Replace(".00", ""))
                            If String.IsNullOrEmpty(Convert.ToString(dtProperty.Rows(i)("sqft"))) Then
                                accordianContent = accordianContent.Replace("$sqft$", "")
                            Else
                                accordianContent = accordianContent.Replace("$sqft$", Convert.ToInt32(dtProperty.Rows(i)("sqft")).ToString("C", CultureInfo.CreateSpecificCulture("en-US")).Replace("$", "").Replace(".00", ""))
                            End If

                            accordianContent = accordianContent.Replace("$bed$", Convert.ToString(dtProperty.Rows(i)("BedBaths")).Split("!")(0))
                            accordianContent = accordianContent.Replace("$baths$", Convert.ToString(dtProperty.Rows(i)("BedBaths")).Split("!")(1))
                            accordianContent = accordianContent.Replace("$Street$", dtProperty.Rows(i)("StreetAddress"))
                            accordianContent = accordianContent.Replace("$City$", dtProperty.Rows(i)("City"))
                            accordianContent = accordianContent.Replace("$State$", dtProperty.Rows(i)("State"))
                            accordianContent = accordianContent.Replace("$zip$", dtProperty.Rows(i)("zip"))
                            accordianContent = accordianContent.Replace("$href$", "AddProperty.aspx?cid=" & hidCustomerId.Value & "&pid=" & dtProperty.Rows(i)("PropertyId"))
                            If String.IsNullOrEmpty(dtProperty.Rows(i)("propertyImage")) Then
                                accordianContent = accordianContent.Replace("$imgId$", "Images/browse.png")
                            Else
                                accordianContent = accordianContent.Replace("$imgId$", "CustomFiles/Property/" & Config.UserId & "/" & dtProperty.Rows(i)("PropertyId") & "/" & dtProperty.Rows(i)("propertyImage"))
                            End If
                            If dtPropertyValueScore.Tables(0).Rows.Count > 0 Then
                                accordianContent = accordianContent.Replace("$btnclass$", "")
                                accordianContent = accordianContent.Replace("$btntext$", "Evaluated")
                            Else
                                accordianContent = accordianContent.Replace("$btnclass$", "btn-red")
                                accordianContent = accordianContent.Replace("$btntext$", "Evaluate")
                            End If
                            If valueMapStatus = False Then
                                accordianContent = accordianContent.Replace("$btndisabled$", "disabled")
                            Else
                                accordianContent = accordianContent.Replace("$btndisabled$", "")
                                ' propertyBuilder.Append("<div class='col-md-2'></div>")
                            End If

                            propertyBuilder.Append("<div class='col-md-4'>").Append(accordianContent).Append("</div>")
                            ' propertyBuilder.Append("<div class='col-md-2'></div>")
                        Next
                        divViewProperty.InnerHtml = propertyBuilder.ToString()
                    End If
                End If

            End If


        End If
        If LoadAllPropertyEvaluation(Request.QueryString("cid")) = 0 Then
            aCompareDecide.Disabled = True
            aCompareDecide.HRef = ""
            aCompareDecide.Style.Add("color", "lightgray")
            aCompareDecide.Style.Add("cursor", "text")
        End If
    End Sub
    Public Sub LoadCustomer(ByVal customerID As Integer, ByVal userID As Integer)
        Dim objCustomerProcessor As New CustomerProcessor()
        Dim dtCustomer As New DataTable
        objCustomerProcessor.GetContactInfoByUser(userID, customerID, dtCustomer)
        If dtCustomer.Rows.Count > 0 Then

        End If
    End Sub

    Protected Sub btnSaveProperty_Click(sender As Object, e As EventArgs)

        Dim propertyImage As String = String.Empty
        Dim objPropertyProcessor As New PropertyProcessor()
        If hidPropertyId.Value = String.Empty Or hidPropertyId.Value = "-1" Then
            If fupPhoto1.HasFile = True Then
                propertyImage = fupPhoto1.FileName
            End If
            If Not String.IsNullOrEmpty(hidCropFile.Value) Then
                propertyImage = Path.GetFileName(hidCropFile.Value)
            End If
            If objPropertyProcessor.CheckPropertyNameByUser(Config.UserId, txtPropertyName.Value.Trim) > 0 Then
                divPropertyError.Visible = True
                ddlProperties.Style.Add("display", "none")
                txtPropertyName.Style.Add("display", "")
                Exit Sub
            End If
            Dim propertyId As Integer = objPropertyProcessor.InsertCustomerPropery(CapitalizeName(txtPropertyName.Value.Trim), CapitalizeName(txtPrimaryAddress.Value.Trim()), CapitalizeName(txtPrimaryCity.Value.Trim()), CapitalizeName(txtPrimaryState.Value.Trim()), CapitalizeName(txtPrimaryZip.Value.Trim()), CapitalizeName(txtPrimaryCountry.Value.Trim()), txtPrice.Value.Replace(",", "").Replace("$", "").Split(".")(0).Trim(), txtBedRooms.Value.Trim() & "!" & txtBaths.Value.Trim(), txtSquareFeet.Value.Replace(",", "").Replace("$", "").Split(".")(0).Trim(), propertyImage, hidCustomerId.Value.Trim(), Not chkArchieve.Checked, Config.UserId)
            hidPropertyId.Value = propertyId
            If fupPhoto1.HasFile = True Or hidCropFile.Value <> "" Then
                propertyImage = fupPhoto1.FileName
                If (Not System.IO.Directory.Exists(Server.MapPath("~/CustomFiles/Property/" & Config.UserId & "/" & propertyId & "/"))) Then
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/CustomFiles/Property/" & Config.UserId & "/" & propertyId & "/"))
                End If
                'System.IO.File.Copy(Server.MapPath("~" & hidCropFile.Value), Server.MapPath("~/CustomFiles/Property/" & hidCustomerId.Value & "/" & propertyId & "/") & Path.GetFileName(hidCropFile.Value))
                fupPhoto1.PostedFile.SaveAs(Server.MapPath("~/CustomFiles/Property/" & Config.UserId & "/" & propertyId & "/" & fupPhoto1.FileName))
            End If
            If fupPhoto2.HasFile = True Then
                fupPhoto2.PostedFile.SaveAs(Server.MapPath("~/CustomFiles/Property/" & Config.UserId & "/" & propertyId & "/" & fupPhoto2.FileName))
            End If
        Else
            propertyImage = hidPropertyImage.Value

            If Not String.IsNullOrEmpty(hidCropFile.Value) Then
                propertyImage = Path.GetFileName(hidCropFile.Value)
            End If

            If fupPhoto1.HasFile = True Then
                If (Not System.IO.Directory.Exists(Server.MapPath("~/CustomFiles/Property/" & Config.UserId & "/" & hidPropertyId.Value & "/"))) Then
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/CustomFiles/Property/" & Config.UserId & "/" & hidPropertyId.Value & "/"))
                End If
                propertyImage = fupPhoto1.FileName
                'System.IO.File.Copy(Server.MapPath("~" & hidCropFile.Value), Server.MapPath("~/CustomFiles/Property/" & hidCustomerId.Value & "/" & hidPropertyId.Value & "/") & Path.GetFileName(hidCropFile.Value))
                fupPhoto1.PostedFile.SaveAs(Server.MapPath("~/CustomFiles/Property/" & Config.UserId & "/" & hidPropertyId.Value & "/" & fupPhoto1.FileName))
            End If
            If fupPhoto2.HasFile = True Then
                fupPhoto2.PostedFile.SaveAs(Server.MapPath("~/CustomFiles/Property/" & Config.UserId & "/" & hidPropertyId.Value & "/" & fupPhoto2.FileName))
            End If
            objPropertyProcessor.UpdateCustomerPropery(hidPropertyId.Value, CapitalizeName(txtPropertyName.Value.Trim), CapitalizeName(txtPrimaryAddress.Value.Trim()), CapitalizeName(txtPrimaryCity.Value.Trim()), CapitalizeName(txtPrimaryState.Value.Trim()), CapitalizeName(txtPrimaryZip.Value.Trim()), CapitalizeName(txtPrimaryCountry.Value.Trim()), txtPrice.Value.Replace(",", "").Replace("$", "").Split(".")(0).Trim(), txtBedRooms.Value.Trim() & "!" & txtBaths.Value.Trim(), txtSquareFeet.Value.Replace(",", "").Replace("$", "").Split(".")(0).Trim(), propertyImage, hidCustomerId.Value.Trim(), Not chkArchieve.Checked)
        End If
        Response.Redirect("~/AddProperty.aspx?cid=" & hidCustomerId.Value)
    End Sub
    <System.Web.Services.WebMethod()>
    Public Shared Function LoadZip(ByVal zipCode As String) As String
        'major changes in this function to reflect allowing canadian zips
        'check version 102 for the logic that has been deleted
        zipCode = zipCode.Replace(" ", "")
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
    Shared Function CapitalizeName(ByVal name As String) As String
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

    Public Sub LoadPropertyDetails(ByVal id As Integer)
        Dim objPropertyProcessor As New PropertyProcessor()
        Dim dtProperty As New DataTable
        objPropertyProcessor.GetPropertyDetailsById(id, Request.QueryString("cid"), dtProperty)
        If dtProperty.Rows.Count > 0 Then
            txtPropertyName.Value = dtProperty.Rows(0)("PropertyName")
            txtPrimaryAddress.Value = dtProperty.Rows(0)("StreetAddress")
            txtPrimaryCity.Value = dtProperty.Rows(0)("City")
            txtPrimaryState.Value = dtProperty.Rows(0)("State")
            txtPrimaryZip.Value = dtProperty.Rows(0)("Zip")
            txtPrimaryCountry.Value = dtProperty.Rows(0)("Country")
            txtPrice.Value = dtProperty.Rows(0)("Price")
            txtBedRooms.Value = Convert.ToString(dtProperty.Rows(0)("BedBaths")).Split("!")(0)
            txtBaths.Value = Convert.ToString(dtProperty.Rows(0)("BedBaths")).Split("!")(1)
            Try
                txtSquareFeet.Value = dtProperty.Rows(0)("sqFt")
            Catch ex As Exception
                txtSquareFeet.Value = ""
            End Try

            hidPropertyImage.Value = dtProperty.Rows(0)("PropertyImage")
            chkArchieve.Checked = Not Convert.ToBoolean(dtProperty.Rows(0)("Active"))
        End If
    End Sub
    Public Function LoadAllPropertyDetails(ByVal custId As Integer) As DataTable
        Dim objPropertyProcessor As New PropertyProcessor()
        Dim dtProperty As New DataTable
        objPropertyProcessor.GetAllPropertyDetailsByCustomer(custId, Not chkIncludeArchieve.Checked, dtProperty)
        Return dtProperty
    End Function
    Public Shared Function LoadAllPropertyEvaluation(ByVal custId As Integer) As Integer
        Dim objPropertyProcessor As New PropertyProcessor()
        Return objPropertyProcessor.GetAllPropertyValueMapScore(custId)
    End Function

    Protected Sub btnAddProperty_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/AddProperty.aspx?cid=" & hidCustomerId.Value & "&type=new")
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/AddProperty.aspx?cid=" & hidCustomerId.Value)
    End Sub

    Protected Sub chkIncludeArchieve_CheckedChanged(sender As Object, e As EventArgs)
        hidPropertyId.Value = String.Empty
        Dim dtProperty As New DataTable()
        divViewProperty.InnerHtml = ""
        dtProperty = LoadAllPropertyDetails(hidCustomerId.Value)
        If dtProperty.Rows.Count = 0 Then
            divViewProperty.Style.Add("Visibility", "hidden")
            divAddProperty.Style.Add("Visibility", "visible")
            chkIncludeArchieve.Visible = False
        Else
            divViewProperty.Style.Add("Visibility", "visible")
            divAddProperty.Style.Add("Visibility", "hidden")
            chkIncludeArchieve.Visible = True
        End If
        Dim valueMapStatus As Boolean = GetValueMapStatus(hidCustomerId.Value)
        If dtProperty.Rows.Count > 0 Then
            Dim propertyBuilder As New StringBuilder()
            For i As Integer = 0 To dtProperty.Rows.Count - 1
                Dim objProcessor As New PropertyProcessor()
                Dim dtPropertyValueScore As New DataSet
                objProcessor.GetPropertyValueMapScoreById(dtProperty.Rows(i)("PropertyId"), hidCustomerId.Value, dtPropertyValueScore)
                Dim accordianContent As String = System.IO.File.ReadAllText(Server.MapPath("~/CustomFiles/PropertyUI.html"))
                accordianContent = accordianContent.Replace("$divid$", "divPriority" & dtProperty.Rows(i)("PropertyId"))
                accordianContent = accordianContent.Replace("$propid$", dtProperty.Rows(i)("PropertyId"))
                accordianContent = accordianContent.Replace("$id$", dtProperty.Rows(i)("PropertyId"))
                accordianContent = accordianContent.Replace("collapseOne", "collapse" & dtProperty.Rows(i)("PropertyId"))
                accordianContent = accordianContent.Replace("$PropertyName$", dtProperty.Rows(i)("PropertyName"))
                accordianContent = accordianContent.Replace("Price$", Convert.ToInt32(dtProperty.Rows(i)("Price")).ToString("C", CultureInfo.CreateSpecificCulture("en-US")).Replace(".00", ""))
                If String.IsNullOrEmpty(Convert.ToString(dtProperty.Rows(i)("sqft"))) Then
                    accordianContent = accordianContent.Replace("$sqft$", "")
                Else
                    accordianContent = accordianContent.Replace("$sqft$", Convert.ToInt32(dtProperty.Rows(i)("sqft")).ToString("C", CultureInfo.CreateSpecificCulture("en-US")).Replace("$", "").Replace(".00", ""))
                End If

                accordianContent = accordianContent.Replace("$bed$", Convert.ToString(dtProperty.Rows(i)("BedBaths")).Split("!")(0))
                accordianContent = accordianContent.Replace("$baths$", Convert.ToString(dtProperty.Rows(i)("BedBaths")).Split("!")(1))
                accordianContent = accordianContent.Replace("$Street$", dtProperty.Rows(i)("StreetAddress"))
                accordianContent = accordianContent.Replace("$City$", dtProperty.Rows(i)("City"))
                accordianContent = accordianContent.Replace("$State$", dtProperty.Rows(i)("State"))
                accordianContent = accordianContent.Replace("$zip$", dtProperty.Rows(i)("zip"))
                accordianContent = accordianContent.Replace("$href$", "AddProperty.aspx?cid=" & hidCustomerId.Value & "&pid=" & dtProperty.Rows(i)("PropertyId"))
                If String.IsNullOrEmpty(dtProperty.Rows(i)("propertyImage")) Then
                    accordianContent = accordianContent.Replace("$imgId$", "Images/browse.png")
                Else
                    accordianContent = accordianContent.Replace("$imgId$", "CustomFiles/Property/" & Config.UserId & "/" & dtProperty.Rows(i)("PropertyId") & "/" & dtProperty.Rows(i)("propertyImage"))
                End If
                If dtPropertyValueScore.Tables(0).Rows.Count > 0 Then
                    accordianContent = accordianContent.Replace("$btnclass$", "")
                    accordianContent = accordianContent.Replace("$btntext$", "Evaluated")
                Else
                    accordianContent = accordianContent.Replace("$btnclass$", "btn-red")
                    accordianContent = accordianContent.Replace("$btntext$", "Evaluate")
                End If
                If valueMapStatus = False Then
                    accordianContent = accordianContent.Replace("$btndisabled$", "disabled")
                Else
                    accordianContent = accordianContent.Replace("$btndisabled$", "")
                    ' propertyBuilder.Append("<div class='col-md-2'></div>")
                End If

                propertyBuilder.Append("<div class='col-md-4'>").Append(accordianContent).Append("</div>")
                ' propertyBuilder.Append("<div class='col-md-2'></div>")
            Next
            divViewProperty.InnerHtml = propertyBuilder.ToString()
        End If
    End Sub

    Public Sub BindValueProperty(ByVal custId As Integer)
        Dim objPropertyProcessor As New PropertyProcessor()
        Dim ds As New DataSet
        objPropertyProcessor.GetPropertyValueMapInfoByCustomer(custId, ds)
        Dim dtPriority As DataTable = ds.Tables(0)
        Dim dtProrityContent As DataTable = ds.Tables(1)
        If dtPriority.Rows.Count = 0 Then
            hidValueMapProperty.Value = ""
        Else
            hidValueMapProperty.Value = dtPriority.Rows(0)("PriorityID") & "!" & dtPriority.Rows(0)("PriorityName") & "~" & dtPriority.Rows(1)("PriorityID") & "!" & dtPriority.Rows(1)("PriorityName") & "~" & dtPriority.Rows(2)("PriorityID") & "!" & dtPriority.Rows(2)("PriorityName")
            For i As Integer = 0 To dtPriority.Rows.Count - 1
                Dim priorityID As Integer = dtPriority.Rows(i)("PriorityID")
                Dim dtImpacts As New DataTable()
                Dim dtBenefits As New DataTable()
                Try
                    dtImpacts = dtProrityContent.Select("PriorityID=" & priorityID & " and QuestionTypeID=1").CopyToDataTable()
                Catch ex As Exception

                End Try
                Try
                    dtBenefits = dtProrityContent.Select("PriorityID=" & priorityID & " and QuestionTypeID=4").CopyToDataTable()
                Catch ex As Exception

                End Try


                If dtImpacts.Rows.Count > 0 Then
                    Dim impactBuilder As New StringBuilder()

                    For j As Integer = 0 To dtImpacts.Rows.Count - 1
                        impactBuilder.Append(dtImpacts.Rows(j)("Answer") & "~")
                    Next
                    If i = 0 Then
                        hidValueMapPropertyContentImpacts1.Value = impactBuilder.ToString().TrimEnd("~")
                    ElseIf i = 1 Then
                        hidValueMapPropertyContentImpacts2.Value = impactBuilder.ToString().TrimEnd("~")
                    ElseIf i = 2 Then
                        hidValueMapPropertyContentImpacts3.Value = impactBuilder.ToString().TrimEnd("~")
                    End If
                Else
                    If i = 0 Then
                        hidValueMapPropertyContentImpacts1.Value = String.Empty
                    ElseIf i = 1 Then
                        hidValueMapPropertyContentImpacts2.Value = String.Empty
                    ElseIf i = 2 Then
                        hidValueMapPropertyContentImpacts3.Value = String.Empty
                    End If
                End If
                If dtBenefits.Rows.Count > 0 Then
                    Dim BenefitsBuilder As New StringBuilder()
                    For j As Integer = 0 To dtBenefits.Rows.Count - 1
                        BenefitsBuilder.Append(dtBenefits.Rows(j)("Answer") & "~")
                    Next
                    If i = 0 Then
                        hidValueMapPropertyContentBenefits1.Value = BenefitsBuilder.ToString().TrimEnd("~")
                    ElseIf i = 1 Then
                        hidValueMapPropertyContentBenefits2.Value = BenefitsBuilder.ToString().TrimEnd("~")
                    ElseIf i = 2 Then
                        hidValueMapPropertyContentBenefits3.Value = BenefitsBuilder.ToString().TrimEnd("~")
                    End If
                Else
                    If i = 0 Then
                        hidValueMapPropertyContentBenefits1.Value = String.Empty
                    ElseIf i = 1 Then
                        hidValueMapPropertyContentBenefits2.Value = String.Empty
                    ElseIf i = 2 Then
                        hidValueMapPropertyContentBenefits3.Value = String.Empty
                    End If
                End If
            Next
        End If

    End Sub

    <System.Web.Services.WebMethod()>
    Public Shared Function GetPropertyValueMapScore(ByVal customerID As Integer, ByVal propertyID As Integer, ByVal priorityID As Integer) As String
        'major changes in this function to reflect allowing canadian zips
        'check version 102 for the logic that has been deleted
        Dim objPropertyProcessor As New PropertyProcessor()
        Dim dt As New DataTable()
        objPropertyProcessor.GetPropertyValueMapScore(customerID, propertyID, priorityID, dt)
        If dt.Rows.Count = 0 Then
            Return ""
        Else
            'Return dt.Rows(0)("MatchScore") & "~" & HttpContext.Current.Server.UrlEncode(dt.Rows(0)("Note"))
            Return dt.Rows(0)("MatchScore") & "~" & (dt.Rows(0)("Note"))
        End If


    End Function
    <System.Web.Services.WebMethod()>
    Public Shared Function InsertPropertyValueMapScore(ByVal customerID As Integer, ByVal propertyID As Integer, ByVal priorityID As Integer, ByVal score As Integer, ByVal notes As String) As String
        'major changes in this function to reflect allowing canadian zips
        'check version 102 for the logic that has been deleted
        Dim objPropertyProcessor As New PropertyProcessor()
        Dim result As Integer = 0
        Dim dt As New DataTable()
        result = objPropertyProcessor.InsertPropertyValueMapScore(customerID, propertyID, priorityID, score, HttpContext.Current.Server.UrlDecode(notes))
        Dim ds As New DataSet()
        objPropertyProcessor.GetPropertyValueMapScoreById(propertyID, customerID, ds)
        If ds.Tables(0).Rows.Count = 1 Then
            Dim objCustomerProcessor As New CustomerProcessor()
            Dim dtCustomer As New DataTable
            objCustomerProcessor.GetContactInfoByUser(Config.UserId, customerID, dtCustomer)
            Dim vmstatus As String = Convert.ToString(dtCustomer.Rows(0)("VMCustomerStatus"))
            'If vmstatus.ToLower() = "completed" Then
            Dim result1 As Integer = 0
            Dim objToDoProcessor As New ToDoProcessor()
            Dim description As String = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/CustomFiles/Compareanddecide.html"))
            description = description.Replace("$CustomerFirstName$", dtCustomer.Rows(0)("firstName"))
            description = description.Replace("$UserName$", Config.CurrentUserName)
            result1 = objToDoProcessor.InsertToDoDetails("Send Discuss and Decide Letter", description, DateTime.Now, 1, Config.UserId, customerID)
            UserProcessor.InsertUserActivity("To Do", "was created for ", result1, Config.UserId)
        End If
        ' End If
        If result > 1 Then
            Return "success"
        Else
            Return "fail"
        End If


    End Function
    Public Function GetValueMapStatus(ByVal customerID As Integer) As Boolean
        Dim ds As New DataSet()
        Dim isCompleted As Boolean = False
        Dim objPriorityProcessor As New PriorityProcessor()
        objPriorityProcessor.GetValueMapOveriew(customerID, ds)
        Dim dtPriority As DataTable = ds.Tables(1)
        Dim dtPriorityChoices As DataTable = ds.Tables(0)
        Dim Count As Integer = 0
        If dtPriority.Rows.Count = 0 Then
            isCompleted = False
        Else
            Dim Priority1 As Integer = dtPriority.Rows(0)(0)
            Dim Priority2 As Integer = dtPriority.Rows(1)(0)
            Dim Priority3 As Integer = dtPriority.Rows(2)(0)
            Dim dtPriority1 = dtPriorityChoices.Select("PriorityID =" & Priority1).CopyToDataTable()
            Dim dtPriority2 = dtPriorityChoices.Select("PriorityID =" & Priority2).CopyToDataTable()
            Dim dtPriority3 = dtPriorityChoices.Select("PriorityID =" & Priority3).CopyToDataTable()

            Try

                Count = dtPriority1.Select("QuestionType=1")(0)("TotalCount")
            Catch ex As Exception
                Count = 0
            End Try
            Try
                Count = dtPriority2.Select("QuestionType=1")(0)("TotalCount")
            Catch ex As Exception
                Count = 0
            End Try
            Try
                Count = dtPriority3.Select("QuestionType=1")(0)("TotalCount")
            Catch ex As Exception
                Count = 0
            End Try

            Try
                Count = dtPriority1.Select("QuestionType=2")(0)("TotalCount")
            Catch ex As Exception
                Count = 0
            End Try
            Try
                Count = dtPriority2.Select("QuestionType=2")(0)("TotalCount")
            Catch ex As Exception
                Count = 0
            End Try
            Try
                Count = dtPriority3.Select("QuestionType=2")(0)("TotalCount")
            Catch ex As Exception
                Count = 0
            End Try

            Try
                Count = dtPriority1.Select("QuestionType=3")(0)("TotalCount")
            Catch ex As Exception
                Count = 0
            End Try
            Try
                Count = dtPriority2.Select("QuestionType=3")(0)("TotalCount")
            Catch ex As Exception
                Count = 0
            End Try
            Try
                Count = dtPriority3.Select("QuestionType=3")(0)("TotalCount")
            Catch ex As Exception

            End Try

            Try
                Count = dtPriority1.Select("QuestionType=4")(0)("TotalCount")
            Catch ex As Exception
                Count = 0
            End Try
            Try
                Count = dtPriority2.Select("QuestionType=4")(0)("TotalCount")
            Catch ex As Exception
                Count = 0
            End Try
            Try
                Count = dtPriority3.Select("QuestionType=4")(0)("TotalCount")
            Catch ex As Exception
                Count = 0
            End Try
        End If
        If Count > 0 Then
            isCompleted = True
        Else
            isCompleted = False

        End If
        Return isCompleted
    End Function


    'Crop Implementation
    <System.Web.Services.WebMethod()>
    Public Shared Function UploadImage(ByVal imageData As String, ByVal w As String, ByVal h As String, ByVal x As String, ByVal y As String, ByVal custId As String) As String
        Dim fileNameWitPath As String = HttpContext.Current.Server.MapPath("~/Images/") & "test.png"
        'Dim imgFileName As String = Path.GetFileName(imageData)
        'Using fs As FileStream = New FileStream(fileNameWitPath, FileMode.Create)

        '    Using bw As BinaryWriter = New BinaryWriter(fs)
        '        Dim data As Byte() = Convert.FromBase64String(imageData.Split(",")(1))
        '        bw.Write(data)
        '        bw.Close()
        '    End Using
        Dim Imagefilename As String = System.IO.Path.GetFileName(imageData)
        'Dim w As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("Width").ToString())
        'Dim h As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("Height").ToString())
        'Dim x As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("chordx").ToString())
        'Dim y As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("chordy").ToString())
        Dim path As String = HttpContext.Current.Server.MapPath(imageData.Replace(Imagefilename, ""))

        Dim CropImage As Byte() = Crop(HttpContext.Current.Server.MapPath("~" & imageData), w, h, x, y)

        Using ms As MemoryStream = New MemoryStream(CropImage, 0, CropImage.Length)
            ms.Write(CropImage, 0, CropImage.Length)

            Using CroppedImage As SD.Image = SD.Image.FromStream(ms, True)
                Dim SaveTo As String = path & System.IO.Path.GetFileNameWithoutExtension(Imagefilename) & "_thumb" & System.IO.Path.GetExtension(Imagefilename)
                CroppedImage.Save(SaveTo, CroppedImage.RawFormat)

            End Using
        End Using
        Return "/CustomFiles/Property/temp/" & System.IO.Path.GetFileNameWithoutExtension(Imagefilename) & "_thumb" & System.IO.Path.GetExtension(Imagefilename)
        ' End Using
    End Function

    Private Shared Function Crop(ByVal Img As String, ByVal Width As Integer, ByVal Height As Integer, ByVal X As Integer, ByVal Y As Integer) As Byte()
        Try

            Using OriginalImage As SD.Image = SD.Image.FromFile(Img)

                Using bmp As SD.Bitmap = New SD.Bitmap(Width, Height)
                    bmp.SetResolution(OriginalImage.HorizontalResolution, OriginalImage.VerticalResolution)

                    Using Graphic As SD.Graphics = SD.Graphics.FromImage(bmp)
                        Graphic.SmoothingMode = SmoothingMode.AntiAlias
                        Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic
                        Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality
                        Graphic.DrawImage(OriginalImage, New SD.Rectangle(0, 0, Width, Height), X, Y, Width, Height, SD.GraphicsUnit.Pixel)
                        Dim ms As MemoryStream = New MemoryStream()
                        bmp.Save(ms, OriginalImage.RawFormat)
                        Return ms.GetBuffer()
                    End Using
                End Using
            End Using

        Catch Ex As Exception
            Throw (Ex)
        End Try
    End Function

    Private Shared Function GetFileBytes(ByVal f As FileInfo) As Byte()
        Using stream As FileStream = f.OpenRead()
            Dim buffer As Byte() = New Byte(f.Length - 1) {}
            stream.Read(buffer, 0, CInt(f.Length))
            Return buffer
        End Using
    End Function

    Public Sub BindAllPropertiesByCustomerUser(ByVal custID As Integer, ByVal userID As Integer)
        Dim objPropertyProcessor As New PropertyProcessor()
        Dim dt As New DataTable()
        objPropertyProcessor.GetAllPropertyDetailsByCustomerUser(custID, userID, dt)
        If dt.Rows.Count = 0 Then
            ddlProperties.Style.Add("display", "none")
            txtPropertyName.Style.Add("display", "")
            hidAssignProperty.Value = "1"
            rfvddlProperties.Enabled = False
        Else
            ddlProperties.Style.Add("display", "")
            txtPropertyName.Style.Add("display", "none")
            ddlProperties.DataSource = dt
            ddlProperties.DataTextField = "PropertyName"
            ddlProperties.DataValueField = "PropertyId"
            ddlProperties.DataBind()
            ddlProperties.Items.Insert(0, New ListItem("Please Select", ""))
            ddlProperties.Items.Insert(1, New ListItem("Add New", "-1"))
            rfvddlProperties.Enabled = True
        End If
    End Sub

    Protected Sub ddlProperties_SelectedIndexChanged(sender As Object, e As EventArgs)
        'rfvPropertyName.ControlToValidate = "ddlProperties"
        'If ddlProperties.SelectedValue = "" Then
        '    Exit Sub
        'End If
        'If ddlProperties.SelectedValue = "-1" Then
        '    rfvPropertyName.ControlToValidate = "txtPropertyName"
        '    Exit Sub
        'End If
        hidAssignProperty.Value = "1"
        hidPropertyId.Value = ddlProperties.SelectedValue
        LoadPropertyDetails(ddlProperties.SelectedValue)
        divViewProperty.Style.Add("Visibility", "hidden")
        chkIncludeArchieve.Visible = False
        divAddProperty.Style.Add("Visibility", "visible")
        txtPropertyName.Focus()
        divPropertyActions.Visible = True
        'lblPageTitle.InnerText = "Edit Property"
        btnAddProperty.Visible = False
    End Sub
End Class