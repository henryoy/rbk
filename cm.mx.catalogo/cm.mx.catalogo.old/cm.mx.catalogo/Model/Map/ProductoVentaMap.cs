using FluentNHibernate.Mapping;

namespace cm.mx.catalogo.Model.Map
{
    public class ProductoVentaMap : ClassMap<ProductoVenta>
    {
        public ProductoVentaMap()
        {
            Id(x => x.ID).GeneratedBy.Identity();
            Map(x => x.FolioFactura);
            Map(x => x.Check).Column("Cheque");
            Map(x => x.Item);
            Map(x => x.Cantidad);
            Map(x => x.Precio);
            Map(x => x.Descripcion);
            Map(x => x.Asiento);
        }
    }
}