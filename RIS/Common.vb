Imports NavCon.Business
Imports System.Net.Mail
Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Web.Configuration

Public Class Common
    Public Enum UserType
        None
        SysAdmin
        SuperUser
        NewUser
        SubContractor
        Io
        IoLead
        IOR
    End Enum
    Public Enum IRStatusesStandard
        Open
        Ready
        Closed
        WaitingForResub
        Hold
    End Enum
    Public Enum CommTypes
        IRNote
        RoomStatusNote
        InspectionResultsNote
        CONote
        APLNote
        APLIssueNote
    End Enum
    Public Shared Function CheckPermission(ByVal FormName) As Boolean
        Return True
    End Function
    Public Shared Function isSysAdmin(ByVal UserName As String, ByVal Pwd As String) As Boolean
        If UserName.ToLower.Equals("sysadmin") And Pwd.ToLower.Equals("sys123") Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function CheckPrivilage(ByVal UserID As Integer, ByVal ProjectID As Integer, ByVal isApl As Boolean) As Boolean
        'Dim userProcessor As UserProcessor = New UserProcessor
        'Return userProcessor.CanEditUser(UserID, ProjectID, isApl)
    End Function

    Public Shared Sub AddDefaultItem(ByVal lc As ListControl, ByVal itemtext As String, Optional ByVal value As String = "0", Optional addStyle As Boolean = False)
        Dim defaultitem As New ListItem(itemtext, value)
        defaultitem.Text = itemtext
        defaultitem.Value = value
        If addStyle Then
            defaultitem.Attributes.CssStyle.Add("font-weight", "bold")
            defaultitem.Attributes.CssStyle.Add("background", "orange")
        End If
        lc.Items.Insert(0, defaultitem)

    End Sub
    Public Shared Sub DupeDDL(ByVal originalDDL As DropDownList, ByVal newDDL As DropDownList)
        newDDL.Items.Clear()

        For Each eli As ListItem In originalDDL.Items
            Dim x As New ListItem(eli.Text)
            x.Value = eli.Value
            If eli.Selected Then x.Selected = True
            newDDL.Items.Add(x)
        Next
    End Sub

    Public Shared Sub AddNewIRStatus(ByRef ddl As DropDownList)
        Dim defaultitem As ListItem = New ListItem
        defaultitem.Text = "Room Statuses"
        defaultitem.Value = "1"
        ddl.Items.Insert(0, defaultitem)

        defaultitem = New ListItem
        defaultitem.Text = "not submitted"
        defaultitem.Value = "2"
        ddl.Items.Insert(0, defaultitem)

        defaultitem = New ListItem
        defaultitem.Text = "submitted"
        defaultitem.Value = "3"
        ddl.Items.Insert(0, defaultitem)

        defaultitem = New ListItem
        defaultitem.Text = "not approved"
        defaultitem.Value = "4"
        ddl.Items.Insert(0, defaultitem)

        defaultitem = New ListItem
        defaultitem.Text = "approved"
        defaultitem.Value = "5"
        ddl.Items.Insert(0, defaultitem)

        defaultitem = New ListItem
        defaultitem.Text = "not applicable"
        defaultitem.Value = "5"
        ddl.Items.Insert(0, defaultitem)

    End Sub

    Public Shared Sub AddRoomDetailsStatus(ByVal ddl As DropDownList)
        Dim defaultitem As ListItem = New ListItem
        defaultitem.Text = "Not submitted"
        defaultitem.Value = "1"
        ddl.Items.Insert(0, defaultitem)

        defaultitem = New ListItem
        defaultitem.Text = "Submitted"
        defaultitem.Value = "2"
        ddl.Items.Insert(0, defaultitem)

        defaultitem = New ListItem
        defaultitem.Text = "Not approved"
        defaultitem.Value = "3"
        ddl.Items.Insert(0, defaultitem)

        defaultitem = New ListItem
        defaultitem.Text = "Approved"
        defaultitem.Value = "4"
        ddl.Items.Insert(0, defaultitem)

        defaultitem = New ListItem
        defaultitem.Text = "Not applicable"
        defaultitem.Value = "5"
        ddl.Items.Insert(0, defaultitem)

    End Sub

    Public Shared Function checkNull(ByVal value As Object) As String
        If IsDBNull(value) Then
            Return ""
        Else
            Return value.ToString()
        End If
    End Function
    Public Shared Function SendMail(ByVal toAddress As String,
                               ByVal ccAddress As String,
                               ByVal fromAddress As String,
                                ByVal subject As String,
                                ByVal body As String,
                                ByVal attachments As String,
                              Optional ByVal machineName As String = "",
                              Optional ByVal sender As String = "") As Boolean

        Try
            Dim msg As New System.Net.Mail.MailMessage()
            msg.IsBodyHtml = True


            'msg.From = New System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings("MailFrom"))
            msg.From = New MailAddress(fromAddress)
            msg.To.Add(toAddress)
            'msg.CC.Add(System.Configuration.ConfigurationManager.AppSettings("MailFrom"))
            If Not String.IsNullOrEmpty(ccAddress) Then
                msg.CC.Add(ccAddress)
            End If
            msg.Subject = subject
            msg.Body = body
            If Not String.IsNullOrEmpty(sender) Then
                msg.Sender = New MailAddress(sender)
            End If
            If Len(attachments) > 0 Then
                If File.Exists(attachments) Then msg.Attachments.Add(New System.Net.Mail.Attachment(attachments))
            End If

            Dim MailServ As New SmtpClient
            If String.IsNullOrEmpty(machineName) Then
                machineName = System.Web.HttpContext.Current.Server.MachineName
            End If
            MailServ.Host = machineName

            'If behalf Then
            'MailServ.
            'End If

            If Not (machineName = "CMSPROD") Then
                Dim emails As String
                emails = "admin@buildercms.com,claudia@buildercms.com"
                'emails = "srudrappa@buildercms.com"
                msg.Body &= "<br><br>Original Email List: To:" & toAddress & "<br><br> cc: " & ccAddress
                msg.Subject &= " - NAVCON TEST Generated Email From " & machineName
                msg.To.Clear()
                msg.To.Add(emails)
                msg.Bcc.Clear()
                msg.CC.Clear()
            End If
            MailServ.Send(msg)
            Return True

        Catch
            Return False
        End Try

    End Function
    Public Shared Function SelectDDLFromValue(ByVal drop As DropDownList, ByVal value As String, Optional AddItemIfItDoesntExist As Boolean = False) As Boolean
        If drop.Items.Count < 1 Then Return False
        drop.ClearSelection()
        Dim selected As Boolean
        For Each l As ListItem In drop.Items
            If l.Value.ToUpper = value.ToUpper Then
                l.Selected = True
                selected = True
                Exit For
            End If
        Next
        If Not selected And AddItemIfItDoesntExist Then
            Dim newItem As New ListItem(value, value)
            newItem.Selected = True
            drop.Items.Add(newItem)
        End If

        Return selected
    End Function
    Public Shared Function GetStandardCustomIRStatuses() As Hashtable

        Dim statusList As Hashtable

        If System.Web.HttpContext.Current.Session("IRStatusList") Is Nothing Then
            statusList = New Hashtable
            Dim db As New RickProject.DB.DataBase
            db.Init("IRStatusGet")
            db.command.Parameters.AddWithValue("@projectID", Config.ProjectID)
            Dim dr As SqlClient.SqlDataReader = db.command.ExecuteReader

            Try
                If dr.HasRows Then
                    While dr.Read
                        statusList(dr.GetString(0)) = dr.GetString(1)
                    End While
                Else
                    'major error if the project doesn't have statuses
                End If
            Catch ex As Exception
            Finally
                dr.Close()
                db.Close()
            End Try
            Return statusList
        Else
            Return DirectCast(System.Web.HttpContext.Current.Session("IRStatusList"), Hashtable)
        End If

    End Function
    Public Shared Function GetStandardCustomRoomStatuses() As Hashtable
        Dim statusList As Hashtable
        If System.Web.HttpContext.Current.Session("RoomStatusList") Is Nothing Then
            statusList = New Hashtable
            Dim db As New RickProject.DB.DataBase
            db.Init("RoomStatusGet")
            db.command.Parameters.AddWithValue("@projectID", Config.ProjectID)
            Dim dr As SqlClient.SqlDataReader = db.command.ExecuteReader

            Try
                If dr.HasRows Then
                    While dr.Read
                        Dim stdStatus As String = dr.GetString(0)
                        Dim projStatus As String = dr.GetString(1)
                        statusList(dr.GetString(0)) = dr.GetString(1)
                    End While
                Else
                    'major error if the project doesn't have statuses
                End If
            Catch ex As Exception
            Finally
                dr.Close()
            End Try
            Return statusList
        Else
            Return DirectCast(System.Web.HttpContext.Current.Session("IRStatusList"), Hashtable)
        End If
    End Function
    Public Shared Sub GetRoomStatusListControl(ByRef listCtrl As ListControl)
        Dim statusList As Hashtable = GetStandardCustomRoomStatuses()
        With listCtrl
            .Items.Clear()

            .Items.Add(New ListItem(statusList("Not Applicable"), "Not Applicable"))
            .Items.Add(New ListItem(statusList("Not Submitted"), "Not Submitted"))
            .Items.Add(New ListItem(statusList("Submitted"), "Submitted"))
            .Items.Add(New ListItem(statusList("Not Approved"), "Not Approved"))
            .Items.Add(New ListItem(statusList("Approved"), "Approved"))
        End With

    End Sub


    Public Shared Sub GetIRStatusListControl(ByRef listCtrl As ListControl)
        Dim statusList As Hashtable = GetStandardCustomIRStatuses()
        With listCtrl
            .Items.Clear()
            'statuses go in a clear order
            .Items.Add(New ListItem(statusList("Open"), "Open"))
            .Items.Add(New ListItem(statusList("Ready"), "Ready"))
            .Items.Add(New ListItem(statusList("Scheduled"), "Scheduled"))
            .Items.Add(New ListItem(statusList("Continuous"), "Continuous"))
            .Items.Add(New ListItem(statusList("Waiting Re-Sub."), "Waiting Re-Sub."))
            .Items.Add(New ListItem(statusList("Closed"), "Closed"))
        End With


    End Sub
    Public Shared Function DownloadFileToBrowser(ByVal filePath As String, ByVal userFileName As String, Optional ByVal contentType As String = "application/pdf", Optional ByVal popOpenSave As Integer = -1) As Boolean
        'popOpenSave param values
        ' -1 - nothing was passed in, get the user preferences
        ' 0  - make it inline
        ' 1  - make it an attachment (opening the open save dialog)

        'Content Type
        'Excel - application/vnd.ms-excel
        'Word - application/msword

        'Below is a list of the recommended content-types for Office 2007 files, which would allow it to be uniquely identified:
        '"application/vnd.openxmlformats-officedocument.wordprocessingml.document" (for .docx files)
        '"application/vnd.openxmlformats-officedocument.wordprocessingml.template" (for .dotx files)
        '"application/vnd.openxmlformats-officedocument.presentationml.presentation" (for .pptx files)
        '"application/vnd.openxmlformats-officedocument.presentationml.slideshow" (for .ppsx files)
        '"application/vnd.openxmlformats-officedocument.presentationml.template" (for .potx files)
        '"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" (for .xlsx files)
        '"application/vnd.openxmlformats-officedocument.spreadsheetml.template" (for .xltx files)

        'These content-types are also supported:

        '"vnd.ms-word.document.macroEnabled.12"
        '"vnd.ms-word.template.macroEnabled.12"
        '"vnd.ms-powerpoint.template.macroEnabled.12" 
        '"vnd.ms-powerpoint.addin.macroEnabled.12" 
        '"vnd.ms-powerpoint.slideshow.macroEnabled.12" 
        '"vnd.ms-powerpoint.presentation.macroEnabled.12" 
        '"vnd.ms-excel.addin.macroEnabled.12" 
        '"vnd.ms-excel.sheet.binary.macroEnabled.12" 
        '"vnd.ms-excel.sheet.macroEnabled.12" 
        '"vnd.ms-excel.template.macroEnabled.12"
        Dim r As System.Web.HttpResponse = System.Web.HttpContext.Current.Response
        Dim dispType As String

        If popOpenSave = -1 Then
            dispType = "attachment"
        Else
            If popOpenSave = 1 Then dispType = "attachment" Else dispType = "inline"
        End If


        r.Clear()
        r.ClearContent()
        r.ClearHeaders()

        r.AppendHeader("content-disposition", dispType & "; filename=" & """" & userFileName & """")
        r.ContentType = contentType

        r.TransmitFile(filePath)

        r.Flush()
        r.End()
        Return True
    End Function
    Public Shared Function GetNotes(ByVal NoteType As Integer, EntityID As Integer, Optional ByVal HTMLStyleBreaks As Boolean = False) As String

        Dim db As New RickProject.DB.DataBase
        db.Init("GetNotes")
        db.AddParameter("@userID", Config.UserId)
        db.AddParameter("@noteType", NoteType)
        db.AddParameter("@entityID", EntityID)

        'Dim pReturn As SqlParameter = cmdGetNotes.Parameters.Add("@RETURN", SqlDbType.Int)
        'pReturn.Direction = ParameterDirection.ReturnValue

        Dim dr As SqlDataReader
        Try
            dr = db.command.ExecuteReader
            If dr.HasRows Then
                While dr.Read
                    Dim importance As Integer = dr.GetInt32(5)
                    Dim visibility As Integer = dr.GetInt32(6)
                    Dim note As String = dr.GetString(4)
                    Dim userID As Integer = dr.GetInt32(7)

                    Dim initials As String
                    Dim firstName As String = dr.GetString(0)
                    Dim lastName As String = dr.GetString(1)
                    If Not dr.IsDBNull(2) Then
                        initials = dr.GetString(2).ToUpper()
                    Else
                        'Case no 4223 code modified by Canarys
                        initials = (firstName.Substring(0, 1) & lastName.Substring(0, 1)).ToUpper()
                    End If

                    If userID < 0 Then initials = "System"
                    If userID = -666 Then initials = "Customer"

                    Dim dateRec As DateTime = dr.GetDateTime(3)
                    Dim prefix As String
                    prefix = String.Format("{0} -({1}) ", dateRec.ToString("M/d/yy"), initials)
                    Dim contentPrefix As String = "", contentSuffix As String = ""
                    If importance = 1 Then prefix &= "! "
                    If visibility = 1 Then prefix &= " (private) "
                    If visibility = 2 Then prefix &= " (internal) "

                    If note.Contains("~") Then
                        Dim rooms As String() = note.Split(New Char() {"~"c})
                        note = ""
                        For Each word In rooms
                            'If Not String.IsNullOrEmpty(note) Then
                            '	note = note + prefix
                            'End If
                            note = note + word + vbCrLf
                        Next
                    End If

                    GetNotes &= prefix & note & vbCrLf

                End While
            End If

        Catch ex As Exception
        Finally
            If Not dr.IsClosed Then dr.Close()
            db.Close()
        End Try

        If HTMLStyleBreaks Then
            If Not String.IsNullOrEmpty(GetNotes) Then
                GetNotes = GetNotes.Replace(vbLf, "<br/>")
                GetNotes = GetNotes.Replace(vbCrLf, "<br/>")
            End If
        End If

        'If HTMLStyleBreaks Then
        Return GetNotes
    End Function
    Public Shared Function AddNote(NoteType As Integer, EntityID As Integer, NewNote As String, Optional UserID As Integer = Integer.MinValue) As Boolean
        If UserID = Integer.MinValue Then UserID = Config.UserId

        Dim db As New RickProject.DB.DataBase
        db.Init("NotesInsert")
        db.AddParameter("@userID", Config.UserId)
        db.AddParameter("@noteType", NoteType)
        db.AddParameter("@entityID", EntityID)
        db.AddParameter("@note", NewNote)

        If Not db.Execute Then
            db.Close()
            Return False
        End If
        db.Close()
        Return True
    End Function
    Public Shared Function UniqueString() As String
        Dim rand As New Random()
        Dim rand5Digits As Integer = rand.Next(99999)
        'Dim strDate As String = Date.Now.ToString
        'strDate = strDate.Replace(" ", "")
        'strDate = strDate.Replace("/", "")
        'strDate = strDate.Replace(":", "")
        Return Config.UserId & "-" & rand5Digits
    End Function
    Public Shared Function MakeFileNameUniqueIfExists(ByVal path As String) As String
        Dim re As New Regex("\.[^.]*$")
        Dim ext As String = re.Match(path).ToString

        Dim EmergencyBreak As Integer = 1000
        Dim count As Integer = 0
        While (System.IO.File.Exists(path))
            path = Regex.Replace(path, ext & "$", "2" & ext)
            If count > EmergencyBreak Then Exit While 'So we don't get infinite loop ever
            count += 1
        End While

        Return path
    End Function
    Public Shared Function RecordAuditLog(userID As Integer, tableName As String, actionDescription As String, keyID As Integer)



        Dim db As New RickProject.DB.DataBase
        db.Init("AuditLogInsert")
        db.AddParameter("@userID", userID)
        db.AddParameter("@tableName", tableName)
        db.AddParameter("@actionDescription", actionDescription)
        db.AddParameter("@keyID", keyID)

        If Not db.Execute Then
            db.Close()
            Return False
        End If
        db.Close()
        Return True
    End Function
    Public Shared Function GetCSVValuesFromCollection(ByVal liCol As ListItemCollection, Optional ByVal selectedOnly As Boolean = False) As String
        Dim returnStr As String = ""
        For Each li As ListItem In liCol
            If selectedOnly Then
                If li.Selected Then returnStr &= li.Value & ","
            Else
                returnStr &= li.Value & ","
            End If
        Next li
        returnStr = returnStr.TrimEnd(",")
        Return returnStr
    End Function
    Public Shared Function GetDateFrom2TextFields(txtDate As TextBox, txtTime As TextBox, Optional SetToNowIfNotValid As Boolean = False) As DateTime

        If IsDate(txtDate.Text) Then
            If txtTime.Text.Length > 0 AndAlso IsDate(txtDate.Text & " " & txtTime.Text) Then
                GetDateFrom2TextFields = Date.Parse(txtDate.Text & " " & txtTime.Text)
            Else
                GetDateFrom2TextFields = Date.Parse(txtDate.Text)
            End If
        Else
            If SetToNowIfNotValid Then
                GetDateFrom2TextFields = Now
            Else
                GetDateFrom2TextFields = #1/1/1800#
            End If
        End If
        Return GetDateFrom2TextFields
    End Function
    Public Shared Sub SetStatusMessage(lbl As Label, msg As String, isErr As Boolean)
        lbl.Text = msg
        lbl.Font.Bold = True
        'Case No. 3927 ---Commented by Canarys
        'lbl.BackColor = Drawing.Color.White
        If isErr Then
            lbl.ForeColor = Drawing.Color.DarkRed
        Else
            lbl.ForeColor = Drawing.Color.Blue
        End If
    End Sub
    Public Shared Function CalculateIRDays(ByVal IrDaysSetting As Integer, ByVal saturdayCutOff As Boolean, ByVal sundayCutoff As Boolean) As Integer
        'we need to do a calculation to add days for weekends when setting IR days
        'for each saturday or sunday we cross, we want to add 1
        Dim weekendDays As Integer
        Dim startingIRDays As Integer

        Do Until startingIRDays > IrDaysSetting
            weekendDays = 0
            For ird As Integer = startingIRDays To IrDaysSetting
                Dim testDate As DateTime = Date.Today.AddDays(ird)
                If Not saturdayCutOff Then
                    If testDate.DayOfWeek = DayOfWeek.Saturday Then
                        weekendDays += 1
                    End If
                End If
                If Not sundayCutoff Then
                    If testDate.DayOfWeek = DayOfWeek.Sunday Then
                        weekendDays += 1
                    End If
                End If
                startingIRDays += 1
            Next

            IrDaysSetting += weekendDays
        Loop
        Return IrDaysSetting
    End Function
    Public Shared Function RecordIRDateHistory(IRID As Integer, DateChanged As DateTime, OldDate As DateTime, NewDate As DateTime, status As String, ChangedByUserID As Integer) As Boolean
        Dim db As New RickProject.DB.DataBase
        db.Init("IRDateHistoryInsert")
        db.AddParameter("@IRID", IRID)
        db.AddParameter("@dateChanged", DateChanged)
        db.AddParameter("@oldDate", OldDate)
        db.AddParameter("@newDate", NewDate)
        db.AddParameter("@status", status)
        db.AddParameter("@changedByUserID", ChangedByUserID)

        Dim rv As Integer = db.ExecuteAndReturn
        db.Close()
        If rv <> 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    Public Shared Function CleanFilename(ByVal filename As String) As String
        'remove invalid characters for a filename

        'preserve the suffix
        Dim suffix As String = ""
        Dim prefix As String = ""
        If filename.IndexOf(".") > -1 Then
            Dim lastDotPos As Integer = filename.LastIndexOf(".")
            Dim suffixLength As Integer = filename.Length - filename.LastIndexOf(".")
            suffix = TrimAll(filename.Substring(lastDotPos, suffixLength))
            prefix = TrimAll(filename.Substring(0, lastDotPos))
        Else
            prefix = filename
        End If

        'prefix = prefix.Replace("  ", " ")

        'Trim out illegal characters

        prefix = Regex.Replace(prefix, "[^A-Za-z0-9-_ ]", "")

        'Trim Length
        If prefix.Length > 100 Then
            prefix = prefix.Substring(0, 99)
        End If

        Return prefix & suffix

    End Function
    Public Shared Function TrimAll(ByVal TextIn As String, Optional ByVal TrimChar As String = " ", Optional ByVal CtrlChar As String = Chr(0)) As String
        Dim trimString As String = ""
        If TextIn Is Nothing OrElse TextIn.Length < 1 Then Return TextIn
        trimString = Replace(TextIn, TrimChar, CtrlChar)   ' In case of CrLf etc.

        While InStr(trimString, CtrlChar + CtrlChar) > 0
            trimString = trimString.Replace(CtrlChar + CtrlChar, CtrlChar)
        End While

        trimString = trimString.Trim(CtrlChar)    ' Trim Begining and End
        trimString = trimString.Replace(CtrlChar, TrimChar)   ' Replace with Original Trim Character(s)

        Return trimString

    End Function
    Public Shared Function DropHasValue(ByVal drop As DropDownList, ByVal text As String) As Boolean
        For Each li As ListItem In drop.Items
            If li.Value = text Then Return True
        Next
        Return False
    End Function

    Public Shared Function FormatAddress(ByVal street As String, ByVal city As String, ByVal state As String, ByVal zip As String) As String
        Dim address As String = ""
        If Not String.IsNullOrEmpty(street) Then address &= street

        If Not String.IsNullOrEmpty(city) Then
            If Not String.IsNullOrEmpty(address) Then
                address &= ", " & city
            Else
                address = city
            End If
        End If
        If Not String.IsNullOrEmpty(state) Then
            If Not String.IsNullOrEmpty(address) Then
                address &= ", " & state
            Else
                address = state
            End If
        End If
        If Not String.IsNullOrEmpty(zip) Then
            If Not String.IsNullOrEmpty(address) Then
                address &= " " & zip
            Else
                address = zip
            End If
        End If
        Return address
    End Function



    Public Shared Function CheckFileSize(ByVal myFile As FileUpload) As Boolean
        'get Max upload size in MB                 
        Dim maxFileSize As Double = Math.Round(15360 / 1024.0, 1)

        'get File size in MB
        Dim fileSize As Double = (myFile.PostedFile.ContentLength / 1024) / 1024.0

        If fileSize > maxFileSize Then
            Return False
        Else
            Return True
        End If
    End Function

    'Important Don't Delete "GetEmailServerSettings" method
    Public Shared Function GetEmailServerSettings() As System.Net.Mail.SmtpClient
        Dim ServerName As String = System.Environment.MachineName.ToString
        Dim SMTPServer As String = ServerName
        Dim New1and1SMTPServer As String = "smtp.ionos.com"
        Dim TestServerSMTP As String = "74.208.229.74"
        Dim LiveServerSMTP As String = "74.208.79.143"

        Dim settingValue As String = 0
        Dim dt As New DataTable
        Dim db As New RickProject.DB.DataBase
        db.Init("GetEmailServerSetting")
        settingValue = Convert.ToInt32(db.ExecuteScalarString())
        db.Close()

        Dim MailServ As New System.Net.Mail.SmtpClient
        Select Case settingValue
            Case 0
                MailServ.Host = SMTPServer
            Case 1
                MailServ.UseDefaultCredentials = False
                MailServ.Credentials = New System.Net.NetworkCredential("emailsender@buildercms.com", "CMScms2!")
                MailServ.Port = 25
                MailServ.EnableSsl = True
                MailServ.Host = New1and1SMTPServer
            Case 2
                MailServ.Host = TestServerSMTP
            Case 3
                MailServ.Host = LiveServerSMTP
            Case Else
        End Select
        Return MailServ
    End Function




End Class
