using cm.mx.dbCore.Clases;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.mx.catalogo.Model
{
    internal class DistribucionRepository : RepositoryBase<Distribucion>
    {
        public override Distribucion GetById(int id)
        {
            return _session.Get<Distribucion>(id);
        }

        public override Distribucion GetById(object id)
        {
            return _session.Get<Distribucion>(id);
        }

        public override Distribucion GetNewEntidad()
        {
            throw new NotImplementedException();
        }

        public override bool Update(Distribucion modificado)
        {
            throw new NotImplementedException();
        }

        public bool Guardar(Distribucion obj)
        {
            _exito = false;
            _session.Clear();
            _session.BeginTransaction();
            _session.SaveOrUpdate(obj);
            _session.Transaction.Commit();
            _exito = true;
            return _exito;
        }

        public List<Distribucion> GetAllActivos()
        {
            _exito = false;
            List<Distribucion> ls = new List<Distribucion>();
            ls = _session.CreateCriteria<Distribucion>().Add(Restrictions.Eq("Estado", "ACTIVO")).List<Distribucion>().ToList();
            _exito = true;
            return ls;
        }
    }
}
