using System;
using System.Runtime.Serialization;

namespace cm.mx.catalogo.Model
{
    [Serializable()]
    [DataContract]
    public class Notificacion
    {
        [DataMember]
        public virtual string Estatus { get; set; }
        [DataMember]
        public virtual DateTime FechaRegistro { get; set; }
        [DataMember]
        public virtual string Mensaje { get; set; }
        [DataMember]
        public virtual int NotificacionID { get; set; }
        [DataMember]
        public virtual int PromocionID { get; set; }
        [DataMember]
        public virtual string Referencia { get; set; }
        [DataMember]
        public virtual int SucursalId { get; set; }
        [DataMember]
        public virtual string Tipo { get; set; }
        [DataMember]
        public virtual int UsuarioID { get; set; }
        public virtual int UsuarioAlta { get; set; }
        [DataMember]
        public virtual DateTime Vigencia { get; set; }
        [DataMember]
        public virtual Usuario Usuario { get; set; }
        [DataMember]
        public virtual string FolioNota { get; set; }
        [DataMember]
        public virtual bool Enviado { get; set; }

        [DataMember]
        public virtual bool Relacionado { get; set; }
        [DataMember]
        public virtual decimal ImporteVisita { get; set; }
    }
}