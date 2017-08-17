using FluentNHibernate.Mapping;

namespace cm.mx.catalogo.Model.Map
{
    public class UsuarioMap : ClassMap<Usuario>
    {
        public UsuarioMap()
        {
            Table("Usuario");
            LazyLoad();

            Id(x => x.Usuarioid).GeneratedBy.Identity().Column("UsuarioId");
            Map(x => x.Email).Column("Email").Not.Nullable();
            Map(x => x.Contrasena).Column("Contrasena").Not.Nullable();
            Map(x => x.VerificacionContrasena).Column("VerificacionContrasena").Not.Nullable();
            Map(x => x.Tipo).Column("Tipo").Not.Nullable();
            Map(x => x.FechaAlta).Column("FechaAlta").Not.Nullable();
            Map(x => x.FechaBaja).Column("FechaBaja").Not.Nullable();
            Map(x => x.Estatus).Column("Estatus").Not.Nullable();
            //Map(x => x.TarjetaID).Column("TarjetaID").Formula("TarjetaID");
            Map(x => x.Nombre).Column("Nombre").Not.Nullable();
            Map(x => x.FechaNacimiento).Column("FechaNacimiento").Not.Nullable();
            Map(x => x.Imagen).Column("Imagen").Not.Nullable();
            Map(x => x.VisitaActual).Column("VisitaActual").Not.Nullable();
            Map(x => x.VisitaGlobal).Column("VisitaGlobal").Not.Nullable();
            Map(x => x.Codigo).Column("Codigo").Not.Nullable();
            Map(x => x.Origen).Column("Origen").Not.Nullable();
            Map(x => x.IdExterno).Column("IdExterno").Nullable();
            Map(x => x.ImporteActual);
            Map(x => x.ImporteTotal);
            Map(x => x.MRSuscriberId);
            References(x => x.oTarjeta).Column("TarjetaID").Cascade.None().Not.LazyLoad();
            HasManyToMany(x => x.Intereses).Cascade.None().Table("UsuarioTipoInteres").ParentKeyColumn("UsuarioId").ChildKeyColumn("TipoInteresID").Not.LazyLoad();
            HasMany(x => x.Notificaciones).KeyColumn("UsuarioId").Cascade.All().Inverse();

            Map(x => x.UltimaSerie);
            Map(x => x.UltimaPlataforma);
            Map(x => x.UltimoInicio);
            //HasOne(x => x.oTargeta).Cascade.None().ForeignKey("TarjetaID");
            //.Formula("TarjetaID");
            //HasOne(x=>x.TarjetaID)
        }
    }
}