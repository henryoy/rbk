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
        if (!IsPostBack)
        {
            var lsSucursales = cCatalogo.GetAllSucursales();
            cbxSucursal.Items.Clear();
            cbxSucursal.DataSource = lsSucursales;
            cbxSucursal.DataTextField = "Nombre";
            cbxSucursal.DataValueField = "SucursalID";
            cbxSucursal.DataBind();
            cbxSucursal.Items.Insert(0, new ListItem { Text = "Seleccione una sucursal", Value = "" });
        }
    }

    protected void lnkAcceder_Click(object sender, EventArgs e)
    {
        try
        {
            //CatalogoController cCatalogo = new CatalogoController();
            //string Usuario = "hency.oy@gmail.com";
            //int ClienteID = 2;
            //string Referencia = "TESTING";
            //int SucursalId = 0;
            //bool isExito = false;
            //isExito = cCatalogo.RegistroVisita(Usuario, ClienteID, Referencia, SucursalId);
            //if (isExito)
            //{

            //}
            //return;
            string url = string.Empty;

            mensajes.Clear();
            if (string.IsNullOrEmpty(txtEmail.Text.Trim()) && string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                mensajes.Add("¡El usuario y contraseña esta vacío!");
                string mjs = string.Empty;

                mjs = Funciones.FormatoMsj(mensajes);

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "StartupScript",
                    "notification('" + mjs + "','error')",
                    true);
                return;
            }

            if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                mensajes.Add("¡Usuario invalido!");
            }
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                mensajes.Add("¡Contraseña invalida!");
            }

            if (string.IsNullOrEmpty(cbxSucursal.SelectedItem.Value))
            {
                string mjs = "Seleccione la sucursal";
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "StartupScript",
                    "notification('" + mjs + "','error')",
                    true);
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
                //mensajes.AddRange(cCatalogo.Mensajes);
                //MostrarMensaje();
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

            int Sucursal = 0;
            if (cbxSucursal.Items.Count > 0)
            {
                int.TryParse(cbxSucursal.SelectedItem.Value, out Sucursal);
            }

            Session["Usuario"] = Usuario;
            Session["Sucursal"] = Sucursal;
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
            msj += " " + m;
        }

        ScriptManager.RegisterStartupScript(
                  this,
                  this.GetType(),
                  "StartupScript",
                  "notification('" + msj + "','error')",
                  true);
    }
}
