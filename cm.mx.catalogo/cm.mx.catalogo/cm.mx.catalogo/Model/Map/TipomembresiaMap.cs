using FluentNHibernate.Mapping;

namespace cm.mx.catalogo.Model.Map
{
    public class TipomembresiaMap : ClassMap<Tipomembresia>
    {
        public TipomembresiaMap()
        {
            Table("TipoMembresia");
            LazyLoad();
            Id(x => x.Membresiaid).GeneratedBy.Identity().Column("MembresiaId");
            Map(x => x.Nombre).Column("Nombre").Not.Nullable();
            Map(x => x.ApartirDe).Column("ApartirDe").Not.Nullable();
            Map(x => x.Hasta).Column("Hasta").Not.Nullable();
            Map(x => x.Porcientodescuento).Column("PorcientoDescuento");
            Map(x => x.Color).Column("Color").Not.Nullable();
            Map(x => x.Estado).Column("Estado").Not.Nullable();
            Map(x => x.UrlImagen).Column("UrlImagen").Not.Nullable();
            HasMany(x => x.Promocionmembresia).KeyColumn("MembresiaId");
        }
    }
}