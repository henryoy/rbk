using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace cm.mx.catalogo.Model
{
    [Serializable]
    [DataContract]
    public class Tipomembresia
    {
        public Tipomembresia()
        {
            Promocionmembresia = new List<Promocionmembresia>();
        }

        [DataMember]
        public virtual int Membresiaid { get; set; }
        [DataMember]
        public virtual string Nombre { get; set; }
        [DataMember]
        public virtual int NumeroDeVisitas { get;set; }
        [DataMember]
        public virtual int ApartirDe { get; set; }
        [DataMember]
        public virtual int Hasta { get; set; }
        [DataMember]
        public virtual decimal? Porcientodescuento { get; set; }
        [DataMember]
        public virtual string Color { get; set; }
        [DataMember]
        public virtual string Estado { get; set; }
        [DataMember]
        public virtual string UrlImagen { get; set; }
        public virtual IList<Promocionmembresia> Promocionmembresia { get; set; }
    }
}