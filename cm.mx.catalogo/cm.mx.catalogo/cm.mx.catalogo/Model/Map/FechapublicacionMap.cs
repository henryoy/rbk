using FluentNHibernate.Mapping;

namespace cm.mx.catalogo.Model.Map
{
    public class FechapublicacionMap : ClassMap<Fechapublicacion>
    {
        public FechapublicacionMap()
        {
            Table("FechaPublicacion");
            LazyLoad();
            Id(x => x.Fechapublicacionid).GeneratedBy.Identity().Column("FechaPublicacionId");
            References(x => x.Promocion).Column("PromocionId");
            Map(x => x.Estado).Column("Estado").Not.Nullable();
            Map(x => x.Fechainicio).Column("FechaInicio").Not.Nullable();
            Map(x => x.Fechafin).Column("FechaFin").Not.Nullable();
            Map(x => x.Fechaalta).Column("FechaAlta").Not.Nullable();
            Map(x => x.Usuarioalta).Column("UsuarioAlta").Not.Nullable();
            Map(x => x.Fechabaja).Column("FechaBaja").Not.Nullable();
            Map(x => x.Usuariobaja).Column("UsuarioBaja").Not.Nullable();
        }
    }
}