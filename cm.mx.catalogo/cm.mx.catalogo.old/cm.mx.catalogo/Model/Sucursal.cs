using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace cm.mx.catalogo.Model
{
    [Serializable]
    [DataContract]
    public class Sucursal
    {
        [DataMember]
        public virtual float Latitud { get; set; }
        [DataMember]
        public virtual float Longitud { get; set; }
        [DataMember]
        public virtual string Nombre { get; set; }
        [DataMember]
        public virtual int SucursalID { get; set; }
        [DataMember]
        public virtual string Direccion { get; set; }
        [DataMember]
        public virtual string LinkFacebook { get; set; }
        [DataMember]
        public virtual string Estado { get; set; }
        [DataMember]
        public virtual int UsuarioBaja { get; set; }
        [DataMember]
        public virtual DateTime FechaBaja { get; set; }
        [DataMember]
        public virtual IList<Promocionsucursal> PromocionSucursal { get; set; }

        public Sucursal()
        {
            PromocionSucursal = new List<Promocionsucursal>();
        }
    }
}