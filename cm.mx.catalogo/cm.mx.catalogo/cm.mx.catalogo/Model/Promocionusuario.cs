using System;
using System.Text;
using System.Collections.Generic;


namespace cm.mx.catalogo.Model {
    [Serializable]
    public partial class PromocionUsuario
    {        
        public virtual int PromocionId { get; set; }
        public virtual int UsuarioId { get; set; }
        public virtual Promocion Promocion { get; set; }
        public virtual string Estatus { get; set; }
        public virtual DateTime FechaAplicada { get; set; }
        public virtual DateTime FechaAlta { get; set; }
        #region NHibernate Composite Key Requirements
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var t = obj as PromocionUsuario;
            if (t == null) return false;
            if (PromocionId == t.PromocionId
             && UsuarioId == t.UsuarioId)
                return true;

            return false;
        }
        public override int GetHashCode()
        {
            int hash = GetType().GetHashCode();
            hash = (hash * 397) ^ PromocionId.GetHashCode();
            hash = (hash * 397) ^ UsuarioId.GetHashCode();

            return hash;
        }
        #endregion
    }
}
