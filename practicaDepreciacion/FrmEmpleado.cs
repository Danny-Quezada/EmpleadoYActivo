﻿using AppCore.Helper;
using AppCore.IServices;
using Domain.Entities;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace practicaDepreciacion
{
    public partial class FrmEmpleado : Form
    {
        public IEmpleadoServices EmpleadoServices { get; set; }
        public IActivoServices ActivoServices { get; set; }
        public IRegistroServices registroServices { get; set; }
        private int Seleccionado=-1;
        public FrmEmpleado()
        {
            InitializeComponent();
        }

        private void Guna2ImageButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Guna2ImageButton4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void FrmEmpleado_Load(object sender, EventArgs e)
        {
            FillDgv();
            this.cmbEstado.Items.AddRange(Enum.GetValues(typeof(EstadoEmpleado)).Cast<object>().ToArray());
        }

        private void BtnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                Validator.Validar(txtEmail.Text, txtTelefono.Text, txtCedula.Text);
            }
            catch (ArgumentException argument)
            {
                MessageBox.Show(argument.Message, "Error");
                return;
            }
            if (String.IsNullOrEmpty(txtApellido.Text) || String.IsNullOrEmpty(txtNombre.Text) || String.IsNullOrEmpty(txtCedula.Text) || String.IsNullOrEmpty(txtEmail.Text) || String.IsNullOrEmpty(txtDireccion.Text) || String.IsNullOrEmpty(txtTelefono.Text) || cmbEstado.SelectedIndex == -1)
            {
                MessageBox.Show("Rellena todo el formulario, por favor.");
                return;
            }

            else
            {
                Empleado Empleado = new Empleado()
                {
                    Cedula = txtCedula.Text,
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Direccion = txtDireccion.Text,
                    Telefono = txtTelefono.Text,
                    Email = txtEmail.Text,
                    Estado = (EstadoEmpleado)cmbEstado.SelectedIndex
                };
                EmpleadoServices.Add(Empleado);
                ClearForms();
                FillDgv();
            }
        }
        private void ClearForms()
        {
            txtApellido.Clear();
            txtNombre.Clear();
            txtCedula.Clear();
            txtEmail.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            cmbEstado.SelectedIndex = -1;
        }




        private void dgvEmpleados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void FillDgv()
        {
            dgvEmpleados.Rows.Clear();

            foreach (Empleado empleado in EmpleadoServices.Read())
            {
                dgvEmpleados.Rows.Add(empleado.Id, empleado.Nombre, empleado.Cedula, empleado.Apellido, empleado.Direccion, empleado.Telefono, empleado.Email, empleado.Estado);
            }
        }

        private void borrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Seleccionado > 0)
            {
                EmpleadoServices.Delete(Seleccionado);
               
                FillDgv();
            }
        }

        private void dgvEmpleados_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    this.dgvEmpleados.CurrentCell = this.dgvEmpleados.Rows[e.RowIndex].Cells[0];
                    Seleccionado = int.Parse(dgvEmpleados.Rows[e.RowIndex].Cells[0].Value.ToString());

                }
                cmsOption.Show(Cursor.Position);

            }
        }

        private void dgvEmpleados_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Click");
        }

        private void dgvEmpleados_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    this.dgvEmpleados.CurrentCell = this.dgvEmpleados.Rows[e.RowIndex].Cells[0];
                    Seleccionado = int.Parse(dgvEmpleados.Rows[e.RowIndex].Cells[0].Value.ToString());

                }
                cmsOption.Show(Cursor.Position);

            }
        }

        private void agregarleUnEmpleadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EmpleadoServices.Read().Count > 0)
            {
                FrmMostrar frmMostrar = new FrmMostrar(EmpleadoServices.GetById(Seleccionado));
                frmMostrar.empleadoServices = EmpleadoServices;
                frmMostrar.activoServices = ActivoServices;
                frmMostrar.ShowDialog();
            }
            else
            {
                MessageBox.Show("Agrega activos para poder asignarlos a los empleados");
            }
        }
    }
}
