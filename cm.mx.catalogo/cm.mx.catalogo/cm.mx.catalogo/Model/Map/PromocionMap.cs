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
            Map(x => x.Vigenciainicial).Column("VigenciaInicial").Nullable();
            Map(x => x.Vigenciafinal).Column("VigenciaFinal").Nullable();
            Map(x => x.Fechaalta).Column("FechaAlta").Not.Nullable();
            Map(x => x.Usuarioaltaid).Column("UsuarioAltaId").Not.Nullable();
            Map(x => x.Fechabaja).Column("FechaBaja").Not.Nullable();
            Map(x => x.Usuariobajaid).Column("UsuarioBajaId").Not.Nullable();
            Map(x => x.Estado).Column("Estado").Not.Nullable();
            Map(x => x.Tipomembresia).Column("TipoMembresia").Not.Nullable();
            Map(x => x.Descuento).Column("Descuento").Not.Nullable();
            Map(x => x.Tipocliente).Column("TipoCliente").Not.Nullable();
            Map(x => x.Resumen).Column("Resumen").Not.Nullable();
            Map(x => x.TerminosCondiciones).Column("TerminosCondiciones").Nullable();
            Map(x => x.ImagenUrl).Column("ImagenUrl").Nullable();
            HasMany(x => x.Fechapublicacion).KeyColumn("PromocionId").Cascade.None();
            HasMany(x => x.Promociondetalle).KeyColumn("PromocionId").Cascade.All().Inverse();
            HasMany(x => x.Promocionmembresia).KeyColumn("PromocionId").Cascade.All().Inverse();
            //HasMany(x => x.PromocionRedimir).KeyColumn("PromocionId").Cascade.None().ReadOnly();
            //HasMany(x => x.Promocionusuario).KeyColumn("PromocionId").Cascade.None().ReadOnly();
            HasMany(x => x.Promocionsucursal).KeyColumn("PromocionId").Cascade.All().Inverse();
            HasMany(x => x.Promocionnotificacion).KeyColumn("PromocionId").Cascade.All().Inverse();

        }
    }
}