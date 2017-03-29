<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="Clientes.aspx.cs" Inherits="Dashboard_Clientes" %>

<asp:Content runat="server" ID="Css" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <div id="list_view" class=" list_subs">
        <!-- Top Bar -->
        <div id="list_name_bar">
            <h2><b class="light cat">Clientes</b></h2>
            <!-- dropdown -->
            <div id="action" class="semi_bold">
            </div>
        </div>
        <!-- Main -->
        <div id="mainWrapper">
            <!-- Tabs -->
            <div id="tabs">
                <!-- Ul that hold the tabs -->
                <ul class="clear-fix semi_bold">
                    <!-- Today tab -->
                    <li>
                        <asp:LinkButton runat="server" ID="lnkRegistroHoy" OnClick="lnkRegistroHoy_Click">
                            Registros Hoy
                            <asp:Label runat="server" ID="lbRegistrosHoy" CssClass="tabs_today_active">0</asp:Label><%--<span class="tabs_today_active">0</span>--%>
                        </asp:LinkButton>
                    </li>
                    <!-- All tab -->
                    <li class="activedTab">
                        <asp:LinkButton runat="server" ID="lnkRegistroAll" OnClick="lnkRegistroAll_Click">
                            Todos
                        <asp:Label runat="server" ID="lblRegistros" CssClass="tabs_all_active bubble">0</asp:Label>
                        </asp:LinkButton>
                    </li>
                    <!-- VIP tab -->
                    <li>
                        <asp:LinkButton runat="server" ID="lnkRegistroVip" OnClick="lnkRegistroVip_Click">
                            VIP
                        <asp:Label runat="server" ID="lblRegistroVip" CssClass="tabs_vip_active ">0</asp:Label>
                        </asp:LinkButton>
                    </li>
                    <!-- Blocked tab -->
                    <%--<li>Bloqueados
                        <asp:Label runat="server" ID="lblBloqueado">0</asp:Label>
                    </li>--%>
                </ul>

            </div>
            <!-- Indicator -->
            <div id="indicator" class="indicator_today semi_bold">                
                <div class="indicator_today_email_address" style="width: 40%;">
                    <input type="text" runat="server" id="search_bar" placeholder="Buscar cliente" data-search="all">
                    <div id="search_bar_result" class="semi_bold hidden"></div>
                </div>
                <div class="indicator_today_name semi_bold" style="width: 20%;">Nombre</div>
                <div class="indicator_today_name semi_bold" style="width: 30%;">Fecha Registro</div>
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
                                <li data-opens="<%# Eval("VisitaGlobal") %>">
                                    <div class="subscriber_email_address" style="width: 40%;">
                                        <div class="crown semi_bold" title="<%# Eval("VisitaGlobal") %> Visitas"><%# Eval("VisitaGlobal") %></div>
                                        <div class="subscriber_email_original done">
                                            <asp:Label runat="server" ID="lblEmail"><%# Eval("Email") %></asp:Label><asp:Image runat="server" ImageUrl='<%#Eval("oTargeta.UrlImagen") %>' />
                                        </div>
                                    </div>
                                    <div class="subscriber_name" style="width: 20%;">
                                        <asp:Label runat="server" ID="lblNombre"><%# Eval("Nombre") %></asp:Label>
                                    </div>
                                    <div class="subscriber_country" style="width: 30%;">
                                        <asp:Label runat="server" ID="lblFechaRegistro"><%# Eval("FechaAlta","{0:d}") %></asp:Label>
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
                    <asp:AsyncPostBackTrigger ControlID="lnkRegistroHoy" />
                    <asp:AsyncPostBackTrigger ControlID="lnkRegistroAll" />
                    <asp:AsyncPostBackTrigger ControlID="lnkRegistroVip" />
                </Triggers>
            </asp:UpdatePanel>
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
    </script>
</asp:Content>
