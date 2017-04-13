<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="asignarventa.aspx.cs" Inherits="Dashboard_Notificacion_asignarventa" %>

<%--<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="membresia.aspx.cs" Inherits="membresia" %>--%>

<asp:Content runat="server" ID="Css" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div id="mainWrapper" class="credits-row-titles" style="padding-top: 26px;">
        <div id="plans-info" class="empty_campaigns credits-info">
            <div class="left">
                <h1>Enrique rosado: <b class="semi_bold" style="font-weight: normal;">Blanco</b></h1>
                <%--<p>You can use the calculator on the right in case you need more. <font class="extra-for-small">A <a href="../plans/index.php" class="semi_bold brandColor">Plan</a> will suit you better if you plan to send regularly.</font></p>--%>
            </div>
            <div class="right">
                <div class="arrow_credits"></div>
                <div id="calculatorWrapper">
                    <div class="calculator-icon">
                        <asp:Image runat="server" ID="imgTarjeta" ImageUrl="~/uploads/file.png" />
                    </div>
                    <div class="calculator">
                        <h4 class="semi_bold">
                            <%--<input type="text" placeholder="Amount?" id="define_amount_plan" maxlength="6" class="semi_bold numericOnly">--%>
                            <asp:TextBox runat="server" ID="txtFolio" CssClass="define_amount_plan semi_bold" placeholder="000"></asp:TextBox>
                        </h4>
                    </div>
                </div>
                <div class="clear-fix">&nbsp;</div>
            </div>
        </div>

    </div>
</asp:Content>
<asp:Content runat="server" ID="ScriptsJS" ContentPlaceHolderID="ScriptsPages">
    <script src="<%= ResolveClientUrl("~/Scripts/js/easing.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/jquery.gravatar.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/functions.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/custom.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/chartjs/waypoints.min.js") %>" type="text/javascript"></script>
    <script type="text/javascript">

    </script>
</asp:Content>
