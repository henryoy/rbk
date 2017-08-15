using cm.mx.catalogo.Controller;
using cm.mx.catalogo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;

public partial class Dashboard_Campanas_Editor : System.Web.UI.MasterPage
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
    private CatalogoController cCatalogo;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void lnkEnviarCampana_Click(object sender, EventArgs e)
    {
        initDataCampana();

        rbk.mailrelay.RbkMail oRbkMail = new rbk.mailrelay.RbkMail();

        int CampaniaMailRelayId = 0;
        rbk.mailrelay.Model.SendCampana oSendCampana = new rbk.mailrelay.Model.SendCampana();
        cCatalogo = new CatalogoController();
        Mailrelaylog Mail = cCatalogo.GetMailLogCampanaId(oCampana.CampanaId);

        if(Mail == null)
        {
            return;
        }

        CampaniaMailRelayId = Mail.MRCampanaId ?? 0;

        oSendCampana.apiKey = codeApiRelay;
        oSendCampana.id = CampaniaMailRelayId;
        oSendCampana.email = "hency.oy@gmail.com";
        oSendCampana.vmta = 0;

        int IsSend = oRbkMail.sendCampaign(oSendCampana);

        Mail.MRSendCampana = IsSend;

        cCatalogo.GuardarMailLog(Mail);
    }

    protected void lnkEnviarTestCampana_Click(object sender, EventArgs e)
    {
        initDataCampana();
        rbk.mailrelay.RbkMail oRbkMail = new rbk.mailrelay.RbkMail();

        int CampaniaMailRelayId = 0;
        rbk.mailrelay.Model.SendCampana oSendCampana = new rbk.mailrelay.Model.SendCampana();
        oSendCampana.apiKey = codeApiRelay;
        oSendCampana.id = oCampana.CampanaId;
        oSendCampana.email = "hency.oy@gmail.com";
        oSendCampana.vmta = 0;

        int value = oRbkMail.sendCampaignTest(oSendCampana);
    }

    public void initDataCampana()
    {
        if (CampanaId > 0)
        {
            cCatalogo = new CatalogoController();

            if (oCampana.CampanaId == 0)
            {
                oCampana = cCatalogo.GetCampana(CampanaId);
            }
        }

        if (string.IsNullOrEmpty(codeApiRelay))
        {
            codeApiRelay = rbk.mailrelay.RbkMail.Codigo;
        }
    }

    protected void lnkSincronizar_Click(object sender, EventArgs e)
    {
        initDataCampana();
        rbk.mailrelay.RbkMail oRbkMail = new rbk.mailrelay.RbkMail();
        cCatalogo = new CatalogoController();
        int DistribucionId = oCampana.DistribucionId;
        int grupoId = 0;
        Distribucion oDistribucion = cCatalogo.GetDistribucion(oCampana.DistribucionId);

        if(oDistribucion.MRGroupId == null)
        {
            rbk.mailrelay.Model.Group oGroup = new rbk.mailrelay.Model.Group();
            oGroup.apiKey = codeApiRelay;
            oGroup.description = oDistribucion.Descripcion;
            oGroup.name = oDistribucion.Nombre;
            oGroup.visible = true;
            oGroup.enable = true;

            int MRGroupId = oRbkMail.addGroup(oGroup);
            
            if (MRGroupId > 0)
            {
                grupoId = MRGroupId;                
                cCatalogo.UpdateGroupId(oDistribucion.DistribucionID, MRGroupId);
            }

        }
        else
        {
            grupoId = oDistribucion.MRGroupId.Value;
        }

      

        if(oDistribucion != null && oDistribucion.DistribucionID > 0)
        {
            List<Usuario>  lsUsuarios = cCatalogo.GetEmailForDistribucionId(oCampana.DistribucionId);
            foreach(var item in lsUsuarios)
            {
                if(item.MRSuscriberId == null || item.MRSuscriberId == 0)
                {
                    rbk.mailrelay.Model.Suscriber oSuscriber = new rbk.mailrelay.Model.Suscriber();
                    oSuscriber.apiKey = codeApiRelay;
                    oSuscriber.email = item.Email;
                    oSuscriber.name = item.Nombre;
                    oSuscriber.groups = new List<int>() { grupoId };

                    int SuscriberId = oRbkMail.addSuscriber(oSuscriber);
                    if(SuscriberId > 0)
                        cCatalogo.UpdateSuscriberId(item.Usuarioid, SuscriberId);
                }
            }
        }
    }
}
