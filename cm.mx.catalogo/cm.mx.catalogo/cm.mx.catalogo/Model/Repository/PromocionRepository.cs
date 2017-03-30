using cm.mx.catalogo.Enums;
using cm.mx.dbCore.Clases;
using System;
using System.Collections.Generic;
using System.Linq;

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
            lsPromocion = _session.QueryOver<Promocion>().Where(f => f.Estado == Enums.Estatus.ALTA.ToString() || f.Estado == Enums.Estatus.INACTIVO.ToString()).List().ToList();
            return lsPromocion;
        }

        public List<Promocion> GetAllPromocionActivas()
        {
            List<Promocion> lsPromocion = new List<Promocion>();
            lsPromocion = _session.QueryOver<Promocion>().Where(f => f.Estado == Enums.Estatus.ALTA.ToString()).List().ToList();
            return lsPromocion;
        }

        public Promocion GuardarPromocion(Promocion oPromocion)
        {
            _exito = false;
            _session.Transaction.Begin();
            _session.SaveOrUpdate(oPromocion);
            _session.Transaction.Commit();
            _exito = false;
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
    }
}