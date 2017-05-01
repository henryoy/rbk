using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.mx.catalogo.Model
{
    class CampanaVR
    {
        private static List<string> _lsMensajes = new List<string>();
        public static List<string> Mensajes { get { return _lsMensajes; } }
        private static bool _exito = true;

        public static bool InsertarVR(Campana Objeto)
        {
            _exito = true;
            _lsMensajes.Clear();

            if (string.IsNullOrEmpty(Objeto.Nombre))
            {
                _lsMensajes.Add("Nombre no puede ser vacío.");
                _exito = false;
            }
            if (string.IsNullOrEmpty(Objeto.DestinoCampana))
            {
                _lsMensajes.Add("El grupo a enviar no puede ser vacio.");
                _exito = false;
            }
            if (Objeto.Programacion)
            {
                if (!Objeto.FechaProgramacion.HasValue && Objeto.FechaProgramacion.Value.Year == 0)
                {
                    _lsMensajes.Add("La fecha de programcion no puede ser vacia.");
                    _exito = false;
                }
            }
            if (string.IsNullOrEmpty(Objeto.MensajePrevio))
            {
                _lsMensajes.Add("El mensaje previo no puede ser vacio.");
                _exito = false;
            }
            if (string.IsNullOrEmpty(Objeto.MensajePrevio))
            {
                _lsMensajes.Add("El mensaje previo no puede ser vacio.");
                _exito = false;
            }
            if (string.IsNullOrEmpty(Objeto.TipoCampana))
            {
                _lsMensajes.Add("El tipo de campaña no puede ser vacio.");
                _exito = false;
            }
            else
            {
                if (Objeto.TipoCampana.ToUpper() == "PROMOCION")
                {
                    if (Objeto.PromocionId <= 0)
                    {
                        _lsMensajes.Add("El el identificador de promocion no puede ser menor o igual que cero.");
                        _exito = false;
                    }
                }
            }
          
            return _exito;
        }

        public static bool ActualizarVR(Campana Objeto)
        {
            _exito = true;
            _lsMensajes.Clear();

            if (Objeto.CampanaId <= 0)
            {
                _lsMensajes.Add("El identificador de la campaña no puede ser menor o igual a cero.");
                _exito = false;
            }
            if (string.IsNullOrEmpty(Objeto.Nombre))
            {
                _lsMensajes.Add("Nombre no puede ser vacío.");
                _exito = false;
            }
            if (string.IsNullOrEmpty(Objeto.DestinoCampana))
            {
                _lsMensajes.Add("El grupo a enviar no puede ser vacio.");
                _exito = false;
            }
            if (Objeto.Programacion)
            {
                if (!Objeto.FechaProgramacion.HasValue && Objeto.FechaProgramacion.Value.Year == 0)
                {
                    _lsMensajes.Add("La fecha de programcion no puede ser vacia.");
                    _exito = false;
                }
            }
            if (string.IsNullOrEmpty(Objeto.MensajePrevio))
            {
                _lsMensajes.Add("El mensaje previo no puede ser vacio.");
                _exito = false;
            }
            if (string.IsNullOrEmpty(Objeto.TipoCampana))
            {
                _lsMensajes.Add("El tipo de campaña no puede ser vacio.");
                _exito = false;
            }
            else
            {
                if (Objeto.TipoCampana.ToUpper() == "PROMOCION")
                {
                    if (Objeto.PromocionId <= 0)
                    {
                        _lsMensajes.Add("El el identificador de promocion no puede ser menor o igual que cero.");
                        _exito = false;
                    }
                }
            }

            return _exito;
        }

        public static bool EliminarVR(Campana Objeto)
        {
            _exito = true;
            _lsMensajes.Clear();
            return _exito;
        }
    }
}