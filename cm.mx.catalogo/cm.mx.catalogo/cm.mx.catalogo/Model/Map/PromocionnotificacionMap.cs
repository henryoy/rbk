using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;
using cm.mx.catalogo.Model; 

namespace cm.mx.catalogo.Model.Mappings {
    
    
    public partial class PromocionnotificacionMap : ClassMap<Promocionnotificacion> {
        
        public PromocionnotificacionMap() {
			Table("PromocionNotificacion");
			LazyLoad();
			CompositeId().KeyProperty(x => x.Notificacionid, "NotificacionID")
			             .KeyReference(x => x.Promocion, "PromocionId");
			References(x => x.Notificacion).Column("NotificacionID");
			//References(x => x.Promocion).Column("PromocionId");
        }
    }
}
