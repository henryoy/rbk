using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.mx.catalogo.Model
{
    [Serializable()]
    public class CondicionDistribucion
    {
        public virtual string Campo { get; set; }
        public virtual int CondicionID { get; set; }
        public virtual int DistribucionID { get; set; }
        public virtual string Nexo { get; set; }
        public virtual string Operador { get; set; }
        public virtual string Tipo { get; set; }
        public virtual string Valor { get; set; }

        public virtual Distribucion Distrubucion { get; set; }
    }
}
