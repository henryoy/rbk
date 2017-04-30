<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Distribucion.aspx.cs" Inherits="Dashboard_Distribucion" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style>
        .fila {
            width: 100%;
            padding: 10px;
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
    </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <%--<div class="fila">
        <div class="columna" style="padding-left: 0px;">
            <label for="txtNombre">Nombre:</label>
            <asp:TextBox runat="server" ID="txtNombre" CssClass="regular"></asp:TextBox>
        </div>
        <div class="columna" style="padding-left: 0px;">
            <label for="txtDescripcion">Descripcion:</label>
            <asp:TextBox runat="server" ID="txtDescripcion" CssClass="regular"></asp:TextBox>
        </div>
    </div>--%>
    <ul class="data_change clear-fix">
        <li class="clear-fix editar">
            <div class="data_name semi_bold" style="background-image: url(../images/icon/data_name_name@2x.png)">Nombre:</div>
            <div class="data_value">
                <asp:TextBox runat="server" ID="txtNombre" CssClass="regular goFocus" MaxLength="30"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvNombre" ControlToValidate="txtNombre" Display="Dynamic" ErrorMessage="*Requerido" SetFocusOnError="true" ValidationGroup="guardar" CssClass="Validators"></asp:RequiredFieldValidator>
            </div>
        </li>
        <li class="clear-fix editar">
            <div class="data_name semi_bold" style="background-image: url(../images/icon/data_name_custom1@2x.png)">Descripción:</div>
            <div class="data_value">
                <asp:TextBox runat="server" ID="txtDescripcion" CssClass="regular"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtDescripcion" Display="Dynamic" ErrorMessage="* Requerido" SetFocusOnError="true" ValidationGroup="guardar" CssClass="Validators"></asp:RequiredFieldValidator>
            </div>
        </li>
    </ul>
    <div class="fila">
        <asp:UpdatePanel runat="server" ID="upGrid">
            <ContentTemplate>
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
                        <div role="alert">
                            <p>¡No exite información para mostrar!</p>
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
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

