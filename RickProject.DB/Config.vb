Imports System.Data.SqlClient
Public Class Config

    Public _ConnectingString As String
    Public Property ConnectionString() As String
        Get
            Return System.Configuration.ConfigurationManager.AppSettings("strConnectionStringRISDev")
        End Get
        Set(ByVal value As String)
            _ConnectingString = value
        End Set
    End Property

    Public Sub IsLive()

    End Sub


End Class
