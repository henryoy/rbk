using System.Collections.Generic;

namespace cm.mx.catalogo.Model.Rules
{
    public class MembresiaVR
    {
        private static List<string> _lsMensajes = new List<string>();
        public static List<string> Mensajes { get { return _lsMensajes; } }
        private static bool _exito = true;

        public static bool InsertarVR(Tipomembresia Objeto)
        {
            _exito = true;
            _lsMensajes.Clear();

            if (string.IsNullOrEmpty(Objeto.Nombre))
            {
                _lsMensajes.Add("Nombre no puede ser vacío.");
                _exito = false;
            }

            if (Objeto.Hasta <= 0)
            {
                _lsMensajes.Add("Hasta no puede ser menor o igual a cero.");
                _exito = false;
            }

            if (Objeto.ApartirDe >= Objeto.Hasta)
            {
                _lsMensajes.Add("ApartirDe no puede ser mayor o igual a Hasta");
                _exito = false;
            }

            if (Objeto.Porcientodescuento <= 0)
            {
                _lsMensajes.Add("Descuento no puede ser menor o igual que cero.");
                _exito = false;
            }

            if (string.IsNullOrEmpty(Objeto.UrlImagen))
            {
                _lsMensajes.Add("UrlImagen no puede ser vacío.");
                _exito = false;
            }

            return _exito;
        }

        public static bool ActualizarVR(Tipomembresia Objeto)
        {
            _exito = true;
            _lsMensajes.Clear();

            if (Objeto.Membresiaid <= 0)
            {
                _lsMensajes.Add("El identificador de la membresia no puede ser menor o igual a cero.");
                _exito = false;
            }

            if (string.IsNullOrEmpty(Objeto.Nombre))
            {
                _lsMensajes.Add("Nombre no puede ser vacío.");
                _exito = false;
            }

            if (Objeto.Hasta <= 0)
            {
                _lsMensajes.Add("Hasta no puede ser menor o igual a cero.");
                _exito = false;
            }

            if (Objeto.ApartirDe >= Objeto.Hasta)
            {
                _lsMensajes.Add("ApartirDe no puede ser mayor o igual a Hasta");
                _exito = false;
            }

            if (Objeto.Porcientodescuento <= 0)
            {
                _lsMensajes.Add("El descuento no puede ser menor o igual que cero.");
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