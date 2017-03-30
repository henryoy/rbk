using cm.mx.dbCore.Clases;
using System;

namespace cm.mx.catalogo.Model
{
    internal class FechaPublicacionRepository : RepositoryBase<Fechapublicacion>
    {
        public override Fechapublicacion GetById(int id)
        {
            return _session.Get<Fechapublicacion>(id);
        }

        public override Fechapublicacion GetById(object id)
        {
            return _session.Get<Fechapublicacion>(id);
        }

        public override Fechapublicacion GetNewEntidad()
        {
            throw new NotImplementedException();
        }

        public override bool Update(Fechapublicacion modificado)
        {
            throw new NotImplementedException();
        }
    }
}