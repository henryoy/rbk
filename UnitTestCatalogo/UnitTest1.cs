using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cm.mx.catalogo.Controller;

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
    }
}
