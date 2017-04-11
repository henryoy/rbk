using System;
using System.Text;
using System.Collections.Generic;


namespace cm.mx.catalogo.Model {
    
    public partial class Promocionsucursal {
        public virtual int Promocionid { get; set; }
        public virtual int Sucursalid { get; set; }
        public virtual Promocion Promocion { get; set; }
        public virtual Sucursal Sucursal { get; set; }
        #region NHibernate Composite Key Requirements
        public override bool Equals(object obj) {
			if (obj == null) return false;
			var t = obj as Promocionsucursal;
			if (t == null) return false;
			if (Promocionid == t.Promocionid
			 && Sucursalid == t.Sucursalid)
				return true;

			return false;
        }
        public override int GetHashCode() {
			int hash = GetType().GetHashCode();
			hash = (hash * 397) ^ Promocionid.GetHashCode();
			hash = (hash * 397) ^ Sucursalid.GetHashCode();

			return hash;
        }
        #endregion
    }
}
