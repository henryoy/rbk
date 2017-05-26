using cm.mx.catalogo.Controller;
using cm.mx.catalogo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard_Notificacion_RedimirPromocion : System.Web.UI.Page
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

    public Notificacion oNotificacion
    {
        get
        {
            Notificacion _oNotificacion = new Notificacion();
            if (ViewState["Notificacion"] != null)
            {
                _oNotificacion = ViewState["Notificacion"] as Notificacion;
            }
            return _oNotificacion;
        }
        set
        {
            ViewState["Notificacion"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void RedimirPromocion()
    {
        RedimirPromocionVM oRedimirPromocion = new RedimirPromocionVM();
        cCatalogo = new CatalogoController();

        int NotificacionId = 0;
        int PromocionId = oNotificacion.PromocionID;        
        int SucursalId = 0;

        string Sucursal = string.Empty;
        if (Session["Sucursal"] != null)
        {
            Sucursal = Session["Sucursal"] as string;
        }
        int.TryParse(Sucursal, out SucursalId);
        oRedimirPromocion.UsuarioId = oUsuario.Usuarioid;
        oRedimirPromocion.UsuarioRedimioId = oUsuario.Usuarioid;
        oRedimirPromocion.PromocionId = PromocionId;
        oRedimirPromocion.PromocionRedimirId = 0;
        oRedimirPromocion.SucursalId = SucursalId;
        oRedimirPromocion.NotificacionId = oNotificacion.NotificacionId;

        bool isRedimido = cCatalogo.RedimirPromocion(oRedimirPromocion);
        if (isRedimido)
        {
            string msj = "Se redimio de manera correcta";

            ScriptManager.RegisterStartupScript(
                      this,
                      this.GetType(),
                      "StartupScript",
                      "notification('" + msj + "','success');",
                      true);
        }
        else
        {
            string msj = Funciones.FormatoMsj(cCatalogo.Mensajes);

            ScriptManager.RegisterStartupScript(
                      this,
                      this.GetType(),
                      "StartupScript",
                      "notification('" + msj + "','error');",
                      true);
            return;
        }

    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (btnGuardar.Text.ToUpper() == "REDIMIR PROMOCIÓN")
        {
            RedimirPromocion();
            //if (!string.IsNullOrEmpty(txtReferencia.Text))
            //{
            //    bool isRegisterVisita = false;
            //    cCatalogo = new CatalogoController();
            //    int Sucursal = 0;
            //    if (Session["Sucursal"] != null)
            //    {
            //        Sucursal = (int)Session["Sucursal"];
            //    }
            //    isRegisterVisita = cCatalogo.RegistroVisita("Administrador.root", oUsuario.Usuarioid, txtReferencia.Text, Sucursal);
            //    if (isRegisterVisita)
            //    {
            //        ScriptManager.RegisterStartupScript(
            //     this,
            //     this.GetType(),
            //     "StartupScript",
            //     "notification('Se ha registrado correctamente','success');",
            //     true);

            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "open", "$('#popupOverlay').hide();", true);
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(
            //     this,
            //     this.GetType(),
            //     "StartupScript",
            //     "notification('Ocurrio un error al registrar visita','error');",
            //     true);
            //    }
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(
            //     this,
            //     this.GetType(),
            //     "StartupScript",
            //     "notification('Necesita ingresar el folio para aplicar la visita','error');",
            //     true);
            //}
        }
    }
    protected void lnkActualizarNot_Click(object sender, EventArgs e)
    {
        try
        {
            
            cCatalogo = new CatalogoController();
            string Codigo = txtFolio.Text;
            int NotificacionId = 0;
            int.TryParse(Codigo, out NotificacionId);
            //string CodigoPromocion = txtPromocion.Text;

            if (!string.IsNullOrEmpty(Codigo))
            {
                //codigo es el numero de notificacion-id

                oNotificacion = cCatalogo.GetNotificacion(NotificacionId);
                if (oNotificacion != null && oNotificacion.NotifiacionID > 0)
                {
                    if (oNotificacion.PromocionID > 0 && oNotificacion.UsuarioID > 0)
                    {
                        oUsuario = cCatalogo.getUsuarioById(oNotificacion.UsuarioID);

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
                           btnGuardar.Text = "Redimir promoción";
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
                }
                else
                {
                    ScriptManager.RegisterStartupScript(
                     this,
                     this.GetType(),
                     "StartupScript",
                     "notification('La notificación no existe, verifique e intente de nuevo','error');",
                     true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(
                      this,
                      this.GetType(),
                      "StartupScript",
                      "notification('El codigo ó promocion esta vacío','error');",
                      true);
            }

        }
        catch (Exception InnerException)
        {
            ScriptManager.RegisterStartupScript(
                      this,
                      this.GetType(),
                      "StartupScript",
                      "notification('" + InnerException.Message + "','error');",
                      true);
            
        }
    }
}