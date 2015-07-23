Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Friend NotInheritable Class ConnectionManager
    ''' <summary>
    ''' 07/22/2015 - rjg: Function GetConnection establishes, opens and returns connection to support T-SQL operations
    ''' </summary>
    ''' <remarks></remarks>

    Public Shared Function GetConnection() As SqlConnection

        Dim connectionString As String = "User ID=<username>;Password=<strong password>;Initial Catalog=Restaurant;Data Source=(local)"

        ' create a new connection object
        Dim connection As New SqlConnection(connectionString)

        ' open and return connection
        connection.Open()

        Return connection

    End Function

End Class

