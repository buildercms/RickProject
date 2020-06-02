Imports RickProject.Business
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports u = RIS.Common
Imports RickProject.DB


Public Class Reports
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("searchList") Is Nothing Then
        '    Response.Redirect("~/Home.aspx")
        'Else
        '    SearchCustomers(Session("searchList"))
        'End If
        If String.IsNullOrEmpty(Config.LoginUserType) Then
            Response.Redirect("~/Login.aspx")
        End If
        If Not Page.IsPostBack Then
            BindAllPriorities()
            BindAllWordSelectionPriorities()
            BindAllCommunities()
            txtStartDate.Text = DateTime.Now.AddDays(-7).ToString("MM/dd/yyyy")
            txtEndDate.Text = DateTime.Now.ToString("MM/dd/yyyy")
        End If
    End Sub
    Public Sub BindAllPriorities()
        Dim objPriorityProcessor As New PriorityProcessor()
        Dim dt As New DataTable
        objPriorityProcessor.GetAllPriorities(dt)
        ddlPriority.DataSource = dt
        ddlPriority.DataTextField = "PriorityName"
        ddlPriority.DataValueField = "PriorityName"
        ddlPriority.DataBind()
    End Sub
    Public Sub BindAllWordSelectionPriorities()
        Dim objPriorityProcessor As New PriorityProcessor()
        Dim dt As New DataTable
        objPriorityProcessor.GetAllPriorities(dt)
        ddlWordSelectionPriority.DataSource = dt
        ddlWordSelectionPriority.DataTextField = "PriorityName"
        ddlWordSelectionPriority.DataValueField = "PriorityName"
        ddlWordSelectionPriority.DataBind()
    End Sub
    Public Sub BindAllCommunities()
        Dim objPriorityProcessor As New PriorityProcessor()
        Dim dt As New DataTable
        'objPriorityProcessor.GetAllCommunities(dt)
        objPriorityProcessor.GetAllCommunitiesByGlobalGroup(Config.LoginUserType, Config.UserId, dt)
        lbCommunity.DataSource = dt
        lbCommunity.DataTextField = "CommName"
        lbCommunity.DataValueField = "CommID"
        lbCommunity.DataBind()
    End Sub

    Protected Sub btnGenerate_Click(sender As Object, e As EventArgs)
        Dim redirPath As String, outFile As String = "", friendlyFileName As String = ""
        Dim commIds As String = String.Empty
        Dim alAttachments As New ArrayList
        'Dim exportOpts As New ExportOptions
        'Dim pdfRtfWordOpts As New PdfRtfWordFormatOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim fileNameOut As String

        Dim crExportOptions As ExportOptions

        Dim crDoc As New ReportDocument
        'Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Tables
        'Dim CrTable As Table
        Dim prmFields As ParameterFieldDefinitions
        Dim prmField1 As ParameterFieldDefinition
        Dim prmValues As New ParameterValues

        Dim commNames As String = String.Empty
        For Each item As ListItem In lbCommunity.Items
            If item.Selected Then
                commIds &= item.Value.Trim() & ","
                commNames &= item.Text.Trim() & ","
            End If
        Next
        commIds = commIds.TrimEnd(",")
        If String.IsNullOrEmpty(commIds) Then
            For Each item As ListItem In lbCommunity.Items
                commIds &= item.Value.Trim() & ","
                commNames &= item.Text.Trim() & ","
            Next
            commIds = commIds.TrimEnd(",")
            commNames = commNames.TrimEnd(",")
        End If
        With crConnectionInfo
            .ServerName = System.Environment.MachineName.ToString
            .DatabaseName = "RISProd"
            .UserID = "RISDB"
            .Password = "RISAdmin#2018"
        End With

        Dim repName As String
        'If rblVisitReport.SelectedIndex = 0 Then repName = "VisitReport.rpt" Else repName = "VisitInternetReport.rpt"
        repName = "ImpactBenefitsCountPercent.rpt"
        Dim ReportPath As String = Server.MapPath("~/Reports/")
        Dim repFile As String = ReportPath & repName
        crDoc.Load(repFile, OpenReportMethod.OpenReportByTempCopy)
        CrTables = crDoc.Database.Tables

        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        fileNameOut = u.CleanFilename(("ImpactBenefitsCountPercent" & "-" & ddlPriority.SelectedItem.Text).Replace(" ", "")) & Guid.NewGuid().ToString() & ".pdf"
        friendlyFileName = u.CleanFilename("ImpactBenefitsCountPercent" & "-" & ddlPriority.SelectedItem.Text) & ".pdf"

        outFile = ReportPath & "/ReportsTemp/" & fileNameOut
        alAttachments.Add(outFile)
        crDiskFileDestinationOptions = New DiskFileDestinationOptions
        crDiskFileDestinationOptions.DiskFileName = outFile

        crExportOptions = crDoc.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        Dim prmMyParameterField As New CrystalDecisions.Shared.ParameterDiscreteValue
        prmFields = crDoc.DataDefinition.ParameterFields

        prmField1 = prmFields("@PriorityId")
        prmMyParameterField.Value = ddlPriority.SelectedValue
        prmValues.Add(prmMyParameterField)
        prmField1.ApplyCurrentValues(prmValues)

        prmField1 = prmFields("@StartDate")
        prmMyParameterField.Value = txtStartDate.Text.Trim()
        prmValues.Add(prmMyParameterField)
        prmField1.ApplyCurrentValues(prmValues)

        prmField1 = prmFields("@EndDate")
        prmMyParameterField.Value = txtEndDate.Text.Trim() & " 11:59:59 PM"
        prmValues.Add(prmMyParameterField)
        prmField1.ApplyCurrentValues(prmValues)

        prmField1 = prmFields("@Community")
        prmMyParameterField.Value = commIds
        prmValues.Add(prmMyParameterField)
        prmField1.ApplyCurrentValues(prmValues)

        prmField1 = prmFields("@CommunityNames")
        prmMyParameterField.Value = commNames
        prmValues.Add(prmMyParameterField)
        prmField1.ApplyCurrentValues(prmValues)
        Try
            crDoc.Export()
            crDoc.Close()
            crDoc.Dispose()
            GC.Collect()
        Catch ex As System.Exception
            'u.AppendLabel(lblError, "Error generating report file: " & ex.Message)
        End Try
        redirPath = ReportPath & fileNameOut
        u.DownloadFileToBrowser(outFile, friendlyFileName)

    End Sub

    Protected Sub lnkYearToDate_Click(sender As Object, e As EventArgs)
        txtEndDate.Text = Date.Today.ToShortDateString
        Dim startDate = CDate("1/1/" & Date.Today.ToString("yyyy"))
        txtStartDate.Text = startDate.ToShortDateString
    End Sub

    Protected Sub lnkMonthToDate_Click(sender As Object, e As EventArgs)
        txtEndDate.Text = Date.Today.ToShortDateString
        Dim startDate = CDate(Date.Today.ToString("MM") & "/1/" & Date.Today.ToString("yyyy"))
        txtStartDate.Text = startDate.ToShortDateString
    End Sub

    Protected Sub lnkWeekToDate_Click(sender As Object, e As EventArgs)
        Dim endDate = Date.Today.AddDays(6 - Date.Today.DayOfWeek)
        Dim startDate = endDate.AddDays(-6)

        txtStartDate.Text = startDate.ToShortDateString
        txtEndDate.Text = Today.ToShortDateString
    End Sub

    Protected Sub lnkLastWeek_Click(sender As Object, e As EventArgs)
        Dim dateToday As DateTime = Date.Today
        Dim day As Integer = -(dateToday.DayOfWeek + 1)        ' returns int of day of week with monday = 1

        'We want sunday to saturday

        Dim endDate = dateToday.AddDays(day)
        Dim startDate = endDate.AddDays(-6)

        txtStartDate.Text = startDate.ToShortDateString
        txtEndDate.Text = endDate.ToShortDateString
    End Sub

    Protected Sub lnkYesterday_Click(sender As Object, e As EventArgs)
        txtEndDate.Text = Date.Today.AddDays(-1).ToShortDateString
        txtStartDate.Text = txtEndDate.Text
    End Sub

    Protected Sub lnkPrioritySelection_ServerClick(sender As Object, e As EventArgs)
        Dim redirPath As String, outFile As String = "", friendlyFileName As String = ""
        Dim commIds As String = String.Empty
        Dim alAttachments As New ArrayList
        'Dim exportOpts As New ExportOptions
        'Dim pdfRtfWordOpts As New PdfRtfWordFormatOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim fileNameOut As String

        Dim crExportOptions As ExportOptions

        Dim crDoc As New ReportDocument
        'Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Tables
        'Dim CrTable As Table
        Dim prmFields As ParameterFieldDefinitions
        Dim prmField1 As ParameterFieldDefinition
        Dim prmValues As New ParameterValues

        Dim commNames As String = String.Empty
        For Each item As ListItem In lbCommunity.Items
            If item.Selected Then
                commIds &= item.Value.Trim() & ","
                commNames &= item.Text.Trim() & ","
            End If
        Next
        commIds = commIds.TrimEnd(",")
        commNames = commNames.TrimEnd(",")
        If String.IsNullOrEmpty(commIds) Then
            For Each item As ListItem In lbCommunity.Items
                commIds &= item.Value.Trim() & ","
                commNames &= item.Text.Trim() & ","
            Next
            commIds = commIds.TrimEnd(",")
            commNames = commNames.TrimEnd(",")
        End If
        With crConnectionInfo
            .ServerName = System.Environment.MachineName.ToString
            .DatabaseName = "RISProd"
            .UserID = "RISDB"
            .Password = "RISAdmin#2018"
        End With

        Dim repName As String
        'If rblVisitReport.SelectedIndex = 0 Then repName = "VisitReport.rpt" Else repName = "VisitInternetReport.rpt"
        repName = "PriorityCountPercent.rpt"
        Dim ReportPath As String = Server.MapPath("~/Reports/")
        Dim repFile As String = ReportPath & repName
        crDoc.Load(repFile, OpenReportMethod.OpenReportByTempCopy)
        CrTables = crDoc.Database.Tables

        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        fileNameOut = u.CleanFilename("PriorityCountPercent" & Guid.NewGuid().ToString() & ".pdf")
        friendlyFileName = u.CleanFilename("PriorityCountPercent" & ".pdf")

        outFile = ReportPath & "/ReportsTemp/" & fileNameOut
        alAttachments.Add(outFile)
        crDiskFileDestinationOptions = New DiskFileDestinationOptions
        crDiskFileDestinationOptions.DiskFileName = outFile

        crExportOptions = crDoc.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        Dim prmMyParameterField As New CrystalDecisions.Shared.ParameterDiscreteValue
        prmFields = crDoc.DataDefinition.ParameterFields

        'prmField1 = prmFields("@PriorityId")
        'prmMyParameterField.Value = ddlPriority.SelectedValue
        'prmValues.Add(prmMyParameterField)
        'prmField1.ApplyCurrentValues(prmValues)

        prmField1 = prmFields("@StartDate")
        prmMyParameterField.Value = txtStartDate.Text.Trim()
        prmValues.Add(prmMyParameterField)
        prmField1.ApplyCurrentValues(prmValues)

        prmField1 = prmFields("@EndDate")
        prmMyParameterField.Value = txtEndDate.Text.Trim() & " 11:59:59 PM"
        prmValues.Add(prmMyParameterField)
        prmField1.ApplyCurrentValues(prmValues)

        prmField1 = prmFields("@Community")
        prmMyParameterField.Value = commIds
        prmValues.Add(prmMyParameterField)
        prmField1.ApplyCurrentValues(prmValues)

        prmField1 = prmFields("@CommunityNames")
        prmMyParameterField.Value = commNames
        prmValues.Add(prmMyParameterField)
        prmField1.ApplyCurrentValues(prmValues)
        Try
            crDoc.Export()
            crDoc.Close()
            crDoc.Dispose()
            GC.Collect()
        Catch ex As System.Exception
            'u.AppendLabel(lblError, "Error generating report file: " & ex.Message)
        End Try
        redirPath = ReportPath & fileNameOut
        u.DownloadFileToBrowser(outFile, friendlyFileName)
    End Sub

    Protected Sub lnkWordSelection_ServerClick(sender As Object, e As EventArgs)
        Dim redirPath As String, outFile As String = "", friendlyFileName As String = ""
        Dim commIds As String = String.Empty
        Dim alAttachments As New ArrayList
        'Dim exportOpts As New ExportOptions
        'Dim pdfRtfWordOpts As New PdfRtfWordFormatOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim fileNameOut As String

        Dim crExportOptions As ExportOptions

        Dim crDoc As New ReportDocument
        'Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Tables
        'Dim CrTable As Table
        Dim prmFields As ParameterFieldDefinitions
        Dim prmField1 As ParameterFieldDefinition
        Dim prmValues As New ParameterValues

        Dim commNames As String = String.Empty
        For Each item As ListItem In lbCommunity.Items
            If item.Selected Then
                commIds &= item.Value.Trim() & ","
                commNames &= item.Text.Trim() & ","
            End If
        Next
        commIds = commIds.TrimEnd(",")
        commNames = commNames.TrimEnd(",")
        If String.IsNullOrEmpty(commIds) Then
            For Each item As ListItem In lbCommunity.Items
                commIds &= item.Value.Trim() & ","
                commNames &= item.Text.Trim() & ","
            Next
            commIds = commIds.TrimEnd(",")
            commNames = commNames.TrimEnd(",")
        End If
        With crConnectionInfo
            .ServerName = System.Environment.MachineName.ToString
            .DatabaseName = "RISProd"
            .UserID = "RISDB"
            .Password = "RISAdmin#2018"
        End With

        Dim repName As String
        'If rblVisitReport.SelectedIndex = 0 Then repName = "VisitReport.rpt" Else repName = "VisitInternetReport.rpt"
        repName = "ImpactBenefitsCountPercent.rpt"
        If chkCurrentFuture.Checked = True Then
            repName = "WWDCountPercent.rpt"
        End If
        Dim ReportPath As String = Server.MapPath("~/Reports/")
        Dim repFile As String = ReportPath & repName
        crDoc.Load(repFile, OpenReportMethod.OpenReportByTempCopy)
        CrTables = crDoc.Database.Tables

        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        fileNameOut = u.CleanFilename(("ImpactBenefitsCountPercent" & "-" & ddlPriority.SelectedItem.Text).Replace(" ", "")) & Guid.NewGuid().ToString() & ".pdf"
        friendlyFileName = u.CleanFilename("ImpactBenefitsCountPercent" & "-" & ddlPriority.SelectedItem.Text) & ".pdf"
        If chkCurrentFuture.Checked = True Then
            fileNameOut = u.CleanFilename(("WWDCountPercent" & "-" & ddlPriority.SelectedItem.Text).Replace(" ", "")) & Guid.NewGuid().ToString() & ".pdf"
            friendlyFileName = u.CleanFilename("WWDCountPercent" & "-" & ddlPriority.SelectedItem.Text) & ".pdf"
        End If

        outFile = ReportPath & "/ReportsTemp/" & fileNameOut
        alAttachments.Add(outFile)
        crDiskFileDestinationOptions = New DiskFileDestinationOptions
        crDiskFileDestinationOptions.DiskFileName = outFile

        crExportOptions = crDoc.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        Dim prmMyParameterField As New CrystalDecisions.Shared.ParameterDiscreteValue
        prmFields = crDoc.DataDefinition.ParameterFields

        prmField1 = prmFields("@PriorityId")
        prmMyParameterField.Value = ddlWordSelectionPriority.SelectedValue
        prmValues.Add(prmMyParameterField)
        prmField1.ApplyCurrentValues(prmValues)

        prmField1 = prmFields("@StartDate")
        prmMyParameterField.Value = txtStartDate.Text.Trim()
        prmValues.Add(prmMyParameterField)
        prmField1.ApplyCurrentValues(prmValues)

        prmField1 = prmFields("@EndDate")
        prmMyParameterField.Value = txtEndDate.Text.Trim() & " 11:59:59 PM"
        prmValues.Add(prmMyParameterField)
        prmField1.ApplyCurrentValues(prmValues)

        prmField1 = prmFields("@Community")
        prmMyParameterField.Value = commIds
        prmValues.Add(prmMyParameterField)
        prmField1.ApplyCurrentValues(prmValues)

        prmField1 = prmFields("@CommunityNames")
        prmMyParameterField.Value = commNames
        prmValues.Add(prmMyParameterField)
        prmField1.ApplyCurrentValues(prmValues)
        Try
            crDoc.Export()
            crDoc.Close()
            crDoc.Dispose()
            GC.Collect()
        Catch ex As System.Exception
            'u.AppendLabel(lblError, "Error generating report file: " & ex.Message)
        End Try
        redirPath = ReportPath & fileNameOut
        u.DownloadFileToBrowser(outFile, friendlyFileName)
    End Sub

    Protected Sub lnkSalesPerformance_ServerClick(sender As Object, e As EventArgs)
        Dim redirPath As String, outFile As String = "", friendlyFileName As String = ""
        Dim commIds As String = String.Empty
        Dim alAttachments As New ArrayList
        'Dim exportOpts As New ExportOptions
        'Dim pdfRtfWordOpts As New PdfRtfWordFormatOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim fileNameOut As String

        Dim crExportOptions As ExportOptions

        Dim crDoc As New ReportDocument
        'Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Tables
        'Dim CrTable As Table
        Dim prmFields As ParameterFieldDefinitions
        Dim prmField1 As ParameterFieldDefinition
        Dim prmValues As New ParameterValues

        Dim commNames As String = String.Empty
        For Each item As ListItem In lbCommunity.Items
            If item.Selected Then
                commIds &= item.Value.Trim() & ","
                commNames &= item.Text.Trim() & ","
            End If
        Next
        commIds = commIds.TrimEnd(",")
        commNames = commNames.TrimEnd(",")
        If String.IsNullOrEmpty(commIds) Then
            For Each item As ListItem In lbCommunity.Items
                commIds &= item.Value.Trim() & ","
                commNames &= item.Text.Trim() & ","
            Next
            commIds = commIds.TrimEnd(",")
            commNames = commNames.TrimEnd(",")
        End If
        With crConnectionInfo
            .ServerName = System.Environment.MachineName.ToString
            .DatabaseName = "RISProd"
            .UserID = "RISDB"
            .Password = "RISAdmin#2018"
        End With

        Dim repName As String
        'If rblVisitReport.SelectedIndex = 0 Then repName = "VisitReport.rpt" Else repName = "VisitInternetReport.rpt"
        repName = "SalesPersonPerformanceReport.rpt"

        Dim ReportPath As String = Server.MapPath("~/Reports/")
        Dim ReportTempPath As String = Server.MapPath("~/Reports/ReportsTemp/")
        Dim repFile As String = ReportPath & repName
        crDoc.Load(repFile, OpenReportMethod.OpenReportByTempCopy)
        CrTables = crDoc.Database.Tables

        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        fileNameOut = u.CleanFilename(("SalesPersonPerformanceReport" & "-" & ddlPriority.SelectedItem.Text).Replace(" ", "")) & Guid.NewGuid().ToString() & ".pdf"
        friendlyFileName = u.CleanFilename("SalesPersonPerformanceReport" & "-" & ddlPriority.SelectedItem.Text) & ".pdf"


        outFile = ReportTempPath & fileNameOut
        alAttachments.Add(outFile)
        crDiskFileDestinationOptions = New DiskFileDestinationOptions
        crDiskFileDestinationOptions.DiskFileName = outFile

        crExportOptions = crDoc.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        Dim prmMyParameterField As New CrystalDecisions.Shared.ParameterDiscreteValue
        prmFields = crDoc.DataDefinition.ParameterFields



        prmField1 = prmFields("@StartDate")
        prmMyParameterField.Value = txtStartDate.Text.Trim()
        prmValues.Add(prmMyParameterField)
        prmField1.ApplyCurrentValues(prmValues)

        prmField1 = prmFields("@EndDate")
        prmMyParameterField.Value = txtEndDate.Text.Trim() & " 11:59:59 PM"
        prmValues.Add(prmMyParameterField)
        prmField1.ApplyCurrentValues(prmValues)

        prmField1 = prmFields("@CommunityID")
        prmMyParameterField.Value = commIds
        prmValues.Add(prmMyParameterField)
        prmField1.ApplyCurrentValues(prmValues)


        'Try
        crDoc.Export()
            crDoc.Close()
            crDoc.Dispose()
            GC.Collect()
        'Catch ex As System.Exception
        'u.AppendLabel(lblError, "Error generating report file: " & ex.Message)
        'End Try
        redirPath = ReportPath & fileNameOut
        u.DownloadFileToBrowser(outFile, friendlyFileName)
    End Sub

    Protected Sub lnkSales_ServerClick(sender As Object, e As EventArgs)
        Dim redirPath As String, outFile As String = "", friendlyFileName As String = ""
        Dim commIds As String = String.Empty
        Dim alAttachments As New ArrayList
        'Dim exportOpts As New ExportOptions
        'Dim pdfRtfWordOpts As New PdfRtfWordFormatOptions
        Dim crDiskFileDestinationOptions As DiskFileDestinationOptions

        Dim fileNameOut As String

        Dim crExportOptions As ExportOptions

        Dim crDoc As New ReportDocument
        'Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Tables
        'Dim CrTable As Table
        Dim prmFields As ParameterFieldDefinitions
        Dim prmField1 As ParameterFieldDefinition
        Dim prmValues As New ParameterValues
        Dim commNames As String = String.Empty
        For Each item As ListItem In lbCommunity.Items
            If item.Selected Then
                commIds &= item.Value.Trim() & ","
                commNames &= item.Text.Trim() & ","
            End If
        Next
        commIds = commIds.TrimEnd(",")
        commNames = commNames.TrimEnd(",")
        If String.IsNullOrEmpty(commIds) Then
            For Each item As ListItem In lbCommunity.Items
                commIds &= item.Value.Trim() & ","
                commNames &= item.Text.Trim() & ","
            Next
            commIds = commIds.TrimEnd(",")
            commNames = commNames.TrimEnd(",")
        End If
        With crConnectionInfo
            .ServerName = System.Environment.MachineName.ToString
            .DatabaseName = "RISProd"
            .UserID = "RISDB"
            .Password = "RISAdmin#2018"
        End With

        Dim repName As String
        'If rblVisitReport.SelectedIndex = 0 Then repName = "VisitReport.rpt" Else repName = "VisitInternetReport.rpt"
        repName = "ImpactBenefitsCountPercent.rpt"
        Dim ReportPath As String = Server.MapPath("~/Reports/")
        Dim repFile As String = ReportPath & repName
        crDoc.Load(repFile, OpenReportMethod.OpenReportByTempCopy)
        CrTables = crDoc.Database.Tables

        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        fileNameOut = u.CleanFilename(("ImpactBenefitsCountPercent" & "-" & ddlPriority.SelectedItem.Text).Replace(" ", "")) & Guid.NewGuid().ToString() & ".pdf"
        friendlyFileName = u.CleanFilename("ImpactBenefitsCountPercent" & "-" & ddlPriority.SelectedItem.Text) & ".pdf"

        outFile = ReportPath & "/ReportsTemp/" & fileNameOut
        alAttachments.Add(outFile)
        crDiskFileDestinationOptions = New DiskFileDestinationOptions
        crDiskFileDestinationOptions.DiskFileName = outFile

        crExportOptions = crDoc.ExportOptions
        With crExportOptions
            .DestinationOptions = crDiskFileDestinationOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.PortableDocFormat
        End With

        Dim prmMyParameterField As New CrystalDecisions.Shared.ParameterDiscreteValue
        prmFields = crDoc.DataDefinition.ParameterFields

        prmField1 = prmFields("@PriorityId")
        prmMyParameterField.Value = ddlPriority.SelectedValue
        prmValues.Add(prmMyParameterField)
        prmField1.ApplyCurrentValues(prmValues)

        prmField1 = prmFields("@StartDate")
        prmMyParameterField.Value = txtStartDate.Text.Trim()
        prmValues.Add(prmMyParameterField)
        prmField1.ApplyCurrentValues(prmValues)

        prmField1 = prmFields("@EndDate")
        prmMyParameterField.Value = txtEndDate.Text.Trim() & " 11:59:59 PM"
        prmValues.Add(prmMyParameterField)
        prmField1.ApplyCurrentValues(prmValues)

        prmField1 = prmFields("@Community")
        prmMyParameterField.Value = commIds
        prmValues.Add(prmMyParameterField)
        prmField1.ApplyCurrentValues(prmValues)

        prmField1 = prmFields("@CommunityNames")
        prmMyParameterField.Value = commNames
        prmValues.Add(prmMyParameterField)
        prmField1.ApplyCurrentValues(prmValues)
        Try
            crDoc.Export()
            crDoc.Close()
            crDoc.Dispose()
            GC.Collect()
        Catch ex As System.Exception
            'u.AppendLabel(lblError, "Error generating report file: " & ex.Message)
        End Try
        redirPath = ReportPath & fileNameOut
        u.DownloadFileToBrowser(outFile, friendlyFileName)
    End Sub

    Protected Sub btnResetDates_Click(sender As Object, e As EventArgs)
        txtStartDate.Text = DateTime.Now.AddDays(-7).ToString("MM/dd/yyyy")
        txtEndDate.Text = DateTime.Now.ToString("MM/dd/yyyy")
    End Sub
    Private Sub GetCategories(ByVal ggid As Integer, ByVal dropdown As DropDownList)
        Dim objMaster As New MasterProcessor()
        Dim dt As DataTable = New DataTable()
        objMaster.GetCategoryByGGID(ggid, dt)
        dropdown.Items.Clear()
        dropdown.DataSource = dt
        dropdown.DataTextField = "Type"
        dropdown.DataValueField = "VMCategoryId"
        dropdown.DataBind()
        'If dt.Rows.Count > 0 Then
        '    dropdown.Items.Insert(0, New ListItem("Please select", 0))
        'End If
        'If dropCategories.Items.Count > 0 Then
        '    AddPleaseSelect(dropCategories)
        'End If
    End Sub


End Class