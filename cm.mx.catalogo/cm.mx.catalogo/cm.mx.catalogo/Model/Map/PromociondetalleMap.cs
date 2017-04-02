using FluentNHibernate.Mapping;

namespace cm.mx.catalogo.Model.Map
{
    public class PromociondetalleMap : ClassMap<Promociondetalle>
    {
        public PromociondetalleMap()
        {
            Table("PromocionDetalle");
            LazyLoad();
            Id(x => x.Promociondetalleid).GeneratedBy.Identity().Column("PromocionDetalleId");
            References(x => x.Promocion).Column("PromocionId"); 
            Map(x => x.Valor1).Column("Valor1").Not.Nullable();
            Map(x => x.Valor2).Column("Valor2").Not.Nullable();
            Map(x => x.Condicion).Column("Condicion").Not.Nullable();
            Map(x => x.Todos).Column("Todos").Not.Nullable();
        }
    }
}