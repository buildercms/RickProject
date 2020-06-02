Imports RickProject.Business

Public Class SearchToDo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("searchList") Is Nothing Then
        '    Response.Redirect("~/Home.aspx")
        'Else
        '    SearchCustomers(Session("searchList"))
        'End If
        If String.IsNullOrEmpty(Config.LoginUserType) Then
            Response.Redirect("~/Login.aspx")
        End If
        If Not Page.IsPostBack Then
            BindAllTodayToDosByUser(Config.UserId)
            BindAllUpComingToDosByUser(Config.UserId)
        End If
    End Sub





    Public Sub BindAllToDosByUser(ByVal userID As Integer)
        Dim objToDoProcessor As New ToDoProcessor
        Dim dt As New DataTable()
        objToDoProcessor.GetAllToDoListByUser(userID, dt)
        gvTodayToDos.DataSource = dt
        gvTodayToDos.DataBind()
    End Sub
    Public Sub BindAllUpComingToDosByUser(ByVal userID As Integer)
        Dim objToDoProcessor As New ToDoProcessor
        Dim dt As New DataTable()
        objToDoProcessor.GetAllUpComingToDoListByUser(userID, dt)
        Dim todayCount As Integer = 0
        Try
            todayCount = gvTodayToDos.Rows.Count
        Catch ex As Exception

        End Try
        If dt.Rows.Count > 0 Then
            Dim totalRows As Integer = 0
            Try
                totalRows = 10 - todayCount
                If totalRows < 0 Then
                    totalRows = 0

                End If
            Catch ex As Exception

            End Try
            If totalRows > 0 Then
                gvToDos.DataSource = dt.AsEnumerable().Take(totalRows).CopyToDataTable()
                gvToDos.DataBind()
            End If

        Else
            gvToDos.DataSource = dt
            gvToDos.DataBind()
        End If

    End Sub
    Public Sub BindAllTodayToDosByUser(ByVal userID As Integer)
        Dim objToDoProcessor As New ToDoProcessor
        Dim dt As New DataTable()
        objToDoProcessor.GetAllTodayToDoListByUser(userID, dt)
        gvTodayToDos.DataSource = dt
        gvTodayToDos.DataBind()
    End Sub

    Protected Sub gvToDos_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim hidToDoId As HiddenField = DirectCast(e.Row.FindControl("hidToDoId"), HiddenField)
            Dim hidCustId As HiddenField = DirectCast(e.Row.FindControl("hidCustId"), HiddenField)
            Dim aToDoViewLink As HtmlAnchor = DirectCast(e.Row.FindControl("aToDoViewLink"), HtmlAnchor)
            Dim aToDoEditLink As HtmlAnchor = DirectCast(e.Row.FindControl("aToDoEditLink"), HtmlAnchor)
            Dim chkCompleteStatus As CheckBox = DirectCast(e.Row.FindControl("chkCompleteStatus"), CheckBox)
            Dim lblToDoStatus As Label = DirectCast(e.Row.FindControl("lblToDoStatus"), Label)
            If aToDoViewLink.InnerText.Contains("Send Evaluation") And lblToDoStatus.Text <> "Completed" Then
                aToDoViewLink.HRef = "javascript:EvaluationEmail('" & hidCustId.Value & "','" & hidToDoId.Value & "')"
            ElseIf (aToDoViewLink.InnerText.Contains("Send Compare and Decide") Or aToDoViewLink.InnerText.Contains("Send Discuss and Decide")) And lblToDoStatus.Text <> "Completed" Then
                aToDoViewLink.HRef = "javascript:CompareDecideEmail('" & hidCustId.Value & "','" & hidToDoId.Value & "')"
            Else

                aToDoViewLink.HRef = "ViewToDo.aspx?cid=" & hidCustId.Value & "&todoId=" & hidToDoId.Value
            End If
            aToDoEditLink.HRef = "AddToDo.aspx?cid=" & hidCustId.Value & "&todoId=" & hidToDoId.Value
            If lblToDoStatus.Text = "Completed" Then
                chkCompleteStatus.Checked = True
            End If
            'If lblToDoStatus.Text = "Over Due" Then
            '    lblToDoStatus.ForeColor = Drawing.Color.Red
            'End If
        End If
    End Sub
    Protected Sub gvTodayToDos_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim hidToDoId As HiddenField = DirectCast(e.Row.FindControl("hidToDoId"), HiddenField)
            Dim hidCustId As HiddenField = DirectCast(e.Row.FindControl("hidCustId"), HiddenField)
            Dim aToDoViewLink As HtmlAnchor = DirectCast(e.Row.FindControl("aToDoViewLink"), HtmlAnchor)
            Dim aToDoEditLink As HtmlAnchor = DirectCast(e.Row.FindControl("aToDoEditLink"), HtmlAnchor)
            Dim chkCompleteStatus As CheckBox = DirectCast(e.Row.FindControl("chkCompleteStatus"), CheckBox)
            Dim lblToDoStatus As Label = DirectCast(e.Row.FindControl("lblToDoStatus"), Label)
            Dim hidDueDate As HiddenField = DirectCast(e.Row.FindControl("hidDueDate"), HiddenField)
            If aToDoViewLink.InnerText.Contains("Send Evaluation") And lblToDoStatus.Text <> "Completed" Then
                aToDoViewLink.HRef = "javascript:EvaluationEmail('" & hidCustId.Value & "','" & hidToDoId.Value & "')"
            ElseIf (aToDoViewLink.InnerText.Contains("Send Compare and Decide") Or aToDoViewLink.InnerText.Contains("Send Discuss and Decide")) And lblToDoStatus.Text <> "Completed" Then
                aToDoViewLink.HRef = "javascript:CompareDecideEmail('" & hidCustId.Value & "','" & hidToDoId.Value & "')"
            Else

                aToDoViewLink.HRef = "ViewToDo.aspx?cid=" & hidCustId.Value & "&todoId=" & hidToDoId.Value
            End If

            aToDoEditLink.HRef = "AddToDo.aspx?cid=" & hidCustId.Value & "&todoId=" & hidToDoId.Value
            If lblToDoStatus.Text = "Completed" Then
                chkCompleteStatus.Checked = True
            End If
            If (DateTime.Compare(Convert.ToDateTime(hidDueDate.Value).ToShortDateString(), DateTime.Now.ToShortDateString) < 0) Then
                lblToDoStatus.ForeColor = Drawing.Color.Red
            End If
            'If lblToDoStatus.Text = "Over Due" Then
            '    lblToDoStatus.ForeColor = Drawing.Color.Red
            'End If
        End If
    End Sub

    Protected Sub chkCompleteStatus_CheckedChanged(sender As Object, e As EventArgs)
        Dim todoid As Integer = DirectCast(sender, CheckBox).Attributes("todoid")
        Dim objToDoProcessor As New ToDoProcessor()
        objToDoProcessor.UpdateToDOStatus(todoid)
        BindAllTodayToDosByUser(Config.UserId)
        BindAllUpComingToDosByUser(Config.UserId)
    End Sub
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
            result1 = objToDoProcessor.InsertToDoDetails("Schedule Evaluation", "", DateTime.Now.AddDays(2), 1, Config.UserId, customerID)
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
End Class