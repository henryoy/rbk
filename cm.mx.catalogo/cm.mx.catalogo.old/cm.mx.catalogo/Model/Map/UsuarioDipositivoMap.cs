using FluentNHibernate.Mapping;

namespace cm.mx.catalogo.Model.Map
{
    public class UsuarioDispositivoMap : ClassMap<UsuarioDispositivo>
    {
        public UsuarioDispositivoMap()
        {
            Id(x => x.UsuarioDispositivoId).GeneratedBy.Identity();
            Map(x => x.UsuarioId).Formula("UsuarioID");
            Map(x => x.Token);
            Map(x => x.Dispositivo);
            Map(x => x.Serie);
            Map(x => x.Plataforma);
            Map(x => x.FechaAlta);
            References(x => x.Usuario).Column("UsuarioId").ForeignKey("UsuarioID");
        }
    }
}