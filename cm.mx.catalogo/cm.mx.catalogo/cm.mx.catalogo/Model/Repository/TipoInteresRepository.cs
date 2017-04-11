using cm.mx.dbCore.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.mx.catalogo.Model.Repository
{
    internal class TipoInteresRepository : RepositoryBase<TipoInteres>
    {
        public override TipoInteres GetById(int id)
        {
            return _session.Get<TipoInteres>(id);
        }

        public override TipoInteres GetById(object id)
        {
            return _session.Get<TipoInteres>(id);
        }

        public override TipoInteres GetNewEntidad()
        {
            throw new NotImplementedException();
        }

        public override bool Update(TipoInteres modificado)
        {
            throw new NotImplementedException();
        }
    }
}
