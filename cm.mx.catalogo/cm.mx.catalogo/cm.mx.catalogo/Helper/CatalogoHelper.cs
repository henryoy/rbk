using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cm.mx.catalogo.Helper
{
    public static class Extension
    {
        public static bool IsNumeric(this string s)
        {
            float output;
            return float.TryParse(s, out output);
        }
    }
    public static class CatalogoHelper
    {

        private static List<string> _errores = new List<string>();
        private static List<string> _mensajes = new List<string>();
        private static string _mensaje = string.Empty;
        private static bool _exito = false;

        public static List<string> Errores
        {
            get { return _errores; }
        }
        public static bool Exito
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
        public static string Mensaje
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
        public static List<string> Mensajes
        {
            get { return _mensajes; }
        }
        public static int GetNumPag()
        {
            string _reportePath = string.Empty;
            int numPagina = 10;

            try
            {
                _reportePath = ConfigurationManager.AppSettings["NumPag"];

                if (string.IsNullOrEmpty(_reportePath))
                {
                    _exito = false;
                }

                bool isNumeric = Extension.IsNumeric(_reportePath);
                if (isNumeric)
                {
                    int numPag = 0;
                    numPag = Convert.ToInt32(_reportePath);
                    if (numPag > 50)
                    {
                        numPagina = 50;
                        _mensajes.Add("La paginación no puede exeder de 50 elementos.");
                    }
                    else numPagina = numPag;
                }


            }
            catch (Exception innerException)
            {
                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                }
                Errores.Add(innerException.Message + ". Metodo: " + innerException.TargetSite.Name);
                _exito = false;
            }
            return numPagina;
        }
        public static int GetNumPagModal()
        {
            string _reportePath = string.Empty;
            int numPagina = 10;

            try
            {
                _reportePath = ConfigurationManager.AppSettings["NumPagModal"];

                if (string.IsNullOrEmpty(_reportePath))
                {
                    _exito = false;
                }

                bool isNumeric = Extension.IsNumeric(_reportePath);
                if (isNumeric)
                {
                    int numPag = 0;
                    numPag = Convert.ToInt32(_reportePath);
                    if (numPag > 15)
                    {
                        numPagina = 15;
                        _mensajes.Add("La paginación no puede exeder de 50 elementos.");
                    }
                    else numPagina = numPag;
                }


            }
            catch (Exception innerException)
            {
                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                }
                Errores.Add(innerException.Message + ". Metodo: " + innerException.TargetSite.Name);
                _exito = false;
            }
            return numPagina;
        }
        public static Control FindControlRecursive(Control root, string id)
        {
            if (root.ID == id)
            {
                return root;
            }

            foreach (Control c in root.Controls)
            {
                Control t = FindControlRecursive(c, id);
                if (t != null) return t;
            }
            return null;
        }
        public static void GenerarFooter(Repeater paginador, Label etiqueta, int indice, int TotalRegistros, int cantidad)
        {
            double getPageCount = (double)((decimal)TotalRegistros / (decimal)cantidad);
            int pageCount = (int)Math.Ceiling(getPageCount);
            List<ListItem> pages = new List<ListItem>();

            if (TotalRegistros > 0)
            {
                if (pageCount > 1)
                {

                    if ((indice + 1) > 1)
                    {
                        pages.Add(new ListItem("&laquo;", "1", indice > 1));
                        pages.Add(new ListItem("...", indice.ToString(), true));
                    }
                    for (int i = (indice + 1); i < (indice + 3); i++)
                    {
                        if (i <= pageCount)
                            pages.Add(new ListItem(i.ToString(), i.ToString(), i != indice + 1));
                    }
                    if (pageCount > 2 && indice < (pageCount - 2))
                    {
                        pages.Add(new ListItem("...", (indice + 2).ToString(), true));
                        pages.Add(new ListItem("&raquo;", pageCount.ToString(), indice < pageCount - 1));
                    }
                }

                if (TotalRegistros > 1)
                {
                    int d = (indice * cantidad) + 1;
                    int t = (indice + 1) * cantidad;
                    if (TotalRegistros < t)
                        t = TotalRegistros;
                    etiqueta.Text = string.Format("{0}-{1} de {2}", d, t, TotalRegistros);
                }
                else
                {
                    etiqueta.Text = string.Format("Total registros: {0}", TotalRegistros);
                }
            }

            paginador.DataSource = pages;
            paginador.DataBind();
        }
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
