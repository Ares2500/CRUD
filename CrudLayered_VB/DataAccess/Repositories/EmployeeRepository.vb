Imports MySql.Data.MySqlClient
Imports DataAccess

Public Class EmployeeRepository
    Inherits MasterRepository
    Implements IEmployeeRepository


    Private selectAll As String
    Private insert As String
    Private update As String
    Private delete As String

    Public Sub New()
        selectAll = "SELECT * FROM Employee"
        insert = "insert INTO Employee (IdNumber, Name, Mail, Birthday) VALUES (@idNumber, @name, @mail, @birthday);"
        update = "UPDATE Employee SET IdNumber = @idNumber, Name = @name, Mail = @mail, Birthday = @birthday WHERE IdPK = @idPk"
        delete = "DELETE FROM Employee WHERE IdPk = @idPk"
    End Sub



    Public Function GetAll() As IEnumerable(Of Employee) Implements IGenericRepository(Of Employee).GetAll
        Dim resultTable = ExecuteReader(selectAll)
        Dim listEmployees = New List(Of Employee)

        For Each item As DataRow In resultTable.Rows
            listEmployees.Add(New Employee With {
                .IdPk = item(0),
                .IdNumber = item(1),
                .Name = item(2),
                .Mail = item(3),
                .Birthday = item(4)
             })
        Next
        Return listEmployees
    End Function

    Public Function Add(entity As Employee) As Integer Implements IGenericRepository(Of Employee).Add
        parameters = New List(Of MySqlParameter)
        parameters.Add(New MySqlParameter("@idNumber", entity.IdNumber))
        parameters.Add(New MySqlParameter("@name", entity.Name))
        parameters.Add(New MySqlParameter("@mail", entity.Mail))
        parameters.Add(New MySqlParameter("@birthday", entity.Birthday))
        Return ExecuteNonQuery(insert)
    End Function

    Public Function Edit(entity As Employee) As Integer Implements IGenericRepository(Of Employee).Edit
        parameters = New List(Of MySqlParameter)
        parameters.Add(New MySqlParameter("@idPk", entity.IdPk))
        parameters.Add(New MySqlParameter("@idNumber", entity.IdNumber))
        parameters.Add(New MySqlParameter("@name", entity.Name))
        parameters.Add(New MySqlParameter("@mail", entity.Mail))
        parameters.Add(New MySqlParameter("@birthday", entity.Birthday))
        Return ExecuteNonQuery(update)
    End Function

    Public Function Remove(id As Integer) As Integer Implements IGenericRepository(Of Employee).Remove
        parameters = New List(Of MySqlParameter)
        parameters.Add(New MySqlParameter("@idPk", id))

        Return ExecuteNonQuery(delete)
    End Function

    Public Function GetBySalary() As IEnumerable(Of Employee) Implements IEmployeeRepository.GetBySalary
        Throw New NotImplementedException()
    End Function
End Class
