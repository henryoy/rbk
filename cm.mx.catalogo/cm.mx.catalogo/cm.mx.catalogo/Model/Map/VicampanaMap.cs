using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;
using cm.mx.catalogo.Model; 

namespace cm.mx.catalogo.Model.Map {
    
    
    public class VicampanaMap : ClassMap<Vicampana> {
        
        public VicampanaMap() {
			Table("viCampana");
			LazyLoad();
			Id(x => x.CampanaId).Column("CampanaId").Not.Nullable();
			Map(x => x.Nombre).Column("Nombre").Not.Nullable();
			Map(x => x.TipoCampana).Column("TipoCampana").Not.Nullable();
			Map(x => x.DestinoCampana).Column("DestinoCampana").Not.Nullable();
			Map(x => x.MRCampanaId).Column("MRCampanaId");
        }
    }
}
