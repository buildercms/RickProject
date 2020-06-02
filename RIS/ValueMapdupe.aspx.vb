Imports RickProject.Business
Public Class ValueMapdupe
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Config.LoginUserType) Then
            Response.Redirect("~/Login.aspx")
        End If
        If Not Page.IsPostBack Then
            Dim customerId As Integer = Request.QueryString("cid")

            hidCustId.Value = customerId
            GetPriorityQuestionDisplayByCustomer(hidCustId.Value)
            aOverView.HRef = "Overview.aspx?cid=" & Request.QueryString("cid")
            aCreateValueMap.HRef = "ValueMapdupe.aspx?cid=" & Request.QueryString("cid")
            aCreateProperty.HRef = "AddProperty.aspx?cid=" & Request.QueryString("cid")
            aCompareDecide.HRef = "CompareDecide.aspx?cid=" & Request.QueryString("cid")
            If LoadAllPropertyDetails(Request.QueryString("cid")).Rows.Count < 2 Then
                aCompareDecide.Disabled = True
                aCompareDecide.HRef = ""
                aCompareDecide.Style.Add("color", "lightgray")
                aCompareDecide.Style.Add("cursor", "text")
            End If
            Dim objCustomerProcessor As New CustomerProcessor()
            Dim objPriorityProcessor As New PriorityProcessor()
            Dim dt, dt1 As New DataTable()
            Dim ds As New DataSet()
            objCustomerProcessor.GetContactInfoByUser(Config.UserId, customerId, dt1)
            lblCustomerName.InnerText = dt1.Rows(0)("FirstName") & " " & dt1.Rows(0)("LastName")
            lblPrintCustomerName.Text = dt1.Rows(0)("FirstName") & " " & dt1.Rows(0)("LastName")
            Dim user As User = Session("CurrentLogin")
            lblPrintUserName.Text = user.FirstName & " " & user.LastName
            lblPrintUserPhoneNumber.Text = user.Phone
            GetPrioritySelection(customerId, ds)
            GetAllPriorityChoices(customerId, dt)
            Dim dtOverAllVMScore As New DataTable
            objPriorityProcessor.GetOverAllValueMapScore(customerId, dtOverAllVMScore)
            If dtOverAllVMScore.Rows.Count > 0 Then

                Dim dtValueGap As DataTable = GetValueGapScore(customerId)
                If dtValueGap.Rows.Count > 0 Then
                    lblValueGap.Text = Convert.ToString(dtValueGap.Rows(0)(0))
                    Dim plus As String = Convert.ToString(dtValueGap.Rows(0)(3))
                    Dim minus As String = Convert.ToString(dtValueGap.Rows(0)(2))
                    txtScore.Value = minus & "," & plus
                    Dim Score As Decimal = Convert.ToDecimal(dtValueGap.Rows(0)(0))
                    If Score <= 15.0 Then
                        lblValueGapProbability.Text = "Not Likey"
                    ElseIf Score > 15.0 And Score <= 16.0 Then
                        lblValueGapProbability.Text = "Possible"
                    ElseIf Score > 16.0 And Score <= 18.0 Then
                        lblValueGapProbability.Text = "Likely"
                    ElseIf Score > 18.0 Then
                        lblValueGapProbability.Text = "Very Likely"
                    End If
                Else
                    txtScore.Value = "0,0"
                    lblValueGapProbability.Text = ""
                End If
            Else
                txtScore.Value = "0,0"
                lblValueGapProbability.Text = ""
            End If
            If ds.Tables(0).Rows.Count = 0 Then
                divPriorityOverView.Style.Add("display", "none")
                divEditPriority.Style.Add("display", "")
                hidCustomerPriority.Value = "new"
                'create_pdf.Style.Add("display", "none")
                divValueGapTitle.Style.Add("display", "none")
                divValueGapContent.Style.Add("display", "none")
                lblValueMapTitle.Style.Add("display", "none")
                btnVmReview.Style.Add("display", "none")
            Else
                'create_pdf.Style.Add("display", "none")
                divValueGapTitle.Style.Add("display", "")
                divValueGapContent.Style.Add("display", "none")
                lblValueMapTitle.Style.Add("display", "")
                divPriorityOverView.Style.Add("display", "")
                btnVmReview.Style.Add("display", "")
                divEditPriority.Style.Add("display", "none")
                hidCustomerPriority.Value = "old"
                Dim PriorityTable As DataTable = ds.Tables(0)
                Dim PriorityChoicesTable As DataTable = ds.Tables(1)
                lblPriority1Content.InnerText = PriorityTable.Rows(0)("PriorityName")
                lblPriority1Content.Attributes.Add("priorityId", PriorityTable.Rows(0)("PriorityID"))
                lblPriority2Content.InnerText = PriorityTable.Rows(1)("PriorityName")
                lblPriority2Content.Attributes.Add("priorityId", PriorityTable.Rows(1)("PriorityID"))
                lblPriority3Content.InnerText = PriorityTable.Rows(2)("PriorityName")
                lblPriority3Content.Attributes.Add("priorityId", PriorityTable.Rows(2)("PriorityID"))
                For i As Integer = 0 To PriorityTable.Rows.Count - 1
                    Dim priorityId As Integer = PriorityTable.Rows(i)("PriorityID")
                    Dim priorityName As String = PriorityTable.Rows(i)("PriorityName")
                    Dim impactresult() As DataRow = PriorityChoicesTable.Select("PriorityID =" & priorityId & " And QuestionTypeID = 1")

                    Dim dtImpactScore As New DataTable()
                    objPriorityProcessor.GetValueMapScore(hidCustId.Value, 1, priorityId, dtImpactScore)
                    Dim impactScore As Integer = 0
                    Dim impactColor As Integer = 0
                    If dtImpactScore.Rows.Count > 0 Then
                        impactScore = dtImpactScore.Rows(0)("Score")
                    End If
                    Dim impactContent As New StringBuilder()
                    Dim p As Integer = 1
                    For Each row In impactresult
                        impactContent.Append("<p style='display:inline-flex'>" & p & ". " & "<font style='margin-left:5px'>" & row(3) & "</font></p>")
                        impactColor = row(4)
                        p += 1
                    Next
                    If (i = 0) Then
                        divPriority1Impacts.InnerHtml = impactContent.ToString()
                        divPriority1Impacts.Attributes.Add("priorityid", priorityId)
                        divPriority1Impacts.Attributes.Add("priorityname", priorityName)
                        If impactScore > 0 Then
                            lblImpactsScore1.InnerText = impactScore
                            lblImpactsScore1.Style.Add("display", "")
                            If impactColor = 1 Then
                                lblImpactsScore1.Attributes.Add("class", "leftcircle skyblue")
                            Else
                                lblImpactsScore1.Attributes.Add("class", "leftcircle red")
                            End If
                        End If
                    ElseIf i = 1 Then
                        divPriority2Impacts.InnerHtml = impactContent.ToString()
                        divPriority2Impacts.Attributes.Add("priorityid", priorityId)
                        divPriority2Impacts.Attributes.Add("priorityname", priorityName)
                        If impactScore > 0 Then
                            lblImpactsScore2.InnerText = impactScore
                            lblImpactsScore2.Style.Add("display", "")
                            If impactColor = 1 Then
                                lblImpactsScore2.Attributes.Add("class", "leftcircle skyblue")
                            Else
                                lblImpactsScore2.Attributes.Add("class", "leftcircle red")
                            End If
                        End If
                    ElseIf i = 2 Then
                        divPriority3Impacts.InnerHtml = impactContent.ToString()
                        divPriority3Impacts.Attributes.Add("priorityid", priorityId)
                        divPriority3Impacts.Attributes.Add("priorityname", priorityName)
                        If impactScore > 0 Then
                            lblImpactsScore3.InnerText = impactScore
                            lblImpactsScore3.Style.Add("display", "")
                            If impactColor = 1 Then
                                lblImpactsScore3.Attributes.Add("class", "leftcircle skyblue")
                            Else
                                lblImpactsScore3.Attributes.Add("class", "leftcircle red")
                            End If
                        End If
                    End If


                    Dim csresult() As DataRow = PriorityChoicesTable.Select("PriorityID =" & priorityId & " And QuestionTypeID = 2")
                    Dim csContent As New StringBuilder()
                    p = 1
                    For Each row In csresult
                        csContent.Append("<p style='display:inline-flex'>" & p & ". " & "<font style='margin-left:5px'>" & row(3) & "</font></p>")
                        p += 1
                    Next
                    If (i = 0) Then
                        divPriority1CSDesc.InnerHtml = csContent.ToString()
                        divPriority1CSDesc.Attributes.Add("priorityid", priorityId)
                        divPriority1CSDesc.Attributes.Add("priorityname", priorityName)
                    ElseIf i = 1 Then
                        divPriority2CSDesc.InnerHtml = csContent.ToString()
                        divPriority2CSDesc.Attributes.Add("priorityid", priorityId)
                        divPriority2CSDesc.Attributes.Add("priorityname", priorityName)
                    ElseIf i = 2 Then
                        divPriority3CSDesc.InnerHtml = csContent.ToString()
                        divPriority3CSDesc.Attributes.Add("priorityid", priorityId)
                        divPriority3CSDesc.Attributes.Add("priorityname", priorityName)
                    End If

                    Dim fsresult() As DataRow = PriorityChoicesTable.Select("PriorityID =" & priorityId & " And QuestionTypeID = 3")
                    Dim fsContent As New StringBuilder()
                    p = 1
                    For Each row In fsresult
                        fsContent.Append("<p style='display:inline-flex'>" & p & ". " & "<font style='margin-left:5px'>" & row(3) & "</font></p>")
                        p += 1
                    Next
                    If (i = 0) Then
                        divPriority1FSDesc.InnerHtml = fsContent.ToString()
                        divPriority1FSDesc.Attributes.Add("priorityid", priorityId)
                        divPriority1FSDesc.Attributes.Add("priorityname", priorityName)
                    ElseIf i = 1 Then
                        divPriority2FSDesc.InnerHtml = fsContent.ToString()
                        divPriority2FSDesc.Attributes.Add("priorityid", priorityId)
                        divPriority2FSDesc.Attributes.Add("priorityname", priorityName)
                    ElseIf i = 2 Then
                        divPriority3FSDesc.InnerHtml = fsContent.ToString()
                        divPriority3FSDesc.Attributes.Add("priorityid", priorityId)
                        divPriority3FSDesc.Attributes.Add("priorityname", priorityName)
                    End If

                    Dim benefitresult() As DataRow = PriorityChoicesTable.Select("PriorityID =" & priorityId & " And QuestionTypeID = 4")
                    Dim benefitContent As New StringBuilder()
                    p = 1
                    For Each row In benefitresult
                        benefitContent.Append("<p style='display:inline-flex'>" & p & ". " & "<font style='margin-left:5px'>" & row(3) & "</font></p>")
                        p += 1
                    Next
                    Dim dtBenefitScore As New DataTable()
                    objPriorityProcessor.GetValueMapScore(hidCustId.Value, 4, priorityId, dtBenefitScore)
                    Dim benefitScore As Integer = 0
                    If dtBenefitScore.Rows.Count > 0 Then
                        benefitScore = dtBenefitScore.Rows(0)("Score")
                    End If
                    If (i = 0) Then
                        divPriority1FSBenefits.InnerHtml = benefitContent.ToString()
                        divPriority1FSBenefits.Attributes.Add("priorityid", priorityId)
                        divPriority1FSBenefits.Attributes.Add("priorityname", priorityName)
                        If benefitScore > 0 Then
                            lblBenefitScore1.InnerText = benefitScore
                            lblBenefitScore1.Style.Add("display", "")
                        End If
                    ElseIf i = 1 Then
                        divPriority2FSBenefits.InnerHtml = benefitContent.ToString()
                        divPriority2FSBenefits.Attributes.Add("priorityid", priorityId)
                        divPriority2FSBenefits.Attributes.Add("priorityname", priorityName)
                        If benefitScore > 0 Then
                            lblBenefitScore2.InnerText = benefitScore
                            lblBenefitScore2.Style.Add("display", "")
                        End If

                    ElseIf i = 2 Then
                        divPriority3FSBenefits.InnerHtml = benefitContent.ToString()
                        divPriority3FSBenefits.Attributes.Add("priorityid", priorityId)
                        divPriority3FSBenefits.Attributes.Add("priorityname", priorityName)
                        If benefitScore > 0 Then
                            lblBenefitScore3.InnerText = benefitScore
                            lblBenefitScore3.Style.Add("display", "")
                        End If
                    End If
                Next

            End If


            'GetCSFuture(customerId, 3, 19, 1, dt)
            'GetCSFutureChoice(customerId, 3, 19, 1, dt)
            'GetCSCurrentChoice(customerId, 3, 19, 1, dt)
            'Dim ShowCurrentOrFuture As Integer = GetCustomerWWDCD(customerId, 1, 19, 1)
            'If ShowCurrentOrFuture > 0 Then
            '    GetFSImpactChoice(customerId, 1, 19, 1, dt)
            'ElseIf ShowCurrentOrFuture < 0 Then
            '    GetCSImpactChoice(customerId, 1, 19, 1, dt)
            'ElseIf ShowCurrentOrFuture = 0 Then
            '    GetCSImpactChoice(customerId, 1, 19, 1, dt)
            '    GetCSBenefitChoice(customerId, 4, 19, 1, dt)
            'End If
            LoadCustomer(Request.QueryString("cid"), Config.UserId)
        End If
        BindPropertyEvaluationStatus(hidCustId.Value)
    End Sub
    Public Sub GetPriorityChoices(ByVal customerID As Integer, ByVal dt As DataTable)
        Dim objPriorityProcessor As New PriorityProcessor()
        objPriorityProcessor.GetPriorityChoicesByCustomer(customerID, dt)
        divPriorities.InnerHtml = ""
        Dim priorityHtml As New StringBuilder()
        For i As Integer = 0 To dt.Rows.Count - 1
            priorityHtml.Append("<div class='col-md-6 btns'>").Append("<button type='button' rank='-1' class='btn-default' id='" & dt.Rows(i)("PriorityId") & "'>" & dt.Rows(i)("PriorityName") & "</button>").Append("</div>")
        Next
        divPriorities.InnerHtml = priorityHtml.ToString()
    End Sub
    Public Sub GetAllPriorityChoices(ByVal customerID As Integer, ByVal dt As DataTable)
        Dim objPriorityProcessor As New PriorityProcessor()
        objPriorityProcessor.GetAllPriorityChoicesByCustomer(customerID, dt)
        divPriorities.InnerHtml = ""
        Dim dataset As New DataSet()
        objPriorityProcessor.GetPrioritySelectionByCustomer(customerID, dataset)
        Dim priorityHtml As New StringBuilder()
        If dataset.Tables(0).Rows.Count = 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                priorityHtml.Append("<div class='col-md-6 btns'>").Append("<button type='button'  rank='-1' class='btn-default' id='" & dt.Rows(i)("PriorityId") & "'>" & dt.Rows(i)("PriorityName") & "</button>").Append("</div>")
            Next
        Else
            Dim list As New List(Of Integer)
            For j As Integer = 0 To dataset.Tables(0).Rows.Count - 1
                list.Add(dataset.Tables(0).Rows(j)("PriorityId"))
                hidSelectedPriorities.Value = hidSelectedPriorities.Value & dataset.Tables(0).Rows(j)("PriorityId") & "~"
            Next
            For i As Integer = 0 To dt.Rows.Count - 1
                list.FindIndex(Function(a) a = dt.Rows(i)("PriorityId"))
                If list.Contains(dt.Rows(i)("PriorityId")) Then
                    priorityHtml.Append("<div class='col-md-6 btns'>").Append("<button type='button'  rank='" & (list.FindIndex(Function(a) a = dt.Rows(i)("PriorityId")) + 1) & "' class='btn-default active' id='" & dt.Rows(i)("PriorityId") & "'>" & dt.Rows(i)("PriorityName") & "</button>").Append("</div>")
                Else
                    priorityHtml.Append("<div class='col-md-6 btns'>").Append("<button type='button'  rank='-1' class='btn-default' id='" & dt.Rows(i)("PriorityId") & "'>" & dt.Rows(i)("PriorityName") & "</button>").Append("</div>")
                End If

            Next
        End If

        divPriorities.InnerHtml = priorityHtml.ToString()
    End Sub

    Public Sub GetPrioritySelection(ByVal customerID As Integer, ByRef ds As DataSet)
        Dim objPriorityProcessor As New PriorityProcessor()
        objPriorityProcessor.GetPrioritySelectionByCustomer(customerID, ds)

    End Sub
    Public Sub GetCSFuture(ByVal customerID As Integer, ByVal questionType As Integer, ByVal priorityID As Integer, ByVal VMType As Integer, ByRef dt As DataTable)
        Dim objPriorityProcessor As New PriorityProcessor()
        objPriorityProcessor.GetCSFutureChoices(customerID, questionType, priorityID, VMType, dt)
        divFuture.InnerHtml = ""
        Dim priorityHtml As New StringBuilder()
        For i As Integer = 0 To dt.Rows.Count - 1
            priorityHtml.Append("<div class='col-md-3 btns'>").Append("<button type='button' class='btn-default' id='" & dt.Rows(i)("FS") & "'>" & dt.Rows(i)("FS") & "</button>").Append("</div>")
        Next
        divFuture.InnerHtml = priorityHtml.ToString()
    End Sub
    Public Shared Function GetCSFutureChoice(ByVal customerID As Integer, ByVal questionType As Integer, ByVal priorityID As Integer, ByVal VMType As Integer, ByRef dt As DataTable) As String
        Dim objPriorityProcessor As New PriorityProcessor()
        objPriorityProcessor.GetCSFutureChoices(customerID, questionType, priorityID, VMType, dt)

        Dim priorityHtml As New StringBuilder()
        'If dt.Rows.Count > 0 Then
        dt.Rows.Add("Other", questionType, priorityID)
        'End If
        For i As Integer = 0 To dt.Rows.Count - 1
            If i Mod 2 = 0 Then
                priorityHtml.Append("<div class='btns'>").Append("<button type='button' style='' class='btn-default' id='" & dt.Rows(i)("FS") & "'>" & dt.Rows(i)("FS") & "</button>")
            Else
                priorityHtml.Append("<button type='button' style='' class='btn-default' id='" & dt.Rows(i)("FS") & "'>" & dt.Rows(i)("FS") & "</button>").Append("</div>")
            End If

        Next
        Return priorityHtml.ToString()
    End Function
    Public Shared Function GetCSCurrentChoice(ByVal customerID As Integer, ByVal questionType As Integer, ByVal priorityID As Integer, ByVal VMType As Integer, ByRef dt As DataTable) As String
        Dim objPriorityProcessor As New PriorityProcessor()
        objPriorityProcessor.GetCSCurrentChoices(customerID, questionType, priorityID, VMType, dt)

        Dim priorityHtml As New StringBuilder()
        'If dt.Rows.Count > 0 Then
        dt.Rows.Add("Other", questionType, priorityID)
        'End If
        For i As Integer = 0 To dt.Rows.Count - 1
            If i Mod 2 = 0 Then
                priorityHtml.Append("<div class='btns'>").Append("<button type='button' class='btn-default' style='' id='" & dt.Rows(i)("CS") & "'>" & dt.Rows(i)("CS") & "</button>")
            Else
                priorityHtml.Append("<button type='button' class='btn-default' style='' id='" & dt.Rows(i)("CS") & "'>" & dt.Rows(i)("CS") & "</button>").Append("</div>")

            End If

        Next
        Return priorityHtml.ToString()
    End Function
    Public Shared Function GetCustomerWWDCD(ByVal customerID As Integer, ByVal questionType As Integer, ByVal priorityID As Integer, ByVal VMType As Integer) As Integer
        Dim objPriorityProcessor As New PriorityProcessor()
        Return objPriorityProcessor.GetCustomerWWDCD(customerID, questionType, priorityID, VMType)
    End Function
    Public Shared Function GetCSImpactChoice(ByVal customerID As Integer, ByVal questionType As Integer, ByVal priorityID As Integer, ByVal VMType As Integer, ByRef dt As DataTable) As String
        Dim objPriorityProcessor As New PriorityProcessor()
        objPriorityProcessor.GetCSCurrentChoices(customerID, questionType, priorityID, VMType, dt)

        Dim priorityHtml As New StringBuilder()
        For i As Integer = 0 To dt.Rows.Count - 1
            priorityHtml.Append("<div class='col-md-4 btns'>").Append("<button future='0' type='button' class='btn-default' id='" & dt.Rows(i)("CS") & "'>" & dt.Rows(i)("CS") & "</button>").Append("</div>")
        Next
        Return priorityHtml.ToString()
    End Function
    Public Shared Function GetFSImpactChoice(ByVal customerID As Integer, ByVal questionType As Integer, ByVal priorityID As Integer, ByVal VMType As Integer, ByRef dt As DataTable) As String
        Dim objPriorityProcessor As New PriorityProcessor()
        objPriorityProcessor.GetCSFutureChoices(customerID, questionType, priorityID, VMType, dt)

        Dim priorityHtml As New StringBuilder()
        For i As Integer = 0 To dt.Rows.Count - 1
            priorityHtml.Append("<div class='col-md-4 btns'>").Append("<button future='1' type='button' class='btn-default' id='" & dt.Rows(i)("FS") & "'>" & dt.Rows(i)("FS") & "</button>").Append("</div>")
        Next
        Return priorityHtml.ToString()
    End Function
    Public Shared Function GetCSBenefitChoice(ByVal customerID As Integer, ByVal questionType As Integer, ByVal priorityID As Integer, ByVal VMType As Integer, ByRef dt As DataTable) As String
        Dim objPriorityProcessor As New PriorityProcessor()
        objPriorityProcessor.GetCSFutureChoices(customerID, questionType, priorityID, VMType, dt)

        Dim priorityHtml As New StringBuilder()
        'If dt.Rows.Count > 0 Then
        dt.Rows.Add("Other", questionType, priorityID)
        'End If
        For i As Integer = 0 To dt.Rows.Count - 1
            priorityHtml.Append("<div class='col-md-4 btns'>").Append("<button type='button' class='btn-default' id='" & dt.Rows(i)("FS") & "'>" & dt.Rows(i)("FS") & "</button>").Append("</div>")
        Next
        Return priorityHtml.ToString()
    End Function

    <System.Web.Services.WebMethod()> Public Shared Function InsertCustomerPriorityChoices(ByVal CustId As String, ByVal Priority As String) As String
        ' Return ""
        Dim result As Integer = 0
        Dim PriorityList As String() = Priority.TrimEnd("!").Split("!")
        Dim objPriorityProcessor As New PriorityProcessor()
        Dim PriorityIds As String = String.Empty
        For i As Integer = 0 To PriorityList.Length - 1
            PriorityIds &= PriorityList(i).Split("~")(0) & ","
        Next
        objPriorityProcessor.DeleteCustomerPriorityChoices(CustId, PriorityIds.TrimEnd(",").Replace("!", ","))
        UserProcessor.InsertUserActivity("Value Map", "Started for", CustId, Config.UserId)
        For i As Integer = 0 To PriorityList.Length - 1
            Dim PriorityId As String = PriorityList(i).Split("~")(0)
            result = objPriorityProcessor.InsertCustomerPriorityChoices(CustId, PriorityId, i + 1)
        Next
        If result > 0 Then
            Return "success"
        Else
            Return "fail"
        End If
    End Function
    <System.Web.Services.WebMethod()> Public Shared Function UpdateCustomerPriorityChoiceRank(ByVal CustId As String, ByVal Priority As String) As String
        ' Return ""
        Dim result As Integer = 0
        Dim PriorityList As String() = Priority.TrimEnd("!").Split("!")
        Dim objPriorityProcessor As New PriorityProcessor()
        Dim initialPriorityId = PriorityList(0).Split("~")(0)
        For i As Integer = 0 To PriorityList.Length - 1
            Dim PriorityId As String = PriorityList(i).Split("~")(0)
            result = objPriorityProcessor.UpdateCustomerPriorityRank(CustId, PriorityId, (i + 1))
        Next
        Dim dt As New DataTable()
        objPriorityProcessor.GetCSFutureChoices(CustId, 3, initialPriorityId, 2, dt)

        Dim priorityHtml As New StringBuilder()
        For i As Integer = 0 To dt.Rows.Count - 1
            priorityHtml.Append("<div class='col-md-3 btns'>").Append("<button type='button' class='btn-default' id='" & dt.Rows(i)("FS") & "'>" & dt.Rows(i)("FS") & "</button>").Append("</div>")
        Next
        Dim rowCount As Integer = dt.Rows.Count
        'If rowCount > 0 Then
        'If rowCount Mod 4 = 0 Then
        '    priorityHtml.Append("<div class='col-md-3></div><div class='col-md-3></div><div class='col-md-3></div><div class='col-md-3 btns'>").Append("<button type='button' class='btn-default' id='btnOther'>" & "Other" & "</button>").Append("</div>")
        'Else
        priorityHtml.Append("<div class='col-md-3 btns'>").Append("<button type='button' class='btn-default' id='" & "btnOther" & "'>" & "Other" & "</button>").Append("</div>")
        'End If
        'End If
        Return priorityHtml.ToString()
        'If result > 0 Then
        '    Return "success"
        'Else
        '    Return "fail"
        'End If
    End Function
    <System.Web.Services.WebMethod()> Public Shared Function InsertCustomerPriorityChoiceDetails(ByVal CustId As String, ByVal PriorityId As String, ByVal QuestionTypeId As String, ByVal Answer As String, ByVal WWDC As Integer) As String
        ' Return ""
        Dim result As Integer = 0
        Dim AnswerList As String() = HttpUtility.UrlDecode(Answer).Replace("~", """").TrimEnd("!").Split("!")
        Dim objPriorityProcessor As New PriorityProcessor()
        objPriorityProcessor.DeleteCustomerPriorityChoiceDetails(CustId, PriorityId, QuestionTypeId, HttpUtility.UrlDecode(Answer).Replace("~", """").Replace("!", ",").TrimEnd(","))
        For i As Integer = 0 To AnswerList.Length - 1
            Dim AnswerData As String = AnswerList(i)
            result = objPriorityProcessor.InsertCustomerPriorityChoiceDetails(CustId, PriorityId, QuestionTypeId, AnswerData.Trim(), WWDC)
        Next
        If result > 0 Then
            Return "success"
        Else
            Return "fail"
        End If

    End Function
    <System.Web.Services.WebMethod()> Public Shared Function GetCustomerPriorityCurrent(ByVal CustId As String, ByVal PriorityId As String, ByVal QuestionTypeId As String) As String
        ' Return ""
        Dim result As String = ""
        Dim dt As New DataTable()
        Dim objPriorityProcessor As New PriorityProcessor()
        result = GetCSCurrentChoice(CustId, QuestionTypeId, PriorityId, 2, dt)
        result += "~" + GetCSFutureChoice(CustId, QuestionTypeId, PriorityId, 2, dt)
        Dim ShowCurrentOrFuture As Integer = GetCustomerWWDCD(CustId, QuestionTypeId, PriorityId, 2)
        result += "~" + Convert.ToString(ShowCurrentOrFuture)
        'If ShowCurrentOrFuture > 0 Then
        '    result = GetFSImpactChoice(CustId, QuestionTypeId, PriorityId, 1, dt)
        'ElseIf ShowCurrentOrFuture < 0 Then
        '    result = GetCSImpactChoice(CustId, QuestionTypeId, PriorityId, 1, dt)
        'ElseIf ShowCurrentOrFuture = 0 Then
        '    result = GetCSImpactChoice(CustId, QuestionTypeId, PriorityId, 1, dt)
        '    result += "~" + GetCSBenefitChoice(CustId, 4, PriorityId, 1, dt)
        'End If
        Return result
    End Function
    <System.Web.Services.WebMethod()> Public Shared Function GetCustomerPriorityImpact(ByVal CustId As String, ByVal PriorityId As String, ByVal QuestionTypeId As String) As String
        ' Return ""
        Dim result As String = ""
        Dim dt As New DataTable()
        Dim objPriorityProcessor As New PriorityProcessor()
        Dim ShowCurrentOrFuture As Integer = GetCustomerWWDCD(CustId, 2, PriorityId, 2)
        'result = GetCSImpactChoice(CustId, QuestionTypeId, PriorityId, 1, dt)
        If ShowCurrentOrFuture > 0 Then
            result = GetFSImpactChoice(CustId, QuestionTypeId, PriorityId, 1, dt)
        ElseIf ShowCurrentOrFuture < 0 Then
            result = GetCSImpactChoice(CustId, QuestionTypeId, PriorityId, 1, dt)
        ElseIf ShowCurrentOrFuture = 0 Then
            result = GetCSImpactChoice(CustId, QuestionTypeId, PriorityId, 1, dt)
            '    ' result += "~" + GetCSBenefitChoice(CustId, 4, PriorityId, 1, dt)
        End If
        result += "~" + Convert.ToString(ShowCurrentOrFuture)
        Return result
    End Function
    <System.Web.Services.WebMethod()> Public Shared Function GetCustomerPriorityBenefit(ByVal CustId As String, ByVal PriorityId As String, ByVal QuestionTypeId As String) As String
        ' Return ""
        Dim result As String = ""
        Dim dt As New DataTable()
        Dim objPriorityProcessor As New PriorityProcessor()
        result = GetCSBenefitChoice(CustId, QuestionTypeId, PriorityId, 1, dt)
        Return result
    End Function
    <System.Web.Services.WebMethod()> Public Shared Function GetCustomerFuture(ByVal CustId As String, ByVal PriorityId As String) As String
        ' Return ""
        Dim result As Integer = 0
        'Dim PriorityList As String() = Priority.TrimEnd("!").Split("!")
        Dim objPriorityProcessor As New PriorityProcessor()
        'Dim initialPriorityId = PriorityList(0).Split("~")(0)
        'For i As Integer = 0 To PriorityList.Length - 1
        '    Dim PriorityId As String = PriorityList(i).Split("~")(0)
        '    'result = objPriorityProcessor.UpdateCustomerPriorityRank(CustId, PriorityId, (i + 1))
        'Next
        Dim dt As New DataTable()
        objPriorityProcessor.GetCSFutureChoices(CustId, 3, PriorityId, 2, dt)

        Dim priorityHtml As New StringBuilder()
        'If dt.Rows.Count > 0 Then
        dt.Rows.Add("Other", "3", PriorityId)
        'End If
        For i As Integer = 0 To dt.Rows.Count - 1
            priorityHtml.Append("<div class='col-md-3 btns'>").Append("<button type='button' class='btn-default' id='" & dt.Rows(i)("FS") & "'>" & dt.Rows(i)("FS") & "</button>").Append("</div>")
        Next
        Return priorityHtml.ToString()
        'If result > 0 Then
        '    Return "success"
        'Else
        '    Return "fail"
        'End If
    End Function

    <System.Web.Services.WebMethod()> Public Shared Function GetCustomerFutureEdit(ByVal CustId As String, ByVal PriorityId As String) As String
        ' Return ""
        Dim result As Integer = 0
        'Dim PriorityList As String() = Priority.TrimEnd("!").Split("!")
        Dim objPriorityProcessor As New PriorityProcessor()
        'Dim initialPriorityId = PriorityList(0).Split("~")(0)
        'For i As Integer = 0 To PriorityList.Length - 1
        '    Dim PriorityId As String = PriorityList(i).Split("~")(0)
        '    'result = objPriorityProcessor.UpdateCustomerPriorityRank(CustId, PriorityId, (i + 1))
        'Next
        Dim dt As New DataTable()
        objPriorityProcessor.GetCSFutureChoicesEdit(CustId, 3, PriorityId, 2, dt)

        Dim priorityHtml As New StringBuilder()
        'If dt.Rows.Count > 0 Then
        dt.Rows.Add("Other", 3, PriorityId)
        'End If
        For i As Integer = 0 To dt.Rows.Count - 1
            priorityHtml.Append("<div class='col-md-3 btns'>").Append("<button type='button' class='btn-default' id='" & dt.Rows(i)("FS") & "'>" & dt.Rows(i)("FS") & "</button>").Append("</div>")
            'If i Mod 2 = 0 Then
            '    priorityHtml.Append("<div class='btns'>").Append("<button type='button' class='btn-default' id='" & dt.Rows(i)("FS") & "'>" & dt.Rows(i)("FS") & "</button>")
            'Else
            '    priorityHtml.Append("<button type='button' class='btn-default' id='" & dt.Rows(i)("FS") & "'>" & dt.Rows(i)("FS") & "</button>").Append("</div>")
            'End If
        Next
        Return priorityHtml.ToString()
        'If result > 0 Then
        '    Return "success"
        'Else
        '    Return "fail"
        'End If
    End Function
    <System.Web.Services.WebMethod()> Public Shared Function GetCustomerFutureImpactEdit(ByVal CustId As String, ByVal PriorityId As String) As String
        ' Return ""
        Dim result As Integer = 0
        'Dim PriorityList As String() = Priority.TrimEnd("!").Split("!")
        Dim objPriorityProcessor As New PriorityProcessor()
        'Dim initialPriorityId = PriorityList(0).Split("~")(0)
        'For i As Integer = 0 To PriorityList.Length - 1
        '    Dim PriorityId As String = PriorityList(i).Split("~")(0)
        '    'result = objPriorityProcessor.UpdateCustomerPriorityRank(CustId, PriorityId, (i + 1))
        'Next
        Dim dt As New DataTable()
        objPriorityProcessor.GetCSFutureChoices(CustId, 4, PriorityId, 1, dt)
        'If dt.Rows.Count > 0 Then
        dt.Rows.Add("Other", 4, PriorityId)
        'End If
        Dim priorityHtml As New StringBuilder()
        For i As Integer = 0 To dt.Rows.Count - 1
            priorityHtml.Append("<div class='col-md-4 btns'>").Append("<button type='button' class='btn-default' id='" & dt.Rows(i)("FS") & "'>" & dt.Rows(i)("FS") & "</button>").Append("</div>")
        Next
        Return priorityHtml.ToString()
        'If result > 0 Then
        '    Return "success"
        'Else
        '    Return "fail"
        'End If
    End Function
    <System.Web.Services.WebMethod()> Public Shared Function GetCustomerCSImpactEdit(ByVal CustId As String, ByVal PriorityId As String) As String
        ' Return ""
        'Dim result As Integer = 0
        ''Dim PriorityList As String() = Priority.TrimEnd("!").Split("!")
        'Dim objPriorityProcessor As New PriorityProcessor()
        ''Dim initialPriorityId = PriorityList(0).Split("~")(0)
        ''For i As Integer = 0 To PriorityList.Length - 1
        ''    Dim PriorityId As String = PriorityList(i).Split("~")(0)
        ''    'result = objPriorityProcessor.UpdateCustomerPriorityRank(CustId, PriorityId, (i + 1))
        ''Next
        'Dim dt As New DataTable()
        'objPriorityProcessor.GetCSCurrentChoiceEdit(CustId, 1, PriorityId, 1, dt)

        'Dim priorityHtml As New StringBuilder()
        'For i As Integer = 0 To dt.Rows.Count - 1
        '    priorityHtml.Append("<div class='col-md-3'>").Append("<button type='button' class='btn-default' id='" & dt.Rows(i)("CS") & "'>" & dt.Rows(i)("CS") & "</button>").Append("</div>")
        'Next
        'Return priorityHtml.ToString()
        ''If result > 0 Then
        ''    Return "success"
        ''Else
        ''    Return "fail"
        ''End If
        'Dim result As String = ""
        'Dim dt As New DataTable()
        'Dim objPriorityProcessor As New PriorityProcessor()
        'Dim ShowCurrentOrFuture As Integer = GetCustomerWWDCD(CustId, 1, PriorityId, 1)
        'If ShowCurrentOrFuture > 0 Then
        '    objPriorityProcessor.GetCSFutureChoicesEdit(CustId, 3, PriorityId, 1, dt)

        '    Dim priorityHtml As New StringBuilder()
        '    For i As Integer = 0 To dt.Rows.Count - 1
        '        priorityHtml.Append("<div class='col-md-4 btns'>").Append("<button type='button' class='btn-default' id='" & dt.Rows(i)("FS") & "'>" & dt.Rows(i)("FS") & "</button>").Append("</div>")
        '    Next
        '    Return priorityHtml.ToString()
        'ElseIf ShowCurrentOrFuture < 0 Then

        '    objPriorityProcessor.GetCSCurrentChoiceEdit(CustId, 1, PriorityId, 1, dt)
        '    Dim priorityHtml As New StringBuilder()
        '    For i As Integer = 0 To dt.Rows.Count - 1

        '        priorityHtml.Append("<div class='col-md-4 btns'>").Append("<button type='button' class='btn-default' id='" & dt.Rows(i)("CS") & "'>" & dt.Rows(i)("CS") & "</button></div>")

        '    Next
        '    Return priorityHtml.ToString()
        'ElseIf ShowCurrentOrFuture = 0 Then

        '    objPriorityProcessor.GetCSCurrentChoiceEdit(CustId, 1, PriorityId, 1, dt)
        '    Dim priorityHtml As New StringBuilder()
        '    For i As Integer = 0 To dt.Rows.Count - 1

        '        priorityHtml.Append("<div class='col-md-4 btns'>").Append("<button type='button' class='btn-default' id='" & dt.Rows(i)("CS") & "'>" & dt.Rows(i)("CS") & "</button></div>")

        '    Next
        '    Return priorityHtml.ToString()
        '    ' result += "~" + GetCSBenefitChoice(CustId, 4, PriorityId, 1, dt)
        'End If
        Dim result As String = ""
        Dim dt As New DataTable()
        Dim objPriorityProcessor As New PriorityProcessor()
        Dim ShowCurrentOrFuture As Integer = GetCustomerWWDCD(CustId, 2, PriorityId, 2)
        If ShowCurrentOrFuture > 0 Then
            result = GetFSImpactChoice(CustId, 1, PriorityId, 1, dt)
        ElseIf ShowCurrentOrFuture < 0 Then
            result = GetCSImpactChoice(CustId, 1, PriorityId, 1, dt)
        ElseIf ShowCurrentOrFuture = 0 Then
            result = GetCSImpactChoice(CustId, 1, PriorityId, 1, dt)
            '    ' result += "~" + GetCSBenefitChoice(CustId, 4, PriorityId, 1, dt)
        End If
        ' result = GetCSImpactChoice(CustId, 1, PriorityId, 1, dt)
        result += "~" + Convert.ToString(ShowCurrentOrFuture)
        Return result
    End Function
    <System.Web.Services.WebMethod()> Public Shared Function GetCustomerPriorityCurrentEdit(ByVal CustId As String, ByVal PriorityId As String) As String
        ' Return ""
        Dim result As String = ""
        Dim dt As New DataTable()
        Dim objPriorityProcessor As New PriorityProcessor()
        result = GetCSCurrentChoice(CustId, 2, PriorityId, 2, dt)
        result += "~" + GetCSFutureChoice(CustId, 2, PriorityId, 2, dt)
        Dim ShowCurrentOrFuture As Integer = GetCustomerWWDCD(CustId, 2, PriorityId, 2)
        result += "~" + Convert.ToString(ShowCurrentOrFuture)
        'If ShowCurrentOrFuture > 0 Then
        '    result = GetFSImpactChoice(CustId, QuestionTypeId, PriorityId, 1, dt)
        'ElseIf ShowCurrentOrFuture < 0 Then
        '    result = GetCSImpactChoice(CustId, QuestionTypeId, PriorityId, 1, dt)
        'ElseIf ShowCurrentOrFuture = 0 Then
        '    result = GetCSImpactChoice(CustId, QuestionTypeId, PriorityId, 1, dt)
        '    result += "~" + GetCSBenefitChoice(CustId, 4, PriorityId, 1, dt)
        'End If
        Return result
    End Function
    'Public Shared Function GetCSCurrentChoiceEdit(ByVal customerID As Integer, ByVal questionType As Integer, ByVal priorityID As Integer, ByVal VMType As Integer, ByRef dt As DataTable) As String
    '    Dim objPriorityProcessor As New PriorityProcessor()
    '    objPriorityProcessor.GetCSCurrentChoiceEdit(customerID, questionType, priorityID, VMType, dt)

    '    Dim priorityHtml As New StringBuilder()
    '    For i As Integer = 0 To dt.Rows.Count - 1
    '        If i Mod 2 = 0 Then
    '            priorityHtml.Append("<div class='col-md-6'>").Append("<button type='button' class='btn-default' style='width: 109% !important;padding:20px!important;' id='" & dt.Rows(i)("CS") & "'>" & dt.Rows(i)("CS") & "</button>").Append("</div>")
    '        Else
    '            priorityHtml.Append("<div class='col-md-6' style=margin-left:-13px;>").Append("<button type='button' class='btn-default' style='width: 108%!important; padding:20px!important;' id='" & dt.Rows(i)("CS") & "'>" & dt.Rows(i)("CS") & "</button>").Append("</div>")

    '        End If
    '    Next
    '    Return priorityHtml.ToString()
    'End Function
    Public Shared Function GetCSCurrentChoiceEdit(ByVal customerID As Integer, ByVal questionType As Integer, ByVal priorityID As Integer, ByVal VMType As Integer, ByRef dt As DataTable) As String
        Dim objPriorityProcessor As New PriorityProcessor()
        objPriorityProcessor.GetCSCurrentChoiceEdit(customerID, questionType, priorityID, VMType, dt)
        Dim priorityHtml As New StringBuilder()
        For i As Integer = 0 To dt.Rows.Count - 1
            If i Mod 2 = 0 Then
                priorityHtml.Append("<div class='btns'>").Append("<button type='button' class='btn-default' id='" & dt.Rows(i)("CS") & "'>" & dt.Rows(i)("CS") & "</button>")
            Else
                priorityHtml.Append("<button type='button' class='btn-default' id='" & dt.Rows(i)("CS") & "'>" & dt.Rows(i)("CS") & "</button>").Append("</div>")
            End If
        Next
        Return priorityHtml.ToString()
    End Function

    'Public Shared Function GetCSFutureChoiceEdit(ByVal customerID As Integer, ByVal questionType As Integer, ByVal priorityID As Integer, ByVal VMType As Integer, ByRef dt As DataTable) As String
    '    Dim objPriorityProcessor As New PriorityProcessor()
    '    objPriorityProcessor.GetCSFutureChoicesEdit(customerID, questionType, priorityID, VMType, dt)

    '    Dim priorityHtml As New StringBuilder()
    '    For i As Integer = 0 To dt.Rows.Count - 1
    '        If i Mod 2 = 0 Then
    '            priorityHtml.Append("<div class='col-md-6'>").Append("<button type='button' style='width:109% !important; padding:20px!important;' class='btn-default' id='" & dt.Rows(i)("FS") & "'>" & dt.Rows(i)("FS") & "</button>").Append("</div>")
    '        Else
    '            priorityHtml.Append("<div class='col-md-6' style='margin-left:-13px;'>").Append("<button type='button' style='width:108% !important; padding:20px!important;' class='btn-default' id='" & dt.Rows(i)("FS") & "'>" & dt.Rows(i)("FS") & "</button>").Append("</div>")
    '        End If

    '    Next
    '    Return priorityHtml.ToString()
    'End Function
    Public Shared Function GetCSFutureChoiceEdit(ByVal customerID As Integer, ByVal questionType As Integer, ByVal priorityID As Integer, ByVal VMType As Integer, ByRef dt As DataTable) As String
        Dim objPriorityProcessor As New PriorityProcessor()
        objPriorityProcessor.GetCSFutureChoicesEdit(customerID, questionType, priorityID, VMType, dt)

        Dim priorityHtml As New StringBuilder()
        For i As Integer = 0 To dt.Rows.Count - 1
            If i Mod 2 = 0 Then
                priorityHtml.Append("<div class='btns'>").Append("<button type='button' class='btn-default' id='" & dt.Rows(i)("FS") & "'>" & dt.Rows(i)("FS") & "</button>")
            Else
                priorityHtml.Append("<button type='button' class='btn-default' id='" & dt.Rows(i)("FS") & "'>" & dt.Rows(i)("FS") & "</button>").Append("</div>")
            End If

        Next
        Return priorityHtml.ToString()
    End Function


    <System.Web.Services.WebMethod()>
    Public Shared Function GetValueMapScore(ByVal customerID As Integer, ByVal questionTypeID As Integer, ByVal priorityID As Integer) As String
        'major changes in this function to reflect allowing canadian zips
        'check version 102 for the logic that has been deleted
        Dim objPriorityProcessor As New PriorityProcessor()
        Dim dt As New DataTable()
        objPriorityProcessor.GetValueMapScore(customerID, questionTypeID, priorityID, dt)
        If dt.Rows.Count = 0 Then
            Return ""
        Else
            'Return dt.Rows(0)("MatchScore") & "~" & HttpContext.Current.Server.UrlEncode(dt.Rows(0)("Note"))
            Return dt.Rows(0)("Score")
        End If
    End Function
    <System.Web.Services.WebMethod()>
    Public Shared Function InsertValueMapScore(ByVal customerID As Integer, ByVal questionTypeID As Integer, ByVal priorityID As Integer, ByVal score As Integer) As String
        'major changes in this function to reflect allowing canadian zips
        'check version 102 for the logic that has been deleted
        Dim objPriorityProcessor As New PriorityProcessor()
        Dim result As Integer = 0
        Dim dt As New DataTable()
        result = objPriorityProcessor.InsertValueMapScore(customerID, questionTypeID, priorityID, score)
        If result > 1 Then
            Return "success"
        Else
            Return "fail"
        End If


    End Function

    <System.Web.Services.WebMethod()>
    Public Shared Function DeleteAllCustomerPriority(ByVal customerID As Integer) As String
        'major changes in this function to reflect allowing canadian zips
        'check version 102 for the logic that has been deleted
        Dim objPriorityProcessor As New PriorityProcessor()
        Dim dt As New DataTable()
        objPriorityProcessor.DeleteAllCustomerPriority(customerID)
        Return "success"
    End Function

    <System.Web.Services.WebMethod()>
    Public Shared Function GetCustomerPriorityChoices(ByVal customerID As Integer, ByVal priorityID As Integer, ByVal questionTypeID As Integer) As String
        'major changes in this function to reflect allowing canadian zips
        'check version 102 for the logic that has been deleted
        Dim objPriorityProcessor As New PriorityProcessor()
        Dim dt As New DataTable()
        objPriorityProcessor.GetCustomerPriortityChoices(customerID, questionTypeID, priorityID, dt)
        Dim result As String = String.Empty
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                result = result & dt.Rows(i)("Answer") & "~"
            Next
            result = result.TrimEnd("~")
        End If
        Return result
    End Function

    <System.Web.Services.WebMethod()> Public Shared Function GetCustomerPriorityIdeal(ByVal customerID As Integer, ByVal priorityID As Integer, ByVal questionType As Integer) As String
        ' Return ""
        Dim dt As New DataTable()
        Dim objPriorityProcessor As New PriorityProcessor()

        objPriorityProcessor.GetCSFutureChoices(customerID, 3, priorityID, 2, dt)

        Dim priorityHtml As New StringBuilder()
        For i As Integer = 0 To dt.Rows.Count - 1
            priorityHtml.Append("<div class='col-md-3 btns'>").Append("<button type='button' class='btn-default' id='" & dt.Rows(i)("FS") & "'>" & dt.Rows(i)("FS") & "</button>").Append("</div>")
        Next
        'If dt.Rows.Count > 0 Then
        'If rowCount Mod 4 = 0 Then
        '    priorityHtml.Append("<div class='col-md-3></div><div class='col-md-3></div><div class='col-md-3></div><div class='col-md-3 btns'>").Append("<button type='button' class='btn-default' id='btnOther'>" & "Other" & "</button>").Append("</div>")
        'Else
        priorityHtml.Append("<div class='col-md-3 btns'>").Append("<button type='button' class='btn-default' id='" & "btnOther" & "'>" & "Other" & "</button>").Append("</div>")
        'End If
        'End If
        Return priorityHtml.ToString()
        'If result > 0 Then
        '    Return "success"
        'Else
        '    Return "fail"
        'End If
    End Function
    <System.Web.Services.WebMethod()> Public Shared Function GetValueMapChoicesByQuestionId(ByVal customerID As Integer, ByVal priorityID As Integer, ByVal questionType As Integer) As String
        ' Return ""
        Dim dt As New DataTable()
        Dim objPriorityProcessor As New PriorityProcessor()

        objPriorityProcessor.GetValueMapChoicesByQuestionId(customerID, questionType, priorityID, dt)
        Dim result As String = String.Empty

        For i As Integer = 0 To dt.Rows.Count - 1
            result &= dt.Rows(i)("Answer") & "!"
        Next
        Return result
        'If result > 0 Then
        '    Return "success"
        'Else
        '    Return "fail"
        'End If
    End Function
    Public Function LoadAllPropertyDetails(ByVal custId As Integer) As DataTable
        Dim objPropertyProcessor As New PropertyProcessor()
        Dim dtProperty As New DataTable
        objPropertyProcessor.GetAllPropertyDetailsByCustomer(custId, True, dtProperty)
        Return dtProperty
    End Function

    Public Sub BindPropertyEvaluationStatus(ByVal custId As Integer)
        Dim objPriorityProcessor As New PriorityProcessor()
        Dim count As Integer = objPriorityProcessor.GetPropertyEvaluationStatus(custId)
        If count > 0 Then
            btnVmReview.Style.Add("display", "none")

        End If
    End Sub
    Public Function GetValueGapScore(ByVal custId As Integer) As DataTable
        Dim dt As New DataTable
        Dim objPriorityProcessor As New PriorityProcessor()
        objPriorityProcessor.GetValueGapScore(custId, dt)
        Return dt
    End Function
    Public Sub LoadCustomer(ByVal customerID As Integer, ByVal userID As Integer)
        Dim objCustomerProcessor As New CustomerProcessor()
        Dim dtCustomer As New DataTable
        objCustomerProcessor.GetContactInfoByUser(userID, customerID, dtCustomer)

        Dim vmstatus As String = Convert.ToString(dtCustomer.Rows(0)("VMCustomerStatus"))
        If vmstatus.ToLower() = "completed" Then
            create_pdf.Style.Add("display", "")
        Else
            create_pdf.Style.Add("display", "")
        End If
    End Sub

    Public Sub GetPriorityQuestionDisplayByCustomer(ByVal customerID As Integer)
        Dim objPriorityProcessor As New PriorityProcessor()
        Dim dt As New DataTable
        objPriorityProcessor.GetPriorityQuestionDisplayByCustomer(customerID, dt)
        If dt.Rows.Count > 0 Then
            hidValueMapStatus.Value = "old"
            hidValueMapQuestionDisplay.Value = dt.Rows(0)("PriorityId") & "~" & dt.Rows(0)("QuestionTypeId") & "~" & dt.Rows(0)("Ranking")
        Else
            hidValueMapStatus.Value = "new"
        End If
    End Sub
End Class
