using System;
using System.Collections.Generic;
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
            msj += x + "&#160;&#160;";
        });

        //if (!string.IsNullOrEmpty(msj))
        //{
        //    msj = msj.Remove(msj.Length - 3, 3);
        //}
        return msj;
    }
}