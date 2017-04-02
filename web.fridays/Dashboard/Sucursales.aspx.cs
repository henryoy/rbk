using cm.mx.catalogo.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cm.mx.catalogo.Model;
public partial class Sucursales : System.Web.UI.Page
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
    int SucursalId
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
            CargarSucursales();
        }
    }

    protected void CargarSucursales()
    {
        try
        {
            var lsSucursales = cCatalogo.GetAllSucursales();
            rptItems.DataSource = lsSucursales;
            rptItems.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(
                 this,
                 this.GetType(),
                 "StartupScript",
                 "notification('" + ex.Message + "','error')",
                 true);
        }
    }

    protected void llenarCampos(bool read)
    {
        var oSucursal = cCatalogo.GetSucursal(SucursalId);
        if (oSucursal == null) oSucursal = new Sucursal();
        txtDireccion.Text = oSucursal.Direccion;
        txtLatitud.Text = oSucursal.Latitud.Equals(0) ? "" : String.Format("{0:N7}", oSucursal.Latitud);
        txtLongitud.Text = oSucursal.Longitud.Equals(0) ? "" : String.Format("{0:N7}", oSucursal.Longitud);
        txtNombre.Text = oSucursal.Nombre;
        txtBuscarMaps.Text = "";

        txtDireccion.ReadOnly = read;
        txtLatitud.ReadOnly = read;
        txtLongitud.ReadOnly = read;
        txtNombre.ReadOnly = read;

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "open", "VerPopUp(); initMap();", true);
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            float longitud;
            float latitud;
            float.TryParse(txtLatitud.Text, out latitud);
            float.TryParse(txtLongitud.Text, out longitud);
            Sucursal oSucursal = new Sucursal();
            oSucursal.Direccion = txtDireccion.Text.Trim();
            oSucursal.Nombre = txtNombre.Text.Trim();
            oSucursal.Latitud = latitud;
            oSucursal.Longitud = longitud;
            oSucursal.SucursalID = SucursalId;

            var r = cCatalogo.GuardarSucursal(oSucursal);
            if (cCatalogo.Exito)
            {
                CargarSucursales();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "open", "closePopup(); $('#popupOverlay').hide();", true);
                //ScriptManager.RegisterStartupScript(
                //this,
                //this.GetType(),
                //"StartupScript",
                //"notification('" + "La operación se realizó con exito" + "','error')",
                //true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(
                  this,
                  this.GetType(),
                  "StartupScript",
                  "notification('" + Funciones.FormatoMsj(cCatalogo.Mensajes) + "','error')",
                  true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(
                  this,
                  this.GetType(),
                  "StartupScript",
                  "notification('" + ex.Message + "','error')",
                  true);
        }
    }

    protected void btnEditar_Click(object sender, EventArgs e)
    {
        try
        {
            SucursalId = int.Parse(((LinkButton)sender).CommandArgument);
            llenarCampos(false);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(
                 this,
                 this.GetType(),
                 "StartupScript",
                 "notification('" + ex.Message + "','error')",
                 true);
        }
    }

    protected void AddSucursal_Click(object sender, EventArgs e)
    {
        SucursalId = 0;
        llenarCampos(false);
    }

    protected void btnVer_Click(object sender, EventArgs e)
    {
        try
        {
            SucursalId = int.Parse(((LinkButton)sender).CommandArgument);
            llenarCampos(true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(
                 this,
                 this.GetType(),
                 "StartupScript",
                 "notification('" + ex.Message + "','error')",
                 true);
        }
    }
}
