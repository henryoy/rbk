using cm.mx.catalogo.Model;
using cm.mx.dbCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cm.mx.catalogo.Controller
{
    public class CatalogoController : IController
    {
        #region propiedades
        private List<string> _errores = new List<string>();
        private bool _exito = false;
        private string _mensaje = string.Empty;
        private List<string> _mensajes = new List<string>();
        public List<string> Errores
        {
            get { return _errores; }
            set { _errores = value; }
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
            set { _mensajes = value; }
        }
        #endregion
        #region Repositorios
        PromocionRepository rPromocion;
        #endregion
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
    }
}
