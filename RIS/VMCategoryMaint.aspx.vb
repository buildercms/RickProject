Imports System.Data.SqlClient
Imports System.Web.Security
Imports Microsoft.VisualBasic
Imports System.Linq
Imports RickProject.DB
Imports System.Drawing

Public Class VMCategoryMaint
    Inherits System.Web.UI.Page

#Region "PROPERTIES"

    Public Property PriorityId() As Integer
        Get
            Return DirectCast(ViewState("PriorityId"), Integer)
        End Get
        Set(value As Integer)
            ViewState("PriorityId") = value
        End Set
    End Property

#End Region
#Region "EVENTS"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Page.Header.Title = "Value Map - Category Maintenance"

        If Not IsPostBack Then
            'If p.MyUserID = 4339 Or p.MyUserID = 4266 Or p.MyUserID = 2921 Or p.MyUserID = 1143 Then
            GetGroups()
            BindGroups1()
            ' GetCategories()
            'VM_Sessions.CommunityName = ""
            ' VM_Sessions.CustomerId = 0
            'VM_Sessions.CommunityName = ""
            ' Else
            ' Response.Redirect("Search.aspx", False)
            'End If
        End If
        'LoadMasterPageControls(VM_Sessions.CustomerId)
        'End If
    End Sub

    Protected Sub lnkAddCategory_Click(sender As Object, e As EventArgs) Handles lnkAddCategory.Click
        lblCategoryError.Text = ""
        divCategoryError.Visible = False
        BindGroups()
        divCopyGroupCategory.Visible = False
        divGroupCategory.Visible = True
        Dim myScript As String = "$(document).ready(function () {jqdialogShowControl();});"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowDialogScript1", myScript, True)
    End Sub
    Protected Sub lnkAddCommunity_Click(sender As Object, e As EventArgs) Handles lnkAddCommunity.Click
        lblCommunityError.Text = ""
        BindCommunityGroups()
        Dim myScript As String = "$(document).ready(function () {jqdialogShowCommunityControl();});"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowDialogScript1", myScript, True)
    End Sub
    Protected Sub lnkAddCommunityCategory_Click(sender As Object, e As EventArgs) Handles lnkAddCommunityCategory.Click
        lblCommunityError.Text = ""
        BindCommunityCategoryGroups()
        Dim myScript As String = "$(document).ready(function () {jqdialogShowCommunityCategoryControl();});"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowDialogScript111", myScript, True)
    End Sub
    Protected Sub lnkAddGroup_Click(sender As Object, e As EventArgs) Handles lnkAddGroup.Click
        Dim myScript As String = "$(document).ready(function () {jqdialogShowGroupControl();});"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowDialogScriptG1", myScript, True)
    End Sub
    Protected Sub lnkAddUser_Click(sender As Object, e As EventArgs) Handles lnkAddUser.Click
        BindUsers()
        trUserAdmin.Visible = False
        trUserCommunity.Visible = False
        trUserCommunitySubmit.Visible = False
        gvUserCommunities.Visible = False
        btnAddUserCommunity.Visible = False
        Dim myScript As String = "$(document).ready(function () {jqdialogShowUserControl();});"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowDialogScriptGU1", myScript, True)
    End Sub

    Protected Sub dropCategories_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dropCategories.SelectedIndexChanged
        If Not String.IsNullOrEmpty(dropCategories.SelectedValue) Then
            GetPriorities(dropCategories.SelectedValue)
        Else
            gvPriorities.DataSource = New DataTable()
            gvPriorities.DataBind()
        End If
    End Sub

    Protected Sub dropDialogGroups_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dropDialogGroups.SelectedIndexChanged
        lblCategoryError.Text = ""
        divCategoryError.Visible = False
        If Not String.IsNullOrEmpty(dropDialogGroups.SelectedValue) Then
            GetGroupCategories(dropDialogGroups.SelectedValue)
        Else
            gvCategories.DataSource = New DataTable()
            gvCategories.DataBind()
        End If
    End Sub
    Protected Sub dropDialogCommunityGroups_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dropDialogCommunityGroups.SelectedIndexChanged

        If Not String.IsNullOrEmpty(dropDialogCommunityGroups.SelectedValue) Then
            GetGroupCommunities(dropDialogCommunityGroups.SelectedValue)
        Else
            gvCommunities.DataSource = New DataTable()
            gvCommunities.DataBind()
        End If
    End Sub
    Protected Sub dropDialogUsers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dropDialogUsers.SelectedIndexChanged

        If Not String.IsNullOrEmpty(dropDialogUsers.SelectedValue) Then
            BindUserGroupCommunities()
            If dropDialogUserCommunities.Items.Count > 0 Then '
                ' trUserCommunitySubmit.Visible = False
                ' trUserAdmin.Visible = True
                ' trUserCommunity.Visible = True
                ' gvUserCommunities.Visible = True
                btnAddUserCommunity.Visible = True
            Else
                btnAddUserCommunity.Visible = False
            End If
            trUserCommunitySubmit.Visible = False
            trUserAdmin.Visible = False
            trUserCommunity.Visible = False
            GetGroupUserCommunities()
            gvUserCommunities.Visible = True
        Else
            trUserCommunitySubmit.Visible = True
            trUserAdmin.Visible = False
            trUserCommunity.Visible = False
            gvUserCommunities.Visible = False
            btnAddUserCommunity.Visible = False
        End If
    End Sub
    Protected Sub btnAddUserCommunity_Click(sender As Object, e As EventArgs) Handles btnAddUserCommunity.Click
        trUserCommunitySubmit.Visible = True
        trUserAdmin.Visible = True
        trUserCommunity.Visible = True
        btnAddUserCommunity.Visible = False
        chkUserAdmin.Checked = False
    End Sub
    Protected Sub btnUserCommunitySubmit_Click(sender As Object, e As EventArgs) Handles btnUserCommunitySubmit.Click
        lblUserError.Visible = False
        Dim isSuccess As Boolean = False
        Try
            Dim ra As Integer = -1
            Dim Db As DataBase = New DataBase
            Db.Init("VM_AddUserCommunityCategory")
            Db.AddParameter("@CommId", dropDialogUserCommunities.SelectedValue)
            Db.AddParameter("@UserID", dropDialogUsers.SelectedValue)
            Db.AddParameter("@IsAdmin", chkUserAdmin.Checked)
            ra = Db.ExecuteAndReturn()
            Db.Close()
            If ra > 0 Then
            Else

                divCommunityCategoryError.Visible = False
                isSuccess = True
                dropDialogUsers_SelectedIndexChanged(sender, Nothing)
            End If

        Catch ex As Exception
            lblCommunityCategoryError.Text = "There was an error associating this category: " & ex.Message
            divCommunityCategoryError.Visible = True
        End Try
    End Sub


    Protected Sub dropDialogCommunityCategoryGroups_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dropDialogCommunityCategoryGroups.SelectedIndexChanged

        If Not String.IsNullOrEmpty(dropDialogCommunityCategoryGroups.SelectedValue) Then
            GetGroupCategoryCommunities(dropDialogCommunityCategoryGroups.SelectedValue)
        Else
            dropDialogCommunityCategory.DataSource = New DataTable()
            dropDialogCommunityCategory.DataBind()
        End If
    End Sub
    Protected Sub dropDialogCommunityCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dropDialogCommunityCategory.SelectedIndexChanged

        If Not String.IsNullOrEmpty(dropDialogCommunityCategory.SelectedValue) Then
            GetGroupCommunityCategories(dropDialogCommunityCategory.SelectedValue)
        Else
            gvCommunityCategories.DataSource = New DataTable()
            gvCommunityCategories.DataBind()
        End If
    End Sub


    Protected Sub dropGroups_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dropGroups.SelectedIndexChanged

        If Not String.IsNullOrEmpty(dropGroups.SelectedValue) Then
            GetCategories(dropGroups.SelectedValue)
        Else
            dropCategories.DataSource = New DataTable()
            dropCategories.DataBind()
            ' dropCategories.Items.Clear()
        End If
        dropCategories_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Protected Sub gvCategories_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvCategories.RowCommand
        lblError.Text = ""
        If (e.CommandName = "AddNew") Then
            Dim category As TextBox = DirectCast(DirectCast(sender, GridView).FooterRow.FindControl("txtNewCategory"), TextBox)
            If Not String.IsNullOrEmpty(category.Text.Trim) Then
                Dim isSuccess As Boolean = AddNewCategory(category.Text.Trim)
                If isSuccess Then
                    category.Text = ""
                End If
            End If
        End If
    End Sub
    Protected Sub gvCommunities_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvCommunities.RowCommand
        lblCommunityError.Text = ""
        If (e.CommandName = "AddNew") Then
            Dim community As TextBox = DirectCast(DirectCast(sender, GridView).FooterRow.FindControl("txtNewCommunity"), TextBox)
            If Not String.IsNullOrEmpty(community.Text.Trim) Then
                Dim isSuccess As Boolean = AddNewCommunity(community.Text.Trim)
                If isSuccess Then
                    community.Text = ""
                End If
            End If
        End If
    End Sub
    Protected Sub gvCategories_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvCategories.RowEditing

        gvCategories.EditIndex = e.NewEditIndex
        GetGroupCategories(dropDialogGroups.SelectedValue)
        Dim lnkCategoryDelete As Button = DirectCast(gvCategories.Rows(e.NewEditIndex).FindControl("lnkCategoryDelete"), Button)
        lnkCategoryDelete.Visible = False
    End Sub

    Protected Sub gvCategories_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvCategories.RowUpdating
        Dim cId As Label = DirectCast(gvCategories.Rows(e.RowIndex).FindControl("lblEditCategoryId"), Label)
        Dim txtCategory As TextBox = DirectCast(gvCategories.Rows(e.RowIndex).FindControl("txtCategory"), TextBox)

        Dim lblEditCategoryName As Label = DirectCast(gvCategories.Rows(e.RowIndex).FindControl("lblEditCategory"), Label)
        'Using dbcon As New SqlConnection(p.GetSqlConnection)
        '    dbcon.Open()
        '    Using cmd As New SqlCommand("update VM_Priority set PriorityName='" + txtPriority.Text.Trim + "',Sort='" + txtSort.Text.Trim + "' where PriorityID=" + pId.Text, dbcon)
        '        cmd.ExecuteNonQuery()
        '    End Using
        'End Using
        Dim sort As Integer = 0
        lblCategoryError.Text = ""
        Dim isSuccess As Boolean = UpdateCategory(txtCategory.Text.Trim, cId.Text, lblEditCategoryName.Text.Trim)
        If isSuccess Then
            gvCategories.EditIndex = -1
            GetGroupCategories(dropDialogGroups.SelectedValue)
        End If
    End Sub
    Protected Sub gvCategories_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvCategories.RowCancelingEdit
        gvCategories.EditIndex = -1
        GetGroupCategories(dropDialogGroups.SelectedValue)
    End Sub
    Protected Sub gvCommunities_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvCommunities.RowEditing

        gvCommunities.EditIndex = e.NewEditIndex
        GetGroupCommunities(dropDialogCommunityGroups.SelectedValue)
        Dim lnkCommunityDelete As Button = DirectCast(gvCommunities.Rows(e.NewEditIndex).FindControl("lnkCommunityDelete"), Button)
        lnkCommunityDelete.Visible = False
    End Sub
    Protected Sub gvCommunities_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvCommunities.RowUpdating
        Dim cId As Label = DirectCast(gvCommunities.Rows(e.RowIndex).FindControl("lblEditCommunityId"), Label)
        Dim txtCommunity As TextBox = DirectCast(gvCommunities.Rows(e.RowIndex).FindControl("txtCommunity"), TextBox)

        Dim lblEditCommunity As Label = DirectCast(gvCommunities.Rows(e.RowIndex).FindControl("lblEditCommunity"), Label)
        'Using dbcon As New SqlConnection(p.GetSqlConnection)
        '    dbcon.Open()
        '    Using cmd As New SqlCommand("update VM_Priority set PriorityName='" + txtPriority.Text.Trim + "',Sort='" + txtSort.Text.Trim + "' where PriorityID=" + pId.Text, dbcon)
        '        cmd.ExecuteNonQuery()
        '    End Using
        'End Using
        Dim sort As Integer = 0
        lblCommunityError.Text = ""
        Dim isSuccess As Boolean = UpdateCommunity(txtCommunity.Text.Trim, cId.Text, lblEditCommunity.Text.Trim)
        If isSuccess Then
            gvCommunities.EditIndex = -1
            GetGroupCommunities(dropDialogCommunityGroups.SelectedValue)
        End If
    End Sub
    Protected Sub gvCommunities_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvCommunities.RowCancelingEdit
        gvCommunities.EditIndex = -1
        GetGroupCommunities(dropDialogCommunityGroups.SelectedValue)
    End Sub
    Protected Sub gvCategories_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvCategories.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Try
                Dim lblCategoryCount As Label = DirectCast(e.Row.FindControl("lblCategoryCount"), Label)
                Dim lnkCategoryEdit As LinkButton = DirectCast(e.Row.FindControl("lnkCategoryEdit"), LinkButton)
                Dim lnkCategoryDelete As Button = DirectCast(e.Row.FindControl("lnkCategoryDelete"), Button)
                If Convert.ToInt32(lblCategoryCount.Text) = 0 Then
                    'lnkCategoryEdit.Visible = True
                    lnkCategoryDelete.Visible = True
                Else
                    'lnkCategoryEdit.Visible = False
                    lnkCategoryDelete.Visible = False
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub
    Protected Sub gvCommunities_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvCommunities.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Try
                Dim lblCommunityCount As Label = DirectCast(e.Row.FindControl("lblCommunityCategoryCount"), Label)
                Dim lnkCommunityEdit As LinkButton = DirectCast(e.Row.FindControl("lnkCommunityEdit"), LinkButton)
                Dim lnkCommunityDelete As Button = DirectCast(e.Row.FindControl("lnkCommunityDelete"), Button)
                Dim lblCommunity As Label = DirectCast(e.Row.FindControl("lblCommunity"), Label)
                If Convert.ToString(lblCommunity.Text) = "" Then
                    lnkCommunityEdit.Visible = False
                    lnkCommunityDelete.Visible = False
                End If
                If Convert.ToInt32(lblCommunityCount.Text) = 0 Then
                    'lnkCategoryEdit.Visible = True
                    lnkCommunityDelete.Visible = True
                Else
                    'lnkCategoryEdit.Visible = False
                    lnkCommunityDelete.Visible = False
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub
    Protected Sub gvCategories_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvCategories.RowDeleting

        Dim cId As Label = DirectCast(gvCategories.Rows(e.RowIndex).FindControl("lblCategoryId"), Label)

        Dim returnValue As Integer = -1
        Dim Db As DataBase = New DataBase
        Db.Init("VM_DeleteCategory")
        Db.AddParameter("@VmCategoryId", cId.Text.Trim)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        GetGroupCategories(dropDialogGroups.SelectedValue)
        BindGroups1()

    End Sub
    Protected Sub gvCommunities_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvCommunities.RowDeleting
        Dim cId As Label = DirectCast(gvCommunities.Rows(e.RowIndex).FindControl("lblCommunityId"), Label)
        Dim returnValue As Integer = -1
        Dim Db As DataBase = New DataBase
        Db.Init("VM_DeleteCommunity")
        Db.AddParameter("@CommId", cId.Text.Trim)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        GetGroupCommunities(dropDialogCommunityGroups.SelectedValue)
    End Sub
    Protected Sub gvGroups_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvGroups.RowCommand
        lblError.Text = ""
        If (e.CommandName = "AddNew") Then
            Dim group As TextBox = DirectCast(DirectCast(sender, GridView).FooterRow.FindControl("txtNewGroup"), TextBox)
            If Not String.IsNullOrEmpty(group.Text.Trim) Then
                Dim isSuccess As Boolean = AddNewGroup(group.Text.Trim)
                If isSuccess Then
                    group.Text = ""
                End If
            End If
        End If
    End Sub
    Protected Sub gvGroups_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvGroups.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Try
                Dim lblGroupCategoryCount As Label = DirectCast(e.Row.FindControl("lblGroupCategoryCount"), Label)
                Dim lnkGroupEdit As LinkButton = DirectCast(e.Row.FindControl("lnkGroupEdit"), LinkButton)
                Dim lnkGroupDelete As Button = DirectCast(e.Row.FindControl("lnkGroupDelete"), Button)
                If Convert.ToInt32(lblGroupCategoryCount.Text) = 0 Then
                    'lnkGroupEdit.Visible = True
                    lnkGroupDelete.Visible = True
                Else
                    'lnkGroupEdit.Visible = False
                    lnkGroupDelete.Visible = False
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub
    Protected Sub gvGroups_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvGroups.RowCancelingEdit
        gvGroups.EditIndex = -1
        GetGroups()
    End Sub
    Protected Sub gvGroups_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvGroups.RowEditing

        gvGroups.EditIndex = e.NewEditIndex
        GetGroups()
        Dim lnkGroupDelete As Button = DirectCast(gvGroups.Rows(e.NewEditIndex).FindControl("lnkGroupDelete"), Button)
        lnkGroupDelete.Visible = False
    End Sub
    Protected Sub gvGroups_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvGroups.RowUpdating
        Dim gId As Label = DirectCast(gvGroups.Rows(e.RowIndex).FindControl("lblEditGroupId"), Label)
        Dim txtGroup As TextBox = DirectCast(gvGroups.Rows(e.RowIndex).FindControl("txtGroup"), TextBox)

        Dim lblEditGroupName As Label = DirectCast(gvGroups.Rows(e.RowIndex).FindControl("lblEditGroup"), Label)
        'Using dbcon As New SqlConnection(p.GetSqlConnection)
        '    dbcon.Open()
        '    Using cmd As New SqlCommand("update VM_Priority set PriorityName='" + txtPriority.Text.Trim + "',Sort='" + txtSort.Text.Trim + "' where PriorityID=" + pId.Text, dbcon)
        '        cmd.ExecuteNonQuery()
        '    End Using
        'End Using
        Dim sort As Integer = 0
        lblGroupError.Text = ""
        Dim isSuccess As Boolean = UpdateGroup(txtGroup.Text.Trim, gId.Text, lblEditGroupName.Text.Trim)
        If isSuccess Then
            gvGroups.EditIndex = -1
            GetGroups()
        End If
    End Sub

    Protected Sub gvGroups_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvGroups.RowDeleting

        Dim gId As Label = DirectCast(gvGroups.Rows(e.RowIndex).FindControl("lblGroupId"), Label)

        Dim returnValue As Integer = -1
        Dim Db As DataBase = New DataBase
        Db.Init("VM_DeleteGroup")
        Db.AddParameter("@GGId", gId.Text.Trim)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        GetGroups()
        BindGroups1()

    End Sub

    Protected Sub gvPriorities_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvPriorities.RowCommand
        lblError.Text = ""
        If Not String.IsNullOrEmpty(e.CommandArgument) Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = gvPriorities.Rows(index)
            Dim pId As Integer = Convert.ToInt32(DirectCast(row.FindControl("lblPriorityId"), Label).Text.Trim)
            Dim priorityName As String = Convert.ToString(DirectCast(row.FindControl("lblPriority"), Label).Text.Trim)
            PriorityId = pId
            If (e.CommandName = "AffectsBenefits") Then
                lblABTitle.Text = "MANAGE AFFECTS/BENEFITS FOR " & priorityName.ToUpper()
                GetPriorityDetails(pId, 1)
                ShowAffectsBenefitsPopUp()
            ElseIf (e.CommandName = "WWDC") Then
                lblWWDCTitle.Text = "MANAGE WWDC/WWDF FOR " & priorityName.ToUpper()
                GetPriorityDetails(pId, 2)
                ShowWWDCPopup()
            End If
        Else
            If Not String.IsNullOrEmpty(e.CommandName) Then
                If (e.CommandName = "AddNew") Then
                    Dim priority As TextBox = DirectCast(DirectCast(sender, GridView).FooterRow.FindControl("txtAddPriority"), TextBox)
                    Dim sort As TextBox = DirectCast(DirectCast(sender, GridView).FooterRow.FindControl("txtAddPrioritySort"), TextBox)
                    Dim sortNum As Integer = 0
                    If Not String.IsNullOrEmpty(sort.Text.Trim) Then
                        sortNum = CInt(sort.Text.Trim)
                    End If
                    Dim isSuccess As Boolean = AddNewPriority(priority.Text.Trim, sortNum)
                    If isSuccess Then
                        priority.Text = ""
                        sort.Text = ""
                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub gvPriorities_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvPriorities.RowEditing
        gvPriorities.EditIndex = e.NewEditIndex
        GetPriorities(dropCategories.SelectedValue)
        Dim lnkPriorityDelete As Button = DirectCast(gvPriorities.Rows(e.NewEditIndex).FindControl("lnkPriorityDelete"), Button)
        lnkPriorityDelete.Visible = False
    End Sub

    Protected Sub gvPriorities_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvPriorities.RowCancelingEdit
        gvPriorities.EditIndex = -1
        GetPriorities(dropCategories.SelectedValue)
    End Sub

    Protected Sub gvPriorities_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvPriorities.RowUpdating
        Dim pId As Label = DirectCast(gvPriorities.Rows(e.RowIndex).FindControl("lblEditPriorityId"), Label)
        Dim txtPriority As TextBox = DirectCast(gvPriorities.Rows(e.RowIndex).FindControl("txtPriority"), TextBox)
        Dim txtSort As TextBox = DirectCast(gvPriorities.Rows(e.RowIndex).FindControl("txtSort"), TextBox)
        Dim oldPriority As Label = DirectCast(gvPriorities.Rows(e.RowIndex).FindControl("lblEditPriorityName"), Label)
        'Using dbcon As New SqlConnection(p.GetSqlConnection)
        '    dbcon.Open()
        '    Using cmd As New SqlCommand("update VM_Priority set PriorityName='" + txtPriority.Text.Trim + "',Sort='" + txtSort.Text.Trim + "' where PriorityID=" + pId.Text, dbcon)
        '        cmd.ExecuteNonQuery()
        '    End Using
        'End Using
        Dim sort As Integer = 0
        If Not String.IsNullOrEmpty(txtSort.Text.Trim) Then
            sort = CInt(txtSort.Text.Trim)
        End If
        Dim isSuccess As Boolean = UpdatePriority(txtPriority.Text.Trim, sort, pId.Text, oldPriority.Text.Trim)
        If isSuccess Then
            gvPriorities.EditIndex = -1
            GetPriorities(dropCategories.SelectedValue)
        End If
    End Sub

    Protected Sub gvPriorities_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvPriorities.RowDeleting

        Dim pId As Label = DirectCast(gvPriorities.Rows(e.RowIndex).FindControl("lblPriorityId"), Label)
        Dim returnValue As Integer = -1
        Dim Db As DataBase = New DataBase
        Db.Init("VM_DeletePriority")
        Db.AddParameter("@PriorityId", pId.Text.Trim)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        GetPriorities(dropCategories.SelectedValue)

    End Sub

    Protected Sub gvPriorities_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvPriorities.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) AndAlso (Not e.Row.DataItem Is Nothing) AndAlso (CInt(e.Row.DataItem("Sort")) = -1) Then
            e.Row.Visible = False
        Else
            Dim lblPriorityCount As Label = DirectCast(e.Row.FindControl("lblPriorityCount"), Label)
            Dim lnkPriorityDelete As Button = DirectCast(e.Row.FindControl("lnkPriorityDelete"), Button)
            If Not lblPriorityCount Is Nothing Then
                If Convert.ToInt32(lblPriorityCount.Text) > 0 Then
                    lnkPriorityDelete.Visible = False
                Else
                    lnkPriorityDelete.Visible = True
                End If
            End If
        End If
    End Sub

    Protected Sub gvAffectsBenefits_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvAffectsBenefits.RowEditing
        gvAffectsBenefits.EditIndex = e.NewEditIndex
        GetPriorityDetails(PriorityId, 1)
        ShowAffectsBenefitsPopUp()
    End Sub

    Protected Sub gvAffectsBenefits_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvAffectsBenefits.RowCancelingEdit
        gvAffectsBenefits.EditIndex = -1
        GetPriorityDetails(PriorityId, 1)
        ShowAffectsBenefitsPopUp()
    End Sub

    Protected Sub gvAffectsBenefits_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvAffectsBenefits.RowUpdating
        Dim vmType As Label = DirectCast(gvAffectsBenefits.Rows(e.RowIndex).FindControl("lblEditVMType"), Label)
        Dim pId As Label = DirectCast(gvAffectsBenefits.Rows(e.RowIndex).FindControl("lblEditPriorityId"), Label)
        Dim oldCS As Label = DirectCast(gvAffectsBenefits.Rows(e.RowIndex).FindControl("lblEditCS"), Label)
        Dim oldFS As Label = DirectCast(gvAffectsBenefits.Rows(e.RowIndex).FindControl("lblEditBenefits"), Label)
        Dim txtCS As TextBox = DirectCast(gvAffectsBenefits.Rows(e.RowIndex).FindControl("txtCS"), TextBox)
        Dim txtCSSort As TextBox = DirectCast(gvAffectsBenefits.Rows(e.RowIndex).FindControl("txtCSSort"), TextBox)
        Dim txtFS As TextBox = DirectCast(gvAffectsBenefits.Rows(e.RowIndex).FindControl("txtFS"), TextBox)
        Dim txtFSSort As TextBox = DirectCast(gvAffectsBenefits.Rows(e.RowIndex).FindControl("txtFSSort"), TextBox)

        Dim csSort As Integer = 0
        Dim fsSort As Integer = 0
        If Not String.IsNullOrEmpty(txtCSSort.Text.Trim) Then
            csSort = CInt(txtCSSort.Text.Trim)
        End If
        If Not String.IsNullOrEmpty(txtFSSort.Text.Trim) Then
            fsSort = CInt(txtFSSort.Text.Trim)
        End If
        Dim returnValue As Integer = -1
        Dim Db As DataBase = New DataBase
        Db.Init("VM_UpdateVMChoices")
        Db.AddParameter("@CS", txtCS.Text.Trim)
        Db.AddParameter("@CSSort", csSort)
        Db.AddParameter("@FS", txtFS.Text.Trim)
        Db.AddParameter("@FSSort", fsSort)
        Db.AddParameter("@OldCS", oldCS.Text.Trim)
        Db.AddParameter("@OldFS", oldFS.Text.Trim)
        Db.AddParameter("@VMType", vmType.Text.Trim)
        Db.AddParameter("@PriorityId", pId.Text.Trim)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()


        gvAffectsBenefits.EditIndex = -1
        GetPriorityDetails(PriorityId, 1)
        ShowAffectsBenefitsPopUp()
    End Sub

    Protected Sub gvAffectsBenefits_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvAffectsBenefits.RowDeleting
        Dim vmType As Label = DirectCast(gvAffectsBenefits.Rows(e.RowIndex).FindControl("lblVMType"), Label)
        Dim pId As Label = DirectCast(gvAffectsBenefits.Rows(e.RowIndex).FindControl("lblPriorityId"), Label)
        Dim CS As Label = DirectCast(gvAffectsBenefits.Rows(e.RowIndex).FindControl("lblPriority"), Label)
        Dim FS As Label = DirectCast(gvAffectsBenefits.Rows(e.RowIndex).FindControl("lblBenefits"), Label)
        Dim returnValue As Integer = -1
        Dim Db As DataBase = New DataBase
        Db.Init("VM_DeleteVMChoices")
        Db.AddParameter("@CS", CS.Text.Trim)
        Db.AddParameter("@FS", FS.Text.Trim)
        Db.AddParameter("@VMType", vmType.Text.Trim)
        Db.AddParameter("@PriorityId", pId.Text.Trim)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()

        GetPriorityDetails(PriorityId, 1)
        ShowAffectsBenefitsPopUp()
    End Sub

    Protected Sub gvAffectsBenefits_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvAffectsBenefits.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) AndAlso (Not e.Row.DataItem Is Nothing) AndAlso (CInt(e.Row.DataItem("FSSort")) = -1) Then
            e.Row.Visible = False
        End If
    End Sub

    Protected Sub gvAffectsBenefits_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvAffectsBenefits.RowCommand
        lblError.Text = ""
        If (e.CommandName = "AddNew") Then
            Dim CS As TextBox = DirectCast(DirectCast(sender, GridView).FooterRow.FindControl("txtAddCS"), TextBox)
            Dim txtCSSort As TextBox = DirectCast(DirectCast(sender, GridView).FooterRow.FindControl("txtAddCSSort"), TextBox)
            Dim FS As TextBox = DirectCast(DirectCast(sender, GridView).FooterRow.FindControl("txtAddFS"), TextBox)
            Dim txtFSSort As TextBox = DirectCast(DirectCast(sender, GridView).FooterRow.FindControl("txtAddFSSort"), TextBox)
            Dim csSort As Integer = 0
            Dim fsSort As Integer = 0
            If Not String.IsNullOrEmpty(txtCSSort.Text.Trim) Then
                csSort = CInt(txtCSSort.Text.Trim)
            End If
            If Not String.IsNullOrEmpty(txtFSSort.Text.Trim) Then
                fsSort = CInt(txtFSSort.Text.Trim)
            End If
            Dim isSuccess As Boolean = AddNewABPriorityDetails(CS.Text.Trim, csSort, FS.Text.Trim, fsSort)
            If isSuccess Then
                CS.Text = ""
                txtCSSort.Text = ""
                txtFSSort.Text = ""
                FS.Text = ""
            End If
        End If
    End Sub

    Protected Sub gvWWDC_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvWWDC.RowEditing
        gvWWDC.EditIndex = e.NewEditIndex
        GetPriorityDetails(PriorityId, 2)
        ShowWWDCPopup()
    End Sub

    Protected Sub gvWWDC_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvWWDC.RowCancelingEdit
        gvWWDC.EditIndex = -1
        GetPriorityDetails(PriorityId, 2)
        ShowWWDCPopup()
    End Sub

    Protected Sub gvWWDC_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvWWDC.RowUpdating
        Dim vmType As Label = DirectCast(gvWWDC.Rows(e.RowIndex).FindControl("lblEditVMType"), Label)
        Dim pId As Label = DirectCast(gvWWDC.Rows(e.RowIndex).FindControl("lblEditPriorityId"), Label)
        Dim oldCS As Label = DirectCast(gvWWDC.Rows(e.RowIndex).FindControl("lblEditCS"), Label)
        Dim oldFS As Label = DirectCast(gvWWDC.Rows(e.RowIndex).FindControl("lblEditBenefits"), Label)
        Dim txtCS As TextBox = DirectCast(gvWWDC.Rows(e.RowIndex).FindControl("txtCS"), TextBox)
        Dim txtCSSort As TextBox = DirectCast(gvWWDC.Rows(e.RowIndex).FindControl("txtCSSort"), TextBox)
        Dim txtFS As TextBox = DirectCast(gvWWDC.Rows(e.RowIndex).FindControl("txtFS"), TextBox)
        Dim txtFSSort As TextBox = DirectCast(gvWWDC.Rows(e.RowIndex).FindControl("txtFSSort"), TextBox)

        Dim csSort As Integer = 0
        Dim fsSort As Integer = 0
        If Not String.IsNullOrEmpty(txtCSSort.Text.Trim) Then
            csSort = CInt(txtCSSort.Text.Trim)
        End If
        If Not String.IsNullOrEmpty(txtFSSort.Text.Trim) Then
            fsSort = CInt(txtFSSort.Text.Trim)
        End If

        Dim returnValue As Integer = -1
        Dim Db As DataBase = New DataBase
        Db.Init("VM_UpdateVMChoices")
        Db.AddParameter("@CS", txtCS.Text.Trim)
        Db.AddParameter("@CSSort", csSort)
        Db.AddParameter("@FS", txtFS.Text.Trim)
        Db.AddParameter("@FSSort", fsSort)
        Db.AddParameter("@OldCS", oldCS.Text.Trim)
        Db.AddParameter("@OldFS", oldFS.Text.Trim)
        Db.AddParameter("@VMType", vmType.Text.Trim)
        Db.AddParameter("@PriorityId", pId.Text.Trim)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()

        gvWWDC.EditIndex = -1
        GetPriorityDetails(PriorityId, 2)
        ShowWWDCPopup()
    End Sub

    Protected Sub gvWWDC_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvWWDC.RowDeleting
        Dim vmType As Label = DirectCast(gvWWDC.Rows(e.RowIndex).FindControl("lblVMType"), Label)
        Dim pId As Label = DirectCast(gvWWDC.Rows(e.RowIndex).FindControl("lblPriorityId"), Label)
        Dim CS As Label = DirectCast(gvWWDC.Rows(e.RowIndex).FindControl("lblPriority"), Label)
        Dim FS As Label = DirectCast(gvWWDC.Rows(e.RowIndex).FindControl("lblBenefits"), Label)
        Dim returnValue As Integer = -1
        Dim Db As DataBase = New DataBase
        Db.Init("VM_DeleteVMChoices")

        Db.AddParameter("@CS", CS.Text.Trim)
        Db.AddParameter("@FS", FS.Text.Trim)
        Db.AddParameter("@VMType", vmType.Text.Trim)
        Db.AddParameter("@PriorityId", pId.Text.Trim)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()

        GetPriorityDetails(PriorityId, 2)
        ShowWWDCPopup()
    End Sub

    Protected Sub gvWWDC_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvWWDC.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) AndAlso (Not e.Row.DataItem Is Nothing) AndAlso (CInt(e.Row.DataItem("FSSort")) = -1) Then
            e.Row.Visible = False
        End If
    End Sub

    Protected Sub gvWWDC_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvWWDC.RowCommand
        lblError.Text = ""
        If (e.CommandName = "AddNew") Then
            Dim CS As TextBox = DirectCast(DirectCast(sender, GridView).FooterRow.FindControl("txtAddCS"), TextBox)
            Dim txtCSSort As TextBox = DirectCast(DirectCast(sender, GridView).FooterRow.FindControl("txtAddCSSort"), TextBox)
            Dim FS As TextBox = DirectCast(DirectCast(sender, GridView).FooterRow.FindControl("txtAddFS"), TextBox)
            Dim txtFSSort As TextBox = DirectCast(DirectCast(sender, GridView).FooterRow.FindControl("txtAddFSSort"), TextBox)
            Dim csSort As Integer = 0
            Dim fsSort As Integer = 0
            If Not String.IsNullOrEmpty(txtCSSort.Text.Trim) Then
                csSort = CInt(txtCSSort.Text.Trim)
            End If
            If Not String.IsNullOrEmpty(txtFSSort.Text.Trim) Then
                fsSort = CInt(txtFSSort.Text.Trim)
            End If
            Dim isSuccess As Boolean = AddNewWWDPriorityDetails(CS.Text.Trim, csSort, FS.Text.Trim, fsSort)
            If isSuccess Then
                CS.Text = ""
                txtCSSort.Text = ""
                txtFSSort.Text = ""
                FS.Text = ""
            End If
        End If
    End Sub

    Protected Sub gvCommunityCategories_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvCommunityCategories.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Try
                Dim lblCommunityCategoryAssociatedCount As Label = DirectCast(e.Row.FindControl("lblCommunityCategoryAssociatedCount"), Label)
                Dim chkInclude As CheckBox = DirectCast(e.Row.FindControl("chkInclude"), CheckBox)

                If Convert.ToInt32(lblCommunityCategoryAssociatedCount.Text) = 0 Then
                    'lnkCategoryEdit.Visible = True
                    chkInclude.Enabled = True
                Else
                    'lnkCategoryEdit.Visible = False
                    chkInclude.Enabled = False
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Protected Sub gvUserCommunities_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvUserCommunities.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Try
                'Dim lblCommunityCategoryCount As Label = DirectCast(e.Row.FindControl("lblCommunityCategoryCount"), Label)
                'Dim chkInclude As CheckBox = DirectCast(e.Row.FindControl("chkCommunityInclude"), CheckBox)

                'If Convert.ToInt32(lblCommunityCategoryCount.Text) = 0 Then
                '    'lnkCategoryEdit.Visible = True
                '    chkInclude.Enabled = True
                'Else
                '    'lnkCategoryEdit.Visible = False
                '    chkInclude.Enabled = False
                'End If
            Catch ex As Exception

            End Try
        End If

    End Sub
    Protected Sub gvUserCommunities_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvUserCommunities.RowCommand
        If (e.CommandName = "DeleteCommunity") Then
            Try
                Try
                    Dim ra As Integer = -1
                    Dim Db As DataBase = New DataBase
                    Db.Init("VM_DeleteUserCommunity")
                    Db.AddParameter("@CommId", e.CommandArgument)
                    Db.AddParameter("@UserID", dropDialogUsers.SelectedValue)
                    ra = Db.ExecuteAndReturn()
                    Db.Close()
                    dropDialogUsers_SelectedIndexChanged(sender, Nothing)
                    If ra > 0 Then
                    Else

                        divCommunityCategoryError.Visible = False

                    End If

                Catch ex As Exception
                    lblCommunityCategoryError.Text = "There was an error associating this category: " & ex.Message
                    divCommunityCategoryError.Visible = True
                End Try
            Catch ex As Exception

            End Try
        End If
    End Sub

    Protected Sub chkInclude_CheckedChanged(sender As Object, e As EventArgs)
        lblCommunityCategoryError.Visible = False

        Dim categoryId As Integer = DirectCast(sender, CheckBox).ValidationGroup
        Dim isSuccess As Boolean = False
        Try
            Dim ra As Integer = -1
            Dim Db As DataBase = New DataBase
            Db.Init("VM_AddCommunityCategory")
            Db.AddParameter("@CommId", dropDialogCommunityCategory.SelectedValue)
            Db.AddParameter("@CategoryID", categoryId)
            Db.AddParameter("@IsIncluded", DirectCast(sender, CheckBox).Checked)
            ra = Db.ExecuteAndReturn()
            Db.Close()
            If ra > 0 Then

            Else
                dropDialogCommunityCategory_SelectedIndexChanged(Nothing, Nothing)
                divCommunityCategoryError.Visible = False
                isSuccess = True
            End If

        Catch ex As Exception
            lblCommunityCategoryError.Text = "There was an error associating this category: " & ex.Message
            divCommunityCategoryError.Visible = True
        End Try

    End Sub

    Protected Sub chkCommunityInclude_CheckedChanged(sender As Object, e As EventArgs)
        lblUserError.Visible = False

        Dim commId As Integer = DirectCast(sender, CheckBox).ValidationGroup
        Dim isSuccess As Boolean = False
        Try
            Dim ra As Integer = -1
            Dim Db As DataBase = New DataBase
            Db.Init("VM_UpdateUserCommunity")
            Db.AddParameter("@CommId", commId)
            Db.AddParameter("@UserID", dropDialogUsers.SelectedValue)
            Db.AddParameter("@IsAdmin", DirectCast(sender, CheckBox).Checked)

            ra = Db.ExecuteAndReturn()
            Db.Close()
            If ra > 0 Then

            Else
                GetGroupUserCommunities()
                gvUserCommunities.Visible = True
                divCommunityCategoryError.Visible = False
                isSuccess = True
            End If

        Catch ex As Exception
            lblCommunityCategoryError.Text = "There was an error associating this category: " & ex.Message
            divCommunityCategoryError.Visible = True
        End Try

    End Sub
#End Region

#Region "METHODS"

    ''' <summary>
    ''' Get Categories
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetCategories(ByVal ggid As Integer)
        Dim dt As New DataTable()

        Dim Db As DataBase = New DataBase
        Db.Init("VM_GetCategoryByGroup")
        Db.AddParameter("@GGID", ggid)
        If Not Db.Execute(dt) Then
            Db.Close()
        End If
        Db.Close()
        'gvCategories.DataSource = dt
        'gvCategories.DataBind()
        dropCategories.DataSource = dt
        dropCategories.DataTextField = "Type"
        dropCategories.DataValueField = "VMCategoryId"
        dropCategories.DataBind()
        If dropCategories.Items.Count > 0 Then
            AddPleaseSelect(dropCategories)
        End If
    End Sub

    Private Sub GetGroupCategories(ByVal groupID As Integer)
        Dim dt As New DataTable()

        Dim Db As DataBase = New DataBase
        Db.Init("VM_GetCategoryByGroup")
        Db.AddParameter("@GGID", groupID)
        If Not Db.Execute(dt) Then
            Db.Close()
        End If
        Db.Close()
        Dim HasRows = True
        If dt.Rows.Count = 0 Then
            HasRows = False
            dt.Rows.Add(dt.NewRow)
        End If
        gvCategories.DataSource = dt
        gvCategories.DataBind()
        'dropCategories.DataSource = dt
        'dropCategories.DataTextField = "Type"
        'dropCategories.DataValueField = "VMCategoryId"
        'dropCategories.DataBind()
        'If dropCategories.Items.Count > 0 Then
        '    AddPleaseSelect(dropCategories)
        'End If
        Dim myScript As String = "$(document).ready(function () {jqdialogShowControl();});"
        If HasRows = False Then
            myScript = "$(document).ready(function () {HideCategoryRow('" & gvCategories.ClientID & "');jqdialogShowControl();});"
        End If
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowDialogScriptGC1", myScript, True)
    End Sub
    Private Sub GetGroupCommunities(ByVal groupID As Integer)
        Dim dt As New DataTable()

        Dim Db As DataBase = New DataBase
        Db.Init("VM_GetCommunityByGroup")
        Db.AddParameter("@GGID", groupID)
        If Not Db.Execute(dt) Then
            Db.Close()
        End If
        Db.Close()
        Dim HasRows = True
        If dt.Rows.Count = 0 Then
            HasRows = False
            dt.Rows.Add(dt.NewRow)
        End If
        gvCommunities.DataSource = dt
        gvCommunities.DataBind()
        'dropCategories.DataSource = dt
        'dropCategories.DataTextField = "Type"
        'dropCategories.DataValueField = "VMCategoryId"
        'dropCategories.DataBind()
        'If dropCategories.Items.Count > 0 Then
        '    AddPleaseSelect(dropCategories)
        'End If
        Dim myScript As String = "$(document).ready(function () {jqdialogShowCommunityControl();});"
        If HasRows = False Then
            myScript = "$(document).ready(function () {HideCategoryRow('" & gvCommunities.ClientID & "');jqdialogShowCommunityControl();});"
        End If
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowDialogScriptGC1", myScript, True)
    End Sub
    Private Sub GetGroupUserCommunities()
        Dim dt As New DataTable()

        Dim Db As DataBase = New DataBase
        Db.Init("VM_GetUserCommunityByGroup")
        Db.AddParameter("@UserID", dropDialogUsers.SelectedValue)

        If Not Db.Execute(dt) Then
            Db.Close()
        End If
        Db.Close()
        Dim HasRows = True
        'If dt.Rows.Count = 0 Then
        '    HasRows = False
        '    dt.Rows.Add(dt.NewRow)
        'End If
        gvUserCommunities.DataSource = dt
        gvUserCommunities.DataBind()
        'dropCategories.DataSource = dt
        'dropCategories.DataTextField = "Type"
        'dropCategories.DataValueField = "VMCategoryId"
        'dropCategories.DataBind()
        'If dropCategories.Items.Count > 0 Then
        '    AddPleaseSelect(dropCategories)
        'End If
        Dim myScript As String = "$(document).ready(function () {jqdialogShowUserControl();});"
        'If HasRows = False Then
        '    myScript = "$(document).ready(function () {HideCategoryRow('" & gvUserCommunities.ClientID & "');jqdialogShowUserControl();});"
        'End If
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowDialogScriptGUC1", myScript, True)
    End Sub
    Private Sub GetGroupCategoryCommunities(ByVal groupID As Integer)
        Dim dt As New DataTable()

        Dim Db As DataBase = New DataBase
        Db.Init("VM_GetCommunityByGroup")
        Db.AddParameter("@GGID", groupID)
        If Not Db.Execute(dt) Then
            Db.Close()
        End If
        Db.Close()
        Dim HasRows = True
        If dt.Rows.Count = 0 Then
            HasRows = False
            dt.Rows.Add(dt.NewRow)
        End If
        dropDialogCommunityCategory.DataSource = dt
        dropDialogCommunityCategory.DataTextField = "CommName"
        dropDialogCommunityCategory.DataValueField = "CommId"
        dropDialogCommunityCategory.DataBind()
        'dropCategories.DataSource = dt
        'dropCategories.DataTextField = "Type"
        'dropCategories.DataValueField = "VMCategoryId"
        'dropCategories.DataBind()
        'If dropCategories.Items.Count > 0 Then
        '    AddPleaseSelect(dropCategories)
        'End If
        Dim myScript As String = "$(document).ready(function () {jqdialogShowCommunityCategoryControl();});"
        'If HasRows = False Then
        '    myScript = "$(document).ready(function () {HideCategoryRow('" & gvCategories.ClientID & "');jqdialogShowCommunity();});"
        'End If
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowDialogScriptGC11", myScript, True)
        dropDialogCommunityCategory_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub GetGroupCommunityCategories(ByVal commID As Integer)
        Dim dt As New DataTable()

        Dim Db As DataBase = New DataBase
        Db.Init("VM_GetCategoriesByCommID")
        Db.AddParameter("@CommID", commID)
        If Not Db.Execute(dt) Then
            Db.Close()
        End If
        Db.Close()
        Dim HasRows = True
        If dt.Rows.Count = 0 Then
            HasRows = False
            'dt.Rows.Add(dt.NewRow)
        End If

        gvCommunityCategories.DataSource = dt
        gvCommunityCategories.DataBind()


        'dropCategories.DataSource = dt
        'dropCategories.DataTextField = "Type"
        'dropCategories.DataValueField = "VMCategoryId"
        'dropCategories.DataBind()
        'If dropCategories.Items.Count > 0 Then
        '    AddPleaseSelect(dropCategories)
        'End If
        Dim myScript As String = "$(document).ready(function () {jqdialogShowCommunityCategoryControl();});"
        'If HasRows = False Then
        '    myScript = "$(document).ready(function () {HideCategoryRow('" & gvCategories.ClientID & "');jqdialogShowCommunity();});"
        'End If
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowDialogScriptGC111", myScript, True)
    End Sub

    ''' <summary>
    ''' Get Groups
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetGroups()
        Dim dt As New DataTable()

        Dim Db As DataBase = New DataBase
        Db.Init("VM_GetGroup")
        If Not Db.Execute(dt) Then
            Db.Close()
        End If
        Db.Close()
        gvGroups.DataSource = dt
        gvGroups.DataBind()
        'dropCategories.DataSource = dt
        'dropCategories.DataTextField = "Type"
        'dropCategories.DataValueField = "VMCategoryId"
        'dropCategories.DataBind()
        'If dropCategories.Items.Count > 0 Then
        '    AddPleaseSelect(dropCategories)
        'End If
    End Sub
    Private Sub BindGroups()
        Dim dt As New DataTable()

        Dim Db As DataBase = New DataBase
        Db.Init("VM_GetGroup")
        If Not Db.Execute(dt) Then
            Db.Close()
        End If
        Db.Close()

        dropDialogGroups.DataSource = dt
        dropDialogGroups.DataTextField = "GlobalGroupName"
        dropDialogGroups.DataValueField = "GGID"
        dropDialogGroups.DataBind()
        dropGroups.DataSource = dt
        dropGroups.DataTextField = "GlobalGroupName"
        dropGroups.DataValueField = "GGID"
        dropGroups.DataBind()
        dropDialogCommunityGroups.DataSource = dt
        dropDialogCommunityGroups.DataTextField = "GlobalGroupName"
        dropDialogCommunityGroups.DataValueField = "GGID"
        dropDialogCommunityGroups.DataBind()
        dropDialogCurrentGroup.DataSource = dt
        dropDialogCurrentGroup.DataTextField = "GlobalGroupName"
        dropDialogCurrentGroup.DataValueField = "GGID"
        dropDialogCurrentGroup.DataBind()
        dropDialogCopyGroup.DataSource = dt
        dropDialogCopyGroup.DataTextField = "GlobalGroupName"
        dropDialogCopyGroup.DataValueField = "GGID"
        dropDialogCopyGroup.DataBind()


        If dropDialogGroups.Items.Count > 0 Then
            AddPleaseSelect(dropDialogGroups)
            AddPleaseSelect(dropGroups)
            AddPleaseSelect(dropDialogCommunityGroups)
            AddPleaseSelect(dropDialogCurrentGroup)
            AddPleaseSelect(dropDialogCopyGroup)
        End If
        dropDialogGroups_SelectedIndexChanged(Nothing, Nothing)
    End Sub
    Private Sub BindCommunityGroups()
        Dim dt As New DataTable()

        Dim Db As DataBase = New DataBase
        Db.Init("VM_GetGroup")
        If Not Db.Execute(dt) Then
            Db.Close()
        End If
        Db.Close()


        dropDialogCommunityGroups.DataSource = dt
        dropDialogCommunityGroups.DataTextField = "GlobalGroupName"
        dropDialogCommunityGroups.DataValueField = "GGID"
        dropDialogCommunityGroups.DataBind()
        If dropDialogCommunityGroups.Items.Count > 0 Then
            AddPleaseSelect(dropDialogCommunityGroups)
        End If
        dropDialogCommunityGroups_SelectedIndexChanged(Nothing, Nothing)
    End Sub
    Private Sub BindCommunityCategoryGroups()
        Dim dt As New DataTable()

        Dim Db As DataBase = New DataBase
        Db.Init("VM_GetGroup")
        If Not Db.Execute(dt) Then
            Db.Close()
        End If
        Db.Close()


        dropDialogCommunityCategoryGroups.DataSource = dt
        dropDialogCommunityCategoryGroups.DataTextField = "GlobalGroupName"
        dropDialogCommunityCategoryGroups.DataValueField = "GGID"
        dropDialogCommunityCategoryGroups.DataBind()
        'If dropDialogCommunityGroups.Items.Count > 0 Then
        '    AddPleaseSelect(dropDialogCommunityGroups)
        'End If
        dropDialogCommunityCategoryGroups_SelectedIndexChanged(Nothing, Nothing)
    End Sub
    Private Sub BindGroups1()
        Dim dt As New DataTable()

        Dim Db As DataBase = New DataBase
        Db.Init("VM_GetGroup")
        If Not Db.Execute(dt) Then
            Db.Close()
        End If
        Db.Close()
        dropGroups.DataSource = dt
        dropGroups.DataTextField = "GlobalGroupName"
        dropGroups.DataValueField = "GGID"
        dropGroups.DataBind()
        If dropGroups.Items.Count > 0 Then
            AddPleaseSelect(dropGroups)
        End If
        dropGroups_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub BindUsers()
        Dim dt As New DataTable()

        Dim Db As DataBase = New DataBase
        Db.Init("VM_GetAllUsers")
        If Not Db.Execute(dt) Then
            Db.Close()
        End If
        Db.Close()
        dropDialogUsers.DataSource = dt
        dropDialogUsers.DataTextField = "FullName"
        dropDialogUsers.DataValueField = "UserID"
        dropDialogUsers.DataBind()
        If dropDialogUsers.Items.Count > 0 Then
            AddPleaseSelect(dropDialogUsers)
        End If
        'dropDialogUserGroups_SelectedIndexChanged(Nothing, Nothing)
    End Sub
    Private Sub BindUserGroupCommunities()
        dropDialogUserCommunities.Items.Clear()
        Dim dt As New DataTable()

        Dim Db As DataBase = New DataBase
        Db.Init("VM_GetUserGroupAdmin")
        Db.AddParameter("@UserID", dropDialogUsers.SelectedValue)
        If Not Db.Execute(dt) Then
            Db.Close()
        End If
        Db.Close()
        Dim GroupId = dt.Rows(0)("GGID")
        dt = New DataTable()

        Db = New DataBase
        Db.Init("VM_GetCommunityByUserGroup")
        Db.AddParameter("@GGID", GroupId)
        Db.AddParameter("@UserID", dropDialogUsers.SelectedValue)
        If Not Db.Execute(dt) Then
            Db.Close()
        End If
        Db.Close()
        Dim HasRows = True
        If dt.Rows.Count = 0 Then
            HasRows = False

        End If

        dropDialogUserCommunities.DataSource = dt
        dropDialogUserCommunities.DataTextField = "CommName"
        dropDialogUserCommunities.DataValueField = "CommID"
        dropDialogUserCommunities.DataBind()
        'If dropDialogUserCommunities.Items.Count > 0 Then
        '    AddPleaseSelect(dropDialogUserCommunities)
        'End If
    End Sub
    Shared Sub AddPleaseSelect(ByRef drop As DropDownList, Optional ByVal label As String = "Please Select", Optional ByVal cssclass As String = "PleaseSelect", Optional ByVal Force As Boolean = False)
        If drop.Items.Count > 1 Or Force Then
            If drop.Items.Count = 0 OrElse drop.Items(0).Text <> label Then
                drop.Items.Insert(0, New ListItem(label, ""))
            End If
            SetPleaseSelectClass(drop, cssclass)
        End If
    End Sub
    Shared Sub SetPleaseSelectClass(ByRef drop As DropDownList, Optional ByVal cssclass As String = "PleaseSelect")
        If drop.Items.Count > 1 Then drop.Items(0).Attributes("class") = cssclass
    End Sub

    Private Function AddNewCategory(ByVal category As String) As Boolean
        Dim isSuccess As Boolean = False
        Try
            lblCategoryError.ForeColor = Color.Red
            Dim ra As Integer = -1
            Dim Db As DataBase = New DataBase
            Db.Init("VM_AddCategory")
            Db.AddParameter("@Category", category)
            Db.AddParameter("@GGID", dropDialogGroups.SelectedValue)
            ra = Db.ExecuteAndReturn()
            Db.Close()
            If ra > 0 Then
                lblCategoryError.Text = "Category Name exists"
                divCategoryError.Visible = True
            Else
                GetGroupCategories(dropDialogGroups.SelectedValue)
                divCategoryError.Visible = False
                isSuccess = True
            End If

        Catch ex As Exception
            lblCategoryError.Text = "There was an error adding this category: " & ex.Message
            divCategoryError.Visible = True
        End Try
        Dim myScript As String = "$(document).ready(function () {jqdialogShowControl();});"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowDialogScript11", myScript, True)
        Return isSuccess
    End Function
    Private Function AddNewCopyCategory(ByVal category As String) As Boolean
        Dim isSuccess As Boolean = False
        Try
            lblCategoryError.ForeColor = Color.Red
            If String.IsNullOrEmpty(category) Then
                lblCategoryError.Text = "Please Select Category"
                divCategoryError.Visible = True
                isSuccess = False
            End If
            Dim ra As Integer = -1
            Dim Db As DataBase = New DataBase
            Db.Init("VM_AddCopyCategory")
            Db.AddParameter("@Category", category)
            Db.AddParameter("@GGID", dropDialogCurrentGroup.SelectedValue)
            Db.AddParameter("@SourceVMCategoryId", dropDialogCopyGroupCategory.SelectedValue)
            ra = Db.ExecuteAndReturn()
            Db.Close()
            If ra > 0 Then
                lblCategoryError.Text = "Category Name exists"
                divCategoryError.Visible = True
                isSuccess = False
            Else
                lblCategoryError.Text = "Category Copied successfully."
                lblCategoryError.ForeColor = Color.Blue
                divCategoryError.Visible = True
                'divCategoryError.Visible = False
                isSuccess = True
            End If

        Catch ex As Exception
            lblCategoryError.Text = "There was an error adding this category: " & ex.Message
            divCategoryError.Visible = True
            isSuccess = False
        End Try
        Dim myScript As String = "$(document).ready(function () {jqdialogShowControl();});"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowDialogScript11", myScript, True)
        Return isSuccess
    End Function
    Private Function AddNewCommunity(ByVal community As String) As Boolean
        Dim isSuccess As Boolean = False
        Try
            Dim ra As Integer = -1
            Dim Db As DataBase = New DataBase
            Db.Init("VM_AddCommunity")
            Db.AddParameter("@Community", community)
            Db.AddParameter("@GGID", dropDialogCommunityGroups.SelectedValue)
            ra = Db.ExecuteAndReturn()
            Db.Close()
            If ra > 0 Then
                lblCommunityError.Text = "Community Name exists"
                divCommunityError.Visible = True
            Else
                GetGroupCommunities(dropDialogCommunityGroups.SelectedValue)
                divCommunityError.Visible = False
                isSuccess = True
            End If

        Catch ex As Exception
            lblCommunityError.Text = "There was an error adding this community: " & ex.Message
            divCategoryError.Visible = True
        End Try
        Dim myScript As String = "$(document).ready(function () {jqdialogShowCommunityControl();});"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowDialogScript11", myScript, True)
        Return isSuccess
    End Function

    Private Function AddNewGroup(ByVal group As String) As Boolean
        Dim isSuccess As Boolean = False
        Try
            Dim ra As Integer = -1
            Dim Db As DataBase = New DataBase
            Db.Init("VM_AddGroup")
            Db.AddParameter("@Group", group)
            ra = Db.ExecuteAndReturn()
            Db.Close()
            If ra > 0 Then
                lblGroupError.Text = "Group Name exists"
                divGroupError.Visible = True
            Else
                GetGroups()
                BindGroups1()
                divGroupError.Visible = False
                isSuccess = True
            End If

        Catch ex As Exception
            lblGroupError.Text = "There was an error adding this group: " & ex.Message
            divGroupError.Visible = True
        End Try
        Dim myScript As String = "$(document).ready(function () {jqdialogShowGroupControl();});"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowDialogScriptG11", myScript, True)
        Return isSuccess
    End Function

    Private Sub GetPriorities(ByVal categoryId As String)
        Dim dt As New DataTable()
        Dim Db As DataBase = New DataBase
        Db.Init("VM_GetPriorities")
        Db.AddParameter("@CategoryId", categoryId)
        Try
            If Not Db.Execute(dt) Then
                Db.Close()

            End If
            Db.Close()
        Catch ex As Exception
            Db.Close()
        End Try

        gvPriorities.DataSource = dt
        gvPriorities.DataBind()
    End Sub

    Private Function AddNewPriority(ByVal priority As String, ByVal sort As Integer) As Boolean
        Dim isSuccess As Boolean = False
        Try
            Dim returnValue As Integer = -1
            Dim Db As DataBase = New DataBase
            Db.Init("VM_AddPriority")

            Db.AddParameter("@Priority", priority)
            Db.AddParameter("@CategoryId", dropCategories.SelectedValue)
            Db.AddParameter("@Sort", sort)
            returnValue = Db.ExecuteAndReturn()
            Db.Close()
            If returnValue > 0 Then
                lblError.Text = "Priority name exists"
            Else
                GetPriorities(dropCategories.SelectedValue)
                isSuccess = True
            End If
        Catch ex As Exception
            lblError.Text = "There was an error adding this priority: " & ex.Message
        End Try
        Return isSuccess
    End Function

    Private Function UpdatePriority(ByVal priority As String, ByVal sort As Integer, ByVal pId As Integer, ByVal oldPriorityName As String) As Boolean
        Dim isSuccess As Boolean = False
        Dim priorityChanged As Boolean = False
        If priority.ToLower() <> oldPriorityName.ToLower() Then
            priorityChanged = True
        End If
        Try
            Dim returnValue As Integer = -1
            Dim Db As DataBase = New DataBase
            Db.Init("VM_UpdatePriority")
            Db.AddParameter("@Priority", priority)
            Db.AddParameter("@CategoryId", dropCategories.SelectedValue)
            Db.AddParameter("@Sort", sort)
            Db.AddParameter("@PriorityId", pId)
            Db.AddParameter("@PriorityChanged", priorityChanged)
            returnValue = Db.ExecuteAndReturn()
            Db.Close()
            If returnValue > 0 Then
                lblError.Text = "Priority name exists"
            Else
                GetPriorities(dropCategories.SelectedValue)
                isSuccess = True
            End If
        Catch ex As Exception
            lblError.Text = "There was an error editing this priority: " & ex.Message
        End Try
        Return isSuccess
    End Function

    Private Function UpdateGroup(ByVal group As String, ByVal gid As Integer, ByVal oldGroupName As String) As Boolean
        Dim isSuccess As Boolean = False
        Dim groupChanged As Boolean = False
        If group.ToLower() <> oldGroupName.ToLower() Then
            groupChanged = True
        End If
        Try
            Dim returnValue As Integer = -1
            Dim Db As DataBase = New DataBase
            Db.Init("VM_UpdateGroup")
            Db.AddParameter("@Group", group)


            Db.AddParameter("@GGID", gid)
            Db.AddParameter("@GroupChanged", groupChanged)
            returnValue = Db.ExecuteAndReturn()
            Db.Close()
            If returnValue > 0 Then
                lblGroupError.Text = "Group name exists"
                divGroupError.Visible = True
            Else
                GetGroups()
                isSuccess = True
                BindGroups1()
                divGroupError.Visible = False
            End If
        Catch ex As Exception
            lblGroupError.Text = "There was an error editing this Group: " & ex.Message
            divGroupError.Visible = True
        End Try
        Return isSuccess
    End Function

    Private Function UpdateCategory(ByVal category As String, ByVal cid As Integer, ByVal oldCategoryName As String) As Boolean
        Dim isSuccess As Boolean = False
        Dim categoryChanged As Boolean = False
        If category.ToLower() <> oldCategoryName.ToLower() Then
            categoryChanged = True
        End If
        Try
            Dim returnValue As Integer = -1
            Dim Db As DataBase = New DataBase
            Db.Init("VM_UpdateCategory")
            Db.AddParameter("@Category", category)

            Db.AddParameter("@VmCategoryId", cid)
            Db.AddParameter("@GGID", dropDialogGroups.SelectedValue)
            Db.AddParameter("@CategoryChanged", categoryChanged)
            returnValue = Db.ExecuteAndReturn()
            Db.Close()
            If returnValue > 0 Then
                lblCategoryError.Text = "Category name exists"
                divCategoryError.Visible = True
            Else
                GetGroupCategories(dropDialogGroups.SelectedValue)
                isSuccess = True
                BindGroups1()
                divCategoryError.Visible = False
            End If
        Catch ex As Exception
            lblCategoryError.Text = "There was an error editing this Category: " & ex.Message
            divCategoryError.Visible = True
        End Try
        Return isSuccess
    End Function

    Private Function UpdateCommunity(ByVal community As String, ByVal cid As Integer, ByVal oldCommunityName As String) As Boolean
        Dim isSuccess As Boolean = False
        Dim communityChanged As Boolean = False
        If community.ToLower() <> oldCommunityName.ToLower() Then
            communityChanged = True
        End If
        Try
            Dim returnValue As Integer = -1
            Dim Db As DataBase = New DataBase
            Db.Init("VM_UpdateCommunity")
            Db.AddParameter("@CommName", community)

            Db.AddParameter("@CommId", cid)
            Db.AddParameter("@GGID", dropDialogCommunityGroups.SelectedValue)
            Db.AddParameter("@CommunityChanged", communityChanged)
            returnValue = Db.ExecuteAndReturn()
            Db.Close()
            If returnValue > 0 Then
                lblCommunityError.Text = "Community name exists"
                divCommunityError.Visible = True
            Else
                GetGroupCommunities(dropDialogCommunityGroups.SelectedValue)
                isSuccess = True
            End If
        Catch ex As Exception
            lblCategoryError.Text = "There was an error editing this Community: " & ex.Message
            divCommunityError.Visible = True
        End Try
        Return isSuccess
    End Function

    Private Sub GetPriorityDetails(ByVal PriorityId As Integer, ByVal VMType As Integer)
        Dim dt As New DataTable()
        Dim Db As DataBase = New DataBase
        Db.Init("VM_GetPriorityDetailsForMaint")
        Db.AddParameter("@PriorityId", PriorityId)
        Db.AddParameter("@VMType", VMType)

        Try
            If Not Db.Execute(dt) Then
                Db.Close()
            End If
            Db.Close()

        Catch ex As Exception
            Db.Close()
        End Try

        If VMType = 1 Then
            gvAffectsBenefits.DataSource = dt
            gvAffectsBenefits.DataBind()
        Else
            gvWWDC.DataSource = dt
            gvWWDC.DataBind()
        End If
    End Sub

    Private Function AddNewABPriorityDetails(ByVal CS As String, ByVal CSsort As Integer, ByVal FS As String, ByVal FSsort As Integer) As Boolean
        Dim isSuccess As Boolean = False
        Try
            Dim returnValue As Integer = -1
            Dim Db As DataBase = New DataBase
            Db.Init("VM_AddPriorityDetails")
            Db.AddParameter("@PriorityId", PriorityId)
            Db.AddParameter("@VMType", 1)
            Db.AddParameter("@CSSort", CSsort)
            Db.AddParameter("@FSSort", FSsort)
            Db.AddParameter("@CS", CS)
            Db.AddParameter("@FS", FS)
            returnValue = Db.ExecuteAndReturn()
            Db.Close()
            If returnValue > 0 Then
                lblABError.Text = "Affects/Benefits name exists"
                divABError.Visible = True
            Else
                GetPriorityDetails(PriorityId, 1)
                isSuccess = True
                divABError.Visible = False
            End If

        Catch ex As Exception
            lblABError.Text = "There was an error adding this Affects/Benefits: " & ex.Message
            divABError.Visible = True
        End Try
        ShowAffectsBenefitsPopUp()
        Return isSuccess
    End Function

    Private Function AddNewWWDPriorityDetails(ByVal CS As String, ByVal CSsort As Integer, ByVal FS As String, ByVal FSsort As Integer) As Boolean
        Dim isSuccess As Boolean = False
        Try
            Dim returnValue As Integer = -1
            Dim Db As DataBase = New DataBase
            Db.Init("VM_AddPriorityDetails")
            Db.AddParameter("@PriorityId", PriorityId)
            Db.AddParameter("@VMType", 2)
            Db.AddParameter("@CSSort", CSsort)
            Db.AddParameter("@FSSort", FSsort)
            Db.AddParameter("@CS", CS)
            Db.AddParameter("@FS", FS)
            returnValue = Db.ExecuteAndReturn()
            Db.Close()
            If returnValue > 0 Then
                lblWWDCError.Text = "WWDC/WWDF name exists"
                divWWDError.Visible = True
            Else
                GetPriorityDetails(PriorityId, 2)
                isSuccess = True
                divWWDError.Visible = False
            End If
        Catch ex As Exception
            lblWWDCError.Text = "There was an error adding this WWDC/WWDF: " & ex.Message
            divWWDError.Visible = True
        End Try
        ShowWWDCPopup()
        Return isSuccess
    End Function

    Private Sub ShowAffectsBenefitsPopUp()
        Dim myScript As String = "$(document).ready(function () {jqdialogABShowControl();});"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowDialogScript2", myScript, True)
    End Sub

    Private Sub ShowWWDCPopup()
        Dim myScript As String = "$(document).ready(function () {jqdialogWWDCShowControl();});"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowDialogScript3", myScript, True)
    End Sub

    Protected Sub btnCopyCategory_Click(sender As Object, e As EventArgs)
        divCopyGroupCategory.Visible = True
        divGroupCategory.Visible = False
        dropDialogCopyGroup.SelectedIndex = 0

        Try
            dropDialogCurrentGroup.SelectedValue = dropDialogGroups.SelectedValue
            dropDialogCurrentGroup.Enabled = False
        Catch ex As Exception
            dropDialogCurrentGroup.SelectedIndex = 0
            dropDialogCurrentGroup.Enabled = False
        End Try
        dropDialogCopyGroupCategory.Items.Clear()
        lblCategoryError.Text = ""
        divCategoryError.Visible = False
        dropDialogCopyGroupCategory.Items.Insert(0, New ListItem("Please Select", "0"))
        If Not String.IsNullOrEmpty(dropDialogGroups.SelectedValue) Then
            GetGroupCategories(dropDialogGroups.SelectedValue)
        Else
            gvCategories.DataSource = New DataTable()
            gvCategories.DataBind()
        End If
    End Sub

    Protected Sub dropDialogCopyGroup_SelectedIndexChanged(sender As Object, e As EventArgs)
        dropDialogCopyGroupCategory.Items.Clear()

        If Not String.IsNullOrEmpty(dropDialogCopyGroup.SelectedValue) Then
            Dim dt As New DataTable()

            Dim Db As DataBase = New DataBase
            Db.Init("VM_GetCategoryByGroup")
            Db.AddParameter("@GGID", dropDialogCopyGroup.SelectedValue)
            If Not Db.Execute(dt) Then
                Db.Close()
            End If
            Db.Close()
            Dim HasRows = True
            If dt.Rows.Count = 0 Then

            Else
                dropDialogCopyGroupCategory.DataSource = dt
                dropDialogCopyGroupCategory.DataTextField = "Type"
                dropDialogCopyGroupCategory.DataValueField = "VMCategoryID"
                dropDialogCopyGroupCategory.DataBind()
            End If
        Else
            dropDialogCopyGroupCategory.Items.Clear()
        End If
        dropDialogCopyGroupCategory.Items.Insert(0, New ListItem("Please Select", "0"))
        If Not String.IsNullOrEmpty(dropDialogGroups.SelectedValue) Then
            GetGroupCategories(dropDialogGroups.SelectedValue)
        Else
            gvCategories.DataSource = New DataTable()
            gvCategories.DataBind()
        End If
    End Sub

    Protected Sub btnSubmitCopyCategory_Click(sender As Object, e As EventArgs)
        lblCategoryError.Text = ""
        divCategoryError.Visible = False
        If dropDialogCurrentGroup.SelectedIndex <= 0 Then
            lblCategoryError.Text = "Please Select Current Group"
            divCategoryError.Visible = True
        ElseIf dropDialogCopyGroup.SelectedIndex <= 0 Then
            lblCategoryError.Text = "Please Select Copy from Group"
            divCategoryError.Visible = True
            'ElseIf dropDialogCurrentGroup.SelectedValue = dropDialogCopyGroup.SelectedValue Then
            '    lblCategoryError.Text = "Current Group and Copy from Group should not be same."
            '    divCategoryError.Visible = True
        ElseIf dropDialogCopyGroupCategory.SelectedIndex <= 0 Then
            lblCategoryError.Text = "Please Select Category"
            divCategoryError.Visible = True
        Else
            Dim isAdded As Boolean = AddNewCopyCategory(dropDialogCopyGroupCategory.SelectedItem.Text & " - copy ")
            If isAdded = True Then
                dropDialogGroups.SelectedValue = dropDialogCurrentGroup.SelectedValue
                dropDialogGroups_SelectedIndexChanged(sender, Nothing)
                divCopyGroupCategory.Visible = False
                divGroupCategory.Visible = True
            End If

        End If

    End Sub

    Protected Sub btnCancelCopyCategory_Click(sender As Object, e As EventArgs)
        divCopyGroupCategory.Visible = False
        divGroupCategory.Visible = True
    End Sub



#End Region

End Class