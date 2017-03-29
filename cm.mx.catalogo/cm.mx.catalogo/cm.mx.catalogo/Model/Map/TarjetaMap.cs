using FluentNHibernate.Mapping;

namespace cm.mx.catalogo.Model.Mappings
{
    public class TarjetaMap : ClassMap<Tarjeta>
    {
        public TarjetaMap()
        {
            Id(x => x.TarjetaID);
            Map(x => x.Descripcion);
            Map(x => x.Estatus);
            Map(x => x.UrlImagen);
            Map(x => x.Nivel);
        }
    }
}