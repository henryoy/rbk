using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.mx.catalogo.Model
{
    [Serializable]
    public class PromocionDetalleVM
    {
        public int Promociondetalleid { get; set; }
        public string Valor1 { get; set; }
        public string Valor2 { get; set; }
        public string Condicion { get; set; }
        public bool Todos { get; set; }
        public string Cambio { get; set; }
    }
}
