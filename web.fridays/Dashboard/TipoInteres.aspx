<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="TipoInteres.aspx.cs" Inherits="TipoInteress" Async="true" %>

<asp:Content ID="Css" ContentPlaceHolderID="HeadContent" runat="Server">
    <style>
        .col-name {
            width: 35%;
            height: 100%;
            float: left;
            margin-right: 40px;
        }

        body {
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

        #examples a {
            text-decoration: underline;
        }

        #geocomplete {
            width: 200px;
        }

        .map_canvas {
            width: 600px;
            height: 200px;
            margin: 10px 20px 10px 0;
        }

        #multiple li {
            cursor: pointer;
            text-decoration: underline;
        }

        .mapa-google {
            border: 1px solid #ababab;
            height: 200px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server" ID="upForm">
        <ContentTemplate>
            <div id="mainWrapper">
                <div id="box_row_titles">
                    <!-- Titles of the rows -->
                    <div id="row_titles2">
                        <div class="col-name">Nombre</div>
                        <div class="col-name">Descripción</div>
                        <asp:LinkButton runat="server" ID="add_subscriber" CssClass="semi_bold agregar" Style="top: -7px" ClientIDMode="Static" OnClick="AddTipo_Click"></asp:LinkButton>
                    </div>
                </div>
                <ul id="edit-urls-images" class="regular hidden" style="display: block;">
                    <asp:Repeater runat="server" ID="rptItems">
                        <ItemTemplate>
                            <li data-id="<%# Eval("TipoInteresID") %>" data-url="#" data-token="IzE0MzYwOTEyMzAzMzA=" data-value="San Francisco">
                                <div class="row_name"><%# Eval("Nombre") %></div>
                                <div><%# Eval("Descripcion") %></div>
                                <div class="actions semi_bold">
                                    <asp:LinkButton runat="server" ID="btnBaja" CssClass="analytics" OnClick="btnBaja_Click" CommandArgument='<%# Eval("TipoInteresID") %>'>Baja</asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnEditar" CssClass="analytics" OnClick="btnEditar_Click" CommandArgument='<%# Eval("TipoInteresID") %>'>Editar</asp:LinkButton>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!--popup-->
    <div id="popupOverlay" style="opacity: 1; transition: all 0.46s ease; display: none;">
        <div id="popup" style="opacity: 1; transition: all 0.46s ease; transform: scale(1) translateY(-50%);">
            <asp:Button runat="server" ID="btnGuardar" CssClass="btnTrue semi_bold" Style="left: 0px; width: 50%;" OnClick="btnGuardar_Click" Text="Aceptar" CausesValidation="true" ValidationGroup="guardar" UseSubmitBehavior="false" />
            <input type="button" value="Cancelar" class="btnFalse semi_bold" style="right: 0px; width: 50%;" onclick="closePopup();"><div id="sub_data_info" class="bold">
                <img src="/Images/default.png">Agregar dirección
            </div>
            <ul class="data_change clear-fix">
                <asp:UpdatePanel runat="server" ID="upModal">
                    <ContentTemplate>
                        <li class="clear-fix editar">
                            <div class="data_name semi_bold" style="background-image: url(../images/icon/data_name_name@2x.png)">Nombre:</div>
                            <div class="data_value">
                                <asp:TextBox runat="server" ID="txtNombre" CssClass="regular goFocus" MaxLength="30"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfvNombre" ControlToValidate="txtNombre" Display="Dynamic" ErrorMessage="*Requerido" SetFocusOnError="true" ValidationGroup="guardar" CssClass="Validators"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <li class="clear-fix editar">
                            <div class="data_name semi_bold" style="background-image: url(../images/icon/data_name_email@2x.png)">Descripción:</div>
                            <div class="data_value">
                                <asp:TextBox runat="server" ID="txtDescripcion" CssClass="regular input-direccion" MaxLength="200"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfvDireccion" ControlToValidate="txtDescripcion" Display="Dynamic" ErrorMessage="*Requerido" SetFocusOnError="true" ValidationGroup="guardar" CssClass="Validators"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnGuardar" />
                    </Triggers>
                </asp:UpdatePanel>
            </ul>
            <div class="closePopup"></div>
        </div>
    </div>

</asp:Content>
<asp:Content runat="server" ID="ScriptsJS" ContentPlaceHolderID="ScriptsPages">

  <%--  <script src="<%= ResolveClientUrl("~/Scripts/js/jquery.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/easing.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/jquery.gravatar.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/functions.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/custom.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/chartjs/waypoints.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/Scripts/js/mapa.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/Scripts/js/autoNumeric-min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/Scripts/js/funciones-generales.js") %>" type="text/javascript"></script>--%>

    <!-- -->
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/easing.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/jquery.gravatar.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/functions.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/custom.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/chartjs/waypoints.min.js") %>" type="text/javascript"></script>
    <%--<script src="<%= ResolveClientUrl("~/Scripts/js/color-picker.min.js") %>" type="text/javascript"></script>--%>
    <script src="<%= ResolveUrl("~/Scripts/js/autoNumeric-min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/Scripts/js/funciones-generales.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/Scripts/js/ajaxupload-min.js") %>" type="text/javascript"></script>
</asp:Content>

