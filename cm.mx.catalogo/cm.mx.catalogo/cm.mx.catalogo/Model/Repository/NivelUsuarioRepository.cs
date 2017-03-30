using cm.mx.dbCore.Clases;
using System;
using System.Collections.Generic;
using System.Linq;

namespace cm.mx.catalogo.Model
{
    internal class NivelUsuarioRepository : RepositoryBase<NivelUsuario>
    {
        public List<NivelUsuario> GetAllNivelUsuario()
        {
            List<NivelUsuario> lsNivelUsuario = new List<NivelUsuario>();
            lsNivelUsuario = _session.QueryOver<NivelUsuario>().List().ToList();
            return lsNivelUsuario;
        }

        public NivelUsuario GuardarNivelUsuario(NivelUsuario oNivelUsuario)
        {
            _exito = false;
            _session.BeginTransaction();
            _session.SaveOrUpdate(oNivelUsuario);
            _session.Transaction.Commit();
            _exito = true;
            return oNivelUsuario;
        }

        public override NivelUsuario GetById(int id)
        {
            return _session.Get<NivelUsuario>(id);
        }

        public override NivelUsuario GetById(object id)
        {
            return _session.Get<NivelUsuario>(id);
        }

        public override NivelUsuario GetNewEntidad()
        {
            throw new NotImplementedException();
        }

        public override bool Update(NivelUsuario modificado)
        {
            throw new NotImplementedException();
        }
    }
}