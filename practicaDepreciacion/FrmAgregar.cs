using AppCore.IServices;
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
    public partial class FrmAgregar : Form
    {
        public IEmpleadoServices empleado { get; set; };
        public IActivoServices activoServices { get; set; };
        public FrmAgregar()
        {
            InitializeComponent();
        }

        private void FrmAgregar_Load(object sender, EventArgs e)
        {

        }
    }
}
