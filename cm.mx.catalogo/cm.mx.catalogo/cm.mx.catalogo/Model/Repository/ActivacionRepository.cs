﻿using cm.mx.dbCore.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;

namespace cm.mx.catalogo.Model
{
    internal class ActivacionRepository : RepositoryBase<Activacion>
    {
        public List<Activacion> GetAllActivacion()
        {
            List<Activacion> lsActivacion = new List<Activacion>();
            lsActivacion = _session.QueryOver<Activacion>().List().ToList();
            return lsActivacion;
        }

        public int GetUsuarioIDByLlaveActivacion(string key)
        {
            int iUsuario = 0;

            Activacion oAct = _session.Query<Activacion>().Where(x => x.Llave == key).FirstOrDefault();

            if (oAct != null)
                iUsuario = oAct.UsuarioId;

            return iUsuario;
        }

        public Activacion GetKeyByLlave(string key)
        {

            Activacion oAct = _session.Query<Activacion>().Where(x => x.Llave == key).FirstOrDefault();
            _session.Clear();
            return oAct;
        }

        public List<Activacion> GetAllActivacionActivadas()
        {
            List<Activacion> lsActivacion = new List<Activacion>();
            lsActivacion = _session.QueryOver<Activacion>().Where(f => f.Activado).List().ToList();
            return lsActivacion;
        }

        public Activacion GuardarActivacion(Activacion oActivacion)
        {
            _exito = false;
            _session.Transaction.Begin();
            _session.SaveOrUpdate(oActivacion);
            _session.Transaction.Commit();
            _exito = false;
            return oActivacion;
        }

        public bool EliminarActivacion(Activacion oActivacion)
        {
            _exito = false;
            _session.Transaction.Begin();
            // oActivacion.Estado = Estatus.BAJA.ToString();
            _session.SaveOrUpdate(oActivacion);
            _session.Transaction.Commit();
            _exito = false;
            return _exito;
        }

        public override Activacion GetById(int id)
        {
            return _session.Get<Activacion>(id);
        }

        public override Activacion GetById(object id)
        {
            return _session.Get<Activacion>(id);
        }

        public override Activacion GetNewEntidad()
        {
            throw new NotImplementedException();
        }

        public override bool Update(Activacion modificado)
        {
            throw new NotImplementedException();
        }
    }
}