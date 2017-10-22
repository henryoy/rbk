using System;
using System.Text;
using System.Collections.Generic;
using Iesi.Collections.Generic;


namespace cm.mx.catalogo.Model
{
    [Serializable]
    public class Imagentetri {
        public virtual int ID { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual string Imagen { get; set; }
        public virtual string Codigo { get; set; }
    }
}
