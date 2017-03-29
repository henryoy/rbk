using System;

namespace cm.mx.catalogo.Model
{
    [Serializable()]
    public class Tarjeta
    {
        public virtual string Descripcion { get; set; }
        public virtual string Estatus { get; set; }
        public virtual int TarjetaID { get; set; }
        public virtual string UrlImagen { get; set; }
        public virtual int Nivel { get; set; }
    }
}