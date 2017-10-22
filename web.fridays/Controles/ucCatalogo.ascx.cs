using cm.mx.catalogo.Controller;
using cm.mx.catalogo.Helper;
using cm.mx.catalogo.Model;
using cm.mx.dbCore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controles_ucCatalogo : System.Web.UI.UserControl
{
    #region Enum
    public enum EnumCatalogo
    {
        Campana
    }
    #endregion

    #region Variables
    CatalogoController _cCatalogo;
    private CatalogoController cCatalogo
    {
        get
        {
            if (_cCatalogo == null) _cCatalogo = new CatalogoController();
            return _cCatalogo;
        }
    }
    internal List<KeyValuePair<string, object>> _resp
    {
        get
        {
            var temp = ViewState["clinte"];
            return temp == null ? new List<KeyValuePair<string, object>>() : (List<KeyValuePair<string, object>>)temp;
        }
        set
        {
            ViewState["clinte"] = value;
        }
    }

    public List<KeyValuePair<string, object>> Resultado
    {
        get
        {
            return _resp;
        }
    }
    public string ClaveGRID
    {
        get
        {
            var temp = ViewState["ClaveGRID"];
            return temp == null ? "" : (string)temp;
        }
        set
        {
            ViewState["ClaveGRID"] = value;
        }
    }

    public string Empresa
    {
        get
        {
            var temp = ViewState["_Empresa"];
            return temp == null ? "" : (string)temp;
        }
        set
        {
            ViewState["_Empresa"] = value;
        }
    }


    public int ObjetoId
    {
        get
        {
            var temp = ViewState["ojetoid"];
            return temp == null ? 0 : (int)temp;
        }
        set
        {
            ViewState["ojetoid"] = value;
        }
    }
    public object Parametro1
    {
        get
        {
            return ViewState["parametro1"];
        }
        set
        {
            ViewState["parametro1"] = value;
        }
    }
    public object Parametro2
    {
        get
        {
            return ViewState["parametro2"];
        }
        set
        {
            ViewState["parametro2"] = value;
        }
    }
    public object Parametro3
    {
        get
        {
            return ViewState["parametro3"];
        }
        set
        {
            ViewState["parametro3"] = value;
        }
    }
    public object Parametro4
    {
        get
        {
            return ViewState["parametro4"];
        }
        set
        {
            ViewState["parametro4"] = value;
        }
    }

    public object Parametro5
    {
        get
        {
            return ViewState["parametro5"];
        }
        set
        {
            ViewState["parametro5"] = value;
        }
    }

    public object VerificarClienteAsignado
    {
        get
        {
            return ViewState["VerificarClienteAsignado"];
        }
        set
        {
            ViewState["VerificarClienteAsignado"] = value;
        }
    }
   
    public event EventHandler Click;
    private Paginacion oPaginacion;
    private int PageIndex
    {
        get
        {
            int _PageIndex = 0;
            string _to = string.Empty;
            if (ViewState["_PageIndex"] != null)
            {
                _to = (Convert.ToString(ViewState["_PageIndex"]));
                _PageIndex = Convert.ToInt32(_to);
            }
            return _PageIndex;
        }
        set
        {
            ViewState["_PageIndex"] = value;
        }
    }
    private int PageCant
    {
        get
        {
            int _PageCant = CatalogoHelper.GetNumPag();
            string _to = string.Empty;
            if (ViewState["_PageCant"] != null)
            {
                _to = (Convert.ToString(ViewState["_PageCant"]));
                _PageCant = Convert.ToInt32(_to);
            }
            return _PageCant;
        }
        set
        {
            ViewState["_PageCant"] = value;
        }
    }
    private int TotalColumnas
    {
        get
        {
            int _TotalColumnas = 0;
            string _to = string.Empty;
            if (ViewState["TotalColumnas"] != null)
            {
                _to = (Convert.ToString(ViewState["TotalColumnas"]));
                _TotalColumnas = Convert.ToInt32(_to);
            }
            return _TotalColumnas;
        }
        set
        {
            ViewState["TotalColumnas"] = value;
        }
    }

    public List<dynamic> lsEntidad
    {
        get
        {
            var temp = ViewState["lsentidad"];
            return temp == null ? new List<dynamic>() : (List<dynamic>)temp;
        }
        set
        {
            ViewState["lsentidad"] = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lsEntidad = new List<dynamic>();
            //ClientScript.RegisterStartupScript(typeof(ScriptManager), "CallShowDialog", "", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCliente", "", true);
        }
        else
        {

        }
    }

    private void LoadCatalogo()
    {
        grvClientes.Columns.Clear();       
        CargarValores(PageIndex, PageCant);

    }
    private void CargarValores(int Pagina, int Cantidad = 5, FiltroVM oFiltro = null)
    {
        int Total;

        oPaginacion = new Paginacion();
        oPaginacion.Cantidad = Cantidad;
        oPaginacion.Pagina = Pagina;
        oPaginacion.Paginar = true;

        lsEntidad = new List<dynamic>();

        tituloModal.InnerText = "Campañas";
        txtBusqueda.Text = (string)Parametro1;

        if (oFiltro != null)
            lsEntidad.AddRange(cCatalogo.GetAllMrCampana(oFiltro, oPaginacion));
        else
            lsEntidad.AddRange(cCatalogo.GetAllMrCampana(oPaginacion));
        
        Total = oPaginacion.TotalRegistros;
        TotalColumnas = Total;
        grvClientes.DataSource = lsEntidad;
        grvClientes.DataBind();

        //grvClientes.UseAccessibleHeader = true;
        //grvClientes.HeaderRow.TableSection = TableRowSection.TableHeader;
        ucPagination.GenerarPaginas(Pagina, Cantidad, Total);

    }

    public void open()
    {
        txtBusqueda.Text = "";
        ddlBusqueda.Items.Clear();
        this.LoadCatalogo();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCliente", "VerModal($('#" + popupOverlay.ClientID + "'));", true);
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        string TextoBuscar = txtBusqueda.Text;
        if (string.IsNullOrEmpty(TextoBuscar))
        {
            this.Busqueda(null);
            return;
        }

        FiltroVM oFiltro = new FiltroVM();
               
        oFiltro.Texto = TextoBuscar;

        this.Busqueda(oFiltro);
    }
    private void Busqueda(FiltroVM oFiltro)
    {
        this.CargarValores(0, PageCant, oFiltro);
    }
    protected void ucPagination_Click(object sender, EventArgs e)
    {
        PageIndex = ucPagination.Pagina;
        CargarValores(PageIndex, PageCant);
    }

    protected void grvClientes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["title"] = "doble click para seleccionr";
            e.Row.Attributes["style"] += "cursor:pointer;cursor:hand;";
            e.Row.Attributes["ondblclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvClientes, "Select$" + e.Row.RowIndex);
        }
    }

    protected void grvClientes_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        _resp = new List<KeyValuePair<string, object>>();
        GridViewRow r = grvClientes.Rows[e.NewSelectedIndex];
        var _ID = grvClientes.DataKeys[e.NewSelectedIndex].Value;
        _resp.Add(new KeyValuePair<string, object>("ID", _ID));
        int objid;
        int.TryParse(_ID.ToString(), out objid);
        ObjetoId = objid;

        foreach (TableCell celda in grvClientes.HeaderRow.Cells)
        {
            int pos = grvClientes.HeaderRow.Cells.GetCellIndex(celda);
            _resp.Add(new KeyValuePair<string, object>(HttpUtility.HtmlDecode(celda.Text), HttpUtility.HtmlDecode(r.Cells[pos].Text)));
        }

        if (Click != null)
        {
            LinkButton btnAceptar = new LinkButton();
            btnAceptar.CommandArgument = Newtonsoft.Json.JsonConvert.SerializeObject(_resp);
            Click(btnAceptar, new EventArgs());
        }

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCliente", "CerrarModal($('.modalCatalogo')); ", true);
    }

    protected void ddlBusqueda_SelectedIndexChanged(object sender, EventArgs e)
    {
        string TextoBuscar = txtBusqueda.Text;
   
    }
}