using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cm.mx.catalogo.Controller;
using cm.mx.catalogo.Model;

public partial class Dashboard_Promocion : System.Web.UI.Page
{
    private CatalogoController cCatalogo;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int thePID = 0;

            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                thePID = Convert.ToInt32(Request.QueryString["id"]);
            }

            Carga(thePID);
        }
    }
    public void Carga(int PromocionId = 0)
    {
        cCatalogo = new CatalogoController();
        List<cm.mx.catalogo.Model.Tipomembresia> lsTipomembresia = new List<cm.mx.catalogo.Model.Tipomembresia>();
        lsTipomembresia = cCatalogo.GetAllMembresias();
        if (lsTipomembresia.Count > 0)
        {
            dpTarjeta.Items.Insert(0, new ListItem()
            {
                Value = "0",
                Text = "Selecciona tarjeta"
            });

            foreach (Tipomembresia oTarjeta in lsTipomembresia)
            {
                dpTarjeta.Items.Add(new ListItem()
                {
                    Value = Convert.ToString(oTarjeta.Membresiaid),
                    Text = oTarjeta.Nombre
                });
            }
        }
        Promocion oPromocion = new Promocion();
        if (PromocionId > 0)
        {
            oPromocion = cCatalogo.GetPromocion(PromocionId);
            if (oPromocion != null)
            {
                dpTarjeta.SelectedIndex = dpTarjeta.Items.IndexOf(dpTarjeta.Items.FindByText(oPromocion.Tipomembresia));
            }
            txtTitulo.Text = oPromocion.Titulo;
            txtDescripcion.Text = oPromocion.Descripcion;
            txtFechaInicio.Text = oPromocion.Vigenciainicial.ToString("yyyy-MM-dd");;
            txtFechaFinal.Text = oPromocion.Vigenciafinal.ToString("yyyy-MM-dd");;

            Page.ClientScript.RegisterStartupScript(
                  this.GetType(),
                  "StartupCalendar",
                  "ActiveCalendar();",
                  true);
            

            if (oPromocion.Promociondetalle != null)
            {
                Promociondetalle oPromocionDetalle =  oPromocion.Promociondetalle.FirstOrDefault();
                if (oPromocionDetalle != null)
                {

                    dpTipoPromocion.SelectedIndex = dpTipoPromocion.Items.IndexOf(dpTipoPromocion.Items.FindByText(oPromocionDetalle.Condicion));
                    txtValor1.Text = oPromocionDetalle.Valor1;
                    txtValor2.Text = oPromocionDetalle.Valor2;
                }
            }
        }
        

    }
    protected void lnkGuardarPromocion_Click(object sender, EventArgs e)
    {
        string Tipo = string.Empty;
        string msj = string.Empty;

        int thePID = 0;

        if (!String.IsNullOrEmpty(Request.QueryString["id"]))
        {
            thePID = Convert.ToInt32(Request.QueryString["id"]);
        }



        Promocion oPromocion = new Promocion();
        cCatalogo = new CatalogoController();
        oPromocion.Titulo       = txtTitulo.Text;
        oPromocion.Descripcion  = txtDescripcion.Text;
        oPromocion.Promocionid = thePID;
        oPromocion.Resumen = txtDescripcion.Text;
        oPromocion.Tipomembresia = dpTarjeta.SelectedItem.Text;
        oPromocion.Tipocliente = oPromocion.Tipomembresia;

        int TarjetaId = Convert.ToInt32(dpTarjeta.SelectedItem.Value);

        Promociondetalle oPromocionDetalle = new Promociondetalle();

        if (string.IsNullOrEmpty(dpTipoPromocion.SelectedItem.Value)) {
            
            msj = "La condicion de la promoción esta vacia";

            Page.ClientScript.RegisterStartupScript(
                  this.GetType(),
                  "StartupScript",
                  "notification('" + msj + "','error')",
                  true);
        }

        oPromocionDetalle.Condicion = dpTipoPromocion.SelectedItem.Value;
        oPromocionDetalle.Todos = true;

        if (dpTipoPromocion.SelectedItem.Value == "VISITA"  && string.IsNullOrEmpty(txtValor1.Text))
        {

            msj = "El valor 1 no puede ser vacío";

            Page.ClientScript.RegisterStartupScript(
                  this.GetType(),
                  "StartupScript",
                  "notification('" + msj + "','error')",
                  true);
        }

        oPromocionDetalle.Valor1 = txtValor1.Text;
        oPromocionDetalle.Valor2 = txtValor2.Text;


        oPromocion.AddDetalle(oPromocionDetalle);

        oPromocion.AddMembresia(new Promocionmembresia()
        {
            Membresiaid = TarjetaId,
            Promocionid = oPromocion.Promocionid            
        });

        if (!string.IsNullOrEmpty(txtFechaInicio.Text))
            oPromocion.Vigenciainicial = Convert.ToDateTime(txtFechaInicio.Text);
        if (!string.IsNullOrEmpty(txtFechaFinal.Text)) 
            oPromocion.Vigenciafinal = Convert.ToDateTime(txtFechaFinal.Text);
        
        Promocion _oPromocion = cCatalogo.GuardarPromocion(oPromocion);

       

        if (_oPromocion != null)
        {
            Tipo = "success";
        }
        if (cCatalogo.Exito)
        {
            Tipo = "success";
            msj = "Se guardo correctamente la promoción";
        }
        else {
            Tipo = "error";
            msj = Funciones.FormatoMsj(cCatalogo.Errores);
            if (cCatalogo.Errores.Count == 0)
            {
                msj = Funciones.FormatoMsj(cCatalogo.Mensajes);
            }
        }
        /* Mensajes */
        Page.ClientScript.RegisterStartupScript(
                   this.GetType(),
                   "StartupScript",
                   "notification('" + msj + "','" + Tipo + "')",
                   true);
    }
}