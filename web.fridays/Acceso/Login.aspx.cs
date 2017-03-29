using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cm.mx.catalogo.Controller;
using cm.mx.catalogo.Model;

public partial class Login : System.Web.UI.Page
{
    CatalogoController _catalogo;
    CatalogoController cCatalogo
    {
        get
        {
            if (_catalogo == null) _catalogo = new CatalogoController();
            return _catalogo;
        }
    }
    List<string> mensajes = new List<string>();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void lnkAcceder_Click(object sender, EventArgs e)
    {
        try
        {
            string url = string.Empty;

            mensajes.Clear();
            if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                mensajes.Add("¡Usuario invalido!");
            }
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                mensajes.Add("¡Contraseña invalida!");
            }
            if (mensajes.Count > 0)
            {
                MostrarMensaje();
                return;
            }

            string urlAcceso = string.Empty;
            string Usuario = txtEmail.Text.Trim();

            if (!cCatalogo.Login(Usuario, txtPassword.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "StartupScript",
                    "notification('El usuario o la contraseña no existe','error')",
                    true);
                mensajes.AddRange(cCatalogo.Mensajes);
                MostrarMensaje();
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(
                  this,
                  this.GetType(),
                  "StartupScript",
                  "notification('El usuario o la contraseña no existe','success')",
                  true);
            }

            Session["Usuario"] = Usuario;
            urlAcceso = "~/Dashboard/index";
            Response.Redirect(urlAcceso);
        }
        catch (Exception ex)
        {
            mensajes.Add(ex.Message);
            MostrarMensaje();
        }
    }
    protected void MostrarMensaje()
    {
        string msj = "";
        foreach (string m in mensajes)
        {
            msj += "<li><label id=\"error-usuario\" class=\"error\">" + m + "</label></li>";
        }
        errosmj.InnerHtml = msj;
        errorContent.Visible = true;
    }
}
