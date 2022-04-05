using AppCore.Factories;
using Domain.Entities;
using Domain.Enum;
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
    public partial class FrmDepreciacion : Form
    {
        private Activo activo;
        public FrmDepreciacion(Activo Activo)
        {
            this.activo = Activo;
            InitializeComponent();
            
           
        }
     

        private void FrmDepreciacion_Load(object sender, EventArgs e)
        {

        }
    }
}
