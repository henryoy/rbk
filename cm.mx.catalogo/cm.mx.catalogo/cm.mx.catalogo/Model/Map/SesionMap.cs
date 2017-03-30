using FluentNHibernate.Mapping;

namespace cm.mx.catalogo.Model.Mappings
{
    public class SesionMap : ClassMap<Sesion>
    {
        public SesionMap()
        {
            Id(x => x.SesionID);
            Map(x => x.Estatus);
            Map(x => x.Token);
            Map(x => x.UsuarioID);
        }
    }
}