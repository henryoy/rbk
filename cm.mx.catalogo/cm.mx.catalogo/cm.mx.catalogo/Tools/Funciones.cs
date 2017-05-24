using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace cm.mx.catalogo
{
    public class Funciones
    {
        private static string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                return null;
            }
            return match.Groups[1].Value + domainName;
        }

        public static bool ValidarCorreo(string correo)
        {
            bool valido = false;
            try
            {
                if (!string.IsNullOrEmpty(correo))
                {
                    correo = Regex.Replace(correo, @"(@)(.+)$", DomainMapper, RegexOptions.None);
                    valido = Regex.IsMatch(correo, @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                             @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$", RegexOptions.IgnoreCase);
                }
            }
            catch (ArgumentException)
            {
                valido = false;
            }
            return valido;
        }

        public static object FormatoMensajes(List<string> mensajes)
        {
            string mensaje = "";
            foreach (var m in mensajes)
            {
                mensaje += m + "<br/>";
            }
            if (!string.IsNullOrEmpty(mensaje))
            {
                mensaje = mensaje.Remove(mensaje.Length - 5, 5);
            }

            return mensaje;
        }

        public static String GetRandomString()
        {
            var allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            var length = 15;

            var chars = new char[length];
            var rd = new Random();

            for (var i = 0; i < length; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new String(chars);
        }
    }
}