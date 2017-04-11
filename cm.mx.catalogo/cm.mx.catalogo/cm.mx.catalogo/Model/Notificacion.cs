using System;

namespace cm.mx.catalogo.Model
{
    [Serializable()]
    public class Notificacion
    {
        public virtual string Estatus { get; set; }
        public virtual DateTime FechaRegistro { get; set; }
        public virtual string Mensaje { get; set; }
        public virtual int NotifiacionID { get; set; }
        public virtual int PromocionID { get; set; }
        public virtual string Referencia { get; set; }
        public virtual string Tipo { get; set; }
        public virtual int UsuarioID { get; set; }
        public virtual DateTime Vigencia { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}