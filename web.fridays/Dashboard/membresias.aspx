<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="membresias.aspx.cs" Inherits="membresias" Async="true" %>

<asp:Content runat="server" ID="Css" ContentPlaceHolderID="HeadContent">
    <link rel="stylesheet" type="text/css" href="<%= ResolveClientUrl("~/Content/classicTheme/style.css") %>" media="screen">
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <style>
        .Validators {
            float: right !important;
            margin-top: -50px;
            color: #f31111 !important;
        }

        #campaigns #row_max {
            width: 84px;
            height: 100%;
            float: left;
        }

        input[disabled], input[readonly], fieldset[disabled] {
            cursor: not-allowed;
        }

        /*Ajax Upload*/
        .ax-clear,
        .ax-browse-c,
        .ax-main-title,
        .ax-upload-all,
        .ax-upload.ax-button {
            display: none !important;
        }

        .ax-remove {
            width: 25px;
            height: 25px;
            background-image: url(../images/delete2.jpg);
            background-repeat: no-repeat;
            background-position-x: center;
            background-position-y: center;
            border: none;
        }

        .imgTarjeta:hover {
            opacity: 0.5;
            cursor: pointer;
        }

        input.color-picker {
            width: 40px !important;
            height: 40px !important;
            padding: 0px;
            border: none !important;
        }

        .color-picker:hover {
            cursor: pointer;
        }
    </style>
    <asp:UpdatePanel runat="server" ID="upForm">
        <ContentTemplate>
            <div id="mainWrapper">
                <div id="box_row_titles">
                    <!-- Titles of the rows -->
                    <div id="row_titles2">
                        <%--<div id="row_picture">#</div>--%>
                        <div id="row_color">Color</div>
                        <div id="row_name" class="semi_bold">Nombre</div>
                        <div id="row_date" class="semi_bold">Apartir De</div>
                        <div id="row_max" class="semi_bold">Hasta</div>
                        <div id="row_descuento" class="semi_bold">Descuento </div>
                        <%--<div id="row_tarjeta" class="semi_bold">Tarjeta </div>--%>
                        <asp:LinkButton runat="server" ID="add_subscriber" CssClass="semi_bold agregar" Style="top: -7px" ClientIDMode="Static" OnClick="AddMembresia_Click"></asp:LinkButton>
                    </div>
                </div>
                <ul id="edit-urls-images" class="regular hidden" style="display: block;">
                    <asp:Repeater runat="server" ID="rptItems">
                        <ItemTemplate>
                            <li data-id="<%# Eval("MembresiaId") %>">
                                <div class="row_color">
                                    <div <%# "style=\"margin: 10px; width: 50px; height: 50px; background: " + Eval("Color") + ";\"" %>>
                                        <span <%# "style=\"text-align: center; vertical-align: middle; color: " + Eval("ColorLetra") + ";\"" %>>Letras</span>
                                    </div>
                                </div>
                                <div class="row_name"><%# Eval("Nombre") %></div>
                                <div class="row_date"><%# Eval("ApartirDe") %></div>
                                <div class="row_date"><%# Eval("Hasta") %></div>
                                <div class="row_descuento"><%# Eval("PorcientoDescuento") %>%</div>
                                <div class="actions semi_bold">
                                    <asp:LinkButton runat="server" ID="btnBaja" CssClass="analytics" OnClick="btnBaja_Click" CommandArgument='<%# Eval("MembresiaId") %>'>Baja</asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnEditar" CssClass="analytics" CommandArgument='<%#Eval("MembresiaId") %>' OnClick="btnEditar_Click">Editar</asp:LinkButton>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="add_subscriber" />
        </Triggers>
    </asp:UpdatePanel>

    <!--popup-->
    <div id="popupOverlay" style="opacity: 1; transition: all 0.46s ease; display: none;">
        <div id="popup" style="opacity: 1; transition: all 0.46s ease; transform: scale(1) translateY(-50%);">
            <asp:LinkButton runat="server" ID="btnSave" CssClass="btn-save" OnClick="btnGuardar_Click" CausesValidation="true" ValidationGroup="guardar" UseSubmitBehavior="false" Style="display: none"></asp:LinkButton>
            <asp:Button runat="server" ID="btnGuardar" CssClass="btnTrue semi_bold" Style="left: 0px; width: 50%;" Text="Aceptar" />
            <input type="button" value="Cancelar" class="btnFalse semi_bold" style="right: 0px; width: 50%;" onclick="closePopup();"><div id="sub_data_info" class="bold">
                <img src="<%: ResolveUrl("~/Content/img/icons/default.png") %>">Agregar Membresia
            </div>
            <asp:UpdatePanel runat="server" ID="upMpdal">
                <ContentTemplate>
                    <ul class="data_change clear-fix">
                        <li class="clear-fix editar">
                            <div class="data_name semi_bold" style="background-image: url(../images/icon/data_name_name@2x.png)">Nombre:</div>
                            <div class="data_value">
                                <asp:TextBox runat="server" CssClass="regular goFocus" ID="txtNombre"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfvLongitud" ControlToValidate="txtNombre" Display="Dynamic" ErrorMessage="* Requerido" SetFocusOnError="true" ValidationGroup="guardar" CssClass="Validators"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <li class="clear-fix editar">
                            <div class="data_name semi_bold" style="background-image: url(../images/icon/data_name_custom1@2x.png)">Minimo Visitas:</div>
                            <div class="data_value">
                                <asp:TextBox runat="server" CssClass="regular entero" ID="txtVisitasMin"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtVisitasMin" Display="Dynamic" ErrorMessage="* Requerido" SetFocusOnError="true" ValidationGroup="guardar" CssClass="Validators"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <li class="clear-fix editar">
                            <div class="data_name semi_bold" style="background-image: url(../images/icon/data_name_custom1@2x.png)">Máximo Visitas:</div>
                            <div class="data_value">
                                <asp:TextBox runat="server" CssClass="regular entero" ID="txtVisitasMax"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtVisitasMax" Display="Dynamic" ErrorMessage="* Requerido" SetFocusOnError="true" ValidationGroup="guardar" CssClass="Validators"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <li class="clear-fix editar">
                            <div class="data_name semi_bold" style="background-image: url(../images/icon/data_name_custom1@2x.png)">Descuento:</div>
                            <div class="data_value">
                                <asp:TextBox runat="server" CssClass="regular decimal" ID="txtDescuento"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtDescuento" Display="Dynamic" ErrorMessage="*Requerido" SetFocusOnError="true" ValidationGroup="guardar" CssClass="Validators"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <li class="clear-fix editar">
                            <div class="data_name semi_bold" style="background-image: url(../images/icon/data_name_date@2x.png)">Color:</div>
                            <div class="data_value">
                                <asp:TextBox runat="server" type="color" CssClass="regular color-picker" ID="txtColor"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtColor" Display="Dynamic" ErrorMessage="*Requerido" SetFocusOnError="true" ValidationGroup="guardar" CssClass="Validators"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <li class="clear-fix editar">
                            <div class="data_name semi_bold" style="background-image: url(../images/icon/data_name_date@2x.png)">Color Letra:</div>
                            <div class="data_value">
                                <asp:TextBox runat="server" type="color" CssClass="regular color-picker" ID="txtColorL"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtColorL" Display="Dynamic" ErrorMessage="*Requerido" SetFocusOnError="true" ValidationGroup="guardar" CssClass="Validators"></asp:RequiredFieldValidator>
                            </div>
                        </li>
                        <li class="clear-fix editar">
                            <div class="data_name semi_bold" style="background-image: url(../images/icon/data_name_date@2x.png)">Tarjeta:</div>
                            <div class="data_value">
                                <asp:Image runat="server" ID="imgTarjeta" CssClass="imgTarjeta" ToolTip="Click para seleccionar imagen" Height="36" Width="62" ImageUrl="~/Images/icon-gallery.svg" />
                                <asp:HiddenField runat="server" ID="hfTajeta" ClientIDMode="Static" Value="" />
                            </div>
                        </li>
                        <li class="clear-fix">
                            <div id="uploader_div">
                            </div>
                        </li>
                    </ul>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" />
                    <asp:AsyncPostBackTrigger ControlID="rptItems" />
                    <asp:AsyncPostBackTrigger ControlID="btnSave" />
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
    <script src="<%= ResolveUrl("~/Scripts/js/ajaxupload-min.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        function pageLoad(sender, args) {
            $(document).ready(function () {
                var filePath = '<%= ConfigurationManager.AppSettings["RutaImagenes"]%>';
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

                $('#uploader_div').ajaxupload({
                    url: '../upload.aspx',
                    maxFileSize: '1M',
                    maxFiles: 1,
                    resizeImage: {
                        maxWidth: 60,
                        maxHeight: 42,
                        quality: 0.5,
                        scaleMethod: undefined,
                        format: undefined,
                        removeExif: false
                    },
                    allowExt: ['jpg', 'jpeg', 'bmp', 'png'],
                    removeOnSuccess: true,
                    error: function (txt, obj) {
                        notification(txt, 'error');
                    },
                    onSelect: function (files) {
                        var ruta = filePath.replace("~", "");
                        console.log(ruta);
                        var name = ruta + files[0].name;
                        $("#hfTajeta").val(name);
                    },
                    finish: function (file) {
                        GudarDatos();
                    },
                    success: function (file_name) {
                        var ruta = filePath.replace("~", "");
                        $(".imgTarjeta").attr("src", ruta + file_name);
                    }
                });

                $(document).on("click", ".imgTarjeta", function (e) {
                    $(".ax-browse").trigger("click");
                    e.preventDefault();
                    e.stopPropagation();
                    e.stopImmediatePropagation();
                });

                $(document).on("click", ".btnTrue", function (e) {
                    var Archivos = $('.ax-file-list li');
                    var seleccionado = $("#hfTajeta").val();
                    var procesado = $(".imgTarjeta").attr("src");

                    console.log(seleccionado);
                    console.log(procesado);

                    if (seleccionado == procesado) {
                        GudarDatos();
                    }
                    else if (Archivos.length == 0) {
                        notification('Seleccione una imagen', 'error');
                    }
                    else {
                        $(".ax-upload").click();
                    }
                    e.preventDefault();
                    e.stopPropagation();
                    e.stopImmediatePropagation();
                });

                window.GudarDatos = function () {
                    document.getElementById("<%= btnSave.ClientID %>").click();
                    //eval($(".btn-save").attr('href'));
                }
            });
        }
    </script>
</asp:Content>

