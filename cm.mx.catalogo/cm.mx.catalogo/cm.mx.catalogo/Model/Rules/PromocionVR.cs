using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.mx.catalogo.Model.Rules
{
    public class PromocionVR
    {
        private static List<string> _lsMensajes = new List<string>();
        public static List<string> Mensajes { get { return _lsMensajes; } }
        private static bool _exito = true;

        public static bool InsertarVR(Promocion Objeto)
        {
            _exito = true;
            _lsMensajes.Clear();

            if (string.IsNullOrEmpty(Objeto.Descripcion))
            {
                _lsMensajes.Add("Nombre no puede ser vacío.");
                _exito = false;
            }

            if (string.IsNullOrEmpty(Objeto.Estado))
            {
                _lsMensajes.Add("El estado no puede ser vacío.");
                _exito = false;
            }           

            return _exito;
        }

        public static bool ActualizarVR(Promocion Objeto)
        {
            _exito = true;
            _lsMensajes.Clear();
            if (Objeto.Promocionid == 0)
            {
                _lsMensajes.Add("El identificador de la promocion no puede ser cero.");
                _exito = false;
            }
            if (string.IsNullOrEmpty(Objeto.Descripcion))
            {
                _lsMensajes.Add("Nombre no puede ser vacío.");
                _exito = false;
            }

            if (string.IsNullOrEmpty(Objeto.Estado))
            {
                _lsMensajes.Add("El estado no puede ser vacío.");
                _exito = false;
            }          

            return _exito;
        }

        public static bool EliminarVR(Promocion Objeto)
        {
            _exito = true;
            _lsMensajes.Clear();
            return _exito;
        }
    }
}
