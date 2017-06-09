using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cm.mx.catalogo.Controller;
using cm.mx.catalogo.Model;
using System.Text;

public partial class Dashboard_Distribucion : System.Web.UI.Page
{
    private CatalogoController cCatalogo;
    List<CamposDistribucion> lsCampos
    {
        get
        {
            var temp = ViewState["campos"];
            return temp == null ? new List<CamposDistribucion>() : (List<CamposDistribucion>)temp;
        }
        set
        {
            ViewState["campos"] = value;
        }
    }
    Distribucion oDistribucion
    {
        get
        {
            var temp = ViewState["distribucion"];
            return temp == null ? null : (Distribucion)temp;
        }
        set
        {
            ViewState["distribucion"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int id;
            int.TryParse(Request.QueryString["id"], out id);
            lsCampos = new List<CamposDistribucion>();
            cCatalogo = new CatalogoController();
            lsCampos = cCatalogo.GetAllCamposDistrubucion();
            GetDistribucion(id);
        }
    }

    protected void Page_LoadComplete()
    {
        if (grvCampos.Columns.Count > 0)
        {
            grvCampos.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        if (grvCondicion.Columns.Count > 0)
        {
            grvCondicion.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        if (grvCondicion.EditIndex > -1)
        {
            btnGuardar.CssClass = "add-option semi_bold deshabilitado";
            btnAddCondicion.CssClass = "add-option semi_bold deshabilitado";
        }
        else
        {
            btnGuardar.CssClass = "add-option semi_bold";
            btnAddCondicion.CssClass = "add-option semi_bold";
        }
    }

    protected void BindGrid(GridView grid, List<dynamic> ls)
    {
        grid.DataSource = ls;
        grid.DataBind();
    }

    protected void GetDistribucion(int id)
    {
        try
        {
            cCatalogo = new CatalogoController();
            oDistribucion = cCatalogo.GetDistribucion(id);
            if (oDistribucion == null) oDistribucion = new Distribucion();
            txtDescripcion.Text = oDistribucion.Descripcion;
            txtNombre.Text = oDistribucion.Nombre;
            BindGrid(grvCampos, new List<dynamic>(lsCampos));
            BindGrid(grvCondicion, new List<dynamic>(oDistribucion.Condiciones));
        }
        catch (Exception ex)
        {
            Funciones.MostarMensajes("Error", new List<string> { ex.Message });
        }
    }

    protected string GetCampos()
    {
        string campos = "";
        foreach (GridViewRow r in grvCampos.Rows)
        {
            CheckBox chk = (CheckBox)r.Cells[0].Controls[1];
            if (chk.Checked)
            {
                campos += grvCampos.DataKeys[r.RowIndex].Value.ToString() + ",";
            }
        }
        if (!string.IsNullOrEmpty(campos)) campos = campos.Remove(campos.Length - 1, 1);
        return campos;
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow fila in grvCampos.Rows)
            {
                ((CheckBox)fila.Cells[0].Controls[1]).Checked = ((CheckBox)sender).Checked;
            }
        }
        catch (Exception ex)
        {
            Funciones.MostarMensajes("error", new List<string> { ex.Message });
        }
    }

    protected void btnAddCondicion_Click(object sender, EventArgs e)
    {
        oDistribucion.Add(new CondicionDistribucion { });
        grvCondicion.EditIndex = oDistribucion.Condiciones.Count() - 1;
        BindGrid(grvCondicion, new List<dynamic>(oDistribucion.Condiciones));
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            oDistribucion.Condiciones.RemoveAt(row.RowIndex);
            BindGrid(grvCondicion, new List<dynamic>(oDistribucion.Condiciones));
        }
        catch (Exception ex)
        {
            Funciones.MostarMensajes("error", new List<string> { ex.Message });
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
           List<string> mensajes = new List<string>();
            StringBuilder sb = new StringBuilder();

            if (mensajes.Count() > 0)
            {
                Funciones.MostarMensajes("error", mensajes);
            }
            else
            {
                bool isContinue = true;
                if (oDistribucion == null) oDistribucion = new Distribucion();
                foreach (var item in oDistribucion.Condiciones)
                {
                    if (string.IsNullOrEmpty(item.Campo))
                    {
                        Funciones.MostarMensajes("error", new List<string> { "Existe una condicion vacia."});
                        isContinue = false;
                        break;
                    }
                }

                if (!isContinue)
                    return;

                oDistribucion.Campos = GetCampos();
                oDistribucion.Descripcion = txtDescripcion.Text.Trim();
                oDistribucion.Nombre = txtNombre.Text.Trim();
                cCatalogo = new CatalogoController();
                if (!cCatalogo.GuardarDistribucion(oDistribucion))
                {
                    mensajes = cCatalogo.Mensajes;
                    if (cCatalogo.Errores.Count() > 0) mensajes.AddRange(cCatalogo.Errores);
                    Funciones.MostarMensajes("error", cCatalogo.Mensajes);
                    btnGuardar.CssClass = "add-option semi_bold";
                }
                else
                {
                    GetDistribucion(oDistribucion.DistribucionID);
                    Funciones.MostarMensajes("success", new List<string> { "La operación se completo correctamente" });
                }
            }
        }
        catch (Exception ex)
        {
            Funciones.MostarMensajes("error", new List<string> { ex.Message });
        }
    }

    protected void grvCondicion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
            e.Row.Cells[0].CssClass = "col-empty";
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CondicionDistribucion obj = (CondicionDistribucion)e.Row.DataItem;
            if (grvCondicion.EditIndex != e.Row.RowIndex)
            {
                if (grvCondicion.EditIndex > -1) e.Row.CssClass = "deshabilitado";
                else
                {
                    e.Row.Attributes["ondblclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvCondicion, "Edit$" + e.Row.RowIndex);
                }
                var camp = lsCampos.FirstOrDefault(a => a.Campo == obj.Campo);
                if (camp == null) camp = new CamposDistribucion();
                e.Row.Cells[1].Text = camp.Nombre;
            }
            else
            {
                e.Row.CssClass = "row-edit";
                if (e.Row.RowIndex == 0)
                {
                    e.Row.Cells[0].Controls[1].Visible = false;
                }
                DropDownList cbxCampos = (DropDownList)e.Row.Cells[1].Controls[1];
                cbxCampos.DataSource = lsCampos;
                cbxCampos.DataTextField = "Nombre";
                cbxCampos.DataValueField = "Campo";
                cbxCampos.DataBind();
                DropDownList cbxU = (DropDownList)e.Row.Cells[0].Controls[1];
                DropDownList cbxO = (DropDownList)e.Row.Cells[2].Controls[1];
                DropDownList cbxT = (DropDownList)e.Row.Cells[3].Controls[1];

                cbxCampos.SelectedIndex = cbxCampos.Items.IndexOf(cbxCampos.Items.FindByValue(obj.Campo));
                cbxU.SelectedIndex = cbxU.Items.IndexOf(cbxU.Items.FindByValue(obj.Nexo));
                cbxO.SelectedIndex = cbxO.Items.IndexOf(cbxO.Items.FindByValue(obj.Operador));
                cbxT.SelectedIndex = cbxT.Items.IndexOf(cbxT.Items.FindByValue(obj.Tipo));
            }
        }
    }

    protected void grvCondicion_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grvCondicion.EditIndex = -1;
        BindGrid(grvCondicion, new List<dynamic>(oDistribucion.Condiciones));
    }

    protected void grvCondicion_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grvCondicion.EditIndex = e.NewEditIndex;
        BindGrid(grvCondicion, new List<dynamic>(oDistribucion.Condiciones));
    }

    protected void grvCondicion_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GridView _gridview = (GridView)sender;
            GridViewRow r = _gridview.Rows[e.RowIndex];
            List<string> mensajes = new List<string>();
            DropDownList cbxU = (DropDownList)r.Cells[0].Controls[1];
            if (string.IsNullOrEmpty(cbxU.SelectedValue) && e.RowIndex > 0)
            {
                mensajes.Add("Indique el tipo de union Y/O");
            }
            else
            {
                DropDownList cbxC = (DropDownList)r.Cells[1].Controls[1];
                DropDownList cbxO = (DropDownList)r.Cells[2].Controls[1];
                DropDownList cbxT = (DropDownList)r.Cells[3].Controls[1];
                TextBox txtValor = (TextBox)r.Cells[4].Controls[1];
                if (string.IsNullOrEmpty(txtValor.Text.Trim()))
                {
                    mensajes.Add("Ingrese el valor");
                }
                else
                {
                    var obj = oDistribucion.Condiciones.ElementAt(e.RowIndex);
                    obj.Campo = cbxC.SelectedItem.Value;
                    obj.Operador = cbxO.SelectedItem.Value;
                    obj.Nexo = (e.RowIndex > 0) ? cbxU.SelectedItem.Value : "";
                    obj.Valor = txtValor.Text.Trim();
                    obj.Tipo = cbxT.SelectedItem.Value;
                }
            }

            if (mensajes.Count() > 0)
            {
                Funciones.MostarMensajes("error", mensajes);
            }
            else
            {
                grvCondicion.EditIndex = -1;
                BindGrid(grvCondicion, new List<dynamic>(oDistribucion.Condiciones));
            }
        }
        catch (Exception ex)
        {
            Funciones.MostarMensajes("error", new List<string> { ex.Message });
        }
    }

    protected void grvCampos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
            e.Row.CssClass = "col-empty";
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CamposDistribucion campo = e.Row.DataItem as CamposDistribucion;
            if (!string.IsNullOrEmpty(oDistribucion.Campos))
            {
                CheckBox chk = e.Row.Cells[0].Controls[1] as CheckBox;
                chk.Checked = oDistribucion.Campos.IndexOf(campo.Campo) > -1;
            }
        }
    }

    protected void btnResultado_Click(object sender, EventArgs e)
    {
        try
        {
            UtileriaController cUtileria = new UtileriaController();
            oDistribucion.Campos = GetCampos();
            if (!cUtileria.ProbarDistribucion(oDistribucion, grvResultado))
            {
                Funciones.MostarMensajes("error", cUtileria.Errores);
            }
            else
            {
                cUtileria.ProbarDistribucion(oDistribucion, grvResultado);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "open", "$('#popupOverlay').show();", true);
            }
        }
        catch (Exception ex)
        {
            Funciones.MostarMensajes("error", new List<string> { ex.Message });
        }
    }
}
