using System;
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
            cCatalogo.GetPromocion(1);

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
    }
}
