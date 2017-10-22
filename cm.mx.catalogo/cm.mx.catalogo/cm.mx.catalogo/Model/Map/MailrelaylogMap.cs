using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;
using cm.mx.catalogo.Model; 

namespace cm.mx.catalogo.Model.Map {
    
    
    public class MailrelaylogMap : ClassMap<Mailrelaylog> {
        
        public MailrelaylogMap() {
			Table("MailRelayLog");
			LazyLoad();
			Id(x => x.MailRelayId).GeneratedBy.Identity().Column("MailRelayId");
			Map(x => x.CampanaId).Column("CampanaId").Not.Nullable();
			Map(x => x.Html).CustomType("StringClob").CustomSqlType("nvarchar(max)").Column("Html").Not.Nullable();
			Map(x => x.MRGrupoId).Column("MRGrupoId");
			Map(x => x.MRCampanaId).Column("MRCampanaId");
            Map(x => x.NombreCampana).Column("NombreCampana");
            Map(x => x.MRSendCampana).Column("MRSendCampana");
        }
    }
}
