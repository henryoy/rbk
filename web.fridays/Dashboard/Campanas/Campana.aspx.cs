using cm.mx.catalogo.Controller;
using cm.mx.catalogo.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
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

        List<Promocion> lsPromocion = cCatalogo.GetAllPromocion();

        dpPromocion.Items.Clear();
        dpPromocion.Items.Insert(0, new ListItem()
        {
            Text = "Selecciona promoción",
            Value = "0"
        });

        foreach (Promocion oPromocion in lsPromocion)
        {
            ListItem oItem = new ListItem();
            oItem.Text = oPromocion.Titulo;
            oItem.Value = Convert.ToString(oPromocion.Promocionid);
            dpPromocion.Items.Add(oItem);
        }
        List<Distribucion> lsDistribucion = cCatalogo.GetAllDistribucion();
        dpDistribucion.Items.Clear();
        dpDistribucion.Items.Insert(0, new ListItem()
        {
            Text = "Selecciona Distribucion",
            Value = "0"
        });
        foreach (Distribucion oDistribucion in lsDistribucion)
        {
            ListItem oItem = new ListItem();
            oItem.Text = oDistribucion.Nombre;
            oItem.Value = Convert.ToString(oDistribucion.DistribucionID);
            dpDistribucion.Items.Add(oItem);
        }

        if (CampanaId > 0)
        {
            oCampana = cCatalogo.GetCampana(CampanaId);
            if (cCatalogo.Exito)
            {
                txtTitulo.Text = oCampana.Nombre;
                txtMsjPrevio.Text = oCampana.MensajePrevio;
                dpTipoCampana.SelectedIndex = dpTipoCampana.Items.IndexOf(dpTipoCampana.Items.FindByValue(oCampana.TipoCampana));
                ActiveSidebar(oCampana.TipoCampana);

                if (oCampana.Programacion)
                {
                    if (oCampana.FechaProgramacion.HasValue)
                    {
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
              "ActiveSidebar('" + tipo + "');",
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
        Campana _Result = new Campana();

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
                if (esvalida_la_hora(send_schedule_campaign_time.Value))
                {
                    oCampana.FechaProgramacion = Convert.ToDateTime(send_schedule_campaign_day.Value);

                    string thetime = send_schedule_campaign_time.Value;

                    if (thetime.Trim().Length < 5)
                        thetime = thetime = "0" + thetime;

                    string hh = thetime.Substring(0, 2);
                    string mm = thetime.Substring(3, 2);
                    double horas = 0;
                    double minutos = 0;
                    double.TryParse(hh, out horas);
                    double.TryParse(mm, out minutos);

                    DateTime oTime = oCampana.FechaProgramacion.Value;
                    oTime = oTime.AddHours(horas);
                    oTime = oTime.AddMinutes(minutos);

                    oCampana.FechaProgramacion = oTime;
                }
                else
                {
                    msj = "El formato de la hora no es correcto";
                    oCampana = _Result;
                    ScriptManager.RegisterStartupScript(
                           this,
                           this.GetType(),
                           "StartupScript",
                           "notification('" + msj + "','error')",
                           true);
                    return;
                }
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



        _Result = cCatalogo.GuardarCampana(oCampana);
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



            string RedirectUrl = "~/Dashboard/Template/index.aspx?id=" + oCampana.CampanaId;
            ////HttpContext.Current.Request.Url.AbsolutePath
            //Response.Redirect(RedirectUrl);
            Response.Redirect(RedirectUrl);
        }
        else
        {
            msj = Funciones.FormatoMsj(cCatalogo.Mensajes);
            ActiveSidebar(oCampana.TipoCampana);
            ScriptManager.RegisterStartupScript(
                  this,
                  this.GetType(),
                  "StartupScript",
                  "notification('" + msj + "','error')",
                  true);
        }

        upPromocion.Update();
    }
    public bool esvalida_la_hora(string thetime)
    {
        Regex checktime = new Regex("^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$");
        if (!checktime.IsMatch(thetime))
            return false;

        if (thetime.Trim().Length < 5)
            thetime = thetime = "0" + thetime;

        string hh = thetime.Substring(0, 2);
        string mm = thetime.Substring(3, 2);

        int hh_i, mm_i;
        if ((int.TryParse(hh, out hh_i)) && (int.TryParse(mm, out mm_i)))
        {
            if ((hh_i >= 0 && hh_i <= 23) && (mm_i >= 0 && mm_i <= 59))
            {
                return true;
            }
        }
        return false;
    }
    private Campana SetCampana()
    {
        
       


        return oCampana;
    }
}