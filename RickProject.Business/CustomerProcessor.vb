Imports RickProject.DB

Public Class CustomerProcessor
    Public Function InsertCustomer(ByVal objCustomer As Customer) As Integer
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("InsertCustomerContact")
        Db.AddParameter("@UserID", objCustomer.UserID, 50, SqlDbType.NVarChar, ParameterDirection.Input)
        Db.AddParameter("@FirstName", objCustomer.FirstName)
        Db.AddParameter("@MiddleName", objCustomer.MiddleName)
        Db.AddParameter("@LastName", objCustomer.LastName)
        Db.AddParameter("@EmailId", objCustomer.EmailId)
        Db.AddParameter("@MobilePhone", objCustomer.MobilePhone)
        Db.AddParameter("@WorkPhone", objCustomer.WorkPhone)
        Db.AddParameter("@StreetAddress", objCustomer.StreetAddress)
        Db.AddParameter("@State", objCustomer.State)
        Db.AddParameter("@City", objCustomer.City)
        Db.AddParameter("@Fax", objCustomer.Fax)
        Db.AddParameter("@CountryCode", objCustomer.CountryCode)
        Db.AddParameter("@SecondaryFirstName", objCustomer.SecondaryFirstName)
        Db.AddParameter("@SecondaryLastName", objCustomer.SecondaryLastName)
        Db.AddParameter("@SecondaryMName", objCustomer.SecondaryMName)
        Db.AddParameter("@SecondaryWorkPhone", objCustomer.SecondaryWorkPhone)
        Db.AddParameter("@SecondaryCellPhone", objCustomer.SecondaryCellPhone)
        Db.AddParameter("@SecondaryEmailId", objCustomer.SecondaryEmailID)
        Db.AddParameter("@VMCategoryId", objCustomer.VMCategoryID)
        Db.AddParameter("@WhyChangeId", objCustomer.WhyChangeID)
        Db.AddParameter("@Notes", objCustomer.Notes)
        Db.AddParameter("@GGID", objCustomer.GGID)
        Db.AddParameter("@IsActive", objCustomer.IsActive)
        Db.AddParameter("@VMStatus", objCustomer.ValueMapStatus)
        Db.AddParameter("@Community", objCustomer.Community)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        Return returnValue
    End Function
    Public Function UpdateCustomer(ByVal objCustomer As Customer) As Integer
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("UpdateCustomerContact")
        Db.AddParameter("@UserID", objCustomer.UserID, 50, SqlDbType.NVarChar, ParameterDirection.Input)
        Db.AddParameter("@CustomerId", objCustomer.CustomerID, 50, SqlDbType.NVarChar, ParameterDirection.Input)
        Db.AddParameter("@FirstName", objCustomer.FirstName)
        Db.AddParameter("@MiddleName", objCustomer.MiddleName)
        Db.AddParameter("@LastName", objCustomer.LastName)
        Db.AddParameter("@EmailId", objCustomer.EmailId)
        Db.AddParameter("@MobilePhone", objCustomer.MobilePhone)
        Db.AddParameter("@WorkPhone", objCustomer.WorkPhone)
        Db.AddParameter("@StreetAddress", objCustomer.StreetAddress)
        Db.AddParameter("@State", objCustomer.State)
        Db.AddParameter("@City", objCustomer.City)
        Db.AddParameter("@Fax", objCustomer.Fax)
        Db.AddParameter("@CountryCode", objCustomer.CountryCode)
        Db.AddParameter("@SecondaryFirstName", objCustomer.SecondaryFirstName)
        Db.AddParameter("@SecondaryLastName", objCustomer.SecondaryLastName)
        Db.AddParameter("@SecondaryMName", objCustomer.SecondaryMName)
        Db.AddParameter("@SecondaryWorkPhone", objCustomer.SecondaryWorkPhone)
        Db.AddParameter("@SecondaryCellPhone", objCustomer.SecondaryCellPhone)
        Db.AddParameter("@SecondaryEmailId", objCustomer.SecondaryEmailID)
        Db.AddParameter("@VMCategoryId", objCustomer.VMCategoryID)
        Db.AddParameter("@WhyChangeId", objCustomer.WhyChangeID)
        Db.AddParameter("@Notes", objCustomer.Notes)
        Db.AddParameter("@GGID", objCustomer.GGID)
        Db.AddParameter("@IsActive", objCustomer.IsActive)
        Db.AddParameter("@VMStatus", objCustomer.ValueMapStatus)
        Db.AddParameter("@Community", objCustomer.Community)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        Return returnValue
    End Function
    Public Function GetContactsByUser(ByVal fname As String, ByVal lname As String, ByVal note As String, ByVal includeinactive As Boolean, ByVal VMCategoryId As Integer, ByVal totalRecords As Integer, ByVal VMStatus As String, ByVal agentUserID As Integer, ByVal userID As String, ByVal community As String, ByRef table As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Db.Init("SearchContacts")
        Db.AddParameter("@fname", fname)
        Db.AddParameter("@lname", lname)
        Db.AddParameter("@note", note)
        Db.AddParameter("@includeinactive", IIf(includeinactive = False, 0, 1))
        Db.AddParameter("@VMCategoryId", VMCategoryId)
        Db.AddParameter("@totalRecords", totalRecords)
        Db.AddParameter("@userid", userID)
        Db.AddParameter("@VMStatus", VMStatus)
        Db.AddParameter("@AgentUserId", agentUserID)
        Db.AddParameter("@Community", community)
        If Not Db.Execute(table) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetAllContactsByUser(ByVal userID As Integer, ByRef table As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Db.Init("GetAllContactsByUser")
        Db.AddParameter("@userid", userID)
        If Not Db.Execute(table) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetRecentContactsByUser(ByVal userID As Integer, ByRef table As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Db.Init("GetRecentlyViewedContactsByUser")
        Db.AddParameter("@userid", userID)
        If Not Db.Execute(table) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function

    Public Function GetContactInfoByUser(ByVal userID As Integer, ByVal customerID As Integer, ByRef table As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Db.Init("GetContactInfoByUser")
        Db.AddParameter("@userId", userID)
        Db.AddParameter("@customerId", customerID)
        If Not Db.Execute(table) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function InsertNotes(ByVal userID As Integer, ByVal customerID As Integer, ByVal notes As String) As Integer
        Dim returnValue As Integer = -1
        Dim Db As DataBase = New DataBase
        Db.Init("InsertNotes")
        Db.AddParameter("@userId", userID)
        Db.AddParameter("@customerId", customerID)
        Db.AddParameter("@Notes", notes)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        Return returnValue
    End Function
    Public Function GetNotesInfoByCustomer(ByVal userID As Integer, ByVal customerID As Integer, ByRef table As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Db.Init("GetNotes")
        Db.AddParameter("@userId", userID)
        Db.AddParameter("@customerId", customerID)
        If Not Db.Execute(table) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function

    Public Function UpdateVMCustomer(ByVal customerID As Integer) As Integer
        Dim returnValue As Integer = -1
        Dim Db As DataBase = New DataBase
        Db.Init("UpdateVMCustDate")
        Db.AddParameter("@customerId", customerID)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        Return returnValue
    End Function
End Class
