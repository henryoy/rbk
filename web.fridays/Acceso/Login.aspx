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
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="upLoginForm">
            <ContentTemplate>
                <div class="popup-notification error font-semibold"></div>
                <!-- popup overlay -->
                <div id="popupoverlay" class="hidden" style="display: block; transition: all 0.5s ease; opacity: 1;">
                    <!-- popup-wrapper -->
                    <div id="popup-wrapper" style="width: 340px; height: 500px; transition: all 0.5s ease; opacity: 1; transform: scale(1) translateY(-50%);">
                        <!-- register popup information -->
                        <div id="login-popup-information">
                            <div class="login-popup-information-icon">
                                Register your<br>
                                <span class="font-semibold">FREE StampReady account</span>
                            </div>
                            <h5>Includes:</h5>
                            <!-- register features -->
                            <ul class="font-semibold">
                                <li>250 sending credits p/m</li>
                                <li>Contains all features</li>
                                <li>Premium plan first month free</li>
                                <li>Use the StampReady Editor</li>
                            </ul>
                        </div>
                        <!-- popup -->
                        <div id="popup">
                            <!-- logo  -->
                            <div id="logo-popup"></div>
                            <!-- login form -->
                            <div class="popup-form" data-popup-type="login">
                                <!-- optional paragraph -->
                                <p class="hidden" style="display: none;">Please, type in your new password with a minimum of 6 characters.</p>
                                <asp:TextBox runat="server" type="text" autocomplete="on" ID="txtEmail" placeholder="Email" CssClass="input-field font-regular input-username-email-login"></asp:TextBox>
                                <asp:TextBox runat="server" type="password" autocomplete="on" ID="txtPassword" placeholder="Contrase&ntilde;a" CssClass="input-field font-regular input-password-login"></asp:TextBox>
                                <input type="text" autocomplete="on" placeholder="Usuario" class="input-field font-regular input-username-register hidden" style="display: none;">
                                <input type="text" autocomplete="on" placeholder="Email" class="input-field font-regular input-email-register hidden" style="display: none;">
                                <input type="password" autocomplete="on" placeholder="Contrase&ntilde;a" class="input-field font-regular input-password-register hidden" style="display: none;">
                                <input type="password" placeholder="Password" class="input-field font-regular input-new-password hidden" style="display: none;">
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
