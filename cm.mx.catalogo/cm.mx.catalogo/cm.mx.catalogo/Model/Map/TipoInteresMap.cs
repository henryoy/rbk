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
            HasManyToMany(x => x.Usuarios).Cascade.None().Table("UsuarioTipoInteres").ParentKeyColumn("UsuarioId").ChildKeyColumn("TipoInteresID").ReadOnly();
        }
    }
}
