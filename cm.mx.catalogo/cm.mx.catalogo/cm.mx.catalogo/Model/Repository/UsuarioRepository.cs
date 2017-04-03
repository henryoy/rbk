using cm.mx.catalogo.Enums;
using cm.mx.dbCore.Clases;
using cm.mx.dbCore.Tools;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;

namespace cm.mx.catalogo.Model
{
    internal class UsuarioRepository : RepositoryBase<Usuario>
    {
        public bool Login(string usuario, string pass)
        {
            _exito = false;
            //_mensajes = new List<string>();
            //_errores = new List<string>();
            //try
            //{
            if (!Funciones.ValidarCorreo(usuario)) _mensajes.Add("Ingrese un correo válido");
            else
            {
                var user = _session.QueryOver<Usuario>().Where(x => x.Email.ToLower() == usuario.ToLower() && x.Contrasena.Equals(pass)).SingleOrDefault();
                if (user == null) _mensajes.Add("El usuario y/o contraseña es incorrecto");
                else _exito = true;
            }
            //}
            //catch (Exception ex)
            //{
            //    while (ex != null)
            //    {
            //        _errores.Add(ex.Message);
            //        ex = ex.InnerException;
            //    }
            //    _mensajes.Add("Ocurrio un problema al realizar la operación solicitada.");
            //}

            return _exito;
        }

        public bool Login(string usuario, string pass, TipoUsuario tipo)
        {
            _exito = false;

            if (!Funciones.ValidarCorreo(usuario)) _mensajes.Add("Ingrese un correo válido");
            else
            {
                Usuario oUser = this.Query(f => f.Email == usuario && f.Contrasena == pass && f.Tipo == tipo.ToString()).ToList().FirstOrDefault();

                if (oUser == null)
                    _mensajes.Add("El usuario y/o contraseña es incorrecto");
                else
                    _exito = true;
            }

            return _exito;
        }

        public List<Usuario> GetAllUserRegisterNow(Paginacion oPaginacion)
        {
            List<Usuario> lsUsuario = new List<Usuario>();
            ICriteria criteria = _session.CreateCriteria<Usuario>();
            DateTime oInit = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            DateTime oEnd = oInit.AddDays(1);
            criteria.Add(Restrictions.Ge("FechaAlta", oInit))
            .Add(Restrictions.Lt("FechaAlta", oEnd));
            criteria.SetProjection(Projections.RowCount());

            int _Count = (int)criteria.UniqueResult();
            oPaginacion.TotalRegistros = _Count;

            ICriteria _criteria = _session.CreateCriteria<Usuario>();
            DateTime _oInit = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            DateTime _oEnd = oInit.AddDays(1);
            _criteria.Add(Restrictions.Ge("FechaAlta", oInit))
            .Add(Restrictions.Lt("FechaAlta", oEnd));


            lsUsuario = _criteria.List<Usuario>().Skip(oPaginacion.Pagina * oPaginacion.Cantidad).Take(oPaginacion.Cantidad).ToList();


            return lsUsuario;

        }

        public List<Usuario> GetAllUserRegister(Paginacion oPaginacion)
        {
            List<Usuario> lsUsuario = new List<Usuario>();
            if (oPaginacion == null)
                oPaginacion = new Paginacion();

            ICriteria criteria = _session.CreateCriteria<Usuario>();
            criteria.Add(Restrictions.Not(Restrictions.Eq("Estatus", "INACTIVO")));

            criteria.SetProjection(Projections.RowCount());
            int _Count = (int)criteria.UniqueResult();
            oPaginacion.TotalRegistros = _Count;


            lsUsuario = this.Query(f => f.Estatus != "INACTIVO").Skip(oPaginacion.Pagina * oPaginacion.Cantidad).Take(oPaginacion.Cantidad).ToList();
                
            this._exito = true;
            
            return lsUsuario;
        }
        public int GetContRegisteDay()
        {
            List<Usuario> lsUsuario = new List<Usuario>();
            int _Count = 0;
            ICriteria criteria = _session.CreateCriteria<Usuario>();
            DateTime oInit = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            DateTime oEnd = oInit.AddDays(1);
            criteria.Add(Restrictions.Ge("FechaAlta", oInit))
            .Add(Restrictions.Lt("FechaAlta", oEnd));


            criteria.SetProjection(Projections.RowCount());
            _Count = (int)criteria.UniqueResult();
            this._exito = true;

            return _Count;
        }

        public int GetContRegisteVIP()
        {
            List<Usuario> lsUsuario = new List<Usuario>();
            int _Count = 0;
            ICriteria criteria = _session.CreateCriteria<Usuario>();
            criteria.CreateAlias("oTargeta", "Tarjeta");
            criteria.Add(Restrictions.Eq("Estatus", "ACTIVO"))
                .Add(Restrictions.Eq("Tarjeta.TarjetaID", 4));

            criteria.SetProjection(Projections.RowCount());
            _Count = (int)criteria.UniqueResult();
            this._exito = true;

            return _Count;
        }

        public List<Usuario> GetAllUserRegisterVIP(Paginacion oPaginacion)
        {
            List<Usuario> lsUsuario = new List<Usuario>();
            if (oPaginacion == null)
                oPaginacion = new Paginacion();

            ICriteria criteria = _session.CreateCriteria<Usuario>();
            criteria.CreateAlias("oTargeta", "Tarjeta");
            criteria.Add(Restrictions.Eq("Estatus", "ACTIVO"))
                .Add(Restrictions.Eq("Tarjeta.TarjetaID", 4));

            criteria.SetProjection(Projections.RowCount());
            int _Count = (int)criteria.UniqueResult();
            oPaginacion.TotalRegistros = _Count;

            ICriteria criteria2 = _session.CreateCriteria<Usuario>();
            criteria2.CreateAlias("oTargeta", "Tarjeta");
            criteria2.Add(Restrictions.Eq("Estatus", "ACTIVO"))
                .Add(Restrictions.Eq("Tarjeta.TarjetaID", 4));
            
            lsUsuario = criteria2.List<Usuario>().Skip(oPaginacion.Pagina * oPaginacion.Cantidad).Take(oPaginacion.Cantidad).ToList();

            this._exito = true;

            return lsUsuario;
        }
        public Usuario GetUserCodigo(string Codigo)
        {
            Usuario oUsuario = new Usuario();

            ICriteria criteria = _session.CreateCriteria<Usuario>();
            criteria.Add(Restrictions.Eq("Codigo", Codigo));

            oUsuario = criteria.List<Usuario>().FirstOrDefault();
            //oUsuario = _session.Query<Usuario>().Where(f => f.Codigo == Codigo).FirstOrDefault();
            return oUsuario;
            
        }
        public override Usuario GetById(int id)
        {
            return _session.Get<Usuario>(id);
        }

        public override Usuario GetById(object id)
        {
            return _session.Get<Usuario>(id);
        }

        public override Usuario GetNewEntidad()
        {
            throw new NotImplementedException();
        }

        public override bool Update(Usuario modificado)
        {
            throw new NotImplementedException();
        }
    }
}