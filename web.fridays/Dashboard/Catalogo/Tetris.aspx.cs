using cm.mx.catalogo.Controller;
using cm.mx.catalogo.Model;
using cm.mx.dbCore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard_Tetris : System.Web.UI.Page
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
    private Usuario oUsuario
    {
        get
        {
            var temp = ViewState["usuario"];
            return temp == null ? null : (Usuario)temp;
        }
        set
        {
            ViewState["usuario"] = value;
        }
    }
    private string Tipo
    {
        get
        {
            var temp = ViewState["tipo"];
            return temp == null ? "" : (string)temp;
        }
        set
        {
            ViewState["tipo"] = value;
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
        Tipo = "ALL";
        CargarValores(0, 5, "ALL");
    }

    private void CargarValores(int Pagina, int Cantidad = 5, string Tipo = "ALL")
    {
        int Total;
        TipeRegister = Tipo;
        oPaginacion = new Paginacion();
        oPaginacion.Cantidad = Cantidad;
        oPaginacion.Pagina = Pagina;

        List<Imagentetri> lsImagentetris = new List<Imagentetri>();
        cCatalogo = new CatalogoController();
        int Registrados = 0;
        int RegistroHoy = 0;
        int RegistroVip = 0;
        int Vip = 0;

        lsImagentetris = cCatalogo.GetAllImagenTetris();

        Total = oPaginacion.TotalRegistros;
        rptRegistrados.DataSource = lsImagentetris;
        rptRegistrados.DataBind();
        lblRegistros.Text = Convert.ToString(Total);
        TotalColumnas = Total;
        ucPagination.GenerarPaginas(Pagina, Cantidad, Total);
    }

    private void CargarTipoInteres(int IdImagenTetris,int pagina, int Cantidad = 5)
    {
        var imi = cCatalogo.GetImagenTetris(IdImagenTetris);
        hdId.Value = Convert.ToString(imi.ID);
        txtTitulo.Text = imi.Nombre;
        txtDescripcion.Text = imi.Descripcion;
        imgTetris.ImageUrl = imi.Imagen;
        imgTetris.DescriptionUrl = imi.Descripcion;
        txtCodigo_.Text = imi.Codigo;
    }

    protected void ucPagination_Click(object sender, EventArgs e)
    {
        PageIndex = ucPagination.Pagina;
        PageCant = 5;
        CargarValores(PageIndex, PageCant, TipeRegister);
    }

    protected void lnkRegistroHoy_Click(object sender, EventArgs e)
    {
        Tipo = "DAY";
        CargarValores(0, 5, "DAY");
    }

    protected void lnkRegistroAll_Click(object sender, EventArgs e)
    {
        Tipo = "ALL";
        CargarValores(PageIndex, 5, "ALL");
    }

    protected void lnkRegistroVip_Click(object sender, EventArgs e)
    {
        Tipo = "VIP";
        CargarValores(PageIndex, 5, "VIP");
    }

    protected void btnInteres_Click(object sender, EventArgs e)
    {
        try
        {
            cCatalogo = new CatalogoController();
            var email = ((LinkButton)sender).CommandArgument;
            //oUsuario = cCatalogo.GetUsuario(email);
            int idImg = 0;
            int.TryParse(email, out idImg);
            CargarTipoInteres(idImg,0);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "open", "$('#popupOverlay').show();", true);
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            if (oUsuario != null)
            {
                cCatalogo = new CatalogoController();
                if (cCatalogo.GuardarUsuario(oUsuario))
                {
                    CargarValores(PageIndex, 5, Tipo);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "open", "$('#popupOverlay').hide();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(
                     this,
                     this.GetType(),
                     "StartupScript",
                     "notification('" + Funciones.FormatoMsj(cCatalogo.Mensajes) + "','error')",
                     true);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void rptItems_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            RepeaterItem item = e.Item;
            TipoInteres oTipo = (TipoInteres)item.DataItem;
            if (oUsuario == null) oUsuario = new Usuario();
            ((LinkButton)item.Controls[1]).CssClass = (oUsuario.Intereses.Any(a => a.TipoInteresID == oTipo.TipoInteresID)) ? "seleccionar checked" : "seleccionar";
        }
        catch (Exception ex)
        {

        }
    }

    protected void chkInteres_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnTipoI = (LinkButton)sender;
            int TipoID;
            int.TryParse(btnTipoI.CommandArgument, out TipoID);
            if (btnTipoI.CssClass.IndexOf("checked") > -1)
            {
                var oTipo = oUsuario.Intereses.FirstOrDefault(a => a.TipoInteresID == TipoID);
                if (oTipo != null)
                {
                    oUsuario.Intereses.Remove(oTipo);
                }
                btnTipoI.CssClass = "seleccionar";
            }
            else
            {
                oUsuario.Addinteres(new TipoInteres
                {
                    TipoInteresID = TipoID
                });
                btnTipoI.CssClass = "seleccionar checked";
            }
        }
        catch (Exception ex)
        {

        }
    }
}