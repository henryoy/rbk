using cm.mx.dbCore.Clases;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;

namespace cm.mx.catalogo.Model
{
    internal class NotificacionRepository : RepositoryBase<Notificacion>
    {
        public List<Notificacion> GetAllActivos()
        {
            List<Notificacion> lsNotificacion = new List<Notificacion>();
            lsNotificacion = _session.QueryOver<Notificacion>().Where(f => f.Estatus == Enums.Estatus.ACTIVO.ToString() || f.Estatus == Enums.Estatus.INACTIVO.ToString()).List().ToList();
            return lsNotificacion;
        }

        public Notificacion GuardarNotificacion(Notificacion oNotificacion)
        {
            _exito = false;
            _session.BeginTransaction();
            _session.SaveOrUpdate(oNotificacion);
            _session.Transaction.Commit();
            _exito = true;
            return oNotificacion;
        }

        public bool EliminarNotificacion(Notificacion oNotificacion)
        {
            _exito = false;
            _session.BeginTransaction();
            oNotificacion.Estatus = Enums.Estatus.BAJA.ToString();
            _session.SaveOrUpdate(oNotificacion);
            _session.Transaction.Commit();
            _exito = true;
            return true;
        }

        public override Notificacion GetById(int id)
        {
            return _session.Get<Notificacion>(id);
        }

        public override Notificacion GetById(object id)
        {
            return _session.Get<Notificacion>(id);
        }

        public Notificacion GetNotificacion(int UsuarioId, int PromocionId)
        {
            Notificacion oNotificacion = _session.QueryOver<Notificacion>().Where(f => f.PromocionID == PromocionId && f.UsuarioID == UsuarioId).List().ToList().FirstOrDefault();
            return oNotificacion;
        }

        public override Notificacion GetNewEntidad()
        {
            throw new NotImplementedException();
        }

        public override bool Update(Notificacion modificado)
        {
            throw new NotImplementedException();
        }

        public List<Notificacion> GetByTipo(string tipo)
        {
            _exito = false;
            List<Notificacion> lsNotificaion = new List<Notificacion>();
            var r = _session.CreateCriteria<Notificacion>().Add(Restrictions.Eq("Tipo", tipo).IgnoreCase()).List<Notificacion>();
            if (r.Count() > 0)
            {
                lsNotificaion = r.ToList();
            }
            return lsNotificaion;
        }

        //<sumary>1 si es la referencia 2, 2 si es la nota de venta</sumary>
        public Notificacion getByReferencias(int tipo, string referencia)
        {
            Notificacion oNotificacion = null;
            if(tipo == 1)
                oNotificacion = _session.Query<Notificacion>().Where(x => x.Referencia == referencia).FirstOrDefault();
            else
                oNotificacion = _session.Query<Notificacion>().Where(x => x.FolioNota == referencia).FirstOrDefault();

            return oNotificacion;
        }
    }
}