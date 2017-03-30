using cm.mx.catalogo.Controller;
using cm.mx.catalogo.Model;
using cm.mx.dbCore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard_Clientes : System.Web.UI.Page
{
    private CatalogoController cCatalogo;
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
            int _PageCant = 10;
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
    private string TipeRegister
    {
        get
        {
            string _to = string.Empty;
            if (ViewState["TipeRegister"] != null)
            {
                _to = (Convert.ToString(ViewState["TipeRegister"]));
            }
            return _to;
        }
        set
        {
            ViewState["TipeRegister"] = value;
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.CargaInicial();
        }
    }
    public void CargaInicial()
    {
        CargarValores(0, 5, "ALL");
    }

    private void CargarValores(int Pagina, int Cantidad = 5, string Tipo = "ALL")
    {
        int Total;
        TipeRegister = Tipo;
        oPaginacion = new Paginacion();
        oPaginacion.Cantidad = Cantidad;
        oPaginacion.Pagina = Pagina;

        List<Usuario> lsUsuarios = new List<Usuario>();
        cCatalogo = new CatalogoController();
        int Registrados = 0;
        int RegistroHoy = 0;
        int RegistroVip = 0;
        int Vip = 0;

        switch (Tipo)
        {
            case "ALL":
                lsUsuarios = cCatalogo.GetAllUsuariosRegistro(oPaginacion);
                RegistroHoy = cCatalogo.GetContRegisteDay();
                RegistroVip = cCatalogo.GetContRegisteVIP();
                lbRegistrosHoy.Text = Convert.ToString(RegistroHoy);
                lblRegistroVip.Text = Convert.ToString(RegistroVip);
                Registrados = lsUsuarios.Count;             

                break;
            case "DAY":
                lsUsuarios = cCatalogo.GetAllUsuariosRegistroHoy(oPaginacion);
                RegistroHoy = cCatalogo.GetContRegisteDay();
                RegistroVip = cCatalogo.GetContRegisteVIP();
                lbRegistrosHoy.Text = Convert.ToString(RegistroHoy);
                lblRegistroVip.Text = Convert.ToString(RegistroVip);
                Registrados = lsUsuarios.Count;           
                break;
            case "BLOCKED":
                break;
            case "VIP":
                lsUsuarios = cCatalogo.GetAllUserRegisterVIP(oPaginacion);
                RegistroHoy = cCatalogo.GetContRegisteDay();
                
                lbRegistrosHoy.Text = Convert.ToString(RegistroHoy);
                lblRegistroVip.Text = Convert.ToString(lsUsuarios.Count);
                Registrados = lsUsuarios.Count; 
                break;
        }



        Total = oPaginacion.TotalRegistros;
        rptRegistrados.DataSource = lsUsuarios;
        rptRegistrados.DataBind();
        lblRegistros.Text = Convert.ToString(Total);
        TotalColumnas = Total;
        ucPagination.GenerarPaginas(Pagina, Cantidad, Total);
    }
    protected void ucPagination_Click(object sender, EventArgs e)
    {
        PageIndex = ucPagination.Pagina;
        PageCant = 5;
        CargarValores(PageIndex, PageCant, TipeRegister);
    }
    protected void lnkRegistroHoy_Click(object sender, EventArgs e)
    {
        CargarValores(0, 5, "DAY");
    }
    protected void lnkRegistroAll_Click(object sender, EventArgs e)
    {
        CargarValores(PageIndex, 5, "ALL");
    }
    protected void lnkRegistroVip_Click(object sender, EventArgs e)
    {
        CargarValores(PageIndex, 5, "VIP");
    }
}