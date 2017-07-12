using cm.mx.dbCore.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.mx.catalogo.Model
{
    internal class MailrelaylogRepository : RepositoryBase<Mailrelaylog>
    {
        public override Mailrelaylog GetById(object id)
        {
            throw new NotImplementedException();
        }

        public override Mailrelaylog GetById(int id)
        {
            Mailrelaylog oMail = new Mailrelaylog();
            oMail = _session.Get<Mailrelaylog>(id);
            return oMail;
        }


        public Mailrelaylog GuardadrMailLog(Mailrelaylog objeto)
        {
            _exito = false;
            _session.Transaction.Begin();
            _session.SaveOrUpdate(objeto);
            _session.Transaction.Commit();

            return objeto;
        }

        public override Mailrelaylog GetNewEntidad()
        {
            throw new NotImplementedException();
        }

        public override bool Update(Mailrelaylog modificado)
        {
            throw new NotImplementedException();
        }
    }
}
