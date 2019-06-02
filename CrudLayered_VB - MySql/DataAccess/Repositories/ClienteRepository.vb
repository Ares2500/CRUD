Imports DataAccess
Imports MySql.Data.MySqlClient

Public Class ClienteRepository
    Inherits MasterRepository
    Implements IClienteRepository

    Private selectAll As String
    Private insert As String
    Private update As String
    Private delete As String


    Public Sub New()
        selectAll = "SELECT * FROM cliente;"
        insert = "INSERT INTO Cliente (Nombre, Apellido, rfc, Domicilio) VALUES (@nombre, @apellido, @rfc, @domicilio);"
        update = "UPDATE Cliente SET Nombre = @nombre, Apellido = @apellido, rfc = @rfc, Domicilio = @domicilio WHERE ClienteId = @idPk"
        delete = "DELETE FROM Employee WHERE IdPk = @idPk"
    End Sub


    Public Function GetAll() As IEnumerable(Of Cliente) Implements IGenericRepository(Of Cliente).GetAll
        Dim resultado = ExecuteReader(selectAll)
        Dim empleados = New List(Of Cliente)

        For Each item As DataRow In resultado.Rows
            empleados.Add(New Cliente With {
                .ClienteId = item(0),
                .Nombre = item(1),
                .Apellido = item(2),
                .rfc = item(3),
                .Domicilio = item(4),
                .Status = item(5)
            })
        Next
        Return empleados
    End Function

    Public Function Add(entity As Cliente) As Integer Implements IGenericRepository(Of Cliente).Add
        parameters = New List(Of MySqlParameter)
        parameters.Add(New MySqlParameter("@nombre", entity.Nombre))
        parameters.Add(New MySqlParameter("@apellido", entity.Apellido))
        parameters.Add(New MySqlParameter("@rfc", entity.rfc))
        parameters.Add(New MySqlParameter("@domicilio", entity.Domicilio))
        Return ExecuteNonQuery(insert)
    End Function

    Public Function Edit(entity As Cliente) As Integer Implements IGenericRepository(Of Cliente).Edit
        parameters = New List(Of MySqlParameter)
        parameters.Add(New MySqlParameter("@idPk", entity.ClienteId))
        parameters.Add(New MySqlParameter("@nombre", entity.Nombre))
        parameters.Add(New MySqlParameter("@apellido", entity.Apellido))
        parameters.Add(New MySqlParameter("@rfc", entity.rfc))
        parameters.Add(New MySqlParameter("@domicilio", entity.Domicilio))
        Return ExecuteNonQuery(update)
    End Function

    Public Function Remove(id As Integer) As Integer Implements IGenericRepository(Of Cliente).Remove
        Throw New NotImplementedException()
    End Function
End Class
