using cm.mx.dbCore.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.mx.catalogo.Model
{
    internal class ImagenTetrisRepository : RepositoryBase<Imagentetri>
    {
        public override Imagentetri GetById(object id)
        {
            Imagentetri oimagen = new Imagentetri();
            oimagen = _session.Get<Imagentetri>(id);
            return oimagen;
        }

        public Imagentetri GetByName(string Nombre)
        {
            Imagentetri oimagen = new Imagentetri();
            oimagen = _session.QueryOver<Imagentetri>().Where(f => f.Nombre == Nombre).List().FirstOrDefault();
            return oimagen;
        }

        public List<Imagentetri> GetAllImagen()
        {
            List<Imagentetri> oimagen = new List<Imagentetri>();
            oimagen = _session.QueryOver<Imagentetri>().List().ToList();
            return oimagen;
        }

        public Imagentetri GuardarImagen(Imagentetri objeto)
        {
            _exito = false;
            _session.BeginTransaction();
            _session.SaveOrUpdate(objeto);
            _session.Transaction.Commit();
            return objeto;
        }
        public override Imagentetri GetById(int id)
        {
            throw new NotImplementedException();
        }

        public override Imagentetri GetNewEntidad()
        {
            throw new NotImplementedException();
        }

        public override bool Update(Imagentetri modificado)
        {
            throw new NotImplementedException();
        }
    }
}
