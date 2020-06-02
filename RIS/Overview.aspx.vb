Imports RickProject.Business

Public Class Overview
    Inherits System.Web.UI.Page
    Dim vmcategoryid As Integer = 0
    Dim vmstatus As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Config.LoginUserType) Then
            Response.Redirect("~/Login.aspx")
        End If
        If Not Page.IsPostBack Then
            'Session("valueMapMode") = ""
            LoadCustomer(Request.QueryString("cid"), Config.UserId)
            Dim objCustomerProcessor As New CustomerProcessor()
            Dim dt, dt1 As New DataTable()
            hidCustomerID.Value = Request.QueryString("cid")
            objCustomerProcessor.GetContactInfoByUser(Config.UserId, Request.QueryString("cid"), dt1)
            lblCustomerName.InnerText = dt1.Rows(0)("FirstName") & " " & dt1.Rows(0)("LastName")
            lnkCustomerName.Text = dt1.Rows(0)("FirstName") & " " & dt1.Rows(0)("LastName")
            Dim primaryEmailId As String = Convert.ToString(dt1.Rows(0)("EmailId"))
            Dim secondaryEmailId As String = Convert.ToString(dt1.Rows(0)("SecondaryEmailId"))
            If String.IsNullOrEmpty(primaryEmailId) Then
                lblCustomerEmail.InnerText = secondaryEmailId.Trim()
            Else
                lblCustomerEmail.InnerText = primaryEmailId.Trim()
            End If
            If lblCustomerEmail.InnerText = "" Then
                Dim btnClass As String = btnSendVMLink.CssClass
                btnSendVMLink.CssClass = btnClass & " btn-disabled"
            End If

            aOverView.HRef = "Overview.aspx?cid=" & Request.QueryString("cid")
            aCreateValueMap.HRef = "ValueMap.aspx?cid=" & Request.QueryString("cid")
            aCreateProperty.HRef = "AddProperty.aspx?cid=" & Request.QueryString("cid")
            aCompareDecide.HRef = "CompareDecide.aspx?cid=" & Request.QueryString("cid")
            BindValueMapLinkByCustomer(Request.QueryString("cid"), Config.UserId)
            If LoadAllPropertyEvaluation(Request.QueryString("cid")) = 0 Then
                aCompareDecide.Disabled = True
                aCompareDecide.HRef = ""
                aCompareDecide.Style.Add("color", "lightgray")
                aCompareDecide.Style.Add("cursor", "text")
            Else
                btnRedirectVMLink.Enabled = False
                'btnSendVMLink.Enabled = False
            End If
            BindPropertyDetails(Request.QueryString("cid"))
            BindAllToDosByUser(Config.UserId, Request.QueryString("cid"))
            LoadValueMap(Request.QueryString("cid"))
            BindVMCategoryByGGID(Config.LoginUserType)
        End If
    End Sub
    Public Sub LoadCustomer(ByVal customerID As Integer, ByVal userID As Integer)
        Dim objCustomerProcessor As New CustomerProcessor()
        Dim dtCustomer As New DataTable
        objCustomerProcessor.GetContactInfoByUser(userID, customerID, dtCustomer)
        If dtCustomer.Rows.Count > 0 Then
            lblViewContactFirstName.Text = Convert.ToString(dtCustomer.Rows(0)("FirstName"))
            lblViewContactSecondaryFirstName.Text = Convert.ToString(dtCustomer.Rows(0)("SecondaryFirstName"))
            lblViewContactSecondaryLastName.Text = Convert.ToString(dtCustomer.Rows(0)("SecondaryLastName"))
            lblViewContactLastName.Text = Convert.ToString(dtCustomer.Rows(0)("LastName"))
            lblViewContactWorkPhone.Text = Config.FormatPhoneNumber(Convert.ToString(dtCustomer.Rows(0)("WorkPhone")))
            lblViewContactMobilePhone.Text = Config.FormatPhoneNumber(Convert.ToString(dtCustomer.Rows(0)("MobilePhone")))
            lblViewContactEmail.Text = Convert.ToString(dtCustomer.Rows(0)("EmailId"))
            If lblViewContactEmail.Text.Trim() = "" Then
                lblViewContactEmail.Text = Convert.ToString(dtCustomer.Rows(0)("SecondaryEmailId"))
            End If
            lblViewContactStreetAddress.Text = Convert.ToString(dtCustomer.Rows(0)("StreetAddress"))
            'lblViewContactState.Text = Convert.ToString(dtCustomer.Rows(0)("State"))
            lblViewContactCity.Text = Convert.ToString(dtCustomer.Rows(0)("City")) & ", " & Convert.ToString(dtCustomer.Rows(0)("State"))
            lblViewContactZip.Text = Convert.ToString(dtCustomer.Rows(0)("Fax"))

            lblViewContactNotes.Text = Convert.ToString(GetNotesByCustomer(userID, customerID))

            lblViewContactVMCategory.Text = Convert.ToString(dtCustomer.Rows(0)("Type"))
            lblViewContactCommunity.Text = Convert.ToString(dtCustomer.Rows(0)("CommunityName"))
            Try
                vmcategoryid = Convert.ToInt32(dtCustomer.Rows(0)("VMCategoryId"))
            Catch ex As Exception
                vmcategoryid = 0
            End Try
            vmstatus = Convert.ToString(dtCustomer.Rows(0)("VMCustomerStatus"))
        End If
    End Sub
    Public Sub BindAllToDosByUser(ByVal userID As Integer, ByVal custId As Integer)
        Dim objToDoProcessor As New ToDoProcessor
        Dim dt As New DataTable()
        objToDoProcessor.GetAllToDoListByCustomer(userID, custId, dt)
        Dim todoBuilder As New StringBuilder()
        For i As Integer = 0 To dt.Rows.Count - 1
            todoBuilder.Append("<tr>")
            'todoBuilder.Append("<td> <a href='ViewTodo.aspx?todoid=" & dt.Rows(i)("Id") & "&cid=" & Request.QueryString("cid") & "'>").Append(dt.Rows(i)("Title")).Append("</a></td>")
            If dt.Rows(i)("Title").ToString().Contains("Send Evaluation") And dt.Rows(i)("ToDoStatus").ToString() <> "Completed" Then
                todoBuilder.Append("<td>").Append("<a style='cursor:pointer;color:#5addbe;' onclick='Evaluate(" & dt.Rows(i)("Id") & ")' >" & dt.Rows(i)("Title").ToString() & "</a>").Append("</td>")
            ElseIf (dt.Rows(i)("Title").ToString().Contains("Send Compare and Decide") Or dt.Rows(i)("Title").ToString().Contains("Send Discuss and Decide")) And dt.Rows(i)("ToDoStatus").ToString() <> "Completed" Then
                todoBuilder.Append("<td>").Append("<a style='cursor:pointer;color:#5addbe' onclick='CompareDecide(" & dt.Rows(i)("Id") & ")' >" & dt.Rows(i)("Title").ToString() & "</a>").Append("</td>")
            Else
                todoBuilder.Append("<td>").Append(dt.Rows(i)("Title")).Append("</td>")
            End If
            ' todoBuilder.Append("<td>").Append(dt.Rows(i)("Title")).Append("</td>")
            todoBuilder.Append("<td>").Append(Convert.ToDateTime(dt.Rows(i)("DueDate")).ToString("MM/dd/yy")).Append("</td>")
            todoBuilder.Append("<td>").Append(dt.Rows(i)("ToDoStatus")).Append("</td>")
            todoBuilder.Append("<td  class='text-center'>").Append("<a href='AddTodo.aspx?todoid=" & dt.Rows(i)("Id") & "&cid=" & Request.QueryString("cid") & "&mode=overview'>Edit</a>").Append("</td>")
            todoBuilder.Append("</tr>")
        Next
        divHtmlContent.Value = todoBuilder.ToString()
    End Sub

    Public Sub LoadValueMap(ByVal customerID As Integer)
        Dim ds As New DataSet()
        Dim objPriorityProcessor As New PriorityProcessor()
        objPriorityProcessor.GetValueMapOveriew(customerID, ds)
        Dim dtPriority As DataTable = ds.Tables(1)
        Dim dtPriorityChoices As DataTable = ds.Tables(0)
        If dtPriority.Rows.Count = 0 Then
        Else
            Dim Priority1 As Integer = dtPriority.Rows(0)(0)
            Dim Priority2 As Integer = dtPriority.Rows(1)(0)
            Dim Priority3 As Integer = dtPriority.Rows(2)(0)
            Dim dtPriority1 = dtPriorityChoices.Select("PriorityID =" & Priority1).CopyToDataTable()
            Dim dtPriority2 = dtPriorityChoices.Select("PriorityID =" & Priority2).CopyToDataTable()
            Dim dtPriority3 = dtPriorityChoices.Select("PriorityID =" & Priority3).CopyToDataTable()
            lblPriority1.Text = dtPriority1.Rows(0)("PriorityName")
            lblPriority2.Text = dtPriority2.Rows(0)("PriorityName")
            lblPriority3.Text = dtPriority3.Rows(0)("PriorityName")
            Try
                lblImpact1.Text = dtPriority1.Select("QuestionType=1")(0)("TotalCount")
            Catch ex As Exception

            End Try
            Try
                lblImpact2.Text = dtPriority2.Select("QuestionType=1")(0)("TotalCount")
            Catch ex As Exception

            End Try
            Try
                lblImpact3.Text = dtPriority3.Select("QuestionType=1")(0)("TotalCount")
            Catch ex As Exception

            End Try

            Try
                lblCurrent1.Text = dtPriority1.Select("QuestionType=2")(0)("TotalCount")
            Catch ex As Exception

            End Try
            Try
                lblCurrent2.Text = dtPriority2.Select("QuestionType=2")(0)("TotalCount")
            Catch ex As Exception

            End Try
            Try
                lblCurrent3.Text = dtPriority3.Select("QuestionType=2")(0)("TotalCount")
            Catch ex As Exception

            End Try

            Try
                lblFuture1.Text = dtPriority1.Select("QuestionType=3")(0)("TotalCount")
            Catch ex As Exception

            End Try
            Try
                lblFuture2.Text = dtPriority2.Select("QuestionType=3")(0)("TotalCount")
            Catch ex As Exception

            End Try
            Try
                lblFuture3.Text = dtPriority3.Select("QuestionType=3")(0)("TotalCount")
            Catch ex As Exception

            End Try

            Try
                lblBenefits1.Text = dtPriority1.Select("QuestionType=4")(0)("TotalCount")
            Catch ex As Exception

            End Try
            Try
                lblBenefits2.Text = dtPriority2.Select("QuestionType=4")(0)("TotalCount")
            Catch ex As Exception

            End Try
            Try
                lblBenefits3.Text = dtPriority3.Select("QuestionType=4")(0)("TotalCount")
            Catch ex As Exception

            End Try


        End If
    End Sub

    Protected Sub lnkEditContact_Click(sender As Object, e As EventArgs) Handles lnkEditContact.Click, lnkCustomerName.Click
        Response.Redirect("~/AddContact.aspx?cid=" & Request.QueryString("cid") & "&mode=overview")
    End Sub

    Protected Sub btnAddToDo_Click(sender As Object, e As EventArgs) Handles btnAddToDo.Click
        Response.Redirect("~/AddToDo.aspx?cid=" & Request.QueryString("cid"))
    End Sub
    Public Function LoadAllPropertyDetails(ByVal custId As Integer) As DataTable
        Dim objPropertyProcessor As New PropertyProcessor()
        Dim dtProperty As New DataTable
        objPropertyProcessor.GetAllPropertyDetailsByCustomer(custId, True, dtProperty)
        Return dtProperty
    End Function
    Public Function LoadAllPropertyDetailsRank(ByVal custId As Integer) As DataTable
        Dim objPropertyProcessor As New PropertyProcessor()
        Dim dtProperty As New DataTable
        objPropertyProcessor.GetAllPropertyDetailsByCustomerRank(custId, True, dtProperty)
        Return dtProperty
    End Function
    Public Sub BindPropertyDetails(ByVal custId As Integer)
        Dim dtProperty As DataTable = LoadAllPropertyDetailsRank(custId)
        Dim propertyBuilder As New StringBuilder()
        For i As Integer = 0 To dtProperty.Rows.Count - 1
            Dim objProcessor As New PropertyProcessor()
            Dim dtPropertyValueScore As New DataSet
            objProcessor.GetPropertyValueMapScoreById(dtProperty.Rows(i)("PropertyId"), custId, dtPropertyValueScore)
            propertyBuilder.Append("<tr>")
            propertyBuilder.Append("<td class='street'>").Append(dtProperty.Rows(i)("StreetAddress")).Append("<br/>").Append(dtProperty.Rows(i)("city")).Append(", ").Append(dtProperty.Rows(i)("state")).Append(" ").Append(dtProperty.Rows(i)("Zip")).Append("</td>")
            If Convert.ToBoolean(IIf(String.IsNullOrEmpty(Convert.ToString(dtProperty.Rows(i)("IsSelected"))), False, dtProperty.Rows(i)("IsSelected"))) = True Then
                propertyBuilder.Append("<td style='vertical-align:middle; text-align:center;' class='propertySelected'><input style='vertical-align:middle;' type='checkbox' onclick='EvaluateProperty(" & dtProperty.Rows(i)("PropertyId") & ")' checked='checked' id='chk" & dtProperty.Rows(i)("PropertyId") & "'/></td>")
            Else
                propertyBuilder.Append("<td style='vertical-align:middle;  text-align:center;' class='propertySelected'><input style='vertical-align:middle;' type='checkbox' onclick='EvaluateProperty(" & dtProperty.Rows(i)("PropertyId") & ")'  id='chk" & dtProperty.Rows(i)("PropertyId") & "'/></td>")
            End If
            If dtPropertyValueScore.Tables(0).Rows.Count > 0 Then
                propertyBuilder.Append("<td class='evaluation'>Yes</td>")
            Else
                propertyBuilder.Append("<td class='evaluation'>No</td>")
            End If
            propertyBuilder.Append("<td class='rank'>" & (i + 1) & "</td>")
            propertyBuilder.Append("</tr>")
        Next
        hidPropertyContent.Value = propertyBuilder.ToString()

    End Sub

    Protected Sub btnSendVMLink_ServerClick(sender As Object, e As EventArgs)
        Dim user As User = Session("CurrentLogin")
        Dim subject As String = "Value Map Link for " & lblViewContactFirstName.Text.Trim & " " & lblViewContactLastName.Text.Trim
        If Not String.IsNullOrEmpty(lblViewContactSecondaryFirstName.Text) Then
            subject = subject & " and " & lblViewContactSecondaryFirstName.Text.Trim() & " " & lblViewContactSecondaryLastName.Text.Trim
        End If
        subject = subject & " from " & user.FirstName & " " & user.LastName
        Dim body As String = ConfigurationManager.AppSettings("vmlink")
        Dim rawURL As String = HttpContext.Current.Request.Url.AbsoluteUri
        If rawURL.ToLower().Contains("reimaginesellinglogin.com") Then
            body = ConfigurationManager.AppSettings("vmprodlink")
        End If
        body = body.Replace("$customerid$", Request.QueryString("cid"))
        body = body.Replace("$userid$", Config.UserId)
        Dim htmlBody As String = System.IO.File.ReadAllText(Server.MapPath("~/CustomFiles/VMLinkBodyContent.html"))
        htmlBody = htmlBody.Replace("$href$", body)
        htmlBody = htmlBody.Replace("$custName$", lblViewContactFirstName.Text.Trim)

        Dim address As String = Common.FormatAddress("", user.OfficeCity, user.OfficeState, user.OfficeZip)
        htmlBody = htmlBody.Replace("$realtorStreet$", user.OfficeStreet)
        htmlBody = htmlBody.Replace("$realtorAddress$", address)
        htmlBody = htmlBody.Replace("$realtorPhone$", user.Phone)
        htmlBody = htmlBody.Replace("$realtorName$", user.FirstName & " " & user.LastName)
        Dim imgURL As String = body.Split("ValueMapCustomer.aspx")(0) & "CustomFiles/UserImages/" & Config.UserId & "/" & Config.UserId & ".png" & "?" & DateTime.Now.Ticks
        htmlBody = htmlBody.Replace("$imgLogo$", imgURL)

        Dim fromAddress As String = user.Email
        Dim senderEmail As String = "valuedesk@yourvaluestory.com"
        If String.IsNullOrEmpty(fromAddress) Then
            fromAddress = "valuedesk@yourvaluestory.com"
            senderEmail = ""
        End If
        'Case 1630
        senderEmail = ""
        fromAddress = "ValueDesk@yourvaluestory.com"
        'End case
        Dim success As Boolean = Config.SendMail(lblCustomerEmail.InnerText, "", fromAddress, subject, htmlBody, "",, senderEmail)
        If success = True Then
            Dim objPriorityProcessor As New PriorityProcessor()
            objPriorityProcessor.InsertValueMapLink(Request.QueryString("cid"), Config.UserId)
            UserProcessor.InsertUserActivity("vm link", "was sent to ", Request.QueryString("cid"), Config.UserId)
        End If
        BindValueMapLinkByCustomer(Request.QueryString("cid"), Config.UserId)
    End Sub
    Protected Sub btnGetVMLink_ServerClick(sender As Object, e As EventArgs)

        Dim body As String = ConfigurationManager.AppSettings("vmlink")
        Dim rawURL As String = HttpContext.Current.Request.Url.AbsoluteUri
        If rawURL.ToLower().Contains("reimaginesellinglogin.com") Then
            body = ConfigurationManager.AppSettings("vmprodlink")
        End If
        body = body.Replace("$customerid$", Request.QueryString("cid"))
        body = body.Replace("$userid$", Config.UserId)
        Response.Write("<script>")
        Response.Write("window.open('" & body & "&realtor=1" & "' ,'_blank')")
        Response.Write("</script>")
    End Sub
    Protected Sub btnGetVMOLink_ServerClick(sender As Object, e As EventArgs)
        If Session("valueMapMode") = "VMO" Then
            Response.Redirect("ValueMap.aspx?cid=" & Request.QueryString("cid"))
        End If
        Dim body As String = ConfigurationManager.AppSettings("vmlink")
        Dim rawURL As String = HttpContext.Current.Request.Url.AbsoluteUri
        If rawURL.ToLower().Contains("reimaginesellinglogin.com") Then
            body = ConfigurationManager.AppSettings("vmprodlink")
        End If
        body = body.Replace("$customerid$", Request.QueryString("cid"))
        body = body.Replace("$userid$", Config.UserId)
        body = body.Replace("ValueMapCustomer.aspx", "ValueMapOCustomer.aspx")
        Response.Write("<script>")
        Response.Write("window.open('" & body & "&realtor=1" & "' ,'_blank')")
        Response.Write("</script>")
    End Sub
    Public Sub BindValueMapLinkByCustomer(ByVal custId As Integer, ByVal userId As Integer)
        pVMLink.Visible = False
        Dim dt As New DataTable()
        Dim objPriorityProcessor As New PriorityProcessor()
        objPriorityProcessor.GetValueMapCompareDecideLink(custId, userId, dt)
        Dim vmLinkBuilder As New StringBuilder()
        If dt.Rows.Count > 0 Then
            pVMLink.Visible = True

            For i As Integer = 0 To dt.Rows.Count - 1
                vmLinkBuilder.Append("<p style='margin-top:0px; margin-bottom:0px;'>Discuss & Decide email sent: ").Append(Convert.ToDateTime(dt.Rows(i)("SentDate")).ToString("MM/dd/yyyy")).Append("</p>")
            Next

        End If
        dt = New DataTable()
        objPriorityProcessor.GetValueMapEvaluationLink(custId, userId, dt)
        If dt.Rows.Count > 0 Then
            pVMLink.Visible = True

            For i As Integer = 0 To dt.Rows.Count - 1
                vmLinkBuilder.Append("<p style='margin-top:0px; margin-bottom:0px;'>Evaluation email sent: ").Append(Convert.ToDateTime(dt.Rows(i)("SentDate")).ToString("MM/dd/yyyy")).Append("</p>")
            Next

        End If
        dt = New DataTable()
        objPriorityProcessor.GetValueMapLink(custId, userId, dt)
        If dt.Rows.Count > 0 Then
            pVMLink.Visible = True

            For i As Integer = 0 To dt.Rows.Count - 1
                vmLinkBuilder.Append("<p style='margin-top:0px; margin-bottom:0px;'>VM link sent: ").Append(Convert.ToDateTime(dt.Rows(i)("SentDate")).ToString("MM/dd/yyyy")).Append("</p>")
            Next

        End If
        pVMLink.InnerHtml = vmLinkBuilder.ToString()
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
                    GetNotes &= prefix & CapitalizeName(note) & "<br/>"
                End If

            Next

        End If
        Return GetNotes
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
        If dt.Rows.Count > 0 Then
            Dim resultRow() As DataRow = dt.Select("VMCategoryId=" & vmcategoryid)
            If resultRow.Count = 0 Then
                btnRedirectVMLink.Enabled = False
                btnSendVMLink.Enabled = False
            End If
        Else
            btnRedirectVMLink.Enabled = False
            btnSendVMLink.Enabled = False
        End If
        If vmstatus.ToLower() = "completed" Or vmstatus.ToLower() = "sold" Then
            'btnRedirectVMLink.Enabled = False
            btnSendVMLink.Enabled = False
        End If


    End Sub
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
    <System.Web.Services.WebMethod()> Public Shared Function EvaluateToDo(ByVal todoID As Integer, customerID As Integer) As String
        ' Return ""
        Dim dt As New DataTable()
        Dim objToDoProcessor As New ToDoProcessor()
        Dim result As Integer = objToDoProcessor.UpdateToDoDetailsById(Config.UserId, todoID)
        Dim user As User = HttpContext.Current.Session("CurrentLogin")
        Dim fromAddress As String = user.Email
        Dim senderEmail As String = "valuedesk@yourvaluestory.com"
        If String.IsNullOrEmpty(fromAddress) Then
            fromAddress = "valuedesk@yourvaluestory.com"
            senderEmail = ""
        End If
        'Case 1630
        senderEmail = ""
        fromAddress = "ValueDesk@yourvaluestory.com"
        'End case
        Dim objCustomerProcessor As New CustomerProcessor()
        Dim dtCustomer As New DataTable
        objCustomerProcessor.GetContactInfoByUser(Config.UserId, customerID, dtCustomer)
        Dim description As String = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/CustomFiles/Evaluate.html"))
        description = description.Replace("$CustomerFirstName$", dtCustomer.Rows(0)("firstName"))
        description = description.Replace("$UserName$", user.FirstName & " " & user.LastName)
        description = description.Replace("$imgSrc$", HttpContext.Current.Server.MapPath("~/Images/Taylor.png"))
        Dim primaryEmailId As String = Convert.ToString(dtCustomer.Rows(0)("EmailId"))
        Dim secondaryEmailId As String = Convert.ToString(dtCustomer.Rows(0)("SecondaryEmailId"))
        Dim email As String = String.Empty
        If String.IsNullOrEmpty(primaryEmailId) Then
            email = secondaryEmailId.Trim()
        Else
            email = primaryEmailId.Trim()
        End If
        Dim success As Boolean = Config.SendMail(email, "", fromAddress, "Evaluation Email", description, "",, senderEmail)
        If success = True Then
            Dim objPriorityProcessor As New PriorityProcessor()
            objPriorityProcessor.InsertValueMapEvaluationLink(customerID, Config.UserId)
            UserProcessor.InsertUserActivity("Evaluation email", "was sent to ", customerID, Config.UserId)
            Dim result1 As Integer = 0
            result1 = objToDoProcessor.InsertToDoDetails("Schedule Evaluation", "", DateTime.Now.AddDays(2), 1, user.UserId, customerID)
            UserProcessor.InsertUserActivity("To Do", "was created for ", result1, Config.UserId)
        End If
        'Return result
        If result > 0 Then
            Return "success"
        Else
            Return "fail"
        End If
    End Function
    <System.Web.Services.WebMethod()> Public Shared Function CompareDecideToDo(ByVal todoID As Integer, customerID As Integer) As String
        ' Return ""
        Dim dt As New DataTable()
        Dim objToDoProcessor As New ToDoProcessor()
        Dim result As Integer = objToDoProcessor.UpdateToDoDetailsById(Config.UserId, todoID)
        Dim user As User = HttpContext.Current.Session("CurrentLogin")
        Dim fromAddress As String = user.Email
        Dim senderEmail As String = "valuedesk@yourvaluestory.com"
        If String.IsNullOrEmpty(fromAddress) Then
            fromAddress = "valuedesk@yourvaluestory.com"
            senderEmail = ""
        End If
        'Case 1630
        senderEmail = ""
        fromAddress = "ValueDesk@yourvaluestory.com"
        'End case
        Dim objCustomerProcessor As New CustomerProcessor()
        Dim dtCustomer As New DataTable
        objCustomerProcessor.GetContactInfoByUser(Config.UserId, customerID, dtCustomer)
        Dim description As String = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/CustomFiles/Compareanddecide.html"))
        description = description.Replace("$CustomerFirstName$", dtCustomer.Rows(0)("firstName"))
        description = description.Replace("$UserName$", user.FirstName & " " & user.LastName)
        description = description.Replace("$imgSrc$", HttpContext.Current.Server.MapPath("~/Images/Taylor.png"))
        Dim primaryEmailId As String = Convert.ToString(dtCustomer.Rows(0)("EmailId"))
        Dim secondaryEmailId As String = Convert.ToString(dtCustomer.Rows(0)("SecondaryEmailId"))
        Dim email As String = String.Empty
        If String.IsNullOrEmpty(primaryEmailId) Then
            email = secondaryEmailId.Trim()
        Else
            email = primaryEmailId.Trim()
        End If
        Dim success As Boolean = Config.SendMail(email, "", fromAddress, "Discuss & Decide", description, "",, senderEmail)
        If success = True Then
            Dim objPriorityProcessor As New PriorityProcessor()
            objPriorityProcessor.InsertValueMapCompareDecideLink(customerID, Config.UserId)
            UserProcessor.InsertUserActivity("Discuss & Decide email", "was sent to ", customerID, Config.UserId)
            Dim result1 As Integer = 0
            result1 = objToDoProcessor.InsertToDoDetails("Schedule Discuss and Decide", "", DateTime.Now.AddDays(2), 1, Config.UserId, customerID)
            UserProcessor.InsertUserActivity("To Do", "was created for ", result1, Config.UserId)
        End If
        'Return result
        If result > 0 Then
            Return "success"
        Else
            Return "fail"
        End If
    End Function
    Public Function LoadAllPropertyEvaluation(ByVal custId As Integer) As Integer
        Dim objPropertyProcessor As New PropertyProcessor()
        Return objPropertyProcessor.GetAllPropertyValueMapScore(custId)
    End Function
End Class