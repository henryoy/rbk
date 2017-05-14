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

    private Promocion _oPromocion = new Promocion();
    private Promocion oPromocion
    {
        get
        {
            if (ViewState["oPromocion"] != null)
            {
                _oPromocion = ViewState["oPromocion"] as Promocion;
            }
            return _oPromocion;
        }
        set
        {
            ViewState["oPromocion"] = value;
        }
    }
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

            if (!string.IsNullOrEmpty(oPromocion.ImagenUrl))
            {
                hfTajeta.Value = oPromocion.ImagenUrl;
                imgTarjeta.ImageUrl = oPromocion.ImagenUrl;
            }
            txtCondiciones.Text = oPromocion.TerminosCondiciones;

            ScriptManager.RegisterStartupScript(this,
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

            if (oPromocion.Promocionsucursal != null)
            {
                lsSucusalVM = new List<SucursalVM>();
                foreach (Promocionsucursal opromoSucursal in oPromocion.Promocionsucursal)
                {
                    SucursalVM oSucursalVM = new SucursalVM();
                    if (opromoSucursal.Sucursal != null)
                    {
                        oSucursalVM.Nombre = opromoSucursal.Sucursal.Nombre;
                        oSucursalVM.SucursalID = opromoSucursal.Sucursalid;
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



        Promocion _oPromocion = new Promocion();
        cCatalogo = new CatalogoController();
        _oPromocion.Titulo = txtTitulo.Text;
        _oPromocion.Descripcion = txtDescripcion.Text;
        _oPromocion.Promocionid = thePID;
        _oPromocion.Resumen = txtDescripcion.Text;
        _oPromocion.Tipomembresia = dpTarjeta.SelectedItem.Text;
        _oPromocion.Tipocliente = _oPromocion.Tipomembresia;
        _oPromocion.TerminosCondiciones = txtCondiciones.Text;
        _oPromocion.ImagenUrl = hfTajeta.Value;

        int TarjetaId = Convert.ToInt32(dpTarjeta.SelectedItem.Value);

        Promociondetalle oPromocionDetalle = new Promociondetalle();

        if (string.IsNullOrEmpty(dpTipoPromocion.SelectedItem.Value))
        {

            msj = "La condicion de la promoción esta vacia";

            ScriptManager.RegisterStartupScript(this,
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

            ScriptManager.RegisterStartupScript(this,
                  this.GetType(),
                  "StartupScript",
                  "notification('" + msj + "','error')",
                  true);
        }

        
        if (oPromocion.Promociondetalle.Count > 0)
        {
            var detalle = oPromocion.Promociondetalle.FirstOrDefault();
            if(detalle != null){
                oPromocionDetalle.Promociondetalleid = detalle.Promociondetalleid;                
            }
        }

        oPromocionDetalle.Valor1 = txtValor1.Text;
        oPromocionDetalle.Valor2 = txtValor2.Text;


        _oPromocion.AddDetalle(oPromocionDetalle);

        _oPromocion.AddMembresia(new Promocionmembresia()
        {
            Membresiaid = TarjetaId,
            Promocionid = _oPromocion.Promocionid
        });

        if (!string.IsNullOrEmpty(txtFechaInicio.Text))
            _oPromocion.Vigenciainicial = Convert.ToDateTime(txtFechaInicio.Text);
        if (!string.IsNullOrEmpty(txtFechaFinal.Text))
            _oPromocion.Vigenciafinal = Convert.ToDateTime(txtFechaFinal.Text);

        foreach (SucursalVM oPromoSucursal in lsSucusalVM)
        {
            Promocionsucursal _oPromoSucursal = new Promocionsucursal();
            _oPromoSucursal.Sucursalid = oPromoSucursal.SucursalID;
            _oPromoSucursal.Promocionid = _oPromocion.Promocionid;

            _oPromocion.AddSucursal(_oPromoSucursal);
        }

        Promocion __oPromocion = cCatalogo.GuardarPromocion(_oPromocion);



        if (__oPromocion != null)
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
        ScriptManager.RegisterStartupScript(this,
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
        checkCalendar();
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
                ScriptManager.RegisterStartupScript(this,
                   this.GetType(),
                   "StartupScript",
                   "notification('La sucursal ha sido agregada recientemente','error')",
                   true);
                return;
            }


        }
    }
    protected void dpTipoPromocion_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblValor2.Visible = false;
        txtValor2.Visible = false;
        checkCalendar();
        if (dpTipoPromocion.SelectedItem.Text == "VISITA")
        {
            lblValor1.InnerText = "Número de visita";
            lblValor1.Visible = true;
            txtValor1.Visible = true;
        }
        else
        {
            lblValor1.Visible = false;
            txtValor1.Visible = false;
        }
    }
    private void checkCalendar()
    {
        ScriptManager.RegisterStartupScript(this,
              this.GetType(),
              "StartupCalendar",
              "ActiveCalendar();",
              true);
    }
}