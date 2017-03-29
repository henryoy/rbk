namespace cm.mx.catalogo.Model
{
    public class NivelUsuario
    {
        public virtual string Color { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual int NivelUsuarioID { get; set; }
        public virtual string Nombre { get; set; }
        public virtual float Pocentaje { get; set; }
        public virtual string UrlColor { get; set; }
        public virtual int VisitaMax { get; set; }
        public virtual int VisitaMin { get; set; }
    }
}