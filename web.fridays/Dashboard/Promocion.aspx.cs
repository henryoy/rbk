using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cm.mx.catalogo.Controller;
using cm.mx.catalogo.Model;
using System.Data;

public partial class Dashboard_Promocion : System.Web.UI.Page
{
    private List<SucursalVM> _lsSucursal = new List<SucursalVM>();
    private List<SucursalVM> lsSucusalVM
    {
        get
        {

            if (ViewState["Sucursal"] != null)
            {
                _lsSucursal = ViewState["Sucursal"] as List<SucursalVM>;
            }
            return _lsSucursal;
        }
        set
        {
            ViewState["Sucursal"] = value;
        }
    }
    private CatalogoController cCatalogo;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int thePID = 0;
            lsSucusalVM = new List<SucursalVM>();

            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                thePID = Convert.ToInt32(Request.QueryString["id"]);
            }

            if (ViewState["DBSucursal"] == null)
            {
                DataTable DBSucursal = new DataTable();
                DBSucursal.Columns.AddRange(new DataColumn[2] { 
                                    new DataColumn("SucursalID", typeof(int)),
                                    new DataColumn("Nombre", typeof(string)) 
                });
                //DBSucursal.Columns["SucursalID"].AutoIncrement = true;
                //DBSucursal.Columns["SucursalID"].AutoIncrementSeed = 1;
                //DBSucursal.Columns["SucursalID"].AutoIncrementStep = 1;

                ViewState["DBSucursal"] = DBSucursal;
            }

            Carga(thePID);
        }
    }
    public void Carga(int PromocionId = 0)
    {
        cCatalogo = new CatalogoController();
        List<cm.mx.catalogo.Model.Tipomembresia> lsTipomembresia = new List<cm.mx.catalogo.Model.Tipomembresia>();
        List<Sucursal> lsSucursal = new List<Sucursal>();
        lsSucursal = cCatalogo.GetAllSucursales();
        dpSucursales.Items.Insert(0, new ListItem()
        {
            Value = "0",
            Text = "TODAS"
        });

        if (lsSucursal.Count > 0)
        {

            foreach (Sucursal oSucursal in lsSucursal)
            {
                ListItem oItem = new ListItem()
                {
                    Value = Convert.ToString(oSucursal.SucursalID),
                    Text = oSucursal.Nombre,

                };

                oItem.Attributes.Add("data-list-id", oSucursal.Nombre);

                dpSucursales.Items.Add(oItem);
            }
        }

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
            if (oPromocion.Vigenciainicial.HasValue)
                txtFechaInicio.Text = String.Format("{0:yyyy-MM-dd}", oPromocion.Vigenciainicial);//oPromocion.Vigenciainicial.ToString("yyyy-MM-dd");
            if (oPromocion.Vigenciafinal.HasValue)
                txtFechaFinal.Text = String.Format("{0:yyyy-MM-dd}", oPromocion.Vigenciafinal);//oPromocion.Vigenciafinal.ToString("yyyy-MM-dd"); ;

            Page.ClientScript.RegisterStartupScript(
                  this.GetType(),
                  "StartupCalendar",
                  "ActiveCalendar();",
                  true);


            if (oPromocion.Promociondetalle != null)
            {
                Promociondetalle oPromocionDetalle = oPromocion.Promociondetalle.FirstOrDefault();
                if (oPromocionDetalle != null)
                {

                    dpTipoPromocion.SelectedIndex = dpTipoPromocion.Items.IndexOf(dpTipoPromocion.Items.FindByText(oPromocionDetalle.Condicion));
                    txtValor1.Text = oPromocionDetalle.Valor1;
                    txtValor2.Text = oPromocionDetalle.Valor2;
                }
            }
        }

        if (dpTipoPromocion.SelectedItem == null)
        {
            lblValor1.InnerText = "Número de visita";
        }
        else if (dpTipoPromocion.SelectedItem.Text == "VISITA")
        {
            lblValor1.InnerText = "Número de visita";
        }

        lblValor2.Visible = false;
        txtValor2.Visible = false;
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
        oPromocion.Titulo = txtTitulo.Text;
        oPromocion.Descripcion = txtDescripcion.Text;
        oPromocion.Promocionid = thePID;
        oPromocion.Resumen = txtDescripcion.Text;
        oPromocion.Tipomembresia = dpTarjeta.SelectedItem.Text;
        oPromocion.Tipocliente = oPromocion.Tipomembresia;
        //oPromocion.TerminosCondiciones = txtCondiciones.Text;
        //oPromocion.ImagenUrl = hfTajeta.Value;

        int TarjetaId = Convert.ToInt32(dpTarjeta.SelectedItem.Value);

        Promociondetalle oPromocionDetalle = new Promociondetalle();

        if (string.IsNullOrEmpty(dpTipoPromocion.SelectedItem.Value))
        {

            msj = "La condicion de la promoción esta vacia";

            Page.ClientScript.RegisterStartupScript(
                  this.GetType(),
                  "StartupScript",
                  "notification('" + msj + "','error')",
                  true);
        }

        oPromocionDetalle.Condicion = dpTipoPromocion.SelectedItem.Value;
        oPromocionDetalle.Todos = true;

        if (dpTipoPromocion.SelectedItem.Value == "VISITA" && string.IsNullOrEmpty(txtValor1.Text))
        {

            msj = "El número de visitas no puede ser vacío";

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

        foreach (SucursalVM oPromoSucursal in lsSucusalVM)
        {
            Promocionsucursal _oPromoSucursal = new Promocionsucursal();
            _oPromoSucursal.Sucursalid = oPromoSucursal.SucursalID;
            _oPromoSucursal.Promocionid = oPromocion.Promocionid;

            oPromocion.AddSucursal(_oPromoSucursal);
        }

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
        else
        {
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
    protected void btnEliminarSucursal_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in lBSucursal.Items)
        {
            if (item.Selected)
            {
                var Sucursal = lsSucusalVM.FirstOrDefault(f => f.SucursalID == Convert.ToInt32(item.Value));
                lsSucusalVM.Remove(Sucursal);
            }
        }

        DataTable DBSucursal = (DataTable)ViewState["DBSucursal"];
        DBSucursal.Rows.Clear();

        foreach (SucursalVM oSucursal in lsSucusalVM)
        {
            DataRow row = DBSucursal.NewRow();
            row["SucursalID"] = oSucursal.SucursalID;
            row["Nombre"] = oSucursal.Nombre;
            DBSucursal.Rows.Add(row);
        }

        ViewState["DBSucursal"] = DBSucursal;

        lBSucursal.DataSource = DBSucursal;
        lBSucursal.DataTextField = "Nombre";
        lBSucursal.DataValueField = "SucursalID";
        lBSucursal.DataBind();
    }
    protected void dpSucursales_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dpSucursales.SelectedItem.Value != "0")
        {
            if (!lsSucusalVM.Any(f => f.SucursalID == Convert.ToInt32(dpSucursales.SelectedItem.Value)))
            {
                lsSucusalVM.Add(new SucursalVM()
                {
                    Nombre = dpSucursales.SelectedItem.Text,
                    SucursalID = Convert.ToInt32(dpSucursales.SelectedItem.Value)
                });

                DataTable DBSucursal = (DataTable)ViewState["DBSucursal"];
                DBSucursal.Rows.Clear();

                foreach (SucursalVM oSucursal in lsSucusalVM)
                {
                    DataRow row = DBSucursal.NewRow();
                    row["SucursalID"] = oSucursal.SucursalID;
                    row["Nombre"] = oSucursal.Nombre;
                    DBSucursal.Rows.Add(row);
                }

                ViewState["DBSucursal"] = DBSucursal;

                lBSucursal.DataSource = DBSucursal;
                lBSucursal.DataTextField = "Nombre";
                lBSucursal.DataValueField = "SucursalID";
                lBSucursal.DataBind();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(
                   this.GetType(),
                   "StartupScript",
                   "notification('La sucursal ha sido agregada recientemente','error')",
                   true);
                return;
            }


        }
    }
}