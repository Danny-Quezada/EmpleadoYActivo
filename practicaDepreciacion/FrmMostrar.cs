using AppCore.IServices;
using Domain.Entities;
using Domain.Interfaces;
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
    public partial class FrmMostrar : Form
    {
        public IEmpleadoServices empleadoServices { get; set; }
        public IActivoServices activoServices { get; set; }
        public IRegistroServices registroServices { get; set; }
        public int Opcion=-1;
        
        private Activo activo;
        private Empleado Empleado;
        private int Seleccionado;
        public FrmMostrar()
        {
            
            InitializeComponent();

        }
        public FrmMostrar(int opciones)
        {
            this.Opcion = opciones;
            InitializeComponent();
        }
        public FrmMostrar(Empleado empleado)
        {
            this.Empleado = empleado;
            InitializeComponent();
        }
        public FrmMostrar(Activo Cactivo)
        {
            this.activo = Cactivo;
            InitializeComponent();
        }
        private void guna2ImageButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2ImageButton4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void FrmMostrar_Load(object sender, EventArgs e)
        {
            if (Opcion !=-1)
            {

                dgvMostrar.DataSource = registroServices.RegistroEspecifico(x=>x.Empleado.Id==Opcion);
            }
            if (activo!=null)
            {
                dgvMostrar.DataSource = empleadoServices.Read();
            }
            else if (Empleado!=null)
            {
                dgvMostrar.DataSource = activoServices.Read();
                //this.dgvMostrar.Columns.RemoveAt(0);
            }
        }

        private void dgvMostrar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >=0)
            {

                Seleccionado = int.Parse(dgvMostrar.Rows[e.RowIndex].Cells[0].Value.ToString());
                if (activo!=null)
                {
                    btnSeleccionar.Visible = true;
                }
                else if (Empleado!= null)
                {
                    btnSeleccionar.Text = "Seleccionar activo";
                    btnSeleccionar.Visible = true;
                }
             
            }

        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (activo != null)
            {
                Empleado empleado = empleadoServices.GetById(Seleccionado);
                
                activo.Estado = Domain.Enum.EstadoActivo.Asignado;
                activoServices.Update(activo);
                Registro registro = new Registro()
                {
                    Estado = Domain.Enum.EstadoRegistro.Activo,
                    Empleado = empleado,
                    Activo = activo,
                    TiempoInicial = DateTime.Now.ToOADate()
                };
                registroServices.Add(registro);
                btnSeleccionar.Visible = false;
            }
           else if (Empleado != null)
           {
                Activo activos = activoServices.GetById(Seleccionado);
                activos.Estado = Domain.Enum.EstadoActivo.Asignado;
                activoServices.Update(activos);
                registroServices.Add(new Registro()
                {
                    Estado=Domain.Enum.EstadoRegistro.Activo,
                    Activo=activos,
                    Empleado=Empleado,
                    TiempoInicial=DateTime.Now.ToOADate()
                });
                btnSeleccionar.Visible = false;
           }
        }
    }
}
