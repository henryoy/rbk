using System;
using System.Runtime.Serialization;

namespace cm.mx.catalogo.Model
{
    public class ProductoVenta
    {
        public ProductoVenta()
        {
            Descripcion = "";
        }

        public virtual int ID { get; set; }
        public virtual int FolioFactura { get; set; }
        public virtual int Check { get; set; }
        public virtual int Item { get; set; }
        public virtual decimal Cantidad { get; set; }
        public virtual int Asiento { get; set; }
        public virtual decimal Precio { get; set; }
        public virtual string Descripcion { get; set; }
    }
}