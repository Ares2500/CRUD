Imports System.ComponentModel.DataAnnotations
Imports DataAccess
Imports Domain

Public Class ClienteModel
    Private _ClienteId As Integer
    Private _Nombre As String
    Private _Apellido As String
    Private _rfc As String
    Private _Domicilio As String
    Private _Status As Integer
    Private _State As EntityState

    Private repositorio As IClienteRepository
    Private ClientesViewModel As List(Of ClienteModel)



#Region "Propiedades"
    Public Property ClienteId As Integer
        Get
            Return _ClienteId
        End Get
        Set(value As Integer)
            _ClienteId = value
        End Set
    End Property

    <Required(ErrorMessage:="El campo Nombre es requerido")>
    Public Property Nombre As String
        Get
            Return _Nombre
        End Get
        Set(value As String)
            _Nombre = value
        End Set
    End Property

    Public Property Apellido As String
        Get
            Return _Apellido
        End Get
        Set(value As String)
            _Apellido = value
        End Set
    End Property

    Public Property Rfc As String
        Get
            Return _rfc
        End Get
        Set(value As String)
            _rfc = value
        End Set
    End Property

    Public Property Domicilio As String
        Get
            Return _Domicilio
        End Get
        Set(value As String)
            _Domicilio = value
        End Set
    End Property

    Public Property Status As Integer
        Private Get
            Return _Status
        End Get
        Set(value As Integer)
            _Status = value
        End Set
    End Property

    Public Property State As EntityState
        Get
            Return _State
        End Get
        Set(value As EntityState)
            _State = value
        End Set
    End Property
#End Region


    Public Sub New()
        repositorio = New ClienteRepository()
    End Sub

    Public Function saveChanges() As String
        Dim message As String = Nothing

        Try
            Dim cliente As New Cliente
            cliente.ClienteId = ClienteId
            cliente.Nombre = Nombre
            cliente.Apellido = Apellido
            cliente.rfc = Rfc
            cliente.Domicilio = Domicilio
            cliente.Status = Status


            Select Case State
                Case EntityState.Added
                    repositorio.Add(cliente)
                    message = "Successfully record"
                Case EntityState.Modified
                    repositorio.Edit(cliente)
                    message = "Successfully edited"
                Case EntityState.Deleted
                    repositorio.Remove(ClienteId)
                    message = "Successfully removed"
            End Select

        Catch ex As Exception
            ex.ToString()
        End Try

        Return message
    End Function

    Public Function GetClientes() As List(Of ClienteModel)
        Dim ListClientesDataModel = repositorio.GetAll()
        ClientesViewModel = New List(Of ClienteModel)

        For Each item As Cliente In ListClientesDataModel

            ClientesViewModel.Add(New ClienteModel With {
                .ClienteId = item.ClienteId,
                .Nombre = item.Nombre,
                .Apellido = item.Apellido,
                .Rfc = item.rfc,
                .Domicilio = item.Domicilio
            })
        Next

        Return ClientesViewModel
    End Function


End Class
