Imports System.Data.SqlClient
Imports MAX.USPS
Imports RickProject.DB
Imports RickProject.Business

Public Class AddToDo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Config.LoginUserType) Then
            Response.Redirect("~/Login.aspx")
        End If
        If Not Page.IsPostBack Then
            BindToDoStatus()
            LoadCustomer(Request.QueryString("cid"), Config.UserId)
            hidCustomerId.Value = Request.QueryString("cid")
            txtTitle.Focus()
            lblPageTitle.InnerText = "Add ""To Do"" for: " & txtCustomerName.Value
            If Not (Request.QueryString("todoid") Is Nothing) Then
                hidToDoId.Value = Request.QueryString("todoid")
                LoadToDoDetails(hidToDoId.Value)
                lblPageTitle.InnerText = "Edit ""To Do"" for: " & txtCustomerName.Value
            End If


        End If
    End Sub

    Protected Sub btnAddToDo_Click(sender As Object, e As EventArgs)
        If Not Page.IsValid Then
            Exit Sub
        End If
        Dim objToDoProcessor As New ToDoProcessor()
        lblToDoStatus.Style.Add("display", "none")
        Dim result As Integer = 0
        If String.IsNullOrEmpty(hidToDoId.Value) Then

            result = objToDoProcessor.InsertToDoDetails(CapitalizeName(txtTitle.Value.Trim), CapitalizeName(txtDescription.Value.Trim), txtDueDate.Value.Trim(), ddlStatus.SelectedValue, Config.UserId, hidCustomerId.Value)
        Else
            objToDoProcessor.UpdateToDoDetails(CapitalizeName(txtTitle.Value.Trim), CapitalizeName(txtDescription.Value.Trim), txtDueDate.Value.Trim(), ddlStatus.SelectedValue, Config.UserId, hidCustomerId.Value, hidToDoId.Value)

        End If
        If result > 0 Then
            UserProcessor.InsertUserActivity("To Do", "was created for ", result, Config.UserId)
        End If
        lblToDoStatus.Style.Add("display", "")
        lblToDoStatus.Text = """To do"" saved successfully."

        Select Case Request.QueryString("mode")
            Case "view"
                Response.Redirect("~/ViewTodo.aspx?todoid=" & hidToDoId.Value & "&cid=" & hidCustomerId.Value)
            Case "overview"
                Response.Redirect("~/OverView.aspx?cid=" & hidCustomerId.Value)
            Case "valuemap"
                Response.Redirect("~/ValueMap.aspx?cid=" & hidCustomerId.Value)
            Case "property"
                Response.Redirect("~/AddProperty.aspx?cid=" & hidCustomerId.Value)
            Case "home"
                Response.Redirect("~/Home.aspx")
            Case Else
                Response.Redirect("~/Searchtodo.aspx")
        End Select
    End Sub
    Public Sub LoadCustomer(ByVal customerID As Integer, ByVal userID As Integer)
        Dim objCustomerProcessor As New CustomerProcessor()
        Dim dtCustomer As New DataTable
        objCustomerProcessor.GetContactInfoByUser(userID, customerID, dtCustomer)
        If dtCustomer.Rows.Count > 0 Then
            txtCustomerName.Value = CapitalizeName(Convert.ToString(dtCustomer.Rows(0)("FirstName"))) & " " & CapitalizeName(Convert.ToString(dtCustomer.Rows(0)("LastName")))
        End If
    End Sub
    Public Sub LoadToDoDetails(ByVal todoId As Integer)
        Dim objTodoProcessor As New ToDoProcessor()
        Dim dtToDo As New DataTable
        objTodoProcessor.GetToDoDetailsById(todoId, dtToDo)
        If dtToDo.Rows.Count > 0 Then
            txtTitle.Value = CapitalizeName(dtToDo.Rows(0)("Title"))
            txtDescription.Value = CapitalizeName(dtToDo.Rows(0)("Description"))
            txtDueDate.Value = Convert.ToDateTime(dtToDo.Rows(0)("DueDate")).ToString("MM/dd/yy")
            ddlStatus.SelectedValue = dtToDo.Rows(0)("Status")
            txtCustomerName.Value = CapitalizeName(dtToDo.Rows(0)("CustName"))
            If (dtToDo.Rows(0)("Title").ToString().ToLower().Contains("send evaluation")) Or (dtToDo.Rows(0)("Title").ToString().ToLower().Contains("send discuss")) Or (dtToDo.Rows(0)("Title").ToString().ToLower().Contains("send compare")) Then
                txtTitle.Disabled = True
                txtDueDate.Focus()
                txtDescription.Value = ""
                Dim myScript As String = "$(document).ready(function () {$('#txtDueDate').datepicker('hide');});"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowDialogScript11", myScript, True)
                txtDescription.Value = ""
            End If
        End If
    End Sub
    Public Sub BindToDoStatus()
        Dim objMaster As New MasterProcessor()
        Dim dt As DataTable = New DataTable()
        objMaster.GetToDoStatus(dt)
        ddlStatus.Items.Clear()
        ddlStatus.DataSource = dt
        ddlStatus.DataTextField = "Name"
        ddlStatus.DataValueField = "Id"
        ddlStatus.DataBind()
        If dt.Rows.Count > 0 Then
            ddlStatus.Items.Insert(0, New ListItem("Please select", 0))
        End If
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        'If Request.QueryString("mode") = "view" Then
        '    Response.Redirect("~/ViewTodo.aspx?todoid=" & hidToDoId.Value & "&cid=" & hidCustomerId.Value)
        'End If
        'If Request.QueryString("mode") = "overview" Then
        '    Response.Redirect("~/OverView.aspx?cid=" & hidCustomerId.Value)
        'End If
        'If Request.QueryString("mode") = "valuemap" Then
        '    Response.Redirect("~/ValueMap.aspx?cid=" & hidCustomerId.Value)
        'End If
        'If Request.QueryString("mode") = "property" Then
        '    Response.Redirect("~/AddProperty.aspx?cid=" & hidCustomerId.Value)
        'End If
        'If Request.QueryString("mode") = "home" Then
        '    Response.Redirect("~/Home.aspx")
        'End If
        Select Case Request.QueryString("mode")
            Case "view"
                Response.Redirect("~/ViewTodo.aspx?todoid=" & hidToDoId.Value & "&cid=" & hidCustomerId.Value)
            Case "overview"
                Response.Redirect("~/OverView.aspx?cid=" & hidCustomerId.Value)
            Case "valuemap"
                Response.Redirect("~/ValueMap.aspx?cid=" & hidCustomerId.Value)
            Case "property"
                Response.Redirect("~/AddProperty.aspx?cid=" & hidCustomerId.Value)
            Case "home"
                Response.Redirect("~/Home.aspx")
            Case Else
                Response.Redirect("~/Searchtodo.aspx")
        End Select
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