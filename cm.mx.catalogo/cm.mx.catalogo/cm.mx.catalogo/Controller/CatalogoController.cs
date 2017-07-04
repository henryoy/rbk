using cm.mx.catalogo.Enums;
using cm.mx.catalogo.Model;
using cm.mx.catalogo.Model.ObjectVM;
using cm.mx.catalogo.Model.Rules;
using cm.mx.catalogo.Rules;
using cm.mx.dbCore.Interfaces;
using cm.mx.dbCore.Tools;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private ConfiguracionRepository rConfiguracion;

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

            bool NuevoUsuario = true;

            Usuario oUsuarioSaved = null;

            try
            {
                if (oUsuario.Usuarioid == 0)
                {
                    oUsuario.FechaAlta = DateTime.Now;
                    if (oUsuario.Origen == "FACEBOOK" || oUsuario.Origen == "INSTAGRAM")
                        oUsuario.Estatus = Estatus.ACTIVO.ToString();
                    else
                        oUsuario.Estatus = Estatus.PENDIENTE.ToString();
                    oUsuario.FechaBaja = new DateTime(1900, 01, 01);
                    oUsuario.oTarjeta.Membresiaid = 1;
                }

                if (oUsuario.Origen == "FACEBOOK" || oUsuario.Origen == "INSTAGRAM")
                {
                    oUsuarioSaved = GetUsuarioByIDExterno(oUsuario.IdExterno);
                    if (oUsuarioSaved != null)
                    {
                        oUsuarioSaved.Nombre = oUsuario.Nombre;
                        NuevoUsuario = false;
                    }
                }

                UsuarioVR vrUsaurio = new UsuarioVR();
                bool res = false;
                if (oUsuarioSaved == null)
                    res = vrUsaurio.Insertar(oUsuario);
                else
                    res = true;

                if (!res)
                {
                    bResult = false;
                    Errores.AddRange(vrUsaurio.Mensajes);
                }
                else
                {
                    rUsuario = new UsuarioRepository();
                    string clave = "";

                    if (NuevoUsuario)
                        rUsuario.SaveUsuario(oUsuario);
                    else
                        rUsuario.SaveUsuario(oUsuarioSaved);

                    if (rUsuario.Exito)
                    {

                        Mensajes.Add("Se realizo el registro correctamente del usuario.");
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

        public Usuario getUsuarioById(int UsuarioId)
        {
            _exito = true;
            _mensajes.Clear();
            Usuario oUsuario = new Usuario();
            try
            {
                rUsuario = new UsuarioRepository();
                oUsuario = rUsuario.GetById(UsuarioId);
                rUsuario._session.Clear();
                _exito = true;
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
                    if (entidad.Vigenciainicial.HasValue && entidad.Vigenciainicial.Value.Year == 0)
                        entidad.Vigenciainicial = Convert.ToDateTime("1900-01-01");

                    if (entidad.Vigenciafinal.HasValue && entidad.Vigenciafinal.Value.Year == 0)
                        entidad.Vigenciafinal = Convert.ToDateTime("1900-01-01");

                    IsTransaction = PromocionVR.InsertarVR(entidad);

                }

                if (IsTransaction)
                {
                    oPromocion = rPromocion.GuardarPromocion(entidad);
                    //if (oPromocion.Promocionid > 0)
                    //{
                    //var task = this.CrearNotificacion(oPromocion.Promocionid);
                    //task.Start();
                    //}
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
                    obj.ColorLetra = entidad.ColorLetra;
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
                if (!brMembresia.Insertar(obj))
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
                                if (oPromocion.Promociondetalle.Any(f => f.Condicion == "VISITA"))
                                {
                                    if (oPromocion.Promociondetalle.Any(f => f.Condicion == "IMPORTE"))
                                    {
                                        var ProImporte = oPromocion.Promociondetalle.Where(f => f.Condicion == "IMPORTE").ToList();
                                        ProImporte.ForEach(f =>
                                        {
                                            double Valor1 = 0;
                                            double Valor2 = 0;

                                            double.TryParse(f.Valor1, out Valor1);
                                            double.TryParse(f.Valor2, out Valor2);


                                            if (oUsuario.ImporteActual >= Valor1 && oUsuario.ImporteActual <= Valor2)
                                            {
                                                _lsPromocion.Add(oPromocion);
                                                _exito = true;
                                            }

                                        });
                                    }
                                }
                                else _lsPromocion.Add(oPromocion);

                                //foreach (Promociondetalle oPromocionDetalle in oPromocion.Promociondetalle)
                                //{
                                //    if (oPromocionDetalle.Condicion == "VISITA")
                                //    {
                                //        int Valor1 = 0;
                                //        if (!string.IsNullOrEmpty(oPromocionDetalle.Valor1))
                                //        {
                                //            Valor1 = Convert.ToInt32(oPromocionDetalle.Valor1);
                                //        }
                                //        if (oUsuario.VisitaActual == Valor1)
                                //            _lsPromocion.Add(oPromocion);
                                //    }
                                //    else
                                //        _lsPromocion.Add(oPromocion);
                                //}
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

        public bool RegistroVisita(int Usuario, int ClienteID, string Referencia, int SucursalId)
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
                    rConfiguracion = new ConfiguracionRepository();
                    Configuracion oConfig = rConfiguracion.GetByClave("NVISITAS_DIA");
                    bool ValidaNoVisitas = true;
                    bool ValidaDiffVisitas = true;

                    if (oConfig != null)
                    {
                        int nVisitas = rNotificacion.getNumeroVisitasDelDia(ClienteID);
                        if (nVisitas >= int.Parse(oConfig.Valor))
                        {
                            ValidaNoVisitas = false;
                            Errores.Add("El usuario ya ha registrado las visitas maximas diarias permitidas. (" + oConfig.Valor + " VISITAS)");
                        }
                    }

                    if (ValidaNoVisitas)
                    {
                        oConfig = rConfiguracion.GetByClave("NHORAS_DIF");
                        if (oConfig != null)
                        {
                            oNot = rNotificacion.getUltimaVisita(ClienteID);
                            if (oNot != null)
                            {
                                double diff = (DateTime.Now - oNot.FechaRegistro).TotalHours;
                                if (diff < double.Parse(oConfig.Valor))
                                {
                                    ValidaDiffVisitas = false;
                                    Errores.Add("La diferencia entre registro de visitas es muy corta (Debe Haber Diff de " + oConfig.Valor + " HRS)");
                                }
                            }
                        }
                    }

                    List<int> Notificacion = new List<int>();
                    rUsuario = new UsuarioRepository();

                    if (ValidaDiffVisitas && ValidaNoVisitas)
                    {
                        _exito = rUsuario.RegistrarVisita(Usuario, ClienteID, Referencia, SucursalId, out Notificacion);
                        if (!_exito)
                        {
                            _mensajes.AddRange(rUsuario.Mensajes);
                            _errores.AddRange(rUsuario.Errores);
                        }
                        else
                        {
                            if (Notificacion.Count > 0)
                            {
                                new Task(() => EnviaNotificacion(Notificacion)).Start();
                            }

                            Mensajes.AddRange(rUsuario.Mensajes);
                        }
                        List<Promocion> tskPromocion = new List<Promocion>();
                        Task<List<Promocion>> lsPromocion = this.CrearNotificacionPromocion(ClienteID);
                    }
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

        //Guardar Usuario
        public bool GuardarUsuario(Usuario obj)
        {
            _exito = false;
            _errores = new List<string>();
            _mensajes = new List<string>();
            try
            {
                UsuarioVR vrUsaurio = new UsuarioVR();
                if (!vrUsaurio.Guardar(obj)) _mensajes.AddRange(vrUsaurio.Mensajes);
                else
                {
                    rUsuario = new UsuarioRepository();
                    _exito = rUsuario.Guardar(obj);
                    if (!_exito)
                    {
                        _mensajes.AddRange(rUsuario.Mensajes);
                        _errores.AddRange(rUsuario.Errores);
                    }
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
            oNotificacion.NotificacionID = 0;
            oNotificacion.PromocionID = oPromocion.Promocionid;
            oNotificacion.Referencia = "";
            oNotificacion.Tipo = "PROMOCION";
            oNotificacion.Usuario = new Usuario() { Usuarioid = UsuarioId };
            oNotificacion.UsuarioID = UsuarioId;
            oNotificacion.Vigencia = DateTime.Now;

            rNotificacion = new NotificacionRepository();

            Notificacion _oNotificacion = rNotificacion.GuardarNotificacion(oNotificacion);
            bool _isContinue = false;
            if (_oNotificacion != null && oNotificacion.NotificacionID > 0)
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
            oNotificacion.NotificacionID = 0;
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
            if (_oNotificacion != null && oNotificacion.NotificacionID > 0)
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
            List<Usuario> _lsUser = new List<Usuario>();

            try
            {
                List<Usuario> lsUser = new List<Usuario>();
                rUsuario = new UsuarioRepository();
                Promocion oPromocion = this.GetPromocion(PromocionId);
                if (oPromocion != null && oPromocion.Promocionid > 0)
                {
                    if (oPromocion.Promociondetalle != null)
                    {
                        Promociondetalle oPromoDetalle = new Promociondetalle();
                        oPromoDetalle = oPromocion.Promociondetalle.FirstOrDefault(f => f.Condicion == "VISITA");
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

                                    lsUser.ForEach(d =>
                                    {
                                        if (oPromocion.Promociondetalle.Any(f => f.Condicion == "VISITA"))
                                        {
                                            if (oPromocion.Promociondetalle.Any(f => f.Condicion == "IMPORTE"))
                                            {
                                                var ProImporte = oPromocion.Promociondetalle.Where(f => f.Condicion == "IMPORTE").ToList();
                                                ProImporte.ForEach(f =>
                                                {
                                                    double Valor1 = 0;
                                                    double Valor2 = 0;

                                                    double.TryParse(f.Valor1, out Valor1);
                                                    double.TryParse(f.Valor2, out Valor2);

                                                    //if (Valor2 < d.ImporteActual || Valor1 > d.ImporteActual)
                                                    if (d.ImporteActual >= Valor1 && d.ImporteActual <= Valor2)
                                                    {
                                                        _lsUser.Add(d);
                                                        _exito = true;
                                                    }

                                                });
                                            }
                                        }

                                    });


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

            return _lsUser;
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
                                    if (oPromocion.Promociondetalle.Any(f => f.Condicion == "VISITA"))
                                    {
                                        if (oPromocion.Promociondetalle.Any(f => f.Condicion == "IMPORTE"))
                                        {
                                            var ProImporte = oPromocion.Promociondetalle.Where(f => f.Condicion == "IMPORTE").ToList();
                                            ProImporte.ForEach(f =>
                                            {
                                                double Valor1 = 0;
                                                double Valor2 = 0;

                                                double.TryParse(f.Valor1, out Valor1);
                                                double.TryParse(f.Valor2, out Valor2);

                                                //if (Valor2 < oUsuario.ImporteActual || Valor1 > oUsuario.ImporteActual)
                                                if (oUsuario.ImporteActual >= Valor1 && oUsuario.ImporteActual <= Valor2)
                                                {
                                                    _lsPromocion.Add(oPromocion);
                                                    _exito = true;
                                                }

                                            });
                                        }
                                    }
                                    else _lsPromocion.Add(oPromocion);

                                    //var DetallePromocion = oPromocion.Promociondetalle.FirstOrDefault();
                                    //if (DetallePromocion != null)
                                    //{
                                    //    if (DetallePromocion.Condicion == "VISITA")
                                    //    {
                                    //        int Valor1 = 0;
                                    //        if (!string.IsNullOrEmpty(DetallePromocion.Valor1))
                                    //        {
                                    //            Valor1 = Convert.ToInt32(DetallePromocion.Valor1);
                                    //        }
                                    //        if (oUsuario.VisitaActual == Valor1)
                                    //            _lsPromocion.Add(oPromocion);
                                    //    }
                                    //    else
                                    //        _lsPromocion.Add(oPromocion);
                                    //}
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

                    rNotificacion = new NotificacionRepository();

                    List<Notificacion> lNotificacion = rNotificacion.GetPromocionByUser(oRedimirPromo.UsuarioId);

                    IsSave = rPromocionRedimir.Guardar(oPromocionRedimir);
                    if (IsSave)
                    {
                        bool IsSaveVisita = this.ActualizarVisitaPromocion(oRedimirPromo.PromocionId, oRedimirPromo.UsuarioId);
                        if (IsSaveVisita)
                        {
                            bool IsSaveBajaNot = this.BajaNotificacionPromocion(oRedimirPromo.UsuarioId);
                            if (IsSaveBajaNot)
                            {
                                _exito = true;
                            }
                            else
                            {
                                _mensajes.Add("No se pudo dar de baja la notificación");
                                _exito = false;
                            }
                        }
                        else
                        {
                            _exito = false;
                            Mensajes.Add("No se pudo actualizar las visita del usuario");
                        }
                    }
                }
                else
                {
                    _mensajes.Add("La promoción ha sido redimida con anterioridad");
                }

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
        /*public Usuario GetUsuario(string usuario)
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
        }*/

        public Usuario GetUsuarioByIDExterno(string usuario)
        {
            _exito = false;
            _mensajes = new List<string>();
            _errores = new List<string>();
            Usuario oUsuario = null;

            try
            {
                rUsuario = new UsuarioRepository();
                oUsuario = rUsuario.Query(a => a.IdExterno == usuario).FirstOrDefault();
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
                    oNotSuc.NotificacionID = oNot.NotificacionID;
                    oNotSuc.PromocionID = oNot.PromocionID;
                    oNotSuc.Referencia = oNot.Referencia;
                    oNotSuc.SucursalId = oNot.SucursalId;
                    oNotSuc.ImporteVisita = oNot.ImporteVisita;
                    oNotSuc.Relacionado = oNot.Relacionado;
                    oNotSuc.Tipo = oNot.Tipo;
                    oNotSuc.Usuario = oNot.Usuario;
                    oNotSuc.UsuarioAlta = oNot.UsuarioAlta;
                    oNotSuc.UsuarioID = oNot.UsuarioID;
                    oNotSuc.Vigencia = oNot.Vigencia;
                    Sucursal oSuc = rSucursal.GetById(oNotSuc.SucursalId);
                    if (oSuc != null)
                    {
                        oNotSuc.Sucursal = rSucursal.GetById(oNotSuc.SucursalId).Nombre;
                    }
                    else
                    {
                        oNotSuc.Sucursal = "Todas";
                    }

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
        public Usuario LoginMovil(string usuario, string password, Origen origen = Origen.MOBILE, bool Login1 = false)
        {
            UsuarioRepository rUsuario = new UsuarioRepository();
            ActivacionRepository rAcrivacion = new ActivacionRepository();
            Usuario oUsuario = null;

            try
            {
                if (Login1)
                {
                    if (!Funciones.ValidarCorreo(usuario))
                        throw new Exception("Ingrese un correo válido");

                    usuario = rUsuario.GetUsuarioID(usuario).ToString();
                    //_errores.Add("Ingrese un correo válido");
                }

                oUsuario = rUsuario.LoginMovil(usuario, password, origen);

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

                    if (oCampana.DistribucionId > 0)
                    {
                        List<Usuario> lsUsuario = this.GetUserForCampana(oCampana);

                        if (lsUsuario.Count > 0)
                        {
                            this.CrearNotificacion(oCampana, lsUsuario);
                        }
                    }

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

                if (obj.DistribucionID == 0)
                {
                    obj.Estado = "ACTIVO";
                }

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

        public bool deleteTokens(int usuario, string token = "", bool EliminarTodos = true)
        {
            _exito = false;

            try
            {
                rUsuarioDispositivo = new UsuarioDispositivoRepository();
                Usuario oUsuario = getUsuarioById(usuario);

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

        public List<Usuario> GetUserForCampana(Campana oCampana)
        {
            _exito = false;
            _mensajes.Clear();
            _errores.Clear();
            List<Usuario> lsUsuario = new List<Usuario>();
            try
            {
                UtileriaController oUtileria = new UtileriaController();
                Distribucion obj = this.GetDistribucion(oCampana.DistribucionId);

                //if (obj.Condiciones != null && obj.Condiciones.Count > 0)
                if (obj.Condiciones != null)
                {
                    var cond = oUtileria.CrearCondion(obj.Condiciones.ToList());
                    rUsuario = new UsuarioRepository();
                    lsUsuario = rUsuario.GetUsuarios(cond);
                }

                _exito = true;
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
            return lsUsuario;
        }

        public void CrearNotificacion(Campana oCampana, List<Usuario> lsUsuario)
        {
            string Tipo = string.Empty;
            if (oCampana.PromocionId > 0)
                Tipo = "PROMOCION";
            else Tipo = "INFORMATIVO";

            switch (Tipo.ToUpper())
            {
                case "PROMOCION":

                    Promocion oPromocion = this.GetPromocion(oCampana.PromocionId.Value);
                    if (oCampana.PromocionId.HasValue && oCampana.PromocionId.Value > 0)
                    {
                        rNotificacion = new NotificacionRepository();
                        rPromoUser = new PromocionUsuarioRepository();
                        bool todoMembresia = true;

                        try
                        {
                            var tipoMembresia = oPromocion.Promocionmembresia.ToList();

                            List<int> lsIntMembresia = new List<int>();

                            if (tipoMembresia != null && tipoMembresia.Count > 0)
                            {
                                todoMembresia = false;

                                foreach (var item_ in oPromocion.Promocionmembresia)
                                {
                                    lsIntMembresia.Add(item_.Membresiaid);
                                }

                                lsUsuario = lsUsuario.Where(f => lsIntMembresia.Contains(f.TarjetaID)).ToList();
                            }

                        }
                        catch (Exception)
                        {

                        }

                        DateTime oDateNow = DateTime.Now;

                        DateTime inicial = oPromocion.Vigenciainicial.Value;
                        DateTime final = oPromocion.Vigenciafinal.Value;

                        int result1 = DateTime.Compare(inicial, oDateNow);
                        int result2 = DateTime.Compare(final, oDateNow);


                        if (result1 <= 0 && result2 >= 0)
                        {
                            lsUsuario.ForEach(d =>
                                        {
                                            decimal porcentaje = (d.oTarjeta.Porcientodescuento.HasValue ? (d.oTarjeta.Porcientodescuento.Value / 100) : 0);
                                            double ImporteCompare = d.ImporteActual * (double)porcentaje;

                                            if (oPromocion.Promociondetalle.Any(f => f.Condicion == "VISITA"))
                                            {
                                                if (oPromocion.Promociondetalle.Any(f => f.Condicion == "IMPORTE"))
                                                {
                                                    var ProImporte = oPromocion.Promociondetalle.Where(f => f.Condicion == "IMPORTE").ToList();
                                                    ProImporte.ForEach(f =>
                                                    {
                                                        double Valor1 = 0;
                                                        double Valor2 = 0;

                                                        double.TryParse(f.Valor1, out Valor1);
                                                        double.TryParse(f.Valor2, out Valor2);
                                                        // 0 >= 12 || 0 <= 14
                                                        //if (d.ImporteActual >= Valor1 && d.ImporteActual <= Valor2)
                                                        if (ImporteCompare >= Valor1 && ImporteCompare <= Valor2)
                                                        {
                                                            Notificacion _oNotificacion = rNotificacion.GetNotificacion(d.Usuarioid, oPromocion.Promocionid);
                                                            Notificacion oNotificacionProm = new Notificacion();

                                                            if (_oNotificacion != null && _oNotificacion.NotificacionID > 0)
                                                            {
                                                                oNotificacionProm.Estatus = "ACTIVO";
                                                                oNotificacionProm.FechaRegistro = DateTime.Now;
                                                                oNotificacionProm.Mensaje = oPromocion.Descripcion;
                                                                oNotificacionProm.NotificacionID = _oNotificacion.NotificacionID;
                                                                oNotificacionProm.PromocionID = oPromocion.Promocionid;
                                                                oNotificacionProm.Referencia = "";
                                                                oNotificacionProm.Tipo = "PROMOCION";
                                                                oNotificacionProm.Usuario = new Usuario() { Usuarioid = d.Usuarioid };
                                                                oNotificacionProm.UsuarioID = d.Usuarioid;
                                                                oNotificacionProm.Vigencia = DateTime.Now;
                                                            }
                                                            else
                                                            {
                                                                oNotificacionProm.Estatus = "ACTIVO";
                                                                oNotificacionProm.FechaRegistro = DateTime.Now;
                                                                oNotificacionProm.Mensaje = oPromocion.Descripcion;
                                                                oNotificacionProm.NotificacionID = 0;
                                                                oNotificacionProm.PromocionID = oPromocion.Promocionid;
                                                                oNotificacionProm.Referencia = "";
                                                                oNotificacionProm.Tipo = "PROMOCION";
                                                                oNotificacionProm.Usuario = new Usuario() { Usuarioid = d.Usuarioid };
                                                                oNotificacionProm.UsuarioID = d.Usuarioid;
                                                                oNotificacionProm.Vigencia = DateTime.Now;
                                                            }

                                                            Notificacion _oNotificacionProm = rNotificacion.GuardarNotificacion(oNotificacionProm);
                                                            _exito = true;
                                                        }

                                                    });
                                                }
                                            }
                                            else
                                            {
                                                Notificacion _oNotificacion = rNotificacion.GetNotificacion(d.Usuarioid, oPromocion.Promocionid);
                                                Notificacion oNotificacionProm = new Notificacion();

                                                if (_oNotificacion != null && _oNotificacion.NotificacionID > 0)
                                                {
                                                    oNotificacionProm.Estatus = "ACTIVO";
                                                    oNotificacionProm.FechaRegistro = DateTime.Now;
                                                    oNotificacionProm.Mensaje = oPromocion.Descripcion;
                                                    oNotificacionProm.NotificacionID = _oNotificacion.NotificacionID;
                                                    oNotificacionProm.PromocionID = oPromocion.Promocionid;
                                                    oNotificacionProm.Referencia = "";
                                                    oNotificacionProm.Tipo = "PROMOCION";
                                                    oNotificacionProm.Usuario = new Usuario() { Usuarioid = d.Usuarioid };
                                                    oNotificacionProm.UsuarioID = d.Usuarioid;
                                                    oNotificacionProm.Vigencia = DateTime.Now;
                                                }
                                                else
                                                {
                                                    oNotificacionProm.Estatus = "ACTIVO";
                                                    oNotificacionProm.FechaRegistro = DateTime.Now;
                                                    oNotificacionProm.Mensaje = oPromocion.Descripcion;
                                                    oNotificacionProm.NotificacionID = 0;
                                                    oNotificacionProm.PromocionID = oPromocion.Promocionid;
                                                    oNotificacionProm.Referencia = "";
                                                    oNotificacionProm.Tipo = "PROMOCION";
                                                    oNotificacionProm.Usuario = new Usuario() { Usuarioid = d.Usuarioid };
                                                    oNotificacionProm.UsuarioID = d.Usuarioid;
                                                    oNotificacionProm.Vigencia = DateTime.Now;
                                                }

                                                Notificacion _oNotificacionProm = rNotificacion.GuardarNotificacion(oNotificacionProm);
                                                _exito = true;
                                            }

                                        });
                        }

                    }


                    break;
                case "INFORMATIVO":

                    foreach (Usuario oUser in lsUsuario)
                    {
                        Notificacion oNotificacion = new Notificacion();

                        oNotificacion.Estatus = "ACTIVO";
                        oNotificacion.FechaRegistro = DateTime.Now;
                        oNotificacion.Mensaje = oCampana.MensajePrevio;
                        oNotificacion.NotificacionID = 0;
                        oNotificacion.PromocionID = 0;
                        oNotificacion.Referencia = "";
                        oNotificacion.Tipo = "EVENTO";
                        oNotificacion.Usuario = new Usuario() { Usuarioid = oUser.Usuarioid };
                        oNotificacion.UsuarioID = oUser.Usuarioid;
                        oNotificacion.Vigencia = DateTime.Now;

                        rNotificacion = new NotificacionRepository();

                        Notificacion _oNotificacion = rNotificacion.GuardarNotificacion(oNotificacion);

                    }

                    break;
            }
        }

        public List<Distribucion> GetAllDistribucion()
        {
            _exito = false;
            _errores = new List<string>();
            _mensajes = new List<string>();
            List<Distribucion> ls = new List<Distribucion>();
            try
            {
                rDistribucion = new DistribucionRepository();
                ls = rDistribucion.GetAllActivos();
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

        public bool GuardarTipoInteres(TipoInteres oTipo)
        {
            _exito = false;
            _errores = new List<string>();
            _mensajes = new List<string>();
            try
            {
                TipoInteresVR vrTipoInteres = new TipoInteresVR();
                rTipoInteres = new TipoInteresRepository();
                if (rTipoInteres.ExisteTipoInteres(oTipo))
                {
                    _mensajes.Add("El tipo de interes ya ha sido ingresado.");
                }
                else if (!vrTipoInteres.Insertar(oTipo)) _mensajes.AddRange(vrTipoInteres.Mensajes);
                else
                {
                    var modificado = new TipoInteres();
                    if (oTipo.TipoInteresID == 0)
                    {
                        oTipo.FechaAlta = DateTime.Now;
                        oTipo.Estatus = Estatus.ACTIVO.ToString();
                        oTipo.UsuarioAlta = UsuarioId;
                        oTipo.FechaBaja = new DateTime(1900, 01, 01);
                        modificado = oTipo;
                    }
                    else
                    {
                        modificado = rTipoInteres.GetById(oTipo.TipoInteresID);
                        modificado.Descripcion = oTipo.Descripcion;
                        modificado.Nombre = oTipo.Nombre;
                    }
                    _exito = rTipoInteres.Guardar(modificado);
                }
            }
            catch (Exception ex)
            {
                if (rTipoInteres._session.Transaction.IsActive) rTipoInteres._session.Transaction.Rollback();

                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
                _exito = false;
            }
            return _exito;
        }

        public bool BajaTipoInteres(int TipoInteresID)
        {
            _exito = false;
            _errores = new List<string>();
            _mensajes = new List<string>();
            try
            {
                rTipoInteres = new TipoInteresRepository();
                var oTipo = rTipoInteres.GetById(TipoInteresID);
                oTipo.Estatus = Estatus.BAJA.ToString();
                oTipo.FechaBaja = DateTime.Now;
                oTipo.UsuarioBaja = UsuarioId;
                _exito = rTipoInteres.Guardar(oTipo);
            }
            catch (Exception ex)
            {
                if (rTipoInteres._session.Transaction.IsActive) rTipoInteres._session.Transaction.Rollback();

                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
                _exito = false;
            }
            return _exito;
        }

        public TipoInteres GetTipoInteres(int TipoInteresID)
        {
            _exito = false;
            _errores = new List<string>();
            _mensajes = new List<string>();
            TipoInteres oTipo = null;
            try
            {
                rTipoInteres = new TipoInteresRepository();
                oTipo = rTipoInteres.GetById(TipoInteresID);
                _exito = true;
            }
            catch (Exception ex)
            {
                if (rTipoInteres._session.Transaction.IsActive) rTipoInteres._session.Transaction.Rollback();

                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
                _exito = false;
            }
            return oTipo;
        }

        public List<TipoInteres> GetAllTipoInteres()
        {
            _exito = false;
            _errores = new List<string>();
            _mensajes = new List<string>();
            List<TipoInteres> ls = new List<TipoInteres>();
            try
            {
                rTipoInteres = new TipoInteresRepository();
                ls = rTipoInteres.GetAllActivos();
                _exito = true;
            }
            catch (Exception ex)
            {
                if (rTipoInteres._session.Transaction.IsActive) rTipoInteres._session.Transaction.Rollback();

                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
                _exito = false;
            }
            return ls;
        }

        //Para dar de baja las notificaciones primero se debe aplicar la actualizacion de la visita de usuario
        public bool BajaNotificacionPromocion(int UsuarioId)
        {
            _exito = true;
            _mensajes.Clear();
            _errores.Clear();
            try
            {
                int VisitaActual = 0;
                Usuario oUsuario = this.getUsuarioById(UsuarioId);
                if (oUsuario != null)
                {
                    VisitaActual = oUsuario.VisitaActual;
                    List<Promocion> lsPromocion = this.GetAllPromocionIdBaja(VisitaActual);
                    foreach (Promocion oPromocion in lsPromocion)
                    {
                        bool isSave = BajaNotificacion(UsuarioId, oPromocion.Promocionid);
                        if (!isSave)
                        {
                            Mensajes.Add("La notificacion de la promocion '" + oPromocion.Descripcion + "' no se puede dar de baja.");
                        }
                    }
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
                _exito = false;
            }
            return _exito;
        }
        //Solo se usa la promocion para poder actualizar las visitas
        public bool ActualizarVisitaPromocion(int PromocionId, int UsuarioId)
        {
            _exito = true;
            _mensajes.Clear();
            _errores.Clear();
            try
            {
                Promocion oPromocion = this.GetPromocion(PromocionId);

                if (oPromocion != null && oPromocion.Promocionid > 0)
                {
                    Promociondetalle oDetalle = oPromocion.Promociondetalle.FirstOrDefault();
                    int NumeroARestar = 0;
                    int VisitaAnterior = 0;
                    int VisitaActual = 0;
                    if (oDetalle != null)
                    {
                        if (oDetalle.Condicion == "VISITA")
                        {
                            int.TryParse(oDetalle.Valor1, out NumeroARestar);
                            if (NumeroARestar > 0)
                            {
                                Usuario oUsuario = this.getUsuarioById(UsuarioId);
                                if (oUsuario != null)
                                {
                                    VisitaAnterior = oUsuario.VisitaActual;
                                    VisitaActual = VisitaAnterior - NumeroARestar;
                                    rUsuario = new UsuarioRepository();
                                    bool IsSave = rUsuario.SaveNumVisita(UsuarioId, VisitaActual);
                                    if (!IsSave)
                                    {
                                        Mensajes.Add("Ocurrio un error al actualizar el numero de visitas");
                                        _exito = false;
                                        return false;
                                    }
                                    else _exito = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    _exito = false;
                    Mensajes.Add("No existe la promoción");
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
                _exito = false;
            }
            return _exito;
        }
        public List<Promocion> GetAllPromocionIdBaja(int NumeroVisita)
        {
            _exito = true;
            _mensajes.Clear();
            _errores.Clear();
            List<Promocion> lsPromocion = new List<Promocion>();
            try
            {
                lsPromocion = rPromocion.GetPromocionBajaVisita(NumeroVisita);
                _exito = true;

            }
            catch (Exception ex)
            {
                if (rPromocion._session.Transaction.IsActive)
                {
                    rPromocion._session.Transaction.Rollback();
                }
                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
                _exito = false;
            }
            return lsPromocion;
        }
        private bool BajaNotificacion(int UsuarioId, int PromocionId)
        {
            _exito = true;
            _mensajes.Clear();
            _errores.Clear();
            rNotificacion = new NotificacionRepository();

            try
            {
                _exito = rNotificacion.BajaNotificacionPorPromocion(UsuarioId, PromocionId);
            }
            catch (Exception ex)
            {
                if (rNotificacion._session.Transaction.IsActive) rNotificacion._session.Transaction.Rollback();
                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
                _exito = false;
            }
            return _exito;
        }

        public List<Notificacion> GetNotificacionesNoRelacionadas()
        {
            List<Notificacion> lNot = null;

            try
            {
                rNotificacion = new NotificacionRepository();
                lNot = rNotificacion.Query(x => x.Relacionado == false && x.Tipo == "VISITA").ToList();

                rNotificacion._session.Clear();
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
            return lNot;
        }

        public bool GuardaProductos(List<ProductoVenta> lProductos)
        {
            bool bResult = false;

            ProductoVentaRepository rProductoVenta = new ProductoVentaRepository();

            try
            {
                rProductoVenta._session.BeginTransaction();
                foreach (ProductoVenta oProducto in lProductos)
                {
                    rProductoVenta.Add(oProducto);

                }

                rProductoVenta._session.Transaction.Commit();
                bResult = true;
            }
            catch (Exception ex)
            {
                Errores.Add(ex.Message);
            }

            return bResult;
        }

        public bool ActualizaNotificacionYUsuario(Usuario usuaurio, Notificacion notificacion)
        {
            bool bResult = false;
            rUsuario = new UsuarioRepository();
            rNotificacion = new NotificacionRepository();

            try
            {
                if (rUsuario.GuardarVarios(usuaurio, notificacion))
                    bResult = true;
                /*if(rUsuario.Errores.Count > 0)
                {
                    rUsuario._session.Transaction.Rollback();
                }
                else
                {
                    rNotificacion.GuardarNotificacion2(notificacion);

                    if (rNotificacion.Errores.Count > 0)
                        rUsuario._session.Transaction.Rollback();
                    else
                        rUsuario._session.Transaction.Commit();
                }*/
            }
            catch (Exception ex)
            {
                if (rUsuario._session.Transaction != null)
                    rUsuario._session.Transaction.Rollback();

                Errores.Add(ex.Message);
            }

            return bResult;
        }

        public UsuarioDispositivo GetTokenActivoUsuario(int UsuarioId, Plataforma _plataforma)
        {
            Mensajes.Clear();
            Exito = false;
            Errores.Clear();
            UsuarioDispositivo _oDispositivo = new UsuarioDispositivo();
            rUsuarioDispositivo = new UsuarioDispositivoRepository();
            try
            {
                _oDispositivo = rUsuarioDispositivo.Query(a => a.UsuarioId == UsuarioId && a.Plataforma == _plataforma.ToString()).OrderByDescending(a => a.FechaAlta).FirstOrDefault();
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

        public async void EnviaNotificacion(List<int> notificaciones)
        {
            try
            {
                foreach (int notificacionId in notificaciones)
                {
                    Notificacion notificacion = GetNotificacion(notificacionId);
                    rUsuarioDispositivo = new UsuarioDispositivoRepository();
                    UtileriaController cUtil = new UtileriaController();

                    if (notificacion != null)
                    {
                        UsuarioDispositivo oUsuario = rUsuarioDispositivo.getTokenActivo(notificacion.UsuarioID);
                        if (oUsuario != null)
                        {
                            try
                            {
                                if (oUsuario.Plataforma == Plataforma.ANDROID.ToString())
                                    cUtil.sendGoogleNotifications(notificacion, oUsuario);

                                if (oUsuario.Plataforma == Plataforma.IOS.ToString())
                                    cUtil.sendAppleNotification(notificacion, oUsuario);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public bool EliminarDetallePromocion(int Promocionid, int PromocionDetalleId)
        {
            _exito = false;
            _mensajes.Clear();
            _errores.Clear();
            rPromocion = new PromocionRepository();
            try
            {
                _exito = rPromocion.EliminarDetalle(Promocionid, PromocionDetalleId);
            }
            catch (Exception InnerException)
            {
                if (rPromocion._session.Transaction.IsActive)
                    rPromocion._session.Transaction.Rollback();

                Errores.Add(InnerException.Message);
            }

            return _exito;
        }

        public List<Usuario> GetUsaurios(List<CondicionDistribucion> lsCond)
        {
            _exito = false;
            _errores = new List<string>();
            _mensajes = new List<string>();
            List<Usuario> lsUser = new List<Usuario>();
            try
            {
                rUsuario = new UsuarioRepository();
                UtileriaController cUtileria = new UtileriaController();
                var cond = cUtileria.CrearCondion(lsCond);
                lsUser = rUsuario.GetUsuarios(cond);
            }
            catch (Exception ex)
            {
                _exito = false;
                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
            }
            return lsUser;
        }

        public bool BajaUsuario(int UsuarioID)
        {
            _exito = false;
            _errores = new List<string>();
            _mensajes = new List<string>();
            try
            {
                rUsuario = new UsuarioRepository();
                var oUsuario = rUsuario.GetById(UsuarioID);
                oUsuario.FechaBaja = DateTime.Now;
                oUsuario.Estatus = "BAJA";
                _exito = rUsuario.Guardar(oUsuario);
            }
            catch (Exception ex)
            {
                _exito = false;
                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
            }
            return _exito;
        }

        public List<Distribucion> GetAllDistribucion(int TipoMembresia)
        {
            _exito = false;
            _errores = new List<string>();
            _mensajes = new List<string>();
            List<Distribucion> ls = new List<Distribucion>();
            try
            {
                rDistribucion = new DistribucionRepository();
                ls = rDistribucion.GetDistribucion(TipoMembresia);
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


        public bool RecuperarPassword(string usuario)
        {
            bool bResult = false;

            rUsuario = new UsuarioRepository();
            int idUsuario = rUsuario.GetUsuarioID(usuario);

            if(idUsuario == 0)
            {
                Errores.Add("No se encontró el usuario solicitado. Favor de verificarlo. Codigo 01X01");
                return bResult;
            }

            Usuario oUsuario = rUsuario.GetById(idUsuario);

            if(oUsuario != null)
            {
                if(oUsuario.Tipo == "MOBILE" && oUsuario.Origen == "MOBILE")
                {
                    string key = UtileriaController.doKeyActivation(usuario, "KeyPartStatic");
                    Activacion oActivacion = new Activacion();
                    oActivacion.Activado = false;
                    oActivacion.Email = oUsuario.Email;
                    oActivacion.FechaAlta = DateTime.Now;
                    oActivacion.FechaVencimiento = oActivacion.FechaAlta.AddDays(5);
                    oActivacion.Llave = key;
                    oActivacion.UsuarioId = oUsuario.Usuarioid;
                    rUsuario._session.SaveOrUpdate(oActivacion);

                    List<string> para = new List<string>();
                    para.Add(oUsuario.Email);

                    rConfiguracion = new ConfiguracionRepository();
                    Configuracion oConfig = rConfiguracion.GetByClave("RECUPERA_PLANTILLA");
                    string mensaje = oConfig.Valor;

                    oConfig = rConfiguracion.GetByClave("URL_RECUPERAR");

                    mensaje = mensaje.Replace("[URL]", oConfig.Valor + key);
                    mensaje = mensaje.Replace("[USUARIO]", oUsuario.Nombre + "(" + oUsuario.Email + ")");

                    rUsuario.EnviarCorreo(para, "Recuperación de Contraseña - Friday's App", mensaje, true);
                }
                else
                {
                    Errores.Add("El proceso de recuperar contraseña no puede ser realizado para el usuario solicitado, ya que el tipo de acceso es por medio de " + oUsuario.Origen + ". Debe intentar acceder por el mismo medio. 01X02");
                    return bResult;
                }
            }
            else
            {
                Errores.Add("No se encontró el usuario solicitado. Favor de verificarlo. 01X03");
                return bResult;
            }


            return bResult;
        }
    }
}