using cm.mx.dbCore.Clases;
using System;
using System.Collections.Generic;
using System.Linq;

namespace cm.mx.catalogo.Model
{
    internal class NotificacionRepository : RepositoryBase<Notificacion>
    {
        public List<Notificacion> GetAllActivos()
        {
            List<Notificacion> lsNotificacion = new List<Notificacion>();
            lsNotificacion = _session.QueryOver<Notificacion>().Where(f => f.Estatus == Enums.Estatus.ALTA.ToString() || f.Estatus == Enums.Estatus.INACTIVO.ToString()).List().ToList();
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

        public override Notificacion GetNewEntidad()
        {
            throw new NotImplementedException();
        }

        public override bool Update(Notificacion modificado)
        {
            throw new NotImplementedException();
        }
    }
}