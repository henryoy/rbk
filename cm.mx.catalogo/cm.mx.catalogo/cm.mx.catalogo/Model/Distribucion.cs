using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.mx.catalogo.Model
{
    [Serializable()]
    public class Distribucion
    {
        public virtual string Campos { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual int DistribucionID { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string Estado { get; set; }
        public virtual int UsuarioBaja { get; set; }
        public virtual DateTime FechaBaja { get; set; }
        public virtual int TipoMembresia { get; set; }
        public virtual IList<CondicionDistribucion> Condiciones { get; set; }

        public Distribucion()
        {
            Condiciones = new List<CondicionDistribucion>();
        }
        public virtual void Add(CondicionDistribucion obj)
        {
            obj.Distrubucion = this;
            Condiciones.Add(obj);
        }
    }
}
