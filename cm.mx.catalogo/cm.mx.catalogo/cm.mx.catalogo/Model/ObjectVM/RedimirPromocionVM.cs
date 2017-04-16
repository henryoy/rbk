using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.mx.catalogo.Model
{
    public class RedimirPromocionVM
    {
        public int PromocionRedimirId { get; set; }
        public int UsuarioId { get; set; }
        public int UsuarioRedimioId { get; set; }
        public int  SucursalId { get; set; }
        public int PromocionId { get; set; }
    }
}
