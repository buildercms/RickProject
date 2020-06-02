Imports RickProject.DB

Public Class PriorityProcessor
    Public Function GetPriorityChoicesByCustomer(ByVal customerID As Integer, ByRef table As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Db.Init("GetPriorityChoices")
        Db.AddParameter("@CustomerId", customerID)
        If Not Db.Execute(table) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetAllPriorityChoicesByCustomer(ByVal customerID As Integer, ByRef table As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Db.Init("GetAllPriorityChoices")
        Db.AddParameter("@CustomerID", customerID)
        If Not Db.Execute(table) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetPrioritySelectionByCustomer(ByVal customerID As Integer, ByRef dataSet As DataSet) As Boolean
        Dim Db As DataBase = New DataBase
        Db.Init("GetCustomerPrioritySelection")
        Db.AddParameter("@CustomerId", customerID)
        If Not Db.Execute(dataSet) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetCSFutureChoices(ByVal customerID As Integer, ByVal questionType As Integer, ByVal priorityID As Integer, ByVal VMType As Integer, ByRef table As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Db.Init("GetPriorityFSChoices")
        Db.AddParameter("@CustomerId", customerID)
        Db.AddParameter("@QuestionTypeId", questionType)
        Db.AddParameter("@PriorityId", priorityID)
        Db.AddParameter("@VmType", VMType)
        If Not Db.Execute(table) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetCSPreviousFutureChoices(ByVal customerID As Integer, ByVal questionType As Integer, ByVal priorityID As Integer, ByVal VMType As Integer, ByRef table As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Db.Init("GetPriorityPreviousFSChoices")
        Db.AddParameter("@CustomerId", customerID)
        Db.AddParameter("@QuestionTypeId", questionType)
        Db.AddParameter("@PriorityId", priorityID)
        Db.AddParameter("@VmType", VMType)
        If Not Db.Execute(table) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetCSFutureChoicesEdit(ByVal customerID As Integer, ByVal questionType As Integer, ByVal priorityID As Integer, ByVal VMType As Integer, ByRef table As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Db.Init("GetPriorityFSChoicesEdit")
        Db.AddParameter("@CustomerId", customerID)
        Db.AddParameter("@QuestionTypeId", questionType)
        Db.AddParameter("@PriorityId", priorityID)
        Db.AddParameter("@VmType", VMType)
        If Not Db.Execute(table) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetCSCurrentChoiceEdit(ByVal customerID As Integer, ByVal questionType As Integer, ByVal priorityID As Integer, ByVal VMType As Integer, ByRef table As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Db.Init("GetPriorityCSChoicesEdit")
        Db.AddParameter("@CustomerId", customerID)
        Db.AddParameter("@QuestionTypeId", questionType)
        Db.AddParameter("@PriorityId", priorityID)
        Db.AddParameter("@VmType", VMType)
        If Not Db.Execute(table) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetCSCurrentChoices(ByVal customerID As Integer, ByVal questionType As Integer, ByVal priorityID As Integer, ByVal VMType As Integer, ByRef table As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Db.Init("GetPriorityCSChoices")
        Db.AddParameter("@CustomerId", customerID)
        Db.AddParameter("@QuestionTypeId", questionType)
        Db.AddParameter("@PriorityId", priorityID)
        Db.AddParameter("@VmType", VMType)
        If Not Db.Execute(table) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetCSPreviousCurrentChoices(ByVal customerID As Integer, ByVal questionType As Integer, ByVal priorityID As Integer, ByVal VMType As Integer, ByRef table As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Db.Init("GetPriorityPreviousCSChoices")
        Db.AddParameter("@CustomerId", customerID)
        Db.AddParameter("@QuestionTypeId", questionType)
        Db.AddParameter("@PriorityId", priorityID)
        Db.AddParameter("@VmType", VMType)
        If Not Db.Execute(table) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function

    Public Function GetCustomerWWDCD(ByVal customerID As Integer, ByVal questionType As Integer, ByVal priorityID As Integer, ByVal VMType As Integer) As Integer
        Dim Db As DataBase = New DataBase
        Db.Init("GetCustomerWWDCDisplayGrid")
        Db.AddParameter("@CustomerId", customerID)
        Db.AddParameter("@QuestionTypeId", questionType)
        Db.AddParameter("@PriorityId", priorityID)
        Db.AddParameter("@VmType", VMType)
        Return Db.ExecuteAndReturn()
        'If Not Db.Execute(table) Then
        '    Db.Close()
        '    Return False
        'End If
        'Db.Close()
        'Return True
    End Function

    Public Function InsertCustomerPriorityChoices(ByVal CustomerID As Integer, ByVal PriorityId As Integer, ByVal Sort As Integer) As Integer
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("AddCustomerPriorityChoices")
        Db.AddParameter("@CustomerId", CustomerID)
        Db.AddParameter("@PriorityId", PriorityId)
        Db.AddParameter("@Sort", Sort)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        Return returnValue
    End Function
    Public Function DeleteCustomerPriorityChoices(ByVal CustomerID As Integer, ByVal priorityIDs As String) As Integer
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("DeleteCustomerPriorityChoices")
        Db.AddParameter("@CustomerId", CustomerID)
        Db.AddParameter("@PriorityIds", priorityIDs)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        Return returnValue
    End Function
    Public Function ResetCustomerPriorityChoices(ByVal CustomerID As Integer) As Integer
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("ResetCustomerPriorityChoices")
        Db.AddParameter("@CustomerId", CustomerID)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        Return returnValue
    End Function
    Public Function DeleteAllCustomerPriority(ByVal CustomerID As Integer) As Integer
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("DeleteAllCustomerPriority")
        Db.AddParameter("@CustomerId", CustomerID)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        Return returnValue
    End Function
    Public Function UpdateCustomerPriorityRank(ByVal CustomerID As Integer, ByVal PriorityId As Integer, ByVal Rank As Integer) As Integer
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("UpdateCustomerPriorityRanking")
        Db.AddParameter("@CustomerId", CustomerID)
        Db.AddParameter("@PriorityId", PriorityId)
        Db.AddParameter("@Ranking", Rank)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        Return returnValue
    End Function
    Public Function InsertCustomerPriorityChoiceDetails(ByVal CustomerID As Integer, ByVal PriorityId As Integer, ByVal QuestionTypeID As Integer, ByVal Answer As String, Optional ByVal WWDC As Integer = -1) As Integer

        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("AddCustomerPriorityChoiceDetails")
        Db.AddParameter("@CustomerId", CustomerID)
        Db.AddParameter("@PriorityId", PriorityId)
        Db.AddParameter("@QuestionTypeID", QuestionTypeID)
        Db.AddParameter("@Answer", Answer)
        Db.AddParameter("@WWDC", WWDC)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        Return returnValue
    End Function
    Public Function DeleteCustomerPriorityChoiceDetails(ByVal CustomerID As Integer, ByVal PriorityId As Integer, ByVal QuestionTypeID As Integer, ByVal Answer As String) As Integer
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("DeleteCustomerPriorityChoiceDetails")
        Db.AddParameter("@CustomerId", CustomerID)
        Db.AddParameter("@PriorityId", PriorityId)
        Db.AddParameter("@QuestionTypeID", QuestionTypeID)
        Db.AddParameter("@Answers", Answer)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        Return returnValue
    End Function

    Public Function GetValueMapOveriew(ByVal customerID As Integer, ByRef dataSet As DataSet) As Boolean
        Dim Db As DataBase = New DataBase
        Db.Init("GetValueMapOveriew")
        Db.AddParameter("@CustomerId", customerID)
        If Not Db.Execute(dataSet) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetValueMapScore(ByVal customerID As Integer, ByVal questionTypeID As Integer, ByVal PriorityID As Integer, ByRef datatable As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Db.Init("GetValueMapScore")
        Db.AddParameter("@CustomerId", customerID)
        Db.AddParameter("@QuestionTypeID", questionTypeID)
        Db.AddParameter("@PriorityID", PriorityID)
        If Not Db.Execute(datatable) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetOverAllValueMapScore(ByVal customerID As Integer, ByRef datatable As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Db.Init("GetOverallValueMapScore")
        Db.AddParameter("@CustomerId", customerID)
        If Not Db.Execute(datatable) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetCustomerPriortityChoices(ByVal customerID As Integer, ByVal questionTypeID As Integer, ByVal PriorityID As Integer, ByRef datatable As DataTable) As Boolean
        Dim Db As DataBase = New DataBase
        Db.Init("GetCustomerPriortityChoices")
        Db.AddParameter("@CustomerId", customerID)
        Db.AddParameter("@QuestionTypeID", questionTypeID)
        Db.AddParameter("@PriorityID", PriorityID)
        If Not Db.Execute(datatable) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function InsertValueMapScore(ByVal customerID As Integer, ByVal questionTypeID As Integer, ByVal PriorityID As Integer, ByVal score As Integer) As Integer
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("InsertValueMapScore")
        Db.AddParameter("@CustomerId", customerID)
        Db.AddParameter("@QuestionTypeID", questionTypeID)
        Db.AddParameter("@PriorityID", PriorityID)
        Db.AddParameter("@Score", score)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        Return returnValue
    End Function
    Public Function GetValueMapChoicesByQuestionId(ByVal customerID As Integer, ByVal questionType As Integer, ByVal priorityID As Integer, ByRef datatable As DataTable) As Integer
        Dim Db As DataBase = New DataBase
        Db.Init("GetValueMapChoicesByQuestionId")
        Db.AddParameter("@CustomerId", customerID)
        Db.AddParameter("@QuestionTypeId", questionType)
        Db.AddParameter("@PriorityId", priorityID)
        If Not Db.Execute(datatable) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function

    Public Function GetPropertyEvaluationStatus(ByVal customerID As Integer) As Integer
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("GetPropertyEvaluationStatus")
        Db.AddParameter("@CustomerId", customerID)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        Return returnValue
    End Function
    Public Function InsertValueMapLink(ByVal customerID As Integer, ByVal userID As Integer) As Integer
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("InsertVMLink")
        Db.AddParameter("@CustomerId", customerID)
        Db.AddParameter("@UserId", userID)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        Return returnValue
    End Function
    Public Function InsertValueMapEvaluationLink(ByVal customerID As Integer, ByVal userID As Integer) As Integer
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("InsertVMEvaluationLINK")
        Db.AddParameter("@CustomerId", customerID)
        Db.AddParameter("@UserId", userID)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        Return returnValue
    End Function
    Public Function InsertValueMapCompareDecideLink(ByVal customerID As Integer, ByVal userID As Integer) As Integer
        Dim Db As DataBase = New DataBase
        Dim returnValue As Integer = -1
        Db.Init("InsertVMCompareDecideLINK")
        Db.AddParameter("@CustomerId", customerID)
        Db.AddParameter("@UserId", userID)
        returnValue = Db.ExecuteAndReturn()
        Db.Close()
        Return returnValue
    End Function
    Public Function GetValueMapLink(ByVal customerID As Integer, ByVal userID As Integer, ByRef datatable As DataTable) As Integer
        Dim Db As DataBase = New DataBase
        Db.Init("GetVMLinkByCustomer")
        Db.AddParameter("@CustomerId", customerID)
        Db.AddParameter("@UserId", userID)
        If Not Db.Execute(datatable) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetValueMapEvaluationLink(ByVal customerID As Integer, ByVal userID As Integer, ByRef datatable As DataTable) As Integer
        Dim Db As DataBase = New DataBase
        Db.Init("GetVMEvaluationLinkByCustomer")
        Db.AddParameter("@CustomerId", customerID)
        Db.AddParameter("@UserId", userID)
        If Not Db.Execute(datatable) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetValueMapCompareDecideLink(ByVal customerID As Integer, ByVal userID As Integer, ByRef datatable As DataTable) As Integer
        Dim Db As DataBase = New DataBase
        Db.Init("GetVMCompareDecideLinkByCustomer")
        Db.AddParameter("@CustomerId", customerID)
        Db.AddParameter("@UserId", userID)
        If Not Db.Execute(datatable) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function CheckValueMapStatusByCustomer(ByVal customerID As Integer, ByRef datatable As DataTable) As Integer
        Dim Db As DataBase = New DataBase
        Db.Init("CheckValueMapStatusByCustomer")
        Db.AddParameter("@CustomerId", customerID)
        If Not Db.Execute(datatable) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetValueGapScore(ByVal customerID As Integer, ByRef datatable As DataTable) As Integer
        Dim Db As DataBase = New DataBase
        Db.Init("SELECT * FROM dbo.C_Points WHERE CustomerID=@CustomerId", False)
        Db.AddParameter("@CustomerId", customerID)
        If Not Db.Execute(datatable) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetAllPriorities(ByRef datatable As DataTable) As Integer
        Dim Db As DataBase = New DataBase
        Db.Init("GetAllPriorities", True)
        If Not Db.Execute(datatable) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetAllCommunities(ByRef datatable As DataTable) As Integer
        Dim Db As DataBase = New DataBase
        Db.Init("GetAllCommunities", True)
        If Not Db.Execute(datatable) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetAllCommunitiesByGlobalGroup(ByVal ggID As Integer, ByVal userID As Integer, ByRef datatable As DataTable) As Integer
        Dim Db As DataBase = New DataBase
        Db.Init("GetAllCommunitiesByGlobalGroup", True)
        Db.AddParameter("@GGID", ggID)
        Db.AddParameter("@userID", userID)
        If Not Db.Execute(datatable) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetPriorityQuestionDisplayByCustomer(ByVal customerID As Integer, ByRef datatable As DataTable) As Integer
        Dim Db As DataBase = New DataBase
        Db.Init("GetPriorityQuestionDisplayByCustomer", True)
        Db.AddParameter("@customerId", customerID)
        If Not Db.Execute(datatable) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
    Public Function GetPriorityQuestionDisplayByCustomerO(ByVal customerID As Integer, ByRef datatable As DataTable) As Integer
        Dim Db As DataBase = New DataBase
        Db.Init("GetPriorityQuestionDisplayByCustomerO", True)
        Db.AddParameter("@customerId", customerID)
        If Not Db.Execute(datatable) Then
            Db.Close()
            Return False
        End If
        Db.Close()
        Return True
    End Function
End Class
