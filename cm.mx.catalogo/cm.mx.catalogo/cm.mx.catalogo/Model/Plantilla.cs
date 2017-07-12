using System;
using System.Text;
using System.Collections.Generic;


namespace cm.mx.catalogo.Model {
    [Serializable()]
    public partial class Plantilla {
        public virtual int PlantillaId { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string Html { get; set; }
        public virtual string Estatus { get; set; }
    }
}
