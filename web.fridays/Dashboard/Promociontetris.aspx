﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="Promociontetris.aspx.cs" Inherits="Dashboard_Promociontetris" EnableEventValidation="false" Async="true" %>

<asp:Content runat="server" ID="Css" ContentPlaceHolderID="HeadContent">
    <link rel="stylesheet" type="text/css" href="../Content/css/jquery-ui.css" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveClientUrl("~/Content/classicTheme/style.css") %>" media="screen">
    <link href="<%= ResolveClientUrl("~/Content/css/Distribucion.css") %>" rel="stylesheet" />
    <%--<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/css/bootstrap.min.css" />--%>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-material-design/0.5.10/css/bootstrap-material-design.min.css" />


    <link href='http://fonts.googleapis.com/css?family=Roboto:400,500' rel='stylesheet' type='text/css'>
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
    <link href="<%= ResolveClientUrl("~/Content/plugin/css/bootstrap-material-datetimepicker.css") %>" rel="stylesheet" />
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <style>
        .generateChecklist {
            width: 100%;
            height: 80px;
            background-color: #69c0af;
            color: #FFF;
            margin-bottom: 40px;
        }


        .ui-icon-cenis-calendar {
            background-image: url(../images/icon/2424_calendar.png);
        }

        .ui-icon-cenis {
            width: 24px;
            height: 24px;
        }

        .ui-icon-cenis {
            margin-top: 35%;
            display: block;
            text-indent: -99999px;
            overflow: hidden;
            background-repeat: no-repeat;
        }

        textarea {
            box-shadow: 0px 0px 0px 1px #d8d8d8;
            padding: 0 40px 0 10px;
            width: 100%;
            box-sizing: border-box;
            -moz-box-sizing: border-box;
            line-height: 20px;
            color: #23282d;
            font-size: 13px;
            margin-bottom: 29px;
            border: 0px;
            resize: none;
        }


        body {
            background-color: #757575 !important;
        }

        label {
            width: 100%;
            display: block;
            clear: both;
            margin-bottom: 35px;
            float: none;
            position: relative;
        }

        select {
            box-shadow: 0px 0px 0px 1px #d8d8d8;
            height: 45px;
            padding: 0 53px 0 10px;
            width: 100%;
            box-sizing: border-box;
            -moz-box-sizing: border-box;
            line-height: 20px;
            color: #23282d;
            font-size: 13px;
            border: 0px;
            -webkit-appearance: none;
            -moz-appearance: none;
            appearance: none;
            cursor: pointer;
            z-index: 99;
            display: block;
            width: 100%;
            clear: both;
            border-radius: 0px;
            position: relative;
            font-family: Helvetica, Arial, "Lucida Grande", sans-serif !important;
        }

            select.tipopromo {
                box-shadow: 0px 0px 0px 1px #d8d8d8;
                height: 45px;
                color: #23282d;
                font-size: 12px;
                border: 0px;
                cursor: pointer;
                z-index: 99;
                display: block;
                width: 100%;
                clear: both;
                border-radius: 0px;
                position: relative;
                font-family: Helvetica, Arial, "Lucida Grande", sans-serif !important;
            }

            select:focus {
                outline: none;
            }

        .filter_item {
            box-shadow: 0px 0px 0px 1px #d8d8d8;
            height: 45px;
            padding: 0 53px 0 10px;
            width: 100%;
            box-sizing: border-box;
            -moz-box-sizing: border-box;
            line-height: 45px;
            color: #23282d;
            font-size: 13px;
            border: 0px;
            -webkit-appearance: none;
            -moz-appearance: none;
            appearance: none;
            cursor: pointer;
            z-index: 99;
            display: block;
            width: 100%;
            clear: both;
            border-radius: 0px;
            position: relative;
            font-family: Helvetica, Arial, "Lucida Grande", sans-serif !important;
            margin-bottom: 35px;
        }

        .selection_item {
            box-shadow: 1px 0 0 rgba(0,0,0,0.12), 0 1px 0 rgba(0,0,0,0.12), -1px 0 0 rgba(0,0,0,0.12);
            height: 45px;
            padding: 0 53px 0 10px;
            width: 100%;
            box-sizing: border-box;
            -moz-box-sizing: border-box;
            line-height: 45px;
            color: #23282d;
            font-size: 13px;
            border: 0px;
            -webkit-appearance: none;
            -moz-appearance: none;
            appearance: none;
            cursor: default;
            z-index: 99;
            display: block;
            width: 100%;
            clear: both;
            border-radius: 0px;
            position: relative;
            font-family: Helvetica, Arial, "Lucida Grande", sans-serif !important;
        }

        #selection_form div:first-of-type {
            box-shadow: 0px 0px 0px 1px rgba(0,0,0,0.12);
        }

        #selection_form .selected {
            background-color: #70cce5;
            color: #FFF;
            font-weight: 600;
            box-shadow: 1px 0 0 #70cce5, 0 1px 0 #70cce5, -1px 0 0 #70cce5;
        }

        #filter_form .selected {
            box-shadow: 0 0 0 2px #beddfe;
        }

        #send_form .selected {
            box-shadow: 0 0 0 2px #beddfe !important;
        }

        #selection_form .selected:first-of-type {
            box-shadow: 0px 0px 0px 1px #70cce5;
        }

        #list_name_bar h2 {
            margin-left: 30px;
        }

        .filter_amount {
            position: absolute;
            right: 12px;
            top: 13px;
            border-radius: 100px;
            font-size: 11px;
            color: #FFF;
            background-color: #70cce5;
            padding: 4px 8px 3px 8px;
            line-height: 11px;
            z-index: 103;
            font-weight: 600;
        }

            .filter_amount:empty {
                display: none;
            }

        #filter_sidebar h5 {
            font-size: 13px;
            text-transform: uppercase;
            color: #5a5a5a;
        }

        /*#send_view li {
            box-shadow: 1px 0 0 rgba(0,0,0,0.12), 0 1px 0 rgba(0,0,0,0.12), -1px 0 0 rgba(0,0,0,0.12);
            height: 45px;
            padding: 0 53px 0 10px;
            width: 100%;
            box-sizing: border-box;
            -moz-box-sizing: border-box;
            line-height: 20px;
            color: #23282d;
            font-size: 13px;
            border: 0px;
            cursor: pointer;
            z-index: 99;
            display: block;
            width: 100%;
            clear: both;
            border-radius: 0px;
            position: relative;
            font-family: Helvetica, Arial, "Lucida Grande", sans-serif !important;
            line-height: 45px;
        }*/

        .edit_lists_btn {
            position: absolute;
            right: 0px;
            top: 0px;
            cursor: pointer;
            display: none;
        }

        .removeListBtn {
            position: absolute;
            left: 12px;
            top: 15px;
            background-color: #ff5656;
            width: 16px;
            height: 16px;
            color: #FFF;
            border-radius: 16px;
            text-align: center;
            line-height: 16px;
            font-size: 13px;
            font-weight: bold;
            z-index: 102;
        }

        #generateChecklist {
            width: 100%;
            height: 80px;
            background-color: #69c0af;
            position: absolute;
            bottom: 0px;
            left: 0px;
            color: #FFF;
            font-size: 14px;
            text-transform: uppercase;
            box-sizing: border-box;
            -moz-box-sizing: border-box;
            cursor: pointer;
            background-position: 30px center;
            background-repeat: no-repeat;
        }

            #generateChecklist:hover {
                background-color: #5caa9a;
            }

        .generateChecklistName {
            line-height: 80px;
            text-align: center;
            font-size: 17px;
            color: #fff;
            margin-left: 25%;
        }

        .scheduleCampaignWrapper {
            position: relative;
            display: none;
            margin-bottom: 5%;
        }

        .scheduleCampaignWrapperLeft {
            float: left;
            width: 70%;
            padding-right: 25px;
            box-sizing: border-box;
            -moz-box-sizing: border-box;
            height: 45px;
        }

        .scheduleCampaignWrapperRight {
            float: left;
            width: 30%;
            height: 45px;
        }

        .send_schedule_campaign_day {
            width: 100% !important;
            cursor: pointer;
        }

        #datepicker, #datepicker_init {
            -webkit-transition: all 0.3s ease;
            z-index: 9999999999;
            position: relative;
            margin-left: -1px;
            display: none;
        }

        .ui-datepicker {
            width: 307px;
            padding: .2em .2em 0;
            display: none;
        }

        /*.uploadFile {
            position: absolute;
            right: 0px;
            top: 0px;
            background-color: #F7F7F7;
            border-radius: 0px;
            margin-top: -2px;
            box-shadow: 0 0 0 1px rgba(0,0,0,0.08);
        }

            .uploadFile [type="button"] {
                background-color: transparent;
                color: #8F8F8F;
                text-transform: uppercase;
                margin: 0px;
                padding: 8px 8px 6px 8px;
                cursor: pointer;
            }

        #embed_form_notification {
            font-size: 12px;
            text-transform: uppercase;
            color: #8e8e8e;
            padding-top: 50px;
            margin-top: 40px;
            background-image: url(../img/icons/send_form_icon.png);
            background-repeat: no-repeat;
            background-position: center top;
            text-align: center;
            line-height: 18px;
            cursor: pointer;
        }*/

        #selection_form h4 {
            height: 14px;
        }

        .clear_selection_states {
            position: absolute;
            right: 0px;
            top: 0px;
            text-align: right;
            box-shadow: none !important;
            display: none;
            cursor: pointer;
        }

        h4 span {
            position: absolute;
            right: 0px;
            top: 0px;
            color: #c06969;
            display: none;
        }

        .listResult {
            padding-bottom: 0px;
            cursor: default;
            position: relative;
        }

        #send_schedule_campaign_time {
            text-transform: uppercase;
        }

        /*.filter_button {
            position: absolute;
            right: 0px;
            top: 0px;
            cursor: pointer;
            font-size: 13px;
            color: #70cce5;
        }*/

        .viewList {
            right: 0px;
            top: 0px;
            height: 100%;
            width: 32px;
        }


        /* AJAX UPLOAD */
        .ax-clear,
        .ax-browse-c,
        .ax-main-title,
        .ax-upload-all,
        .ax-upload.ax-button {
            display: none !important;
        }

        .ax-remove {
            margin-top: -10px;
            width: 25px;
            height: 25px;
            background-image: url(../images/delete2.jpg);
            background-repeat: no-repeat;
            background-position-x: center;
            background-position-y: center;
            border: none;
        }

        .imgTarjeta:hover {
            opacity: 0.5;
            cursor: pointer;
        }

        input.color-picker {
            width: 40px !important;
            height: 40px !important;
            padding: 0px;
            border: none !important;
        }

        .color-picker:hover {
            cursor: pointer;
        }

        .add-option {
            margin: 0 0 10px 0;
        }

        #send_sidebar {
            width: 40%;
            height: 100%;
            /* background-color: #FFF; */
            box-sizing: border-box;
            -moz-box-sizing: border-box;
            /* float: left; */
            /* overflow: auto; */
            padding-bottom: 80px;
            z-index: 2;
            position: relative;
            box-shadow: 1px 0 0 rgba(0,0,0,0.08);
            z-index: 3;
        }

        #filter_sidebar {
            width: 50%;
            /* height: 100%; */
            background-color: #FFF;
            box-sizing: border-box;
            -moz-box-sizing: border-box;
            overflow: auto;
            padding-bottom: 50px;
            padding-left: 0px;
            position: absolute;
            left: -41px;
            z-index: 2;
            box-shadow: 1px 0 0 rgba(0,0,0,0.08);
        }
    </style>
    <asp:UpdatePanel runat="server" ID="upPromocion" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="mainWrapper" style="padding-top: 26px;">
                <div id="send_view">
                    <div id="send_sidebar">
                        <!-- Form -->
                        <div id="send_form">

                            <h4 class="semi_bold">Título <span class="errorSenderName"></span></h4>
                            <asp:TextBox runat="server" ID="txtTitulo" placeholder="Título"></asp:TextBox>
                            <h4 class="semi_bold">Descripción <span class="errorSenderEmailAddress"></span></h4>
                            <asp:TextBox runat="server" ID="txtDescripcion" placeholder="Descripción"></asp:TextBox>
                            <h4 class="semi_bold">Términos y/o condiciones <span class="errorSenderEmailAddress"></span></h4>
                            <asp:TextBox runat="server" TextMode="MultiLine" Rows="6" Columns="40" ID="txtCondiciones" placeholder="..."></asp:TextBox>
                            <h4 class="semi_bold">Tipo tarjeta
							<span class="errorSenderSubscriberList"></span>
                            </h4>
                            <label id="tipoMembresia">
                                <asp:DropDownList class="subscriber_lists" runat="server" ID="dpTarjeta"></asp:DropDownList>
                            </label>
                            <h4 class="semi_bold">Tipo Membresia
							<span class="errorSenderSubscriberList"></span>
                            </h4>
                            <label id="tipoPromocion">
                                <asp:DropDownList class="subscriber_lists" runat="server" ID="dpTipoPromocion" OnSelectedIndexChanged="dpTipoPromocion_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="VISITA">VISITA</asp:ListItem>
                                    <asp:ListItem Value="EVENTO">EVENTO</asp:ListItem>
                                </asp:DropDownList>
                            </label>
                            <h4 class="semi_bold" style="padding-bottom: 18px!important; margin-top: 6px;">Configurar Fecha de promoción					
							<div runat="server" id="switch" class="switch" name="schedule_switch" style="right: -1px; top: -4px;">
                                <div runat="server" id="switch_thumb" class="switch_thumb" style="right: 2px;"></div>
                                <input runat="server" type="hidden" value="0" name="schedule" id="schedule_campaign">
                            </div>
                            </h4>
                            <div class="scheduleCampaignWrapper clear-fix" style="display: block;">
                                <h4 class="semi_bold">Fecha Inicio </h4>
                                <asp:TextBox runat="server" ID="txtFechaInicio" CssClass="floating-label" placeholder="Fecha Inicio" ViewStateMode="Enabled"></asp:TextBox>
                                <h4 class="semi_bold">Fecha Final</h4>
                                <asp:TextBox runat="server" ID="txtFechaFinal" CssClass="floating-label" placeholder="Fecha Final" ViewStateMode="Enabled"></asp:TextBox>
                            </div>
                            <%--                            <div class="scheduleCampaignWrapper clear-fix">
                                <div class="scheduleCampaignWrapperLeft">
                                    <asp:TextBox runat="server" ID="txtFechaInicio" CssClass="send_schedule_campaign_day_init" ViewStateMode="Enabled"></asp:TextBox>
                                </div>
                                <div class="scheduleCampaignWrapperRight">
                                    <asp:TextBox runat="server" ID="txtTimeInicio" CssClass="send_schedule_campaign_time" />
                                </div>
                            </div>
                            <div id="datepicker_init" class="ll-skin-melon clear-fix"></div>
                            <div class="scheduleCampaignWrapper clear-fix">
                                <div class="scheduleCampaignWrapperLeft">
                                    <asp:TextBox runat="server" ID="txtFechaFinal" CssClass="send_schedule_campaign_day" ViewStateMode="Enabled"></asp:TextBox>
                                </div>
                                <div class="scheduleCampaignWrapperRight">
                                    <asp:TextBox runat="server" ID="txtTimeFinal" CssClass="send_schedule_campaign_time" />
                                </div>
                            </div>
                            <!---->
                            <div id="datepicker" class="ll-skin-melon clear-fix"></div>--%>
                        </div>
                        <div id="generateChecklist" class="semi_bold">
                            <asp:LinkButton runat="server" ID="lnkGuardarPromocion" CssClass="btnTrue generateChecklistName" OnClick="lnkGuardarPromocion_Click">Guardar promoción</asp:LinkButton>
                            <%-- <div class="generateChecklistName">
                            Guardar promoción
                        </div>--%>

                            <!-- test -->

                        </div>
                    </div>
                    <div id="filter_sidebar" class="expanded" style="left: 50%;">
                        <div id="filter_form">
                            <br />
                            <h4 class="semi_bold" runat="server" id="lblValor1">Valor 1</h4>
                            <asp:TextBox runat="server" ID="txtValor1" CssClass="entero"></asp:TextBox>

                            <h4 class="semi_bold" runat="server" id="lblValor2">Valor 2</h4>
                            <asp:TextBox runat="server" ID="txtValor2"></asp:TextBox>
                            <h4 class="semi_bold" runat="server">Imagen</h4>
                            <asp:Image runat="server" ID="imgTarjeta" CssClass="imgTarjeta" ToolTip="Click para seleccionar imagen" Height="36" Width="62" ImageUrl="~/Images/icon-gallery.svg" />
                            <asp:HiddenField runat="server" ID="hfTajeta" ClientIDMode="Static" Value="" />
                            <%--<asp:LinkButton runat="server" ID="btnSave" CssClass="btn-save" OnClick="lnkGuardarPromocion_Click" CausesValidation="true" ValidationGroup="guardar" UseSubmitBehavior="false" Style="display: none"></asp:LinkButton>--%>
                            <div id="uploader_div">
                            </div>
                            <asp:UpdatePanel runat="server" ID="upSucursal" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <h4 class="semi_bold">Sucursales </h4>
                                    <label id="subscriber_lists_ul">
                                        <asp:DropDownList class="subscriber_lists" runat="server" ID="dpSucursales" OnSelectedIndexChanged="dpSucursales_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </label>
                                    <asp:ListBox ID="lBSucursal" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                    <br />
                                    <asp:Button Text="Eliminar sucursal" runat="server" ID="btnEliminarSucursal" CssClass="generateChecklist" OnClick="btnEliminarSucursal_Click" />
                                    <div class="fila" style="background: #fff; height: 30px; padding: 0px;">
                                        <span style="display: table-cell; vertical-align: middle;" class="sub_title">Importes</span>
                                        <asp:Button runat="server" ID="btnAddDetalle" CssClass="add-option semi_bold" OnClick="btnAddDetalle_Click" Text="Agregar Importes" UseSubmitBehavior="false" />
                                        <%--<asp:Button runat="server" ID="btnResultado" CssClass="add-option semi_bold" OnClick="btnResultado_Click" Text="Probar" UseSubmitBehavior="false" Style="margin-right: 10px;" />--%>
                                    </div>
                                    <hr />
                                    <asp:GridView runat="server"
                                        ID="grvDetalle"
                                        GridLines="None"
                                        ShowHeaderWhenEmpty="true"
                                        AutoGenerateColumns="false"
                                        CssClass="table"
                                        OnRowDataBound="grvDetalle_RowDataBound"
                                        OnRowCancelingEdit="grvDetalle_RowCancelingEdit"
                                        OnRowEditing="grvDetalle_RowEditing"
                                        OnRowDeleting="grvDetalle_RowDeleting"
                                        OnRowUpdating="grvDetalle_RowUpdating">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Valor 1" HeaderStyle-CssClass="checkbox" ItemStyle-CssClass="checkbox">
                                                <ItemTemplate>
                                                    <%# Eval("Valor1") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtValor1" Text='<%# Eval("Valor1") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Valor 2" HeaderStyle-CssClass="col-data" ItemStyle-CssClass="col-data">
                                                <ItemTemplate>
                                                    <%# Eval("Valor2") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtValor2" Text='<%# Eval("Valor2") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cambio" HeaderStyle-CssClass="col-name" ItemStyle-CssClass="col-name">
                                                <ItemTemplate>
                                                    <%# Eval("Cambio") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList runat="server" ID="dpCatTetris">
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Figura" HeaderStyle-CssClass="col-name" ItemStyle-CssClass="col-name">
                                                <ItemTemplate>
                                                   <asp:Image runat="server" ID="imgTetris2" ImageUrl=' <%# Eval("Imagen") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Image runat="server" ID="imgTetris" ImageUrl=' <%# Eval("Imagen") %>' />
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
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content runat="server" ID="ScriptsJS" ContentPlaceHolderID="ScriptsPages">

    <script src="<%= ResolveClientUrl("~/Scripts/js/easing.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/jquery.color.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/jquery-ui.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/functions.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/custom.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/chartjs/waypoints.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/Scripts/js/autoNumeric-min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveUrl("~/Scripts/js/ajaxupload-min.js") %>" type="text/javascript"></script>
    <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-material-design/0.5.10/js/ripples.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-material-design/0.5.10/js/material.min.js"></script>
    <script type="text/javascript" src="https://rawgit.com/FezVrasta/bootstrap-material-design/master/dist/js/material.min.js"></script>
    <script type="text/javascript" src="http://momentjs.com/downloads/moment-with-locales.min.js"></script>
    <script src="<%= ResolveUrl("~/Content/plugin/js/bootstrap-material-datetimepicker.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {


            $('#<%=txtFechaFinal.ClientID%>').bootstrapMaterialDatePicker
                ({
                    weekStart: 0, format: 'DD/MM/YYYY HH:mm'
                });
            $('#<%=txtFechaInicio.ClientID%>').bootstrapMaterialDatePicker
                ({
                    weekStart: 0, format: 'DD/MM/YYYY HH:mm', shortTime: true
                }).on('change', function (e, date) {
                    $('#<%=txtFechaFinal.ClientID%>').bootstrapMaterialDatePicker('setMinDate', date);
                });

            $('#min-date').bootstrapMaterialDatePicker({ format: 'DD/MM/YYYY HH:mm', minDate: new Date() });

            $.material.init()
        });

        function pageLoad(sender, args) {



            $(document).ready(function () {

                $('.decimal').autoNumeric('init', {
                    aForm: false,
                    aDec: '.',
                    vMin: '-999999999.99',
                    vMax: '999999999.99'
                });

                $('.entero').autoNumeric('init', {
                    aForm: false,
                    vMin: '0',
                    vMax: '999999999'
                });

                var filePath = '<%= Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "" + ConfigurationManager.AppSettings["RutaImagenes"]%>';


                console.log(filePath);

                $('#uploader_div').ajaxupload({
                    url: '../upload.aspx',
                    maxFileSize: '1M',
                    maxFiles: 1,
                    resizeImage: {
                        maxWidth: 60,
                        maxHeight: 42,
                        quality: 0.5,
                        scaleMethod: undefined,
                        format: undefined,
                        removeExif: false
                    },
                    allowExt: ['jpg', 'jpeg', 'bmp', 'png'],
                    removeOnSuccess: true,
                    error: function (txt, obj) {
                        notification(txt, 'error');
                    },
                    onSelect: function (files) {
                        var name = filePath + files[0].name;
                        $("#hfTajeta").val(name);
                    },
                    finish: function (file) {
                        GudarDatos();
                    },
                    success: function (file_name) {
                        console.log("------success: --");
                        $(".imgTarjeta").attr("src", filePath + file_name);
                    }
                });

                $(document).on("click", ".imgTarjeta", function (e) {
                    $(".ax-browse").trigger("click");
                    e.preventDefault();
                    e.stopPropagation();
                    e.stopImmediatePropagation();
                });

                //$(document).on("click", ".btnTrue", function (e) {
                //    var Archivos = $('.ax-file-list li');
                //    var seleccionado = $("#hfTajeta").val();
                //    var procesado = $(".imgTarjeta").attr("src");

                //    console.log(seleccionado);
                //    console.log(procesado);

                //    if (seleccionado == procesado) {
                //        GudarDatos();
                //    }
                //    else if (Archivos.length == 0) {
                //        notification('Seleccione una imagen', 'error');
                //    }
                //    else {
                //        $(".ax-upload").click();
                //    }
                //    e.preventDefault();
                //    e.stopPropagation();
                //    e.stopImmediatePropagation();
                //});

                window.GudarDatos = function () {
                    console.log("test");
                    //document.getElementById("<%= lnkGuardarPromocion.ClientID %>").click();

                }
            });
        }
    </script>
    <script type="text/javascript">
        function ActiveCalendar() {

            the_switch = $('[name="schedule_switch"]');
            switch_thumb = $('[name="schedule_switch"]').find('.switch_thumb');

            $(the_switch).removeClass('disabled');
            $(switch_thumb).removeClass('active');
            $(switch_thumb).addClass('disabled');
            $(switch_thumb).css('right', '2px;')

            $(the_switch).addClass('active');
            if ($(switch_thumb).hasClass('active')) {

                $(switch_thumb).animate({ 'right': '2px' }, { duration: 100, easing: 'linear' });
                $(the_switch).removeClass('disabled');
                $(switch_thumb).removeClass('active');
            }
            else {

                $(switch_thumb).animate({ 'right': '19px' }, { duration: 100, easing: 'linear' });
                $(the_switch).addClass('disabled');
                $(switch_thumb).addClass('active');
            }
        }
        $(document).ready(function () {

            //vars
            window.t = undefined;
            i = 0;
            sentFlag = 0;
            user_given_date = '';
            user_given_time = '';
            user_current_time = '';
            user_current_date = '';
            testFlag = false;
            isMouseDown = false;


            showTime();

            setInterval(function () {

                showTime();

            }, 1000);

<%--            var dt = new Date();

            dateMonth = dt.getMonth();
            if (dt.getMonth() + 1 < 10) { dateMonth = '0' + (dt.getMonth() + 1) }

            clientDate = dt.getFullYear() + "-" + (dateMonth) + "-" + dt.getDate();

            $('#<%=txtTimeInicio.ClientID%>').val(clientDate)
            $('#<%=txtTimeFinal.ClientID%>').val(clientDate)--%>

            function showTime() {

                dt = new Date();
                h = dt.getHours(),
                    m = dt.getMinutes();

                console.log(dt);
                console.log(h);
                console.log(m);

                if (m < 10) { m = '0' + m; }

                _time = (h > 12) ? (h - 12 + ':' + m + ' PM') : (h + ':' + m + ' AM');

               <%-- $('#<%=txtTimeInicio.ClientID%>').attr('placeholder', _time);
                $('#<%=txtTimeFinal.ClientID%>').attr('placeholder', _time);--%>

            }

            $(document).on('mousedown', '.switch', function () {

                console.log("gjghjghghj-----");
                the_switch = $(this);
                switch_thumb = $(this).find('.switch_thumb');

                if ($(switch_thumb).hasClass('active')) {

                    $(switch_thumb).animate({ 'right': '2px' }, { duration: 100, easing: 'linear' });
                    $(the_switch).removeClass('disabled');
                    $(switch_thumb).removeClass('active');
                }
                else {

                    $(switch_thumb).animate({ 'right': '19px' }, { duration: 100, easing: 'linear' });
                    $(the_switch).addClass('disabled');
                    $(switch_thumb).addClass('active');
                }
            });

            $(document).on('mousedown', '[name="schedule_switch"]', function () {
                console.log("hgdhddgh");
                the_switch = $(this);
                switch_thumb = $(this).find('.switch_thumb');

                if ($(switch_thumb).hasClass('active')) {
                    $('[name="schedule"]').val('0');
                    $('#datepicker, .scheduleCampaignWrapper').hide();

                }

                else {
                    $('[name="schedule"]').val('1');
                    $('.scheduleCampaignWrapper').show();
                }
            });

            $(document).on('click', '.send_schedule_campaign_day', function (e) {
                e.stopPropagation();
                console.log("---->");

                $('#datepicker').show();
                $('#send_form .selected').removeClass('selected');
            });

            $(document).on('click', '.send_schedule_campaign_day_init', function (e) {
                e.stopPropagation();
                console.log("q---->");

                $('#datepicker_init').show();
                $('#send_form .selected').removeClass('selected');
            });


            $(document).on('click', 'body', function () {
                $('#datepicker').hide();
                $('#datepicker_init').hide();
            });


        });

        $(function () {

            $('#datepicker').datepicker({
                onSelect: function (date) {

                    $('.send_schedule_campaign_day').val(date);
                    $('#datepicker').hide();

                },
                selectWeek: true,
                dateFormat: "yy-mm-dd",
                inline: true,
                defaultDate: new Date()
            });

            $('#datepicker_init').datepicker({
                onSelect: function (date) {
                   <%-- //$("#<%=txtFechaInicio.ClientID%>").val(date);--%>
                    $('#datepicker_init').hide();

                },
                selectWeek: true,
                dateFormat: "yy-mm-dd",
                inline: true,
                defaultDate: new Date()
            });
        });

        function ActiveCalendar2() {

            the_switch = $('[name="schedule_switch"]');
            switch_thumb = $('[name="schedule_switch"]').find('.switch_thumb');

            $(the_switch).removeClass('disabled');
            $(switch_thumb).removeClass('active');
            $(switch_thumb).addClass('disabled');
            $(switch_thumb).css('right', '2px;')
            $(the_switch).addClass('active');
            console.log(the_switch);
            console.log(switch_thumb);

            the_switch = $(this);
            switch_thumb = $(this).find('.switch_thumb');

            if ($(switch_thumb).hasClass('active')) {

                $(switch_thumb).animate({ 'right': '2px' }, { duration: 100, easing: 'linear' });
                $(the_switch).removeClass('disabled');
                $(switch_thumb).removeClass('active');
            }
            else {

                $(switch_thumb).animate({ 'right': '19px' }, { duration: 100, easing: 'linear' });
                $(the_switch).addClass('disabled');
                $(switch_thumb).addClass('active');
            }
            the_switch = $(this);
            switch_thumb = $(this).find('.switch_thumb');

            if ($(switch_thumb).hasClass('active')) {
                $('[name="schedule"]').val('0');
                $('#datepicker, .scheduleCampaignWrapper').hide();

            }

            else {
                $('[name="schedule"]').val('1');
                $('.scheduleCampaignWrapper').show();
            }

            $(document).on('click', '.send_schedule_campaign_day_init', function (e) {
                e.stopPropagation();
                console.log("click --->");

                $('#datepicker_init').show();
                $('#send_form .selected').removeClass('selected');
            });


            $(document).on('click', 'body', function () {
                $('#datepicker').hide();
                $('#datepicker_init').hide();
            });
            //if ($(switch_thumb).hasClass('active')) {
            //    $('[name="schedule"]').val('0');
            //    $('#datepicker, .scheduleCampaignWrapper').hide();
            //    console.log("1132");
            //}
            //else {
            //    $('[name="schedule"]').val('1');
            //    $('.scheduleCampaignWrapper').show();
            //    console.log("11");
            //}
        }



    </script>
</asp:Content>


