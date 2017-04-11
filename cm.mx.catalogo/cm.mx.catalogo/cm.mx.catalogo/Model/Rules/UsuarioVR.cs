﻿using cm.mx.catalogo.Enums;
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
            if (!Funciones.ValidarCorreo(oUsuario.Email)) _mensajes.Add("Ingrese un correo válido");
            if (!Enum.TryParse(oUsuario.Tipo, out tipo)) _mensajes.Add("Ingrese el tipo de usuario");
            if (!oUsuario.Contrasena.Equals(oUsuario.VerificacionContrasena)) _mensajes.Add("La contraseña y la verificación no coincide");
            //if (string.IsNullOrEmpty(oUsuario.Nivel)) _mensajes.Add("Ingrese el nivel del usuario");
            if (rUsuario.ExisteCorreo(oUsuario.Email)) _mensajes.Add("El correo ya ha sido registrado");
            if (string.IsNullOrEmpty(oUsuario.Nombre)) _mensajes.Add("Ingrese el nombre del usuario");
            DateTime ini = DateTime.Now.AddYears(-300);
            DateTime fin = DateTime.Now.AddYears(-10);
            if (oUsuario.FechaNacimiento <= ini || oUsuario.FechaNacimiento >= fin) _mensajes.Add("Ingrese una fecha válida");
            return _mensajes.Count() == 0;
        }
    }
}