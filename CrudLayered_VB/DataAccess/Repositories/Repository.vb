Imports System.Configuration
Imports MySql.Data.MySqlClient


Public MustInherit Class Repository
    Private ReadOnly connectionString As String

    Public Sub New()
        connectionString = ConfigurationManager.ConnectionStrings("ConnMyCompany").ToString()
    End Sub

    Protected Function GetConnection() As MySqlConnection
        Dim conexion As MySqlConnection = New MySqlConnection
        conexion.ConnectionString = connectionString
        Return conexion
    End Function
End Class
