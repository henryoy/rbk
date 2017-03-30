<%@ Page Title="Dashboard" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="index.aspx.cs" Inherits="Dashboard_index" %>

<%--<%@ Page Title="Causas" EnableEventValidation="false" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Causas.aspx.cs" Inherits="Configurar_Causas" %>--%>
<asp:Content runat="server" ID="Css" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div id="analytics" class="disable_selection">
        <div id="list_name_bar">
            <h2><font class="light cat">Tablero</font></h2>
            <div id="action" class="semi_bold" style="width: 160px;">
                <h3 style="text-align: center;"><a href="new_campaign/index.php"><font><font>Nueva campaña</font></font></a></h3>
            </div>
        </div>
        <div id="mainWrapper">
            <div class="analytics_reports_wrapper clear-fix">
                <!-- Line chart -->
                <div id="canvas_linechart_wrapper2">
                    <div id="canvas-holder1" style="padding-bottom: 0px;">
                        <canvas id="chart1" height="222" width="933" style="width: 933px; height: 222px;"></canvas>
                    </div>
                    <div id="chartjs-tooltip" style="opacity: 0.9; left: 756px; top: 199.12px; font-family: &quot; helvetica neue&quot; , helvetica, arial, sans-serif; font-size: 14px; font-style: normal; display: block;">
                        <div class="chartjs-tooltip-section">
                            <span class="chartjs-tooltip-value" style="color: rgba(0,0,0,0)">
                                <font><font>0</font></font>
                            </span>
                        </div>
                        <div class="chartjs-tooltip-section">
                            <span class="chartjs-tooltip-value" style="color: rgba(255,255,255,0)">
                                <font><font>0</font></font>
                            </span>
                        </div>
                    </div>
                </div>
                <div id="charts" class="clear-fix" style="padding-top: 20px;">
                    <div id="charts_left" style="width: 50%;">
                        <div id="charts_left_1">
                            <div id="chart_sent" class="round_chart" style="position: relative;">
                                <span><font><font>Expedido</font></font></span>
                                <p class="progressbar-text" style="position: absolute; left: 50%; top: 50%; padding: 0px; margin: 0px; transform: translate(-50%, -50%); color: rgb(105, 192, 175);"><font><font>0</font></font></p>
                            </div>
                        </div>
                        <div id="charts_left_2">
                            <div id="chart_delivered" class="round_chart" style="position: relative;">
                                <span><font><font>Entregado</font></font></span>
                                <p class="progressbar-text" style="position: absolute; left: 50%; top: 50%; padding: 0px; margin: 0px; transform: translate(-50%, -50%); color: rgb(251, 217, 112);"><font><font>0</font></font></p>
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
                                <font><font>0 </font></font>
                                <h6 class="regular"><font><font>Hoy</font></font></h6>
                            </span>
                            <span class="rate_wrapper_result_month regular">
                                <font><font>&nbsp;/ 0 </font></font>
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


</asp:Content>
<asp:Content runat="server" ID="ScriptsJS" ContentPlaceHolderID="ScriptsPages">
    <script src="<%= ResolveClientUrl("~/Scripts/js/easing.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/jquery.gravatar.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/functions.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/chartjs/chart.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/circle-progress.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/custom.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/chartjs/waypoints.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/dashboard.custom.js") %>"></script>

</asp:Content>
