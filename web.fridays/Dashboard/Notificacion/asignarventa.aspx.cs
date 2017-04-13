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
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("~/Dashboard/index");
            }
            else
            {
                int NotificacionId = Convert.ToInt32(Request.QueryString["id"]);
                Notificacion oNotificacion = new Notificacion();
                cCatalogo = new CatalogoController();
                oNotificacion = cCatalogo.GetNotificacion(NotificacionId);
                if (!string.IsNullOrEmpty(oNotificacion.FolioNota))
                {
                    Response.Redirect("~/Dashboard/index");
                    return;
                }
                if (oNotificacion.Usuario != null)
                {
                    hNombreUser.InnerText = oNotificacion.Usuario.Nombre;
                    if (oNotificacion.Usuario.oTarjeta != null)
                    {
                        bTarjeta.InnerText = oNotificacion.Usuario.oTarjeta.Nombre;
                        imgTarjeta.ImageUrl = oNotificacion.Usuario.oTarjeta.UrlImagen;
                    }
                    else
                    {
                        Tipomembresia oTipo = cCatalogo.GetMembresia(oNotificacion.Usuario.TarjetaID);
                        if (oTipo != null)
                        {
                            bTarjeta.InnerText = oTipo.Nombre;
                            imgTarjeta.ImageUrl = oTipo.UrlImagen;
                        }
                    }
                }
            }
        }
    }
    protected void lnkActualizarNot_Click(object sender, EventArgs e)
    {
        cCatalogo = new CatalogoController();

        if (Request.QueryString["id"] != null)
        {
            int NotificacionId = Convert.ToInt32(Request.QueryString["id"]);
            if (NotificacionId > 0)
            {
                string FolioVnta = txtFolio.Text;
                bool isGuardado = cCatalogo.GuardarRerenciaNotifiacion(NotificacionId,FolioVnta);
                if (isGuardado)
                {
                    ScriptManager.RegisterStartupScript(
                  this,
                  this.GetType(),
                  "StartupScript",
                  "notification('Se ha actualizado el folio correctamente.','success')",
                  true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(
                 this,
                 this.GetType(),
                 "StartupScript",
                 "notification('No se pudo actualizar el folio.','error')",
                 true);
                }
            }
        }
    }
}