using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cm.mx.catalogo.Controller;
using cm.mx.catalogo.Model;
using cm.mx.dbCore.Tools;
public partial class Dashboard_Promociones : System.Web.UI.Page
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
    private void CargaInicial()
    {
        
        CargarValores(0, 5);
    }

    private void CargarValores(int Pagina, int Cantidad = 5)
    {
        int Total;
        oPaginacion = new Paginacion();
        oPaginacion.Cantidad = Cantidad;
        oPaginacion.Pagina = Pagina;

        List<Promocion> lsPromocion = new List<Promocion>();
        cCatalogo = new CatalogoController();
        lsPromocion = cCatalogo.GetAllPromocion(oPaginacion);
        rptPromociones.DataSource = lsPromocion;
        rptPromociones.DataBind();


        Total = oPaginacion.TotalRegistros;
        TotalColumnas = Total;
        ucPagination.GenerarPaginas(Pagina, Cantidad, Total);
    }
    protected void lnkEliminar_Click(object sender, EventArgs e)
    {
        //String CustomerID = e.CommandArgument.ToString();
        LinkButton source = (LinkButton)sender;
        String CustomerID = source.CommandArgument.ToString();
        

    }

    protected void ucPagination_Click(object sender, EventArgs e)
    {
        PageIndex = ucPagination.Pagina;
        PageCant = 5;
        CargarValores(PageIndex, PageCant);
    }
    protected void lnkVer_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)(sender);
        string PromocionId = btn.CommandArgument;                
        if (!string.IsNullOrEmpty(PromocionId))
        {
            cCatalogo = new CatalogoController();
            Promocion oPromocion = cCatalogo.GetPromocion(Convert.ToInt32(PromocionId));
            if(oPromocion != null)
            {
                if(oPromocion.PromociondetalleTetris != null && oPromocion.PromociondetalleTetris.Count > 0)
                    Response.Redirect("~/Dashboard/Promociontetris?id=" + PromocionId);
                else Response.Redirect("~/Dashboard/Promocion?id=" + PromocionId);
            }
            
        }
    }
}