﻿using System;
using System.Collections.Generic;
namespace cm.mx.catalogo.Model
{
    public class Sucursal
    {
        public virtual float Latitud { get; set; }
        public virtual float Longitud { get; set; }
        public virtual string Nombre { get; set; }
        public virtual int SucursalID { get; set; }
        public virtual string Direccion { get; set; }
        public virtual string LinkFacebook { get; set; }
        public virtual string Estado { get; set; }
        public virtual int UsuarioBaja { get; set; }
        public virtual DateTime FechaBaja { get; set; }
        public virtual IList<Promocionsucursal> PromocionSucursal { get; set; }

        public Sucursal()
        {
            PromocionSucursal = new List<Promocionsucursal>();
        }
    }
}