﻿using cm.mx.catalogo.Enums;
using cm.mx.dbCore.Clases;
using cm.mx.dbCore.Tools;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using System.Net.Mail;
using System.Text;
using System.Net;

namespace cm.mx.catalogo.Model
{
    internal class UsuarioRepository : RepositoryBase<Usuario>
    {
        public bool EnviarCorreo(List<string> para, string asunto, string mensaje, bool eshtml = false)
        {
            _exito = false;
            _errores = new List<string>();
            _mensajes = new List<string>();
            try
            {
                ConfigSMTP oConfsmtp = new ConfigSMTP
                {
                    ConfigSMTPID = 0,
                    EnableSSl = true,
                    Host = "smtp.gmail.com",
                    Password = "Muna/2017",
                    Port = 587,
                    UseDefCred = true,
                    Usuario = "elieupa.desarrollo@gmail.com"
                };

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(oConfsmtp.Usuario);
                para.ForEach(a => mail.To.Add(a));
                mail.Subject = asunto;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.Body = mensaje;
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = eshtml;
                mail.Priority = MailPriority.High;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = oConfsmtp.Host;
                smtp.EnableSsl = oConfsmtp.EnableSSl;
                smtp.UseDefaultCredentials = oConfsmtp.UseDefCred;
                smtp.Credentials = new NetworkCredential(oConfsmtp.Usuario, oConfsmtp.Password);
                smtp.Port = oConfsmtp.Port;

                smtp.Send(mail);
                _exito = true;
            }
            catch (Exception ex)
            {
                _exito = false;
                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
                _mensajes.Add("Ocurrio un problema al enviar el correo");
            }
            return _exito;
        }

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

        public Usuario LoginMovil(string usuario, string pass)
        {
            Usuario oUsuario = null;

            if (!Funciones.ValidarCorreo(usuario)) _errores.Add("Ingrese un correo válido");
            else
            {
                oUsuario = this.Query(f => f.Email == usuario && f.Contrasena == pass).ToList().FirstOrDefault();

                if (oUsuario == null)
                    _errores.Add("El usuario y/o contraseña es incorrecto");
            }

            return oUsuario;
        }

        public Usuario getByCodigo(string codigo)
        {
            return _session.Query<Usuario>().Where(x => x.Codigo == codigo).FirstOrDefault();
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
        public bool RegistrarVisiata(string Usuario, int ClienteID, string Referencia, int SucursalId)
        {
            _exito = false;
            _session.Clear();
            var oUsuario = _session.Get<Usuario>(ClienteID);
            if (oUsuario == null || oUsuario.Estatus == Estatus.BAJA.ToString() || oUsuario.Estatus == Estatus.INACTIVO.ToString())
            {
                _exito = false;
                _mensajes.Add("Usuario no válido");
            }
            else
            {
                oUsuario.VisitaActual += 1;
                oUsuario.VisitaGlobal += 1;

                if (string.IsNullOrEmpty(oUsuario.Codigo)) oUsuario.Codigo = "";
                _session.BeginTransaction();
                var emp = _session.CreateCriteria<Usuario>().Add(Restrictions.Eq("Email", Usuario).IgnoreCase()).List<Usuario>().FirstOrDefault();
                if (emp == null) emp = new Usuario();

                Notificacion oNotifiacion = new Notificacion
                {
                    Estatus = Estatus.ACTIVO.ToString(),
                    FechaRegistro = DateTime.Now,
                    Mensaje = "Se ha registrado una nueva visita",
                    NotifiacionID = 0,
                    PromocionID = 0,
                    Tipo = TipoNotificacion.VISITA.ToString(),
                    UsuarioID = oUsuario.Usuarioid,
                    Vigencia = DateTime.Now.AddDays(5),
                    UsuarioAlta = emp.Usuarioid,
                    Referencia = Referencia,
                    SucursalId = SucursalId
                };
                oUsuario.AddNotifiacion(oNotifiacion);
                //_session.SaveOrUpdate(oNotifiacion);
                var tm = _session.Query<Tipomembresia>().FirstOrDefault(a => oUsuario.VisitaActual >= a.ApartirDe && oUsuario.VisitaActual <= a.Hasta);
                if (tm != null && tm.ApartirDe == oUsuario.VisitaActual)
                {
                    oNotifiacion = new Notificacion
                    {
                        Estatus = Estatus.ACTIVO.ToString(),
                        FechaRegistro = DateTime.Now,
                        Mensaje = "Nueva membresia alcanzado",
                        NotifiacionID = 0,
                        PromocionID = 0,
                        Tipo = TipoNotificacion.VISITA.ToString(),
                        UsuarioID = oUsuario.Usuarioid,
                        Vigencia = DateTime.Now.AddDays(5),
                        UsuarioAlta = emp.Usuarioid,
                        Referencia = Referencia,
                        SucursalId = SucursalId
                    };
                    //_session.SaveOrUpdate(oNotifiacion);
                    oUsuario.AddNotifiacion(oNotifiacion);
                    oUsuario.oTarjeta = tm;
                }
                _session.SaveOrUpdate(oUsuario);
                _session.Transaction.Commit();
                _exito = true;
            }
            return _exito;
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

        public bool Guardar(Usuario obj)
        {
            _exito = false;
            _session.Clear();
            _session.BeginTransaction();
            _session.SaveOrUpdate(obj);
            _session.Transaction.Commit();
            _exito = true;
            return _exito;
        }

        public bool ExisteCorreo(string correo)
        {
            _exito = false;
            Mensajes.Clear();
            Errores.Clear();
            try
            {
                _exito = _session.Query<Usuario>().Any(a => a.Email.Equals(correo));
            }
            catch (Exception ex)
            {
                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
                _mensajes.Add("Ocurrio un problema al realizar la operación solicitada.");
            }
            return _exito;
        }

        public List<Usuario> GetTipoMemb(int cant, string nivel)
        {
            List<Usuario> ls = new List<Usuario>();
            ls = _session.CreateCriteria<Usuario>().Add(Restrictions.Ge("VisitaActual", cant)).CreateCriteria("oTarjeta").Add(Restrictions.Eq("Nombre", nivel)).List<Usuario>().Distinct().ToList();
            return ls;
        }
    }
}