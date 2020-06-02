Imports RickProject.Business

Public Class Home
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Config.LoginUserType) Then
            Response.Redirect("~/Login.aspx")
        End If
        If Not Page.IsPostBack Then
            If Not Session("riscid") Is Nothing Then
                Response.Redirect("overview.aspx?cid=" & Session("riscid"))
                Session("riscid") = Nothing
            End If
            Dim user As RickProject.Business.User = Session("CurrentLogin")
            lblUserName.InnerHtml = user.FirstName
            lblUserName1.InnerHtml = user.FirstName
            Dim dt As New DataTable()
            dt = UserLoginCount(Config.UserId)
            If (dt.Rows.Count = 0) Then
                divNewCustomer.Style.Add("display", "")
                divReturnCustomer.Style.Add("display", "none")
            Else
                divNewCustomer.Style.Add("display", "none")
                divReturnCustomer.Style.Add("display", "")
                BindAllTodayToDosByUser(Config.UserId)
                BindAllToDosByUser(Config.UserId)
                BindAllActivityByUser(Config.UserId)
            End If

        End If
    End Sub

    Public Sub BindAllToDosByUser(ByVal userID As Integer)
        Dim objToDoProcessor As New ToDoProcessor
        Dim dt As New DataTable()
        objToDoProcessor.GetAllUpComingToDoListByUser(userID, dt)
        lvToDo.DataSource = dt
        lvToDo.DataBind()
    End Sub
    Public Sub BindAllTodayToDosByUser(ByVal userID As Integer)
        Dim objToDoProcessor As New ToDoProcessor
        Dim dt As New DataTable()
        objToDoProcessor.GetAllToDoListByUserforHome(userID, dt)
        lvTodayToDo.DataSource = dt
        lvTodayToDo.DataBind()
    End Sub
    Public Sub BindAllActivityByUser(ByVal userID As Integer)
        Dim objUserProcessor As New UserProcessor
        Dim dt As New DataTable()
        objUserProcessor.GetUserActivity(userID, dt)
        lvRecentActivity.DataSource = dt
        lvRecentActivity.DataBind()
    End Sub
    Public Function UserLoginCount(ByVal userID As Integer) As DataTable
        Dim dt As New DataTable()
        Dim objUserProcessor As New UserProcessor()
        objUserProcessor.GetUserLogin(userID, dt)
        Return dt
    End Function
    Protected Sub lvToDo_ItemDataBound(sender As Object, e As ListViewItemEventArgs) Handles lvToDo.ItemDataBound, lvTodayToDo.ItemDataBound
        If e.Item.ItemType = ListViewItemType.DataItem Then
            Dim hidToDoId As HiddenField = DirectCast(e.Item.FindControl("hidToDoId"), HiddenField)
            Dim hidCustId As HiddenField = DirectCast(e.Item.FindControl("hidCustId"), HiddenField)
            Dim aToDoViewLink As HtmlAnchor = DirectCast(e.Item.FindControl("aToDoViewLink"), HtmlAnchor)
            Dim aToDoEditLink As HtmlAnchor = DirectCast(e.Item.FindControl("aToDoEditLink"), HtmlAnchor)
            Dim aCustomerLink As HtmlAnchor = DirectCast(e.Item.FindControl("aCustomerLink"), HtmlAnchor)
            Dim lblTodoTitle As Label = DirectCast(e.Item.FindControl("lblTodoTitle"), Label)
            ' Dim chkCompleteStatus As CheckBox = DirectCast(e.Item.FindControl("chkCompleteStatus"), CheckBox)
            ' Dim lblToDoStatus As Label = DirectCast(e.Item.FindControl("lblToDoStatus"), Label)
            'aToDoViewLink.HRef = "ViewToDo.aspx?cid=" & hidCustId.Value & "&todoId=" & hidToDoId.Value
            aToDoEditLink.HRef = "AddToDo.aspx?cid=" & hidCustId.Value & "&todoId=" & hidToDoId.Value & "&mode=home"
            aCustomerLink.HRef = "Overview.aspx?cid=" & hidCustId.Value
            lblTodoTitle.Text = aToDoViewLink.InnerHtml & " for " & "<a class='navigate' href='" & aCustomerLink.HRef & "'>" & aCustomerLink.InnerHtml & "</a>"
            'If lblToDoStatus.Text = "Completed" Then
            '    chkCompleteStatus.Checked = True
            'End If
            'If lblToDoStatus.Text = "Over Due" Then
            '    lblToDoStatus.ForeColor = Drawing.Color.Red
            'End If
        End If
    End Sub


    Protected Sub lvRecentActivity_ItemDataBound(sender As Object, e As ListViewItemEventArgs) Handles lvRecentActivity.ItemDataBound
        If e.Item.ItemType = ListViewItemType.DataItem Then
            Dim hidActivityType As HiddenField = DirectCast(e.Item.FindControl("hidActivityType"), HiddenField)
            Dim hidCreatedDate As HiddenField = DirectCast(e.Item.FindControl("hidCreatedDate"), HiddenField)
            Dim hidDescription As HiddenField = DirectCast(e.Item.FindControl("hidDescription"), HiddenField)
            Dim hidReferenceId As HiddenField = DirectCast(e.Item.FindControl("hidReferenceId"), HiddenField)
            Dim hidCustomerName As HiddenField = DirectCast(e.Item.FindControl("hidCustomerName"), HiddenField)
            Dim lblTimeLine As Label = DirectCast(e.Item.FindControl("lblTimeLine"), Label)
            Dim lblDescription As Label = DirectCast(e.Item.FindControl("lblDescription"), Label)
            Try
                If (hidActivityType.Value.ToString().ToLower() = "to do") Then
                    Dim customerId As Integer = hidCustomerName.Value.Split("~")(0)
                    Dim customerName As String = hidCustomerName.Value.Split("~")(1)
                    lblDescription.Text = "A New " & "<a class='navigate' href='AddToDo.aspx?todoid=" & hidReferenceId.Value & "&mode=home'>To Do</a>" & " was created for " & "<a class='navigate' href='Overview.aspx?cid=" & customerId & "'>" & customerName & "</a>"
                End If
                If (hidActivityType.Value.ToString().ToLower() = "new contact") Then
                    lblDescription.Text = "New Contact created for " & "<a class='navigate' href='Overview.aspx?cid=" & hidReferenceId.Value & "'>" & hidCustomerName.Value & "</a>"
                End If
                If (hidActivityType.Value.ToString().ToLower() = "vm link") Then
                    lblDescription.Text = "VM link sent to " & "<a class='navigate' href='Overview.aspx?cid=" & hidReferenceId.Value & "'>" & hidCustomerName.Value & "</a>"
                End If
                If (hidActivityType.Value.ToString().ToLower() = "value map") Then
                    lblDescription.Text = "<a class='navigate'  href='ValueMap.aspx?cid=" & hidReferenceId.Value & "'>Value Map</a>" & " started for " & "<a class='navigate' href='Overview.aspx?cid=" & hidReferenceId.Value & "'>" & hidCustomerName.Value & "</a>"
                End If
                If (hidActivityType.Value.ToString().ToLower() = "evaluation email") Then
                    lblDescription.Text = "Evaluation Email sent to " & "<a class='navigate' href='Overview.aspx?cid=" & hidReferenceId.Value & "'>" & hidCustomerName.Value & "</a>"
                End If
                If (hidActivityType.Value.ToString().ToLower() = "discuss & decide email") Then
                    lblDescription.Text = "Discuss & Decide Email sent to " & "<a class='navigate' href='Overview.aspx?cid=" & hidReferenceId.Value & "'>" & hidCustomerName.Value & "</a>"
                End If
                lblTimeLine.Text = TimeAgo(hidCreatedDate.Value)

            Catch ex As Exception
                e.Item.Visible = False
            End Try
        End If
    End Sub
    Public Shared Function TimeAgo(ByVal dt As DateTime) As String
        Dim span As TimeSpan = DateTime.Now - dt

        If span.Days > 365 Then
            Dim years As Integer = (span.Days / 365)
            If span.Days Mod 365 <> 0 Then years += 1
            Return String.Format("about {0} {1} ago", years, If(years = 1, "year", "years"))
        End If

        If span.Days > 30 Then
            Dim months As Integer = (span.Days / 30)
            If span.Days Mod 31 <> 0 Then months += 1
            Return String.Format("about {0} {1} ago", months, If(months = 1, "month", "months"))
        End If

        If span.Days > 0 Then Return String.Format("{0} {1} ago", span.Days, If(span.Days = 1, "day", "days"))
        If span.Days <= 0 Then Return "today"
        'If span.Hours > 0 Then Return String.Format("about {0} {1} ago", span.Hours, If(span.Hours = 1, "hour", "hours"))
        'If span.Minutes > 0 Then Return String.Format("about {0} {1} ago", span.Minutes, If(span.Minutes = 1, "minute", "minutes"))
        'If span.Seconds > 5 Then Return String.Format("about {0} seconds ago", span.Seconds)
        'If span.Seconds <= 5 Then Return "just now"
        Return String.Empty
    End Function
End Class