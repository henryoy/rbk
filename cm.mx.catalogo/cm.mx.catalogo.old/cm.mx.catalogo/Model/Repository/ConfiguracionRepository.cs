using cm.mx.dbCore.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.mx.catalogo.Model
{
    internal class ConfiguracionRepository : RepositoryBase<Configuracion>
    {
        public override Configuracion GetById(int id)
        {
            throw new NotImplementedException();
        }

        public override Configuracion GetById(object id)
        {
            throw new NotImplementedException();
        }

        public Configuracion GetByClave(string clave)
        {
            return this.Query(x => x.Clave == clave).FirstOrDefault();
        }

        public override Configuracion GetNewEntidad()
        {
            throw new NotImplementedException();
        }

        public override bool Update(Configuracion modificado)
        {
            throw new NotImplementedException();
        }


    }
}
