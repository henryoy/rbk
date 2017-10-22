<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="ImagenTetris.aspx.cs" Inherits="Dashboard_ImagenTetris" %>

<asp:Content runat="server" ID="Css" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div id="mainWrapper">
        <h4 class="semi_bold">Título <span class="errorSenderName"></span></h4>
        <asp:TextBox runat="server" ID="txtTitulo" placeholder="Título"></asp:TextBox>
        <h4 class="semi_bold">Descripción <span class="errorSenderName"></span></h4>
        <asp:TextBox runat="server" ID="txtDescripcion" placeholder="Descripción"></asp:TextBox>
        <h4 class="semi_bold">Imagen <span class="errorSenderName"></span></h4>
        <asp:TextBox runat="server" ID="TextBox2" placeholder="Imagen"></asp:TextBox>
        <h4 class="semi_bold">Codigo <span class="errorSenderName"></span></h4>
        <asp:TextBox runat="server" ID="TextBox1" placeholder="{[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]}" Text="{[0,0,0,0],[0,0,0,0],[0,0,0,0],[0,0,0,0]}"></asp:TextBox>
    </div>
</asp:Content>

<asp:Content runat="server" ID="ScriptsJS" ContentPlaceHolderID="ScriptsPages">
    <script src="<%= ResolveClientUrl("~/Scripts/js/easing.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/functions.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/chartjs/waypoints.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/custom.js") %>" type="text/javascript"></script>

</asp:Content>
