using System;

namespace cm.mx.catalogo.Model
{
    public class Usuario
    {
        public virtual int Usuarioid { get; set; }
        public virtual string Email { get; set; }
        public virtual string Contrasena { get; set; }
        public virtual string VerificacionContrasena { get; set; }
        public virtual string Tipo { get; set; }
        public virtual DateTime FechaAlta { get; set; }
        public virtual DateTime FechaBaja { get; set; }
        public virtual string Estatus { get; set; }
        public virtual int TarjetaID { get;set; }
        public virtual string Nombre { get; set; }
        public virtual DateTime FechaNacimiento { get; set; }
        public virtual string Imagen { get; set; }
        public virtual int VisitaActual { get; set; }
        public virtual int VisitaGlobal { get; set; }
        public virtual string Codigo { get; set; }
        public virtual Tarjeta oTargeta { get; set; }
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