using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using rbk.mailrelay.Model;
using cm.mx.catalogo.Model.ObjectVM;
using cm.mx.catalogo.Controller;
using cm.mx.catalogo.Model;

public partial class Dashboard_index : System.Web.UI.Page
{
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
            this.InitEstadisticas();
        }
    }

    public void InitEstadisticas()
    {
        try
        {
            int iSuscriber = 0;
            int iSuscriberMonth = 0;
            iSuscriber = cCatalogo.GetSuscriberNow();
            iSuscriberMonth = cCatalogo.GetSuscriberMonthNow();
            SuscriberHoy.Text = Convert.ToString(iSuscriber);
            SuscriberMes.Text = Convert.ToString(iSuscriberMonth);


            //List<cm.mx.catalogo.Model.Mailrelaylog> lsMail = new List<cm.mx.catalogo.Model.Mailrelaylog>();
            List<Vicampana> lsMail = new List<Vicampana>();
            // lsMail = cCatalogo.GetAllMailLog();
            lsMail = cCatalogo.GetAllMrCampana();
            dpCampana.Items.Clear();
            dpCampana.Items.Insert(0, new ListItem()
            {
                Value = "0",
                Text = "Seleccione una campaña"
            });

            foreach (Vicampana oItem in lsMail)
            {
                dpCampana.Items.Add(new ListItem()
                {
                    Value = Convert.ToString(oItem.MRCampanaId),
                    Text = Convert.ToString(oItem.Nombre)
                });
            }

            var now = DateTime.Now;
            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            txtFechaInicio.Text = startOfMonth.ToString("yyyy-MM-dd");
            txtFechaFinal.Text = now.ToString("yyyy-MM-dd");
            /*Carga inicial*/
            DateTime oInit = new DateTime();
            DateTime oEnd = new DateTime();
            int DistribucionId = 0;

            DateTime.TryParse(txtFechaInicio.Text, out oInit);
            DateTime.TryParse(txtFechaFinal.Text, out oEnd);

            rbk.mailrelay.RbkMail oRbkMail = new rbk.mailrelay.RbkMail();
            if (string.IsNullOrEmpty(codeApiRelay))
            {
                codeApiRelay = rbk.mailrelay.RbkMail.Codigo;
            }

            CharData oChar = this._GetClickInfo(oInit, oEnd, DistribucionId);

            ScriptManager.RegisterStartupScript(this,
                 this.GetType(),
                 "ReloadStatistics",
                 "ReloadStatistics(" + Newtonsoft.Json.JsonConvert.SerializeObject(oChar) + ");",
                 true);

        }
        catch (Exception ex)
        {

        }
    }

    [WebMethod]
    public static ResultStatistics GetGeneral()
    {
        ResultStatistics oResult = new ResultStatistics();
        try
        {
            string codeApiRelay = string.Empty;

            rbk.mailrelay.RbkMail oRbkMail = new rbk.mailrelay.RbkMail();
            if (string.IsNullOrEmpty(codeApiRelay))
            {
                codeApiRelay = rbk.mailrelay.RbkMail.Codigo;
            }
            Statistics objeto = new Statistics();
            objeto.apiKey = codeApiRelay;

            oResult = oRbkMail.getStats(objeto);
        }
        catch (Exception innerException)
        {

        }

        return oResult;
    }

    [WebMethod]
    public static CharData GetClickInfo()
    {
        CharData oChar = new CharData();
        try
        {
            string codeApiRelay = string.Empty;

            rbk.mailrelay.RbkMail oRbkMail = new rbk.mailrelay.RbkMail();
            if (string.IsNullOrEmpty(codeApiRelay))
            {
                codeApiRelay = rbk.mailrelay.RbkMail.Codigo;
            }
            var now = DateTime.Now;
            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            string txtFechaInicio = startOfMonth.ToString("yyyy-MM-dd");
            string txtFechaFinal = now.ToString("yyyy-MM-dd");

             DateTime oInit = new DateTime();
            DateTime oEnd = new DateTime();
            int DistribucionId = 0;

            DateTime.TryParse(txtFechaInicio, out oInit);
            DateTime.TryParse(txtFechaFinal, out oEnd);

            Statistics objeto = new Statistics();
            objeto.apiKey = codeApiRelay;
            objeto.startDate = oInit;
            objeto.endDate = oEnd;
            //objeto.id = DistribucionId;

            oChar.lsClickInfo = oRbkMail.getClicksInfo(objeto);


            var results = oChar.lsClickInfo.GroupBy(
                p => (p.date.Date.ToShortDateString()),
                (key, g) => new { date = key, Click = g.ToList() });
            
            foreach (DateTime day in EachDay(oInit, oEnd))
            {

                var value = results.FirstOrDefault(f => Convert.ToDateTime(f.date) == day);
                if (value != null)
                {
                    int _Click = value.Click.Count();
                    DateTime oDay = Convert.ToDateTime(value.date);

                    string SDay = oDay.Month + " - " + oDay.Day;

                    oChar.clicksArray.Add(Convert.ToString(_Click));
                    oChar.daysArray.Add(SDay);
                }
                else
                {
                    string SDay = day.Month + " - " + day.Day;
                    oChar.clicksArray.Add("0");
                    oChar.daysArray.Add(SDay);
                }
            }


        }
        catch (Exception innerException)
        {

        }

        return oChar;
    }

    public static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
    {
        for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
            yield return day;
    }

    protected void lnkVerEstadistica_Click(object sender, EventArgs e)
    {
        DateTime oInit = new DateTime();
        DateTime oEnd = new DateTime();
        int DistribucionId = 0;

        DateTime.TryParse(txtFechaInicio.Text, out oInit);
        DateTime.TryParse(txtFechaFinal.Text, out oEnd);

        int.TryParse(dpCampana.SelectedValue, out DistribucionId);

        rbk.mailrelay.RbkMail oRbkMail = new rbk.mailrelay.RbkMail();
        if (string.IsNullOrEmpty(codeApiRelay))
        {
            codeApiRelay = rbk.mailrelay.RbkMail.Codigo;
        }

        CharData oChar = this._GetClickInfo(oInit,oEnd, DistribucionId);

        ScriptManager.RegisterStartupScript(this,
             this.GetType(),
             "ReloadStatistics",
             "ReloadStatistics(" + Newtonsoft.Json.JsonConvert.SerializeObject(oChar) + ");",
             true);

    }

    public CharData _GetClickInfo(DateTime Init, DateTime End, int DistribucionId)
    {
        CharData oChar = new CharData();
        try
        {
           rbk.mailrelay.RbkMail oRbkMail = new rbk.mailrelay.RbkMail();
            if (string.IsNullOrEmpty(codeApiRelay))
            {
                codeApiRelay = rbk.mailrelay.RbkMail.Codigo;
            }

            Statistics objeto = new Statistics();
            objeto.apiKey = codeApiRelay;
            objeto.startDate = Init;
            objeto.endDate = End;
            objeto.id = DistribucionId;

            /*
             getStats
             getClicksInfo
             getUniqueClicksInfo
             getImpressionsInfo
             getUniqueImpressionsInfo
             */

            oChar.lsClickInfo               = oRbkMail.getClicksInfo(objeto);
            oChar.oResultStatistics         = oRbkMail.getStats(objeto);
            oChar.lsResultImpressionsInfo   = oRbkMail.getImpressionsInfo(objeto);
            oChar.oUniqueClicksInfo         = oRbkMail.getUniqueClicksInfo(objeto);
            oChar.lsResultImpressionsInfo   = oRbkMail.getUniqueImpressionsInfo(objeto);
            
            var results = oChar.lsClickInfo.GroupBy(
                p => (p.date.Date.ToShortDateString()),
                (key, g) => new { date = key, Click = g.ToList() });

            foreach (DateTime day in EachDay(Init, End))
            {

                var value = results.FirstOrDefault(f => Convert.ToDateTime(f.date) == day);
                if (value != null)
                {
                    int _Click = value.Click.Count();
                    DateTime oDay = Convert.ToDateTime(value.date);

                    string SDay = oDay.Month + " - " + oDay.Day;

                    oChar.clicksArray.Add(Convert.ToString(_Click));
                    oChar.daysArray.Add(SDay);
                }
                else
                {
                    string SDay = day.Month + " - " + day.Day;
                    oChar.clicksArray.Add("0");
                    oChar.daysArray.Add(SDay);
                }
            }  

        }
        catch (Exception innerException)
        {

        }

        return oChar;
    }

    protected void ucCatalogo_Click(object sender, EventArgs e)
    {
        List<KeyValuePair<string, object>> _DResultado = new List<KeyValuePair<string, object>>();
        _DResultado = ucCatalogos.Resultado;
        if (ucCatalogos.ClaveGRID == "SCLIENTE")
        {            
            //hdCampanaId.Value = _DResultado.FirstOrDefault(f => f.Key.ToUpper() == "CLAVE").Value.ToString();
            //txtCampana.Text = _DResultado.FirstOrDefault(f => f.Key.ToUpper() == "NOMBRE").Value.ToString();
        }      
    }

    protected void lnkBuscar_Click(object sender, EventArgs e)
    {   
        ucCatalogos.open();
    }
}