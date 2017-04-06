using cm.mx.dbCore.Clases;
using NHibernate.Linq;
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
            lsMembresia = _session.QueryOver<Tipomembresia>().Where(a => a.Estado == "ALTA").List().ToList();
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

        public bool ExisteRegistro(int MembresiaID, int numero)
        {
            _exito = false;
            _exito = _session.Query<Tipomembresia>().Any(a => a.Membresiaid != MembresiaID && numero >= a.ApartirDe && numero <= a.Hasta);
            return _exito;
        }

        public bool CambioMembresia(int visitas)
        {
            _exito = false;
            var r = _session.Query<Tipomembresia>().FirstOrDefault(a => visitas >= a.ApartirDe && visitas <= a.Hasta);
            if (r != null)
            {
                _exito = visitas == r.ApartirDe;
            }
            return _exito;
        }
    }
}