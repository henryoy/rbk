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
        public virtual string Condicion { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual int DistribucionID { get; set; }
        public virtual string Nombre { get; set; }
    }
}
