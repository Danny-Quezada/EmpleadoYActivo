using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Registro
    {
        public int Id { get; set; }
        public int IdActivo { get; set; }
        public int IdEmpleado { get; set; }
        public long TiempoInicial { get; set; }
        public EstadoRegistro Estado{ get; set; }
    }
}
