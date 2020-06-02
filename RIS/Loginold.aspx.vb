Imports RickProject.Business
Imports c = RIS.Common

Public Class Loginold
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click

        If Page.IsValid Then

            'If c.isSysAdmin(txtUserId.Value.Trim(), txtPassword.Value.Trim()) Then
            '    Config.LoginUserType = c.UserType.SysAdmin
            '    Config.UserId = 1
            '    c.RecordAuditLog(-1, "Users", "Sysadmin Logged In", 1)

            '    FormsAuthentication.RedirectFromLoginPage(Config.UserId.ToString, False)
            'Else
            Dim Usertable As DataTable = New DataTable
            Dim user As RickProject.Business.User = New RickProject.Business.User
            user.UserName = txtUserId.Value.Trim()
            user.Password = txtPassword.Value.Trim()
            Dim userProcessor As UserProcessor = New UserProcessor

            If userProcessor.CheckUser(user.UserName, user.Password, Usertable) Then
                If Not (Usertable Is Nothing Or Usertable.Rows.Count <= 0) Then
                    'If IsDBNull(Usertable.Rows(0)("ProjectId")) Then
                    '    lblmsg.Text = "You have no permissions in any project. Please contact your system administrator.<br />"
                    'Else
                    InitializeUser(Usertable, user)


                    Dim table As DataTable = New DataTable

                    Session("CurrentLogin") = user

                    c.RecordAuditLog(Config.UserId, "Users", Config.CurrentUserName & " Logged In", Config.UserId)
                    LoadDefaultPage(Config.LoginUserType)
                Else
                    lblmsg.Text = "Incorrect User name or Password.<br />"
                    lblmsg.ForeColor = System.Drawing.Color.Red
                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Invalid User Name or Password.');", True)
                End If

            Else
                lblmsg.Text = "Incorrect User name or Password.<br />"
                lblmsg.ForeColor = System.Drawing.Color.Red
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "msgbox", "alert('Invalid User Name or Password.');", True)
            End If
        End If

        'End If

        'End If

    End Sub
    Private Sub InitializeUser(ByVal UserInfo As DataTable, ByVal user As RickProject.Business.User)
        Config.UserId = UserInfo.Rows(0)("UserId")
        Config.LoginUserType = UserInfo.Rows(0)("GGID")
        'Config.LoginUserType = DirectCast([Enum].Parse(GetType(Common.UserType), UserInfo.Rows(0)("UserTypeName")), Common.UserType)
        'Config.CurrentUserName = UserInfo.Rows(0)("FirstName") + " " + UserInfo.Rows(0)("LastName")
        'Config.CompanyId = UserInfo.Rows(0)("CompanyId")
        'Config.CompanyName = UserInfo.Rows(0)("CompanyName")
        'Config.ActiveProjectSetup = False
        'Config.RecordToReturn = UserInfo.Rows(0)("RecordsToReturn")
        user.UserId = UserInfo.Rows(0)("UserId")
        user.FirstName = UserInfo.Rows(0)("FirstName")
        user.LastName = UserInfo.Rows(0)("LastName")
        user.GGID = UserInfo.Rows(0)("GGID")
        user.UserName = UserInfo.Rows(0)("UserName")
    End Sub
    Private Sub LoadDefaultPage(ByVal userType As Common.UserType)

        FormsAuthentication.RedirectFromLoginPage(Config.UserId.ToString, False)
        'Select Case userType
        '	Case Common.UserType.SysAdmin
        '		Response.Redirect("CompanyList.aspx")
        '	Case Common.UserType.SuperUser
        '		Response.Redirect("Home.aspx")
        '	Case Else
        '		Response.Redirect("Home.aspx")
        'End Select

    End Sub

End Class
