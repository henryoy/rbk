using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Funciones
/// </summary>
public class Funciones
{
    public Funciones()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public static string FormatoMsj(List<string> mensajes)
    {
        string msj = "";
        mensajes.ForEach(x =>
        {
            msj += x.Replace("'", "\"") + "&#160;&#160;";
        });

        //if (!string.IsNullOrEmpty(msj))
        //{
        //    msj = msj.Remove(msj.Length - 3, 3);
        //}
        return msj;
    }

    public static string GetConfig()
    {
        string sConn = "Data source=[SERVER];Initial Catalog=[DATABASE];persist security info=True;user=[USER];password=[PASSWORD];";
        string server = ConfigurationManager.AppSettings["server"];
        string dataBase = ConfigurationManager.AppSettings["dataBase"];
        string usuario = ConfigurationManager.AppSettings["usuario"];
        string password = ConfigurationManager.AppSettings["password"];

        sConn = sConn.Replace("[SERVER]", server);
        sConn = sConn.Replace("[DATABASE]", dataBase);
        sConn = sConn.Replace("[USER]", usuario);
        sConn = sConn.Replace("[PASSWORD]", password);

        return sConn;
    }
}