<%@ Page Title="Dashboard" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="index.aspx.cs" Inherits="Dashboard_index" %>

<%--<%@ Page Title="Causas" EnableEventValidation="false" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Causas.aspx.cs" Inherits="Configurar_Causas" %>--%>
<asp:Content runat="server" ID="Css" ContentPlaceHolderID="HeadContent">
    <!--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-material-design/0.5.10/css/bootstrap-material-design.min.css" />-->
    <link rel="stylesheet" href="<%= ResolveClientUrl("~/Content/plugin/css/fecha/bootstrap-material-design.min.css") %>" />
    <link href='http://fonts.googleapis.com/css?family=Roboto:400,500' rel='stylesheet' type='text/css'>
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
    <link href="<%= ResolveClientUrl("~/Content/plugin/css/bootstrap-material-datetimepicker.css") %>" rel="stylesheet" />
    <style>
        input {
            box-shadow: 0px 0px 0px 1px #d8d8d8;
            height: 45px;
            padding: 0 53px 0 10px;
            width: 20%;
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
            border-radius: 0px;
            position: relative;
            font-family: Helvetica, Arial, "Lucida Grande", sans-serif !important;
        }

        select {
            box-shadow: 0px 0px 0px 1px #d8d8d8;
            height: 45px;
            padding: 0 53px 0 10px;
            width: 30%;
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

        .send {
            height: 45px;
            float: right;
            background-color: #69c0af;
            line-height: 45px;
            color: #6a6a6a;
            font-size: 13px;
            text-transform: uppercase;
            color: #FFF;
            padding: 0 35px 0 35px;
            margin-left: 15px;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div id="" class="disable_selection">
        <div id="mainWrapper" class="analytics credits-row-titles" style="padding-top: 26px;">
            <div style="margin-bottom: 10px;">
                <asp:UpdatePanel runat="server" ID="upPanelSerach">
                    <ContentTemplate>
                        <asp:TextBox runat="server" ID="txtFechaInicio"></asp:TextBox>
                        <asp:TextBox runat="server" ID="txtFechaFinal"></asp:TextBox>
                        <%-- Iniciado pero por falta de tiempo no se concluye el control generico <asp:TextBox runat="server" ID="txtCampana"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="hdCampanaId" />
                        <asp:LinkButton runat="server" ID="lnkBuscar" CssClass="send" OnClick="lnkBuscar_Click">Buscar</asp:LinkButton>
                        --%>
                        <asp:DropDownList runat="server" ID="dpCampana">
                            <asp:ListItem>Selecciona Campaña</asp:ListItem>
                        </asp:DropDownList>
                        <asp:LinkButton runat="server" ID="lnkVerEstadistica" CssClass="send" OnClick="lnkVerEstadistica_Click">Ver Estadistica</asp:LinkButton>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
            <div class="analytics_reports_wrapper clear-fix">
                <!-- Line chart -->
                <div id="canvas_linechart_wrapper2">
                    <div id="canvas-holder1" style="padding-bottom: 0px;">
                       <%--<canvas id="myChart" width="800" height="200"></canvas>--%>
                    </div>
                </div>
                <%--<div id="canvas_linechart_wrapper2">
                    <div id="canvas-holder1" style="padding-bottom: 0px;">
                        <canvas id="chart1" width="100" height="100"></canvas>
                    </div>
                </div>--%>
                <div id="charts" class="clear-fix" style="padding-top: 20px;">
                    <div id="charts_left" style="width: 50%;">
                        <div id="charts_left_1">
                            <div id="chart_sent" class="round_chart">
                                <span>Expedido</span>
                            </div>
                        </div>
                        <div id="charts_left_2">
                            <div id="chart_delivered" class="round_chart">
                                <span>Entregado</span>
                            </div>
                        </div>
                    </div>
                    <div id="charts_right" style="width: 45%; float: right; padding-right: 10px">
                        <div class="chart_meter">
                            <h4 class="semi_bold"><font><font class="">devoluciones fuertes</font></font></h4>
                            <h3 class="regular chart_meter_hard_bounces semi_bold"><font><font>0</font></font></h3>
                            <div class="dashboard_loader">
                                <div class="dashboard_loader_filler hard_bounces" data-percentage="0" style="width: 0%;"></div>
                            </div>
                        </div>
                        <div class="chart_meter">
                            <h4 class="semi_bold"><font><font>Rebotes suaves</font></font></h4>
                            <h3 class="regular chart_meter_soft_bounces semi_bold"><font><font>0</font></font></h3>
                            <div class="dashboard_loader">
                                <div class="dashboard_loader_filler soft_bounces" data-percentage="0" style="width: 0%;"></div>
                            </div>
                        </div>
                        <div class="chart_meter">
                            <h4 class="semi_bold"><font><font>rechazos</font></font></h4>
                            <h3 class="regular chart_meter_rejects semi_bold"><font><font>0</font></font></h3>
                            <div class="dashboard_loader">
                                <div class="dashboard_loader_filler rejects" data-percentage="0" style="width: 0%;"></div>
                            </div>
                        </div>
                        <div class="chart_meter">
                            <h4 class="semi_bold"><font><font>Las quejas de spam</font></font></h4>
                            <h3 class="regular chart_meter_spam_complaints semi_bold"><font><font>0</font></font></h3>
                            <div class="dashboard_loader">
                                <div class="dashboard_loader_filler spam_complaints" data-percentage="0" style="width: 0%;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Subscriber and Unsubscriber charts -->
                <div id="subscribers_unsubscribers_charts" class="clear-fix">
                    <div id="subscribers_rate_wrapper" class="clear-fix">
                        <div class="rate_wrapper_item regular">
                            <font><font>Suscriptores</font></font>
                        </div>
                        <div class="rate_wrapper_result">
                            <span class="rate_wrapper_result_today semi_bold">
                                <font><font><asp:Label runat="server" ID="SuscriberHoy">0</asp:Label></font></font>
                                <h6 class="regular"><font><font>Hoy</font></font></h6>
                            </span>
                            <span class="rate_wrapper_result_month regular">
                                <font><font>&nbsp;/ <asp:Label runat="server" ID="SuscriberMes">0</asp:Label></font></font>
                                <h6><font><font>Mes</font></font></h6>
                            </span>
                        </div>
                    </div>
                    <div id="unsubscribers_rate_wrapper">
                        <div class="rate_wrapper_item regular">
                            <font><font>unsubscribers</font></font>
                        </div>
                        <div class="rate_wrapper_result">
                            <span class="rate_wrapper_result_today semi_bold">
                                <font><font>0 </font></font>
                                <h6 class="regular"><font><font>Hoy</font></font></h6>
                            </span>
                            <span class="rate_wrapper_result_month regular">
                                <font><font>&nbsp;/ 0 </font></font>
                                <h6><font><font>Mes</font></font></h6>
                            </span>
                        </div>
                    </div>
                </div>
                <!-- Dashboard Rates -->
                <div id="dashboard_rates">
                    <div class="chart_rate" id="chart_open_rate">
                        <div class="chart_rate_headline semi_bold">
                            <font><font>
								Rango abierto
								</font></font>
                            <div class="chart_rate_amount"><font><font>0</font></font></div>
                        </div>
                        <div class="chart_rate_percentage semi_bold"><font><font>0%</font></font></div>
                        <div class="dashboard_loader">
                            <div class="dashboard_loader_filler open_rate_filler" data-percentage="0" style="width: 0%;"></div>
                        </div>
                    </div>
                    <div class="chart_rate" id="chart_click_rate">
                        <div class="chart_rate_headline semi_bold">
                            <font><font>Tasa de clics</font></font>
                            <div class="chart_rate_amount">
                                <font><font>0</font></font>
                            </div>
                        </div>
                        <div class="chart_rate_percentage semi_bold">
                            <font><font>0%</font></font>
                        </div>
                        <div class="dashboard_loader">
                            <div class="dashboard_loader_filler click_rate_filler" data-percentage="0" style="width: 0%;"></div>
                        </div>
                    </div>
                    <div class="chart_rate" id="chart_deliverability_rate">
                        <div class="chart_rate_headline semi_bold">
                            <font><font>
								Tasa capacidad de entrega
								</font></font>
                            <div class="chart_rate_amount">
                                <font><font>0</font></font>
                            </div>
                        </div>
                        <div class="chart_rate_percentage semi_bold">
                            <font><font>0%</font></font>
                        </div>
                        <div class="dashboard_loader">
                            <div class="dashboard_loader_filler deliverability_rate_filler" data-percentage="0" style="width: 0%;"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <ucCat:ucCatalogo runat="server" ID="ucCatalogos" OnClick="ucCatalogo_Click" />
</asp:Content>
<asp:Content runat="server" ID="ScriptsJS" ContentPlaceHolderID="ScriptsPages">
    <script src="<%= ResolveClientUrl("~/Scripts/js/fecha/bootstrap.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/fecha/ripples.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/fecha/material.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/fecha/raw.material.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/fecha/moment-with-locales.min.js") %>" type="text/javascript"></script>
    <!-- <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-material-design/0.5.10/js/ripples.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-material-design/0.5.10/js/material.min.js"></script>
    <script type="text/javascript" src="https://rawgit.com/FezVrasta/bootstrap-material-design/master/dist/js/material.min.js"></script>
    <script type="text/javascript" src="http://momentjs.com/downloads/moment-with-locales.min.js"></script>-->
    <script src="<%= ResolveUrl("~/Content/plugin/js/bootstrap-material-datetimepicker.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/easing.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/jquery.gravatar.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/functions.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/chartjs/chart.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/circle-progress.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/custom.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/chartjs/waypoints.min.js") %>" type="text/javascript"></script>
    <%--<script src="<%= ResolveClientUrl("~/Scripts/js/dashboard.custom.js") %>" type="text/javascript"></script>--%>
    <script src="<%= ResolveClientUrl("~/Scripts/js/rbk.custom.3.js") %>" type="text/javascript"></script>
   
    <script>
        function ReloadStatistics(data) {
            //$(document).rbk('DestroyData');}
            $(document).rbk('Assing', {
                opensArray: JSON.stringify(data.opensArray),
                clicksArray: JSON.stringify(data.clicksArray),
                daysArray: JSON.stringify(data.daysArray),
                RStatistics: JSON.stringify(data.oResultStatistics)
            });
        }

        $(document).on('mouseenter', '#chart1', function () {
            setTimeout(function () {
                $('#chartjs-tooltip').show();
            }, 100)
        });
    </script>


    <script type="text/javascript">

        $(document).ready(function () {


            $('#<%=txtFechaFinal.ClientID%>').bootstrapMaterialDatePicker
                ({
                    weekStart: 0, time: false, format: 'YYYY-MM-DD'
                });
            $('#<%=txtFechaInicio.ClientID%>').bootstrapMaterialDatePicker
                ({
                    weekStart: 0, time: false, format: 'YYYY-MM-DD', shortTime: true
                }).on('change', function (e, date) {
                    $('#<%=txtFechaFinal.ClientID%>').bootstrapMaterialDatePicker('setMinDate', date);
                });

            $('#min-date').bootstrapMaterialDatePicker({ format: 'YYYY-MM-DD', minDate: new Date() });

            $.material.init()
        });
        $(document).on("click", ".btnFalse", function (e) {
            $("#popupOverlay").hide();
            ListadoVacio();
        });
    </script>
</asp:Content>

