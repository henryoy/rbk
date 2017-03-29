using FluentNHibernate.Mapping;

namespace cm.mx.catalogo.Model.Map
{
    public class PromocionmembresiaMap : ClassMap<Promocionmembresia>
    {
        public PromocionmembresiaMap()
        {
            Table("PromocionMembresia");
            LazyLoad();
            CompositeId().KeyProperty(x => x.Promocionid, "PromocionId")
                         .KeyProperty(x => x.Membresiaid, "MembresiaId");
            References(x => x.Promocion).Column("PromocionId");
            References(x => x.Tipomembresia).Column("MembresiaId");
        }
    }
}