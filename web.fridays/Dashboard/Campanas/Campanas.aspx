﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="Campanas.aspx.cs" Inherits="Dashboard_Campanas_Campanas" %>

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
                            <div id="row_name" class="semi_bold" style="margin-left: 80px;">Tipo Campaña</div>
                            <div id="row_date" class="semi_bold">Campaña destino </div>
                        </div>
                    </div>
                    <ul id="edit-urls-images" class="regular hidden" style="display: block;">
                </HeaderTemplate>
                <ItemTemplate>
                    <li data-id="<%# Eval("CampanaId") %>" data-url="<%= ResolveClientUrl("~/Dashboard/Campanas/Campana") %>" data-token="IzE0MzYwOTEyMzAzMzA=" data-value="Blanco">
                        <div class="row_picture">
                            <label>
                                <input type="checkbox"><a></a>
                                <asp:HiddenField runat="server" ID="PromocionId" Value='<%# Eval("CampanaId") %>' />
                            </label>
                        </div>
                        <div class="row_titulo">
                            <%# Eval("Nombre") %>
                        </div>
                        <div class="row_descripcion"><%# Eval("TipoCampana") %></div>
                        <div class="row_membresia"><%# Eval("DestinoCampana") %></div>
                        <div class="actions semi_bold">
                            <asp:LinkButton runat="server" ID="lnkVer" OnClick="lnkVer_Click" CssClass="send" CommandArgument='<%# Eval("CampanaId") %>'> VER </asp:LinkButton>                           
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
                     + '<a href="<%= ResolveClientUrl("~/Dashboard/Campanas/Campana.aspx") %>" class="semi_bold" style="padding: 18px 34px; font-size: 13px; margin: auto;">' +
                     'No existen campañas</a>' +
                     ' </div>');

            }
            else {
                $('#sent, #row_titles2, .select_all_checkboxes').show();
            }

        }

    </script>
</asp:Content>


