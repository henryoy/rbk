using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cm.mx.catalogo.Controller;
using cm.mx.catalogo.Model;

public partial class Dashboard_Notificacion_asignarventa : System.Web.UI.Page
{
    private CatalogoController cCatalogo;
    #region propiedades
    private List<string> _errores = new List<string>();
    private List<string> _mensajes = new List<string>();
    private string _mensaje = string.Empty;
    private bool _exito = false;

    private int _totalColumnas = 0;
    public int TotalColumnas
    {
        get { return _totalColumnas; }
        set { _totalColumnas = value; }
    }
    public List<string> Errores
    {
        get { return _errores; }
    }
    public bool Exito
    {
        get
        {
            return _exito;
        }
        set
        {
            _exito = value;
        }
    }
    public string Mensaje
    {
        get
        {
            return _mensaje;
        }
        set
        {
            _mensaje = value;
        }
    }
    public List<string> Mensajes
    {
        get { return _mensajes; }
    }
    #endregion
    public Usuario oUsuario
    {
        get
        {
            Usuario _oUsuario = new Usuario();
            if (ViewState["oUsuario"] != null)
            {
                _oUsuario = ViewState["oUsuario"] as Usuario;
            }
            return _oUsuario;
        }
        set
        {
            ViewState["oUsuario"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (Request.QueryString["id"] == null)
            //{
            //    Response.Redirect("~/Dashboard/index");
            //}
            //else
            //{
            int NotificacionId = Convert.ToInt32(Request.QueryString["id"]);
            Notificacion oNotificacion = new Notificacion();
            cCatalogo = new CatalogoController();
            oNotificacion = cCatalogo.GetNotificacion(NotificacionId);
            //if (!string.IsNullOrEmpty(oNotificacion.FolioNota))
            //{
            //    Response.Redirect("~/Dashboard/index");
            //    return;
            //}
            //if (oNotificacion.Usuario != null)
            //{
            //    hNombreUser.InnerText = oNotificacion.Usuario.Nombre;
            //    if (oNotificacion.Usuario.oTarjeta != null)
            //    {
            //        bTarjeta.InnerText = oNotificacion.Usuario.oTarjeta.Nombre;
            //        imgTarjeta.ImageUrl = oNotificacion.Usuario.oTarjeta.UrlImagen;
            //    }
            //    else
            //    {
            //        Tipomembresia oTipo = cCatalogo.GetMembresia(oNotificacion.Usuario.TarjetaID);
            //        if (oTipo != null)
            //        {
            //            bTarjeta.InnerText = oTipo.Nombre;
            //            imgTarjeta.ImageUrl = oTipo.UrlImagen;
            //        }
            //    }
            //}
            //}
        }
    }
    protected void lnkActualizarNot_Click(object sender, EventArgs e)
    {
        cCatalogo = new CatalogoController();

        string Codigo = txtFolio.Text;

        if (!string.IsNullOrEmpty(Codigo))
        {
            oUsuario = cCatalogo.getDatosUsuarioByCodigo(Codigo);
            if (oUsuario == null || oUsuario.Usuarioid == 0)
            {
                mainWrapper1.Visible = false;
                mainWrapper2.Visible = true;
                ScriptManager.RegisterStartupScript(
                  this,
                  this.GetType(),
                  "StartupScript",
                  "notification('El codigo no esta vinculado con ningun usuario','error');",
                  true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "open", "$('#popupOverlay').show();", true);
            }
            else
            {
                mainWrapper1.Visible = true;
                mainWrapper2.Visible = false;
                btnGuardar.Visible = true;
                btnGuardar.Text = "Generar visita";
                txtNombre.Text = oUsuario.Nombre;
                txtEmail.Text = oUsuario.Email;
                txtFechaNacimiento.Text = oUsuario.FechaNacimiento.ToShortDateString();
                txtVisita.Text = Convert.ToString(oUsuario.VisitaGlobal);
                txtTarjeta.Text = oUsuario.oTarjeta.Nombre;
                if (!string.IsNullOrEmpty(oUsuario.oTarjeta.UrlImagen))
                    imgTarjeta.ImageUrl = ResolveUrl(oUsuario.oTarjeta.UrlImagen);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "open", "$('#popupOverlay').show();", true);
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(
                  this,
                  this.GetType(),
                  "StartupScript",
                  "notification('El codigo esta vacío','error');",
                  true);
        }
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (btnGuardar.Text.ToUpper() == "GENERAR VISITA")
        {
            if (!string.IsNullOrEmpty(txtReferencia.Text))
            {
                bool isRegisterVisita = false;
                cCatalogo = new CatalogoController();
                int Sucursal = 0;
                if (Session["Sucursal"] != null)
                {
                    Sucursal = (int)Session["Sucursal"];
                }
                isRegisterVisita = cCatalogo.RegistroVisita(1, oUsuario.Usuarioid, txtReferencia.Text, Sucursal);
                if (isRegisterVisita)
                {
                    ScriptManager.RegisterStartupScript(
                 this,
                 this.GetType(),
                 "StartupScript",
                 "notification('Se ha registrado correctamente','success');",
                 true);

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "open", "$('#popupOverlay').hide();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(
                 this,
                 this.GetType(),
                 "StartupScript",
                 "notification('Ocurrio un error al registrar visita','error');",
                 true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(
                 this,
                 this.GetType(),
                 "StartupScript",
                 "notification('Necesita ingresar el folio para aplicar la visita','error');",
                 true);
            }
        }
    }
}