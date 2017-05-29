using cm.mx.catalogo.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write(new List<string> { "Hola", "Mundo" });
    }

    [WebMethod(EnableSession = true)]
    public static IEnumerable<Notificacion> GetData()
    {
        int SucursalID = (int)HttpContext.Current.Session["Sucursal"];
        using (var connection = new SqlConnection(Funciones.GetConfig()))
        {
            connection.Open();
            //var sql = @"SELECT UsuarioID, Mensaje, PromocionID, Vigencia, Estatus, SucursalId FROM Notificacion WHERE Tipo = 'VISITA' AND ISNULL(FolioNota,'') = ''";
            var sql = @"SELECT b.Nombre Usuario, a.Referencia, a.FechaRegistro, a.NotificacionID FROM Notificacion a INNER JOIN Usuario b ON a.UsuarioID = b.UsuarioId WHERE a.Tipo = 'VISITA' AND ISNULL(FolioNota,'') = '' AND SucursalId = " + SucursalID;
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                // Make sure the command object does not already have
                // a notification object associated with it.
                command.Notification = null;
                SqlDependency.Start(Funciones.GetConfig());
                SqlDependency dependency = new SqlDependency(command);
                dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using (var reader = command.ExecuteReader())
                    return reader.Cast<IDataRecord>()
                        .Select(x => new Notificacion()
                        {
                            Usuario = new Usuario { Nombre = x.GetString(0) },
                            Referencia = x.GetString(1),
                            FechaRegistro = x.GetDateTime(2),
                            NotificacionID = x.GetInt32(3)
                        }).ToList();
            }
        }
    }
    private static void dependency_OnChange(object sender, SqlNotificationEventArgs e)
    {
        MyHub.Show();
    }

}