using cm.mx.dbCore.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.mx.catalogo.Model
{
    internal class PlantillaRepository : RepositoryBase<Plantilla>
    {
        public override Plantilla GetById(object id)
        {
            throw new NotImplementedException();
        }

        public override Plantilla GetById(int id)
        {
           
            Plantilla oPlantilla = new Plantilla();
            oPlantilla = this._session.Get<Plantilla>(id);
            return oPlantilla;
        }

        public List<Plantilla> GetAllPlantilla()
        {
            List<Plantilla> lsPlantilla = new List<Plantilla>();
            lsPlantilla = this._session.QueryOver<Plantilla>().Where(f=>f.Estatus == "ACTIVO").List().ToList();
            return lsPlantilla;
        }

        public Plantilla GuardarPlantilla(Plantilla objeto)
        {
            _session.Transaction.Begin();
            _session.SaveOrUpdate(objeto);
            _session.Transaction.Commit();
            return objeto;
        }
        public override Plantilla GetNewEntidad()
        {
            throw new NotImplementedException();
        }

        public override bool Update(Plantilla modificado)
        {
            throw new NotImplementedException();
        }
    }
}
