Imports RickProject.Business

Public Class MyProfile
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Config.LoginUserType) Then
            Response.Redirect("~/Login.aspx")
        End If
        If Not Page.IsPostBack Then
            BindUserProfileInfo(Config.UserId)
            If System.IO.File.Exists(Server.MapPath("~/CustomFiles/UserImages/" & Config.UserId & "/" & Config.UserId & ".png")) Then
                imgLogo.Src = "~/CustomFiles/UserImages/" & Config.UserId & "/" & Config.UserId & ".png" & "?" & DateTime.Now.Ticks
            Else
                imgLogo.Src = "~/Images/browse.png"
            End If
            If System.IO.File.Exists(Server.MapPath("~/CustomFiles/UserImages/" & Config.UserId & "/" & Config.UserId & "_Logo.png")) Then
                imgEditLogo.Src = "~/CustomFiles/UserImages/" & Config.UserId & "/" & Config.UserId & "_Logo.png" & "?" & DateTime.Now.Ticks
            Else
                imgEditLogo.Src = "~/Images/browse.png"
            End If
        End If
    End Sub
    Public Sub BindUserProfileInfo(ByVal userID As Integer)
        Dim dt As New DataTable()
        Dim objUserProcessor As New UserProcessor
        objUserProcessor.GetUserProfileInfo(userID, dt)
        lblViewContactFirstName.Text = Convert.ToString(dt.Rows(0)("FirstName"))
        lblViewContactLastName.Text = Convert.ToString(dt.Rows(0)("LastName"))
        'lblViewCompany.Text = Convert.ToString(dt.Rows(0)(""))
        lblViewCompany.Text = Convert.ToString(dt.Rows(0)("CompanyName"))
        lblViewContactMobilePhone.Text = Config.FormatPhoneNumber(Convert.ToString(dt.Rows(0)("CellPhone")))
        lblViewContactEmail.Text = Convert.ToString(dt.Rows(0)("EmailId"))
        lblMyPlan.Text = "Gold Plan"
        lblPricing.Text = "$150/month"
        lblPaymentInfo.Text = "**** **** **** 4956"
        lblBillingAddress.Text = Convert.ToString(dt.Rows(0)("OfficeStreet"))
        lblCityState.Text = Convert.ToString(dt.Rows(0)("OfficeCity")) & ", " & Convert.ToString(dt.Rows(0)("OfficeState")).ToUpper()
        lblZip.Text = Convert.ToString(dt.Rows(0)("OfficeZip"))
    End Sub

    Protected Sub btnSaveLogo_Click(sender As Object, e As EventArgs)
        If fupPhoto1.HasFile Then
            If (Not System.IO.Directory.Exists(Server.MapPath("~/CustomFiles/UserImages/" & Config.UserId))) Then
                System.IO.Directory.CreateDirectory(Server.MapPath("~/CustomFiles/UserImages/" & Config.UserId))
            End If
            fupPhoto1.PostedFile.SaveAs(Server.MapPath("~/CustomFiles/UserImages/" & Config.UserId & "/" & Config.UserId & ".png"))
            Response.Redirect(Request.RawUrl)
        End If
    End Sub
    Protected Sub btnSaveEditLogo_Click(sender As Object, e As EventArgs)
        If fupLogoPhoto1.HasFile Then
            If (Not System.IO.Directory.Exists(Server.MapPath("~/CustomFiles/UserImages/" & Config.UserId))) Then
                System.IO.Directory.CreateDirectory(Server.MapPath("~/CustomFiles/UserImages/" & Config.UserId))
            End If
            fupLogoPhoto1.PostedFile.SaveAs(Server.MapPath("~/CustomFiles/UserImages/" & Config.UserId & "/" & Config.UserId & "_Logo.png"))
            Response.Redirect(Request.RawUrl)
        End If
    End Sub
End Class