using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;
using cm.mx.catalogo.Model; 

namespace cm.mx.catalogo.Model.Mappings {
    
    
    public partial class PromocionsucursalMap : ClassMap<Promocionsucursal> {
        
        public PromocionsucursalMap() {
            Table("PromocionSucursal");
            LazyLoad();
            CompositeId().KeyReference(x => x.Promocion, "PromocionId")
                         .KeyProperty(x => x.Sucursalid, "SucursalID");
            References(x => x.Sucursal).Column("SucursalID").Not.Insert().Not.Update();
        }
    }
}
