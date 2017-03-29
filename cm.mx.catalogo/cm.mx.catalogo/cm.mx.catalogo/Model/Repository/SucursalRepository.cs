using cm.mx.dbCore.Clases;
using System;
using System.Collections.Generic;
using System.Linq;

namespace cm.mx.catalogo.Model
{
    internal class SucursalRepository : RepositoryBase<Sucursal>
    {
        public override Sucursal GetById(int id)
        {
            return _session.Get<Sucursal>(id);
        }

        public Sucursal GuardarSucursal(Sucursal oMembresia)
        {
            _exito = false;
            _session.BeginTransaction();
            _session.SaveOrUpdate(oMembresia);
            _session.Transaction.Commit();
            _exito = true;
            return oMembresia;
        }

        public List<Sucursal> GetAllActivos()
        {
            _exito = false;
            List<Sucursal> lsMembresia = new List<Sucursal>();
            lsMembresia = _session.QueryOver<Sucursal>().List().ToList();
            return lsMembresia;
        }

        public override Sucursal GetById(object id)
        {
            return _session.Get<Sucursal>(id);
        }

        public override Sucursal GetNewEntidad()
        {
            throw new NotImplementedException();
        }

        public override bool Update(Sucursal modificado)
        {
            throw new NotImplementedException();
        }
    }
}