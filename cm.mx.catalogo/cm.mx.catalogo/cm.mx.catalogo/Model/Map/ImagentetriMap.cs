using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;

namespace cm.mx.catalogo.Model.Map
{   
    
    public class ImagentetriMap : ClassMap<Imagentetri> {
        
        public ImagentetriMap() {
			Table("ImagenTetris");
			LazyLoad();
			Id(x => x.ID).GeneratedBy.Identity().Column("ID");
			Map(x => x.Nombre).Column("Nombre").Not.Nullable();
			Map(x => x.Descripcion).Column("Descripcion").Not.Nullable();
			Map(x => x.Imagen).Column("Imagen").Not.Nullable();
			Map(x => x.Codigo).Column("Codigo").Not.Nullable();
        }
    }
}
