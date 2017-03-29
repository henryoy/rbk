using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controles_ucPaginacion : System.Web.UI.UserControl
{
    #region Propiedades
    /// <summary>
    /// Clase para  el contenedor principal
    /// </summary>
    public string contenedorClass
    {
        get
        {
            var temp = ViewState["contenedor"];
            return temp == null ? "pagination-tabla" : (string)temp;
        }
        set
        {
            ViewState["contenedor"] = value;
        }
    }
    /// <summary>
    /// Clase para el contenedor de páginas
    /// </summary>
    public string RowPaginasClass
    {
        get
        {
            var temp = ViewState["row"];
            return temp == null ? "pagination" : (string)temp;
        }
        set
        {
            ViewState["row"] = value;
        }
    }
    /// <summary>
    /// Clase para los botones de la página
    /// </summary>
    public string PaginaClass
    {
        get
        {
            var temp = ViewState["pagina"];
            return temp == null ? "active" : (string)temp;
        }
        set
        {
            ViewState["pagina"] = value;
        }
    }
    public int Pagina
    {
        get
        {
            return page;
        }
    }
    private int page
    {
        get
        {
            var temp = ViewState["temp"];
            return temp == null ? 0 : (int)temp;
        }
        set
        {
            ViewState["temp"] = value;
        }
    }
    public event EventHandler Click;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void GenerarPaginas(int Pagina, int Cantidad, int TotalRegistros)
    {
        double getPageCount = (double)((decimal)TotalRegistros / (decimal)Cantidad);
        int pageCount = (int)Math.Ceiling(getPageCount);
        List<ListItem> pages = new List<ListItem>();

        if (pageCount > 1)
        {

            if ((Pagina + 1) > 1)
            {
                pages.Add(new ListItem("&laquo;", "1", Pagina > 1));
                pages.Add(new ListItem("...", Pagina.ToString(), true));
            }
            for (int i = (Pagina + 1); i < (Pagina + 3); i++)
            {
                if (i <= pageCount)
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != Pagina + 1));
            }
            if (pageCount > 2 && Pagina < (pageCount - 2))
            {
                pages.Add(new ListItem("...", (Pagina + 2).ToString(), true));
                pages.Add(new ListItem("&raquo;", pageCount.ToString(), Pagina < pageCount - 1));
            }
        }

        if (TotalRegistros > 1)
        {
            int d = (Pagina * Cantidad) + 1;
            int t = (Pagina + 1) * Cantidad;
            if (TotalRegistros < t)
                t = TotalRegistros;
            //contador.Text = string.Format("{0}&ndash;{1} de {2}", d, t, TotalRegistros);
        }
        else
        {
            //contador.Text = string.Format("Total registros: {0}", TotalRegistros);
        }

        rptPaginas.DataSource = pages;
        rptPaginas.DataBind();
    }

    protected void btnPagina_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        int pagina;
        int.TryParse(btn.CommandArgument, out pagina);
        if (pagina > 0) pagina -= 1;
        page = pagina;
        if (Click != null)
        {
            LinkButton btnAceptar = new LinkButton();
            btnAceptar.CommandArgument = pagina.ToString();
            Click(btnAceptar, new EventArgs());
        }
    }
}