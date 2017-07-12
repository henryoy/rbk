using cm.mx.catalogo.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cm.mx.catalogo.Model;
using System.Web.Services;

public partial class Dashboard_Template_index : System.Web.UI.Page
{
    public int CampanaId
    {
        get
        {
            string Id = string.Empty;
            int _id = 0;

            if (Request.QueryString["id"] != null)
            {

                Id = Request.QueryString["id"];
                int.TryParse(Id, out _id);
            }
            return _id;

        }
    }
    private Campana oCampana
    {
        get
        {
            Campana _oCampana = new Campana();
            if (ViewState["oCampana"] != null)
            {
                _oCampana = ViewState["oCampana"] as Campana;

            }
            return _oCampana;
        }
        set
        {
            ViewState["oCampana"] = value;
        }
    }

    private Plantilla oPlantilla
    {
        get
        {
            Plantilla _oPlantilla = new Plantilla();
            if (ViewState["oPlantilla"] != null)
            {
                _oPlantilla = ViewState["oPlantilla"] as Plantilla;

            }
            return _oPlantilla;
        }
        set
        {
            ViewState["oPlantilla"] = value;
        }
    }
    private CatalogoController cCatalogo;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarValores();
        }
    }

    private void CargarValores()
    {
        if(CampanaId > 0)
        {
            cCatalogo = new CatalogoController();
            oCampana = cCatalogo.GetCampana(CampanaId);

            if(oCampana != null && oCampana.CampanaId > 0)
            {
                oPlantilla = cCatalogo.GetPlantilla(oCampana.PlantillaId);
                ltTemplate.Text = oPlantilla.Html;
                
            }
        }
    }

    [WebMethod]
    public static string GuardarLog(string campaignId)
    {
        string odata = "version";
        try
        {
            var id = campaignId;
            string shtml = string.Empty;

            Page page = (Page)HttpContext.Current.Handler;
            HiddenField htmlHidden = (HiddenField)page.FindControl("htmlHidden");
            if(htmlHidden != null)
            {
                shtml = htmlHidden.Value;
            }
 
        }
        catch (Exception innerException)
        {

        }
        return odata;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        Mailrelaylog oMailLog = new Mailrelaylog();

        string Html = htmlHidden.Value;

        oMailLog.CampanaId = CampanaId;
        oMailLog.Html = Html;
        
        oMailLog.MRCampanaId = 1;
        oMailLog.MRGrupoId = 1;
        cCatalogo = new CatalogoController();
        Mailrelaylog Mail = cCatalogo.GuardarMailLog(oMailLog);
        if (cCatalogo.Exito)
        {
            var _MAil = Mail;
        }
    }
}