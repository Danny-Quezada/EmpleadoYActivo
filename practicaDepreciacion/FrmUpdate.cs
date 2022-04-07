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
    public partial class FrmUpdate : Form
    {
        public IActivoServices services { get; set; }
        public IEmpleadoServices EmpleadoServices { get; set; }
        public IRegistroServices registroServices { get; set; }
        private int id;
        private Activo Activo;
        private Empleado Empleado;
        private int Opcion;
        public FrmUpdate(int Id,int opcion)
        {
            InitializeComponent();
            this.id = Id;
            this.Opcion = opcion;
        }

        private void FrmUpdate_Load(object sender, EventArgs e)
        {
            if (Opcion == 1)
            {
                Activo = services.GetById(id);
                if (Activo.Estado == Domain.Enum.EstadoActivo.Asignado)
                {
                    lblEmpleado.Visible = true;
                    TB.Visible = true;
                }
                txtNombre.Text = Activo.Nombre;
                txtDescripcion.Text = Activo.Descripcion;
                txtValor.Text = Activo.Valor.ToString();
                txtValorR.Text = Activo.ValorResidual.ToString();
                txtAU.Text = Activo.VidaUtil.ToString();
            }
            
         
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtNombre.Text) ||String.IsNullOrEmpty(txtDescripcion.Text)|| string.IsNullOrEmpty(txtValor.Text) || string.IsNullOrEmpty(txtValorR.Text) || string.IsNullOrEmpty(txtAU.Text))
            {
                MessageBox.Show("Rellene todo el formulario, por favor.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                    Activo activo = new Activo
                    {
                        Nombre = txtNombre.Text,
                        Valor = double.Parse(txtValor.Text),
                        ValorResidual = double.Parse(txtValorR.Text),
                        VidaUtil = int.Parse(txtAU.Text),
                        Id=Activo.Id,
                        Codigo=Activo.Codigo,
                        Descripcion=txtDescripcion.Text,
                        IdEmpleado=Activo.Id

                    };
                if (TB.Checked == false)
                {
                   
                   activo.Estado = Activo.Estado;
                }
                else
                {
               
                    activo.Estado = Domain.Enum.EstadoActivo.Disponible;
                   
                   Registro registro= registroServices.RegistroEspecifico(x => x.Activo.Id == activo.Id).First();
                    registro.Estado = EstadoRegistro.Inactivo;
                    registroServices.Actualizar(registro);
                   
                }
                services.Update(activo);
                this.Dispose();
                }
            
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar))
            {
                e.Handled = true;

                MessageBox.Show("NO SE PUEDEN NUMEROS");

            }
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = true;

                MessageBox.Show("NO SE PUEDEN LETRAS");

            }
        }

        private void txtValorR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = true;

                MessageBox.Show("NO SE PUEDEN LETRAS");

            }
        }

        private void txtAU_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = true;

                MessageBox.Show("NO SE PUEDEN LETRAS");

            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
