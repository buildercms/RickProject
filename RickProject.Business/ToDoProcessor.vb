Imports RickProject.DB
Public Class ToDoProcessor
    Public Function InsertToDoDetails(ByVal title As String, ByVal description As String, ByVal dueDate As DateTime, ByVal status As Integer, ByVal userID As Integer, ByVal custID As Integer) As Integer
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("InsertToDoDetails")
        Db.AddParameter("@UserID", userID)
        Db.AddParameter("@CustomerId", custID)
        Db.AddParameter("@Title", title)
        Db.AddParameter("@DueDate", dueDate)
        Db.AddParameter("@Status", status)
        Db.AddParameter("@Description", description)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        Return returnValue
    End Function
    Public Function UpdateToDoDetails(ByVal title As String, ByVal description As String, ByVal dueDate As DateTime, ByVal status As Integer, ByVal userID As Integer, ByVal custID As Integer, ByVal Id As Integer) As Integer
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("UpdateToDoDetails")
        Db.AddParameter("@UserID", userID)
        Db.AddParameter("@CustomerId", custID)
        Db.AddParameter("@Title", title)
        Db.AddParameter("@DueDate", dueDate)
        Db.AddParameter("@Status", status)
        Db.AddParameter("@Description", description)
        Db.AddParameter("@Id", Id)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        Return returnValue
    End Function
    Public Function UpdateToDoDetailsById(ByVal userID As Integer, ByVal Id As Integer) As Integer
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("UpdateToDoDetailsById")
        Db.AddParameter("@UserID", userID)
        Db.AddParameter("@Id", Id)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        Return returnValue
    End Function
    Public Function GetToDoDetailsById(ByVal Id As Integer, ByRef table As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("GetToDoDetailsByToDoId")
        Db.AddParameter("@ToDoId", Id)
        If Not Db.Execute(table) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetAllToDoListByCustomer(ByVal userId As Integer, ByVal custId As Integer, ByRef table As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("GetToDoListByCustomer")
        Db.AddParameter("@UserId", userId)
        Db.AddParameter("@CustomerId", custId)
        If Not Db.Execute(table) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetAllToDoListByUser(ByVal userId As Integer, ByRef table As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("GetAllToDoListByUser")
        Db.AddParameter("@UserId", userId)
        If Not Db.Execute(table) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetAllToDoListByUserforHome(ByVal userId As Integer, ByRef table As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("GETALLTodayToDoListByUserforHome")
        Db.AddParameter("@UserId", userId)
        If Not Db.Execute(table) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetAllUpComingToDoListByUser(ByVal userId As Integer, ByRef table As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("GETALLUpComingToDoListByUser")
        Db.AddParameter("@UserId", userId)
        If Not Db.Execute(table) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetAllTodayToDoListByUser(ByVal userId As Integer, ByRef table As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("GETALLTodayToDoListByUser")
        Db.AddParameter("@UserId", userId)
        If Not Db.Execute(table) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function UpdateToDOStatus(ByVal todoId As Integer) As Integer
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("UpdateToDOStatus")
        Db.AddParameter("@ToDoId", todoId)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        Return returnValue
        Return True
    End Function
End Class
