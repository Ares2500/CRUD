Imports Domain

Public Class FormCliente
    Dim Cliente As New ClienteModel()

    Private Sub FormCliente_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListarEmpleados()
        Limpiar()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Cliente.Nombre = txtNombre.Text
        Cliente.Apellido = txtApellido.Text
        Cliente.Rfc = txtRfc.Text
        Cliente.Domicilio = txtDomicilio.Text


        Dim valid = New DataValidation(Cliente).validate()
        If valid = True Then
            Dim result = Cliente.saveChanges()
            MessageBox.Show(result)
            ListarEmpleados()
            Limpiar()
        End If
    End Sub

    Private Sub ListarEmpleados()
        dataGridView1.DataSource = Cliente.GetClientes()
    End Sub

    Private Sub Limpiar()
        txtNombre.Clear()
        txtApellido.Clear()
        txtRfc.Clear()
        txtDomicilio.Clear()
        panel1.Enabled = False
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        panel1.Enabled = True
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If dataGridView1.SelectedRows.Count > 0 Then
            panel1.Enabled = True
            Cliente.ClienteId = dataGridView1.CurrentRow.Cells(0).Value
            Cliente.State = EntityState.Modified

            txtNombre.Text = dataGridView1.CurrentRow.Cells(1).Value
            txtApellido.Text = dataGridView1.CurrentRow.Cells(2).Value
            txtRfc.Text = dataGridView1.CurrentRow.Cells(3).Value
            txtDomicilio.Text = dataGridView1.CurrentRow.Cells(4).Value
        Else
            MessageBox.Show("Seleccione una fila")
        End If
    End Sub
End Class