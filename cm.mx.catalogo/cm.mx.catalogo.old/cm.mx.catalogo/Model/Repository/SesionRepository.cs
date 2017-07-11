using cm.mx.dbCore.Clases;
using System;
using System.Collections.Generic;

namespace cm.mx.catalogo.Model
{
    internal class SesionRepository : RepositoryBase<Sesion>
    {
        public override Sesion GetById(int id)
        {
            return _session.Get<Sesion>(id);
        }

        public override Sesion GetById(object id)
        {
            return _session.Get<Sesion>(id);
        }

        public override Sesion GetNewEntidad()
        {
            throw new NotImplementedException();
        }

        public override bool Update(Sesion modificado)
        {
            throw new NotImplementedException();
        }

        public bool Salir(string token)
        {
            _exito = false;
            _errores = new List<string>();
            _mensajes = new List<string>();
            try
            {
                _session.Clear();
                var obj = _session.QueryOver<Sesion>().Where(a => a.Token == token).SingleOrDefault();
                if (obj == null) _mensajes.Add("El token ingresado no es válido");
                else
                {
                    _session.BeginTransaction();
                    _session.Delete(obj);
                    _session.Transaction.Commit();
                    _exito = true;
                }
            }
            catch (Exception ex)
            {
                _exito = false;
                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
                _mensajes.Add("Ocurrio un problema al procesar la información");
            }

            return _exito;
        }
    }
}