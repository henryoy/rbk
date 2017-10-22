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

        public List<Distribucion> GetDistribucion(int TipoMembresia)
        {
            _exito = false;
            List<Distribucion> ls = new List<Distribucion>();
            var IQuery = _session.CreateCriteria<Distribucion>();
            IQuery.Add(Restrictions.Eq("Estado", "ACTIVO"));
            IQuery.Add(Restrictions.Eq("TipoMembresia", TipoMembresia));
            ls = IQuery.List<Distribucion>().ToList();
            _exito = true;
            return ls;
        }

        public bool UpdateGroupId(int DistribucionID, int MRGroupId)
        {
            _exito = false;

            String hqlUpdate = "update Distribucion c set c.MRGroupId =:MRGroupId where c.DistribucionID=:DistribucionID";
            _session.Clear();
            _session.Transaction.Begin();
            int updatedEntities = _session.CreateQuery(hqlUpdate)
                    .SetInt32("DistribucionID", DistribucionID)
                    .SetInt32("MRGroupId", MRGroupId)
                    .ExecuteUpdate();
            _session.Transaction.Commit();
            _exito = true;
            return _exito;
        }
    }
}
