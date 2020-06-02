Imports RickProject.Business

Public Class ViewToDo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'LoadCustomer(Request.QueryString("cid"), Config.UserId)
        hidCustomerId.Value = Request.QueryString("cid")
        If Not (Request.QueryString("todoid") Is Nothing) Then
            hidToDoId.Value = Request.QueryString("todoid")
            LoadToDoDetails(hidToDoId.Value)
        End If
    End Sub

    Protected Sub btnEditToDo_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/AddToDo.aspx?todoid=" & hidToDoId.Value & "&cid=" & hidCustomerId.Value & "&mode=" & "view")
    End Sub
    Public Sub LoadCustomer(ByVal customerID As Integer, ByVal userID As Integer)
        Dim objCustomerProcessor As New CustomerProcessor()
        Dim dtCustomer As New DataTable
        objCustomerProcessor.GetContactInfoByUser(userID, customerID, dtCustomer)
        If dtCustomer.Rows.Count > 0 Then
            lblCustomerName.Text = Convert.ToString(dtCustomer.Rows(0)("FirstName")) & " " & Convert.ToString(dtCustomer.Rows(0)("LastName"))
        End If
    End Sub
    Public Sub LoadToDoDetails(ByVal todoId As Integer)
        Dim objTodoProcessor As New ToDoProcessor()
        Dim dtToDo As New DataTable
        objTodoProcessor.GetToDoDetailsById(todoId, dtToDo)
        If dtToDo.Rows.Count > 0 Then
            todoTitle.InnerText = dtToDo.Rows(0)("Title")
            lblDescription.Text = dtToDo.Rows(0)("Description")
            lblDueDate.Text = dtToDo.Rows(0)("DueDate")
            lblStatus.Text = dtToDo.Rows(0)("ToDoStatus")
            lblCustomerName.Text = dtToDo.Rows(0)("CustName")
        End If
    End Sub
End Class