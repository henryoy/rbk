using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace cm.mx.catalogo.Model
{
    [Serializable]
    [DataContract]
    public class Usuario
    {
        public Usuario()
        {
            Intereses = new List<TipoInteres>();
            Notificaciones = new List<Notificacion>();
        }

        public virtual void Addinteres(TipoInteres oInteres)
        {
            if (!Intereses.Any(a => a.TipoInteresID == oInteres.TipoInteresID))
            {
                oInteres.Usuarios.Add(this);
                Intereses.Add(oInteres);
            }
        }

        public virtual void AddNotifiacion(Notificacion oNotificaion)
        {
            oNotificaion.Usuario = this;
            Notificaciones.Add(oNotificaion);
        }

        [DataMember]
        public virtual int Usuarioid { get; set; }
        [DataMember]
        public virtual string Email { get; set; }
        [DataMember]
        public virtual string Contrasena { get; set; }
        [DataMember]
        public virtual string VerificacionContrasena { get; set; }
        [DataMember]
        public virtual string Tipo { get; set; }
        [DataMember]
        public virtual DateTime FechaAlta { get; set; }
        [DataMember]
        public virtual DateTime FechaBaja { get; set; }
        [DataMember]
        public virtual string Estatus { get; set; }
        [DataMember]
        public virtual int TarjetaID { get; set; }
        [DataMember]
        public virtual string Nombre { get; set; }
        [DataMember]
        public virtual DateTime FechaNacimiento { get; set; }
        [DataMember]
        public virtual string Imagen { get; set; }
        [DataMember]
        public virtual int VisitaActual { get; set; }
        [DataMember]
        public virtual int VisitaGlobal { get; set; }
        [DataMember]
        public virtual string Codigo { get; set; }
        
        public virtual Tipomembresia oTarjeta { get; set; }
        
        public virtual IList<TipoInteres> Intereses { get; set; }
        
        public virtual IList<Notificacion> Notificaciones { get; set; }
        //public virtual string Correo { get; set; }
        //public virtual string Nombre { get; set; }
        //public virtual string Password { get; set; }
        //public virtual DateTime Fechanacimiento { get; set; }
        //public virtual int Totalvisitas { get; set; }
        //public virtual int Visitasactuales { get; set; }
        //public virtual DateTime Fecharegistro { get; set; }
        //public virtual string Tipologin { get; set; }
        //public virtual DateTime Ultimoinicio { get; set; }
        //public virtual int Membresiaactual { get; set; }
        //public virtual string Tipo { get; set; }
        //public virtual string Codigo { get; set; }
    }
}