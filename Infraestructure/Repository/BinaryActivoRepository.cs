using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class BinaryActivoRepository : IActivoModel
    {
        private RAFContext context;
        private const int SIZE = 405+576;
        public BinaryActivoRepository()
        {
            context = new RAFContext("activo", SIZE);
        }
        public void Add(Activo t)
        {
            try
            {
                context.Create<Activo>(t);
            }
            catch (IOException)
            {
                throw;
            }

        }

        public void Delete(Activo t)
        {
            throw new NotImplementedException();
        }

        public Activo GetById(int id)
        {
            try
            {
                return context.Get<Activo>(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Activo> Read()
        {
            return context.GetAll<Activo>();
        }

        public void Update(Activo activo)
        {
            context.Update<Activo>(activo);
        }

        public bool Delete(int id)
        {
            return context.Delete(id);
        }
    }
}
