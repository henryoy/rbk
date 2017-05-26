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
    internal class PromocionRedimirRepository : RepositoryBase<Promocionredimir>
    {
        public override Promocionredimir GetById(int id)
        {
            Promocionredimir oPromo = new Promocionredimir();
            oPromo = _session.Get<Promocionredimir>(id);
            return oPromo;
        }

        public override Promocionredimir GetById(object id)
        {
            Promocionredimir oPromo = new Promocionredimir();
            oPromo = _session.Get<Promocionredimir>(id);
            return oPromo;
        }

        public List<Promocionredimir> GetAllPromoRedimir(object id)
        {
            List<Promocionredimir> lsPromo = new List<Promocionredimir>();
            lsPromo = _session.QueryOver<Promocionredimir>().List().ToList();
            return lsPromo;
        }
        public List<Promocionredimir> GetAllPromoRedimir(Paginacion oPaginacion)
        {
            //List<Promocionredimir> lsPromo = new List<Promocionredimir>();
            //lsPromo = _session.QueryOver<Promocionredimir>().List().ToList();
            List<Promocionredimir> lsPromo = new List<Promocionredimir>();
            if (oPaginacion == null)
                oPaginacion = new Paginacion();

            ICriteria criteria = _session.CreateCriteria<Promocionredimir>();
            criteria.SetProjection(Projections.RowCount());
            int _Count = (int)criteria.UniqueResult();
            oPaginacion.TotalRegistros = _Count;

            ICriteria _criteria = _session.CreateCriteria<Promocionredimir>();
            lsPromo = _criteria.List<Promocionredimir>().Skip(oPaginacion.Pagina * oPaginacion.Cantidad).Take(oPaginacion.Cantidad).ToList();

            this._exito = true;

            return lsPromo;
        }

        public bool Guardar(Promocionredimir oPromocion)
        {
            _exito = false;
            _session.Clear();
            _session.BeginTransaction();
            _session.SaveOrUpdate(oPromocion);

            

            _session.Transaction.Commit();
            _exito = true;
            return _exito;
        }
        public bool PromocionIsRedimida(int UsuarioId, int PromocionId)
        {
            _exito = false;
            Promocionredimir oPromo = new Promocionredimir();
            oPromo = _session.CreateCriteria<Promocionredimir>().CreateAlias("Usuario", "Usuario")
                .CreateAlias("Promocion", "Promocion")
                .Add(Restrictions.Eq("Usuario.Usuarioid", UsuarioId))
                .Add(Restrictions.Eq("Promocion.Promocionid", PromocionId)).List<Promocionredimir>().ToList().FirstOrDefault();

            if (oPromo != null)
            {
                if (oPromo.PromocionRedimirId > 0)
                {
                    _exito = true;
                }
            }

            return _exito;
        }
        public override Promocionredimir GetNewEntidad()
        {
            throw new NotImplementedException();
        }

        public override bool Update(Promocionredimir modificado)
        {
            throw new NotImplementedException();
        }
    }
}
