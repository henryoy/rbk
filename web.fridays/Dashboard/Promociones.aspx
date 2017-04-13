<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="Promociones.aspx.cs" Inherits="Dashboard_Promociones" %>

<asp:Content runat="server" ID="Css" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

        <div id="mainWrapper">
            <asp:Repeater ID="rptPromociones" runat="server">
                <HeaderTemplate>
                    <div id="box_row_titles">
                        <!-- Titles of the rows -->
                        <div id="row_titles2">
                            <div id="row_picture">#</div>
                            <div id="row_color">Titulo</div>
                            <div id="row_name" class="semi_bold">Descripcion</div>
                            <div id="row_date" class="semi_bold">Vigencia Inicial </div>
                            <div id="row_descuento" class="semi_bold">Vigencia Final </div>
                            <div id="row_tarjeta" class="semi_bold">Membresia </div>
                        </div>
                    </div>
                    <ul id="edit-urls-images" class="regular hidden" style="display: block;">
                </HeaderTemplate>
                <ItemTemplate>
                    <li data-id="<%# Eval("Promocionid") %>" data-url="<%= ResolveClientUrl("~/Dashboard/promocion") %>" data-token="IzE0MzYwOTEyMzAzMzA=" data-value="Blanco">
                        <div class="row_picture">
                            <label>
                                <input type="checkbox"><a></a>
                                <asp:HiddenField runat="server" ID="PromocionId" Value='<%# Eval("Promocionid") %>' />
                            </label>
                        </div>
                        <div class="row_titulo">
                            <%# Eval("Titulo") %>
                        </div>
                        <div class="row_descripcion"><%# Eval("Descripcion") %></div>
                        <div class="row_fecha"><%# Eval("Vigenciainicial","{0:d}") %></div>
                        <div class="row_fecha"><%# Eval("Vigenciafinal","{0:d}") %></div>
                        <div class="row_membresia"><%# Eval("Tipomembresia") %></div>
                        <div class="actions semi_bold">
                            <asp:LinkButton runat="server" ID="lnkVer" OnClick="lnkVer_Click" CssClass="send" CommandArgument='<%# Eval("Promocionid") %>'> VER </asp:LinkButton>                           
                        </div>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
             <ucPag:ucPaginacion ID="ucPagination" runat="server" contenedorClass="container" RowPaginasClass="pagination" PaginaClass="active"  OnClick="ucPagination_Click"/>
        </div>
    
</asp:Content>
<asp:Content runat="server" ID="ScriptsJS" ContentPlaceHolderID="ScriptsPages">
    <script src="<%= ResolveClientUrl("~/Scripts/js/easing.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/jquery.gravatar.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/functions.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/custom.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/chartjs/waypoints.min.js") %>" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            emptyCampaignCheck();
        });

        
        function emptyCampaignCheck() {
            $('#edit-urls-images, #row_titles2, .select_all_checkboxes').show();
        
            li_count = $('#edit-urls-images li').size();

            if (li_count < 1) {

                $('#edit-urls-images, #row_titles2, .select_all_checkboxes').hide();
                $('#mainWrapper').append('<div class="empty_campaigns regular" style="font-size: 34px; color: #4a4a4a">'
                     + '<a href="<%= ResolveClientUrl("~/Dashboard/Promocion.aspx") %>" class="semi_bold" style="padding: 18px 34px; font-size: 13px; margin: auto;">' +
                     'No existen promociones</a>' +
                     ' </div>');

            }
            else {
                $('#sent, #row_titles2, .select_all_checkboxes').show();
            }

        }

    </script>
</asp:Content>


