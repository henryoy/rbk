using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cm.mx.catalogo.Controller;
using cm.mx.catalogo.Model;
using System.Data;
using System.IO;
using System.Configuration;

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
    List<PromocionDetalleVM> lsDetalle
    {
        get
        {
            var temp = ViewState["Promociondetalle"];
            return temp == null ? null : (List<PromocionDetalleVM>)temp;
        }
        set
        {
            ViewState["Promociondetalle"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lsDetalle = new List<PromocionDetalleVM>();
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
                string _Path = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "" + ConfigurationManager.AppSettings["RutaImagenes"];
                hfTajeta.Value = _Path + oPromocion.ImagenUrl;
                imgTarjeta.ImageUrl = _Path + oPromocion.ImagenUrl;
            }

            txtCondiciones.Text = oPromocion.TerminosCondiciones;

            ScriptManager.RegisterStartupScript(this,
                  this.GetType(),
                  "StartupCalendar",
                  "ActiveCalendar();",
                  true);


            if (oPromocion.Promociondetalle != null)
            {
                Promociondetalle oPromocionDetalle = oPromocion.Promociondetalle.FirstOrDefault(f=>f.Condicion != "IMPORTE");
                List<Promociondetalle> _oPromocionDetalle = oPromocion.Promociondetalle.Where(f => f.Condicion == "IMPORTE").ToList();
                _oPromocionDetalle.ForEach(d =>
                {
                    lsDetalle.Add(new PromocionDetalleVM()
                    {
                        Cambio = d.Cambio,
                        Condicion = d.Condicion,
                        Promociondetalleid = d.Promociondetalleid,
                        Todos = d.Todos,
                        Valor1 = d.Valor1,
                        Valor2 = d.Valor2
                    });
                });

                BindGrid(this.grvDetalle, new List<dynamic>(lsDetalle));

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

                    lsSucusalVM.Add(oSucursalVM);
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
        lnkGuardarPromocion.Enabled = false;
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

        if (dpTarjeta.SelectedItem.Value == "0")
        {
            msj = "Seleccione tipo de membresia.";

            ScriptManager.RegisterStartupScript(this,
                  this.GetType(),
                  "StartupScript",
                  "notification('" + msj + "','error')",
                  true);
            lnkGuardarPromocion.Enabled = true;
            return; 
        }
        _oPromocion.Tipomembresia = dpTarjeta.SelectedItem.Text;
        _oPromocion.Tipocliente = _oPromocion.Tipomembresia;
        _oPromocion.TerminosCondiciones = txtCondiciones.Text;
        string path = string.Empty;
        string fileName = string.Empty;

        if (!string.IsNullOrEmpty(hfTajeta.Value))
        {
            path = hfTajeta.Value;
            fileName = Path.GetFileName(path);
        }

        _oPromocion.ImagenUrl = fileName;

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
            lnkGuardarPromocion.Enabled = true;
            return;
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
            lnkGuardarPromocion.Enabled = true;

            return;
        }

        
        if (oPromocion.Promociondetalle.Count > 0)
        {
            var detalle = oPromocion.Promociondetalle.Where(f=>f.Condicion != "IMPORTE").FirstOrDefault();
            if(detalle != null){
                oPromocionDetalle.Promociondetalleid = detalle.Promociondetalleid;                
            }
        }

        oPromocionDetalle.Valor1 = txtValor1.Text;
        oPromocionDetalle.Valor2 = txtValor2.Text;
        _oPromocion.AddDetalle(oPromocionDetalle);
        foreach (PromocionDetalleVM oPromocionDetalleVM in lsDetalle)
        {
            Promociondetalle _oPromocionDetalle = new Promociondetalle();

            _oPromocionDetalle.Condicion = oPromocionDetalleVM.Condicion;
            _oPromocionDetalle.Valor1 = oPromocionDetalleVM.Valor1;
            _oPromocionDetalle.Valor2 = oPromocionDetalleVM.Valor2;
            _oPromocionDetalle.Promociondetalleid = oPromocionDetalleVM.Promociondetalleid;
            _oPromocionDetalle.Cambio = oPromocionDetalleVM.Cambio;

            _oPromocion.AddDetalle(_oPromocionDetalle);
        }

        _oPromocion.AddMembresia(new Promocionmembresia()
        {
            Membresiaid = TarjetaId,
            Promocionid = _oPromocion.Promocionid
        });

        if (!string.IsNullOrEmpty(txtFechaInicio.Text))
        {
            DateTime _oInicial = new DateTime();
            DateTime.TryParse(txtFechaInicio.Text, out _oInicial);
            _oPromocion.Vigenciainicial = _oInicial;
        }
        if (!string.IsNullOrEmpty(txtFechaFinal.Text))
        {
            DateTime _oFinal = new DateTime();
            DateTime.TryParse(txtFechaFinal.Text, out _oFinal);
            _oPromocion.Vigenciafinal = _oFinal;
        }

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

        lnkGuardarPromocion.Enabled = true;
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
    protected void grvDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].CssClass = "col-empty";
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PromocionDetalleVM obj = (PromocionDetalleVM)e.Row.DataItem;
            if (grvDetalle.EditIndex != e.Row.RowIndex)
            {
                if (grvDetalle.EditIndex > -1) e.Row.CssClass = "deshabilitado";
                else
                {
                    e.Row.Attributes["ondblclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvDetalle, "Edit$" + e.Row.RowIndex);
                }
                //var camp = lsDetalle.FirstOrDefault(a => a.Promociondetalleid == obj.Promociondetalleid);
                //if (camp == null) 
                //    camp = new PromocionDetalleVM();


                //e.Row.Cells[0].Text = camp.Condicion;
                //e.Row.Cells[1].Text = camp.Valor1;
                //e.Row.Cells[2].Text = camp.Valor2;
                //e.Row.Cells[3].Text = camp.Cambio;
            }
            else
            {
                e.Row.CssClass = "row-edit";

                TextBox cbxU = (TextBox)e.Row.Cells[0].Controls[1];
                TextBox cbxO = (TextBox)e.Row.Cells[1].Controls[1];
                TextBox cbxT = (TextBox)e.Row.Cells[2].Controls[1];

                cbxU.Text = obj.Valor1;
                cbxO.Text = obj.Valor2;
                cbxT.Text = obj.Cambio;
            }
        }
    }
    protected void grvDetalle_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grvDetalle.EditIndex = -1;
        BindGrid(grvDetalle, new List<dynamic>(lsDetalle));
    }
    protected void grvDetalle_RowEditing(object sender, GridViewEditEventArgs e)
    {

        grvDetalle.EditIndex = e.NewEditIndex;
        BindGrid(grvDetalle, new List<dynamic>(lsDetalle));
    }
    protected void grvDetalle_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GridView _gridview = (GridView)sender;
            GridViewRow r = _gridview.Rows[e.RowIndex];
            List<string> mensajes = new List<string>();
            //DropDownList TipoPromocion = (DropDownList)r.Cells[0].Controls[1];
            //if (string.IsNullOrEmpty(TipoPromocion.SelectedValue) && e.RowIndex > 0)
            //{
            //    mensajes.Add("Indique el tipo de promoción");
            //}
            //else
            //{
                TextBox txtValor1 = (TextBox)r.Cells[0].Controls[1];
                TextBox txtValor2 = (TextBox)r.Cells[1].Controls[1];
                TextBox txtCambio = (TextBox)r.Cells[2].Controls[1];

                if (string.IsNullOrEmpty(txtValor1.Text.Trim()))
                {
                    mensajes.Add("Ingrese el valor 1");
                }
                else if (string.IsNullOrEmpty(txtValor1.Text.Trim()))
                {
                    mensajes.Add("Ingrese el valor 2");
                }
                else if (string.IsNullOrEmpty(txtCambio.Text.Trim()))
                {
                    mensajes.Add("Ingrese la condicion para el rango del importe");
                }
                else
                {
                    var obj = lsDetalle.ElementAt(e.RowIndex);
                    obj.Condicion = "IMPORTE";
                    obj.Cambio = txtCambio.Text;
                    obj.Valor1 = txtValor1.Text;
                    obj.Valor2 = txtValor2.Text;
                }
            //}

            if (mensajes.Count() > 0)
            {
                Funciones.MostarMensajes("error", mensajes);
            }
            else
            {
                grvDetalle.EditIndex = -1;
                BindGrid(grvDetalle, new List<dynamic>(lsDetalle));
            }
        }
        catch (Exception ex)
        {
            Funciones.MostarMensajes("error", new List<string> { ex.Message });
        }
    }
    protected void btnAddDetalle_Click(object sender, EventArgs e)
    {
        PromocionDetalleVM oPromoDetalle = new PromocionDetalleVM();
        lsDetalle.Add(oPromoDetalle);
        grvDetalle.EditIndex = lsDetalle.Count() - 1;
        //BindGrid(grvDetalle, new List<dynamic>(lsDetalle));
        grvDetalle.DataSource = lsDetalle;
        grvDetalle.DataBind();
        upSucursal.Update();
    }
    protected void BindGrid(GridView grid, List<dynamic> ls)
    {
        grid.DataSource = ls;
        grid.DataBind();
    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            var obj = lsDetalle.ElementAt(row.RowIndex);
            if (obj != null && obj.Promociondetalleid > 0)
            {
                cCatalogo = new CatalogoController();
                bool IsDelete = cCatalogo.EliminarDetallePromocion(oPromocion.Promocionid, obj.Promociondetalleid);
                if (IsDelete)                
                    lsDetalle.RemoveAt(row.RowIndex);
                else
                {
                    string msj = Funciones.FormatoMsj(cCatalogo.Mensajes);
                    ScriptManager.RegisterStartupScript(this,
                 this.GetType(),
                 "StartupScript",
                 "notification('" + msj + "','error')",
                 true);
                }
            }
            else
            {

                lsDetalle.RemoveAt(row.RowIndex);
            }
            BindGrid(grvDetalle, new List<dynamic>(lsDetalle));
        }
        catch (Exception ex)
        {
            Funciones.MostarMensajes("error", new List<string> { ex.Message });
        }
    }

    protected void grvDetalle_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        grvDetalle.EditIndex = e.RowIndex;

        PromocionDetalleVM o = lsDetalle.ElementAt(e.RowIndex);
        lsDetalle.Remove(o);
        BindGrid(grvDetalle, new List<dynamic>(lsDetalle));
    }
}