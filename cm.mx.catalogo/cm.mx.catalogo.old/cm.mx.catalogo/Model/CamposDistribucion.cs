using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.mx.catalogo.Model
{
    [Serializable()]
    public class CamposDistribucion
    {
        public virtual string Campo { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual string Formato { get; set; }
        public virtual string Nombre { get; set; }
    }
}
