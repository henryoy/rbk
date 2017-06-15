using FluentNHibernate.Mapping;

namespace cm.mx.catalogo.Model.Map
{
    public class NotificacionMap : ClassMap<Notificacion>
    {
        public NotificacionMap()
        {
            Id(x => x.NotificacionID);
            Map(x => x.Estatus);
            Map(x => x.FechaRegistro);
            Map(x => x.FolioNota);
            Map(x => x.Mensaje);
            Map(x => x.PromocionID);
            Map(x => x.Referencia);
            Map(x => x.SucursalId);
            Map(x => x.Tipo);
            Map(x => x.UsuarioID).Formula("UsuarioID");
            Map(x => x.UsuarioAlta);
            Map(x => x.Vigencia);
            References(x => x.Usuario).Column("UsuarioID").ForeignKey("UsuarioID").Not.LazyLoad();
            Map(x => x.Enviado);

            Map(x => x.Relacionado);
            Map(x => x.ImporteVisita);

            Map(x => x.ErrorRelacion);
            Map(x => x.SubTipo);
            Map(x => x.CampanaId);
        }
    }
}