Imports RickProject.Business
Imports RickProject.DB

Public Class Site
    Inherits System.Web.UI.MasterPage
    Protected Sub OnPreInit(ByVal e As EventArgs)
        Dim rawURL As String = HttpContext.Current.Request.Url.AbsoluteUri

        If rawURL.ToLower().Contains("reimaginesellinglogin.com") Then
            DataBase.InstanceName = "production"
        Else
            DataBase.InstanceName = "test"
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim rawURL As String = HttpContext.Current.Request.Url.AbsoluteUri
        If rawURL.ToLower().Contains("reimaginesellinglogin.com") Then
            DataBase.InstanceName = "production"
        Else
            DataBase.InstanceName = "test"
        End If
        If Session("CurrentLogin") Is Nothing Then
            Response.Redirect("~/Login.aspx")
        Else
            Dim user As RickProject.Business.User = Session("CurrentLogin")
            aUser.InnerHtml = user.FirstName & " " & user.LastName
        End If
        If Not Request.QueryString("action") Is Nothing Then
            btnTest_Click(sender, Nothing)
        End If
        If System.IO.File.Exists(Server.MapPath("~/CustomFiles/UserImages/" & Config.UserId & "/" & Config.UserId & ".png")) Then
            imgMasterLogo.Src = "~/CustomFiles/UserImages/" & Config.UserId & "/" & Config.UserId & ".png" & "?" & DateTime.Now.Ticks
        Else
            imgMasterLogo.Src = "~/Images/browse.png"
        End If
        If Not Page.IsPostBack Then
            hidTermsAccepted.Value = Config.IsTermsAccepted.ToString().ToLower()
        End If
        If Config.CurrentUserName <> "rheaston" Then
            liCategoryMaint.Visible = False
        End If
        If Config.CurrentUserName.ToLower = "tester08" Or Config.CurrentUserName.ToLower = "scate" Or Config.CurrentUserName.ToLower = "mmaynardm" Or Config.CurrentUserName.ToLower = "bjohnson" Or Config.CurrentUserName.ToLower = "brademacher" Then
            liReports.Visible = True
        Else
            liReports.Visible = False
        End If
    End Sub

    Protected Sub lnkLogout_Click(sender As Object, e As EventArgs)
        Session.Abandon()
        Response.Redirect("~/Login.aspx")
    End Sub





    Protected Sub btnTest_Click(sender As Object, e As EventArgs)
        Dim redirectURL = "~/AddToDo.aspx?cid=" & Request.QueryString("cid")
        If Request.RawUrl.ToLower().Contains("overview") Then
            redirectURL &= "&mode=overview"
        ElseIf Request.RawUrl.ToLower().Contains("valuemap") Then
            redirectURL &= "&mode=overview"
        ElseIf Request.RawUrl.ToLower().Contains("property") Then
            redirectURL &= "&mode=property"
        End If
        Response.Redirect(redirectURL)
    End Sub

    Protected Sub btnOkTerms_ServerClick(sender As Object, e As EventArgs)
        Dim objUserProcessor As New UserProcessor()
        objUserProcessor.UpdateUserTerms(Config.UserId)
        Config.IsTermsAccepted = True
        hidTermsAccepted.Value = "true"

    End Sub
End Class