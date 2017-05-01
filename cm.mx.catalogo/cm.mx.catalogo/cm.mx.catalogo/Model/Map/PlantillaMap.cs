using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;
using cm.mx.catalogo.Model; 

namespace cm.mx.catalogo.Model.Map {
    
    
    public partial class PlantillaMap : ClassMap<Plantilla> {
        
        public PlantillaMap() {
			Table("Plantilla");
			LazyLoad();
			Id(x => x.PlantillaId).GeneratedBy.Identity().Column("PlantillaId");
			Map(x => x.Nombre).Column("Nombre").Not.Nullable().Length(10);
        }
    }
}
