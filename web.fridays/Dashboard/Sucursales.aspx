<%@ Page Language="C#" Title="Sucursales" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="Sucursales.aspx.cs" Inherits="Sucursales" Async="true" %>

<asp:Content runat="server" ID="Css" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <style>
        .back_btn {
            height: 43px;
            position: absolute;
            left: 50px;
            top: 20px;
            color: #919191;
            font-size: 12px;
            text-transform: uppercase;
            cursor: pointer;
            background-image: url(../content/img/icons/arrow_left.png);
            background-position: 1px center;
            background-repeat: no-repeat;
            background-color: #FFF;
            line-height: 43px;
            padding-left: 15px;
            padding-right: 20px;
        }

            .back_btn:hover {
                color: #69c0af;
                background-image: url(../content/img/icons/arrow_left_hover.png);
            }

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

        .Validators {
            float: right !important;
            margin-top: -50px;
            color: #f31111 !important;
        }

        .formulario-clientes .panel {
            border: 1px solid #ddd;
            margin-bottom: 0px;
            -webkit-box-shadow: none;
            -moz-box-shadow: none;
            box-shadow: none;
        }

        .formulario-clientes .tab-circulacion .panel .contenedor-tabla .tabla-principal {
            border-bottom: 0;
        }

        .formulario-clientes .tab-circulacion .panel .btn {
            border-radius: 0;
        }

        .formulario-clientes .tab-circulacion .panel .btn-default {
            background-color: #fff;
            border-color: #ddd;
        }

            .formulario-clientes .tab-circulacion .panel .btn-default:hover,
            .formulario-clientes .tab-circulacion .panel .btn-default:focus {
                background-color: #D7D7D7;
                border-color: #bbb;
                color: #333;
            }

        .contenedor-btn-alta {
            margin-bottom: 15px;
        }

        .mapa-google {
            border: 1px solid #ababab;
            height: 200px;
        }

        .modal-direccion-entrega .panel {
            border-bottom: 0;
            border-left: 0;
            border-right: 0;
        }

        .tab-content > .active {
            display: block;
            visibility: visible;
        }

        .fade.in {
            opacity: 1;
        }

        .fade {
            opacity: 0;
            -webkit-transition: opacity .15s linear;
            -o-transition: opacity .15s linear;
            transition: opacity .15s linear;
        }
    </style>
    <%--<div id="popupOverlay">
        <div id="popup">
            <div class="closePopup">
            </div>
        </div>
    </div>--%>
    <asp:UpdatePanel runat="server" ID="upForm">
        <ContentTemplate>
            <div id="campaigns" class="semi_bold">
                <div id="list_name_bar">
                    <h2><b class="light cat">Sucursales</b></h2>
                    <!-- dropdown -->
                </div>
                <div id="mainWrapper">
                    <div id="box_row_titles">
                        <!-- Titles of the rows -->
                        <div id="row_titles2">
                            <div id="row_picture">#</div>
                            <div class="col-name">Nombre</div>
                            <div class="col-name">Dirección</div>
                            <asp:LinkButton runat="server" ID="add_subscriber" CssClass="semi_bold agregar" Style="top: -7px" ClientIDMode="Static" OnClick="AddSucursal_Click"></asp:LinkButton>
                        </div>
                    </div>
                    <ul id="edit-urls-images" class="regular hidden" style="display: block;">
                        <asp:Repeater runat="server" ID="rptItems">
                            <ItemTemplate>
                                <li data-id="<%# Eval("SucursalID") %>" data-url="#" data-token="IzE0MzYwOTEyMzAzMzA=" data-value="San Francisco">
                                    <div class="row_picture">
                                        <label>
                                            <input type="checkbox"><a></a>
                                        </label>
                                    </div>
                                    <div class="row_name"><%# Eval("Nombre") %></div>
                                    <div><%# Eval("Direccion") %></div>
                                    <div class="actions semi_bold">
                                        <asp:LinkButton runat="server" ID="btnEditar" CssClass="analytics" OnClick="btnEditar_Click" CommandArgument='<%# Eval("SucursalID") %>'>Editar</asp:LinkButton>
                                        <div class="view">
                                            <asp:LinkButton runat="server" ID="btnVer" CssClass="info" OnClick="btnVer_Click" CommandArgument='<%# Eval("SucursalID") %>'></asp:LinkButton>
                                        </div>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="add_subscriber" />
        </Triggers>
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
                        <li class="clear-fix">
                            <div class="data_name semi_bold" style="background-image: url(../images/icon/data_name_name@2x.png)">Nombre:</div>
                            <div class="data_value">
                                <asp:TextBox runat="server" ID="txtNombre" CssClass="regular goFocus" MaxLength="30"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfvNombre" ControlToValidate="txtNombre" Display="Dynamic" ErrorMessage="*Requerido" SetFocusOnError="true" ValidationGroup="guardar" CssClass="Validators"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <li class="clear-fix">
                            <div class="data_name semi_bold" style="background-image: url(../images/icon/data_name_email@2x.png)">Dirección:</div>
                            <div class="data_value">
                                <asp:TextBox runat="server" ID="txtDireccion" CssClass="regular input-direccion" MaxLength="200"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfvDireccion" ControlToValidate="txtDireccion" Display="Dynamic" ErrorMessage="*Requerido" SetFocusOnError="true" ValidationGroup="guardar" CssClass="Validators"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <li class="clear-fix">
                            <div class="data_name semi_bold" style="background-image: url(../images/icon/data_name_custom1@2x.png)">Longitud:</div>
                            <div class="data_value">
                                <asp:TextBox runat="server" ID="txtLongitud" CssClass="regular position" Style="pointer-events: none"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfvLongitud" ControlToValidate="txtLongitud" Display="Dynamic" ErrorMessage="*Requerido" SetFocusOnError="true" ValidationGroup="guardar" CssClass="Validators"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <li class="clear-fix">
                            <div class="data_name semi_bold" style="background-image: url(../images/icon/data_name_custom1@2x.png)">Latitud:</div>
                            <div class="data_value">
                                <asp:TextBox runat="server" ID="txtLatitud" CssClass="regular position" Style="pointer-events: none"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfvLatitud" ControlToValidate="txtLatitud" Display="Dynamic" ErrorMessage="*Requerido" SetFocusOnError="true" ValidationGroup="guardar" CssClass="Validators"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnGuardar" />
                    </Triggers>
                </asp:UpdatePanel>
                <li class="clear-fix tab-content">
                    <div runat="server" id="tabUbicacionMapa" role="tabpanel" class="tab-pane fade tab-ubicacion active in">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" ID="txtBuscarMaps" CssClass="regular input-map" placeholder="Buscar" />
                                </div>
                            </div>
                        </div>
                        <div id="mapaUbicacion" class="mapa-google"></div>
                    </div>
                </li>
            </ul>
            <div class="closePopup"></div>
        </div>
    </div>

</asp:Content>
<asp:Content runat="server" ID="ScriptsJS" ContentPlaceHolderID="ScriptsPages">
    <script src="http://maps.googleapis.com/maps/api/js?key=AIzaSyCr483jOUSdhjAnDWjUKiCj9rnEWvLMOFk&libraries=geometr‌​y,places" async defer type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/jquery.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/easing.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/jquery.gravatar.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/functions.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/custom.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/chartjs/waypoints.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/Scripts/js/mapa.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/Scripts/js/autoNumeric-min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/Scripts/js/funciones-generales.js") %>" type="text/javascript"></script>
    <script>
        function pageLoad(sender, args) {
            $(document).ready(function () {
                //function to check for empty campaigns
                window.initMap = function () {
                    var lat = $('#<%= txtLatitud.ClientID %>').val(),
                            lng = $('#<%= txtLongitud.ClientID %>').val(),
                    configuracion;
                    if (lat != '' || lng != '') {
                        configuracion = {
                            txtBuscarId: '#<%= txtBuscarMaps.ClientID %>',
                            txtLatId: '#<%= txtLatitud.ClientID %>',
                            txtLngId: '#<%= txtLongitud.ClientID %>',
                            Lat: lat,
                            Lng: lng,
                            zoom: 15
                        }
                    }
                    else {
                        configuracion = {
                            txtBuscarId: '#<%= txtBuscarMaps.ClientID %>',
                            txtLatId: '#<%= txtLatitud.ClientID %>',
                            txtLngId: '#<%= txtLongitud.ClientID %>'
                        }
                    }
                    $('#mapaUbicacion').mapaMultimple(configuracion);
                }
            });
        }
    </script>
</asp:Content>

