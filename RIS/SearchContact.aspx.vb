Imports RickProject.Business

Public Class SearchContact
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
            BindAllCommunities()
            divAllContactsGrid.Style.Add("display", "none")

            ' BindVMCategoryByGGID(Config.LoginUserType)
            BindVMCategoryByCommID(IIf(ddlCommunity.SelectedValue = "", 0, ddlCommunity.SelectedValue))
            txtSearchFirstName.Focus()
            ' btnSearch_Click(sender, Nothing)
            'BindAllContactsByUser(Config.UserId)
            'BindRecentContactsByUser(Config.UserId)
        End If
    End Sub
    Public Sub BindAllCommunities()
        Dim objPriorityProcessor As New PriorityProcessor()
        Dim dt As New DataTable
        'objPriorityProcessor.GetAllCommunities(dt)
        objPriorityProcessor.GetAllCommunitiesByGlobalGroup(Config.LoginUserType, Config.UserId, dt)
        ddlCommunity.DataSource = dt
        ddlCommunity.DataTextField = "CommName"
        ddlCommunity.DataValueField = "CommID"
        ddlCommunity.DataBind()
        ddlCommunity.Items.Insert(0, New ListItem("All", ""))
        If ddlCommunity.Items.Count = 2 Then
            ddlCommunity.SelectedIndex = 1
        End If
    End Sub
    Private Sub SearchCustomers()
        Dim dt As New DataTable()
        Dim objCustomerProcessor As New CustomerProcessor()
        objCustomerProcessor.GetContactsByUser(txtSearchFirstName.Value.Trim, txtSearchLastName.Value.Trim(), txtNoteSearch.Value.Trim(),
                                               chkInactive.Checked, ddlVMCategory.SelectedValue.ToString(), 0, ddlVMStatus.SelectedValue.ToString(), 0,
                                               Config.UserId, ddlCommunity.SelectedValue, dt)
        If dt.Rows.Count = 0 Then
            lblTotalCount.Text = 0 & " Matches Found"
        Else
            If ddlVMStatus.SelectedIndex > 0 Then
                Try
                    If ddlRecordCount.SelectedValue <> "0" Then
                        dt = dt.Select("VMStatus='" & ddlVMStatus.SelectedValue & "'").CopyToDataTable().AsEnumerable().Take(ddlRecordCount.SelectedValue).CopyToDataTable()
                    Else
                        dt = dt.Select("VMStatus='" & ddlVMStatus.SelectedValue & "'").CopyToDataTable()
                    End If

                Catch ex As Exception
                    dt = New DataTable()
                End Try
            Else
                If ddlRecordCount.SelectedValue <> "0" Then
                    dt = dt.AsEnumerable().Take(ddlRecordCount.SelectedValue).CopyToDataTable()
                End If
            End If
            lblTotalCount.Text = dt.Rows.Count & " Matches Found"
            If chkInactive.Checked = True Then
                Dim row As DataRow() = dt.Select("Active='false'")
                If row.Length > 0 Then
                    Dim inactiveDt As DataTable = dt.Select("Active='false'").CopyToDataTable()
                    If inactiveDt.Rows.Count > 0 Then
                        lblTotalCount.Text = lblTotalCount.Text & " , " & inactiveDt.Rows.Count & " Inactive"
                    End If
                End If
            End If
        End If
        divAllContactsGrid.Style.Add("display", "none")
        ' divSearchGrid.Style.Add("display", "inline")
        gvCustomers.DataSource = dt
        gvCustomers.DataBind()
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        divSearchGrid.Style.Add("display", "")
        SearchCustomers()
    End Sub
    Public Sub BindVMCategoryByGGID(ByVal GGID As Integer)
        Dim objMaster As New MasterProcessor()
        Dim dt As DataTable = New DataTable()
        objMaster.GetCategoryByGGID(GGID, dt)
        ddlVMCategory.Items.Clear()
        ddlVMCategory.DataSource = dt
        ddlVMCategory.DataTextField = "Type"
        ddlVMCategory.DataValueField = "VMCategoryId"
        ddlVMCategory.DataBind()
        If dt.Rows.Count > 0 Then
            ddlVMCategory.Items.Insert(0, New ListItem("Value Map Category", 0))
        End If
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
        ddlVMCategory.Items.Insert(0, New ListItem("Value Map Category", 0))
        'End If
    End Sub

    Public Sub BindAllContactsByUser(ByVal userID As Integer)
        Dim objCustomer As New CustomerProcessor
        Dim dt As New DataTable()
        objCustomer.GetAllContactsByUser(userID, dt)
        lvAllContacts.DataSource = dt
        lvAllContacts.DataBind()
    End Sub
    Public Sub BindRecentContactsByUser(ByVal userID As Integer)
        Dim objCustomer As New CustomerProcessor
        Dim dt As New DataTable()
        objCustomer.GetRecentContactsByUser(userID, dt)
        lvRecentContacts.DataSource = dt
        lvRecentContacts.DataBind()
    End Sub

    Protected Sub gvCustomers_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvCustomers.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lblCustomerStatus As Label = DirectCast(e.Row.FindControl("lblCustomerStatus"), Label)
            Dim lblValueMapCategory As Label = DirectCast(e.Row.FindControl("lblValueMapCategory"), Label)
            Dim lblValueMapStatus As Label = DirectCast(e.Row.FindControl("lblValueMapStatus"), Label)
            Dim aCustomerLink As HtmlAnchor = DirectCast(e.Row.FindControl("aCustomerLink"), HtmlAnchor)
            If lblCustomerStatus.Text = "False" Or lblCustomerStatus.Text = "1" Then
                'aCustomerLink.Style.Add("Color", "Red")
                lblValueMapStatus.ForeColor = Drawing.Color.Red
                lblValueMapCategory.ForeColor = Drawing.Color.Red
            Else

            End If
        End If
    End Sub

    Protected Sub ddlCommunity_SelectedIndexChanged(sender As Object, e As EventArgs)
        BindVMCategoryByCommID(IIf(ddlCommunity.SelectedValue = "", 0, ddlCommunity.SelectedValue))
    End Sub
End Class