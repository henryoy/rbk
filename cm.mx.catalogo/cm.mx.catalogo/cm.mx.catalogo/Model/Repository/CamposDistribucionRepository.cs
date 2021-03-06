﻿using cm.mx.dbCore.Clases;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cm.mx.catalogo.Model
{
    internal class CamposDistribucionRepository : RepositoryBase<CamposDistribucion>
    {
        public override CamposDistribucion GetById(int id)
        {
            return _session.Get<CamposDistribucion>(id);
        }

        public override CamposDistribucion GetById(object id)
        {
            return _session.Get<CamposDistribucion>(id);
        }

        public override CamposDistribucion GetNewEntidad()
        {
            throw new NotImplementedException();
        }

        public override bool Update(CamposDistribucion modificado)
        {
            throw new NotImplementedException();
        }

        public List<CamposDistribucion> GetCampos(List<string> campos)
        {
            _exito = false;
            List<CamposDistribucion> ls = _session.CreateCriteria<CamposDistribucion>().Add(Restrictions.In("Campo", campos)).List<CamposDistribucion>().ToList();
            _exito = true;
            return ls;
        }
    }
}
