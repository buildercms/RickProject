Imports RickProject.Business

Imports RickProject.DB
Public Class SiteCustomer
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
    End Sub
End Class