using FluentNHibernate.Mapping;

namespace cm.mx.catalogo.Model.Map
{
    public class TipoInteresMap : ClassMap<TipoInteres>
    {
        public TipoInteresMap()
        {
            Id(x => x.TipoInteresID);
            Map(x => x.Descripcion);
            Map(x => x.Nombre);
        }
    }
}
