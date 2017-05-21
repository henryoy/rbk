using FluentNHibernate.Mapping;

namespace cm.mx.catalogo.Model.Map
{
    public class TipoInteresMap : ClassMap<TipoInteres>
    {
        public TipoInteresMap()
        {
            Id(x => x.TipoInteresID);
            Map(x => x.Estatus);
            Map(x => x.Descripcion);
            Map(x => x.FechaAlta);
            Map(x => x.FechaBaja);
            Map(x => x.Nombre);
            Map(x => x.UsuarioAlta);
            Map(x => x.UsuarioBaja);
            HasManyToMany(x => x.Usuarios).Cascade.None().Table("UsuarioTipoInteres").ParentKeyColumn("UsuarioId").ChildKeyColumn("TipoInteresID").ReadOnly();
        }
    }
}
