﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cm.mx.catalogo.Controller;
using cm.mx.catalogo.Model;
using System.Collections.Generic;

namespace Promocion
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            CatalogoController cCatalogo = new CatalogoController();
            //cCatalogo.GetPromocion(1);
            List<cm.mx.catalogo.Model.Promocion> lsPromo = new List<cm.mx.catalogo.Model.Promocion>();
            lsPromo = cCatalogo.GetAllPromocionAplicable("11");

        }
        [TestMethod]
        public void TestMethod2()
        {
            CatalogoController cCatalogo = new CatalogoController();
            cm.mx.catalogo.Model.Promocion oPromocion = new cm.mx.catalogo.Model.Promocion();
            cm.mx.catalogo.Model.Promocion _oPromocion = new cm.mx.catalogo.Model.Promocion();
            oPromocion.Promocionid = 0;
            oPromocion.Titulo = "Promocion 2";
            oPromocion.Descripcion = "Promocion de prueba";
            oPromocion.Vigenciainicial = DateTime.Now;
            oPromocion.Vigenciafinal = DateTime.Now;
            oPromocion.Fechaalta = DateTime.Now;
            oPromocion.Usuarioaltaid = 1;
            oPromocion.Fechabaja = Convert.ToDateTime("1900-01-01");
            oPromocion.Usuariobajaid = 0;
            oPromocion.Estado = "ALTA";
            oPromocion.Tipomembresia = "BLANCO";
            oPromocion.Descuento = 10;
            oPromocion.Tipocliente = "BLACO";
            oPromocion.Resumen = "resumen de promocion";

            Promociondetalle oDetalle = new Promociondetalle();

            oDetalle.Condicion = "EVENTO";
            oDetalle.Todos = false;
            oDetalle.Valor1 = "EVENTO";
            oDetalle.Valor2 = "EVENTO2";

            oPromocion.AddDetalle(oDetalle);


            int IdGenerate = 0;
            _oPromocion = cCatalogo.GuardarPromocion(oPromocion);
            if (cCatalogo.Exito)
            {
                IdGenerate = _oPromocion.Promocionid;
            }

        }
        [TestMethod]
        public void TestMethod3()
        {
            CatalogoController cCatalogo = new CatalogoController();
            Usuario oUsuario = new Usuario
            {
                Codigo = "",
                Contrasena = "demo",
                Email = "demo@demo.com",
                Estatus = "ACTIVO",
                FechaAlta = new DateTime(2017, 03, 16, 12, 09, 27),
                FechaBaja = new DateTime(1900, 01, 01),
                FechaNacimiento = new DateTime(2006, 04, 16),
                Imagen = "",
                Nombre = "jorge",
                TarjetaID = 4,
                Tipo = "MOBILE",
                Usuarioid = 2,
                VerificacionContrasena = "demo",
                VisitaActual = 100,
                VisitaGlobal = 501,
                oTargeta = new Tarjeta { TarjetaID = 4 }
            };
            
            oUsuario.Addinteres(new TipoInteres { TipoInteresID = 2 });
            oUsuario.Addinteres(new TipoInteres { TipoInteresID = 3 });
            cCatalogo.GuardarUsuario(oUsuario);
        }
    }
}
