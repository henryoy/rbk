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
    List<Condicion> lsCondiciones
    {
        get
        {
            var temp = ViewState["condiciones"];
            return temp == null ? new List<Condicion>() : (List<Condicion>)temp;
        }
        set
        {
            ViewState["condiciones"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lsCondiciones = new List<Condicion>();
            lsCampos = new List<CamposDistribucion>();
            cCatalogo = new CatalogoController();
            lsCampos = cCatalogo.GetAllCamposDistrubucion();
            Cargainicial();
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
    }

    protected void Cargainicial()
    {
        txtDescripcion.Text = string.Empty;
        txtNombre.Text = string.Empty;
        BindGrid(grvCampos, new List<dynamic>(lsCampos));
        BindGrid(grvCondicion, new List<dynamic>(lsCondiciones));
    }

    protected void BindGrid(GridView grid, List<dynamic> ls)
    {
        grid.DataSource = ls;
        grid.DataBind();
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

    protected void grvCondicion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (grvCondicion.EditIndex == -1)
            {
                btnGuardar.CssClass = "add-option semi_bold";
                e.Row.Attributes["ondblclick"] = Page.ClientScript.GetPostBackClientHyperlink(grvCondicion, "Edit$" + e.Row.RowIndex);
            }
            else
            {
                btnGuardar.CssClass = "add-option semi_bold deshabilitado";
                if (grvCondicion.EditIndex != e.Row.RowIndex)
                {
                    e.Row.CssClass = "deshabilitado";
                }
                else
                {
                    DropDownList cbxCampos = (DropDownList)e.Row.Cells[1].Controls[1];
                    cbxCampos.DataSource = lsCampos;
                    cbxCampos.DataTextField = "Nombre";
                    cbxCampos.DataValueField = "Campo";
                    cbxCampos.DataBind();
                    DropDownList cbxU = (DropDownList)e.Row.Cells[0].Controls[1];
                    DropDownList cbxO = (DropDownList)e.Row.Cells[2].Controls[1];

                    Condicion obj = (Condicion)e.Row.DataItem;
                    cbxCampos.SelectedIndex = cbxCampos.Items.IndexOf(cbxCampos.Items.FindByValue(obj.Campo));
                    cbxU.SelectedIndex = cbxU.Items.IndexOf(cbxU.Items.FindByValue(obj.Union));
                    cbxO.SelectedIndex = cbxO.Items.IndexOf(cbxO.Items.FindByValue(obj.Operador));
                }
            }
        }
    }

    protected void btnAddCondicion_Click(object sender, EventArgs e)
    {
        lsCondiciones.Add(new Condicion { });
        grvCondicion.EditIndex = lsCondiciones.Count() - 1;
        BindGrid(grvCondicion, new List<dynamic>(lsCondiciones));
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            lsCondiciones.RemoveAt(row.RowIndex);
            BindGrid(grvCondicion, new List<dynamic>(lsCondiciones));
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
            string campos = "";
            StringBuilder sb = new StringBuilder();

            foreach (GridViewRow r in grvCampos.Rows)
            {
                CheckBox chk = (CheckBox)r.Cells[0].Controls[1];
                if (chk.Checked)
                {
                    campos += grvCampos.DataKeys[r.RowIndex].Value.ToString() + ", ";
                }
            }

            lsCondiciones.ForEach(x =>
            {
                sb.AppendFormat(" {0}", x.Union);
                sb.AppendFormat(" {0}", x.Campo);
                sb.AppendFormat(" {0}", x.Operador);
                sb.AppendFormat(" {0}", x.Valor);
            });

            if (mensajes.Count() > 0)
            {
                Funciones.MostarMensajes("error", mensajes);
            }
            else
            {
                Distribucion oDistribucion = new Distribucion();
                if (!string.IsNullOrEmpty(campos)) campos = campos.Remove(campos.Length - 2, 2);
                string cond = sb.ToString().Trim();
                if (!string.IsNullOrEmpty(cond)) cond = cond.Substring(3, cond.Length - 3);
                oDistribucion.Campos = campos;
                oDistribucion.Condicion = cond;
                oDistribucion.Descripcion = txtDescripcion.Text.Trim();
                oDistribucion.Nombre = txtNombre.Text.Trim();
                cCatalogo = new CatalogoController();
                if (!cCatalogo.GuardarDistribucion(oDistribucion))
                {
                    mensajes = cCatalogo.Mensajes;
                    if (cCatalogo.Errores.Count() > 0) mensajes.AddRange(cCatalogo.Errores);
                    Funciones.MostarMensajes("error", cCatalogo.Mensajes);
                }
                else
                {
                    lsCondiciones = new List<Condicion>();
                    Cargainicial();
                    Funciones.MostarMensajes("success", new List<string> { "La operación se completo correctamente" });
                }
            }
        }
        catch (Exception ex)
        {
            Funciones.MostarMensajes("error", new List<string> { ex.Message });
        }
    }

    protected void grvCondicion_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grvCondicion.EditIndex = -1;
        BindGrid(grvCondicion, new List<dynamic>(lsCondiciones));
    }

    protected void grvCondicion_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grvCondicion.EditIndex = e.NewEditIndex;
        BindGrid(grvCondicion, new List<dynamic>(lsCondiciones));
    }

    protected void grvCondicion_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GridView _gridview = (GridView)sender;
            GridViewRow r = _gridview.Rows[e.RowIndex];
            List<string> mensajes = new List<string>();
            DropDownList cbxU = (DropDownList)r.Cells[0].Controls[1];
            if (string.IsNullOrEmpty(cbxU.SelectedValue))
            {
                mensajes.Add("Indique el tipo de union Y/O");
            }
            else
            {
                DropDownList cbxC = (DropDownList)r.Cells[1].Controls[1];
                DropDownList cbxO = (DropDownList)r.Cells[2].Controls[1];
                TextBox txtValor = (TextBox)r.Cells[3].Controls[1];
                if (string.IsNullOrEmpty(txtValor.Text.Trim()))
                {
                    mensajes.Add("Ingrese el valor");
                }
                else
                {
                    var obj = lsCondiciones.ElementAt(e.RowIndex);
                    obj.Campo = cbxC.SelectedItem.Value;
                    obj.Operador = cbxO.SelectedItem.Value;
                    obj.Union = cbxU.SelectedItem.Value;
                    obj.Valor = txtValor.Text.Trim();
                }
            }

            if (mensajes.Count() > 0)
            {
                Funciones.MostarMensajes("error", mensajes);
            }
            else
            {
                grvCondicion.EditIndex = -1;
                BindGrid(grvCondicion, new List<dynamic>(lsCondiciones));
            }
        }
        catch (Exception ex)
        {
            Funciones.MostarMensajes("error", new List<string> { ex.Message });
        }
    }
}

[Serializable()]
public class Condicion
{
    public string Union { get; set; }
    public string Campo { get; set; }
    public string Operador { get; set; }
    public string Valor { get; set; }
}