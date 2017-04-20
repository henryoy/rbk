using FluentNHibernate.Mapping;

namespace cm.mx.catalogo.Model.Map
{
    public class PromocionmembresiaMap : ClassMap<Promocionmembresia>
    {
        public PromocionmembresiaMap()
        {
            Table("PromocionMembresia");
            LazyLoad();
            CompositeId().KeyReference(x => x.Promocion, "PromocionId")
                         .KeyProperty(x => x.Membresiaid, "MembresiaId");
             //References(x => x.Promocion).Column("PromocionId").Not.Insert().Not.Update();
            //References(x => x.Tipomembresia).Column("MembresiaId").Not.Insert().Not.Update();
        }
    }
}