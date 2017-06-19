<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Usuario.aspx.cs" Inherits="Dashboard_Usuario" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <style>
        .col-name {
            width: 30%;
            height: 100%;
            float: left;
            /*margin-right: 20px;*/
        }

        .row_name {
            width: 30% !important;
            height: 100% !important;
            float: left !important;
            margin-right: 0px;
            padding-left: 0px;
            margin-left: 5px;
        }

        .row_picture {
            height: 30px;
            white-space: nowrap;
            overflow-x: hidden;
            text-overflow: ellipsis;
            height: 100%;
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

        #multiple li {
            cursor: pointer;
            text-decoration: underline;
        }
    </style>
    <asp:UpdatePanel runat="server" ID="upForm">
        <ContentTemplate>

            <div id="mainWrapper">
                <div id="box_row_titles">
                    <!-- Titles of the rows -->
                    <div id="row_titles2">
                        <div class="col-name" style="width: 50px;">Clave</div>
                        <div class="col-name">Nombre</div>
                        <div class="col-name">Correo</div>
                        <div class="col-name">Estatus</div>
                        <asp:LinkButton runat="server" ID="add_subscriber" CssClass="semi_bold agregar" Style="top: -7px" ClientIDMode="Static" OnClick="btnUsaurio_Click"></asp:LinkButton>
                    </div>
                </div>
                <ul id="edit-urls-images" class="regular hidden" style="display: block;">
                    <asp:Repeater runat="server" ID="rptItems">
                        <ItemTemplate>
                            <li data-id="<%# Eval("UsuarioId") %>" data-url="#" data-token="IzE0MzYwOTEyMzAzMzA=" data-value="San Francisco">
                                <div class="row_picture" title='<%# Eval("IDExterno") %>'><%# Eval("IDExterno") %></div>
                                <div class="row_name"><%# Eval("Nombre") %></div>
                                <div class="row_name"><%# Eval("Email") %></div>
                                <div class="row_name"><%# Eval("Estatus") %></div>
                                <div class="actions semi_bold">
                                    <asp:LinkButton runat="server" ID="btnBaja" CssClass="analytics" OnClick="btnBaja_Click" CommandArgument='<%# Eval("UsuarioId") %>'>Baja</asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnEditar" CssClass="analytics" OnClick="btnEditar_Click" CommandArgument='<%# Eval("UsuarioId") %>'>Editar</asp:LinkButton>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="add_subscriber" />
            <asp:AsyncPostBackTrigger ControlID="rptItems" />
        </Triggers>
    </asp:UpdatePanel>

    <!--popup-->
    <div id="popupOverlay" style="opacity: 1; transition: all 0.46s ease; display: none;">
        <div id="popup" style="opacity: 1; transition: all 0.46s ease; transform: scale(1) translateY(-50%);">
            <asp:Button runat="server" ID="btnGuardar" CssClass="btnTrue semi_bold" Style="left: 0px; width: 50%;" OnClick="btnGuardar_Click" Text="Aceptar" CausesValidation="true" ValidationGroup="guardar" UseSubmitBehavior="false" />
            <input type="button" value="Cancelar" class="btnFalse semi_bold" style="right: 0px; width: 50%;" onclick="closePopup();"><div id="sub_data_info" class="bold">
                <img src="/Images/default.png">Agregar Usuario
            </div>
            <ul class="data_change clear-fix">
                <asp:UpdatePanel runat="server" ID="upModal">
                    <ContentTemplate>
                        <li class="clear-fix editar">
                            <div class="data_name semi_bold" style="background-image: url(../images/icon/data_name_custom1@2x.png)">Clave:</div>
                            <div class="data_value">
                                <asp:TextBox runat="server" ID="txtClave" CssClass="regular" MaxLength="200"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtClave" Display="Dynamic" ErrorMessage="*Requerido" SetFocusOnError="true" ValidationGroup="guardar" CssClass="Validators"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <li class="clear-fix editar">
                            <div class="data_name semi_bold" style="background-image: url(../images/icon/data_name_name@2x.png)">Nombre:</div>
                            <div class="data_value">
                                <asp:TextBox runat="server" ID="txtNombre" CssClass="regular goFocus" MaxLength="30"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfvNombre" ControlToValidate="txtNombre" Display="Dynamic" ErrorMessage="*Requerido" SetFocusOnError="true" ValidationGroup="guardar" CssClass="Validators"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <li class="clear-fix editar">
                            <div class="data_name semi_bold" style="background-image: url(../images/icon/data_name_email@2x.png)">Correo:</div>
                            <div class="data_value">
                                <asp:TextBox runat="server" ID="txtCorreo" CssClass="regular" MaxLength="200"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtCorreo" Display="Dynamic" ErrorMessage="Correo no válido" SetFocusOnError="true" ValidationGroup="guardar" CssClass="Validators"></asp:RegularExpressionValidator>
                                <%--<asp:RequiredFieldValidator runat="server" ID="rfvDireccion" ControlToValidate="txtCorreo" Display="Dynamic" ErrorMessage="*Requerido" SetFocusOnError="true" ValidationGroup="guardar" CssClass="Validators"></asp:RequiredFieldValidator>--%>
                            </div>
                        </li>
                        <li class="clear-fix editar">
                            <div class="data_name semi_bold" style="background-image: url(../images/icon/data_name_custom1@2x.png)">Contraseña:</div>
                            <div class="data_value">
                                <asp:TextBox runat="server" ID="txtPass" CssClass="regular" MaxLength="200" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtPass" Display="Dynamic" ErrorMessage="*Requerido" SetFocusOnError="true" ValidationGroup="guardar" CssClass="Validators"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <li class="clear-fix editar">
                            <div class="data_name semi_bold" style="background-image: url(../images/icon/data_name_custom1@2x.png)">Confirmar Contraseña:</div>
                            <div class="data_value">
                                <asp:TextBox runat="server" ID="txtPass2" CssClass="regular" MaxLength="200" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtPass2" Display="Dynamic" ErrorMessage="*Requerido" SetFocusOnError="true" ValidationGroup="guardar" CssClass="Validators"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <li class="clear-fix editar">
                            <div class="data_name semi_bold" style="background-image: url(../images/icon/data_name_custom1@2x.png)">Fecha Nacimiento:</div>
                            <div class="data_value">
                                <asp:TextBox runat="server" ID="txtFechaNac" CssClass="regular" MaxLength="200" TextMode="Date"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtFechaNac" Display="Dynamic" ErrorMessage="*Requerido" SetFocusOnError="true" ValidationGroup="guardar" CssClass="Validators"></asp:RequiredFieldValidator>
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
    <script src="<%= ResolveClientUrl("~/Scripts/js/jquery.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/easing.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/jquery.gravatar.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/functions.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/custom.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/chartjs/waypoints.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/Scripts/js/autoNumeric-min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/Scripts/js/funciones-generales.js") %>" type="text/javascript"></script>
    <%--<script>
        function pageLoad(sender, args) {
            $(document).ready(function () {

            });
        }
    </script>--%>
</asp:Content>
