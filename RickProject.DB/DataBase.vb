Option Strict On

Imports System
Imports System.Configuration
Imports System.Collections
Imports System.Data.SqlClient
Public Class DataBase

    Public Shared InstanceName As String = String.Empty
    'Public Shared ReadOnly strConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
    Public Shared ReadOnly strConnectionStringRISDev As String = System.Configuration.ConfigurationManager.AppSettings("ConnectionStringRISDev")
    'Public Shared ReadOnly strConnectionStringCMSTest As String = System.Configuration.ConfigurationManager.AppSettings("ConnectionStringCMSTest")
    Public Shared ReadOnly strConnectionStringRISProd As String = System.Configuration.ConfigurationManager.AppSettings("ConnectionStringRISPROD")
    ' Dim objcommonDAL As Common.Common
    ':::::::::::::::::::::::::::::::::::::'
    'Public Shared strConnectionString As String = "Server=developer-15; Database=4BERP; uid=sa; pwd=sa;"
    'Public Shared gblDBServer, gblDBName, gblDBUser, gblDBPassword As String
    'Public Shared gblBranchID As Integer
    'Public Shared LoggedInUserID As Integer = 0
    'Public Shared Sub setConnectionString(ByVal serverName As String, ByVal userId As String, ByVal password As String)
    '    gblDBServer = serverName
    '    gblDBName = "wsons"
    '    gblDBUser = userId
    '    gblDBPassword = password

    '    strConnectionString = "Initial Catalog=4BERP; Data Source=" & serverName & "; User Id=" & userId & "; Password=" & password  ''& " '' DAL.Common.Common.strConnectionString"
    '    '   strConnectionString1 = "Initial Catalog=ws-lhr05; Data Source=" & serverName & "; User Id=" & userId & "; Password=" & password  ''& " '' DAL.Common.Common.strConnectionString"

    'End Sub
    'Public Shared ReadOnly Property getConnectionString() As String
    '    Get
    '        Return strConnectionString
    '    End Get
    'End Property
    'Public Shared ReadOnly Property getBusinessCode() As String
    '    Get
    '        Return "AGR"
    '    End Get
    'End Property
    'Public Shared ReadOnly Property getGetBusnessID() As Integer
    '    Get
    '        Return 1
    '    End Get
    'End Property
    'Public Shared ReadOnly Property GetTransactionStartDate() As Date
    '    Get
    '        Return CDate("1/1/2006")
    '    End Get
    'End Property
    'Public Shared ReadOnly Property GetTransactionEndDate() As Date
    '    Get
    '        Return CDate("12/31/2006")
    '    End Get
    'End Property
    ':::::::::::::::::::::::::::::::::::::'
    'Public Shared SeverName As String
    'Public Shared UserId As String
    'Public Shared Password As String


    'Public Shared ReadOnly strConnectionString As String = "Initial Catalog=wsons; Data Source=" & SeverName & "; User Id=" & UserId & "; Password=" & Password ''& " '' DAL.Common.Common.strConnectionString"
    'Database connection strings
    'Public Shared ReadOnly CONN_STRING As String = ConfigurationSettings.AppSettings("MySQLConnString1")

    '// Hashtable to store cached parameters
    'Private parmCache As Hashtable = Hashtable.Synchronized(New Hashtable)

    '/// <summary>
    '/// Execute a OleDbCommand (that returns no resultset) against the database specified in the connection string 
    '/// using the provided parameters.
    '/// </summary>
    '/// <remarks>
    '/// e.g.:  
    '///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new OleDbParameter("@prodid", 24));
    '/// </remarks>
    '/// <param name="connectionString">a valid connection string for a OleDbConnection</param>
    '/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
    '/// <param name="commandText">the stored procedure name or T-SQL command</param>
    '/// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
    '/// <returns>an int representing the number of rows affected by the command</returns>
    Private table As DataTable
    Public command As SqlCommand
    Private _Connection As SqlConnection = New SqlConnection
    Public Property Connection() As SqlConnection
        Get
            Return _Connection

        End Get
        Set(ByVal value As SqlConnection)
            _Connection = value
        End Set
    End Property


    Public Sub CheckConnnection()
        If Connection.State <> System.Data.ConnectionState.Open Then
            Open()
        End If
    End Sub
    Public Sub Open()
        Try
            Try
                Close()
            Catch ex As Exception

            End Try

            'Dim test As String = My.Computer.Name.ToUpper
            Dim test As String = "test"
            If Not String.IsNullOrEmpty(InstanceName) Then
                test = InstanceName
            End If
            Dim connectionString As String
			Select Case test
                'Case "DHARMA"
                '	connectionString = strConnectionStringRISDev
                'Case "CMSTEST"
                '	connectionString = strConnectionStringCMSTest
                Case "production"
                    connectionString = strConnectionStringRISProd
                Case Else
                    connectionString = strConnectionStringRISDev
            End Select

			Connection = New SqlConnection(connectionString)
            Connection.Open()

        Catch ex As Exception
            'this.RollbackTransaction()
        End Try
    End Sub
    Public Sub Close()
        Try
            If Connection.State <> ConnectionState.Closed Then Connection.Close()
            If Not Reader Is Nothing AndAlso Not Reader.IsClosed Then Reader.Close()
        Catch ex As Exception

        End Try
    End Sub
	Protected _Reader As SqlDataReader
    Public Property Reader() As SqlDataReader
		Get
			'If IsNothing(_Reader) Then _Reader = New SqlDataReader
			Return _Reader

		End Get
        Set(ByVal value As SqlDataReader)
            _Reader = value
        End Set
    End Property
    Public Function Execute(ByVal sql As String, ByRef table As DataTable) As Boolean
        table = New DataTable

        Try
            CheckConnnection()
            command = New SqlCommand(sql, Connection)
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(command)
            adapter.Fill(table)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Public Function Execute(ByRef table As DataTable) As Boolean
        table = New DataTable

        Try
            CheckConnnection()
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(command)
            adapter.Fill(table)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Public Function Execute(ByRef ds As DataSet) As Boolean
        ds = New DataSet

        Try
            CheckConnnection()
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(command)
            adapter.Fill(ds)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    Public Function Execute(ByVal returnValue As Boolean) As String
        Dim result As String
        Try

            CheckConnnection()
            command.ExecuteNonQuery()
            'Dim adapter As SqlDataAdapter = New SqlDataAdapter(command)
            'adapter.Fill(table)
            result = command.Parameters("@UserId").Value.ToString()
        Catch ex As Exception
            Return ""
        End Try
        Return result
    End Function
    Public Function Execute() As Boolean

        Try
            CheckConnnection()
            command.ExecuteNonQuery()
            'Dim adapter As SqlDataAdapter = New SqlDataAdapter(command)
            'adapter.Fill(table)
        Catch ex As Exception
            Return False
        End Try
        Return True
	End Function
	Public Function ExecuteReader() As SqlDataReader

		Try
			CheckConnnection()
			Reader = command.ExecuteReader
			'Dim adapter As SqlDataAdapter = New SqlDataAdapter(command)
			'adapter.Fill(table)
		Catch ex As Exception
			Return Reader
		End Try
		Return Reader
	End Function
    Public Function ExecuteScalar() As Boolean

        Try
            CheckConnnection()
            Return Convert.ToBoolean(command.ExecuteScalar())
            'Dim adapter As SqlDataAdapter = New SqlDataAdapter(command)
            'adapter.Fill(table)
        Catch ex As Exception
            Return False
        Finally
            Close()
        End Try
        Return True
    End Function
    Public Function ExecuteScalarString() As String

        Try
            CheckConnnection()
            Return Convert.ToString(command.ExecuteScalar())
        Catch ex As Exception
            Return ""
        Finally
            Close()
        End Try

    End Function
	Public Function ExecuteAndReturn() As Integer
		Try
			CheckConnnection()
			Dim pReturn As SqlParameter = command.Parameters.Add("@RETURN", SqlDbType.Int)
			pReturn.Direction = ParameterDirection.ReturnValue

			Dim rowsAffected As Integer = command.ExecuteNonQuery
			If Not IsDBNull(pReturn.Value) AndAlso IsNumeric(pReturn.Value) Then
				Return Integer.Parse(pReturn.Value.ToString)
			Else
				Return -1
			End If

		Catch ex As Exception
			Return -1
		End Try
	End Function
	Public Sub Init(ByVal ProcedureName As String, Optional ByVal isProcedure As Boolean = True)
		CheckConnnection()
		command = New SqlCommand()
		command.CommandText = ProcedureName
		If isProcedure Then command.CommandType = CommandType.StoredProcedure
		command.Connection = Connection
		If IsTransaction Then
			command.Transaction = Transaction
		End If
	End Sub
    Private IsTransaction As Boolean = False
    Private Transaction As SqlTransaction
    Public Sub BeginTransaction()
        If Not IsTransaction Then
            CheckConnnection()
            Transaction = Connection.BeginTransaction
            IsTransaction = True
        End If
    End Sub
    Private _Parameter As SqlParameter
    Public Property Parameter() As SqlParameter
        Get
            Return _Parameter

        End Get
        Set(ByVal value As SqlParameter)
            _Parameter = value
        End Set
    End Property

    Public Sub AddParameter(ByVal name As String, ByVal value As String, ByVal size As Integer, ByVal type As SqlDbType, ByVal direction As ParameterDirection)
        Parameter = New SqlParameter
        Parameter.ParameterName = name
        Parameter.Value = value
        Parameter.Size = size
        Parameter.SqlDbType = type
        Parameter.Direction = direction
        If Not command.Parameters.Contains(Parameter) Then
            command.Parameters.Add(Parameter)
        End If
    End Sub
    Public Sub AddParameter(name As String, value As Object)
        If Not command.Parameters.Contains(name) Then
            command.Parameters.AddWithValue(name, value)
        End If
    End Sub
    '/// <summary>
    '/// Execute a OleDbCommand (that returns no resultset) against an existing database connection 
    '/// using the provided parameters.
    '/// </summary>
    '/// <remarks>
    '/// e.g.:  
    '///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new OleDbParameter("@prodid", 24));
    '/// </remarks>
    '/// <param name="conn">an existing database connection</param>
    '/// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
    '/// <param name="commandText">the stored procedure name or T-SQL command</param>
    '/// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
    '/// <returns>an int representing the number of rows affected by the command</returns>



End Class
