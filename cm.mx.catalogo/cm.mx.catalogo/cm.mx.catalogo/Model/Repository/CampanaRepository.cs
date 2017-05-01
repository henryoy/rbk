using cm.mx.dbCore.Clases;
using cm.mx.dbCore.Tools;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.mx.catalogo.Model
{
    internal class CampanaRepository : RepositoryBase<Campana>
    {
        public override Campana GetById(int id)
        {
            Campana oCampana = new Campana();
            oCampana = _session.Get<Campana>(id);
            return oCampana;
        }

        public override Campana GetById(object id)
        {
            Campana oCampana = new Campana();
            oCampana = _session.Get<Campana>(id);
            return oCampana;
        }

        public override Campana GetNewEntidad()
        {
            throw new NotImplementedException();
        }
        public Campana GuardarCampana(Campana Objeto)
        {
            _session.Clear();
            _session.Transaction.Begin();
            _session.SaveOrUpdate(Objeto);
            _session.Transaction.Commit();
            return Objeto;
        }
        public List<Campana> GetAllCampana(Paginacion oPaginacion)
        {
            List<Campana> lsCampana = new List<Campana>();
            if (oPaginacion == null)
                oPaginacion = new Paginacion();

            ICriteria criteria = _session.CreateCriteria<Campana>();
            criteria.SetProjection(Projections.RowCount());
            int _Count = (int)criteria.UniqueResult();
            oPaginacion.TotalRegistros = _Count;


            lsCampana = _session.CreateCriteria<Campana>().List<Campana>().Skip(oPaginacion.Pagina * oPaginacion.Cantidad).Take(oPaginacion.Cantidad).ToList();

            this._exito = true;

            return lsCampana;
        }
        public override bool Update(Campana modificado)
        {
            throw new NotImplementedException();
        }
    }
}
