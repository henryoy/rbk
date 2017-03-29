namespace cm.mx.catalogo.Model
{
    public class Promociondetalle
    {
        public Promociondetalle()
        {
            Promocion = new Promocion();
        }

        public virtual void AddPromocion(Promocion oPromocion)
        {
            Promocion = oPromocion;
            oPromocion.Promociondetalle.Add(this);
        }

        public virtual int Promociondetalleid { get; set; }
        public virtual Promocion Promocion { get; set; }
        public virtual string Valor1 { get; set; }
        public virtual string Valor2 { get; set; }
        public virtual string Condicion { get; set; }
        public virtual bool Todos { get; set; }
    }
}