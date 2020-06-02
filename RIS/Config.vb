Option Strict On
Imports System.Web

Imports System.Data.SqlClient

Imports RickProject.DB
Imports System.Net.Mail
Imports System.IO

Public Class Config


    Public Shared Property LoginUserType() As String
        Get
            If Config.UserCookie Then
                Return GetCookieValue("LoginUserType")
            Else
                Return Convert.ToString(HttpContext.Current.Session("LoginUserType"))
            End If
        End Get
        Set(ByVal value As String)
            If Config.UserCookie Then
                SetCookie("LoginUserType", value)
            Else
                HttpContext.Current.Session("LoginUserType") = value
            End If
        End Set

    End Property


    Public Shared ReadOnly CookieAgeInDays As Integer = 15

    Public Shared ReadOnly UseCookie As Boolean = False
    Public Shared Function GetCookieValue(ByVal name As String) As String

        Dim cookie As HttpCookie = HttpContext.Current.Request.Cookies(name)
        If cookie Is Nothing Then
            Return ""
        End If
        Return cookie.Value
    End Function
    Public Shared Function GetCOResponsiblePartyID() As Integer
        Dim coResponsiblePartyID As Integer = 0
        Dim Db As DataBase = New DataBase
        Db.Init("GetCORefAndDefaultParty")
        Db.AddParameter("@ProjectId", CStr(Config.ProjectID), 16, SqlDbType.Int, ParameterDirection.Input)
        Db.AddParameter("@CompanyId", Config.CompanyId, 16, SqlDbType.Int, ParameterDirection.Input)
        Dim dr As SqlDataReader = Db.command.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            If Not dr.IsDBNull(1) Then
                coResponsiblePartyID = dr.GetInt32(1)
            End If
        End If
        dr.Close()
        Db.Close()
        Return coResponsiblePartyID
    End Function

    Private Shared Function GetCOPartyCompanyID() As Integer
        Dim coPartyCompanyID As Integer = 0
        Dim Db As DataBase = New DataBase
        Db.Init("GetCOCompanyName")
        Db.AddParameter("@ProjectId", CStr(Config.ProjectID), 16, SqlDbType.Int, ParameterDirection.Input)
        Db.AddParameter("@userID", CStr(Config.UserId), 16, SqlDbType.Int, ParameterDirection.Input)
        Dim dr As SqlDataReader = Db.command.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            If Not dr.IsDBNull(1) Then
                coPartyCompanyID = dr.GetInt32(1)
            End If
        End If
        dr.Close()
        Db.Close()
        Return coPartyCompanyID
    End Function

    Private Shared Function GetCOPartyIndividualID(ByVal isUserId As Boolean) As Integer
        Dim coPartyIndividualID As Integer = 0
        Dim coPartyAssignedUserID As Integer = 0
        Dim Db As DataBase = New DataBase
        Db.Init("GetCOIndividualID")
        Db.AddParameter("@ProjectId", CStr(Config.ProjectID), 16, SqlDbType.Int, ParameterDirection.Input)
        Db.AddParameter("@userID", CStr(Config.UserId), 16, SqlDbType.Int, ParameterDirection.Input)
        Dim dr As SqlDataReader = Db.command.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            If Not dr.IsDBNull(0) And Not dr.IsDBNull(1) Then
                coPartyIndividualID = dr.GetInt32(0)
                coPartyAssignedUserID = dr.GetInt32(1)
            End If
        End If
        dr.Close()
        Db.Close()
        If Not isUserId Then
            Return coPartyIndividualID
        Else
            Return coPartyAssignedUserID
        End If

    End Function

    Private Shared Function CheckCOAssignedUser() As Boolean
        Dim isAssigendUser As Boolean = False
        Dim Db As DataBase = New DataBase
        Dim dr As SqlDataReader
        Try

            Db.Init("CheckCOAssignedUser")
            Db.AddParameter("@COPID", Config.COPartyCompanyID)
            Db.AddParameter("@userID", Config.UserId)
            dr = Db.command.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If Not dr.IsDBNull(0) Then
                    If dr.GetInt32(0) = 1 Then
                        isAssigendUser = True
                    End If
                End If
            End If
            dr.Close()
            Db.Close()

        Catch ex As Exception
            isAssigendUser = False
        Finally
            If Not (IsNothing(dr)) AndAlso Not (dr.IsClosed) Then dr.Close()
            Db.Close()
        End Try
        Return isAssigendUser
    End Function

    Public Shared Function GetCORef() As String
        Dim coRef As String = ""
        Dim Db As DataBase = New DataBase
        Db.Init("GetCORefAndDefaultParty")
        Db.AddParameter("@ProjectId", CStr(Config.ProjectID), 16, SqlDbType.Int, ParameterDirection.Input)
        Db.AddParameter("@CompanyId", Config.CompanyId, 16, SqlDbType.Int, ParameterDirection.Input)
        Dim dr As SqlDataReader = Db.command.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            If Not dr.IsDBNull(1) Then
                coRef = dr.GetString(0)
            End If
        End If
        If Not (IsNothing(dr)) AndAlso Not (dr.IsClosed) Then dr.Close()
        Db.Close()
        Return coRef
    End Function

    Private Shared Function GetCOUserPermissions(ByVal isGC As Boolean) As Boolean
        Dim isAssigendUser As Boolean = False
        Dim Db As DataBase = New DataBase
        Dim dr As SqlDataReader
        Try

            Db.Init("GetCOUserPermissions")
            Db.AddParameter("@projectID", Config.ProjectID)
            Db.AddParameter("@userID", Config.UserId)
            dr = Db.command.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If isGC Then
                    If Not dr.IsDBNull(0) Then
                        'If dr.GetInt32(0) = 1 Then
                        isAssigendUser = dr.GetBoolean(0)
                        'End If
                    End If
                Else
                    If Not dr.IsDBNull(1) Then
                        'If dr.GetInt32(1) = 1 Then
                        isAssigendUser = dr.GetBoolean(1)
                        'End If
                    End If
                End If
            End If
            dr.Close()
            Db.Close()

        Catch ex As Exception
            isAssigendUser = False
        Finally
            If Not (IsNothing(dr)) AndAlso Not (dr.IsClosed) Then dr.Close()
            Db.Close()
        End Try
        Return isAssigendUser
    End Function

    Private Shared Function CheckForCOAvailability() As Boolean
        Dim isCOAvailable As Boolean = False
        Dim Db As DataBase = New DataBase
        Dim dr As SqlDataReader
        Try

            Db.Init("CheckForCOAvailability")
            Db.AddParameter("@ProjectID", Config.ProjectID)
            dr = Db.command.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If Not dr.IsDBNull(0) Then
                    isCOAvailable = dr.GetBoolean(0)
                End If
            End If

            If Not (IsNothing(dr)) AndAlso Not (dr.IsClosed) Then dr.Close()
            Db.Close()

        Catch ex As Exception
            isCOAvailable = False
        Finally
            If Not (IsNothing(dr)) AndAlso Not (dr.IsClosed) Then dr.Close()
            Db.Close()
        End Try
        Return isCOAvailable
    End Function

    Private Shared Function CheckForAPLProject() As Boolean
        Dim isApl As Boolean = False
        Dim Db As DataBase = New DataBase
        Dim dr As SqlDataReader
        Try

            Db.Init("APL_CheckForAPLProject")
            Db.AddParameter("@ProjectID", Config.ProjectID)
            Db.AddParameter("@CompanyId", Config.CompanyId, 16, SqlDbType.Int, ParameterDirection.Input)
            dr = Db.command.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If Not dr.IsDBNull(0) Then
                    isApl = dr.GetBoolean(0)
                End If
            End If

            If Not (IsNothing(dr)) AndAlso Not (dr.IsClosed) Then dr.Close()
            Db.Close()

        Catch ex As Exception
            isApl = False
        Finally
            If Not (IsNothing(dr)) AndAlso Not (dr.IsClosed) Then dr.Close()
            Db.Close()
        End Try
        Return isApl
    End Function

    Public Shared Function GetAPLProjectId(Optional ByRef aplRPID As Integer = 0) As Integer
        Dim aplProjectId As Integer = 0
        Dim Db As DataBase = New DataBase
        Dim dr As SqlDataReader
        Try

            Db.Init("APL_GetAPLProjectId")
            Db.AddParameter("@ProjectID", Config.APL_ProjectID)
            Db.AddParameter("@ProjectName", Config.ProjectName)
            dr = Db.command.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If Not dr.IsDBNull(0) Then
                    aplProjectId = dr.GetInt32(0)
                End If
                If Not dr.IsDBNull(1) Then
                    aplRPID = dr.GetInt32(1)
                End If
            End If

            If Not (IsNothing(dr)) AndAlso Not (dr.IsClosed) Then dr.Close()
            Db.Close()

        Catch ex As Exception
            aplProjectId = 0
            aplRPID = 0
        Finally
            If Not (IsNothing(dr)) AndAlso Not (dr.IsClosed) Then dr.Close()
            Db.Close()
        End Try
        Return aplProjectId
    End Function

    Public Shared Function CheckForSuperReviewer() As Boolean
        Dim isSuperReviewer As Boolean = False
        Dim Db As DataBase = New DataBase
        Dim dr As SqlDataReader
        Try

            Db.Init("APl_CheckForSuperReviewer")
            Db.AddParameter("@RaProjectID", Config.APL_ProjectID)
            Db.AddParameter("@UserId", Config.UserId)
            dr = Db.command.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If Not dr.IsDBNull(0) Then
                    If dr.GetString(0) = "1" Then
                        isSuperReviewer = True
                    End If
                End If
            End If

            If Not (IsNothing(dr)) AndAlso Not (dr.IsClosed) Then dr.Close()
            Db.Close()

        Catch ex As Exception
            isSuperReviewer = False
        Finally
            If Not (IsNothing(dr)) AndAlso Not (dr.IsClosed) Then dr.Close()
            Db.Close()
        End Try
        Return isSuperReviewer
    End Function

    Public Shared Property UserCookie() As Boolean
        Get
            Try
                Return Convert.ToBoolean(GetCookieValue("UserCookie"))
            Catch ex As Exception

            End Try
            Return False
        End Get
        Set(ByVal value As Boolean)
            SetCookie("UserCookie", value, 14)
        End Set
    End Property

    Public Shared Sub SetCookie(ByVal name As String, ByVal value As Object)
        SetCookie(name, value, Config.CookieAgeInDays)
    End Sub

    Public Shared Sub SetCookie(ByVal name As String, ByVal value As Object, ByVal CookieAgeInDays As Integer)
        Dim cookie As HttpCookie = HttpContext.Current.Request.Cookies(name)
        If cookie Is Nothing Then
            cookie = New HttpCookie(name)
        End If
        cookie.Value = Convert.ToString(value)

        cookie.Expires = DateTime.Now.AddDays(CookieAgeInDays)
        HttpContext.Current.Response.Cookies.Add(cookie)
    End Sub

    Public Shared Property UserId() As Integer
        Get
            If Config.UserCookie Then
                Return CInt(GetCookieValue("UserId"))
            Else
                Return CInt(Convert.ToString(HttpContext.Current.Session("UserId")))
            End If
        End Get
        Set(ByVal value As Integer)
            If Config.UserCookie Then
                SetCookie("UserId", value.ToString)
            End If
            HttpContext.Current.Session("UserId") = value.ToString
        End Set
    End Property

    Public Shared Property IsTermsAccepted() As Boolean
        Get
            If Config.UserCookie Then
                Return Convert.ToBoolean(GetCookieValue("TermsAccepted"))
            Else
                Return Convert.ToBoolean(Convert.ToString(HttpContext.Current.Session("TermsAccepted")))
            End If
        End Get
        Set(ByVal value As Boolean)
            If Config.UserCookie Then
                SetCookie("TermsAccepted", value)
            End If
            HttpContext.Current.Session("TermsAccepted") = value
        End Set
    End Property

    Public Shared Property ProjectID() As Integer
        Get
            If Config.UserCookie Then
                Return CInt(GetCookieValue("ProjectID"))
            Else
                If Not IsNothing(HttpContext.Current.Session("ProjectID")) Then
                    Return CInt(HttpContext.Current.Session("ProjectID"))
                Else
                    Return 0
                End If

            End If
        End Get
        Set(ByVal value As Integer)
            If Config.UserCookie Then
                SetCookie("ProjectID", value.ToString)
            End If
            HttpContext.Current.Session("ProjectID") = value
        End Set
    End Property

    Public Shared Property APL_ProjectID() As Integer
        'Get
        '	Return GetAPLProjectId()
        'End Get
        Get
            If Config.UserCookie Then
                Return CInt(GetCookieValue("APL_ProjectID"))
            Else
                If Not IsNothing(HttpContext.Current.Session("APL_ProjectID")) Then
                    Return CInt(HttpContext.Current.Session("APL_ProjectID"))
                Else
                    Return 0
                End If

            End If
        End Get
        Set(ByVal value As Integer)
            If Config.UserCookie Then
                SetCookie("APL_ProjectID", value.ToString)
            End If
            HttpContext.Current.Session("APL_ProjectID") = value
        End Set
    End Property

    Public Shared Property ProjectName() As String
        Get
            If Config.UserCookie Then
                Return GetCookieValue("ProjectName")
            Else
                Return Convert.ToString(HttpContext.Current.Session("ProjectName"))
            End If
        End Get
        Set(ByVal value As String)
            If Config.UserCookie Then
                SetCookie("ProjectName", value)
            Else
                HttpContext.Current.Session("ProjectName") = value
            End If
        End Set
    End Property
    Public Shared Property CompanyId() As String
        Get
            If Config.UserCookie Then
                Return GetCookieValue("CompanyId")
            Else
                Return Convert.ToString(HttpContext.Current.Session("CompanyId"))
            End If
        End Get
        Set(ByVal value As String)
            If Config.UserCookie Then
                SetCookie("CompanyId", value)
            Else
                HttpContext.Current.Session("CompanyId") = value
            End If
        End Set
    End Property
    Public Shared Property CompanyName() As String
        Get
            If Config.UserCookie Then
                Return GetCookieValue("CompanyName")
            Else
                Return Convert.ToString(HttpContext.Current.Session("CompanyName"))
            End If
        End Get
        Set(ByVal value As String)
            If Config.UserCookie Then
                SetCookie("CompanyName", value)
            Else
                HttpContext.Current.Session("CompanyName") = value
            End If
        End Set
    End Property
    Public Shared Property CurrentUserName() As String
        Get
            If Config.UserCookie Then
                Return GetCookieValue("CurrentUserName")
            Else
                Return Convert.ToString(HttpContext.Current.Session("CurrentUserName"))
            End If

        End Get
        Set(ByVal value As String)
            If Config.UserCookie Then
                SetCookie("CurrentUserName", value)
            Else
                HttpContext.Current.Session("CurrentUserName") = value
            End If
        End Set
    End Property
    Public Shared Sub LogOut()
        Config.SetCookie("CurrentUserName", vbNull, -1)
        Config.SetCookie("UserId", vbNull, -1)
        Config.SetCookie("UserCookie", vbNull, -1)
        Config.UserCookie = False
        HttpContext.Current.Session.Clear()
    End Sub
    Public Shared Property ActiveProjectSetup() As String
        Get
            If Config.UserCookie Then
                Return GetCookieValue("ProjectSetup")
            Else
                Return Convert.ToString(HttpContext.Current.Session("ProjectSetup"))
            End If

        End Get
        Set(ByVal value As String)
            If Config.UserCookie Then
                SetCookie("ProjectSetup", value)
            Else
                HttpContext.Current.Session("ProjectSetup") = value
            End If
        End Set
    End Property


    Public Shared Property ShowHide() As String
        Get
            If Config.UserCookie Then
                Return GetCookieValue("ShowHide")
            Else
                Return Convert.ToString(HttpContext.Current.Session("ShowHide"))
            End If
        End Get
        Set(ByVal value As String)
            If Config.UserCookie Then
                SetCookie("ShowHide", value)
            Else
                HttpContext.Current.Session("ShowHide") = value
            End If
        End Set
    End Property

    Public Shared Property FloorId() As Integer
        Get

            Return Convert.ToInt32(HttpContext.Current.Session("FloorId"))

        End Get
        Set(ByVal value As Integer)

            HttpContext.Current.Session("FloorId") = value

        End Set
    End Property

    Public Shared Property AreaId() As Integer
        Get

            Return Convert.ToInt32(HttpContext.Current.Session("AreaId"))

        End Get
        Set(ByVal value As Integer)

            HttpContext.Current.Session("AreaId") = value

        End Set
    End Property

    Public Shared ReadOnly Property COResponsibleParty() As Integer
        Get
            Return CInt(GetCOResponsiblePartyID())
        End Get
    End Property

    Public Shared ReadOnly Property COPartyCompanyID() As Integer
        Get
            Return CInt(GetCOPartyCompanyID())
        End Get
    End Property

    Public Shared ReadOnly Property COPartyIndividualID() As Integer
        Get
            Return CInt(GetCOPartyIndividualID(False))
        End Get
    End Property

    Public Shared ReadOnly Property COPartyAssignedUserID() As Integer
        Get
            Return CInt(GetCOPartyIndividualID(True))
        End Get
    End Property

    Public Shared ReadOnly Property CORef() As String
        Get
            Return CStr(GetCORef())
        End Get
    End Property

    Public Shared ReadOnly Property IsAssignedUser() As Boolean
        Get
            Return CheckCOAssignedUser()
        End Get
    End Property

    Public Shared ReadOnly Property IsCOGeneralContractor() As Boolean
        Get
            Return GetCOUserPermissions(True)
        End Get
    End Property

    Public Shared ReadOnly Property IsCOSubContractor() As Boolean
        Get
            Return GetCOUserPermissions(False)
        End Get
    End Property

    Public Shared ReadOnly Property IsCOAvailable() As Boolean
        Get
            Return CheckForCOAvailability()
        End Get
    End Property

    'Public Shared ReadOnly Property IsAplProject() As Boolean
    '	Get
    '		Return CheckForAPLProject()
    '	End Get
    'End Property

    Public Shared Property IsAplProject() As Boolean
        Get
            If Config.UserCookie Then
                Return CBool(GetCookieValue("IsAplProject"))
            Else
                If Not IsNothing(HttpContext.Current.Session("IsAplProject")) Then
                    Return CBool(HttpContext.Current.Session("IsAplProject"))
                Else
                    Return False
                End If

            End If
        End Get
        Set(ByVal value As Boolean)
            If Config.UserCookie Then
                SetCookie("IsAplProject", value.ToString)
            End If
            HttpContext.Current.Session("IsAplProject") = value
        End Set
    End Property

    Public Shared ReadOnly Property CheckIsAplProject() As Boolean
        Get
            Return CheckForAPLProject()
        End Get
    End Property

    Public Shared Property APLDefaultRPID() As Integer
        Get
            If Config.UserCookie Then
                Return CInt(GetCookieValue("APLDefaultRPID"))
            Else
                If Not IsNothing(HttpContext.Current.Session("APLDefaultRPID")) Then
                    Return CInt(HttpContext.Current.Session("APLDefaultRPID"))
                Else
                    Return 0
                End If

            End If
        End Get
        Set(ByVal value As Integer)
            If Config.UserCookie Then
                SetCookie("APLDefaultRPID", value.ToString)
            End If
            HttpContext.Current.Session("APLDefaultRPID") = value
        End Set
    End Property

    Public Shared Property IsSuperReviewer() As Boolean
        Get
            If Not IsNothing(HttpContext.Current.Session("IsSuperReviewer")) Then
                Return DirectCast(HttpContext.Current.Session("IsSuperReviewer"), Boolean)
            Else
                Return False
            End If
        End Get
        Set(ByVal value As Boolean)
            HttpContext.Current.Session("IsSuperReviewer") = value
        End Set
    End Property

    Public Shared Property IssuesList() As DataTable
        Get
            If Not IsNothing(HttpContext.Current.Session("IssuesList")) Then
                Return DirectCast(HttpContext.Current.Session("IssuesList"), DataTable)
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As DataTable)
            HttpContext.Current.Session("IssuesList") = value
        End Set
    End Property

    Public Shared Property APLPageType() As String
        Get
            If Not IsNothing(HttpContext.Current.Session("APLPageType")) Then
                Return DirectCast(HttpContext.Current.Session("APLPageType"), String)
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            HttpContext.Current.Session("APLPageType") = value
        End Set
    End Property

    Public Shared Property APLRoomId() As Integer
        Get
            If Not IsNothing(HttpContext.Current.Session("APLRoomId")) Then
                Return DirectCast(HttpContext.Current.Session("APLRoomId"), Integer)
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Integer)
            HttpContext.Current.Session("APLRoomId") = value
        End Set
    End Property

    Public Shared Property AplRAID() As Integer
        Get
            If Not IsNothing(HttpContext.Current.Session("AplRAID")) Then
                Return DirectCast(HttpContext.Current.Session("AplRAID"), Integer)
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Integer)
            HttpContext.Current.Session("AplRAID") = value
        End Set
    End Property

    Public Shared Property AplRoomStatus() As String
        Get
            If Not IsNothing(HttpContext.Current.Session("AplRoomStatus")) Then
                Return DirectCast(HttpContext.Current.Session("AplRoomStatus"), String)
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            HttpContext.Current.Session("AplRoomStatus") = value
        End Set
    End Property

    Public Shared Property AplRoomNumber() As String
        Get
            If Not IsNothing(HttpContext.Current.Session("AplRoomNumber")) Then
                Return DirectCast(HttpContext.Current.Session("AplRoomNumber"), String)
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            HttpContext.Current.Session("AplRoomNumber") = value
        End Set
    End Property

    Public Shared Property AplNewRid() As Integer
        Get
            If Not IsNothing(HttpContext.Current.Session("AplNewRid")) Then
                Return DirectCast(HttpContext.Current.Session("AplNewRid"), Integer)
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Integer)
            HttpContext.Current.Session("AplNewRid") = value
        End Set
    End Property

    Public Shared Property AplRTRSId() As Integer
        Get
            If Not IsNothing(HttpContext.Current.Session("AplRTRSId")) Then
                Return DirectCast(HttpContext.Current.Session("AplRTRSId"), Integer)
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Integer)
            HttpContext.Current.Session("AplRTRSId") = value
        End Set
    End Property

    Public Shared Property AplRTId() As Integer
        Get
            If Not IsNothing(HttpContext.Current.Session("AplRTId")) Then
                Return DirectCast(HttpContext.Current.Session("AplRTId"), Integer)
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Integer)
            HttpContext.Current.Session("AplRTId") = value
        End Set
    End Property

    Public Shared Property AplRId() As Integer
        Get
            If Not IsNothing(HttpContext.Current.Session("AplRId")) Then
                Return DirectCast(HttpContext.Current.Session("AplRId"), Integer)
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Integer)
            HttpContext.Current.Session("AplRId") = value
        End Set
    End Property



    Public Shared Property IsRendering() As Boolean
        Get
            If Not IsNothing(HttpContext.Current.Session("IsRendering")) Then
                Return DirectCast(HttpContext.Current.Session("IsRendering"), Boolean)
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As Boolean)
            HttpContext.Current.Session("IsRendering") = value
        End Set
    End Property

    Public Shared Property ReviewTypeName() As String
        Get
            If Not IsNothing(HttpContext.Current.Session("ReviewTypeName")) Then
                Return DirectCast(HttpContext.Current.Session("ReviewTypeName"), String)
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As String)
            HttpContext.Current.Session("ReviewTypeName") = value
        End Set
    End Property

    Public Shared Property IsCPLProject() As Boolean
        Get
            If Config.UserCookie Then
                Return CBool(GetCookieValue("IsCPLProject"))
            Else
                If Not IsNothing(HttpContext.Current.Session("IsCPLProject")) Then
                    Return CBool(HttpContext.Current.Session("IsCPLProject"))
                Else
                    Return False
                End If

            End If
        End Get
        Set(ByVal value As Boolean)
            If Config.UserCookie Then
                SetCookie("IsCPLProject", value.ToString)
            End If
            HttpContext.Current.Session("IsCPLProject") = value
        End Set
    End Property

    Public Shared Property RecordToReturn() As Integer
        Get
            If Config.UserCookie Then
                Return CInt(GetCookieValue("RecordToReturn"))
            Else
                Return CInt(Convert.ToString(HttpContext.Current.Session("RecordToReturn")))
            End If
        End Get
        Set(ByVal value As Integer)
            If Config.UserCookie Then
                SetCookie("RecordToReturn", value.ToString)
            End If
            HttpContext.Current.Session("RecordToReturn") = value.ToString
        End Set
    End Property

    Public Shared Function SendMail(ByVal toAddress As String,
                               ByVal ccAddress As String,
                               ByVal fromAddress As String,
                                ByVal subject As String,
                                ByVal body As String,
                                ByVal attachments As String,
                              Optional ByVal machineName As String = "",
                              Optional ByVal sender As String = "", Optional ByVal bccAddress As String = "") As Boolean

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
            If Not String.IsNullOrEmpty(bccAddress) Then
                msg.Bcc.Add(bccAddress)
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
            MailServ = Common.GetEmailServerSettings
            'If String.IsNullOrEmpty(machineName) Then
            '    machineName = System.Web.HttpContext.Current.Server.MachineName
            'End If
            'MailServ.Host = machineName

            'If behalf Then
            'MailServ.
            'End If

            'If Not (machineName = "CMSPROD") Then
            '    Dim emails As String
            '    emails = "admin@buildercms.com"
            '    'emails = "srudrappa@buildercms.com"
            '    'emails = "nareshreddyyaradla.net@gmail.com"
            '    msg.Body &= "<br><br>Original Email List: To:" & toAddress & "<br><br> cc: " & ccAddress
            '    msg.Subject &= " - RIS TEST Generated Email From " & machineName
            '    'msg.To.Clear()
            '    'msg.To.Add(emails)
            '    msg.Bcc.Clear()
            '    msg.CC.Clear()
            'End If
            MailServ.Send(msg)
            Return True

        Catch
            Return False
        End Try

    End Function

    Public Shared Function CheckForGloabalResponsibleParty(ByVal ProjectRPID As Integer) As Boolean
        Dim ISGlobalRP As Boolean = False
        Dim db As New RickProject.DB.DataBase
        db.Init("Select GlobalRP from ResponsibleParties where ProjectRPId = @ProjectRPID", False)
        db.AddParameter("@ProjectRPID", ProjectRPID)
        ISGlobalRP = Convert.ToBoolean(db.command.ExecuteScalar)
        db.Close()
        Return ISGlobalRP
    End Function

    'Case 703:
    Public Shared Function GetIEBrowserMode() As Boolean
        '555
        ' Return False

        Dim isComptabilityMode As Boolean = False
        Dim userAgent As String = HttpContext.Current.Request.UserAgent
        If Not String.IsNullOrEmpty(userAgent) Then
            If userAgent.Contains("Trident/6.0") Or userAgent.Contains("Trident/7.0") Then
                If userAgent.Contains("MSIE 10.0") Or userAgent.Contains("rv:11.0") Then
                    isComptabilityMode = False
                Else
                    isComptabilityMode = True
                End If
            End If
        End If
        Return isComptabilityMode
    End Function
    'End Case

    Public Shared Function FormatPhoneNumber(ByVal phoneStr As String) As String
        If Len(phoneStr) < 1 Then
            Return ""
        ElseIf Len(phoneStr) > 10 Then
            Return phoneStr
        End If
        'strip out all the nonnumerics - \D means non-digit characters (\d is digit)
        phoneStr = Regex.Replace(phoneStr, "\D", "")

        If Left(phoneStr, 1) = "1" And Len(phoneStr) > 10 Then
            phoneStr = Mid(phoneStr, 2)
        End If

        Dim testPNum As Decimal
        Try
            testPNum = CDec(Left(phoneStr, 10))
        Catch ex As System.Exception
            Return phoneStr
        End Try
        If Len(testPNum.ToString) < 10 Then Return phoneStr

        Dim formattedString As String = String.Format("{0:(###) ###-####}", testPNum)
        Dim suffix As String = Mid(phoneStr, 11).Trim
        If suffix.Length > 0 Then
            formattedString &= " x" & suffix
        End If
        Return formattedString
    End Function
    Public Shared Function PhoneStorage(ByVal inputString As String) As String
        If Len(inputString) < 1 Then
            Return ""
        ElseIf Len(inputString) > 10 Then
            Return inputString
        End If

        Dim testStr As String = Regex.Replace(inputString, "\D", "")

        'strip off the leading 1 if they entered it
        If Left(testStr, 1) = "1" And Len(testStr) > 10 Then
            testStr = Mid(testStr, 2)
        End If
        Dim fPart As Decimal
        Try
            fPart = CDec(Left(testStr, 10))
        Catch ex As System.Exception
            Return inputString
        End Try
        testStr = fPart.ToString & " " & Mid(testStr, 11)
        testStr = testStr.Trim
        Return testStr
    End Function


End Class
