using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.mx.catalogo.Model.Rules
{
    public class TipoInteresVR
    {
        List<string> _mensajes = new List<string>();
        public List<string> Mensajes
        {
            get
            {
                return _mensajes;
            }
        }

        public bool Insertar(TipoInteres obj)
        {
            _mensajes.Clear();
            if (string.IsNullOrEmpty(obj.Descripcion))
            {
                _mensajes.Add("Descripción no puede ser vacio.");
            }

            if (string.IsNullOrEmpty(obj.Nombre))
            {
                _mensajes.Add("Nombre no puede ser vacio.");
            }

            return _mensajes.Count() == 0;
        }
    }
}
