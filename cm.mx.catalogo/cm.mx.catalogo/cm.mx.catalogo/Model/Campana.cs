using System;
using System.Text;
using System.Collections.Generic;


namespace cm.mx.catalogo.Model {
    [Serializable]
    public partial class Campana {
        public virtual int CampanaId { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string MensajePrevio { get; set; }
        public virtual string TipoCampana { get; set; }
        public virtual bool Programacion { get; set; }
        public virtual DateTime? FechaProgramacion { get; set; }
        public virtual string DestinoCampana { get; set; }
        public virtual int? PromocionId { get; set; }
        public virtual int DistribucionId { get; set; }
        public virtual int PlantillaId { get; set; }
    }
}
