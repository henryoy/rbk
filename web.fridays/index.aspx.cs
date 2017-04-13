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

    }

    [WebMethod]
    public static IEnumerable<Notificacion> GetData()
    {
        using (var connection = new SqlConnection(Funciones.GetConfig()))
        {
            connection.Open();
            var sql = @"SELECT UsuarioID, Mensaje, PromocionID, Vigencia, Estatus, SucursalId FROM Notificacion WHERE Tipo = 'VISITA' AND ISNULL(FolioNota,'') = ''";
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
                            UsuarioID = x.GetInt32(0),
                            Mensaje = x.GetString(1),
                            PromocionID = x.GetInt32(2),
                            Vigencia = x.GetDateTime(3),
                            Estatus = x.GetString(4),
                            SucursalId = x.GetInt32(5)
                        }).ToList();
            }
        }
    }
    private static void dependency_OnChange(object sender, SqlNotificationEventArgs e)
    {
        MyHub.Show();
    }

}