using cm.mx.catalogo.Controller;
using cm.mx.catalogo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TipoInteress : System.Web.UI.Page
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
    int TipoInteresID
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
            CargarTiposInteres();
        }
    }

    protected void CargarTiposInteres()
    {
        try
        {
            var lstipo = cCatalogo.GetAllTipoInteres();
            rptItems.DataSource = lstipo;
            rptItems.DataBind();
        }
        catch (Exception ex)
        {
            Funciones.MostarMensajes("error", new List<string> { ex.Message });
        }
    }

    protected void llenarCampos()
    {
        var oTipo = cCatalogo.GetTipoInteres(TipoInteresID);
        if (oTipo == null) oTipo = new cm.mx.catalogo.Model.TipoInteres();

        txtDescripcion.Text = oTipo.Descripcion;
        txtNombre.Text = oTipo.Nombre;

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "open", "VerPopUp();", true);
    }

    protected void AddTipo_Click(object sender, EventArgs e)
    {
        TipoInteresID = 0;
        llenarCampos();
    }

    protected void btnBaja_Click(object sender, EventArgs e)
    {
        try
        {
            //Agregar para recuperar el usuaruio
            TipoInteresID = int.Parse(((LinkButton)sender).CommandArgument);
            var r = cCatalogo.BajaTipoInteres(TipoInteresID);
            if (cCatalogo.Exito)
            {
                CargarTiposInteres();
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

    protected void btnEditar_Click(object sender, EventArgs e)
    {
        try
        {
            TipoInteresID = int.Parse(((LinkButton)sender).CommandArgument);
            llenarCampos();
        }
        catch (Exception ex)
        {
            Funciones.MostarMensajes("error", new List<string> { ex.Message });
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            TipoInteres tipo = new TipoInteres();
            tipo.Descripcion = txtDescripcion.Text.Trim();
            tipo.Nombre = txtNombre.Text.Trim();
            
            if (cCatalogo.GuardarTipoInteres(tipo))
            {
                CargarTiposInteres();
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