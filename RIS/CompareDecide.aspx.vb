Imports System.Globalization
Imports MAX.USPS
Imports RickProject.Business
Imports RickProject.DB

Public Class CompareDecide
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Config.LoginUserType) Then
            Response.Redirect("~/Login.aspx")
        End If
        If Not Page.IsPostBack Then
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
            '  BindValueProperty(Request.QueryString("cid"))
            If Not Request.QueryString("type") Is Nothing Then
                divViewProperty.Style.Add("Visibility", "hidden")
                'chkIncludeArchieve.Visible = False
            Else
                If Not Request.QueryString("pid") Is Nothing Then
                    hidPropertyId.Value = Request.QueryString("pid")
                    divViewProperty.Style.Add("Visibility", "hidden")
                    'chkIncludeArchieve.Visible = False
                Else
                    hidPropertyId.Value = String.Empty
                    Dim dtProperty As New DataTable()
                    dtProperty = LoadAllPropertyDetails(hidCustomerId.Value)
                    If dtProperty.Rows.Count = 0 Then
                        divViewProperty.Style.Add("Visibility", "hidden")
                        'chkIncludeArchieve.Visible = False

                    Else
                        divViewProperty.Style.Add("Visibility", "visible")
                        'chkIncludeArchieve.Visible = True

                    End If
                    If dtProperty.Rows.Count > 0 Then
                        Dim propertyBuilder As New StringBuilder()
                        For i As Integer = 0 To dtProperty.Rows.Count - 1
                            Dim objProcessor As New PropertyProcessor()
                            Dim dtPropertyValueScore As New DataSet
                            objProcessor.GetPropertyValueMapScoreById(dtProperty.Rows(i)("PropertyId"), hidCustomerId.Value, dtPropertyValueScore)
                            Dim accordianContent As String = System.IO.File.ReadAllText(Server.MapPath("~/CustomFiles/PropertyUIRank.html"))
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
                            'accordianContent = accordianContent.Replace("$sqft$", Convert.ToInt32(dtProperty.Rows(i)("sqft")).ToString("C", CultureInfo.CreateSpecificCulture("en-US")).Replace("$", "").Replace(".00", ""))
                            accordianContent = accordianContent.Replace("$bed$", Convert.ToString(dtProperty.Rows(i)("BedBaths")).Split("!")(0))
                            accordianContent = accordianContent.Replace("$baths$", Convert.ToString(dtProperty.Rows(i)("BedBaths")).Split("!")(1))
                            accordianContent = accordianContent.Replace("$Street$", dtProperty.Rows(i)("StreetAddress"))
                            accordianContent = accordianContent.Replace("$City$", dtProperty.Rows(i)("City"))
                            accordianContent = accordianContent.Replace("$State$", dtProperty.Rows(i)("State"))
                            accordianContent = accordianContent.Replace("$zip$", dtProperty.Rows(i)("zip"))
                            accordianContent = accordianContent.Replace("$href$", "AddProperty.aspx?cid=" & hidCustomerId.Value & "&pid=" & dtProperty.Rows(i)("PropertyId"))
                            If String.IsNullOrEmpty(dtProperty.Rows(i)("propertyImage")) Then
                                'accordianContent = accordianContent.Replace("$imgId$", "Images/DefaultProperty.png")
                            Else
                                accordianContent = accordianContent.Replace("$imgId$", "CustomFiles/Property/" & Config.UserId & "/" & dtProperty.Rows(i)("PropertyId") & "/" & dtProperty.Rows(i)("propertyImage"))
                            End If
                            If dtPropertyValueScore.Tables(0).Rows.Count > 0 Then
                                accordianContent = accordianContent.Replace("$score$", Convert.ToString(dtPropertyValueScore.Tables(1).Rows(0)(0)).Replace(".0", ""))
                            Else
                                accordianContent = accordianContent.Replace("$score$", "0")

                            End If
                            ' propertyBuilder.Append("<div class='col-md-2'></div>")
                            accordianContent = accordianContent.Replace("$rankvalue$", (i + 1))
                            If Convert.ToBoolean(IIf(String.IsNullOrEmpty(Convert.ToString(dtProperty.Rows(i)("IsSelected"))), False, dtProperty.Rows(i)("IsSelected"))) = True Then
                                accordianContent = accordianContent.Replace("$checked$", "checked")
                            Else
                                accordianContent = accordianContent.Replace("$checked$", "")

                            End If
                            propertyBuilder.Append("<div class='col-md-4 drophere'>").Append(accordianContent).Append("</div>")
                            ' propertyBuilder.Append("<div class='col-md-2'></div>")
                        Next
                        divViewProperty.InnerHtml = propertyBuilder.ToString()
                    End If
                End If

            End If


        End If
    End Sub
    Public Sub LoadCustomer(ByVal customerID As Integer, ByVal userID As Integer)
        Dim objCustomerProcessor As New CustomerProcessor()
        Dim dtCustomer As New DataTable
        objCustomerProcessor.GetContactInfoByUser(userID, customerID, dtCustomer)
        If dtCustomer.Rows.Count > 0 Then

        End If
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
        If Regex.IsMatch(zipCode, "^[0-9]{5}$|^[a-zA-Z][0-9][a-zA-Z][ ]?[0-9][a-zA-Z][0-9]$") Then
            success = GetZip(zipCode, city, state, countryCode)
        Else
        End If
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


    Public Function LoadAllPropertyDetails(ByVal custId As Integer) As DataTable
        Dim objPropertyProcessor As New PropertyProcessor()
        Dim dtProperty As New DataTable
        objPropertyProcessor.GetAllPropertyDetailsByCustomerRank(custId, True, dtProperty)
        Return dtProperty
    End Function
    <System.Web.Services.WebMethod()> Public Shared Function UpdateProperty(ByVal propertyID As Integer, ByVal selected As Integer, customerID As Integer) As String
        ' Return ""
        Dim dt As New DataTable()
        Dim objPropertyProcessor As New PropertyProcessor()


        Dim result As Integer = objPropertyProcessor.UpdatePropertySelection(propertyID, selected, customerID)


        'Return result
        If result > 0 Then
            Return "success"
        Else
            Return "fail"
        End If
    End Function
    <System.Web.Services.WebMethod()> Public Shared Function UpdatePropertyRank(ByVal propertyIDs As String, customerID As Integer) As String
        ' Return ""
        Dim dt As New DataTable()
        Dim objPropertyProcessor As New PropertyProcessor()

        propertyIDs = propertyIDs.TrimEnd(";")
        Dim propertyRanks = propertyIDs.Split(";")

        Dim xmlData As New StringBuilder()
        xmlData.Append("<Properties>")
        For i As Integer = 0 To propertyRanks.Length - 1
            'Case 223
            xmlData.Append("<Property ID=").Append(Chr(34) + propertyRanks(i).Split(",")(0) + Chr(34)).Append(" Rank=").Append(Chr(34) + propertyRanks(i).Split(",")(1) + Chr(34)).Append("></Property>")
            'End Case
        Next
        xmlData.Append("</Properties>")
        Dim result As Integer = objPropertyProcessor.UpdatePropertyRank(xmlData.ToString(), customerID)
        'Return result
        If result > 0 Then
            Return "success"
        Else
            Return "fail"
        End If
    End Function
End Class