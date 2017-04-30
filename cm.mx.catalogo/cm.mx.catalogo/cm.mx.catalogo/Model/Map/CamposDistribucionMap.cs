using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.mx.catalogo.Model.Map
{
    class CamposDistribucionMap : ClassMap<CamposDistribucion>
    {
        public CamposDistribucionMap()
        {
            Id(x => x.Campo);
            Map(x => x.Descripcion);
            Map(x => x.Nombre);
        }
    }
}
