Imports RickProject.Business
Imports RickProject.DB
Imports c = RIS.Common

Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim rawURL As String = HttpContext.Current.Request.Url.AbsoluteUri
        If rawURL.ToLower().Contains("reimaginesellinglogin.com") Then
            DataBase.InstanceName = "production"
        Else
            DataBase.InstanceName = "test"
        End If
        txtUserId.Focus()
        If Not String.IsNullOrEmpty(Request.QueryString("uid")) Then
            Dim userID As Integer = Request.QueryString("uid")
            Dim customerID As Integer = Request.QueryString("cid")
            Session("riscid") = customerID
            Dim userProcessor As UserProcessor = New UserProcessor
            Dim dt As New DataTable
            userProcessor.GetUserProfileInfo(userID, dt)
            txtUserId.Value = dt.Rows(0)("UserName")
            txtPassword.Value = dt.Rows(0)("Password")
            btnLogin_Click(sender, Nothing)
        End If

    End Sub
    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click

        ' If Page.IsValid Then

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
                    userProcessor.InsertUserLogin(Config.UserId, "")
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
        'End If

        'End If

        'End If

    End Sub
    Private Sub InitializeUser(ByVal UserInfo As DataTable, ByVal user As RickProject.Business.User)
        Config.UserId = UserInfo.Rows(0)("UserId")
        Config.LoginUserType = UserInfo.Rows(0)("GGID")
        Config.IsTermsAccepted = UserInfo.Rows(0)("IsTermsAccepted")
        'Config.LoginUserType = DirectCast([Enum].Parse(GetType(Common.UserType), UserInfo.Rows(0)("UserTypeName")), Common.UserType)
        Config.CurrentUserName = UserInfo.Rows(0)("UserName")
        'Config.CompanyId = UserInfo.Rows(0)("CompanyId")
        'Config.CompanyName = UserInfo.Rows(0)("CompanyName")
        'Config.ActiveProjectSetup = False
        'Config.RecordToReturn = UserInfo.Rows(0)("RecordsToReturn")
        user.UserId = UserInfo.Rows(0)("UserId")
        user.FirstName = UserInfo.Rows(0)("FirstName")
        user.LastName = UserInfo.Rows(0)("LastName")
        user.GGID = UserInfo.Rows(0)("GGID")
        user.UserName = UserInfo.Rows(0)("UserName")
        user.OfficeStreet = UserInfo.Rows(0)("OfficeStreet")
        user.OfficeCity = UserInfo.Rows(0)("OfficeCity")
        user.OfficeState = UserInfo.Rows(0)("OfficeState")
        user.OfficeZip = UserInfo.Rows(0)("OfficeZip")
        user.OfficeCountry = UserInfo.Rows(0)("OfficeCountry")
        user.Phone = UserInfo.Rows(0)("CellPhone")
        user.Email = Convert.ToString(UserInfo.Rows(0)("EmailId"))
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