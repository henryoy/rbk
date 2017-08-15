using cm.mx.catalogo.Controller;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cm.mx.catalogo.Model;
using System.Web.Services;
using rbk.mailrelay.Helper;

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

    private Distribucion oDistribucion
    {
        get
        {
            Distribucion _Distribucion = new Distribucion();
            if (ViewState["oDistribucion"] != null)
            {
                _Distribucion = ViewState["oDistribucion"] as Distribucion;

            }
            return _Distribucion;
        }
        set
        {
            ViewState["oDistribucion"] = value;
        }
    }

    public Mailrelaylog oMailrelaylog
    {
        get
        {
            Mailrelaylog _oCampana = new Mailrelaylog();
            if (ViewState["oMrCampana"] != null)
            {
                _oCampana = ViewState["oMrCampana"] as Mailrelaylog;

            }
            return _oCampana;
        }
        set
        {
            ViewState["oMrCampana"] = value;
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

    private string codeApiRelay
    {
        get
        {
            string apiRelay = string.Empty;

            if (ViewState["ApiRelay"] != null)
            {
                apiRelay = ViewState["ApiRelay"] as string;
            }

            return apiRelay;
        }
        set
        {
            ViewState["ApiRelay"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            oMailrelaylog = new Mailrelaylog();
            CargarValores();
        }
    }

    private void CargarValores()
    {
        if (CampanaId > 0)
        {
            cCatalogo = new CatalogoController();
            oCampana = cCatalogo.GetCampana(CampanaId);

            if (oCampana != null && oCampana.CampanaId > 0)
            {
                oPlantilla = cCatalogo.GetPlantilla(oCampana.PlantillaId);
                oDistribucion = cCatalogo.GetDistribucion(oCampana.DistribucionId);

                ltTemplate.Text = oPlantilla.Html;
                oMailrelaylog = cCatalogo.GetMailLogCampanaId(oCampana.CampanaId);
                if (oMailrelaylog != null && oMailrelaylog.MRCampanaId > 0)
                {
                    //cargar plantilla mail relay
                    templateCode.Text = "<div class='parentOfBg'>" + oMailrelaylog.Html + "</div>";
                }

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
            if (htmlHidden != null)
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
        //Mailrelaylog oMailLog = new Mailrelaylog();
        oMailrelaylog = new Mailrelaylog();
        string Html = htmlHidden.Value;

        oMailrelaylog.CampanaId = CampanaId;
        oMailrelaylog.Html = Html;
        if (oMailrelaylog.MRGrupoId == null)
        {
            if (oDistribucion.MRGroupId == null)
                oMailrelaylog.MRGrupoId = 1;
            else
                oMailrelaylog.MRGrupoId = oDistribucion.MRGroupId;
        }

        oMailrelaylog.NombreCampana = oCampana.Nombre;

        cCatalogo = new CatalogoController();

        Mailrelaylog Mail = cCatalogo.GuardarMailLog(oMailrelaylog);

        if (string.IsNullOrEmpty(codeApiRelay))
        {
            codeApiRelay = rbk.mailrelay.RbkMail.Codigo;
        }

        rbk.mailrelay.RbkMail oRbkMail = new rbk.mailrelay.RbkMail();

        int CampaniaMailRelayId = 0;

        rbk.mailrelay.Model.Campana oRbkCampana = new rbk.mailrelay.Model.Campana();
        oRbkCampana.apiKey = codeApiRelay;
        oRbkCampana.campaignFolderId = 1;
        oRbkCampana.mailboxFromId = 2;
        oRbkCampana.mailboxReplyId = 2;
        oRbkCampana.mailboxReportId = 2;
        oRbkCampana.emailReport = true;
        oRbkCampana.id = (Mail.MRCampanaId ?? 0);
        //oRbkCampana.groups
        oRbkCampana.html = Mail.Html;
        oRbkCampana.subject = oCampana.Nombre;
        oRbkCampana.text = oCampana.MensajePrevio;

        if (oRbkCampana.id == 0)
        {
            CampaniaMailRelayId = oRbkMail.addCampaign(oRbkCampana);
            oRbkCampana.id = CampaniaMailRelayId;
        }
        else
        {
            CampaniaMailRelayId = oRbkMail.updateCampaign(oRbkCampana);
            oRbkCampana.id = CampaniaMailRelayId;
        }

        if (cCatalogo.Exito)
        {
            oMailrelaylog.MRCampanaId = oRbkCampana.id;
            Mailrelaylog _Mail = cCatalogo.GuardarMailLog(oMailrelaylog);
            
        }
    }
}