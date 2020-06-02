Imports RickProject.DB
Imports System.Data
Public Class UserProcessor
    Public Function CheckUser(ByVal userID As String, ByVal Password As String, ByRef table As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Db.Init("CheckUser")
        Db.AddParameter("@UserID", userID, 50, SqlDbType.NVarChar, ParameterDirection.Input)
        Db.AddParameter("@Password", Password, 50, SqlDbType.NVarChar, ParameterDirection.Input)
        If Not Db.Execute(table) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function InsertUserLogin(ByVal UserID As Integer, ByVal IPAddress As String) As Integer
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("InsertUserLogin")
        Db.AddParameter("@UserID", UserID)
        Db.AddParameter("@IPAddress", IPAddress)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        Return returnValue
    End Function
    Public Function GetUserLogin(ByVal UserID As Integer, ByRef table As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("GetUserLogin")
        Db.AddParameter("@UserID", UserID)
        If Not Db.Execute(table) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True

    End Function
    Public Shared Function InsertUserActivity(ByVal activityType As String, ByVal description As String, ByVal referenceId As String, ByVal userID As Integer) As Integer
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("InsertUserActivity")
        Db.AddParameter("@ActivityType", activityType)
        Db.AddParameter("@Description", description)
        Db.AddParameter("@ReferenceId", referenceId)
        Db.AddParameter("@CreatedUserId", userID)
        Db.AddParameter("@CreatedBy", userID)

        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        Return returnValue
    End Function
    Public Function GetUserActivity(ByVal UserID As Integer, ByRef table As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("GetUserActivity")
        Db.AddParameter("@UserID", UserID)
        If Not Db.Execute(table) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True

    End Function
    Public Function GetUserProfileInfo(ByVal UserID As Integer, ByRef table As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("GetUserProfileInfo")
        Db.AddParameter("@UserID", UserID)
        If Not Db.Execute(table) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True

    End Function
    '   Public Function CheckExistingUserLogin(ByVal userID As String, ByVal Password As String) As Boolean
    '       Dim Db As DataBase = New DataBase
    '       Db.Init("CheckExistingUserLogin")
    '       Db.AddParameter("@UserID", userID, 50, SqlDbType.NVarChar, ParameterDirection.Input)
    '       Db.AddParameter("@Password", Password, 50, SqlDbType.NVarChar, ParameterDirection.Input)
    '       Return Db.ExecuteScalar()
    '   End Function

    'Public Function CanEditUser(ByVal userID As String, ByVal ProjectID As String, ByVal isApl As Boolean) As Boolean
    '	Dim Db As DataBase = New DataBase
    '	Db.Init("CanEditUser")
    '	Db.AddParameter("@UserID", userID, 8, SqlDbType.Int, ParameterDirection.Input)
    '	Db.AddParameter("@ProjectID", ProjectID, 8, SqlDbType.Int, ParameterDirection.Input)
    '	Db.AddParameter("@IsApl", isApl)
    '	Return Db.ExecuteScalar()
    'End Function

    'Public Function GetSuperUserList(ByVal companyid As Integer, ByRef table As DataTable, ByVal isAPlSuperUser As Boolean) As Boolean
    '	Dim Db As DataBase = New DataBase
    '	Db.Init("GetSuperUserList")
    '	Db.AddParameter("@Companyid", companyid, 16, SqlDbType.Int, ParameterDirection.Input)
    '	Db.AddParameter("@IsAPlSuperUser", isAPlSuperUser, 2, SqlDbType.Bit, ParameterDirection.Input)

    '	If Not Db.Execute(table) Then
    '		Db.Close()
    '		Return False
    '	End If

    '	Db.Close()
    '	Return True
    '   End Function
    '   'Case 519
    '   Public Function GetPassword(ByVal Email As String, ByRef table As DataTable) As Boolean

    '       Dim Db As DataBase = New DataBase
    '       Db.Init("GetUserPassword")
    '       Db.AddParameter("@Email", Email, 50, SqlDbType.NVarChar, ParameterDirection.Input)
    '       If Not Db.Execute(table) Then
    '           Db.Close()
    '           Return False
    '       End If
    '       Db.Close()
    '       Return True

    '   End Function

    '   Public Function GetUserList(ByVal companyid As Integer, ByVal userid As String, ByVal GetActive As Boolean, ByRef table As DataTable) As Boolean
    '       Dim Db As DataBase = New DataBase
    '       Db.Init("GetUserList")
    '       Db.AddParameter("@Companyid", companyid, 16, SqlDbType.Int, ParameterDirection.Input)
    '       Db.AddParameter("@UserID", userid, 16, SqlDbType.Int, ParameterDirection.Input)
    '       Db.AddParameter("@GetActive", GetActive, 16, SqlDbType.Bit, ParameterDirection.Input)

    '       If Not Db.Execute(table) Then
    '           Db.Close()
    '           Return False
    '       End If

    '       Db.Close()
    '       Return True
    '   End Function

    '   Public Function GetIORUserList(ByVal Projectid As Integer, ByRef table As DataTable) As Boolean
    '       Dim Db As DataBase = New DataBase
    '       Db.Init("GetIORUserList")
    '       Db.AddParameter("@Projectid", Projectid, 16, SqlDbType.Int, ParameterDirection.Input)
    '       If Not Db.Execute(table) Then
    '           Db.Close()
    '           Return False
    '       End If

    '       Db.Close()
    '       Return True
    '   End Function

    '   Public Function GetRPUserList(ByVal companyid As Integer, ByVal userid As String, ByVal projectID As String, ByRef table As DataTable) As Boolean
    '       Dim Db As DataBase = New DataBase
    '       Db.Init("GetRPUserList")
    '       Db.AddParameter("@Companyid", companyid, 16, SqlDbType.Int, ParameterDirection.Input)
    '       Db.AddParameter("@UserID", userid, 16, SqlDbType.Int, ParameterDirection.Input)
    '       Db.AddParameter("@projectID", projectID, 16, SqlDbType.Int, ParameterDirection.Input)

    '       If Not Db.Execute(table) Then
    '           Db.Close()
    '           Return False
    '       End If

    '       Db.Close()
    '       Return True
    '   End Function

    'Public Function GetUserInfo(ByVal userID As Integer, ByRef table As DataTable, Optional ByVal CompanyID As Integer = 0) As Boolean
    '	Dim Db As DataBase = New DataBase
    '	Db.Init("GetUserInfo")


    '	Db.AddParameter("@UserId", userID)
    '	Db.AddParameter("@companyID", CompanyID)

    '	If Not Db.Execute(table) Then
    '		Db.Close()
    '		Return False
    '	End If
    '	Db.Close()
    '	Return True
    '   End Function

    'Public Function AddUpdateUser(ByVal User As User, ByVal InsertPermission As Boolean, ByVal isAplProject As Boolean) As Boolean
    '	Dim Db As DataBase = New DataBase
    '	Db.Init("AddUpdateUser")
    '	Db.AddParameter("@UserId", User.UserId, 16, SqlDbType.Int, ParameterDirection.InputOutput)
    '	Db.AddParameter("@CompanyId", User.CompanyId, 16, SqlDbType.Int, ParameterDirection.Input)
    '	Db.AddParameter("@UserTypeID", User.UserTypeID, 16, SqlDbType.Int, ParameterDirection.Input)
    '	Db.AddParameter("@UserName", User.UserName, 50, SqlDbType.NVarChar, ParameterDirection.Input)
    '	Db.AddParameter("@Password", User.Password, 50, SqlDbType.NVarChar, ParameterDirection.Input)
    '	Db.AddParameter("@FirstName", User.FirstName, 50, SqlDbType.NVarChar, ParameterDirection.Input)
    '	Db.AddParameter("@LastName", User.LastName, 50, SqlDbType.NVarChar, ParameterDirection.Input)
    '	Db.AddParameter("@Email", User.Email, 50, SqlDbType.NVarChar, ParameterDirection.Input)
    '	Db.AddParameter("@Address", User.Address.Address, 50, SqlDbType.NVarChar, ParameterDirection.Input)
    '	Db.AddParameter("@City", User.Address.City, 50, SqlDbType.NVarChar, ParameterDirection.Input)
    '	Db.AddParameter("@State", User.Address.State, 50, SqlDbType.NVarChar, ParameterDirection.Input)
    '	Db.AddParameter("@Zip", User.Address.Zip, 8, SqlDbType.NVarChar, ParameterDirection.Input)
    '	Db.AddParameter("@IsSuperUser", User.IsSuperUser, 2, SqlDbType.Bit, ParameterDirection.Input)
    '       Db.AddParameter("@Phone", User.Phone, 25, SqlDbType.NVarChar, ParameterDirection.Input)
    '	Db.AddParameter("@IsActive", User.IsActive, 2, SqlDbType.Bit, ParameterDirection.Input)
    '	Db.AddParameter("@IsAplActive", User.IsAPLActive, 2, SqlDbType.Bit, ParameterDirection.Input)
    '       Db.AddParameter("@IsAplSuperUser", User.IsAPLSuperUser, 2, SqlDbType.Bit, ParameterDirection.Input)
    '       Db.AddParameter("@IsOpenLinkSameWindow", User.IsOpenLinkSameWindow, 2, SqlDbType.Bit, ParameterDirection.Input)

    '	Dim result As String = Db.Execute(True)
    '	If Not String.IsNullOrEmpty(result) Then
    '		If InsertPermission Then
    '			For Each Permission As Permission In User.PermissionList
    '				If Not isAplProject Then
    '					Db.Init("AddUpdatePermission")
    '				Else
    '					Db.Init("APL_AddUpdatePermission")
    '				End If
    '				Db.AddParameter("@IsReviewer", Permission.IsReviewer, 4, SqlDbType.Bit, ParameterDirection.Input)
    '				Db.AddParameter("@IsIor", Permission.IsIOR, 4, SqlDbType.Bit, ParameterDirection.Input)
    '				Db.AddParameter("@ProjectID", Permission.ProjectID, 16, SqlDbType.Int, ParameterDirection.Input)
    '				Db.AddParameter("@UserId", result, 16, SqlDbType.Int, ParameterDirection.Input)
    '				Db.AddParameter("@IsActive", Permission.IsActive, 4, SqlDbType.Bit, ParameterDirection.Input)
    '				Db.AddParameter("@IsProjectManager", Permission.IsProjectManager, 4, SqlDbType.Bit, ParameterDirection.Input)
    '				Db.AddParameter("@IsSubContractor", Permission.IsSubContractor, 4, SqlDbType.Bit, ParameterDirection.Input)
    '				Db.AddParameter("@IsRegular", Permission.IsRegular, 4, SqlDbType.Bit, ParameterDirection.Input)
    '				Db.Execute()
    '			Next
    '		End If
    '		Db.Close()
    '	End If
    '	Db.Close()
    '	Return True
    '   End Function

    '   Public Function UpdateRecordsToReturn(ByVal userID As Integer, ByVal recordsToReturn As Integer) As Boolean
    '       Dim Db As DataBase = New DataBase
    '       Db.Init("UpdateRecordsToReturn")
    '       Db.AddParameter("@UserId", userID)
    '       Db.AddParameter("@RecordsToReturn", recordsToReturn)
    '       If Not Db.Execute() Then
    '           Db.Close()
    '           Return False
    '       End If
    '       Db.Close()
    '       Return True
    '   End Function

    '   Public Sub UpdateRenderingZoom(ByVal userId As Integer, ByVal zoomlevel As Decimal, ByVal displayLabels As Boolean)
    '       Dim Db As New NavCon.DB.DataBase
    '       Db.Init("UpdateRenderingZoom")
    '       Db.AddParameter("@UserId", userId)
    '       Db.AddParameter("@ZoomLevel", zoomlevel)
    '       Db.AddParameter("@DisplayLabels", displayLabels)
    '       Db.command.ExecuteNonQuery()
    '       Db.Close()
    '   End Sub

    '   Public Function GetRenderingZoom(ByVal userId As Integer, ByRef table As DataTable) As Boolean
    '       Dim Db As New NavCon.DB.DataBase
    '       Db.Init("GetRenderingZoom")
    '       Db.AddParameter("@UserId", userId)
    '       If Not Db.Execute(table) Then
    '           Db.Close()
    '           Return False
    '       End If
    '       Db.Close()
    '       Return True
    '   End Function
    Public Function UpdateUserProfile(ByVal objUser As User) As Integer
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("UpdateUserProfile")
        Db.AddParameter("@UserId", objUser.UserId, 50, SqlDbType.NVarChar, ParameterDirection.Input)
        Db.AddParameter("@FirstName", objUser.FirstName)
        Db.AddParameter("@LastName", objUser.LastName)
        Db.AddParameter("@EmailId", objUser.Email)
        Db.AddParameter("@CellPhone", objUser.Phone)
        Db.AddParameter("@OfficeStreet", objUser.OfficeStreet)
        Db.AddParameter("@OfficeState", objUser.OfficeState)
        Db.AddParameter("@OfficeCity", objUser.OfficeCity)
        Db.AddParameter("@OfficeZip", objUser.OfficeZip)
        Db.AddParameter("@OfficeCountry", objUser.OfficeCountry)
        Db.AddParameter("@CompanyName", objUser.CompanyName)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        Return returnValue
    End Function
    Public Function UpdateUserTerms(ByVal userID As Integer) As Integer
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("Update Users set IsTermsAccepted=1 Where Userid=@UserId", False)
        Db.AddParameter("@UserId", userID)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        Return returnValue
    End Function
End Class
