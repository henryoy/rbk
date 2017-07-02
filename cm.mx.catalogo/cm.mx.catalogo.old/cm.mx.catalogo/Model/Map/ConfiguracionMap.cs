using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.mx.catalogo.Model.Map
{
    class ConfiguracionMap : ClassMap<Configuracion>
    {
        public ConfiguracionMap()
        {
            Id(x => x.Clave).GeneratedBy.Assigned();
            Map(x => x.Descripcion);
            Map(x => x.Tipo);
            Map(x => x.Valor);
            Map(x => x.Estado);
        }
    }
}
