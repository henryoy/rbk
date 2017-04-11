using System;
using System.Text;
using System.Collections.Generic;


namespace cm.mx.catalogo.Model {
    
    public partial class Promocionusuario {
        public virtual int Promocionid { get; set; }
        public virtual int Usuarioid { get; set; }
        public virtual Promocion Promocion { get; set; }
        #region NHibernate Composite Key Requirements
        public override bool Equals(object obj) {
			if (obj == null) return false;
			var t = obj as Promocionusuario;
			if (t == null) return false;
			if (Promocionid == t.Promocionid
			 && Usuarioid == t.Usuarioid)
				return true;

			return false;
        }
        public override int GetHashCode() {
			int hash = GetType().GetHashCode();
			hash = (hash * 397) ^ Promocionid.GetHashCode();
			hash = (hash * 397) ^ Usuarioid.GetHashCode();

			return hash;
        }
        #endregion
    }
}
