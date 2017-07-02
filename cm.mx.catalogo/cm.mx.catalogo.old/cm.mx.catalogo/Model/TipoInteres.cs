using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.mx.catalogo.Model
{
    [Serializable()]
    public class TipoInteres
    {
        public TipoInteres()
        {
            Usuarios = new List<Usuario>();
        }

        public virtual string Descripcion { get; set; }
        public virtual string Estatus { get; set; }
        public virtual DateTime FechaAlta { get; set; }
        public virtual DateTime FechaBaja { get; set; }
        public virtual string Nombre { get; set; }
        public virtual int TipoInteresID { get; set; }
        public virtual int UsuarioAlta { get; set; }
        public virtual int UsuarioBaja { get; set; }
        public virtual IList<Usuario> Usuarios { get; set; }
    }
}
