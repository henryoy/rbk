using FluentNHibernate.Mapping;

namespace cm.mx.catalogo.Model.Mappings
{
    public class SucursalMap : ClassMap<Sucursal>
    {
        public SucursalMap()
        {
            Id(x => x.SucursalID);
            Map(x => x.Latitud);
            Map(x => x.Longitud);
            Map(x => x.Nombre);
            Map(x => x.Direccion);
            HasMany(x => x.PromocionSucursal).KeyColumn("SucursalID");
        }
    }
}