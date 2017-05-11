<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Distribuciones.aspx.cs" Inherits="Dashboard_Distribuciones" %>

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
                        <asp:LinkButton runat="server" ID="add_subscriber" CssClass="semi_bold agregar" Style="top: -7px" ClientIDMode="Static" OnClick="add_subscriber_Click"></asp:LinkButton>
                    </div>
                </div>
                <ul id="edit-urls-images" class="regular hidden" style="display: block;">
                    <asp:Repeater runat="server" ID="rptItems">
                        <ItemTemplate>
                            <li data-id="<%# Eval("DistribucionID") %>" data-url="#" data-token="IzE0MzYwOTEyMzAzMzA=" data-value="San Francisco">
                                <div class="row_name"><%# Eval("Nombre") %></div>
                                <div><%# Eval("Descripcion") %></div>
                                <div class="actions semi_bold">
                                    <asp:LinkButton runat="server" ID="btnEditar" CssClass="analytics" OnClick="btnEditar_Click" CommandArgument='<%# Eval("DistribucionID") %>'>Editar</asp:LinkButton>
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
</asp:Content>
<asp:Content ID="scriptJS" ContentPlaceHolderID="ScriptsPages" runat="Server">
    <script src="<%= ResolveClientUrl("~/Scripts/js/jquery.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/easing.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/jquery.gravatar.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/functions.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/custom.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/chartjs/waypoints.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/Scripts/js/autoNumeric-min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/Scripts/js/funciones-generales.js") %>" type="text/javascript"></script>
</asp:Content>

