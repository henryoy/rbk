using cm.mx.dbCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.mx.catalogo.Controller
{
    public class UtileriaController : IController
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

        public string GenerarFolio(int SucursalId)
        {
            _exito = false;
            _errores = new List<string>();
            _mensajes = new List<string>();
            string folio = "";
            try
            {
                var sfecha = String.Format("{0:yyyyMMddHHmmss}", DateTime.Now);
                var sSuc = String.Format("{0:00000}", SucursalId);
                folio = sfecha + "-" + sSuc;
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

            return folio;
        }
    }
}
