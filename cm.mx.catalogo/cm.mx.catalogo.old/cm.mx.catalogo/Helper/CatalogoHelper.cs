using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cm.mx.catalogo.Helper
{
    public static class CatalogoHelper
    {
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
