using cm.mx.catalogo.Enums;
using cm.mx.catalogo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cm.mx.catalogo.Rules
{
    public class UsuarioVR
    {
        UsuarioRepository _rUser;
        UsuarioRepository rUsuario
        {
            get
            {
                if (_rUser == null) _rUser = new UsuarioRepository();
                return _rUser;
            }
        }
        List<string> _mensajes = new List<string>();
        public List<string> Mensajes
        {
            get
            {
                if (_mensajes == null) _mensajes = new List<string>();
                return _mensajes;
            }
        }

        public bool Insertar(Usuario oUsuario)
        {
            TipoUsuario tipo;
            _mensajes.Clear();
            if (string.IsNullOrEmpty(oUsuario.Contrasena)) _mensajes.Add("Ingrese el código");

            if (oUsuario.Origen == "MOBILE")
                if (!Funciones.ValidarCorreo(oUsuario.Email)) _mensajes.Add("Ingrese un correo válido");

            if (!Enum.TryParse(oUsuario.Tipo, out tipo)) _mensajes.Add("Ingrese el tipo de usuario");
            if (!oUsuario.Contrasena.Equals(oUsuario.VerificacionContrasena)) _mensajes.Add("La contraseña y la verificación no coincide");
            //if (string.IsNullOrEmpty(oUsuario.Nivel)) _mensajes.Add("Ingrese el nivel del usuario");
            var origen = rUsuario.ExisteCorreo(oUsuario.Email);
            if (!string.IsNullOrEmpty(origen))
            {
                _mensajes.Add("El correo ya ha sido registrado");
                if (origen != oUsuario.Origen)
                {
                    _mensajes.Add("No puede usar este método de registro");
                    if (origen == Origen.MOBILE.ToString()) _mensajes.Add("Inicie sessión con su correo y contraseña");
                    else _mensajes.Add("inicie sesión con: " + origen);
                }
            }
            if (string.IsNullOrEmpty(oUsuario.Nombre)) _mensajes.Add("Ingrese el nombre del usuario");
            DateTime ini = DateTime.Now.AddYears(-100);
            DateTime fin = DateTime.Now.AddYears(-13);
            if (oUsuario.Origen == Origen.MOBILE.ToString())
            {
                if (oUsuario.FechaNacimiento <= ini || oUsuario.FechaNacimiento >= fin) _mensajes.Add("Ingrese una fecha válida");
            }

            return _mensajes.Count() == 0;
        }


        public bool Actualizar(Usuario oUsuario)
        {
            TipoUsuario tipo;
            _mensajes.Clear();
            if (string.IsNullOrEmpty(oUsuario.Contrasena)) _mensajes.Add("Ingrese el código");
            if (!Funciones.ValidarCorreo(oUsuario.Email)) _mensajes.Add("Ingrese un correo válido");
            if (!Enum.TryParse(oUsuario.Tipo, out tipo)) _mensajes.Add("Ingrese el tipo de usuario");
            if (!oUsuario.Contrasena.Equals(oUsuario.VerificacionContrasena)) _mensajes.Add("La contraseña y la verificación no coincide");
            //if (string.IsNullOrEmpty(oUsuario.Nivel)) _mensajes.Add("Ingrese el nivel del usuario");
            //if (rUsuario.ExisteCorreo(oUsuario.Email, oUsuario.Usuarioid)) _mensajes.Add("El correo ya ha sido registrado");
            if (string.IsNullOrEmpty(oUsuario.Nombre)) _mensajes.Add("Ingrese el nombre del usuario");
            DateTime ini = DateTime.Now.AddYears(-300);
            DateTime fin = DateTime.Now.AddYears(-10);
            if (oUsuario.FechaNacimiento <= ini || oUsuario.FechaNacimiento >= fin) _mensajes.Add("Ingrese una fecha válida");
            return _mensajes.Count() == 0;
        }

        public bool Guardar(Usuario oUser)
        {
            _mensajes = new List<string>();
            DateTime ini = DateTime.Now.AddYears(-100);
            DateTime fin = DateTime.Now.AddYears(-13);
            if (string.IsNullOrEmpty(oUser.Codigo)) _mensajes.Add("Ingrese el código.");
            if (string.IsNullOrEmpty(oUser.Contrasena)) _mensajes.Add("Ingrese la contraseña.");
            else if (!oUser.Contrasena.Equals(oUser.VerificacionContrasena)) _mensajes.Add("Las contraseña y la verifiación no coincide.");
            if (oUser.FechaNacimiento <= ini || oUser.FechaNacimiento >= fin) _mensajes.Add("Ingrese una fecha válida");
            if (string.IsNullOrEmpty(oUser.IdExterno)) _mensajes.Add("Ingrese la clave del usuario");
            if (string.IsNullOrEmpty(oUser.Email)) _mensajes.Add("Ingrse el correo.");
            else if (!Funciones.ValidarCorreo(oUser.Email)) _mensajes.Add("Ingrese un correo válido.");
            if (string.IsNullOrEmpty(oUser.Nombre)) _mensajes.Add("Ingrese el nombre.");
            if (string.IsNullOrEmpty(oUser.Origen)) _mensajes.Add("Ingrese el origen");
            return _mensajes.Count() == 0;
        }
    }
}