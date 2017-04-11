using cm.mx.catalogo.Enums;
using cm.mx.catalogo.Model;
using cm.mx.catalogo.Model.Rules;
using cm.mx.catalogo.Rules;
using cm.mx.dbCore.Interfaces;
using cm.mx.dbCore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace cm.mx.catalogo.Controller
{
    public class CatalogoController : IController
    {
        #region propiedades
        private List<string> _errores = new List<string>();
        private List<string> _mensajes = new List<string>();
        private string _mensaje = string.Empty;
        private bool _exito = false;

        private int _totalColumnas = 0;
        public int TotalColumnas
        {
            get { return _totalColumnas; }
            set { _totalColumnas = value; }
        }
        public List<string> Errores
        {
            get { return _errores; }
        }
        public bool Exito
        {
            get
            {
                return _exito;
            }
            set
            {
                _exito = value;
            }
        }
        public string Mensaje
        {
            get
            {
                return _mensaje;
            }
            set
            {
                _mensaje = value;
            }
        }
        public List<string> Mensajes
        {
            get { return _mensajes; }
        }
        #endregion

        #region Repositorios

        private PromocionRepository rPromocion;

        private TipoMembresiaRepository rMembresia;
        private SucursalRepository rSucursal;
        private TarjetaRepository rTarjeta;
        private UsuarioRepository rUsuario;
        private NotificacionRepository rNotificacion;

        #endregion Repositorios

        public bool RegistrarUsuario(Usuario oUsuario)
        {
            Boolean bResult = false;
            Mensajes.Clear();
            Errores.Clear();

            try
            {
                if (oUsuario.Usuarioid == 0)
                {
                    oUsuario.FechaAlta = DateTime.Now;
                    oUsuario.Estatus = Estatus.PENDIENTE.ToString();
                    oUsuario.FechaBaja = new DateTime(1900, 01, 01);
                    oUsuario.oTarjeta.Membresiaid = 1;
                }

                UsuarioVR vrUsaurio = new UsuarioVR();
                if (!vrUsaurio.Insertar(oUsuario))
                {
                    bResult = false;
                    Errores.AddRange(vrUsaurio.Mensajes);
                }
                else
                {
                    rUsuario = new UsuarioRepository();
                    var clave = rUsuario.Save(oUsuario);
                    if (rUsuario.Exito)
                    {
                        bResult = true;

                        ConfiguracionRepository rConfig = new ConfiguracionRepository();
                        Configuracion oConfig = rConfig.GetByClave("URL_ACTIVACION");

                        if (oConfig != null)
                        {
                            var baseUrl = oConfig.Valor;
                            var link = baseUrl + "/Activar.aspx?val1=" + clave + "&val2=" + oUsuario.Usuarioid;

                            if (!rUsuario.EnviarCorreo(new List<string> { oUsuario.Email }, "Activar Cuenta", link, true))
                            {
                                List<string> msjs = rUsuario.Mensajes;
                                Mensajes.AddRange(rUsuario.Errores);
                            }
                        }
                        else
                        {
                            Mensajes.Add("No se pudo realizar el registro ya que no se encontró la URL de activcación del usuario");
                        }
                    }
                    else
                    {
                        bResult = false;
                        Errores.AddRange(rUsuario.Errores);
                    }
                }

            }
            catch (Exception ex)
            {
                Errores.Add(ex.Message);
            }

            return bResult;
        }

        public Usuario getDatosUsuarioByCodigo(string codigo)
        {
            Usuario oUsuario = null;
            rUsuario = new UsuarioRepository();

            try
            {
                oUsuario = rUsuario.getByCodigo(codigo);
                if (oUsuario == null)
                    Errores.Add("No se encontró un cliente con el codigo de tarjeta '" + codigo + "'");

            }
            catch (Exception ex)
            {
                oUsuario = null;
                _errores.Add(ex.Message);
            }

            return oUsuario;
        }

        public Tipomembresia GetProximoNivel(int visitas, int membresiaActual)
        {
            Tipomembresia Resultado = null;

            try
            {
                TipoMembresiaRepository rMembresia = new TipoMembresiaRepository();
                List<Tipomembresia> lMembresias = rMembresia.GetAllActivos();
                List<Tipomembresia> lProximas = new List<Tipomembresia>();

                foreach (Tipomembresia oMem in lMembresias)
                {

                    if (oMem.ApartirDe > visitas)
                    {
                        lProximas.Add(oMem);
                    }

                }

                Resultado = lProximas.OrderBy(x => x.ApartirDe).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Errores.Add(ex.Message);
            }

            return Resultado;
        }

        public Usuario LoginMovil(string usuario, string password)
        {
            UsuarioRepository rUsuario = new UsuarioRepository();
            ActivacionRepository rAcrivacion = new ActivacionRepository();
            Usuario oUsuario = null;

            try
            {
                oUsuario = rUsuario.LoginMovil(usuario, password);

                if (oUsuario == null)
                {
                    Errores.AddRange(rUsuario.Errores);
                }
            }
            catch (Exception ex)
            {
                Errores.Add(ex.Message);
            }

            return oUsuario;
        }

        public bool Login(string correo, string contrasena)
        {
            _exito = false;
            _errores.Clear();
            _mensaje = string.Empty;
            _mensajes.Clear();
            try
            {
                rUsuario = new UsuarioRepository();
                if (rUsuario.Login(correo, contrasena, Enums.TipoUsuario.WEB)) _exito = true;
                else
                {
                    _mensajes.AddRange(rUsuario.Mensajes);
                    _errores.AddRange(rUsuario.Errores);
                    _exito = false;
                }
            }
            catch (Exception innerException)
            {
                if (rTarjeta._session.Transaction.IsActive)
                {
                    rTarjeta._session.Transaction.Rollback();
                }
                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                }
                this.Errores.Add(innerException.Message);
                throw innerException;
            }
            return _exito;
        }

        public Promocion GetPromocion(int PromocionId)
        {
            Promocion oPromocion = new Promocion();
            _exito = false;
            _errores.Clear();
            _mensaje = string.Empty;
            _mensajes.Clear();
            try
            {
                rPromocion = new PromocionRepository();
                oPromocion = rPromocion.GetById(PromocionId);
                _exito = true;
            }
            catch (Exception innerException)
            {
                if (rPromocion._session.Transaction.IsActive)
                {
                    rPromocion._session.Transaction.Rollback();
                }
                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                }
                this.Errores.Add(innerException.Message);
                throw innerException;
            }
            return oPromocion;
        }

        public Promocion GuardarPromocion(Promocion entidad)
        {
            Promocion oPromocion = new Promocion();
            _exito = false;
            _errores.Clear();
            _mensaje = string.Empty;
            _mensajes.Clear();
            try
            {
                rPromocion = new PromocionRepository();
                bool IsTransaction = false;

                if (entidad.Promocionid > 0)
                {
                    entidad.Fechaalta = DateTime.Now;
                    entidad.Usuarioaltaid = 1;
                    entidad.Fechabaja = Convert.ToDateTime("1900-01-01");
                    entidad.Estado = "ACTIVO";
                    IsTransaction = PromocionVR.ActualizarVR(entidad);
                }
                else
                {
                    entidad.Fechaalta = DateTime.Now;
                    entidad.Estado = "ACTIVO";
                    entidad.Usuarioaltaid = 1;
                    entidad.Fechabaja = Convert.ToDateTime("1900-01-01");
                    entidad.Vigenciainicial = Convert.ToDateTime("1900-01-01");
                    entidad.Vigenciafinal = Convert.ToDateTime("1900-01-01");
                    IsTransaction = PromocionVR.InsertarVR(entidad);

                }

                if (IsTransaction)
                {
                    oPromocion = rPromocion.GuardarPromocion(entidad);

                    _exito = true;
                }
                else Mensajes.AddRange(PromocionVR.Mensajes);


            }
            catch (Exception innerException)
            {
                if (rPromocion._session.Transaction.IsActive)
                {
                    rPromocion._session.Transaction.Rollback();
                }
                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                }
                this.Errores.Add(innerException.Message);
                //throw innerException;
            }
            return oPromocion;
        }

        public List<Promocion> GetAllPromocion()
        {
            List<Promocion> lsPromocion = new List<Promocion>();
            _exito = false;
            _errores.Clear();
            _mensaje = string.Empty;
            _mensajes.Clear();
            try
            {
                rPromocion = new PromocionRepository();
                lsPromocion = rPromocion.GetAllPromocion();
                _exito = true;
            }
            catch (Exception innerException)
            {
                if (rPromocion._session.Transaction.IsActive)
                {
                    rPromocion._session.Transaction.Rollback();
                }
                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                }
                this.Errores.Add(innerException.Message);
                throw innerException;
            }
            return lsPromocion;
        }
        public List<Promocion> GetAllPromocion(Paginacion oPaginacion)
        {
            List<Promocion> lsPromocion = new List<Promocion>();
            _exito = false;
            _errores.Clear();
            _mensaje = string.Empty;
            _mensajes.Clear();
            try
            {
                rPromocion = new PromocionRepository();
                lsPromocion = rPromocion.GetAllPromocion(oPaginacion);
                _exito = true;
            }
            catch (Exception innerException)
            {
                if (rPromocion._session.Transaction.IsActive)
                {
                    rPromocion._session.Transaction.Rollback();
                }
                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                }
                this.Errores.Add(innerException.Message);
                throw innerException;
            }
            return lsPromocion;
        }

        public bool EliminarPromocion(int PromocionId)
        {
            bool isdelete = false;
            _exito = false;
            _errores.Clear();
            _mensaje = string.Empty;
            _mensajes.Clear();
            try
            {
                rPromocion = new PromocionRepository();
                isdelete = rPromocion.EliminarPromocion(PromocionId);
                _exito = true;
            }
            catch (Exception innerException)
            {
                if (rPromocion._session.Transaction.IsActive)
                {
                    rPromocion._session.Transaction.Rollback();
                }
                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                }
                this.Errores.Add(innerException.Message);
                throw innerException;
            }
            return isdelete;
        }

        public Tipomembresia GetMembresia(int MembresiaId)
        {
            Tipomembresia oMembresia = new Tipomembresia();
            _exito = false;
            _errores.Clear();
            _mensaje = string.Empty;
            _mensajes.Clear();
            try
            {
                TipoMembresiaRepository rMembresia = new TipoMembresiaRepository();
                oMembresia = rMembresia.GetById(MembresiaId);
                _exito = true;
            }
            catch (Exception innerException)
            {
                if (rMembresia._session.Transaction.IsActive)
                {
                    rMembresia._session.Transaction.Rollback();
                }
                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                }
                this.Errores.Add(innerException.Message);
                throw innerException;
            }
            return oMembresia;
        }

        public Tipomembresia GuardarMembresia(Tipomembresia entidad)
        {
            Tipomembresia oMembresia = new Tipomembresia();
            _exito = false;
            _errores.Clear();
            _mensaje = string.Empty;
            _mensajes.Clear();
            try
            {
                rMembresia = new TipoMembresiaRepository();
                bool IsValid = false;
                if (entidad.Membresiaid > 0)
                {
                    IsValid = MembresiaVR.ActualizarVR(entidad);
                    Mensajes.AddRange(MembresiaVR.Mensajes);
                }
                else
                {
                    IsValid = MembresiaVR.InsertarVR(entidad);
                    Mensajes.AddRange(MembresiaVR.Mensajes);
                }

                TipoMembresiaBR brMembresia = new TipoMembresiaBR();
                if (!brMembresia.Insertar(entidad))
                {
                    IsValid = false;
                    _mensajes.AddRange(brMembresia.Mensajes);
                }

                if (IsValid)
                {
                    oMembresia = rMembresia.GuardarMembresia(entidad);
                    _exito = true;
                }
            }
            catch (Exception innerException)
            {
                if (rMembresia._session.Transaction.IsActive)
                {
                    rMembresia._session.Transaction.Rollback();
                }
                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                }
                this.Errores.Add(innerException.Message);
                throw innerException;
            }
            return oMembresia;
        }

        public List<Tipomembresia> GetAllMembresias()
        {
            List<Tipomembresia> lsMembresia = new List<Tipomembresia>();
            _exito = false;
            _errores.Clear();
            _mensaje = string.Empty;
            _mensajes.Clear();
            try
            {
                rMembresia = new TipoMembresiaRepository();
                lsMembresia = rMembresia.GetAllActivos();
                _exito = true;
            }
            catch (Exception innerException)
            {
                if (rMembresia._session.Transaction.IsActive)
                {
                    rMembresia._session.Transaction.Rollback();
                }
                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                }
                this.Errores.Add(innerException.Message);
                throw innerException;
            }
            return lsMembresia;
        }

        public Sucursal GetSucursal(int SucursalId)
        {
            Sucursal oSucursal = new Sucursal();
            _exito = false;
            _errores.Clear();
            _mensaje = string.Empty;
            _mensajes.Clear();
            try
            {
                SucursalRepository rSucursal = new SucursalRepository();
                oSucursal = rSucursal.GetById(SucursalId);
                _exito = true;
            }
            catch (Exception innerException)
            {
                if (rSucursal._session.Transaction.IsActive)
                {
                    rSucursal._session.Transaction.Rollback();
                }
                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                }
                this.Errores.Add(innerException.Message);
                throw innerException;
            }
            return oSucursal;
        }

        public Sucursal GuardarSucursal(Sucursal entidad)
        {
            Sucursal oSucursal = new Sucursal();
            _exito = false;
            _errores.Clear();
            _mensaje = string.Empty;
            _mensajes.Clear();
            try
            {
                rSucursal = new SucursalRepository();
                bool IsValid = false;
                if (entidad.SucursalID > 0)
                {
                    IsValid = SucursalVR.ActualizarVR(entidad);
                    Mensajes.AddRange(SucursalVR.Mensajes);
                }
                else
                {
                    IsValid = SucursalVR.InsertarVR(entidad);
                    Mensajes.AddRange(SucursalVR.Mensajes);
                }

                if (IsValid)
                {
                    oSucursal = rSucursal.GuardarSucursal(entidad);
                    _exito = true;
                }
            }
            catch (Exception innerException)
            {
                if (rMembresia._session.Transaction.IsActive)
                {
                    rMembresia._session.Transaction.Rollback();
                }
                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                }
                this.Errores.Add(innerException.Message);
                throw innerException;
            }
            return oSucursal;
        }

        public List<Sucursal> GetAllSucursales()
        {
            List<Sucursal> lsSucursal = new List<Sucursal>();
            _exito = false;
            _errores.Clear();
            _mensaje = string.Empty;
            _mensajes.Clear();
            try
            {
                rSucursal = new SucursalRepository();
                lsSucursal = rSucursal.GetAllActivos();
                _exito = true;
            }
            catch (Exception innerException)
            {
                if (rSucursal._session.Transaction.IsActive)
                {
                    rSucursal._session.Transaction.Rollback();
                }
                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                }
                this.Errores.Add(innerException.Message);
                throw innerException;
            }
            return lsSucursal;
        }

        public Tarjeta GetTarjeta(int TarjetaId)
        {
            Tarjeta oTarjeta = new Tarjeta();
            _exito = false;
            _errores.Clear();
            _mensaje = string.Empty;
            _mensajes.Clear();
            try
            {
                TarjetaRepository rTarjeta = new TarjetaRepository();
                oTarjeta = rTarjeta.GetById(TarjetaId);
                _exito = true;
            }
            catch (Exception innerException)
            {
                if (rTarjeta._session.Transaction.IsActive)
                {
                    rTarjeta._session.Transaction.Rollback();
                }
                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                }
                this.Errores.Add(innerException.Message);
                throw innerException;
            }
            return oTarjeta;
        }

        public Tarjeta GuardarTarjeta(Tarjeta entidad)
        {
            Tarjeta oTarjeta = new Tarjeta();
            _exito = false;
            _errores.Clear();
            _mensaje = string.Empty;
            _mensajes.Clear();
            try
            {
                rTarjeta = new TarjetaRepository();
                oTarjeta = rTarjeta.GuardarTarjeta(entidad);
                _exito = true;
            }
            catch (Exception innerException)
            {
                if (rMembresia._session.Transaction.IsActive)
                {
                    rMembresia._session.Transaction.Rollback();
                }
                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                }
                this.Errores.Add(innerException.Message);
                throw innerException;
            }
            return oTarjeta;
        }

        public List<Tarjeta> GetAllTarjetas()
        {
            List<Tarjeta> lsTarjeta = new List<Tarjeta>();
            _exito = false;
            _errores.Clear();
            _mensaje = string.Empty;
            _mensajes.Clear();
            try
            {
                rTarjeta = new TarjetaRepository();
                lsTarjeta = rTarjeta.GetAllActivos();
                _exito = true;
            }
            catch (Exception innerException)
            {
                if (rTarjeta._session.Transaction.IsActive)
                {
                    rTarjeta._session.Transaction.Rollback();
                }
                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                }
                this.Errores.Add(innerException.Message);
                throw innerException;
            }
            return lsTarjeta;
        }

        public List<Usuario> GetAllUsuariosRegistroHoy(Paginacion oPaginacion)
        {
            List<Usuario> lsUsuario = new List<Usuario>();
            _exito = true;
            Mensajes.Clear();
            Errores.Clear();
            try
            {
                rUsuario = new UsuarioRepository();
                lsUsuario = rUsuario.GetAllUserRegisterNow(oPaginacion);
                _exito = true;
            }
            catch (Exception innerException)
            {
                if (rUsuario._session.Transaction.IsActive)
                {
                    rUsuario._session.Transaction.Rollback();
                }
                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                }
                this.Errores.Add(innerException.Message);
                throw innerException;
            }
            return lsUsuario;
        }
        public List<Usuario> GetAllUsuariosRegistro(Paginacion oPaginacion)
        {
            List<Usuario> lsUsuario = new List<Usuario>();
            _exito = true;
            Mensajes.Clear();
            Errores.Clear();
            try
            {
                rUsuario = new UsuarioRepository();
                lsUsuario = rUsuario.GetAllUserRegister(oPaginacion);
                _exito = true;
            }
            catch (Exception innerException)
            {
                if (rUsuario._session.Transaction.IsActive)
                {
                    rUsuario._session.Transaction.Rollback();
                }
                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                }
                this.Errores.Add(innerException.Message);
                throw innerException;
            }
            return lsUsuario;
        }

        public int GetContRegisteDay()
        {
            int _Count = 0;
            _exito = true;
            Mensajes.Clear();
            Errores.Clear();
            try
            {
                rUsuario = new UsuarioRepository();
                _Count = rUsuario.GetContRegisteDay();
                _exito = true;
            }
            catch (Exception innerException)
            {
                if (rUsuario._session.Transaction.IsActive)
                {
                    rUsuario._session.Transaction.Rollback();
                }
                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                }
                this.Errores.Add(innerException.Message);
                throw innerException;
            }
            return _Count;
        }

        public List<Usuario> GetAllUserRegisterVIP(Paginacion oPaginacion)
        {
            List<Usuario> lsUsuario = new List<Usuario>();
            _exito = true;
            Mensajes.Clear();
            Errores.Clear();
            try
            {
                rUsuario = new UsuarioRepository();
                lsUsuario = rUsuario.GetAllUserRegisterVIP(oPaginacion);
                _exito = true;
            }
            catch (Exception innerException)
            {
                if (rUsuario._session.Transaction.IsActive)
                {
                    rUsuario._session.Transaction.Rollback();
                }
                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                }
                this.Errores.Add(innerException.Message);
                throw innerException;
            }
            return lsUsuario;
        }

        public int GetContRegisteVIP()
        {
            int _Count = 0;
            _exito = true;
            Mensajes.Clear();
            Errores.Clear();
            try
            {
                rUsuario = new UsuarioRepository();
                _Count = rUsuario.GetContRegisteVIP();
                _exito = true;
            }
            catch (Exception innerException)
            {
                if (rUsuario._session.Transaction.IsActive)
                {
                    rUsuario._session.Transaction.Rollback();
                }
                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                }
                this.Errores.Add(innerException.Message);
                throw innerException;
            }
            return _Count;
        }

        public List<Promocion> GetAllPromocionAplicable(string Codigo)
        {
            List<Promocion> lsPromocion = new List<Promocion>();

            try
            {
                List<Promocion> _lsPromocion = new List<Promocion>();
                Usuario oUsuario = new Usuario();
                rUsuario = new UsuarioRepository();
                oUsuario = rUsuario.GetUserCodigo(Codigo);
                if (oUsuario != null)
                {
                    rPromocion = new PromocionRepository();
                    if (oUsuario.VisitaActual > 0)
                    {
                        lsPromocion = rPromocion.GetPromocionAply(oUsuario);
                        foreach (Promocion oPromocion in lsPromocion)
                        {
                            if (oPromocion.Promociondetalle != null && oPromocion.Promociondetalle.Count > 0)
                            {
                                foreach (Promociondetalle oPromocionDetalle in oPromocion.Promociondetalle)
                                {
                                    if (oPromocionDetalle.Condicion == "VISITA")
                                    {
                                        int Valor1 = 0;
                                        if (!string.IsNullOrEmpty(oPromocionDetalle.Valor1))
                                        {
                                            Valor1 = Convert.ToInt32(oPromocionDetalle.Valor1);
                                        }
                                        if (oUsuario.VisitaActual >= Valor1)
                                            _lsPromocion.Add(oPromocion);
                                    }
                                    else
                                        _lsPromocion.Add(oPromocion);
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception innerException)
            {
                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                }
                this.Errores.Add(innerException.Message);
                throw innerException;
            }

            return lsPromocion;
        }

        public List<Notificacion> GetNotifiaciones(int UsuatioID)
        {
            _exito = false;
            _mensajes = new List<string>();
            _errores = new List<string>();
            List<Notificacion> lsNotificaiones = new List<Notificacion>();
            try
            {
                rNotificacion = new NotificacionRepository();
                lsNotificaiones = rNotificacion.Query(a => a.UsuarioID == UsuatioID).ToList();
            }
            catch (Exception ex)
            {
                if (rNotificacion._session.Transaction.IsActive)
                {
                    rNotificacion._session.Transaction.Rollback();
                }
                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
            }
            return lsNotificaiones;
        }

        public bool RegistroVisita(int UsuarioID)
        {
            _exito = false;
            _errores = new List<string>();
            _mensajes = new List<string>();
            try
            {
                rUsuario = new UsuarioRepository();
                _exito = rUsuario.RegistrarVisiata(UsuarioID);
                if (!_exito)
                {
                    _mensajes.AddRange(rUsuario.Mensajes);
                    _errores.AddRange(rUsuario.Errores);
                }
            }
            catch (Exception ex)
            {
                if (rUsuario._session.Transaction.IsActive)
                {
                    rUsuario._session.Transaction.Rollback();
                }
                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
            }
            return _exito;
        }

        //Método de prueba
        public bool GuardarUsuario(Usuario obj)
        {
            _exito = false;
            _errores = new List<string>();
            _mensajes = new List<string>();
            try
            {
                rUsuario = new UsuarioRepository();
                _exito = rUsuario.Guardar(obj);
                if (!_exito)
                {
                    _mensajes.AddRange(rUsuario.Mensajes);
                    _errores.AddRange(rUsuario.Errores);
                }
            }
            catch (Exception ex)
            {
                if (rUsuario._session.Transaction.IsActive)
                {
                    rUsuario._session.Transaction.Rollback();
                }
                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
            }
            return _exito;
        }
    }
}