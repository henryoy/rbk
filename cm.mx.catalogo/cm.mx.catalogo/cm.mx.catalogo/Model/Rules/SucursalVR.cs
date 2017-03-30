using System.Collections.Generic;

namespace cm.mx.catalogo.Model.Rules
{
    public class SucursalVR
    {
        private static List<string> _lsMensajes = new List<string>();
        public static List<string> Mensajes { get { return _lsMensajes; } }
        private static bool _exito = true;

        public static bool InsertarVR(Sucursal Objeto)
        {
            _exito = true;
            _lsMensajes.Clear();

            if (string.IsNullOrEmpty(Objeto.Nombre))
            {
                _lsMensajes.Add("Nombre no puede ser vacío.");
                _exito = false;
            }
            if (Objeto.Longitud == 0)
            {
                _lsMensajes.Add("La longitud no puede ser cero.");
                _exito = false;
            }

            if (Objeto.Latitud == 0)
            {
                _lsMensajes.Add("La latitud no puede ser cero.");
                _exito = false;
            }

            if (string.IsNullOrEmpty(Objeto.Direccion))
            {
                _lsMensajes.Add("La dirección no puede ser vacio");
                _exito = false;
            }

            return _exito;
        }

        public static bool ActualizarVR(Sucursal Objeto)
        {
            _exito = true;
            _lsMensajes.Clear();

            if (Objeto.SucursalID <= 0)
            {
                _lsMensajes.Add("El identificador de la membresia no puede ser menor o igual a cero.");
                _exito = false;
            }

            if (string.IsNullOrEmpty(Objeto.Nombre))
            {
                _lsMensajes.Add("Nombre no puede ser vacío.");
                _exito = false;
            }
            if (Objeto.Longitud == 0)
            {
                _lsMensajes.Add("La longitud no puede ser cero.");
                _exito = false;
            }

            if (Objeto.Latitud == 0)
            {
                _lsMensajes.Add("La latitud no puede ser cero.");
                _exito = false;
            }

            if (string.IsNullOrEmpty(Objeto.Direccion))
            {
                _lsMensajes.Add("La dirección no puede ser vacio");
                _exito = false;
            }

            return _exito;
        }

        public static bool EliminarVR(Tipomembresia Objeto)
        {
            _exito = true;
            _lsMensajes.Clear();
            return _exito;
        }
    }
}