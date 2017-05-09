using FluentNHibernate.Mapping;

namespace cm.mx.catalogo.Model.Map
{
    class CondicionDistribucionMap : ClassMap<CondicionDistribucion>
    {
        public CondicionDistribucionMap()
        {
            Id(x => x.CondicionID);
            Map(x => x.Campo);
            Map(x => x.DistribucionID).Formula("DistribucionID");
            Map(x => x.Nexo);
            Map(x => x.Operador);
            Map(x => x.Tipo);
            Map(x => x.Valor);
            References(x => x.Distrubucion).Column("DistribucionID");
        }
    }
}
