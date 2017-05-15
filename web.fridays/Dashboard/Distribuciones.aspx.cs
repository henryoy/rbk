using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cm.mx.catalogo.Controller;

public partial class Dashboard_Distribuciones : System.Web.UI.Page
{
    #region Variables
    CatalogoController _catalogo;
    CatalogoController cCatalogo
    {
        get
        {
            if (_catalogo == null) _catalogo = new CatalogoController();
            return _catalogo;
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarDistribucion();
        }
    }

    protected void CargarDistribucion()
    {
        try
        {
            var ls = cCatalogo.GetDistribucion(null);
            rptItems.DataSource = ls;
            rptItems.DataBind();
        }
        catch (Exception ex)
        {
            Funciones.MostarMensajes("error", new List<string> { ex.Message });
        }
    }

    protected void add_subscriber_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Dashboard/Distribucion");
    }

    protected void btnEditar_Click(object sender, EventArgs e)
    {
        int id = int.Parse(((LinkButton)sender).CommandArgument);
        var url = "~/Dashboard/Distribucion?id=" + id;
        Response.Redirect(url);
    }

    protected void btnBaja_Click(object sender, EventArgs e)
    {
        try
        {
            int Distribucion = int.Parse(((LinkButton)sender).CommandArgument);
            if (cCatalogo.BajaDistribucion(Distribucion))
            {
                CargarDistribucion();
            }
            else
            {
                Funciones.MostarMensajes("error", cCatalogo.Mensajes);
            }
        }
        catch (Exception ex)
        {
            Funciones.MostarMensajes("error", new List<string>() { ex.Message });
        }
    }
}