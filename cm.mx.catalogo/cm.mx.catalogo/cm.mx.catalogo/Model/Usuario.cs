using System;
using System.Collections.Generic;
using System.Linq;

namespace cm.mx.catalogo.Model
{
    public class Usuario
    {
        public Usuario()
        {
            Intereses = new List<TipoInteres>();
        }

        public virtual void Addinteres(TipoInteres oInteres)
        {
            if (!Intereses.Any(a => a.TipoInteresID == oInteres.TipoInteresID))
            {
                oInteres.Usuarios.Add(this);
                Intereses.Add(oInteres);
            }
        }

        public virtual int Usuarioid { get; set; }
        public virtual string Email { get; set; }
        public virtual string Contrasena { get; set; }
        public virtual string VerificacionContrasena { get; set; }
        public virtual string Tipo { get; set; }
        public virtual DateTime FechaAlta { get; set; }
        public virtual DateTime FechaBaja { get; set; }
        public virtual string Estatus { get; set; }
        public virtual int TarjetaID { get; set; }
        public virtual string Nombre { get; set; }
        public virtual DateTime FechaNacimiento { get; set; }
        public virtual string Imagen { get; set; }
        public virtual int VisitaActual { get; set; }
        public virtual int VisitaGlobal { get; set; }
        public virtual string Codigo { get; set; }
        public virtual Tarjeta oTargeta { get; set; }
        public virtual IList<TipoInteres> Intereses { get; set; }
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