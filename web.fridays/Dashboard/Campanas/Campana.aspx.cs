using cm.mx.catalogo.Controller;
using cm.mx.catalogo.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard_Campanas_Campana : System.Web.UI.Page
{

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

    public string CampanaId
    {
        get
        {
            string Id = string.Empty;

            if (ViewState["CampanaId"] != null)
            {
                Id = ViewState["CampanaId"] as string;                
            }
            return Id;

        }
        set
        {
            ViewState["CampanaId"] = value;
        }
    }

    private CatalogoController cCatalogo;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int thePID = 0;
            oCampana = new Campana();
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                thePID = Convert.ToInt32(Request.QueryString["id"]);
                CampanaId = Convert.ToString(thePID);
            }                 

            Carga(thePID);
        }
    }
    public void Carga(int CampanaId = 0)
    {
        cCatalogo = new CatalogoController();
        
        if (CampanaId > 0) {
            oCampana = cCatalogo.GetCampana(CampanaId);
            if (cCatalogo.Exito)
            {
                txtTitulo.Text = oCampana.Nombre;
                txtMsjPrevio.Text = oCampana.MensajePrevio;
                dpTipoCampana.SelectedIndex = dpTipoCampana.Items.IndexOf(dpTipoCampana.Items.FindByValue(oCampana.TipoCampana));
                ActiveSidebar(oCampana.TipoCampana);
                
                if (oCampana.Programacion)
                {
                    if(oCampana.FechaProgramacion.HasValue){
                        send_schedule_campaign_day.Value = oCampana.FechaProgramacion.Value.ToShortDateString();
                        var src = oCampana.FechaProgramacion.Value;
                        var hm = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0);
                        send_schedule_campaign_day.Value = src.ToShortDateString();
                        //send_schedule_campaign_time.Value = Convert.ToString(hm);
                        checkCalendar();
                    }
                }

                dpDistribucion.SelectedIndex = dpDistribucion.Items.IndexOf(dpDistribucion.Items.FindByValue(Convert.ToString(oCampana.DistribucionId)));
                dpPromocion.SelectedIndex = dpPromocion.Items.IndexOf(dpPromocion.Items.FindByValue(Convert.ToString(oCampana.PromocionId)));
                dpPlantilla.SelectedIndex = dpPlantilla.Items.IndexOf(dpPlantilla.Items.FindByValue(Convert.ToString(oCampana.PlantillaId)));
                dpTipoDestino.SelectedIndex = dpTipoDestino.Items.IndexOf(dpTipoDestino.Items.FindByText(oCampana.DestinoCampana));

            }
        }        
    }

    private void ActiveSidebar(string tipo)
    {
        ScriptManager.RegisterStartupScript(this,
              this.GetType(),
              "StartupSidebar",
              "ActiveSidebar();",
              true);
    }
    private void checkCalendar()
    {
        ScriptManager.RegisterStartupScript(this,
              this.GetType(),
              "StartupCalendar",
              "ActiveCalendar();",
              true);
    }
    protected void lnkGuardarCampana_Click(object sender, EventArgs e)
    {
        string Tipo = string.Empty;
        string msj = string.Empty;
              

        cCatalogo = new CatalogoController();
        Campana _oCampana = new Campana();
        Campana _Result = new Campana();
        _oCampana = SetCampana();
        _Result = cCatalogo.GuardarCampana(_oCampana);
        if (cCatalogo.Exito)
        {
            CampanaId = Convert.ToString(_Result.CampanaId);
            ActiveSidebar(oCampana.TipoCampana);

            msj = "La campaña ha sido guardada correctamente";
            oCampana = _Result;
            ScriptManager.RegisterStartupScript(
                   this,
                   this.GetType(),
                   "StartupScript",
                   "notification('" + msj + "','success')",
                   true);
            
        }
        else
        {
            msj = Funciones.FormatoMsj(cCatalogo.Mensajes);

            ScriptManager.RegisterStartupScript(
                  this,
                  this.GetType(),
                  "StartupScript",
                  "notification('" + msj + "','error')",
                  true);
        }

        upPromocion.Update();
    }
    private Campana SetCampana()
    {
        Campana oCampana = new Campana();
        int sCampanaId = 0;
        int.TryParse(CampanaId, out sCampanaId);
        oCampana.CampanaId = sCampanaId;
        oCampana.Nombre = txtTitulo.Text;
        oCampana.MensajePrevio = txtMsjPrevio.Text;
        if (!string.IsNullOrEmpty(send_schedule_campaign_day.Value))
        {
            if (!string.IsNullOrEmpty(send_schedule_campaign_time.Value))
            {
                oCampana.Programacion = true;
                oCampana.FechaProgramacion = Convert.ToDateTime(send_schedule_campaign_day.Value);
            }
            else
            {
                oCampana.Programacion = true;
                oCampana.FechaProgramacion = Convert.ToDateTime(send_schedule_campaign_day.Value);
            }

        }
        else oCampana.Programacion = false;

        if (dpTipoCampana.SelectedValue != "0")        
            oCampana.TipoCampana = dpTipoCampana.SelectedValue;

        oCampana.DestinoCampana = dpTipoDestino.SelectedValue;
        int PlantillaId = 0;
        int.TryParse(dpPlantilla.SelectedValue, out PlantillaId);
        oCampana.PlantillaId = PlantillaId;
        int DistribucionId = 0;
        int.TryParse(dpDistribucion.Text, out DistribucionId);
        oCampana.DistribucionId = DistribucionId;
        int PromocionId = 0;
        int.TryParse(dpPromocion.Text, out PromocionId);
        oCampana.PromocionId = PromocionId;
        

        return oCampana;
    }
}