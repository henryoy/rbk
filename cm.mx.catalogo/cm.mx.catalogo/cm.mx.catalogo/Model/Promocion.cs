using System;
using System.Collections.Generic;

namespace cm.mx.catalogo.Model
{
    [Serializable]
    public class Promocion
    {
        public Promocion()
        {
            Fechapublicacion = new List<Fechapublicacion>();
            Promociondetalle = new List<Promociondetalle>();
            PromociondetalleTetris = new List<PromociondetalleTetris>();
            Promocionmembresia = new List<Promocionmembresia>();
            Promocionusuario = new List<PromocionUsuario>();
            Promocionsucursal = new List<Promocionsucursal>();
            Promocionnotificacion = new List<Promocionnotificacion>();
            PromocionRedimir = new List<Promocionredimir>();
        }

        public virtual void AddDetalle(Promociondetalle oDetalle)
        {
            Promociondetalle.Add(oDetalle);
            oDetalle.Promocion = this;
        }

        public virtual void AddDetalleTetris(PromociondetalleTetris oDetalle)
        {
            PromociondetalleTetris.Add(oDetalle);
            oDetalle.Promocion = this;
        }

        public virtual void AddFecha(Fechapublicacion oFecha)
        {
            Fechapublicacion.Add(oFecha);
            oFecha.Promocion = this;
        }

        public virtual void AddMembresia(Promocionmembresia oMembresia)
        {
            Promocionmembresia.Add(oMembresia);
            oMembresia.Promocion = this;       
        }

        public virtual void AddPromoUsuario(PromocionUsuario oPromocionUsuario)
        {
            oPromocionUsuario.Promocion = this;
            Promocionusuario.Add(oPromocionUsuario);
        }
        public virtual void AddSucursal(Promocionsucursal oPromocionSucursal)
        {
            oPromocionSucursal.Promocion = this;
            Promocionsucursal.Add(oPromocionSucursal);
        }
        public virtual int Promocionid { get; set; }
        public virtual string Titulo { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual DateTime? Vigenciainicial { get; set; }
        public virtual DateTime? Vigenciafinal { get; set; }
        public virtual DateTime Fechaalta { get; set; }
        public virtual int Usuarioaltaid { get; set; }
        public virtual DateTime Fechabaja { get; set; }
        public virtual int Usuariobajaid { get; set; }
        public virtual string Estado { get; set; }
        public virtual string Tipomembresia { get; set; }
        public virtual float Descuento { get; set; }
        public virtual string Tipocliente { get; set; }
        public virtual string Resumen { get; set; }
        public virtual string ImagenUrl{ get; set; }
        public virtual string TerminosCondiciones { get; set; }
        public virtual IList<Fechapublicacion> Fechapublicacion { get; set; }
        public virtual IList<Promociondetalle> Promociondetalle { get; set; }
        public virtual IList<PromociondetalleTetris> PromociondetalleTetris { get; set; }
        public virtual IList<Promocionmembresia> Promocionmembresia { get; set; }
        public virtual IList<PromocionUsuario> Promocionusuario { get; set; }
        public virtual IList<Promocionsucursal> Promocionsucursal { get; set; }
        public virtual IList<Promocionnotificacion> Promocionnotificacion { get; set; }
        public virtual IList<Promocionredimir> PromocionRedimir { get; set; }
    }
}