using System;
using System.Collections.Generic;
using System.Text;
using FluentNHibernate.Mapping;
using cm.mx.catalogo.Model;

namespace cm.mx.catalogo.Model.Mappings
{


    public partial class PromocionusuarioMap : ClassMap<PromocionUsuario>
    {
        public PromocionusuarioMap()
        {
            Table("PromocionUsuario");
            LazyLoad();
            CompositeId().KeyProperty(x => x.PromocionId, "PromocionId")
                         .KeyProperty(x => x.UsuarioId, "UsuarioId");
            //References(x => x.Promocion).Column("PromocionId");
            Map(x => x.Estatus).Column("Estatus").Not.Nullable().Length(10);
            Map(x => x.FechaAplicada).Column("FechaAplicada").Not.Nullable();
            Map(x => x.FechaAlta).Column("FechaAlta").Not.Nullable();
        }

    }
}
