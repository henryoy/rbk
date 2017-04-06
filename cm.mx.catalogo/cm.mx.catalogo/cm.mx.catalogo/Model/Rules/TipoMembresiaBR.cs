using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.mx.catalogo.Model.Rules
{
    public class TipoMembresiaBR
    {
        List<string> _mesajes = new List<string>();
        public List<string> Mensajes
        {
            get
            {
                return _mesajes;
            }
        }
        TipoMembresiaRepository rTipoMembresia;

        public bool Insertar(Tipomembresia obj)
        {
            _mesajes = new List<string>();
            rTipoMembresia = new TipoMembresiaRepository();
            if (rTipoMembresia.ExisteRegistro(obj.Membresiaid, obj.ApartirDe))
            {
                _mesajes.Add("ApartirDe se encuentra en el rago de otra membresia");
            }
            if (rTipoMembresia.ExisteRegistro(obj.Membresiaid, obj.Hasta))
            {
                _mesajes.Add("Hasta se encuentra en el rago de otra membresia");
            }
            return _mesajes.Count() == 0;
        }
    }
}
