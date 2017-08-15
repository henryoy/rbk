using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.mx.catalogo.Model
{
    public class DistribucionMap : ClassMap<Distribucion>
    {
        public DistribucionMap()
        {
            Id(x => x.DistribucionID);
            Map(x => x.Campos);
            Map(x => x.Descripcion);
            Map(x => x.Nombre);
            Map(x => x.Estado);
            Map(x => x.FechaBaja);
            Map(x => x.UsuarioBaja);
            Map(x => x.TipoMembresia);
            Map(x => x.MRGroupId);
            HasMany(x => x.Condiciones).Table("CondicionDistribucion").KeyColumn("DistribucionID").Cascade.AllDeleteOrphan().Inverse();
        }
    }
}
