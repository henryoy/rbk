using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;
using cm.mx.catalogo.Model; 

namespace cm.mx.catalogo.Model.Map {
    
    
    public partial class CampanaMap : ClassMap<Campana> {
        
        public CampanaMap() {
			Table("Campana");
			LazyLoad();
			Id(x => x.CampanaId).GeneratedBy.Identity().Column("CampanaId");
			Map(x => x.Nombre).Column("Nombre").Not.Nullable().Length(250);
			Map(x => x.MensajePrevio).Column("MensajePrevio").Length(250);
			Map(x => x.TipoCampana).Column("TipoCampana").Not.Nullable().Length(15);
			Map(x => x.Programacion).Column("Programacion").Not.Nullable();
			Map(x => x.FechaProgramacion).Column("FechaProgramacion");
			Map(x => x.DestinoCampana).Column("DestinoCampana").Not.Nullable().Length(10);
			Map(x => x.PromocionId).Column("PromocionId").Precision(10);
			Map(x => x.DistribucionId).Column("DistribucionId").Not.Nullable().Precision(10);
			Map(x => x.PlantillaId).Column("PlantillaId").Not.Nullable().Precision(10);
        }
    }
}
