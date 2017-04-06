using FluentNHibernate.Mapping;

namespace cm.mx.catalogo.Model.Map
{
    public class NivelUsuarioMap : ClassMap<NivelUsuario>
    {
        public NivelUsuarioMap()
        {
            Id(x => x.NivelUsuarioID);
            Map(x => x.Color);
            Map(x => x.Descripcion);
            Map(x => x.Nombre);
            Map(x => x.Pocentaje);
            Map(x => x.UrlColor);
            Map(x => x.VisitaMax);
            Map(x => x.VisitaMin);
        }
    }
}