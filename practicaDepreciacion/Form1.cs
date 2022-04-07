using AppCore.Helper;
using AppCore.IServices;
using Domain.Entities;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace practicaDepreciacion
{
    public partial class Form1 : Form
    {
        private IActivoServices activoServices;
        private IEmpleadoServices EmpleadoServices;
        private IRegistroServices registroServices;
        private int Seleccionado = -1;
        
        //[DllImport("kernel32.dll", SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //static extern bool AllocConsole();
        public Form1(IRegistroServices PregistroServices,IActivoServices ActivoServices,IEmpleadoServices CEmpleadoServices)
        {
            this.registroServices = PregistroServices;
            this.activoServices = ActivoServices;
            this.EmpleadoServices = CEmpleadoServices;
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            //AllocConsole();
            FillDgv();
            this.cmbEstado.Items.AddRange(Enum.GetValues(typeof(EstadoActivo)).Cast<object>().ToArray());
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void dgvActivos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Seleccionado = int.Parse(dgvActivos.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
            else
            {
                cmsOption.Visible = false;
            }
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Seleccionado != -1)
            {
                FrmUpdate frmUpdate = new FrmUpdate(Seleccionado,1);
                frmUpdate.services = activoServices;
                frmUpdate.registroServices = registroServices;
                frmUpdate.ShowDialog();
                FillDgv();
            }
            else
            {
                cmsOption.Visible = false;
            }
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtValor.Text)||string.IsNullOrEmpty(txtValorR.Text)|| string.IsNullOrEmpty(txtAU.Text)||string.IsNullOrEmpty(txtDescripcion.Text)|| cmbEstado.SelectedIndex==-1)
            {
                MessageBox.Show("Rellene todo el formulario, por favor.","Información",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            
            else
            {
                Activo activo = new Activo
                {
                    Nombre = txtNombre.Text,
                    Descripcion = txtDescripcion.Text,
                    Valor = double.Parse(txtValor.Text),
                    ValorResidual = double.Parse(txtValorR.Text),
                    VidaUtil = int.Parse(txtAU.Text),
                    Codigo=System.Guid.NewGuid().ToString(),
                    IdEmpleado = -1,
                    Estado = (EstadoActivo)cmbEstado.SelectedIndex,
                };
                activoServices.Add(activo);
                FillDgv();
                clearForms();
            }

        }
        private void clearForms()
        {
            txtAU.Clear();
            txtNombre.Clear();
            txtValor.Clear();
            txtValorR.Clear();
            txtDescripcion.Clear();
            cmbEstado.SelectedIndex = -1;
        }
        private void FillDgv()
        {
            dgvActivos.Rows.Clear();

            foreach(Activo activo in activoServices.Read())
            {
                dgvActivos.Rows.Add(activo.Id,activo.Nombre,activo.Descripcion,activo.Estado,activo.Valor,activo.ValorResidual,activo.VidaUtil,activo.Codigo);
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

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar))
            {
                e.Handled = true;

                MessageBox.Show("NO SE PUEDEN NUMEROS");

            }
        }

        private void depreciacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void borrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Seleccionado != -1)
            {
                activoServices.Delete(Seleccionado);
                Registro registro = registroServices.RegistroEspecifico(x => x.Activo.Id == Seleccionado).First();
                registro.Estado = EstadoRegistro.Inactivo;
                registroServices.Actualizar(registro);
                FillDgv();
                Seleccionado = -1;
            }
           
        }

        private void dgvActivos_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void dgvActivos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvActivos_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    this.dgvActivos.CurrentCell=this.dgvActivos.Rows[e.RowIndex].Cells[0];
                    Seleccionado = int.Parse(dgvActivos.Rows[e.RowIndex].Cells[0].Value.ToString());
                    if (activoServices.GetById(Seleccionado).Estado==EstadoActivo.Asignado)
                    { 
                        this.cmsOption.Items[3].Available = false;
                    }
                    else
                    {
                        this.cmsOption.Items[3].Enabled = true;
                        this.cmsOption.Items[3].Available = true;
                        
                     
                    }
                }
                
                //this.cmsOption.Show(this.dgvActivos, e.Location);
                cmsOption.Show(Cursor.Position);
                
            }
        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void agregarleUnEmpleadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EmpleadoServices.Read().Count>0)
            {
                FrmMostrar frmMostrar = new FrmMostrar(activoServices.GetById(Seleccionado));
                frmMostrar.empleadoServices = EmpleadoServices;
                frmMostrar.activoServices = activoServices;
                frmMostrar.registroServices = registroServices;
                frmMostrar.ShowDialog();
                FillDgv();
            }
            else
            {
                MessageBox.Show("No tienes empleados, agrega uno para poder seleccionarlo.");
            }
        }

        
        
        private void btnEmpleado_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void btnEmpleado_Click(object sender, EventArgs e)
        {
            FrmEmpleado empleado = new FrmEmpleado();
            empleado.EmpleadoServices = EmpleadoServices;
            empleado.ActivoServices = activoServices;
            empleado.registroServices = registroServices;
            empleado.ShowDialog();
            FillDgv();
        }

        private void dgvActivos_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    this.dgvActivos.CurrentCell = this.dgvActivos.Rows[e.RowIndex].Cells[0];
                    Seleccionado = int.Parse(dgvActivos.Rows[e.RowIndex].Cells[0].Value.ToString());
                    if (activoServices.GetById(Seleccionado).Estado == EstadoActivo.Asignado)
                    {
                        this.cmsOption.Items[3].Available = false;
                    }
                    else
                    {
                        this.cmsOption.Items[3].Enabled = true;
                        this.cmsOption.Items[3].Available = true;


                    }
                }

                //this.cmsOption.Show(this.dgvActivos, e.Location);
                cmsOption.Show(Cursor.Position);

            }
        }
    }
}
