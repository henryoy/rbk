using System;
using System.Text;
using System.Collections.Generic;
using Iesi.Collections.Generic;


namespace cm.mx.catalogo.Model {
    [Serializable]
    public class Vicampana {
        public virtual int CampanaId { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string TipoCampana { get; set; }
        public virtual string DestinoCampana { get; set; }
        public virtual int? MRCampanaId { get; set; }
    }
}
