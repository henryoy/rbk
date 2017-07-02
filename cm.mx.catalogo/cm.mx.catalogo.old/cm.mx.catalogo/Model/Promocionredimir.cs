using System;
using System.Text;
using System.Collections.Generic;


namespace cm.mx.catalogo.Model
{
    [Serializable]
    public partial class Promocionredimir
    {
        public virtual int PromocionRedimirId { get; set; }
        public virtual int SucursalId { get; set; }
        public virtual int UsuarioRedimioId { get; set; }
        public virtual DateTime FechaRedimir { get; set; }

        public virtual Promocion Promocion { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
