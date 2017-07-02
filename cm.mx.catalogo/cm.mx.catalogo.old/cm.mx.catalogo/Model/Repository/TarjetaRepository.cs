using cm.mx.dbCore.Clases;
using System;
using System.Collections.Generic;
using System.Linq;

namespace cm.mx.catalogo.Model
{
    internal class TarjetaRepository : RepositoryBase<Tarjeta>
    {
        public override Tarjeta GetById(int id)
        {
            return _session.Get<Tarjeta>(id);
        }

        public Tarjeta GuardarTarjeta(Tarjeta oMembresia)
        {
            _exito = false;
            _session.BeginTransaction();
            _session.SaveOrUpdate(oMembresia);
            _session.Transaction.Commit();
            _exito = true;
            return oMembresia;
        }

        public List<Tarjeta> GetAllActivos()
        {
            _exito = false;
            List<Tarjeta> lsMembresia = new List<Tarjeta>();
            lsMembresia = _session.QueryOver<Tarjeta>().Where(a => a.Estatus == "ACTIVO").List().ToList();
            return lsMembresia;
        }

        public override Tarjeta GetById(object id)
        {
            return _session.Get<Tarjeta>(id);
        }

        public override Tarjeta GetNewEntidad()
        {
            throw new NotImplementedException();
        }

        public override bool Update(Tarjeta modificado)
        {
            throw new NotImplementedException();
        }
    }
}