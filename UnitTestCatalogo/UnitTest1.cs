using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cm.mx.catalogo.Controller;
using cm.mx.catalogo.Model;

namespace UnitTestCatalogo
{
    [TestClass]
    public class UnitTest1
    {
        
        [TestMethod]
        public void TestActivar()
        {
            CatalogoController cCatalogo = new CatalogoController();
            string Usuario = "hency.oy@gmail.com";
            int ClienteID = 2;
            string Referencia = "TESTING";
            int SucursalId = 0;
            bool isExito = false;
            isExito = cCatalogo.RegistroVisita(Usuario, ClienteID, Referencia, SucursalId);
            if (isExito)
            {

            }
        }
        [TestMethod]
        public void TestActivar1() {
            CatalogoController cCatalogo = new CatalogoController();
            string Usuario = "hency.oy@gmail.com";
            int ClienteID = 2;
            string Referencia = "TESTING";
            int SucursalId = 0;
            bool isExito = false;
            isExito = cCatalogo.RegistroVisita(Usuario, ClienteID, Referencia, SucursalId);
            if (isExito)
            {

            }
        }
        [TestMethod]
        public void TestRedimir()
        {
            CatalogoController cCatalogo = new CatalogoController();
            RedimirPromocionVM oRedimirPromo = new RedimirPromocionVM();
            oRedimirPromo.PromocionRedimirId = 0;
            oRedimirPromo.UsuarioId = 2;
            oRedimirPromo.UsuarioRedimioId = 1;
            oRedimirPromo.SucursalId = 0;
            oRedimirPromo.PromocionId = 22;
            
            bool isExito = false;
            //string Usuario = "hency.oy@gmail.com";
            //int ClienteID = 2;
            //string Referencia = "TESTING";
            //int SucursalId = 0;

            isExito = cCatalogo.RedimirPromocion(oRedimirPromo);
            
            //isExito = cCatalogo.RegistroVisita(Usuario, ClienteID, Referencia, SucursalId);
            if (isExito)
            {

            }
        }
    }
}
