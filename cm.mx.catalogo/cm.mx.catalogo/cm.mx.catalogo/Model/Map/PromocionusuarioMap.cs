using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;
using cm.mx.catalogo.Model; 

namespace cm.mx.catalogo.Model.Mappings {
    
    
    public partial class PromocionusuarioMap : ClassMap<Promocionusuario> {
        
        public PromocionusuarioMap() {
			Table("PromocionUsuario");
			LazyLoad();
			CompositeId().KeyProperty(x => x.Promocionid, "PromocionId")
			             .KeyProperty(x => x.Usuarioid, "UsuarioId");
			References(x => x.Promocion).Column("PromocionId");
        }
    }
}
