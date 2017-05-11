<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="RedimirPromocion.aspx.cs" Inherits="Dashboard_Notificacion_RedimirPromocion" %>
<asp:Content runat="server" ID="Css" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <style>
        
.submit-btn {
    width: 40%;
    height: 80px;
    line-height: 80px;
    position: absolute;
    right:0;
    color: #FFF;
    text-align: center;
    text-transform: uppercase;
    cursor: pointer;
    font-size: 16px;
}
    </style>    <div id="mainWrapper" class="credits-row-titles" style="padding-top: 26px;">
        <div id="plans-info" class="empty_campaigns credits-info">
            <div class="Vistaleft">
                <div id="calculatorWrapper">
                    <div class="calculator">
                        <h4 class="semi_bold">
                            <asp:TextBox runat="server" ID="txtFolio" CssClass="semi_bold" placeholder="Codigo Usuario"></asp:TextBox>
                        </h4>                        
                    </div>
                    <div class="calculator">
                        <h4 class="semi_bold">
                            <asp:TextBox runat="server" ID="txtPromocion" CssClass="semi_bold" placeholder="Identificador promoción"></asp:TextBox>
                        </h4>                        
                    </div>
                </div>               
            </div>
            <div class="Visitaright">
                <asp:LinkButton runat="server" ID="lnkActualizarNot" OnClick="lnkActualizarNot_Click" CssClass="submit-btn-vista brandBgColor brandBgColorHover font-semibold confirm-login" >Redimir Promoción</asp:LinkButton>
            </div>
        </div>

    </div>
    
    <div id="popupOverlay" style="opacity: 1; transition: all 0.46s ease; display: none;">
        <div id="popup" style="opacity: 1; transition: all 0.46s ease; transform: scale(1) translateY(-50%);">
            <asp:Button runat="server" ID="btnGuardar" OnClick="btnGuardar_Click" CssClass="btnTrue semi_bold" Style="left: 0px; width: 50%;" Text="Aceptar" Visible="false" />
            <input type="button" value="Cancelar" class="btnFalse semi_bold" style="right: 0px; width: 50%;" onclick="closePopup();"><div id="sub_data_info" class="bold">
                <img src="<%: ResolveUrl("~/Content/img/icons/default.png") %>">Redimir promoción
            </div>
            <asp:UpdatePanel runat="server" ID="upMpdal">
                <ContentTemplate>
                    <div runat="server" id="mainWrapper1" visible="false">
                        <ul class="data_change clear-fix">
                            <li class="clear-fix">
                                <div class="data_name semi_bold" style="background-image: url(../../img/icons/data_name_name@2x.png)">Nombre</div>
                                <div class="data_value">
                                    <asp:TextBox runat="server" ID="txtNombre" CssClass="regular"></asp:TextBox>
                                </div>
                            </li>
                            <li class="clear-fix">
                                <div class="data_name semi_bold" style="background-image: url(../../img/icons/data_name_email@2x.png)">Email</div>
                                <div class="data_value">
                                    <asp:TextBox runat="server" ID="txtEmail" CssClass="regular"></asp:TextBox>
                                </div>

                            </li>
                            <li class="clear-fix">
                                <div class="data_name semi_bold" style="background-image: url(../../img/icons/data_name_date@2x.png)">Fecha Nacimiento</div>
                                <div class="data_value">
                                    <asp:TextBox runat="server" ID="txtFechaNacimiento" CssClass="regular"></asp:TextBox>
                                </div>

                            </li>
                            <li class="clear-fix">
                                <div class="data_name semi_bold" style="background-image: url(../../img/icons/data_name_list@2x.png)">Número Visitas</div>
                                <div class="data_value">
                                    <asp:TextBox runat="server" ID="txtTarjeta" CssClass="regular"></asp:TextBox>
                                    <asp:Image runat="server" ID="imgTarjeta"/>
                                </div>
                            </li>
                            <li class="clear-fix">
                                <div class="data_name semi_bold" style="background-image: url(../../img/icons/data_name_list@2x.png)">Número Visitas</div>
                                <div class="data_value">
                                    <asp:TextBox runat="server" ID="txtVisita" CssClass="regular"></asp:TextBox>
                                </div>
                            </li>
                        </ul>
                        <%--<asp:TextBox runat="server" ID="txtReferencia" placeholder="Referencia" CssClass="regular"></asp:TextBox>--%>
                    </div>
                    <div runat="server" id="mainWrapper2" visible="false">
                        <p>No se logro redimir la promoción</p>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="closePopup"></div>
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
        $(document).on("click", ".btnFalse", function (e) {
            $("#popupOverlay").hide();
            ListadoVacio();
        });
    </script>
</asp:Content>

