using System.Collections.Generic;

namespace cm.mx.catalogo.Model
{
    public class Tipomembresia
    {
        public Tipomembresia()
        {
            Promocionmembresia = new List<Promocionmembresia>();
        }

        public virtual int Membresiaid { get; set; }
        public virtual string Nombre { get; set; }
        public virtual int ApartirDe { get; set; }
        public virtual int Hasta { get; set; }
        public virtual decimal? Porcientodescuento { get; set; }
        public virtual string Color { get; set; }
        public virtual string Estado { get; set; }
        public virtual string UrlImagen { get; set; }
        public virtual IList<Promocionmembresia> Promocionmembresia { get; set; }
    }
}