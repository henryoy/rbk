<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Distribucion.aspx.cs" Inherits="Dashboard_Distribucion" EnableEventValidation="false" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="<%= ResolveClientUrl("~/Content/css/Distribucion.css") %>" rel="stylesheet" />
    <style>
        .generar_sucriptor{
            margin-right:10px;
        }
        .btn-guardar{margin-left:30px !important; border:1px solid red;}
    </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="fila" style="height: 30px; padding: 0px;">
        <asp:Button runat="server" ID="btnVaciarGroup" CssClass="add-option semi_bold  generar_sucriptor" OnClick="btnVaciarGroup_Click" Text="Generar grupo"/>
        
        <asp:Button runat="server" ID="btnGuardar" CssClass="add-option semi_bold btn-guardar" OnClick="btnGuardar_Click" Text="Guardar" CausesValidation="true" ValidationGroup="guardar" UseSubmitBehavior="false" />
    </div>
    <ul class="data_change clear-fix">
        <li class="clear-fix editar">
            <div class="data_name semi_bold" style="background-image: url(../images/icon/data_name_name@2x.png)">Nombre:</div>
            <div class="data_value">
                <asp:TextBox runat="server" ID="txtNombre" CssClass="regular goFocus" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvNombre" ControlToValidate="txtNombre" Display="Dynamic" ErrorMessage="*Requerido" SetFocusOnError="true" ValidationGroup="guardar" CssClass="Validators"></asp:RequiredFieldValidator>
            </div>
        </li>
        <li class="clear-fix editar">
            <div class="data_name semi_bold" style="background-image: url(../images/icon/data_name_custom1@2x.png)">Descripción:</div>
            <div class="data_value">
                <asp:TextBox runat="server" ID="txtDescripcion" CssClass="regular" MaxLength="250"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtDescripcion" Display="Dynamic" ErrorMessage="* Requerido" SetFocusOnError="true" ValidationGroup="guardar" CssClass="Validators"></asp:RequiredFieldValidator>
            </div>
        </li>
        <li class="clear-fix editar">
            <div class="data_name semi_bold" style="background-image: url(../images/icon/data_name_custom1@2x.png)">Tipo Membresia:</div>
            <div class="data_value">
                <asp:DropDownList runat="server" ID="cbxMembresia" CssClass="regular" Style="width: 80%; height: 25px; border: none;"></asp:DropDownList>
            </div>
        </li>
    </ul>
    <div class="fila" style="padding-top: 1px;">
        <asp:UpdatePanel runat="server" ID="upGrid">
            <ContentTemplate>
                <div class="fila" style="background: #fff;">
                    <span class="sub_title">Campos</span>
                </div>
                <hr />
                <asp:GridView runat="server" ID="grvCampos" GridLines="None" ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" DataKeyNames="Campo"
                    CssClass="table table-fixed" OnRowDataBound="grvCampos_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="checkbox" ItemStyle-CssClass="checkbox">
                            <HeaderTemplate>
                                <asp:CheckBox runat="server" Text="Todos" ID="chkAll" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkCampo" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" HeaderStyle-CssClass="col-name" ItemStyle-CssClass="col-name" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                    </Columns>
                    <EmptyDataTemplate>
                        <div role="alert" class="empty">
                            <p>¡No exite información para mostrar!</p>
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
                <div class="fila" style="background: #fff; height: 30px; padding: 0px;">
                    <span style="display: table-cell; vertical-align: middle;" class="sub_title">Condiciones</span>
                    <%--<div style="display: table-cell; vertical-align: middle;">
                        <label for="cbxmembresia">Tarjeta:</label>
                        <asp:DropDownList runat="server" ID="cbxMembresia" Style="width: 75%; height: 25px; border: none;"></asp:DropDownList>
                    </div>--%>
                    <asp:Button runat="server" ID="btnAddCondicion" CssClass="add-option semi_bold" OnClick="btnAddCondicion_Click" Text="Agregar" UseSubmitBehavior="false" />
                    <asp:Button runat="server" ID="btnResultado" CssClass="add-option semi_bold" OnClick="btnResultado_Click" Text="Probar" UseSubmitBehavior="false" Style="margin-right: 10px;" />
                </div>
                <hr />
                <asp:GridView runat="server" ID="grvCondicion" GridLines="None" ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false"
                    CssClass="table table-fixed"
                    OnRowDataBound="grvCondicion_RowDataBound"
                    OnRowCancelingEdit="grvCondicion_RowCancelingEdit"
                    OnRowEditing="grvCondicion_RowEditing"
                    OnRowUpdating="grvCondicion_RowUpdating">
                    <Columns>
                        <asp:TemplateField HeaderText="Nexo" HeaderStyle-CssClass="checkbox" ItemStyle-CssClass="checkbox">
                            <ItemTemplate>
                                <%# Eval("Nexo") %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList runat="server" ID="cbxUnion">
                                    <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem Value="Y">Y</asp:ListItem>
                                    <asp:ListItem Value="O">O</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Campo" HeaderStyle-CssClass="col-name" ItemStyle-CssClass="col-name">
                            <ItemTemplate>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList runat="server" ID="cbxCampo">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Operador" HeaderStyle-CssClass="checkbox" ItemStyle-CssClass="checkbox">
                            <ItemTemplate>
                                <%# Eval("Operador") %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList runat="server" ID="cbxOperador">
                                    <asp:ListItem Value="=">=</asp:ListItem>
                                    <asp:ListItem Value=">">></asp:ListItem>
                                    <asp:ListItem Value="<"><</asp:ListItem>
                                    <asp:ListItem Value=">=">>=</asp:ListItem>
                                    <asp:ListItem Value="<="><=</asp:ListItem>
                                    <asp:ListItem Value="!=">!=</asp:ListItem>
                                    <asp:ListItem Value="In">In</asp:ListItem>
                                    <asp:ListItem Value="Not in">Not in</asp:ListItem>
                                    <asp:ListItem Value="Like">Like</asp:ListItem>
                                    <asp:ListItem Value="En mes">En mes</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tipo" HeaderStyle-CssClass="col-data" ItemStyle-CssClass="col-data">
                            <ItemTemplate>
                                <%# Eval("Tipo") %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList runat="server" ID="cbxTipo">
                                    <asp:ListItem Value="Texto">Texto</asp:ListItem>
                                    <asp:ListItem Value="Entero">Entero</asp:ListItem>
                                    <asp:ListItem Value="Moneda">Moneda</asp:ListItem>
                                    <asp:ListItem Value="Fecha">Fecha</asp:ListItem>
                                    <asp:ListItem Value="Decimal">Decimal</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Valor" HeaderStyle-CssClass="col-name" ItemStyle-CssClass="col-name">
                            <ItemTemplate>
                                <%# Eval("Valor") %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" ID="txtValor" Text='<%# Eval("Valor") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="btnEliminar" OnClick="btnEliminar_Click">Eliminar</asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton runat="server" ID="btnAplicar" CommandName="Update">Aceptar</asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnCancelar" CommandName="Cancel">Cancelar</asp:LinkButton>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div role="alert" class="empty">
                            <p>¡No exite información para mostrar!</p>
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="grvCampos" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <!--popup-->
    <div id="popupOverlay" style="opacity: 1; transition: all 0.46s ease; display: none;">
        <div id="popup" style="opacity: 1; transition: all 0.46s ease; transform: scale(1) translateY(-50%);">
            <input type="button" value="Cerrar" class="btnFalse semi_bold" style="right: 0px; width: 50%;" onclick="closePopup();"><div id="sub_data_info" class="bold">
                <img src="<%: ResolveUrl("~/Content/img/icons/default.png") %>">Resultado
            </div>
            <asp:UpdatePanel runat="server" ID="upMpdal">
                <ContentTemplate>
                    <div id="overgrid" style="overflow: auto !important; height: 300px;">
                        <asp:GridView runat="server" ID="grvResultado" GridLines="None" ShowHeader="true" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" CssClass="table tb-result">
                            <Columns>
                            </Columns>
                            <EmptyDataTemplate>
                                <div role="alert" class="empty">
                                    <p>¡No exite información para mostrar!</p>
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="closePopup"></div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="ScriptsJS" ContentPlaceHolderID="ScriptsPages" runat="Server">
    <script src="<%= ResolveClientUrl("~/Scripts/js/jquery.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/easing.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/jquery.gravatar.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/functions.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/custom.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/chartjs/waypoints.min.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        Sys.Application.add_load(function () {
            $(document).ready(function (e) {

                $("#popup").css("height", $(window).height() - 200);
                $("#overgrid").css("height", $(window).height() - 200);

                $(document).on('click', '.back_btn', function () {
                    $(location).attr('href', '../Dashboard/Distribuciones');
                });
                $(document).on("click", ".btnFalse", function (e) {
                    $("#popupOverlay").hide();
                });
                window.ocultarHeader = function () {
                    var $rows = $("table.tb-result thead tr");
                    $rows.each(function (index) {
                        $(this).find("th").each(function () {
                            $(this).addClass("columna");
                            $(this).attr("title", $(this).text());
                        });
                    });
                }
            });
        });
    </script>
</asp:Content>
