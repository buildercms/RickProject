Imports RickProject.DB
Imports System.Data
Public Class MasterProcessor
    Public Function GetCategoryByGGID(ByVal GGID As Integer, ByRef table As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Db.Init("GetCategoryByGGID")
        Db.AddParameter("@GGID", GGID, 10, SqlDbType.Int, ParameterDirection.Input)
        If Not Db.Execute(table) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetCategoryByCommID(ByVal commID As Integer, ByVal userID As Integer, ByRef table As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Db.Init("GetCategoryByCommID")
        Db.AddParameter("@CommID", commID, 10, SqlDbType.Int, ParameterDirection.Input)
        Db.AddParameter("@UserId", userID, 10, SqlDbType.Int, ParameterDirection.Input)
        If Not Db.Execute(table) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetToDoStatus(ByRef table As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Db.Init("GetAllToDoStatus")
        If Not Db.Execute(table) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
End Class
