<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1" />
    <title>Acceso - Fridays</title>
    <link href="~/Content/css/style.css" rel="stylesheet" />
</head>
<body style="background-color: rgb(117, 117, 117); overflow: hidden;">
    <style>
        #logo-popup {
            height: 210px !important;
        }
    </style>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="upLoginForm">
            <ContentTemplate>
                <div class="popup-notification error font-semibold"></div>
                <div id="popup-wrapper" style="width: 340px; height: 500px; transition: all 0.5s ease; opacity: 1; transform: scale(1) translateY(-50%);">
                    <div id="popup">
                        <!-- logo  -->
                        <div id="logo-popup"></div>
                        <!-- login form -->
                        <div class="popup-form" data-popup-type="login">
                            <!-- optional paragraph -->
                            <p class="hidden" style="display: none;">Please, type in your new password with a minimum of 6 characters.</p>
                            <asp:TextBox runat="server" type="text" autocomplete="on" ID="txtEmail" placeholder="Email" CssClass="input-field font-regular input-username-email-login"></asp:TextBox>
                            <asp:TextBox runat="server" type="password" autocomplete="on" ID="txtPassword" placeholder="Contrase&ntilde;a" CssClass="input-field font-regular input-password-login"></asp:TextBox>
                            <asp:DropDownList runat="server" ID="cbxSucursal" CssClass="input-field font-regular"></asp:DropDownList>
                            <!-- checkbox -->
                            <label class="keep-crisp">
                                <!-- keep me logged in -->
                                <div class="popup-checkbox"></div>
                                <span class="popup-checkbox-text keep-crisp">Mantenerme conectado</span>
                                <!-- forget password -->
                                <div class="forget-password keep-crisp">
                                    <span class="keep-crisp">Recuperar Contrase&ntilde;a</span>
                                </div>
                            </label>
                            <asp:LinkButton runat="server" ID="lnkAcceder" CssClass="submit-btn brandBgColor brandBgColorHover font-semibold confirm-login" OnClick="lnkAcceder_Click">Acceder al panel</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div runat="server" id="errorContent" class="form-group errorContent" visible="false">
                    <div runat="server" id="divmsj" class="alert alert-danger">
                        <ul id="errosmj" runat="server" clientidmode="Static"></ul>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <div class="bg-radial"></div>
    <script src="<%= ResolveClientUrl("~/Scripts/js/jquery.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/plugins/jquery.transform2d.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/plugins/jquery.transform3d.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/easing.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/custom.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/waypoints.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/api.js") %>" type="text/javascript"></script>
</body>
</html>
