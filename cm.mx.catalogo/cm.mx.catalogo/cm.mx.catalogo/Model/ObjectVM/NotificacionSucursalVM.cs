using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace cm.mx.catalogo.Model.ObjectVM
{
    [Serializable]
    [DataContract]
    public class NotificacionSucursalVM : Notificacion
    {
        [DataMember]
        public virtual string Sucursal { get; set; }

    }
}
