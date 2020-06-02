Imports RickProject.DB

Public Class PropertyProcessor
    Public Function InsertCustomerPropery(ByVal propertyName As String, ByVal streetAddress As String, ByVal city As String, ByVal state As String, ByVal zip As String, ByVal country As String, ByVal price As Integer, ByVal bedBaths As String, ByVal sqft As String, ByVal propertyImage As String, ByVal custID As Integer, ByVal archieved As Boolean, ByVal userID As Integer) As Integer
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("InsertCustomerPropery")
        Db.AddParameter("@CustomerId", custID)
        Db.AddParameter("@PropertyName", propertyName)
        Db.AddParameter("@StreetAddress", streetAddress)
        Db.AddParameter("@City", city)
        Db.AddParameter("@State", state)
        Db.AddParameter("@Zip", zip)
        Db.AddParameter("@Country", country)
        Db.AddParameter("@Price", price)
        Db.AddParameter("@BedBaths", bedBaths)
        Db.AddParameter("@sqft", IIf(String.IsNullOrEmpty(sqft), DBNull.Value, sqft))
        Db.AddParameter("@PropertyImage", propertyImage)
        Db.AddParameter("@Active", archieved)
        Db.AddParameter("@UserID", userID)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        Return returnValue
    End Function
    Public Function UpdateCustomerPropery(ByVal propertyId As Integer, ByVal propertyName As String, ByVal streetAddress As String, ByVal city As String, ByVal state As String, ByVal zip As String, ByVal country As String, ByVal price As Integer, ByVal bedBaths As String, ByVal sqft As String, ByVal propertyImage As String, ByVal custID As Integer, ByVal archieved As Boolean) As Integer
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("UpdateCustomerPropery")
        Db.AddParameter("@CustomerId", custID)
        Db.AddParameter("@PropertyName", propertyName)
        Db.AddParameter("@StreetAddress", streetAddress)
        Db.AddParameter("@City", city)
        Db.AddParameter("@State", state)
        Db.AddParameter("@Zip", zip)
        Db.AddParameter("@Country", country)
        Db.AddParameter("@Price", price)
        Db.AddParameter("@BedBaths", bedBaths)
        Db.AddParameter("@sqft", IIf(String.IsNullOrEmpty(sqft), DBNull.Value, sqft))
        Db.AddParameter("@PropertyImage", propertyImage)
        Db.AddParameter("@PropertyID", propertyId)
        Db.AddParameter("@Active", archieved)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        Return returnValue
    End Function
    Public Function GetPropertyDetailsById(ByVal Id As Integer, ByVal custID As Integer, ByRef table As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("GetPropertyDetails")
        Db.AddParameter("@PropertyId", Id)
        Db.AddParameter("@CustomerId", custID)
        If Not Db.Execute(table) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetAllPropertyDetailsByCustomer(ByVal custId As Integer, ByVal includeArchieved As Boolean, ByRef table As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("GetAllPropertyDetailsByCustomer")
        Db.AddParameter("@CustomerId", custId)
        Db.AddParameter("@Active", includeArchieved)
        If Not Db.Execute(table) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetAllPropertyDetailsByCustomerUser(ByVal custId As Integer, ByVal UserID As Integer, ByRef table As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("GetAllPropertyDetailsByCustomerAndUser")
        Db.AddParameter("@CustomerId", custId)
        Db.AddParameter("@UserID", UserID)
        If Not Db.Execute(table) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetPropertyValueMapInfoByCustomer(ByVal customerID As Integer, ByRef dataSet As DataSet) As Boolean
        Dim Db As DataBase = New DataBase
        Db.Init("GetPropertyValueMapInfoByCustomer")
        Db.AddParameter("@CustomerId", customerID)
        If Not Db.Execute(dataSet) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetPropertyValueMapScore(ByVal customerID As Integer, ByVal propertyID As Integer, ByVal PriorityID As Integer, ByRef datatable As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Db.Init("GetPropertyValueMapScore")
        Db.AddParameter("@CustomerId", customerID)
        Db.AddParameter("@PropertyID", propertyID)
        Db.AddParameter("@PriorityID", PriorityID)
        If Not Db.Execute(datatable) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function InsertPropertyValueMapScore(ByVal customerID As Integer, ByVal propertyID As Integer, ByVal PriorityID As Integer, ByVal score As Integer, ByVal notes As String) As Integer
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("InsertPropertyValueMapScore")
        Db.AddParameter("@CustomerId", customerID)
        Db.AddParameter("@PropertyID", propertyID)
        Db.AddParameter("@PriorityID", PriorityID)
        Db.AddParameter("@Score", score)
        Db.AddParameter("@Notes", notes)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        Return returnValue
    End Function
    Public Function UpdatePropertySelection(ByVal propertyID As Integer, ByVal selected As Integer, ByVal customerID As Integer) As Integer
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("UpdatePropertySelection")
        Db.AddParameter("@CustomerId", customerID)
        Db.AddParameter("@PropertyID", propertyID)
        Db.AddParameter("@selected", selected)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        Return returnValue
    End Function
    Public Function UpdatePropertyRank(ByVal propertyIDs As String, ByVal customerID As Integer) As Integer
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("UpdatePropertyRank")
        Db.AddParameter("@CustomerID", customerID)
        Db.AddParameter("@PropertyIDs", propertyIDs)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        Return returnValue
    End Function
    Public Function GetPropertyValueMapScoreById(ByVal propertyID As Integer, ByVal customerID As Integer, ByRef dataset As DataSet) As Boolean
        Dim Db As DataBase = New DataBase
        Db.Init("GetPropertyValueMapScoreById")
        Db.AddParameter("@customerID", customerID)
        Db.AddParameter("@PropertyID", propertyID)
        If Not Db.Execute(dataset) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetAllPropertyValueMapScore(ByVal customerID As Integer) As Integer
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("GetAllPropertyValueMapScore")
        Db.AddParameter("@customerID", customerID)
        returnValue = Convert.ToInt32(Db.ExecuteScalarString())
        Db.Close()
        Return returnValue
    End Function
    Public Function GetAllPropertyDetailsByCustomerRank(ByVal customerID As Integer, ByVal includeArchieved As Boolean, ByRef datatable As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Db.Init("GetAllPropertyDetailsByCustomerRank")
        Db.AddParameter("@CustomerId", customerID)
        Db.AddParameter("@Active", includeArchieved)
        If Not Db.Execute(datatable) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function CheckPropertyNameByUser(ByVal userID As Integer, ByVal propertyName As String) As Integer
        Dim Db As DataBase = New DataBase
        Db.Init("CheckPropertyNameByUser")
        Db.AddParameter("@userID", userID)
        Db.AddParameter("@propertyName", propertyName)
        Dim result As String = Db.ExecuteScalarString()
        Db.Close()
        Return Convert.ToInt32(result)
    End Function
End Class
