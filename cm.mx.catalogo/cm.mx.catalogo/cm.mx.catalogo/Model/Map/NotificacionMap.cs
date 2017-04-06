using FluentNHibernate.Mapping;

namespace cm.mx.catalogo.Model.Map
{
    public class NotificacionMap : ClassMap<Notificacion>
    {
        public NotificacionMap()
        {
            Id(x => x.NotifiacionID);
            Map(x => x.Estatus);
            Map(x => x.FechaRegistro);
            Map(x => x.Mensaje);
            Map(x => x.PromocionID);
            Map(x => x.UsuarioID);
            Map(x => x.Vigencia);
        }
    }
}