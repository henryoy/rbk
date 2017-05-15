using cm.mx.catalogo.Enums;
using cm.mx.catalogo.Model;
using cm.mx.catalogo.Model.ObjectVM;
using cm.mx.catalogo.Model.Rules;
using cm.mx.catalogo.Rules;
using cm.mx.dbCore.Interfaces;
using cm.mx.dbCore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        private PromocionUsuarioRepository rPromoUser;

        private TipoMembresiaRepository rMembresia;
        private SucursalRepository rSucursal;
        private TarjetaRepository rTarjeta;
        private UsuarioRepository rUsuario;
        private NotificacionRepository rNotificacion;
        private PromocionRedimirRepository rPromocionRedimir;
        private TipoInteresRepository rTipoInteres;
        private CampanaRepository rCampana;
        private CamposDistribucionRepository rCampos;
        private DistribucionRepository rDistribucion;
        private UsuarioDispositivoRepository rUsuarioDispositivo;
        private int UsuarioId
        {
            get
            {
                var temp = (string)System.Web.HttpContext.Current.Session["Usuario"];
                if (rUsuario == null) rUsuario = new UsuarioRepository();
                return rUsuario.GetUsuarioID(temp);
            }
        }
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

                            if (oUsuario.Usuarioid > 0)
                                this.CrearNotificacionPromocion(oUsuario.Usuarioid);
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
                    entidad.Usuarioaltaid = UsuarioId;
                    entidad.Fechabaja = Convert.ToDateTime("1900-01-01");
                    entidad.Estado = "ACTIVO";
                    IsTransaction = PromocionVR.ActualizarVR(entidad);
                }
                else
                {
                    entidad.Fechaalta = DateTime.Now;
                    entidad.Estado = "ACTIVO";
                    entidad.Usuarioaltaid = UsuarioId;
                    entidad.Fechabaja = Convert.ToDateTime("1900-01-01");
                    entidad.Vigenciainicial = Convert.ToDateTime("1900-01-01");
                    entidad.Vigenciafinal = Convert.ToDateTime("1900-01-01");
                    IsTransaction = PromocionVR.InsertarVR(entidad);

                }

                if (IsTransaction)
                {
                    oPromocion = rPromocion.GuardarPromocion(entidad);
                    if (oPromocion.Promocionid > 0)
                    {
                        var task = this.CrearNotificacion(oPromocion.Promocionid);
                        //task.Start();
                    }
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
                Tipomembresia obj = null;
                if (entidad.Membresiaid > 0)
                {
                    IsValid = MembresiaVR.ActualizarVR(entidad);
                    Mensajes.AddRange(MembresiaVR.Mensajes);
                    obj = rMembresia.GetById(entidad.Membresiaid);
                    obj.ApartirDe = entidad.ApartirDe;
                    obj.Color = entidad.Color;
                    obj.Estado = entidad.Estado;
                    obj.Hasta = entidad.Hasta;
                    obj.Nombre = entidad.Nombre;
                    obj.NumeroDeVisitas = entidad.NumeroDeVisitas;
                    obj.Porcientodescuento = entidad.Porcientodescuento;
                    obj.UrlImagen = entidad.UrlImagen;
                    obj.UsuarioBaja = entidad.UsuarioBaja;
                    if (entidad.FechaBaja < new DateTime(1900, 01, 01))
                        obj.FechaBaja = new DateTime(1900, 01, 01);
                    else
                        obj.FechaBaja = entidad.FechaBaja;
                }
                else
                {
                    IsValid = MembresiaVR.InsertarVR(entidad);
                    Mensajes.AddRange(MembresiaVR.Mensajes);
                    obj = entidad;
                    obj.Estado = "ACTIVO";
                    obj.UsuarioBaja = 0;
                    obj.FechaBaja = new DateTime(1900, 01, 01);
                }

                TipoMembresiaBR brMembresia = new TipoMembresiaBR();
                if (!brMembresia.Insertar(entidad))
                {
                    IsValid = false;
                    _mensajes.AddRange(brMembresia.Mensajes);
                }

                if (IsValid)
                {
                    oMembresia = rMembresia.GuardarMembresia(obj);
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
                Sucursal obj = null;
                rSucursal = new SucursalRepository();
                bool IsValid = false;
                if (entidad.SucursalID > 0)
                {
                    IsValid = SucursalVR.ActualizarVR(entidad);
                    Mensajes.AddRange(SucursalVR.Mensajes);
                    obj = rSucursal.GetById(entidad.SucursalID);
                    obj.Direccion = entidad.Direccion;
                    obj.Latitud = entidad.Latitud;
                    obj.LinkFacebook = entidad.LinkFacebook;
                    obj.Longitud = entidad.Longitud;
                    obj.Nombre = entidad.Nombre;
                    obj.Estado = entidad.Estado;
                    obj.UsuarioBaja = entidad.UsuarioBaja;
                    if (entidad.FechaBaja < new DateTime(1900, 01, 01))
                        obj.FechaBaja = new DateTime(1900, 01, 01);
                    else
                        obj.FechaBaja = entidad.FechaBaja;
                }
                else
                {
                    IsValid = SucursalVR.InsertarVR(entidad);
                    Mensajes.AddRange(SucursalVR.Mensajes);
                    obj = entidad;
                    obj.Estado = "ACTIVO";
                    obj.UsuarioBaja = 0;
                    obj.FechaBaja = new DateTime(1900, 01, 01);
                }

                if (IsValid)
                {
                    oSucursal = rSucursal.GuardarSucursal(obj);
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

        public bool RegistroVisita(string Usuario, int ClienteID, string Referencia, int SucursalId)
        {
            _exito = false;
            _errores = new List<string>();
            _mensajes = new List<string>();
            try
            {
                rNotificacion = new NotificacionRepository();

                Notificacion oNot = rNotificacion.getByReferencias(1, Referencia);

                if (oNot != null)
                {
                    _errores.Add("La referencia de la nota ya está asociada a otra visita");
                }
                else
                {
                    rUsuario = new UsuarioRepository();
                    _exito = rUsuario.RegistrarVisita(Usuario, ClienteID, Referencia, SucursalId);
                    if (!_exito)
                    {
                        _mensajes.AddRange(rUsuario.Mensajes);
                        _errores.AddRange(rUsuario.Errores);
                    }
                    else
                    {
                        _mensajes.AddRange(rUsuario.Mensajes);
                    }
                    List<Promocion> tskPromocion = new List<Promocion>();

                    Task<List<Promocion>> lsPromocion = this.CrearNotificacionPromocion(ClienteID);
                }

                //tskPromocion = await lsPromocion;
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

        //Guardar Usuario
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

        public async Task CrearNotificacion(int PromocionId)
        {
            Promocion oPromocion = this.GetPromocion(PromocionId);
            Notificacion oNotificacion = new Notificacion();

            oNotificacion.Estatus = "ACTIVO";
            oNotificacion.FechaRegistro = DateTime.Now;
            oNotificacion.Mensaje = oPromocion.Descripcion;
            oNotificacion.NotifiacionID = 0;
            oNotificacion.PromocionID = oPromocion.Promocionid;
            oNotificacion.Referencia = "";
            oNotificacion.Tipo = "PROMOCION";
            oNotificacion.Usuario = new Usuario() { Usuarioid = UsuarioId };
            oNotificacion.UsuarioID = UsuarioId;
            oNotificacion.Vigencia = DateTime.Now;

            rNotificacion = new NotificacionRepository();

            Notificacion _oNotificacion = rNotificacion.GuardarNotificacion(oNotificacion);
            bool _isContinue = false;
            if (_oNotificacion != null && oNotificacion.NotifiacionID > 0)
            {
                _isContinue = true;
            }
            List<Usuario> lsUsuario = new List<Usuario>();
            lsUsuario = this.GetAllUserPromocion(oPromocion.Promocionid);
            if (_isContinue)
            {
                rPromoUser = new PromocionUsuarioRepository();
                foreach (Usuario oUser in lsUsuario)
                {
                    this.GuardarUsuarioPromocion(oPromocion, oUser, rPromoUser);
                }

                //await Task.Run(() => Parallel.ForEach(lsUsuario, oUser => {
                //    this.GuardarUsuarioPromocion(oPromocion, oUser, rPromoUser);
                //}));

            }

        }

        public void CrearNotificacion(Promocion oPromocion, Usuario oUsuario, int SucursalId)
        {
            Notificacion oNotificacion = new Notificacion();

            oNotificacion.Estatus = "ACTIVO";
            oNotificacion.FechaRegistro = DateTime.Now;
            oNotificacion.Mensaje = oPromocion.Descripcion;
            oNotificacion.NotifiacionID = 0;
            oNotificacion.PromocionID = oPromocion.Promocionid;
            oNotificacion.Referencia = "";
            oNotificacion.Tipo = "PROMOCION";
            oNotificacion.Usuario = oUsuario;
            oNotificacion.UsuarioID = oUsuario.Usuarioid;
            oNotificacion.SucursalId = SucursalId;
            oNotificacion.Vigencia = DateTime.Now;

            rNotificacion = new NotificacionRepository();

            Notificacion _oNotificacion = rNotificacion.GuardarNotificacion(oNotificacion);
            bool _isContinue = false;
            if (_oNotificacion != null && oNotificacion.NotifiacionID > 0)
            {
                _isContinue = true;
            }
            List<Usuario> lsUsuario = new List<Usuario>();
            lsUsuario = this.GetAllUserPromocion(oPromocion.Promocionid);
            if (_isContinue)
            {
                rPromoUser = new PromocionUsuarioRepository();
                foreach (Usuario oUser in lsUsuario)
                {
                    this.GuardarUsuarioPromocion(oPromocion, oUser, rPromoUser);
                }
            }

        }

        public bool GuardarUsuarioPromocion(Promocion oPromocion, Usuario oUser, PromocionUsuarioRepository rPromoUser)
        {

            _exito = false;

            try
            {
                PromocionUsuario oPromocionUser = new PromocionUsuario();
                oPromocionUser.PromocionId = oPromocion.Promocionid;
                oPromocionUser.UsuarioId = oUser.Usuarioid;
                oPromocionUser.Estatus = "ACTIVA";
                oPromocionUser.FechaAlta = DateTime.Now;
                oPromocionUser.FechaAplicada = Convert.ToDateTime("1900-01-01");

                rPromoUser.GuardarPromocioUsuario(oPromocionUser);
                _exito = true;
            }
            catch (Exception innerException)
            {
                if (rPromoUser._session.Transaction.IsActive)
                {
                    rPromoUser._session.Transaction.Rollback();
                }
                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                }
                this.Errores.Add(innerException.Message);
            }
            return _exito;
        }

        public List<Usuario> GetAllUserPromocion(int PromocionId)
        {
            List<Usuario> lsUser = new List<Usuario>();

            try
            {
                rUsuario = new UsuarioRepository();
                Promocion oPromocion = this.GetPromocion(PromocionId);
                if (oPromocion != null && oPromocion.Promocionid > 0)
                {
                    if (oPromocion.Promociondetalle != null)
                    {
                        Promociondetalle oPromoDetalle = new Promociondetalle();
                        oPromoDetalle = oPromocion.Promociondetalle.FirstOrDefault();
                        if (oPromoDetalle != null && oPromoDetalle.Promociondetalleid > 0)
                        {
                            switch (oPromoDetalle.Condicion.ToUpper())
                            {
                                case "VISITA":
                                    int vVisita = 0;
                                    string vNivel = string.Empty;
                                    vNivel = oPromocion.Tipomembresia;
                                    vVisita = Convert.ToInt32(oPromoDetalle.Valor1);
                                    lsUser = rUsuario.GetAllUserForVisita(vVisita, vNivel);
                                    break;
                                case "EVENTO":
                                    int eVisita = Convert.ToInt32(oPromoDetalle.Valor1);
                                    string eNivel = oPromocion.Tipomembresia;
                                    lsUser = rUsuario.GetAllUserForVisita(eVisita, eNivel);
                                    break;
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
            }

            return lsUser;
        }

        public List<Notificacion> GetNotficaionesByTipo(string tipo)
        {
            _exito = false;
            _errores = new List<string>();
            _mensajes = new List<string>();
            List<Notificacion> lsNotificaciones = new List<Notificacion>();
            try
            {
                rNotificacion = new NotificacionRepository();
                lsNotificaciones = rNotificacion.GetByTipo(tipo);
                _exito = true;
            }
            catch (Exception ex)
            {
                lsNotificaciones = new List<Notificacion>();
                _exito = false;
                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
            }
            return lsNotificaciones;
        }

        public List<Usuario> GetUsuariosMemb(int visitas, string nivel)
        {
            _exito = false;
            _errores = new List<string>();
            _mensajes = new List<string>();
            List<Usuario> ls = new List<Usuario>();
            try
            {
                rUsuario = new UsuarioRepository();
                ls = rUsuario.GetTipoMemb(visitas, nivel);
                _exito = true;
            }
            catch (Exception ex)
            {
                ls = new List<Usuario>();
                _exito = false;
                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
            }
            return ls;
        }

        public bool GuardarRerenciaNotifiacion(int NotifiacionId, string Folio)
        {
            _exito = false;
            _errores = new List<string>();
            _mensajes = new List<string>();
            try
            {
                rNotificacion = new NotificacionRepository();
                var oNotifiacion = rNotificacion.GetById(NotifiacionId);
                oNotifiacion.FolioNota = Folio;
                var not = rNotificacion.GuardarNotificacion(oNotifiacion);
                _exito = true;
            }
            catch (Exception ex)
            {
                if (rNotificacion._session.Transaction.IsActive) rNotificacion._session.Transaction.Rollback();
                _exito = false;
                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
            }
            return _exito;
        }

        public Notificacion GetNotificacion(int NotifiacionId)
        {
            _exito = false;
            _errores = new List<string>();
            _mensajes = new List<string>();
            Notificacion oNotif = null;
            try
            {
                rNotificacion = new NotificacionRepository();
                oNotif = rNotificacion.GetById(NotifiacionId);
                _exito = true;
            }
            catch (Exception ex)
            {
                if (rNotificacion._session.Transaction.IsActive) rNotificacion._session.Transaction.Rollback();
                _exito = false;
                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
            }

            rNotificacion._session.Clear();
            return oNotif;
        }

        public async Task<List<Promocion>> CrearNotificacionPromocion(int UsuarioId)
        {
            List<Promocion> lsPromocion = new List<Promocion>();

            try
            {
                List<Promocion> _lsPromocion = new List<Promocion>();
                Usuario oUsuario = new Usuario();

                rUsuario = new UsuarioRepository();
                oUsuario = rUsuario.GetById(UsuarioId);
                if (oUsuario != null)
                {
                    rPromocion = new PromocionRepository();
                    if (oUsuario.VisitaActual > 0)
                    {
                        string Tarjeta = string.Empty;
                        if (oUsuario.oTarjeta != null)
                            Tarjeta = oUsuario.oTarjeta.Nombre;
                        else
                        {
                            Tipomembresia oTipo = this.GetMembresia(oUsuario.TarjetaID);
                            if (oTipo != null)
                                Tarjeta = oTipo.Nombre;
                        }

                        if (!string.IsNullOrEmpty(Tarjeta))
                        {
                            lsPromocion = rPromocion.GetPromocionAplyVisita(Tarjeta);
                            foreach (Promocion oPromocion in lsPromocion)
                            {
                                if (oPromocion.Promociondetalle != null && oPromocion.Promociondetalle.Count > 0)
                                {
                                    var DetallePromocion = oPromocion.Promociondetalle.FirstOrDefault();
                                    if (DetallePromocion != null)
                                    {
                                        if (DetallePromocion.Condicion == "VISITA")
                                        {
                                            int Valor1 = 0;
                                            if (!string.IsNullOrEmpty(DetallePromocion.Valor1))
                                            {
                                                Valor1 = Convert.ToInt32(DetallePromocion.Valor1);
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

                    if (_lsPromocion.Count > 0)
                    {
                        foreach (Promocion oPromocion in _lsPromocion)
                        {
                            this.CrearNotificacion(oPromocion, oUsuario, 1);
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
            }

            return lsPromocion;
        }

        public bool RedimirPromocion(RedimirPromocionVM oRedimirPromo)
        {
            _exito = false;
            _mensajes.Clear();
            _errores.Clear();
            bool isExiste = false;
            bool IsSave = false;
            rPromocionRedimir = new PromocionRedimirRepository();
            try
            {
                Promocionredimir oPromocionRedimir = new Promocionredimir();
                oPromocionRedimir.PromocionRedimirId = oRedimirPromo.PromocionRedimirId;
                oPromocionRedimir.UsuarioRedimioId = oRedimirPromo.UsuarioRedimioId;
                oPromocionRedimir.FechaRedimir = DateTime.Now;
                oPromocionRedimir.Promocion = new Promocion()
                {
                    Promocionid = oRedimirPromo.PromocionId
                };

                oPromocionRedimir.Usuario = new Usuario()
                {
                    Usuarioid = oRedimirPromo.UsuarioId
                };

                isExiste = rPromocionRedimir.PromocionIsRedimida(oRedimirPromo.UsuarioId, oRedimirPromo.PromocionId);

                if (!isExiste)
                {
                    IsSave = rPromocionRedimir.Guardar(oPromocionRedimir);
                }
                else
                {
                    _mensajes.Add("La promoción ha sido redimida");
                }

                _exito = true;

            }
            catch (Exception innerException)
            {
                if (rPromocionRedimir._session.Transaction.IsActive)
                {
                    rPromocionRedimir._session.Transaction.Rollback();
                }

                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                }
                this.Errores.Add(innerException.Message);
            }

            return IsSave;
        }

        //<summary>Metodo que devuelve un objeto usuario por medio del correo electronico</summary>
        public Usuario GetUsuario(string usuario)
        {
            _exito = false;
            _mensajes = new List<string>();
            _errores = new List<string>();
            Usuario oUsuario = null;

            try
            {
                rUsuario = new UsuarioRepository();
                oUsuario = rUsuario.Query(a => a.Email == usuario).FirstOrDefault();
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

            rUsuario._session.Clear();

            return oUsuario;
        }

        //<summary>Metodo que devuelve una lista de visitas del usuario que los dio de alto, pasando el id del usuario alta</summary>
        public List<Notificacion> GetVisitasByUsuarioAlta(int UsuatioID)
        {
            _exito = false;
            _mensajes = new List<string>();
            _errores = new List<string>();
            List<Notificacion> lsNotificaiones = null;
            try
            {
                rNotificacion = new NotificacionRepository();
                lsNotificaiones = rNotificacion.Query(a => a.UsuarioAlta == UsuatioID && a.Tipo == TipoNotificacion.VISITA.ToString()).OrderByDescending(x => x.FechaRegistro).Take(10).ToList();
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

        //<summary>Metodo que devuelve una lista de visitas del usuario destino distinguiendo por tipo de visita</summary>
        public List<NotificacionSucursalVM> GetNotifiacionesApp(int UsuatioID, bool visitas = true, bool SoloNoEnviadas = false)
        {
            _exito = false;
            _mensajes = new List<string>();
            _errores = new List<string>();
            List<Notificacion> lsNotificaiones = new List<Notificacion>();
            List<NotificacionSucursalVM> lsNotificacionesSucursal = new List<NotificacionSucursalVM>();
            try
            {

                rSucursal = new SucursalRepository();

                rNotificacion = new NotificacionRepository();

                if (SoloNoEnviadas)
                {
                    lsNotificaiones = rNotificacion.Query(a => a.Enviado == false).ToList();
                }
                else
                {
                    if (visitas)
                    {
                        lsNotificaiones = rNotificacion.Query(a => a.UsuarioID == UsuatioID && a.Tipo == TipoNotificacion.VISITA.ToString()).OrderByDescending(x => x.FechaRegistro).Take(5).ToList();
                    }
                    else
                    {
                        lsNotificaiones = rNotificacion.Query(a => a.UsuarioID == UsuatioID && a.Tipo != TipoNotificacion.VISITA.ToString()).OrderByDescending(x => x.FechaRegistro).Take(5).ToList();
                    }
                }

                foreach (Notificacion oNot in lsNotificaiones)
                {
                    NotificacionSucursalVM oNotSuc = new NotificacionSucursalVM();
                    oNotSuc.Estatus = oNot.Estatus;
                    oNotSuc.FechaRegistro = oNot.FechaRegistro;
                    oNotSuc.FolioNota = oNot.FolioNota;
                    oNotSuc.Mensaje = oNot.Mensaje;
                    oNotSuc.NotifiacionID = oNot.NotifiacionID;
                    oNotSuc.PromocionID = oNot.PromocionID;
                    oNotSuc.Referencia = oNot.Referencia;
                    oNotSuc.SucursalId = oNot.SucursalId;
                    oNotSuc.Tipo = oNot.Tipo;
                    oNotSuc.Usuario = oNot.Usuario;
                    oNotSuc.UsuarioAlta = oNot.UsuarioAlta;
                    oNotSuc.UsuarioID = oNot.UsuarioID;
                    oNotSuc.Vigencia = oNot.Vigencia;
                    oNotSuc.Sucursal = rSucursal.GetById(oNotSuc.SucursalId).Nombre;
                    lsNotificacionesSucursal.Add(oNotSuc);
                }

                _exito = true;
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
            return lsNotificacionesSucursal;
        }

        //<summary>Metodo que devuelve un objeto de tipo usuario</summary>
        public Usuario LoginMovil(string usuario, string password, TipoUsuario tipo)
        {
            UsuarioRepository rUsuario = new UsuarioRepository();
            ActivacionRepository rAcrivacion = new ActivacionRepository();
            Usuario oUsuario = null;

            try
            {
                oUsuario = rUsuario.LoginMovil(usuario, password, tipo);

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

        public bool BajaSucursal(int SucursalId)
        {
            _exito = false;
            _errores = new List<string>();
            _mensajes = new List<string>();
            try
            {
                rSucursal = new SucursalRepository();
                var obj = rSucursal.GetById(SucursalId);
                obj.UsuarioBaja = UsuarioId;
                obj.Estado = "BAJA";
                obj.FechaBaja = DateTime.Now;
                rSucursal.GuardarSucursal(obj);
                _exito = true;
            }
            catch (Exception ex)
            {
                if (rSucursal._session.Transaction.IsActive) rSucursal._session.Transaction.Rollback();
                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
            }
            return _exito;
        }

        public bool BajaMembresia(int MembresiaId)
        {
            _exito = false;
            _errores = new List<string>();
            _mensajes = new List<string>();
            try
            {
                rMembresia = new TipoMembresiaRepository();
                var obj = rMembresia.GetById(MembresiaId);
                obj.UsuarioBaja = UsuarioId;
                obj.Estado = "BAJA";
                obj.FechaBaja = DateTime.Now;
                rMembresia.GuardarMembresia(obj);
                _exito = true;
            }
            catch (Exception ex)
            {
                if (rMembresia._session.Transaction.IsActive) rMembresia._session.Transaction.Rollback();
                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
            }
            return _exito;
        }

        public List<TipoInteres> GetAllTipoIntereses(Paginacion pag = null)
        {
            _exito = false;
            _errores = new List<string>();
            _mensajes = new List<string>();
            List<TipoInteres> ls = new List<TipoInteres>();
            if (pag == null) pag = new Paginacion();
            try
            {
                rTipoInteres = new TipoInteresRepository();
                var r = rTipoInteres.GetAll();
                if (pag.Paginar)
                {
                    pag.TotalRegistros = r.Count();
                    r = r.Skip(pag.Pagina * pag.Cantidad).Take(pag.Cantidad);
                }
                ls = r.ToList();
            }
            catch (Exception ex)
            {
                ls = new List<TipoInteres>();
                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
                _exito = false;
            }
            return ls;
        }


        public Campana GetCampana(int CampanaId)
        {
            Mensajes.Clear();
            Exito = false;
            Errores.Clear();
            Campana _oCampana = new Campana();
            rCampana = new CampanaRepository();
            try
            {
                _oCampana = rCampana.GetById(CampanaId);
                _exito = rCampana.Exito;
            }
            catch (Exception ex)
            {
                if (rCampana._session.Transaction.IsActive)
                {
                    rCampana._session.Transaction.Rollback();
                }
            }
            return _oCampana;
        }

        public List<CamposDistribucion> GetAllCamposDistrubucion(Paginacion pag = null)
        {
            _exito = false;
            _errores = new List<string>();
            _mensajes = new List<string>();
            List<CamposDistribucion> ls = new List<CamposDistribucion>();
            if (pag == null) pag = new Paginacion();
            try
            {
                rCampos = new CamposDistribucionRepository();
                var r = rCampos.GetAll();
                if (pag.Paginar)
                {
                    pag.TotalRegistros = r.Count();
                    r = r.Skip(pag.Pagina * pag.Cantidad).Take(pag.Cantidad);
                }
                ls = r.ToList();
            }
            catch (Exception ex)
            {
                ls = new List<CamposDistribucion>();
                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
                _exito = false;
            }
            return ls;
        }

        public Campana GuardarCampana(Campana oCampana)
        {
            Mensajes.Clear();
            Exito = false;
            Errores.Clear();
            Campana _oCampana = new Campana();
            rCampana = new CampanaRepository();
            try
            {
                bool IsTransaction = false;
                if (oCampana.CampanaId > 0)
                {
                    IsTransaction = CampanaVR.ActualizarVR(oCampana);
                    Mensajes.AddRange(CampanaVR.Mensajes);
                }
                else
                {
                    IsTransaction = CampanaVR.InsertarVR(oCampana);
                    Mensajes.AddRange(CampanaVR.Mensajes);
                }
                if (IsTransaction)
                {
                    _oCampana = rCampana.GuardarCampana(oCampana);
                    _exito = rCampana.Exito;
                }
                else { _oCampana = oCampana; _exito = false; }
            }
            catch (Exception ex)
            {

                if (rCampana._session.Transaction.IsActive)
                {
                    rCampana._session.Transaction.Rollback();
                }
            }
            return _oCampana;
        }

        public bool GuardarDistribucion(Distribucion obj)
        {
            _exito = false;
            _errores = new List<string>();
            _mensajes = new List<string>();
            try
            {
                if (obj.FechaBaja < new DateTime(1900, 01, 01)) obj.FechaBaja = new DateTime(1900, 01, 01);
                rDistribucion = new DistribucionRepository();
                DistribucionVR vrDistribucion = new DistribucionVR();
                if (!vrDistribucion.Insertar(obj))
                {
                    _mensajes.AddRange(vrDistribucion.Mensajes);
                }
                else
                {
                    _exito = rDistribucion.Guardar(obj);
                }
            }
            catch (Exception ex)
            {
                if (rDistribucion._session.Transaction.IsActive) rDistribucion._session.Transaction.Rollback();

                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
                _exito = false;
            }
            return _exito;
        }


        public List<Campana> GetAllCampana(Paginacion oPaginacion)
        {
            List<Campana> lsCampana = new List<Campana>();
            _exito = true;
            Mensajes.Clear();
            Errores.Clear();
            rCampana = new CampanaRepository();
            try
            {

                lsCampana = rCampana.GetAllCampana(oPaginacion);
                _exito = true;
            }
            catch (Exception innerException)
            {
                if (rCampana._session.Transaction.IsActive)
                {
                    rCampana._session.Transaction.Rollback();
                }
                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                }
                this.Errores.Add(innerException.Message);
                throw innerException;
            }
            return lsCampana;
        }

        public Distribucion GetDistribucion(int DistribucionID)
        {
            _exito = false;
            _errores = new List<string>();
            _mensajes = new List<string>();
            Distribucion obj = null;
            try
            {
                rDistribucion = new DistribucionRepository();
                obj = rDistribucion.GetById(DistribucionID);
            }
            catch (Exception ex)
            {
                if (rDistribucion._session.Transaction.IsActive) rDistribucion._session.Transaction.Rollback();

                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
                _exito = false;
            }
            return obj;
        }

        public List<Distribucion> GetDistribucion(Paginacion pag)
        {
            _exito = false;
            _errores = new List<string>();
            _mensajes = new List<string>();
            List<Distribucion> ls = new List<Distribucion>();
            try
            {
                if (pag == null) pag = new Paginacion();
                rDistribucion = new DistribucionRepository();
                ls = rDistribucion.GetAllActivos();

                if (pag.Paginar)
                {
                    pag.TotalRegistros = ls.Count();
                    ls = ls.Skip(pag.Pagina * pag.Cantidad).Take(pag.Cantidad).ToList();
                }
            }
            catch (Exception ex)
            {
                if (rDistribucion._session.Transaction.IsActive) rDistribucion._session.Transaction.Rollback();

                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
                _exito = false;
            }
            return ls;
        }

        public UsuarioDispositivo GetTokenActivoUsuario(int UsuarioId)
        {
            Mensajes.Clear();
            Exito = false;
            Errores.Clear();
            UsuarioDispositivo _oDispositivo = new UsuarioDispositivo();
            rUsuarioDispositivo = new UsuarioDispositivoRepository();
            try
            {
                _oDispositivo = rUsuarioDispositivo.Query(a => a.UsuarioId == UsuarioId && a.Plataforma == Enums.Plataforma.IOS.ToString()).OrderByDescending(a => a.FechaAlta).FirstOrDefault();
                _exito = rCampana.Exito;
            }
            catch (Exception ex)
            {
                if (rUsuarioDispositivo._session.Transaction.IsActive)
                {
                    rUsuarioDispositivo._session.Transaction.Rollback();
                }
            }
            return _oDispositivo;
        }

        public bool ActualizaNotificacion(Notificacion _notificacion)
        {
            bool bResult = false;

            rNotificacion = new NotificacionRepository();

            try
            {
                rNotificacion.GuardarNotificacion(_notificacion);
                bResult = true;
            }
            catch (Exception ex)
            {
                bResult = false;
                if (rNotificacion._session.Transaction.IsActive)
                {
                    rNotificacion._session.Transaction.Rollback();
                }
            }
            return bResult;
        }

        public bool saveToken(int usuarioId, string token, string dispositivo, string serie, string origen)
        {
            _exito = false;

            try
            {
                rUsuarioDispositivo = new UsuarioDispositivoRepository();
                UsuarioDispositivo oUsuarioD = new UsuarioDispositivo();
                oUsuarioD.UsuarioId = usuarioId;
                oUsuarioD.Usuario = new Usuario { Usuarioid = usuarioId };
                oUsuarioD.Token = token;
                oUsuarioD.Dispositivo = dispositivo;
                oUsuarioD.Serie = serie;
                oUsuarioD.Plataforma = origen.ToString();
                oUsuarioD.FechaAlta = DateTime.Now;

                UsuarioDispositivo UsuarioToken = rUsuarioDispositivo.getByUsuarioToken(oUsuarioD);

                if (UsuarioToken == null)
                    rUsuarioDispositivo.GuardarToken(oUsuarioD);
                else
                    _mensajes.Add("Token/Usuario registrado previamente");
                _exito = true;
            }
            catch (Exception ex)
            {
                if (rUsuarioDispositivo._session.Transaction.IsActive)
                {
                    rUsuarioDispositivo._session.Transaction.Rollback();
                }
                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
            }

            return _exito;
        }

        public bool deleteTokens(string usuario, string token, bool EliminarTodos = false)
        {
            _exito = false;

            try
            {
                rUsuarioDispositivo = new UsuarioDispositivoRepository();
                Usuario oUsuario = GetUsuario(usuario);

                rUsuarioDispositivo._session.BeginTransaction();
                //rUsuarioDispositivo.Delete()
                List<UsuarioDispositivo> lDispositivos = rUsuarioDispositivo.Query(x => x.UsuarioId == oUsuario.Usuarioid).ToList();

                foreach (UsuarioDispositivo oUsuarioDis in lDispositivos)
                {
                    rUsuarioDispositivo.Delete(oUsuarioDis);
                }
                rUsuarioDispositivo._session.Transaction.Commit();
            }
            catch (Exception ex)
            {
                if (rUsuarioDispositivo._session.Transaction.IsActive)
                {
                    rUsuarioDispositivo._session.Transaction.Rollback();
                }
                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
            }

            return _exito;
        }

        public bool BajaDistribucion(int DistribucionID)
        {
            _exito = false;
            _mensajes = new List<string>();
            _errores = new List<string>();
            try
            {
                rDistribucion = new DistribucionRepository();
                var oDistribucion = rDistribucion.GetById(DistribucionID);
                oDistribucion.Estado = Estatus.BAJA.ToString();
                oDistribucion.UsuarioBaja = UsuarioId;
                oDistribucion.FechaBaja = DateTime.Now;
                _exito = rDistribucion.Guardar(oDistribucion);
            }
            catch (Exception ex)
            {
                if (rDistribucion._session.Transaction.IsActive)
                {
                    rDistribucion._session.Transaction.Rollback();
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