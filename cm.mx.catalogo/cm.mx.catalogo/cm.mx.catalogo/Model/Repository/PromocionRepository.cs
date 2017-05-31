using cm.mx.catalogo.Enums;
using cm.mx.dbCore.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using NHibernate;
using cm.mx.dbCore.Tools;
using NHibernate.Criterion;
namespace cm.mx.catalogo.Model
{
    internal class PromocionRepository : RepositoryBase<Promocion>
    {
        public override Promocion GetById(int id)
        {
            Promocion oPromocion = _session.Get<Promocion>(id);
            return oPromocion;
        }

        public List<Promocion> GetAllPromocion()
        {
            List<Promocion> lsPromocion = new List<Promocion>();
            lsPromocion = _session.QueryOver<Promocion>().Where(f => f.Estado == Enums.Estatus.ACTIVO.ToString() || f.Estado == Enums.Estatus.INACTIVO.ToString()).List().ToList();
            return lsPromocion;
        }

        public List<Promocion> GetAllPromocionActivas()
        {
            List<Promocion> lsPromocion = new List<Promocion>();
            lsPromocion = _session.QueryOver<Promocion>().Where(f => f.Estado == Enums.Estatus.ACTIVO.ToString()).List().ToList();
            return lsPromocion;
        }
        public List<Promocion> GetAllPromocion(Paginacion oPaginacion)
        {
            List<Promocion> lsPromocion = new List<Promocion>();

            ICriteria criteria = _session.CreateCriteria<Promocion>();
            criteria.Add(Restrictions.Eq("Estado", "ACTIVO"));
            //DateTime oInit = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            //DateTime oEnd = oInit.AddDays(1);
            //criteria.Add(Restrictions.Ge("FechaAlta", oInit))
            //.Add(Restrictions.Lt("FechaAlta", oEnd));
            criteria.SetProjection(Projections.RowCount());

            int _Count = (int)criteria.UniqueResult();
            oPaginacion.TotalRegistros = _Count;
            ICriteria _criteria = _session.CreateCriteria<Promocion>();
            criteria.Add(Restrictions.Eq("Estado", "ACTIVO"));
            lsPromocion = _criteria.List<Promocion>().Skip(oPaginacion.Pagina * oPaginacion.Cantidad).Take(oPaginacion.Cantidad).ToList();


            return lsPromocion;

        }
        public Promocion GuardarPromocion(Promocion oPromocion)
        {
            _exito = false;
            _session.Transaction.Begin();
            _session.SaveOrUpdate(oPromocion);
            _session.Transaction.Commit();
            _exito = true;
            return oPromocion;
        }

        public bool EliminarPromocion(int PromocionId)
        {
            _exito = false;
            Promocion oPromocion = this.GetById(PromocionId);
            if (oPromocion != null)
            {
                _session.Transaction.Begin();
                oPromocion.Estado = Estatus.BAJA.ToString();
                _session.SaveOrUpdate(oPromocion);
                _session.Transaction.Commit();
            }

            _exito = false;
            return _exito;
        }

        public override Promocion GetById(object id)
        {
            throw new NotImplementedException();
        }

        public override Promocion GetNewEntidad()
        {
            throw new NotImplementedException();
        }

        public override bool Update(Promocion modificado)
        {
            throw new NotImplementedException();
        }

        public List<Promocion> GetPromocionAply(Usuario oUsuario)
        {
           
            List<Promocion> lsPromocion = new List<Promocion>();
            lsPromocion = _session.QueryOver<Promocion>().Where(f => f.Vigenciafinal > DateTime.Now).List().ToList();
            return lsPromocion;
        }
        public List<Promocion> GetPromocionAplyVisita(String TipoMembresia)
        {

            List<Promocion> lsPromocion = new List<Promocion>();

            lsPromocion = _session.CreateCriteria<Promocion>()
               .Add(Restrictions.Eq("Estado", "ACTIVO"))
               .Add(Restrictions.Eq("Tipomembresia", TipoMembresia))
               .CreateCriteria("Promociondetalle").Add(Restrictions.Eq("Condicion", "VISITA"))
               .List<Promocion>().Distinct().ToList();

            //lsPromocion = _session.QueryOver<Promocion>().Where(f => f.Vigenciafinal > DateTime.Now && f.Promociondetalle.Any(x=>x.Condicion == "VISITA")).List().ToList();
            return lsPromocion;
        }

        public List<Promocion> GetPromocionBajaVisita(int  NumeroVisita)
        {

            List<Promocion> lsPromocion = new List<Promocion>();

            lsPromocion = _session.CreateCriteria<Promocion>()
               .Add(Restrictions.Eq("Estado", "ACTIVO"))               
               .CreateCriteria("Promociondetalle")
               .Add(Restrictions.Eq("Condicion", "VISITA"))
               .Add(Restrictions.Lt(Projections.Cast(NHibernateUtil.Int32, Projections.Property("Valor1")), NumeroVisita))
               .List<Promocion>().Distinct().ToList();
           
            return lsPromocion;
        }
    }
}