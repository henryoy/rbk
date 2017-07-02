using cm.mx.dbCore.Clases;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.mx.catalogo.Model
{
    internal class UsuarioDispositivoRepository : RepositoryBase<UsuarioDispositivo>
    {
        public override UsuarioDispositivo GetById(int id)
        {
            throw new NotImplementedException();
        }

        public override UsuarioDispositivo GetById(object id)
        {
            throw new NotImplementedException();
        }

        public override UsuarioDispositivo GetNewEntidad()
        {
            throw new NotImplementedException();
        }

        public override bool Update(UsuarioDispositivo modificado)
        {
            throw new NotImplementedException();
        }

        public UsuarioDispositivo getByUsuarioToken(UsuarioDispositivo oUsuario)
        {
            return _session.Query<UsuarioDispositivo>().Where(x => x.Token == oUsuario.Token && x.UsuarioId == oUsuario.UsuarioId).FirstOrDefault();
        }

        public bool GuardarToken(UsuarioDispositivo oUsuarioD)
        {
            _exito = false;
            _session.Transaction.Begin();
            _session.SaveOrUpdate(oUsuarioD);
            _session.Transaction.Commit();
            _exito = true;

            return _exito;
        }

        public UsuarioDispositivo getTokenActivo(int UsuarioID)
        {
            UsuarioDispositivo oUsuario = new UsuarioDispositivo();

            oUsuario = _session.Query<UsuarioDispositivo>().Where(x => x.UsuarioId == UsuarioID).OrderByDescending(x => x.FechaAlta).FirstOrDefault();

            return oUsuario;
        }

        //public List<UsuarioDispositivo> getTokensByUsuario(int usuarioId)
        //{

        //}
    }
}
