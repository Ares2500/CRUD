Imports MySql.Data.MySqlClient

Public Class MasterRepository
    Inherits Repository

    Protected parameters As List(Of MySqlParameter)

    Protected Function ExecuteNonQuery(transactSql As String) As Integer
        Using connection = GetConnection()
            connection.Open()
            Using Command = New MySqlCommand()
                Command.Connection = connection
                Command.CommandText = transactSql
                Command.CommandType = CommandType.Text
                For Each item As MySqlParameter In parameters
                    Command.Parameters.Add(item)
                Next
                Dim result = Command.ExecuteNonQuery()
                parameters.Clear()
                Return result
            End Using
        End Using
    End Function

    Protected Function ExecuteReader(transactSql As String) As DataTable
        Using connection = GetConnection()
            connection.Open()
            Using Command = New MySqlCommand()
                Command.Connection = connection
                Command.CommandText = transactSql
                Command.CommandType = CommandType.Text
                Dim reader = Command.ExecuteReader()
                Using table = New DataTable()
                    table.Load(reader)
                    reader.Dispose()
                    Return table
                End Using
            End Using
        End Using
    End Function
End Class
