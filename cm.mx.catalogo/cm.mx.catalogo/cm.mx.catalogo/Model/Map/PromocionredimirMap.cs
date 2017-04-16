using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;
using cm.mx.catalogo.Model; 

namespace cm.mx.catalogo.Model.Map {
    
    
    public partial class PromocionredimirMap : ClassMap<Promocionredimir> {
        
        public PromocionredimirMap() {
			Table("PromocionRedimir");
			LazyLoad();
			Id(x => x.PromocionRedimirId).GeneratedBy.Identity().Column("PromocionRedimirId");
            References(x => x.Promocion).Column("PromocionId").ReadOnly();
            References(x => x.Usuario).Column("UsuarioId").ReadOnly();
			Map(x => x.SucursalId).Column("SucursalId").Not.Nullable().Precision(10);
			Map(x => x.UsuarioRedimioId).Column("UsuarioRedimioId").Not.Nullable().Precision(10);
			Map(x => x.FechaRedimir).Column("FechaRedimir").Not.Nullable();
        }
    }
}
