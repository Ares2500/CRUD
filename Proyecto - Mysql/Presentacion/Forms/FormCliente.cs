using Domain.Models;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Forms
{
    public partial class FormCliente : Form
    {
        private ClienteModel cliente = new ClienteModel();

        public FormCliente()
        {
            InitializeComponent();
            panel1.Enabled = true;
        }

        private void FormCliente_Load(object sender, EventArgs e)
        {
            ListarClientes();
        }

        private void ListarClientes()
        {
            dataGridView1.DataSource = cliente.GetAll();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            cliente.Nombre = txtNombre.Text;
            cliente.Apellido = txtApellido.Text;
            cliente.Rfc = txtRfc.Text;
            cliente.Domicilio = txtDomicilio.Text;

            bool valid = new Helps.DataValidation(cliente).Validate();

            if (valid == true)
            {
                string result = cliente.saveChanges();
                MessageBox.Show(result);
                ListarClientes();
                Restart();
            }
        }


        private void Restart()
        {
            panel1.Enabled = false;
            txtNombre.Clear();
            txtApellido.Clear();
            txtDomicilio.Clear();
            txtRfc.Clear();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                cliente.State = EntityState.Modified;
                cliente.ClienteId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

                panel1.Enabled = true;
                txtNombre.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtApellido.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtRfc.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                txtDomicilio.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            }
            else
            {
                MessageBox.Show("seleccione una fila antes de modificar");
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            panel1.Enabled = true;
            cliente.State = EntityState.Added;
        }
    }
}
