using cm.mx.catalogo.Model;
using cm.mx.catalogo.Model.Rules;
using cm.mx.dbCore.Interfaces;
using cm.mx.dbCore.Tools;
using System;
using System.Collections.Generic;

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

        #endregion Repositorios

        public bool Login(string correo, string contrasena)
        {
            _exito = false;
            _errores.Clear();
            _mensaje = string.Empty;
            _mensajes.Clear();
            rUsuario = new UsuarioRepository();
            try
            {
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
            rPromocion = new PromocionRepository();
            try
            {
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
            rPromocion = new PromocionRepository();
            try
            {
                oPromocion = rPromocion.GuardarPromocion(entidad);
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

        public List<Promocion> GetAllPromocion()
        {
            List<Promocion> lsPromocion = new List<Promocion>();
            _exito = false;
            _errores.Clear();
            _mensaje = string.Empty;
            _mensajes.Clear();
            rPromocion = new PromocionRepository();
            try
            {
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

        public bool EliminarPromocion(int PromocionId)
        {
            bool isdelete = false;
            _exito = false;
            _errores.Clear();
            _mensaje = string.Empty;
            _mensajes.Clear();
            rPromocion = new PromocionRepository();
            try
            {
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
            TipoMembresiaRepository rMembresia = new TipoMembresiaRepository();
            try
            {
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
            rMembresia = new TipoMembresiaRepository();
            try
            {
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
            rMembresia = new TipoMembresiaRepository();
            try
            {
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
            SucursalRepository rSucursal = new SucursalRepository();
            try
            {
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
            rSucursal = new SucursalRepository();
            try
            {
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
            rSucursal = new SucursalRepository();
            try
            {
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
            TarjetaRepository rTarjeta = new TarjetaRepository();
            try
            {
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
            rTarjeta = new TarjetaRepository();
            try
            {
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
            rTarjeta = new TarjetaRepository();
            try
            {
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
            rUsuario = new UsuarioRepository();
            _exito = true;
            Mensajes.Clear();
            Errores.Clear();
            try {
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
            rUsuario = new UsuarioRepository();
            _exito = true;
            Mensajes.Clear();
            Errores.Clear();
            try
            {
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
            rUsuario = new UsuarioRepository();
            _exito = true;
            Mensajes.Clear();
            Errores.Clear();
            try
            {
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
            rUsuario = new UsuarioRepository();
            _exito = true;
            Mensajes.Clear();
            Errores.Clear();
            try
            {
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
            rUsuario = new UsuarioRepository();
            _exito = true;
            Mensajes.Clear();
            Errores.Clear();
            try
            {
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
    }
}