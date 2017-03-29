using FluentNHibernate.Mapping;

namespace cm.mx.catalogo.Model.Mappings
{
    public class ActivacionMap : ClassMap<Activacion>
    {
        public ActivacionMap()
        {
            Id(x => x.ActivacionId).GeneratedBy.Identity();
            Map(x => x.Activado);
            Map(x => x.Email);
            Map(x => x.FechaAlta);
            Map(x => x.FechaVencimiento);
            Map(x => x.Llave);
            Map(x => x.UsuarioId);
        }
    }
}