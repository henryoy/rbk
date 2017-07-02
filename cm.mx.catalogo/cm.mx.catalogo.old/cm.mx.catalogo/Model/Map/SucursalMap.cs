using FluentNHibernate.Mapping;

namespace cm.mx.catalogo.Model.Map
{
    public class SucursalMap : ClassMap<Sucursal>
    {
        public SucursalMap()
        {
            Id(x => x.SucursalID);
            Map(x => x.Latitud);
            Map(x => x.Longitud);
            Map(x => x.Nombre);
            Map(x => x.LinkFacebook);
            Map(x => x.Direccion);
            Map(x => x.Estado);
            Map(x => x.FechaBaja);
            Map(x => x.UsuarioBaja);
            HasMany(x => x.PromocionSucursal).KeyColumn("SucursalID");
        }
    }
}