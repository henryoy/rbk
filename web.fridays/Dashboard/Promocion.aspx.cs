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
            Carga();
        }
    }
    public void Carga()
    {
        cCatalogo = new CatalogoController();
        List<cm.mx.catalogo.Model.Tarjeta> lsTarjetas = new List<cm.mx.catalogo.Model.Tarjeta>();
        lsTarjetas = cCatalogo.GetAllTarjetas();
        if (lsTarjetas.Count > 0)
        {
            dpTarjeta.Items.Insert(0, new ListItem()
            {
                Value = "0",
                Text="Selecciona tarjeta"
            });

            foreach (Tarjeta oTarjeta in lsTarjetas)
            {
                dpTarjeta.Items.Add(new ListItem()
                {
                    Value = Convert.ToString(oTarjeta.TarjetaID),
                    Text = oTarjeta.Descripcion
                });
            }
        }

    }
    protected void lnkGuardarPromocion_Click(object sender, EventArgs e)
    {
        Promocion oPromocion = new Promocion();
        cCatalogo = new CatalogoController();
        oPromocion.Titulo       = txtTitulo.Text;
        oPromocion.Descripcion  = txtDescripcion.Text;
        oPromocion.Promocionid = 0;
        oPromocion.Resumen = txtDescripcion.Text;
        oPromocion.Tipomembresia = dpTarjeta.SelectedItem.Text;
        oPromocion.Tipocliente = oPromocion.Tipomembresia;

        int TarjetaId = Convert.ToInt32(dpTarjeta.SelectedItem.Value);
        oPromocion.AddMembresia(new Promocionmembresia()
        {
            Membresiaid = TarjetaId,
            Promocionid = oPromocion.Promocionid
            //Tipomembresia = new Tipomembresia()
            
        });

        if (!string.IsNullOrEmpty(txtFechaInicio.Text))
            oPromocion.Vigenciainicial = Convert.ToDateTime(txtFechaInicio.Text);
        if (!string.IsNullOrEmpty(txtFechaFinal.Text)) 
            oPromocion.Vigenciafinal = Convert.ToDateTime(txtFechaFinal.Text);
        
        Promocion _oPromocion = cCatalogo.GuardarPromocion(oPromocion);

        string msj = string.Empty;

        if (_oPromocion != null)
        {
            
        }
        if (cCatalogo.Exito)
        {
            msj = "Se guardo correctamente la promoción";
        }
        else {
            msj = Funciones.FormatoMsj(cCatalogo.Errores);
        }
        /* Mensajes */
        Page.ClientScript.RegisterStartupScript(
                   this.GetType(),
                   "StartupScript",
                   "notification('" + msj + "','error')",
                   true);
    }
}