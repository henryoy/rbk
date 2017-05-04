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

        [TestMethod]
        public void GuardarUsuario()
        {
            CatalogoController cCatalogo = new CatalogoController();
            Usuario oUsuario = new Usuario
            {
                Codigo = "",
                Contrasena = "qwert",
                Email = "jorge.demo@demo.com",
                Estatus = "ACTIVO",
                FechaAlta = new DateTime(2017, 03, 22, 03, 19, 09),
                FechaBaja = new DateTime(1900, 01, 01),
                FechaNacimiento = new DateTime(1979, 03, 22),
                Imagen = "",
                Nombre = "Jorge Pech",
                //TarjetaID = 4,
                oTarjeta = new Tipomembresia { Membresiaid = 4 },
                Tipo = "MOBILE",
                Usuarioid = 0,
                VerificacionContrasena = "qwert",
                VisitaActual = 0,
                VisitaGlobal = 0,
                Origen = cm.mx.catalogo.Enums.Origen.FECEBOOK.ToString()
                //oTargeta = new Tarjeta { TarjetaID = 4 }
            };

            oUsuario.Addinteres(new TipoInteres { TipoInteresID = 1 });
            oUsuario.Addinteres(new TipoInteres { TipoInteresID = 2 });
            cCatalogo.RegistrarUsuario(oUsuario);
        }
    }
}
