using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.IServices
{
   public interface IRegistroServices:IServices<Registro>
    {
        List<Registro> RegistroEspecifico(Expression<Func<Registro, bool>> where);
        void Actualizar(Registro registro);
    }
}
