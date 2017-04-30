using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cm.mx.catalogo.Controller;
using cm.mx.catalogo.Model;

public partial class Dashboard_Distribucion : System.Web.UI.Page
{
    private CatalogoController cCatalogo;
    List<CamposDistribucion> lsCampos
    {
        get
        {
            var temp = ViewState["campos"];
            return temp == null ? new List<CamposDistribucion>() : (List<CamposDistribucion>)temp;
        }
        set
        {
            ViewState["campos"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lsCampos = new List<CamposDistribucion>();
            cCatalogo = new CatalogoController();
            lsCampos = cCatalogo.GetAllCamposDistrubucion();
            grvCampos.DataSource = lsCampos;
            grvCampos.DataBind();
        }
    }

    protected void Page_LoadComplete()
    {
        if (grvCampos.Columns.Count > 0)
        {
            grvCampos.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow fila in grvCampos.Rows)
            {
                ((CheckBox)fila.Cells[0].Controls[1]).Checked = ((CheckBox)sender).Checked;
            }
        }
        catch (Exception ex)
        {

        }
    }
}