<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="Tetris.aspx.cs" Inherits="Dashboard_Tetris" %>

<asp:Content runat="server" ID="Css" ContentPlaceHolderID="HeadContent">
    <style>
        .subscriber_email_address img {
             width: 10px;
             border-radius: 100%;
             position: static;
             left: 24px; 
             top: 239px; 
        }
    </style>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div id="mainWrapper">
        <!-- Tabs -->
        <div id="tabs">
            <!-- Ul that hold the tabs -->
            <ul class="clear-fix semi_bold">
                <!-- Today tab -->
                <!-- All tab -->
                <li class="activedTab">
                    <asp:LinkButton runat="server" ID="lnkRegistroAll" OnClick="lnkRegistroAll_Click">
                        Todos
                        <asp:Label runat="server" ID="lblRegistros" CssClass="tabs_all_active bubble">0</asp:Label>
                    </asp:LinkButton>
                </li>
            </ul>

        </div>
        <!-- Indicator -->
        <div id="indicator" class="indicator_today semi_bold">
            <div class="indicator_today_name semi_bold" style="width: 18%;padding-left:5px;">Nombre</div>
            <div class="indicator_today_name semi_bold" style="width: 30%;">Descripción</div>
            <div class="indicator_today_name semi_bold" style="width: 10%;">Imagen</div>
            <div class="indicator_today_name semi_bold" style="width: 30%;">Codigo</div>
        </div>
        <!--  -->
        <!-- List that contains the subscribers -->
        <asp:UpdatePanel runat="server" ID="upCliente">
            <ContentTemplate>
                <div id="row_list" class="subscriber_result">
                    <asp:Repeater runat="server" ID="rptRegistrados">
                        <HeaderTemplate>
                            <ul class="subscriber_result_today regular">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li>
                                <div class="subscriber_email_address" style="width: 10%;float:left;padding-left:5px;" >
                                    <div>
                                        <asp:Label runat="server" ID="Label1"><%# Eval("Nombre") %></asp:Label><asp:Image runat="server" />
                                    </div>
                                </div>
                                <div class="subscriber_email_address" style="width: 30%;">
                                    <div class="subscriber_email_original done">
                                        <asp:Label runat="server" ID="lblEmail"><%# Eval("Descripcion") %></asp:Label><asp:Image runat="server" />
                                    </div>
                                </div>
                                <div class="subscriber_email_address" style="width:10%;">
                                    <div>
                                         <asp:Image runat="server" ID="imgTetris" ImageUrl='<%# Eval("Imagen") %>'/>                                        
                                    </div>
                                </div>
                                <div class="subscriber_email_address" style="width: 30%;">
                                    <asp:Label runat="server" ID="lblNombre"><%# Eval("Codigo") %></asp:Label>
                                </div>
                                <div class="actions semi_bold">
                                    <asp:LinkButton runat="server" ID="btnInteres" CssClass="analytics" OnClick="btnInteres_Click" CommandArgument='<%# Eval("Id") %>'>Intereses</asp:LinkButton>
                                </div>
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>

                </div>
                <ucPag:ucPaginacion ID="ucPagination" runat="server" contenedorClass="container" RowPaginasClass="pagination" PaginaClass="active" OnClick="ucPagination_Click" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkRegistroAll" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div id="popupOverlay" style="opacity: 1; transition: all 0.46s ease; display: none;">
        <div id="popup" style="opacity: 1; transition: all 0.46s ease; transform: scale(1) translateY(-50%);">
            <asp:Button runat="server" ID="btnGuardar" OnClick="btnGuardar_Click" CssClass="btnTrue semi_bold" Style="left: 0px; width: 50%;" Text="Aceptar" />
            <input type="button" value="Cancelar" class="btnFalse semi_bold" style="right: 0px; width: 50%;" onclick="closePopup();"><div id="sub_data_info" class="bold">
                <img src="<%: ResolveUrl("~/Content/img/icons/default.png") %>">Agregar Membresia
            </div>
            <asp:UpdatePanel runat="server" ID="upMpdal">
                <ContentTemplate>
                    <div id="mainWrapper2">
                        <div id="box_row_titles">
                            <div id="row_titles2">
                                <div id="row_picture"></div>
                                <div id="row_name" class="col-name">Nombre</div>
                                <div class="col-name">Descripción</div>
                            </div>
                        </div>
                        <div id="">
                                <asp:HiddenField runat="server" id="hdId"></asp:HiddenField>
                           
                                <h4 class="semi_bold">Título <span class="errorSenderName"></span></h4>
                                <asp:TextBox runat="server" ID="txtTitulo" placeholder="Título"></asp:TextBox>
                                <h4 class="semi_bold">Descripción <span class="errorSenderName"></span></h4>
                                <asp:TextBox runat="server" ID="txtDescripcion" placeholder="Descripción"></asp:TextBox>
                                <h4 class="semi_bold">Imagen <span class="errorSenderName"></span></h4>
                                <asp:Image runat="server" ID="imgTetris" />                                
                                <h4 class="semi_bold">Codigo <span class="errorSenderName"></span></h4>
                                <asp:TextBox runat="server" ID="txtCodigo_" placeholder="{[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]}" Text="{[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]}"></asp:TextBox>
             
                        </div>
                        <!--<ul id="edit-urls-images" class="regular hidden" style="display: block;">
                            <asp:Repeater runat="server" ID="rptItems" OnItemDataBound="rptItems_ItemDataBound">
                                <ItemTemplate>
                                    <li>
                                        <div class="row_picture">
                                            <label>
                                                <input type="checkbox"><asp:LinkButton runat="server" class="seleccionar" OnClick="chkInteres_Click" ID="chkInteres" CommandArgument='<%# Eval("TipoInteresID") %>'></asp:LinkButton>
                                            </label>
                                        </div>
                                        <div class="row_name"><%# Eval("Nombre") %></div>
                                        <div><%# Eval("Descripcion") %></div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>-->
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" />
                    <asp:AsyncPostBackTrigger ControlID="rptItems" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="closePopup"></div>
        </div>
    </div>
</asp:Content>

<asp:Content runat="server" ID="ScriptsJS" ContentPlaceHolderID="ScriptsPages">
    <script src="<%= ResolveClientUrl("~/Scripts/js/easing.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/functions.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/chartjs/waypoints.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/custom.js") %>" type="text/javascript"></script>
    <script>
        $(document).on('mouseenter', '.crown', function () {
            opens = $(this).closest('li').attr('data-opens');
            $(this).text(opens + ' visitas');
        }).on('mouseleave', '.crown', function () {
            opens = $(this).closest('li').attr('data-opens');
            $(this).text(opens);
        });

        $('ul li a').click(function () {
            $('ul li.activedTab').removeClass('activedTab');
            $(this).closest('li').addClass('activedTab');
            console.log("------");
            console.log($(this).parent());
        });

        $(document).ready(function () {


            $('ul li a').on('click', function () {
                $(this).parent().addClass('activedTab').siblings().removeClass('activedTab');
            });
        });

        //$(document).on("click", ".seleccionar", function (e) {
        //    $(this).toggleClass("checked");
        //    e.preventDefault();
        //    e.stopImmediatePropagation();
        //});

        $(document).on('click', '.back_btn', function () {
            $(location).attr('href', '../default.aspx');
        });

        $(document).on("click", ".btnFalse", function (e) {
            $("#popupOverlay").hide();
            ListadoVacio();
        });
    </script>
</asp:Content>
