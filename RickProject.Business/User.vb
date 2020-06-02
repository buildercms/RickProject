Public Class User
    Private _UserId As Int16
    Public Property UserId() As Int32
        Get
            Return _UserId
        End Get
        Set(ByVal value As Int32)
            _UserId = value
        End Set
    End Property
    Private _UserName As String
    Public Property UserName() As String
        Get
            Return _UserName
        End Get
        Set(ByVal value As String)
            _UserName = value
        End Set
    End Property
    Private _Password As String
    Public Property Password() As String
        Get
            Return _Password
        End Get
        Set(ByVal value As String)
            _Password = value
        End Set
    End Property

    Private _FirstName As String
    Public Property FirstName() As String
        Get
            Return _FirstName
        End Get
        Set(ByVal value As String)
            _FirstName = value
        End Set
    End Property
    Private _LastName As String
    Public Property LastName() As String
        Get
            Return _LastName
        End Get
        Set(ByVal value As String)
            _LastName = value
        End Set
    End Property

    Private _Email As String
    Public Property Email() As String
        Get
            Return _Email
        End Get
        Set(ByVal value As String)
            _Email = value
        End Set
    End Property
    Private _Phone As String
    Public Property Phone() As String
        Get
            Return _Phone
        End Get
        Set(ByVal value As String)
            _Phone = value
        End Set
    End Property
    Private _CarrierEmail As String
    Public Property CarrierEmail() As String
        Get
            Return _CarrierEmail
        End Get
        Set(ByVal value As String)
            _CarrierEmail = value
        End Set
    End Property
    Private _LogoImage As String
    Public Property LogoImage() As String
        Get
            Return _LogoImage
        End Get
        Set(ByVal value As String)
            _LogoImage = value
        End Set
    End Property
    Private _UserImage As String
    Public Property UserImage() As String
        Get
            Return _UserImage
        End Get
        Set(ByVal value As String)
            _UserImage = value
        End Set
    End Property

    Private _GGID As String
    Public Property GGID() As String
        Get
            Return _GGID
        End Get
        Set(ByVal value As String)
            _GGID = value
        End Set
    End Property

    Private _OfficeStreet As String
    Public Property OfficeStreet() As String
        Get
            Return _OfficeStreet
        End Get
        Set(ByVal value As String)
            _OfficeStreet = value
        End Set
    End Property
    Private _OfficeState As String
    Public Property OfficeState() As String
        Get
            Return _OfficeState
        End Get
        Set(ByVal value As String)
            _OfficeState = value
        End Set
    End Property
    Private _OfficeCity As String
    Public Property OfficeCity() As String
        Get
            Return _OfficeCity
        End Get
        Set(ByVal value As String)
            _OfficeCity = value
        End Set
    End Property
    Private _OfficeZip As String
    Public Property OfficeZip() As String
        Get
            Return _OfficeZip
        End Get
        Set(ByVal value As String)
            _OfficeZip = value
        End Set
    End Property
    Private _OfficeCountry As String
    Public Property OfficeCountry() As String
        Get
            Return _OfficeCountry
        End Get
        Set(ByVal value As String)
            _OfficeCountry = value
        End Set
    End Property
    Private _IsActive As Boolean
    Public Property IsActive() As Boolean
        Get
            Return _IsActive
        End Get
        Set(ByVal value As Boolean)
            _IsActive = value
        End Set
    End Property
    Private _IsSuperUser As Boolean
    Public Property IsSuperUser() As Boolean
        Get
            Return _IsSuperUser
        End Get
        Set(ByVal value As Boolean)
            _IsSuperUser = value
        End Set
    End Property

    Private _companyName As String
    Public Property CompanyName() As String
        Get
            Return _companyName
        End Get
        Set(ByVal value As String)
            _companyName = value
        End Set
    End Property



End Class
