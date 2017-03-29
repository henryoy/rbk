using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cm.mx.catalogo.Controller;
using cm.mx.catalogo.Model;
public partial class Dashboard_Promociones : System.Web.UI.Page
{
    private CatalogoController cCatalogo;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.CargaInicial();
        }
    }
    private void CargaInicial()
    {
        List<Promocion> lsPromocion = new List<Promocion>();
        cCatalogo = new CatalogoController();
        lsPromocion = cCatalogo.GetAllPromocion();
        rptPromociones.DataSource = lsPromocion;
        rptPromociones.DataBind();
    }
    protected void lnkEliminar_Click(object sender, EventArgs e)
    {
        //String CustomerID = e.CommandArgument.ToString();
        LinkButton source = (LinkButton)sender;
        String CustomerID = source.CommandArgument.ToString();
        

    }
}