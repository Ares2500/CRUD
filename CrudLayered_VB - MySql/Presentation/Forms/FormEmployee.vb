Imports Domain

Public Class FormEmployee
    Dim employeeModel As New EmployeeModel()

    Private Sub FormEmployee_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListEmployees()
        panel1.Enabled = False
    End Sub

    Private Sub ListEmployees()
        Try
            dataGridView1.DataSource = employeeModel.GetEmployees()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        dataGridView1.DataSource = employeeModel.FindById(txtSearch.Text)
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        panel1.Enabled = True
        employeeModel.State = EntityState.Added
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If dataGridView1.SelectedRows.Count > 0 Then
            panel1.Enabled = True
            employeeModel.IdPk = dataGridView1.CurrentRow.Cells(0).Value
            employeeModel.State = EntityState.Modified
            txtIdNumber.Text = dataGridView1.CurrentRow.Cells(1).Value
            txtName.Text = dataGridView1.CurrentRow.Cells(2).Value
            txtMail.Text = dataGridView1.CurrentRow.Cells(3).Value
            txtBirthday.Value = dataGridView1.CurrentRow.Cells(4).Value
        Else
            MessageBox.Show("Selected row")
        End If
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        If dataGridView1.SelectedRows.Count > 0 Then
            panel1.Enabled = True
            employeeModel.IdPk = dataGridView1.CurrentRow.Cells(0).Value
            employeeModel.State = EntityState.Deleted
            Dim result = employeeModel.saveChanges()
            MessageBox.Show(result)
            ListEmployees()
        Else
            MessageBox.Show("Selected row")
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        employeeModel.IdNumber = txtIdNumber.Text
        employeeModel.Name = txtName.Text
        employeeModel.Mail = txtMail.Text
        employeeModel.Birthday = txtBirthday.Value

        Dim valid = New DataValidation(employeeModel).validate()
        If valid = True Then
            Dim result = employeeModel.saveChanges()
            MessageBox.Show(result)
            ListEmployees()
            Restart()
        End If
    End Sub


    Private Sub Restart()
        panel1.Enabled = False
        txtIdNumber.Clear()
        txtName.Clear()
        txtMail.Clear()
    End Sub

    Private Sub dataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dataGridView1.CellContentClick

    End Sub

    Private Sub label2_Click(sender As Object, e As EventArgs) Handles label2.Click

    End Sub

    Private Sub txtIdNumber_TextChanged(sender As Object, e As EventArgs) Handles txtIdNumber.TextChanged

    End Sub

    Private Sub txtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged

    End Sub

    Private Sub label1_Click(sender As Object, e As EventArgs) Handles label1.Click

    End Sub

    Private Sub panel1_Paint(sender As Object, e As PaintEventArgs) Handles panel1.Paint

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

    End Sub

    Private Sub txtBirthday_ValueChanged(sender As Object, e As EventArgs) Handles txtBirthday.ValueChanged

    End Sub

    Private Sub label4_Click(sender As Object, e As EventArgs) Handles label4.Click

    End Sub

    Private Sub label3_Click(sender As Object, e As EventArgs) Handles label3.Click

    End Sub

    Private Sub txtMail_TextChanged(sender As Object, e As EventArgs) Handles txtMail.TextChanged

    End Sub
End Class