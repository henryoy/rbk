<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Distribucion.aspx.cs" Inherits="Dashboard_Distribucion" EnableEventValidation="false" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style>
        .fila {
            width: 100%;
            padding-top: 10px;
            padding-bottom: 10px;
        }

        div {
            display: block;
        }

        #campaigns {
            overflow: hidden;
        }

        .table {
            width: 100%;
            max-width: 100%;
            margin-bottom: 20px;
            /*background-color: transparent;*/
            border-spacing: 0;
        }

        .columna {
            position: relative;
            min-height: 1px;
            padding-right: 15px;
            padding-left: 15px;
        }

            .columna label {
                min-width: 70px;
                width: 10%;
            }

            .columna input {
                min-width: 70px;
                width: 90%;
            }

        .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
            padding: 8px;
            line-height: 1.42857143;
            vertical-align: top;
            border-top: 1px solid #ddd;
            text-align: left;
            font-family: 'source_sans_proregular', Helvetica, Arial, sans-serif;
        }

        .table > thead > tr > th {
            border-top: none !important;
            height: 50px;
            line-height: 50px;
            color: #919191;
        }

        .table > tbody > tr > td {
            background-color: #fff !important;
        }

        .table > tbody {
            box-shadow: 0px 0px 0px 1px #e1e1e1;
        }

        .data_name {
            width: 17% !important;
            color: #333;
        }

        .data_value {
            width: 83%;
        }

        /*.check-todos {
        }*/
        label {
            float: none;
        }

        body {
            overflow-y: auto !important;
            color: #333;
        }

        body, input, button {
            line-height: 1.4;
            font: 13px Helvetica,arial,freesans,clean,sans-serif;
        }

        a {
            color: #4183C4;
            text-decoration: none;
        }

        hr {
            margin: 0px;
        }

        .add-option {
            float: right;
            padding: 3px;
            border: 1px solid #919191;
            width: 70px;
            text-align: center;
        }

        .empty {
            text-align: center;
        }

        #campaigns li input[type="text"] {
            width: 85%;
        }

        .deshabilitado {
            cursor: not-allowed !important;
            pointer-events: none;
        }

            tr.deshabilitado > td > ul > li > a,
            tr.deshabilitado > td > li > a,
            tr.deshabilitado > td > a,
            .deshabilitado > ul > li > a,
            .deshabilitado > li > a .deshabilitado > a,
            a.deshabilitado,
            a.btn.deshabilitado,
            .disabled,
            .disabled:focus,
            .btn.disabled,
            .btn.disabled:focus {
                background-color: #eee !important;
                border-color: #dfdfdf !important;
                color: #bdbdbd !important;
                filter: alpha(opacity=100);
                opacity: 1;
                text-shadow: none;
                -webkit-box-shadow: none;
                box-shadow: none;
            }

        tr.deshabilitado {
            background: none !important;
            border: none !important;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="fila" style="height: 30px; padding-right: 10px;">
        <asp:Button runat="server" ID="btnGuardar" CssClass="add-option semi_bold" OnClick="btnGuardar_Click" Text="Guardar" CausesValidation="true" ValidationGroup="guardar" UseSubmitBehavior="false" />
    </div>
    <ul class="data_change clear-fix">
        <li class="clear-fix editar">
            <div class="data_name semi_bold" style="background-image: url(../images/icon/data_name_name@2x.png)">Nombre:</div>
            <div class="data_value">
                <asp:TextBox runat="server" ID="txtNombre" CssClass="regular goFocus" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvNombre" ControlToValidate="txtNombre" Display="Dynamic" ErrorMessage="*Requerido" SetFocusOnError="true" ValidationGroup="guardar" CssClass="Validators"></asp:RequiredFieldValidator>
            </div>
        </li>
        <li class="clear-fix editar">
            <div class="data_name semi_bold" style="background-image: url(../images/icon/data_name_custom1@2x.png)">Descripción:</div>
            <div class="data_value">
                <asp:TextBox runat="server" ID="txtDescripcion" CssClass="regular" MaxLength="250"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtDescripcion" Display="Dynamic" ErrorMessage="* Requerido" SetFocusOnError="true" ValidationGroup="guardar" CssClass="Validators"></asp:RequiredFieldValidator>
            </div>
        </li>
    </ul>
    <div class="fila" style="padding-top: 1px;">
        <asp:UpdatePanel runat="server" ID="upGrid">
            <ContentTemplate>
                <div class="fila" style="background: #fff;">
                    <span>Campos</span>
                </div>
                <hr />
                <asp:GridView runat="server" ID="grvCampos" GridLines="None" ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" DataKeyNames="Campo"
                    CssClass="table">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="check-todos">
                            <HeaderTemplate>
                                <asp:CheckBox runat="server" Text="Todos" ID="chkAll" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkCampo" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                    </Columns>
                    <EmptyDataTemplate>
                        <div role="alert" class="empty">
                            <p>¡No exite información para mostrar!</p>
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
                <div class="fila" style="background: #fff;">
                    <span>Condiciones</span>
                    <asp:LinkButton runat="server" ID="btnAddCondicion" CssClass="add-option semi_bold" OnClick="btnAddCondicion_Click">Agregar</asp:LinkButton>
                </div>
                <hr />
                <asp:GridView runat="server" ID="grvCondicion" GridLines="None" ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="table"
                    OnRowDataBound="grvCondicion_RowDataBound"
                    OnRowCancelingEdit="grvCondicion_RowCancelingEdit"
                    OnRowEditing="grvCondicion_RowEditing"
                    OnRowUpdating="grvCondicion_RowUpdating">
                    <Columns>
                        <asp:TemplateField HeaderText="Union">
                            <ItemTemplate>
                                <%# Eval("Union") %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList runat="server" ID="cbxUnion">
                                    <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem Value="AND">Y</asp:ListItem>
                                    <asp:ListItem Value="OR">O</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Campo">
                            <ItemTemplate>
                                <%# Eval("Campo") %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList runat="server" ID="cbxCampo">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Operador">
                            <ItemTemplate>
                                <%# Eval("Operador") %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList runat="server" ID="cbxOperador">
                                    <asp:ListItem Value="=">=</asp:ListItem>
                                    <asp:ListItem Value=">=">>=</asp:ListItem>
                                    <asp:ListItem Value="<="><=</asp:ListItem>
                                    <asp:ListItem Value="!=">!=</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Valor">
                            <ItemTemplate>
                                <%# Eval("Valor") %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" ID="txtValor" Text='<%# Eval("Valor") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="btnEliminar" OnClick="btnEliminar_Click">Eliminar</asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton runat="server" ID="btnAplicar" CommandName="Update">Aceptar</asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnCancelar" CommandName="Cancel">Cancelar</asp:LinkButton>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <emptydatatemplate>
                        <div role="alert" class="empty">
                            <p>¡No exite información para mostrar!</p>
                        </div>
                    </emptydatatemplate>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="grvCampos" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="ScriptsJS" ContentPlaceHolderID="ScriptsPages" runat="Server">
    <script src="<%= ResolveClientUrl("~/Scripts/js/jquery.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/easing.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/jquery.gravatar.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/functions.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/custom.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/chartjs/waypoints.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/Scripts/js/autoNumeric-min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/Scripts/js/funciones-generales.js") %>" type="text/javascript"></script>
</asp:Content>
