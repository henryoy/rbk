using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;
using cm.mx.catalogo.Model; 

namespace cm.mx.catalogo.Model.Map {
    
    
    public partial class DistribucionMap : ClassMap<Distribucion> {
        
        public DistribucionMap() {
			Table("Distribucion");
			LazyLoad();
			Id(x => x.DistribucionId).GeneratedBy.Identity().Column("DistribucionId");
			Map(x => x.Nombre).Column("Nombre").Not.Nullable().Length(10);
        }
    }
}
