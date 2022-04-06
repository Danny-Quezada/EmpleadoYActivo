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
    public class ActivoServices:BaseServices<Activo>,IActivoServices
    {

        IActivoModel activoModel;
        public ActivoServices(IActivoModel model) : base(model)
        {
            this.activoModel = model;
        }

        public List<Activo> FindSpecific(Expression<Func<Activo, bool>> where)
        {
            return activoModel.FindSpecific(where);
        }

        public Activo GetById(int id)
        {
            return activoModel.GetById(id);
        }

        public void Update(Activo activo)
        {
            activoModel.Update(activo);
        }
    }

}
