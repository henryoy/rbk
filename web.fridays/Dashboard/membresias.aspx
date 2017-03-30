<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="membresias.aspx.cs" Inherits="membresias" Async="true" %>

<asp:Content runat="server" ID="Css" ContentPlaceHolderID="HeadContent">
    <link rel="stylesheet" type="text/css" href="<%= ResolveClientUrl("~/Content/css/color-picker.css") %>" media="screen">
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

        .Validators {
            float: right !important;
            margin-top: -50px;
            color: #f31111 !important;
        }
    </style>
    <asp:UpdatePanel runat="server" ID="upForm">
        <ContentTemplate>
            <div id="campaigns" class="disable_selection semi_bold">
                <!-- Top Bar -->
                <div id="list_name_bar">
                    <h2><b class="light cat">Membresia</b></h2>
                </div>
                <div id="mainWrapper">
                    <div id="box_row_titles">
                        <!-- Titles of the rows -->
                        <div id="row_titles2">
                            <div id="row_picture">#</div>
                            <div id="row_color">Color</div>
                            <div id="row_name" class="semi_bold">Nombre</div>
                            <div id="row_date" class="semi_bold">Visitas </div>
                            <div id="row_descuento" class="semi_bold">Descuento </div>
                            <div id="row_tarjeta" class="semi_bold">Tarjeta </div>
                            <asp:LinkButton runat="server" ID="add_subscriber" CssClass="semi_bold agregar" Style="top: -7px" ClientIDMode="Static" OnClick="AddMembresia_Click"></asp:LinkButton>
                        </div>
                    </div>
                    <ul id="edit-urls-images" class="regular hidden" style="display: block;">
                        <asp:Repeater runat="server" ID="rptItems">
                            <ItemTemplate>
                                <li data-id="<%# Eval("MembresiaId") %>">
                                    <div class="row_picture">
                                        <label>
                                            <input type="checkbox"><a></a>
                                        </label>
                                    </div>
                                    <div class="row_color">
                                        <div <%# "style=\"margin: 10px; width: 50px; height: 50px; background: "+Eval("Color")+";\"" %>>
                                        </div>
                                    </div>
                                    <div class="row_name"><%# Eval("Nombre") %></div>
                                    <div class="row_date"><%# Eval("NumeroDeVisitas") %></div>
                                    <div class="row_descuento"><%# Eval("PorcientoDescuento") %>%</div>
                                    <div class="row_tarjeta">Tarjeta </div>
                                    <div class="actions semi_bold">
                                        <asp:LinkButton runat="server" ID="btnEditar" CssClass="analytics" CommandArgument='<%#Eval("MembresiaId") %>' OnClick="btnEditar_Click">Editar</asp:LinkButton>
                                        <div class="view">
                                            <asp:LinkButton runat="server" ID="btnVer" CssClass="info" OnClick="btnVer_Click" CommandArgument='<%# Eval("MembresiaId") %>'></asp:LinkButton>
                                            <%--<asp:LinkButton runat="server" ID="btnVer" CssClass="info" OnClick="btnVer_Click" CommandArgument='<%# Eval("MembresiaId") %>'></asp:LinkButton>--%>
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
                <img src="<%: ResolveUrl("~/Content/img/icons/default.png") %>">Agregar Membresia
            </div>
            <asp:UpdatePanel runat="server" ID="upMpdal">
                <ContentTemplate>
                    <ul class="data_change clear-fix">
                        <li class="clear-fix">
                            <div class="data_name semi_bold" style="background-image: url()">Nombre:</div>
                            <div class="data_value">
                                <asp:TextBox runat="server" CssClass="regular goFocus" ID="txtNombre"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfvLongitud" ControlToValidate="txtNombre" Display="Dynamic" ErrorMessage="* Requerido" SetFocusOnError="true" ValidationGroup="guardar" CssClass="Validators"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <li class="clear-fix">
                            <div class="data_name semi_bold" style="background-image: url()">Minimo Visitas:</div>
                            <div class="data_value">
                                <asp:TextBox runat="server" CssClass="regular entero" ID="txtVisitasMin"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtVisitasMin" Display="Dynamic" ErrorMessage="* Requerido" SetFocusOnError="true" ValidationGroup="guardar" CssClass="Validators"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <li class="clear-fix">
                            <div class="data_name semi_bold" style="background-image: url()">Máximo Visitas:</div>
                            <div class="data_value">
                                <asp:TextBox runat="server" CssClass="regular entero" ID="txtVisitasMax"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtVisitasMax" Display="Dynamic" ErrorMessage="* Requerido" SetFocusOnError="true" ValidationGroup="guardar" CssClass="Validators"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <li class="clear-fix">
                            <div class="data_name semi_bold" style="background-image: url()">Descuento:</div>
                            <div class="data_value">
                                <asp:TextBox runat="server" CssClass="regular decimal" ID="txtDescuento"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtDescuento" Display="Dynamic" ErrorMessage="*Requerido" SetFocusOnError="true" ValidationGroup="guardar" CssClass="Validators"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <li class="clear-fix">
                            <div class="data_name semi_bold" style="background-image: url()">Color:</div>
                            <div class="data_value">
                                <asp:TextBox runat="server" type="color" CssClass="regular color-picker" ID="txtColor"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtColor" Display="Dynamic" ErrorMessage="*Requerido" SetFocusOnError="true" ValidationGroup="guardar" CssClass="Validators"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                    </ul>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="closePopup"></div>
        </div>
    </div>

</asp:Content>
<asp:Content runat="server" ID="ScriptsJS" ContentPlaceHolderID="ScriptsPages">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/easing.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/jquery.gravatar.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/functions.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/custom.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/chartjs/waypoints.min.js") %>" type="text/javascript"></script>
    <%--<script src="<%= ResolveClientUrl("~/Scripts/js/color-picker.min.js") %>" type="text/javascript"></script>--%>
    <script src="<%= ResolveUrl("~/Scripts/js/autoNumeric-min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/Scripts/js/funciones-generales.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        function pageLoad(sender, args) {
            $(document).ready(function () {
                //var picker = new CP(document.querySelector('.color-picker'));
                //picker.on("change", function (color) {
                //    this.target.value = '#' + color;
                //    document.body.style.backgroundColor = '#' + color;
                //}, 'main-change');

                //var colors = ['012', '123', '234', '345', '456', '567', '678', '789', '89a', '9ab'], box;

                //for (var i = 0, len = colors.length; i < len; ++i) {
                //    box = document.createElement('span');
                //    box.className = 'color-picker-box';
                //    box.title = '#' + colors[i];
                //    box.style.backgroundColor = '#' + colors[i];
                //    box.addEventListener("click", function (e) {
                //        picker.set(this.title);
                //        picker.trigger("change", [this.title.slice(1)], 'main-change');
                //        e.stopPropagation();
                //    }, false);
                //    picker.picker.firstChild.appendChild(box);
                //}

                $('.decimal').autoNumeric('init', {
                    aForm: false,
                    aDec: '.',
                    vMin: '-999999999.99',
                    vMax: '999999999.99'
                });

                $('.entero').autoNumeric('init', {
                    aForm: false,
                    vMin: '0',
                    vMax: '999999999'
                });
            });
        }
    </script>
</asp:Content>

