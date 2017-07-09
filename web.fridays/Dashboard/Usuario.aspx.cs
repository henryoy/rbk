using cm.mx.catalogo.Controller;
using cm.mx.catalogo.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard_Usuario : System.Web.UI.Page
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
    private Usuario oUsuario
    {
        get
        {
            var temp = ViewState["usuario"];
            return temp == null ? null : (Usuario)temp;
        }
        set
        {
            ViewState["usuario"] = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarUsuarios();
        }
    }

    protected void CargarUsuarios()
    {
        try
        {
            List<CondicionDistribucion> ls = new List<CondicionDistribucion>();
            ls.Add(new CondicionDistribucion { Campo = "Estatus", Nexo = "Y", Operador = "=", Tipo = "Texto", Valor = "ACTIVO" });
            ls.Add(new CondicionDistribucion { Campo = "Tipo", Nexo = "Y", Operador = "=", Tipo = "Texto", Valor = "WEB" });
            var lsUsaurios = cCatalogo.GetUsaurios(ls);
            rptItems.DataSource = lsUsaurios;
            rptItems.DataBind();
        }
        catch (Exception ex)
        {
            Funciones.MostarMensajes("error", new List<string> { ex.Message });
        }
    }

    protected void Llenar(int UsuarioID)
    {
        try
        {
            oUsuario = cCatalogo.getUsuarioById(UsuarioID);
            if (oUsuario == null)
            {
                int Tarjetaid;
                var sMembresia = ConfigurationManager.AppSettings["TajetaDefault"];
                int.TryParse(sMembresia, out Tarjetaid);
                oUsuario = new Usuario
                {
                    Estatus = "ACTIVO",
                    FechaAlta = DateTime.Now,
                    Tipo = "WEB",
                    FechaBaja = new DateTime(1900, 01, 01),
                    Origen = "WEB",
                    Codigo = "00000",
                    oTarjeta = new Tipomembresia { Membresiaid = Tarjetaid }
                };
            }
            txtClave.Text = oUsuario.IdExterno;
            txtCorreo.Text = oUsuario.Email;
            txtFechaNac.Text = String.Format(CultureInfo.InvariantCulture, "{0:yyyy-MM-dd}", oUsuario.FechaNacimiento);
            txtNombre.Text = oUsuario.Nombre;
            txtPass.Attributes["value"] = oUsuario.Contrasena;
            txtPass2.Attributes["value"] = oUsuario.VerificacionContrasena;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "open", "VerPopUp();", true);
        }
        catch (Exception ex)
        {
            Funciones.MostarMensajes("error", new List<string> { ex.Message
});
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime fecha;
            DateTime.TryParse(txtFechaNac.Text.Trim(), out fecha);
            oUsuario.Contrasena = txtPass.Text.Trim();
            oUsuario.Email = txtCorreo.Text.Trim();
            oUsuario.FechaNacimiento = fecha;
            oUsuario.IdExterno = txtClave.Text.Trim();
            oUsuario.Nombre = txtNombre.Text.Trim();
            oUsuario.Imagen = "";

            if (!txtPass.Text.Equals(txtPass2.Text))
            {
                Funciones.MostarMensajes("error", new List<string> { "Las contraseñas no coinciden" });
                return;
            }

            oUsuario.Contrasena = txtPass.Text.Trim();
            oUsuario.VerificacionContrasena = txtPass2.Text.Trim();
            oUsuario.Email = txtCorreo.Text.Trim();

            if (!cCatalogo.GuardarUsuario(oUsuario))
            {
                txtPass.Attributes["value"] = txtPass.Text.Trim();
                txtPass2.Attributes["value"] = txtPass2.Text.Trim();
                var msjs = cCatalogo.Mensajes;
                if (cCatalogo.Errores.Count() > 0) msjs.AddRange(cCatalogo.Errores);
                Funciones.MostarMensajes("error", msjs);
            }
            else
            {
                CargarUsuarios();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "open", "closePopup(); $('#popupOverlay').hide();", true);
            }
        }
        catch (Exception ex)
        {
            Funciones.MostarMensajes("error", new List<string> { ex.Message });
        }
    }

    protected void btnUsaurio_Click(object sender, EventArgs e)
    {
        Llenar(0);
    }

    protected void btnBaja_Click(object sender, EventArgs e)
    {
        try
        {
            var UsuarioID = int.Parse(((LinkButton)sender).CommandArgument);
            if (!cCatalogo.BajaUsuario(UsuarioID)) Funciones.MostarMensajes("error", cCatalogo.Errores);
            else
            {
                CargarUsuarios();
            }
        }
        catch (Exception ex)
        {
            Funciones.MostarMensajes("error", new List<string> { ex.Message });
        }
    }

    protected void btnEditar_Click(object sender, EventArgs e)
    {
        var UsuarioID = int.Parse(((LinkButton)sender).CommandArgument);
        Llenar(UsuarioID);
    }
}