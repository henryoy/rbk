<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCatalogo.ascx.cs" Inherits="Controles_ucCatalogo" %>
<%@ Register TagName="ucPag" TagPrefix="pag" Src="~/Controles/ucPaginacion.ascx" %>
<%--<div class="modalCatalogo modal fade" runat="server" id="mCatalogo">
    <div class="modal-dialog modal-lg" role="document">
        <asp:UpdatePanel runat="server" ID="upMCliente" class="modal-content">
            <ContentTemplate>
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" runat="server" id="tituloModal">Clientes</h4>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>--%>

<div runat="server" id="popupOverlay" style="opacity: 1; transition: all 0.46s ease; display: none;">
    <div id="popup" style="opacity: 1; transition: all 0.46s ease; transform: scale(1) translateY(-50%);">
        <asp:Button runat="server" ID="btnGuardar" CssClass="btnTrue semi_bold" Style="left: 0px; width: 50%;" Text="Aceptar" CausesValidation="true" ValidationGroup="guardar" UseSubmitBehavior="false" />
        <input type="button" value="Cancelar" class="btnFalse semi_bold" style="right: 0px; width: 50%;" onclick="closePopup();"><div id="sub_data_info" class="bold">
            <img src="/Images/default.png">
            <h4 class="modal-title" runat="server" id="tituloModal">Campañas</h4>
        </div>
        <ul class="data_change clear-fix">
            <div class="contenedor-tabla">
                <div class="contenedor-tabla-head">
                    <div class="row">
                        <div class="col-sm-5">
                            <div class="input-group">
                                <asp:TextBox runat="server" ID="txtBusqueda" CssClass="form-control input-sm"></asp:TextBox>
                                <asp:LinkButton runat="server" ID="btnBuscar" OnClick="btnBuscar_Click" CssClass="input-group-addon">
                                            <i class="fa fa-search"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-sm-5" runat="server" id="dvBusqueda" visible="false">
                            <div class="input-group">
                                <asp:DropDownList runat="server" ID="ddlBusqueda" OnSelectedIndexChanged="ddlBusqueda_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:UpdatePanel runat="server" ID="upModal">
                <ContentTemplate>
                    <div class="tabla-principal" style="overflow-x: hidden !important;">
                        <asp:GridView runat="server" ID="grvClientes" GridLines="None" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true"
                            OnRowDataBound="grvClientes_RowDataBound" OnSelectedIndexChanging="grvClientes_SelectedIndexChanging"
                            CssClass="table table-striped table-bordered table-hover" DataKeyNames="ClienteID">
                            <Columns>
                                <asp:BoundField DataField="CampanaId" HeaderText="Clave" HeaderStyle-CssClass="text-center" />
                                <asp:BoundField DataField="Nombre" HeaderText=" Nombre" HeaderStyle-CssClass="text-center" />
                                <asp:BoundField DataField="TipoCampana" HeaderText="Tipo" HeaderStyle-CssClass="text-center" />
                                <asp:BoundField DataField="DestinoCampana" HeaderText="Destino" HeaderStyle-CssClass="text-center" />
                                <asp:BoundField DataField="MRCampanaId" HeaderText="ID MR" HeaderStyle-CssClass="text-center" />
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="alert alert-danger" role="alert">
                                    <p class="h4 text-center">¡No se encontraron registros!</p>
                                </div>
                            </EmptyDataTemplate>
                            <EmptyDataRowStyle CssClass="tablaVacia" />
                        </asp:GridView>
                    </div>
                    <pag:ucPag runat="server" ID="ucPagination" OnClick="ucPagination_Click" contenedorClass="pagination-tabla" RowPaginasClass="pagination" PaginaClass="active" />

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" />
                </Triggers>
            </asp:UpdatePanel>
        </ul>
        <div class="closePopup"></div>
    </div>
</div>

