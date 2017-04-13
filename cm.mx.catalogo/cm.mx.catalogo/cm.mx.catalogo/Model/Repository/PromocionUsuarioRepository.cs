using cm.mx.dbCore.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.mx.catalogo.Model
{
    public class PromocionUsuarioRepository : RepositoryBase<PromocionUsuario>
    {
        public override PromocionUsuario GetById(int id)
        {
            PromocionUsuario oPromoUser = new PromocionUsuario();
            oPromoUser = _session.Get<PromocionUsuario>(id);
            return oPromoUser;
        }

        public bool GuardarPromocioUsuario(PromocionUsuario oPromo)
        {
            _exito = false;
            _session.BeginTransaction();
            _session.SaveOrUpdate(oPromo);
            _session.Flush();
            _session.Transaction.Commit();
            _exito = true;
            return _exito;
        }
        public override PromocionUsuario GetById(object id)
        {
            throw new NotImplementedException();
        }

        public override PromocionUsuario GetNewEntidad()
        {
            throw new NotImplementedException();
        }

        public override bool Update(PromocionUsuario modificado)
        {
            throw new NotImplementedException();
        }
    }
}
