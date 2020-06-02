Imports RickProject.Business
Imports RickProject.DB

Public Class EditProfile
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Config.LoginUserType) Then
            Response.Redirect("~/Login.aspx")
        End If
        If Not Page.IsPostBack Then
            BindUserProfileInfo(Config.UserId)
            txtPrimaryFirstName.Focus()
        End If
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
    Public Sub BindUserProfileInfo(ByVal userID As Integer)
        Dim dt As New DataTable()
        Dim objUserProcessor As New UserProcessor
        objUserProcessor.GetUserProfileInfo(userID, dt)
        txtPrimaryFirstName.Value = CapitalizeName(Convert.ToString(dt.Rows(0)("FirstName")))
        txtPrimaryLastName.Value = CapitalizeName(Convert.ToString(dt.Rows(0)("LastName")))
        'lblViewCompany.Text = Convert.ToString(dt.Rows(0)(""))
        'lblViewCompany.Text = "Coldwell Banker"
        txtPrimaryMobilePhone.Value = Config.FormatPhoneNumber(Convert.ToString(dt.Rows(0)("CellPhone")))
        txtPrimaryEmail.Value = Convert.ToString(dt.Rows(0)("EmailId"))

        txtPrimaryAddress.Value = CapitalizeName(Convert.ToString(dt.Rows(0)("OfficeStreet")))
        txtPrimaryCity.Value = CapitalizeName(Convert.ToString(dt.Rows(0)("OfficeCity")))
        txtPrimaryState.Value = CapitalizeName(Convert.ToString(dt.Rows(0)("OfficeState")))
        txtPrimaryCountry.Value = CapitalizeName(Convert.ToString(dt.Rows(0)("OfficeCountry")))
        txtPrimaryZip.Value = Convert.ToString(dt.Rows(0)("OfficeZip"))
        txtCompanyName.Value = Convert.ToString(dt.Rows(0)("CompanyName"))
    End Sub

    Protected Sub btnSaveProfile_Click(sender As Object, e As EventArgs)
        If Page.IsValid Then
            Dim objUser As New User()
            Dim objUserProcessor As New UserProcessor()
            objUser.FirstName = CapitalizeName(txtPrimaryFirstName.Value.Trim())
            objUser.LastName = CapitalizeName(txtPrimaryLastName.Value.Trim())
            objUser.Email = txtPrimaryEmail.Value.Trim()
            objUser.Phone = txtPrimaryMobilePhone.Value.Trim()
            objUser.OfficeStreet = CapitalizeName(txtPrimaryAddress.Value.Trim())
            objUser.OfficeState = CapitalizeName(txtPrimaryState.Value.Trim())
            objUser.OfficeCity = CapitalizeName(txtPrimaryCity.Value.Trim())
            objUser.OfficeCountry = CapitalizeName(txtPrimaryCountry.Value.Trim())
            objUser.OfficeZip = txtPrimaryZip.Value.Trim()
            objUser.CompanyName = txtCompanyName.Value.Trim()
            objUser.UserId = Config.UserId
            objUserProcessor.UpdateUserProfile(objUser)
            btnCancel_Click(e, Nothing)
        End If
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        If Request.QueryString("mode") = "view" Then
            Response.Redirect("~/MyProfile.aspx")
        End If
    End Sub

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
End Class