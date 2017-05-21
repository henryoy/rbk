using cm.mx.dbCore.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Criterion;

namespace cm.mx.catalogo.Model
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

        public bool Guardar(TipoInteres obj)
        {
            _exito = false;
            _session.Clear();
            _session.BeginTransaction();
            _session.SaveOrUpdate(obj);
            _session.Transaction.Commit();
            _exito = true;
            return _exito;
        }

        public List<TipoInteres> GetAllActivos()
        {
            var ls = _session.CreateCriteria<TipoInteres>().Add(Restrictions.Eq("Estatus", Enums.Estatus.ACTIVO.ToString())).List<TipoInteres>().ToList();
            return ls;
        }

        public bool ExisteTipoInteres(TipoInteres obj)
        {
            var ls = _session.CreateCriteria<TipoInteres>().Add(Restrictions.Eq("Nombre", obj.Nombre)).Add(Restrictions.Not(Restrictions.Eq("TipoInteresID", obj.TipoInteresID))).List<TipoInteres>().ToList();
            return ls.Count() > 0;
        }
    }
}
