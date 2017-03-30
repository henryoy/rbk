using FluentNHibernate.Mapping;

namespace cm.mx.catalogo.Model.Map
{
    public class PromocionMap : ClassMap<Promocion>
    {
        public PromocionMap()
        {
            Table("Promocion");
            LazyLoad();
            Id(x => x.Promocionid).GeneratedBy.Identity().Column("PromocionId");
            Map(x => x.Titulo).Column("Titulo").Not.Nullable();
            Map(x => x.Descripcion).Column("Descripcion").Not.Nullable();
            Map(x => x.Vigenciainicial).Column("VigenciaInicial").Not.Nullable();
            Map(x => x.Vigenciafinal).Column("VigenciaFinal").Not.Nullable();
            Map(x => x.Fechaalta).Column("FechaAlta").Not.Nullable();
            Map(x => x.Usuarioaltaid).Column("UsuarioAltaId").Not.Nullable();
            Map(x => x.Fechabaja).Column("FechaBaja").Not.Nullable();
            Map(x => x.Usuariobajaid).Column("UsuarioBajaId").Not.Nullable();
            Map(x => x.Estado).Column("Estado").Not.Nullable();
            Map(x => x.Tipomembresia).Column("TipoMembresia").Not.Nullable();
            Map(x => x.Descuento).Column("Descuento").Not.Nullable();
            Map(x => x.Tipocliente).Column("TipoCliente").Not.Nullable();
            Map(x => x.Resumen).Column("Resumen").Not.Nullable();
            HasMany(x => x.Fechapublicacion).KeyColumn("PromocionId");
            HasMany(x => x.Promociondetalle).KeyColumn("PromocionId").Cascade.All();
            HasMany(x => x.Promocionmembresia).KeyColumn("PromocionId");
        }
    }
}