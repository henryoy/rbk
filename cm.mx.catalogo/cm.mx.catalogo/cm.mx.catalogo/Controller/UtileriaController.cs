using cm.mx.catalogo.Model;
using cm.mx.dbCore.Interfaces;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace cm.mx.catalogo.Controller
{
    public class UtileriaController : IController
    {
        #region propiedades
        private List<string> _errores = new List<string>();
        private List<string> _mensajes = new List<string>();
        private string _mensaje = string.Empty;
        private bool _exito = false;

        private int _totalColumnas = 0;
        public int TotalColumnas
        {
            get { return _totalColumnas; }
            set { _totalColumnas = value; }
        }
        public List<string> Errores
        {
            get { return _errores; }
        }
        public bool Exito
        {
            get
            {
                return _exito;
            }
            set
            {
                _exito = value;
            }
        }
        public string Mensaje
        {
            get
            {
                return _mensaje;
            }
            set
            {
                _mensaje = value;
            }
        }
        public List<string> Mensajes
        {
            get { return _mensajes; }
        }
        #endregion

        public string GenerarFolio(int SucursalId)
        {
            _exito = false;
            _errores = new List<string>();
            _mensajes = new List<string>();
            string folio = "";
            try
            {
                var sfecha = String.Format("{0:yyyyMMddHHmmss}", DateTime.Now);
                var sSuc = String.Format("{0:00000}", SucursalId);
                folio = sfecha + "-" + sSuc;
            }
            catch (Exception ex)
            {
                _exito = false;
                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
            }

            return folio;
        }

        public List<ICriterion> CrearCondion(List<CondicionDistribucion> condiciones)
        {
            var union = condiciones.Where(a => !string.IsNullOrEmpty(a.Nexo)).Select(a => a.Nexo).FirstOrDefault();
            if (string.IsNullOrEmpty(union)) union = "Y";
            List<ICriterion> lsCond = new List<ICriterion>();
            try
            {
                var lsiCriteriones = condiciones.GroupBy(u => u.Campo).Select(grp => grp.ToList()).ToList();
                lsiCriteriones.ForEach(c =>
                {
                    Disjunction ExpresionOr = null;
                    Conjunction ExpresionAnd = null;

                    c.ForEach(x =>
                    {
                        object val = null;
                        List<object> ls = new List<object>();
                        if (x.Campo.Equals("TarjetaID", StringComparison.OrdinalIgnoreCase)) x.Campo = "Membresiaid";
                        switch (x.Tipo)
                        {
                            case "Texto":
                                if (x.Valor.IndexOf(",") == -1) val = x.Valor;
                                else
                                    ls.AddRange(x.Valor.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList());
                                break;
                            case "Entero":
                                int _id;
                                if (x.Valor.IndexOf(",") == -1)
                                {
                                    int.TryParse(x.Valor, out _id);
                                    val = _id;
                                }
                                else
                                {
                                    var r = x.Valor.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                    r.ForEach(a =>
                                    {
                                        int.TryParse(a, out _id);
                                        ls.Add(_id);
                                    });
                                }
                                break;
                            case "Moneda":
                                double _cant;
                                if (x.Valor.IndexOf(",") == -1)
                                {
                                    double.TryParse(x.Valor, out _cant);
                                    val = _cant;
                                }
                                else
                                {
                                    var r = x.Valor.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                    r.ForEach(a =>
                                    {
                                        double.TryParse(a, out _cant);
                                        ls.Add(_cant);
                                    });
                                }
                                break;
                            case "Fecha":
                                DateTime _Fecha;
                                if (x.Valor.IndexOf(",") == -1)
                                {
                                    DateTime.TryParse(x.Valor, out _Fecha);
                                    val = _Fecha;
                                }
                                else
                                {
                                    var r = x.Valor.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                    r.ForEach(a =>
                                    {
                                        DateTime.TryParse(a, out _Fecha);
                                        ls.Add(_Fecha);
                                    });
                                }
                                break;
                            case "Decimal":
                                decimal _Dec;
                                if (x.Valor.IndexOf(",") == -1)
                                {
                                    decimal.TryParse(x.Valor, out _Dec);
                                    val = _Dec;
                                }
                                else
                                {
                                    var r = x.Valor.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                    r.ForEach(a =>
                                    {
                                        decimal.TryParse(a, out _Dec);
                                        ls.Add(_Dec);
                                    });
                                }
                                break;
                        }

                        union = string.IsNullOrEmpty(x.Nexo) ? union : x.Nexo;
                        if (union.Equals("O", StringComparison.OrdinalIgnoreCase))
                        {
                            switch (x.Operador)
                            {
                                case "=":
                                    if (ExpresionOr == null) ExpresionOr = Restrictions.Disjunction();
                                    ExpresionOr.Add(Restrictions.Eq(x.Campo, val));
                                    break;
                                case ">":
                                    if (ExpresionOr == null) ExpresionOr = Restrictions.Disjunction();
                                    ExpresionOr.Add(Restrictions.Gt(x.Campo, val));
                                    break;
                                case "<":
                                    if (ExpresionOr == null) ExpresionOr = Restrictions.Disjunction();
                                    ExpresionOr.Add(Restrictions.Lt(x.Campo, val));
                                    break;
                                case ">=":
                                    if (ExpresionOr == null) ExpresionOr = Restrictions.Disjunction();
                                    ExpresionOr.Add(Restrictions.Ge(x.Campo, val));
                                    break;
                                case "<=":
                                    if (ExpresionOr == null) ExpresionOr = Restrictions.Disjunction();
                                    ExpresionOr.Add(Restrictions.Le(x.Campo, val));
                                    break;
                                case "!=":
                                    if (ExpresionOr == null) ExpresionOr = Restrictions.Disjunction();
                                    ExpresionOr.Add(Restrictions.Eq(x.Campo, val));
                                    break;
                                case "In":
                                    if (ExpresionOr == null) ExpresionOr = Restrictions.Disjunction();
                                    ExpresionOr.Add(Restrictions.In(x.Campo, ls));
                                    break;
                                case "Not in":
                                    if (ExpresionOr == null) ExpresionOr = Restrictions.Disjunction();
                                    ExpresionOr.Add(Restrictions.Not(Restrictions.In(x.Campo, ls)));
                                    break;
                                case "Like":
                                    if (ExpresionOr == null) ExpresionOr = Restrictions.Disjunction();
                                    ExpresionOr.Add(Restrictions.Like(Projections.Cast(NHibernateUtil.String, Projections.Property(x.Campo)), x.Valor, MatchMode.Anywhere));
                                    break;
                            }
                        }
                        else
                        {
                            switch (x.Operador)
                            {
                                case "=":
                                    if (ExpresionAnd == null) ExpresionAnd = Restrictions.Conjunction();
                                    ExpresionAnd.Add(Restrictions.Eq(x.Campo, val));
                                    break;
                                case ">":
                                    if (ExpresionAnd == null) ExpresionAnd = Restrictions.Conjunction();
                                    ExpresionAnd.Add(Restrictions.Gt(x.Campo, val));
                                    break;
                                case "<":
                                    if (ExpresionOr == null) ExpresionOr = Restrictions.Disjunction();
                                    ExpresionOr.Add(Restrictions.Lt(x.Campo, val));
                                    if (ExpresionAnd == null) ExpresionAnd = Restrictions.Conjunction();
                                    ExpresionAnd.Add(Restrictions.Lt(x.Campo, val));
                                    break;
                                case ">=":
                                    if (ExpresionAnd == null) ExpresionAnd = Restrictions.Conjunction();
                                    ExpresionAnd.Add(Restrictions.Ge(x.Campo, val));
                                    break;
                                case "<=":
                                    if (ExpresionAnd == null) ExpresionAnd = Restrictions.Conjunction();
                                    ExpresionAnd.Add(Restrictions.Le(x.Campo, val));
                                    break;
                                case "!=":
                                    if (ExpresionAnd == null) ExpresionAnd = Restrictions.Conjunction();
                                    ExpresionAnd.Add(Restrictions.Eq(x.Campo, val));
                                    break;
                                case "In":
                                    if (ExpresionAnd == null) ExpresionAnd = Restrictions.Conjunction();
                                    ExpresionAnd.Add(Restrictions.In(x.Campo, ls));
                                    break;
                                case "Not in":
                                    if (ExpresionAnd == null) ExpresionAnd = Restrictions.Conjunction();
                                    ExpresionAnd.Add(Restrictions.Not(Restrictions.In(x.Campo, ls)));
                                    break;
                                case "Like":
                                    if (ExpresionAnd == null) ExpresionAnd = Restrictions.Conjunction();
                                    ExpresionAnd.Add(Restrictions.Like(Projections.Cast(NHibernateUtil.String, Projections.Property(x.Campo)), x.Valor, MatchMode.Anywhere));
                                    break;
                            }
                        }
                    });
                    if (ExpresionAnd != null) lsCond.Add(ExpresionAnd);
                    if (ExpresionOr != null) lsCond.Add(ExpresionOr);
                });
                _exito = true;
            }
            catch (Exception ex)
            {
                lsCond = new List<ICriterion>();
                _exito = false;
                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
            }
            return lsCond;
        }

        private bool CrearTabla(List<string> columnas, GridView _grid)
        {
            _exito = false;
            try
            {
                CamposDistribucionRepository rCampos = new CamposDistribucionRepository();
                var lsColumnas = rCampos.GetCampos(columnas);
                _grid.Columns.Clear();
                lsColumnas.ForEach(x =>
                {
                    BoundField col = new BoundField();
                    col.HeaderText = x.Descripcion;
                    col.DataField = x.Campo;
                    if (!string.IsNullOrEmpty(x.Formato)) col.DataFormatString = "{0:" + x.Formato + "}";
                    _grid.Columns.Add(col);
                });
                _exito = true;
            }
            catch (Exception ex)
            {
                _exito = false;
                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
            }
            return _exito;
        }

        public bool ProbarDistribucion(Distribucion obj, GridView grid)
        {
            _exito = false;
            _errores = new List<string>();
            try
            {
                List<string> campos = obj.Campos.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (CrearTabla(campos, grid))
                {
                    UsuarioRepository rUsuario = new UsuarioRepository();
                    var cond = CrearCondion(obj.Condiciones.ToList());
                    var ls = rUsuario.GetUsuarios(cond);
                    grid.DataSource = ls;
                    grid.DataBind();
                    grid.HeaderRow.TableSection = TableRowSection.TableHeader;
                    _exito = true;
                }
            }
            catch (Exception ex)
            {
                _exito = false;
                while (ex != null)
                {
                    _errores.Add(ex.Message);
                    ex = ex.InnerException;
                }
            }
            return _exito;
        }
    }
}
