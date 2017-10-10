using System;
namespace cm.mx.catalogo.Model
{
    [Serializable]
    public class PromociondetalleTetris
    {
        public PromociondetalleTetris()
        {
            Promocion = new Promocion();
        }

        public virtual void AddPromocion(Promocion oPromocion)
        {
            Promocion = oPromocion;
            oPromocion.PromociondetalleTetris.Add(this);
        }

        public virtual int Promociondetalleid { get; set; }
        public virtual Promocion Promocion { get; set; }
        public virtual string Valor1 { get; set; }
        public virtual string Valor2 { get; set; }
        public virtual string Condicion { get; set; }
        public virtual bool Todos { get; set; }
        public virtual string Cambio { get; set; }
    }
}