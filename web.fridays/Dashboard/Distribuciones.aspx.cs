using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cm.mx.catalogo.Controller;

public partial class Dashboard_Distribuciones : System.Web.UI.Page
{
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
            CatalogoController cCatalogo = new CatalogoController();
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
}