Public Interface IEmployeeRepository
    Inherits IGenericRepository(Of Employee)
    Function GetBySalary() As IEnumerable(Of Employee)
End Interface
