using System;
using System.Text;
using System.Collections.Generic;


namespace cm.mx.catalogo.Model {
    [Serializable]
    public partial class Promocionnotificacion {
        public virtual int Notificacionid { get; set; }
        public virtual int Promocionid { get; set; }
        public virtual Notificacion Notificacion { get; set; }
        public virtual Promocion Promocion { get; set; }
        #region NHibernate Composite Key Requirements
        public override bool Equals(object obj) {
			if (obj == null) return false;
			var t = obj as Promocionnotificacion;
			if (t == null) return false;
			if (Notificacionid == t.Notificacionid
			 && Promocionid == t.Promocionid)
				return true;

			return false;
        }
        public override int GetHashCode() {
			int hash = GetType().GetHashCode();
			hash = (hash * 397) ^ Notificacionid.GetHashCode();
			hash = (hash * 397) ^ Promocionid.GetHashCode();

			return hash;
        }
        #endregion
    }
}
