using cm.mx.dbCore.Clases;
using System;
using System.Collections.Generic;
using System.Linq;

namespace cm.mx.catalogo.Model
{
    internal class TipoMembresiaRepository : RepositoryBase<Tipomembresia>
    {
        public override Tipomembresia GetById(int id)
        {
            return _session.Get<Tipomembresia>(id);
        }

        public Tipomembresia GuardarMembresia(Tipomembresia oMembresia)
        {
            _exito = false;
            _session.BeginTransaction();
            _session.SaveOrUpdate(oMembresia);
            _session.Transaction.Commit();
            _exito = true;
            return oMembresia;
        }

        public List<Tipomembresia> GetAllActivos()
        {
            _exito = false;
            List<Tipomembresia> lsMembresia = new List<Tipomembresia>();
            lsMembresia = _session.QueryOver<Tipomembresia>().Where(a => a.Estado == Enums.Estatus.ACTIVO.ToString()).List().ToList();
            return lsMembresia;
        }

        public override Tipomembresia GetById(object id)
        {
            return _session.Get<Tipomembresia>(id);
        }

        public override Tipomembresia GetNewEntidad()
        {
            throw new NotImplementedException();
        }

        public override bool Update(Tipomembresia modificado)
        {
            throw new NotImplementedException();
        }
    }
}