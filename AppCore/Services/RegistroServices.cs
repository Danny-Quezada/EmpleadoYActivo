using AppCore.IServices;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Services
{
    public class RegistroServices:BaseServices<Registro>,IRegistroServices
    {
        protected IRegistroModel registroModel;
        public RegistroServices(IRegistroModel model) : base(model)
        {
            this.registroModel = model;
        }

        public List<Registro> RegistroEspecifico(Expression<Func<Registro, bool>> where)
        {
            return registroModel.RegistroEspecifico(where);
        }
    }
}
