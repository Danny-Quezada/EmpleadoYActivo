﻿using AppCore.IServices;
using Domain.Entities;
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
       
        private Activo activo;
        private Empleado Empleado;
        private int Seleccionado;
        public FrmMostrar()
        {
            
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
            if (activo!=null)
            {
                dgvMostrar.DataSource = empleadoServices.Read();
            }
            else if (Empleado!=null)
            {
                dgvMostrar.DataSource = activoServices.Read();
                this.dgvMostrar.Columns.RemoveAt(0);
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
                activo.Empleado.Id = empleado.Id;
                activo.Estado = Domain.Enum.EstadoActivo.Asignado;
                activoServices.Update(activo);
                btnSeleccionar.Visible = false;
            }
           else if (Empleado != null)
           {
                Activo activos = activoServices.GetById(Seleccionado);
                activos.Empleado.Id = Empleado.Id;
                activos.Estado = Domain.Enum.EstadoActivo.Disponible;
                activoServices.Update(activos);
                btnSeleccionar.Visible = false;
           }
        }
    }
}
