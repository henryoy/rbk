using cm.mx.catalogo.Controller;
using cm.mx.catalogo.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class membresias : System.Web.UI.Page
{
    #region
    CatalogoController _catalogo;
    CatalogoController cCatalogo
    {
        get
        {
            if (_catalogo == null) _catalogo = new CatalogoController();
            return _catalogo;
        }
    }
    int MembresiaId
    {
        get
        {
            var temp = ViewState["id"];
            return temp == null ? 0 : (int)temp;
        }
        set
        {
            ViewState["id"] = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarMembresias();
        }
    }

    protected void CargarMembresias()
    {
        var lsMembresia = cCatalogo.GetAllMembresias();
        rptItems.DataSource = lsMembresia;
        rptItems.DataBind();
    }

    protected void llenarCampos(bool read)
    {
        var oMembresia = cCatalogo.GetMembresia(MembresiaId);
        if (oMembresia == null) oMembresia = new Tipomembresia();
        var filePath = (ConfigurationManager.AppSettings["RutaImagenes"]).Replace("~", "");

        txtColor.Text = oMembresia.Color;
        txtDescuento.Text = string.Format("{0:N2}", oMembresia.Porcientodescuento);
        txtNombre.Text = oMembresia.Nombre;
        txtVisitasMax.Text = oMembresia.Hasta.ToString();
        txtVisitasMin.Text = oMembresia.ApartirDe.ToString();
        txtColor.Enabled = !read;
        txtDescuento.ReadOnly = read;
        txtNombre.ReadOnly = read;
        txtVisitasMax.ReadOnly = read;
        txtVisitasMin.ReadOnly = read;

        if (string.IsNullOrEmpty(oMembresia.UrlImagen))
            imgTarjeta.ImageUrl = "~/Images/icon-gallery.svg";
        else
        {
            imgTarjeta.ImageUrl = filePath + oMembresia.UrlImagen;
            hfTajeta.Value = filePath + oMembresia.UrlImagen.Replace(" ", "%20");
        }


        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "open", "VerPopUp();", true);
    }

    protected void btnEditar_Click(object sender, EventArgs e)
    {
        try
        {
            MembresiaId = int.Parse(((LinkButton)sender).CommandArgument);
            llenarCampos(false);
        }
        catch (Exception ex)
        {
            Funciones.MostarMensajes("error", new List<string> { ex.Message });
        }
    }

    protected void AddMembresia_Click(object sender, EventArgs e)
    {
        try
        {
            MembresiaId = 0;
            llenarCampos(false);
        }
        catch (Exception ex)
        {
            Funciones.MostarMensajes("error", new List<string> { ex.Message });
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        imgTarjeta.ImageUrl = hfTajeta.Value;
        hfTajeta.Value = hfTajeta.Value.Replace(" ", "%20");
        try
        {
            int min;
            int max;
            decimal descuento;
            int.TryParse(txtVisitasMin.Text.Trim(), NumberStyles.AllowThousands, new NumberFormatInfo() { NumberGroupSeparator = "," }, out min);
            int.TryParse(txtVisitasMax.Text.Trim(), NumberStyles.AllowThousands, new NumberFormatInfo() { NumberGroupSeparator = "," },out max);
            decimal.TryParse(txtDescuento.Text, out descuento);
            var filePath = (ConfigurationManager.AppSettings["RutaImagenes"]).Replace("~", "");
            Tipomembresia oMembresia = new Tipomembresia();
            oMembresia.Color = txtColor.Text;
            oMembresia.Estado = "ACTIVO";
            oMembresia.Nombre = txtNombre.Text;
            oMembresia.Color = txtColor.Text.Trim();
            oMembresia.ApartirDe = min;
            oMembresia.Hasta = max;
            oMembresia.Porcientodescuento = descuento;
            oMembresia.Membresiaid = MembresiaId;
            oMembresia.UrlImagen = hfTajeta.Value.Replace(filePath, "");

            var r = cCatalogo.GuardarMembresia(oMembresia);
            if (cCatalogo.Exito)
            {
                CargarMembresias();
                Funciones.MostarMensajes("success", new List<string> { "La operación se completo correctamente" });
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "open", "closePopup(); $('#popupOverlay').hide();", true);
            }
            else
            {
                Funciones.MostarMensajes("error", cCatalogo.Mensajes);
            }
        }
        catch (Exception ex)
        {
            Funciones.MostarMensajes("error", new List<string> { ex.Message });
        }
    }

    protected void btnVer_Click(object sender, EventArgs e)
    {
        try
        {
            MembresiaId = int.Parse(((LinkButton)sender).CommandArgument);
            llenarCampos(true);
        }
        catch (Exception ex)
        {
            Funciones.MostarMensajes("error", new List<string> { ex.Message });
        }
    }

    protected void btnBaja_Click(object sender, EventArgs e)
    {
        try
        {
            MembresiaId = int.Parse(((LinkButton)sender).CommandArgument);
            var r = cCatalogo.BajaMembresia(MembresiaId);
            if (cCatalogo.Exito)
            {
                CargarMembresias();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "open", "closePopup(); $('#popupOverlay').hide();", true);
            }
            else
            {
                Funciones.MostarMensajes("error", cCatalogo.Mensajes);
            }
        }
        catch (Exception ex)
        {
            Funciones.MostarMensajes("error", new List<string> { ex.Message });
        }
    }
}
