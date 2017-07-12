using System;
using System.Text;
using System.Collections.Generic;
using Iesi.Collections.Generic;


namespace cm.mx.catalogo.Model {
    
    public class Mailrelaylog {
        public virtual int MailRelayId { get; set; }
        public virtual int CampanaId { get; set; }
        public virtual string Html { get; set; }
        public virtual int? MRGrupoId { get; set; }
        public virtual int? MRCampanaId { get; set; }
    }
}
