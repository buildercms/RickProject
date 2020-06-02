Imports System.Drawing
Imports System.IO
Imports System.Web
Imports System.Web.Services

Public Class FileUploadHandler
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        Dim customerId As Integer = context.Request.Form("customerId")
        Dim type As String = context.Request.Form("type")
        Dim fname As String = String.Empty
        Dim filePath As String = String.Empty
        If context.Request.Files.Count > 0 Then
            Dim files As HttpFileCollection = context.Request.Files

            For i As Integer = 0 To files.Count - 1
                Dim file As HttpPostedFile = files(i)

                If type = "property" Then
                    'fname = context.Server.MapPath("~/CustomFiles/property/Temp/" & customerId & DateTime.Now.Millisecond & System.IO.Path.GetExtension(file.FileName))
                    filePath = "~/CustomFiles/property/Temp/" & customerId & DateTime.Now.Millisecond & System.IO.Path.GetExtension(file.FileName)
                End If
                file.SaveAs(context.Server.MapPath(filePath))
            Next
            'Dim fileInfo As FileInfo = New FileInfo(context.Server.MapPath("~/CustomFiles/property/Temp/" & customerId & System.IO.Path.GetExtension(File.FileName))

            Dim img As Bitmap = New Bitmap(context.Server.MapPath(filePath))
            Dim imageHeight = img.Height
            Dim imageWidth = img.Width
            context.Response.ContentType = "text/plain"
            context.Response.Write(filePath.Replace("~", "") & "~" & imageWidth & "~" & imageHeight)
        End If

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class