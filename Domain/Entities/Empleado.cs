using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Empleado
    {
        public List<Activo> Activos { get; set; }
        public int Id { get; set; }
        public string Cedula { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set;  }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public EstadoEmpleado Estado { get; set; }

        public override string ToString()
        {
            return $"{this.Id}";
        }
    }
}
