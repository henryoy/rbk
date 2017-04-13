using cm.mx.catalogo.Controller;
using cm.mx.catalogo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class SiteMaster : MasterPage
{
    private const string AntiXsrfTokenKey = "__AntiXsrfToken";
    private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    private string _antiXsrfTokenValue;
    CatalogoController _cCat;
    CatalogoController cCatalogo
    {
        get
        {
            if (_cCat == null) _cCat = new CatalogoController();
            return _cCat;
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        // El código siguiente ayuda a proteger frente a ataques XSRF
        var requestCookie = Request.Cookies[AntiXsrfTokenKey];
        Guid requestCookieGuidValue;
        if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
        {
            // Utilizar el token Anti-XSRF de la cookie
            _antiXsrfTokenValue = requestCookie.Value;
            Page.ViewStateUserKey = _antiXsrfTokenValue;
        }
        else
        {
            // Generar un nuevo token Anti-XSRF y guardarlo en la cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
            Page.ViewStateUserKey = _antiXsrfTokenValue;

            var responseCookie = new HttpCookie(AntiXsrfTokenKey)
            {
                HttpOnly = true,
                Value = _antiXsrfTokenValue
            };
            if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
            {
                responseCookie.Secure = true;
            }
            Response.Cookies.Set(responseCookie);
        }

        Page.PreLoad += master_Page_PreLoad;
    }

    void master_Page_PreLoad(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Establecer token Anti-XSRF
            ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
            ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
        }
        else
        {
            // Validar el token Anti-XSRF
            if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
            {
                throw new InvalidOperationException("Error de validación del token Anti-XSRF.");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["Usuario"] == null)
        {
            Response.Redirect("~/Acceso/Login");
        }
    }
    protected void CerrarSession_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Session.Abandon();
        HttpContext.Current.Session.Clear();
        HttpContext.Current.Session.Remove("Usuario");
    }

    //[WebMethod]
    //public static IEnumerable<Notificacion> GetData()
    //{
    //    using (var connection = new SqlConnection(Funciones.GetConfig()))
    //    {
    //        connection.Open();
    //        var sql = @"SELECT UsuarioID, Mensaje, PromocionID, Vigencia, Estatus, SucursalId FROM Notificacion WHERE Tipo = 'VISITA' AND ISNULL(FolioNota,'') = ''";
    //        using (SqlCommand command = new SqlCommand(sql, connection))
    //        {
    //            // Make sure the command object does not already have
    //            // a notification object associated with it.
    //            command.Notification = null;
    //            SqlDependency.Start(Funciones.GetConfig());
    //            SqlDependency dependency = new SqlDependency(command);
    //            dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

    //            if (connection.State == ConnectionState.Closed)
    //                connection.Open();

    //            using (var reader = command.ExecuteReader())
    //                return reader.Cast<IDataRecord>()
    //                    .Select(x => new Notificacion()
    //                    {
    //                        UsuarioID = x.GetInt32(0),
    //                        Mensaje = x.GetString(1),
    //                        PromocionID = x.GetInt32(2),
    //                        Vigencia = x.GetDateTime(3),
    //                        Estatus = x.GetString(4),
    //                        SucursalId = x.GetInt32(5)
    //                    }).ToList();
    //        }
    //    }
    //}

    private static void dependency_OnChange(object sender, SqlNotificationEventArgs e)
    {
        MyHub.Show();
    }
}