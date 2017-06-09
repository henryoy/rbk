<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="Campana.aspx.cs" Inherits="Dashboard_Campanas_Campana" %>

<asp:Content runat="server" ID="Css" ContentPlaceHolderID="HeadContent">
    <link rel="stylesheet" type="text/css" href="<%= ResolveClientUrl("/Content/css/jquery-ui.css")%>" />
    <link rel="stylesheet" type="text/css" href="<%= ResolveClientUrl("~/Content/classicTheme/style.css") %>" media="screen">
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <style>
        .generateChecklist {
            width: 100%;
            height: 80px;
            background-color: #69c0af;
            color: #FFF;
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
            margin-left: 0%;
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
    </style>
    <asp:UpdatePanel runat="server" ID="upPromocion" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="mainWrapper" style="padding-top: 26px;">
                <div id="send_view">

                    <div id="send_sidebar">
                        <!-- Form -->
                        <div id="send_form">
                            <div id="send_email_form">
                                <!-- Sender Name -->
                                <asp:HiddenField runat="server" ID="hdCampanaId" Value="0" />
                                <h4 class="semi_bold">Título de campaña <span class="errorSenderName"></span></h4>
                                <asp:TextBox runat="server" ID="txtTitulo" placeholder="Título"></asp:TextBox>
                                <!-- Sender Email Address -->
                                <h4 class="semi_bold">Mensaje previo <span class="errorSenderEmailAddress"></span></h4>
                                <asp:TextBox runat="server" ID="txtMsjPrevio" placeholder="Título"></asp:TextBox>                                
                                <!-- List -->
                                <h4 class="semi_bold">Tipo campaña<span class="errorSenderSubscriberList"></span></h4>
                                <label id="subscriber_lists_ul">
                                    <asp:DropDownList runat="server" ID="dpTipoCampana" CssClass="subscriber_lists">
                                        <asp:ListItem Value="0">Seleccione Tipo</asp:ListItem> 
                                        <asp:ListItem Value="PROMOCION">Promoción</asp:ListItem>
                                        <asp:ListItem Value="INFORMATIVO">Informativa</asp:ListItem>
                                    </asp:DropDownList>

                                </label>


                                <!-- Schedule Campaign -->
                                <h4 class="semi_bold" style="padding-bottom: 18px!important; margin-top: 6px;">Campaña programada

								<div class="switch disabled" name="schedule_switch" style="right: -1px; top: -4px;">

                                    <div class="switch_thumb active" style="right: 19px;"></div>

                                    <!-- Detect sig on/off -->
                                    <input type="hidden" value="0" name="schedule" id="schedule_campaign">
                                </div>

                                </h4>

                                <!-- Schedule campaign -->
                                <div class="scheduleCampaignWrapper clear-fix">

                                    <div class="scheduleCampaignWrapperLeft">
                                        
                                        <input type="text" runat="server" enableviewstate="true" placeholder="" name="send_schedule_campaign_day" id="send_schedule_campaign_day" value="" readonly="">
                                    </div>
                                    <div class="scheduleCampaignWrapperRight">

                                        <input runat="server" enableviewstate="true" type="text" placeholder="" name="send_schedule_campaign_day" id="send_schedule_campaign_time">
                                    </div>

                                </div>

                                <!-- Datepicker calendar -->
                                <div id="datepicker" class="ll-skin-melon clear-fix"></div>

                            </div>

                        </div>
                        <div id="generateChecklist" class="semi_bold">
                            <asp:LinkButton runat="server" CssClass="generateChecklistName" ID="lnkGuardarCampana" OnClick="lnkGuardarCampana_Click">
                                Guardar Campaña -> Ir a plantilla
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div id="filter_sidebar">
                        <div id="filter_form">
                            <!-- Promocion -->
                            <!-- Reply To Email Address -->
                            <h4 class="semi_bold">Destinado a: <span class="errorSenderSubscriberList"></span></h4>
                            <label id="tipoCampana">
                                <asp:DropDownList runat="server" ID="dpTipoDestino">
                                    <asp:ListItem>TODOS</asp:ListItem>
                                    <asp:ListItem>MOVIL</asp:ListItem>
                                    <asp:ListItem>WEB</asp:ListItem>
                                </asp:DropDownList>
                            </label>
                            <!-- Promocion -->
                            <h4 class="semi_bold " id="titleListaPromocion">Tipo promoción <span class="errorSenderSubscriberList"></span>
                            </h4>
                            <label id="tipoListaPromocion" style="display: none;">
                                <asp:DropDownList runat="server" ID="dpPromocion">
                                    <asp:ListItem Value="1">Promocion 1</asp:ListItem>
                                    <asp:ListItem Value="2">Promocion 2</asp:ListItem>
                                    <asp:ListItem Value="3">Promocion 3</asp:ListItem>
                                    <asp:ListItem Value="4">Promocion 4</asp:ListItem>
                                </asp:DropDownList>
                            </label>
                            <!--- distribucion -->
                            <h4 class="semi_bold">Tipo distribucion <span class="errorSenderSubscriberList"></span></h4>
                            <label id="tipoDistribucion">
                                <asp:DropDownList runat="server" ID="dpDistribucion">
                                    <asp:ListItem Value ="1">Distribucion 1</asp:ListItem>
                                    <asp:ListItem Value ="2">Distribucion 2</asp:ListItem>
                                    <asp:ListItem Value ="3">Distribucion 3</asp:ListItem>
                                    <asp:ListItem Value ="4">Distribucion 4</asp:ListItem>
                                </asp:DropDownList>
                            </label>
                            <!--- plantilla -->
                            <h4 class="semi_bold">Plantilla <span class="errorSenderSubscriberList"></span></h4>
                            <label id="tipoPlantilla">
                                <asp:DropDownList runat="server" ID="dpPlantilla">
                                    <asp:ListItem Value ="1">Defecto</asp:ListItem>
                                    <asp:ListItem Value ="2">Plantilla 2</asp:ListItem>
                                    <asp:ListItem Value ="3">Plantilla 3</asp:ListItem>
                                    <asp:ListItem Value ="4">Plantilla 4</asp:ListItem>
                                </asp:DropDownList>
                            </label>
                        </div>
                    </div>
                    <div id="selection_sidebar">
                        <div id="selection_form">
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
    <script type="text/javascript">
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


            });
        }

        $(document).on('change', '#send_sidebar .subscriber_lists', function () {


            el = $(this);


            id = $('option:selected', this).attr('data-list-id');
            list = $('option:selected', this).val();

            console.log(el);
            console.log(id);
            console.log(list);

            if (id == 'create_new_list') {
                $('.create_new_list').trigger('click');
                $('#send_sidebar select').prop('selectedIndex', 0);
                return false;
            }

            if (list.toUpperCase() == "PROMOCION") {
                //titleListaPromocion
                $("#titleListaPromocion").css("display", "block");
                $("#tipoListaPromocion").css("display", "block");
            } else {
                $("#titleListaPromocion").css("display", "none");
                $("#tipoListaPromocion").css("display", "none");
            }
            openFilterSidebar();

        });

    </script>
    <script type="text/javascript">

        function ActiveCalendar() {

            the_switch = $('[name="schedule_switch"]');            
            switch_thumb = $('[name="schedule_switch"]').find('.switch_thumb');

            $(the_switch).removeClass('disabled');
            $(switch_thumb).removeClass('active');
            $(switch_thumb).addClass('disabled');
            $(switch_thumb).css('right','2px;')
            $(the_switch).addClass('active');
            console.log(the_switch);
            console.log(switch_thumb);

            if ($(switch_thumb).hasClass('active')) {
                $('[name="schedule"]').val('0');
                $('#datepicker, .scheduleCampaignWrapper').hide();
            }
            else {
                $('[name="schedule"]').val('1');
                $('.scheduleCampaignWrapper').show();
            }
        }
        function notification(message, type) {

            console.log(message);
            console.log(type);
            //modify notification
            $('.popup-notification').html(message);
            $('.popup-notification').removeClass('error').removeClass('success');
            $('.popup-notification').addClass(type);

            $('.popup-notification').slideDown(200);

            setTimeout(function () {

                $('.popup-notification').slideUp(200);

            }, notificationTime)

        }
        function ActiveSidebar(Tipo) {

            console.log("ready");

            if (Tipo.toUpperCase() == "PROMOCION") {
                //titleListaPromocion
                $("#titleListaPromocion").css("display", "block");
                $("#tipoListaPromocion").css("display", "block");
            } else {
                $("#titleListaPromocion").css("display", "none");
                $("#tipoListaPromocion").css("display", "none");
            }


            $('#filter_sidebar').addClass('expanded');
                        

            $('#filter_sidebar').animate({

                left: '340px'

            }, { duration: 400, easing: 'easeOutBack' });
        }
        $(document).ready(function () {

            //vars
            window.t = undefined;
            i = 0;
            errorFlag = 0;
            disableFlag = 0;
            campaignId = "180087";
            sentFlag = 0;
            user_given_date = '';
            user_given_time = '';
            user_current_time = '';
            user_current_date = '';
            user_email = 'hency.oy@gmail.com';
            testFlag = false;

                      
            isMouseDown = false;

            showTime();

            setInterval(function () {

                showTime();

            }, 1000);

            var dt = new Date();

            dateMonth = dt.getMonth();
            if (dt.getMonth() + 1 < 10) { dateMonth = '0' + (dt.getMonth() + 1) }

            clientDate = dt.getFullYear() + "-" + (dateMonth) + "-" + dt.getDate();

            $('#<%=send_schedule_campaign_day.ClientID%>').val(clientDate)

            //auto focus name
            $('#send_from_name').focus();

            $(document).on('mousedown', '.selection_item', function () {
                isMouseDown = true;
            }).mouseup(function () {
                isMouseDown = false;
            });

            $(document).on('mousedown', '.selection_item', function () {

                $(this).toggleClass("selected")
                calculateSelection();

            });

            $(document).on('blur', '#send_reply_to_email', function () {

                //variables
                var count_emails = $(this).val().split('@').length;
                var el = $(this);

                if (count_emails > 2) {

                    $(el).val('');

                    $('.errorSenderReplyToEmailAddress').show().text('Invalid');

                }

                else {

                    $('.errorSenderReplyToEmailAddress').hide().text('');

                }

            })

            $(document).on('click', '#sendPopup', function (e) {

                e.stopPropagation();

            });

            $(document).on('click', '#sendPopupOverlay', function () {

                closeSendPopup();

            })

            $(document).on('click', '#cancel_campaign', function () {

                closeSendPopup();

                setTimeout(function () {

                    closeResultOverview();

                }, 400);

            });

            $(document).keypress(function (e) {
                if (e.keyCode == 27) {

                    closeSendPopup();

                }
            });

            $(document).on('click', '.back_btn', function () {

                closeResultOverview();

            })

            //send
            //campaign
            //filter
            //script
            $(document).on('click', '#confirm_campaign', function () {

                if (disableFlag == 1) {

                    return false;

                }

                disableFlag = 1;

                $('#confirm_campaign').text('Sending..');

                sendEmailCampaign();

                return false;

            });

            $(document).on('click', '#<%=send_schedule_campaign_day.ClientID%>', function (e) {

                e.stopPropagation();
                $('#datepicker').show();

            });

            $(document).on('click', 'body', function () {

                $('#datepicker').hide();

            });


            $(document).on('mousedown', '.switch', function () {

                the_switch = $(this);
                switch_thumb = $(this).find('.switch_thumb');

                if ($(switch_thumb).hasClass('active')) {

                    $(switch_thumb).animate({

                        'right': '2px',

                    }, { duration: 100, easing: 'linear' });

                    $(the_switch).removeClass('disabled')

                    $(switch_thumb).removeClass('active');

                }

                else {

                    $(switch_thumb).animate({

                        'right': '19px'

                    }, { duration: 100, easing: 'linear' });

                    $(the_switch).addClass('disabled')

                    $(switch_thumb).addClass('active');

                }

            });

            $(document).on('mousedown', '[name="signature_switch"]', function () {

                the_switch = $(this);
                switch_thumb = $(this).find('.switch_thumb');

                if ($(switch_thumb).hasClass('active')) {

                    $('[name="signature"]').val('0');

                }

                else {

                    $('[name="signature"]').val('1');

                }

            });

            $(document).on('mousedown', '[name="schedule_switch"]', function () {

                the_switch = $(this);
                switch_thumb = $(this).find('.switch_thumb');

                if ($(switch_thumb).hasClass('active')) {

                    $('[name="schedule"]').val('0');
                    $('#datepicker, .scheduleCampaignWrapper').hide();

                }

                else {

                    $('[name="schedule"]').val('1');
                    $('.scheduleCampaignWrapper').show();

                    setTimeout(function () {

                        $('#send_form').animate({
                            scrollTop: $('#send_form')[0].scrollHeight - $('#send_form')[0].clientHeight
                        }, 500);

                    }, 50);

                }

            });         

        });

        function openSelectionSidebar() {

            $('#selection_sidebar').show();
            $('#selection_sidebar').addClass('expanded');
            $('#selection_sidebar .selected').removeClass('selected');
            $('#selection_sidebar').css('left', '340px');
            $('#selection_sidebar').animate({

                scrollTop: 0

            }, 10);

            setTimeout(function () {

                $('#selection_sidebar').animate({

                    left: '700px'

                }, { duration: 400, easing: 'easeOutBack' });

            }, 10)

        }

        function closeSelectionSidebar() {

            if (!$('#selection_sidebar').hasClass('expanded')) {

                return false;

            }

            $('#selection_sidebar').removeClass('expanded');

            $('#selection_sidebar').animate({

                left: '340px'

            }, { duration: 400, easing: 'easeInBack' });

            setTimeout(function () {

                $('#selection_sidebar').css('left', '-41px');

            }, 420)

        }

        function openFilterSidebar() {

            $('#filter_sidebar').addClass('expanded');

            country_total = $('#send_form .selected').attr('data-total-country');
            os_total = $('#send_form .selected').attr('data-total-os');
            browser_total = $('#send_form .selected').attr('data-total-browser');
            referrer_total = $('#send_form .selected').attr('data-total-referrer');
            vip_status = parseInt($('#send_form .selected').attr('data-selected-vip'));

            //show number
            $('#filter_form [data-selection-type="country"] .filter_amount').each(function () {

                el = $(this);

                $(this).text(country_total);

                if (country_total > 0) {

                    $(el).closest('.filter_item').find('span').text('Selected items');

                }

            })

            $('#filter_form [data-selection-type="os"] .filter_amount').each(function () {

                el = $(this);

                $(this).text(os_total);

                if (os_total > 0) {

                    $(el).closest('.filter_item').find('span').text('Selected items');

                }

            })

            $('#filter_form [data-selection-type="browser"] .filter_amount').each(function () {

                el = $(this);

                $(this).text(browser_total);

                if (browser_total > 0) {

                    $(el).closest('.filter_item').find('span').text('Selected items');

                }

            })

            $('#filter_form [data-selection-type="referrer"] .filter_amount').each(function () {

                el = $(this);

                $(this).text(referrer_total);

                if (referrer_total > 0) {

                    $(el).closest('.filter_item').find('span').text('Selected items');

                }

            });

            $('#filter_sidebar select').prop('selectedIndex', vip_status);

            //hack
            $('.filter_amount').css('right', '11px');
            $('.filter_amount').css('right', '12px');

            $('#filter_sidebar').animate({

                left: '340px'

            }, { duration: 400, easing: 'easeOutBack' });

        }

        function closeFilterSidebar() {

            $('#selection_sidebar').hide();
            $('#filter_sidebar').removeClass('expanded');

            $('#filter_sidebar').animate({

                left: '-41px'

            }, { duration: 400, easing: 'easeInBack' });

        }

        function openResultOverview(data_overview_id) {

            $('.back_btn').fadeIn();

            $('.country_overview, .browser_overview, .os_overview, .referrer_overview, .vip_overview').empty();

            $('.sendMain ul').animate({

                'margin-left': '-50%'

            }, 500);

            array_overview = [];

            $('#send_form li[data-id="' + data_overview_id + '"]').each(function () {

                var $this = $(this);
                var data = {};
                var country = $(this).attr('data-selected-country');
                var os = $(this).attr('data-selected-os');
                var browser = $(this).attr('data-selected-browser');
                var referrer = $(this).attr('data-selected-referrer');
                var vip = $(this).attr('data-selected-vip');

                data.country = country;
                data.os = os;
                data.browser = browser;
                data.referrer = referrer;
                data.vip = vip;

                array_overview.push(data);

            });

            if (array_overview[0]['country'] == undefined) { $('.country_overview').append('<div class="listResult">All</div>'); }
            else {

                var array_country = array_overview[0]['country'].split(";");

                $.each(array_country, function (i) {

                    if (array_country[i] == '') { }
                    else {

                        $('.country_overview').append('<div class="listResult">' + array_country[i] + '</div>')

                    }

                })

            }

            if (array_overview[0]['os'] == undefined) { $('.os_overview').append('<div class="listResult">All</div>') }
            else {

                var array_os = array_overview[0]['os'].split(";");

                $.each(array_os, function (i) {

                    if (array_os[i] == '') { }
                    else {

                        $('.os_overview').append('<div class="listResult">' + array_os[i] + '</div>')

                    }

                })

            }

            if (array_overview[0]['browser'] == undefined) { $('.browser_overview').append('<div class="listResult">All</div>') }
            else {

                var array_browser = array_overview[0]['browser'].split(";");

                $.each(array_browser, function (i) {

                    if (array_browser[i] == '') { }
                    else {

                        $('.browser_overview').append('<div class="listResult">' + array_browser[i] + '</div>')

                    }

                })

            }

            if (array_overview[0]['referrer'] == undefined) { $('.referrer_overview').append('<div class="listResult">All</div>') }
            else {

                var array_referrer = array_overview[0]['referrer'].split(";");

                $.each(array_referrer, function (i) {

                    if (array_referrer[i] == '') { }
                    else {

                        $('.referrer_overview').append('<div class="listResult">' + array_referrer[i] + '</div>')

                    }

                })

            }

            if (array_overview[0]['vip'] == undefined) { $('.vip_overview').append('<div class="listResult">All</div>') }
            else {

                var array_vip = array_overview[0]['vip'].split(";");

                $.each(array_vip, function (i) {

                    if (array_vip[i] == '') { alert() }
                    else if (array_vip[i] == '1') {

                        $('.vip_overview').append('<div class="listResult">VIP Only</div>');

                    }
                    else if (array_vip[i] == '2') {

                        $('.vip_overview').append('<div class="listResult">None VIP Only</div>');

                    }

                })

            }


            /*
    setTimeout(function(){
    
                closeResultOverview();
    
            }, 1500)
    */

        }

        function closeResultOverview() {

            $('.sendMain ul').animate({

                'margin-left': '0%'

            }, 500);

            $('.back_btn').fadeOut();

        }

        function resetFilterSidebar() {

            //reset filter sidebar
            $('.filter_amount').empty();
            setTimeout(function () {

                $('.filter_amount').css('position', 'absolute');

            }, 50);
            $('#filter_sidebar select').prop('selectedIndex', 0);
            $('#filter_sidebar .filter_item span').text('All');
            $('#filter_sidebar .selected').removeClass('selected');

        }

        function resetSelectionSidebar() {

            $('#selection_sidebar .selected').removeClass('selected');

        }

        function appendList(el, id, list) {

            //show edit button
            $('.edit_lists_btn').show();
            $('.errorSenderSubscriberList').empty();

            //reset
            $('#send_sidebar select').prop('selectedIndex', 0);

            $(el).closest('label').append('<li data-id="' + id + '" data-l-name="' + list + '">' + list + '<div class="viewList" style="display: none;"></div></li>');
            $('#send_sidebar select option[data-list-id="' + id + '"]').remove();

            $('#send_sidebar [data-id="' + id + '"]').trigger('click')

            //if empty
            count = $('#subscriber_lists_ul li').size();

            if (count > 8) {

                $('#send_sidebar select, .add_list_btn').hide();

                //add shadow hack
                $('#send_sidebar label li').first().css('box-shadow', '0 0 0 1px rgba(0,0,0,0.12)')

            }


        }

        function closeAllSidebars() {

            closeSelectionSidebar();
            setTimeout(function () {

                closeFilterSidebar();

            }, 420)

        }

        function removeList(id, list) {

            $('#send_sidebar li[data-id="' + id + '"]').remove();
            $('#send_sidebar .subscriber_lists').append('<option class="light" data-list-id="' + id + '">' + list + '</option>');

            count = $('#subscriber_lists_ul li').size();

            if (count < 1) {
                $('.edit_lists_btn').hide();


            }

            if (count > 0) {

                $('#send_sidebar select, .add_list_btn').show();

                //add shadow hack
                $('#send_sidebar label li').first().css({

                    'box-shadow': '1px 0 0 rgba(0,0,0,0.12), 0 1px 0 rgba(0,0,0,0.12), -1px 0 0 rgba(0,0,0,0.12)'

                });

            }

            closeSelectionSidebar();
            closeFilterSidebar();

        }

        function resetFilters() {

            $('#filter_sidebar select').prop('selectedIndex', 0);

        }

        function editableListsOn() {

            $('.edit_lists_btn').text('done');

            $('#send_sidebar label li').each(function () {

                el = $(this);

                $(el).prepend('<div class="removeListBtn">-</div>');
                $(el).css('padding-left', '38px');

            })

        }

        function calculateSelection() {

            count = $('#selection_form .selected').size();
            selection = 'Selected items';
            selection_type = $('#filter_form .selected').attr('data-selection-type');

            if (window.t) {
                clearTimeout(window.t);
                window.t = undefined;
            }

            if (count < 1) {

                count = '';
                selection = 'All';

                $('.clear_selection_states').hide();

            }

            else {

                $('.clear_selection_states').show();

            }

            setTimeout(function () {
                $('#filter_form .selected .filter_amount').css('position', 'absolute')
            }, 50);

            $('#filter_form .selected .filter_amount').text(count);
            $('#filter_form .selected span').text(selection);

            window.t = setTimeout(function () {

                str = '';

                if (count == 0) {

                    $('#send_form .selected').attr('data-selected-' + selection_type, '');
                    $('#send_form .selected').attr('data-total-' + selection_type, count);

                }

                $('#selection_form .selected').each(function () {

                    selected = $(this).text();
                    str += selected + ';';

                    $('#send_form .selected').attr('data-selected-' + selection_type, str);
                    $('#send_form .selected').attr('data-total-' + selection_type, count);

                });

            }, 250);

        }

        function restoreFilterSidebar() {

            $('#filter_sidebar .filter_amount').empty();
            $('#filter_sidebar .filter_item span').text('All');
            $('#filter_sidebar .filter_item').removeClass('selected');

        }

        function restoreSelectionBar() {

            selection_type = $('#filter_form .selected').attr('data-selection-type');
            selected_data = $('#send_form .selected').attr('data-selected-' + selection_type);
            i = 0;

            if (selected_data == undefined) {

                return false;

            }

            var match = selected_data.split(';')
            for (var a in match) {

                if (!match[a] == '') {

                    $('#selection_form .selection_item:textEquals("' + match[a] + '")').addClass('selected');
                    $('.clear_selection_states').show();
                    i++;

                }

            }

        }

        function closeSendPopup() {

            if (sentFlag == 1) {

                return false;

            }

            $('#sendPopupOverlay').css({
                'opacity': '0',
                'transition': '0.4s all ease',
            });

            $('#sendPopup').css({
                'opacity': '0',
                'transition': '0.4s all ease',
                'transform': 'scale(0.8) translateY(-50%)'
            });

            $('.stack').css({
                'transition': '0.4s all ease',
                'transform': 'scale(1)'
            });

            setTimeout(function () {

                $('#sendPopupOverlay').css('display', 'none');

            }, 400);

        }

        function showTime() {

            dt = new Date();
            h = dt.getHours(), m = dt.getMinutes();
            if (m < 10) { m = '0' + m; }

            _time = (h > 12) ? (h - 12 + ':' + m + ' PM') : (h + ':' + m + ' AM');

            $('#<%=send_schedule_campaign_time.ClientID%>').attr('placeholder', _time);

        }

        function getResultFilter(array) {

            headline = 'Result';
            paragraph = '';

            btnTrue = 'Send campaign';
            btnTrueId = 'adsdasd';

            openPopup();

            for (var i = 0; i < array.length; i++) {

                email = array[i]['email'];

                $('#popup h3').append('<h4>' + email + '</h4>');


            }

        }

        $(function () {
            $('#datepicker').datepicker({
                onSelect: function (date) {

                    $('#<%=send_schedule_campaign_day.ClientID%>').val(date);
                    $('#datepicker').hide();

                },
                minDate: clientDate,
                selectWeek: true,
                dateFormat: "yy-mm-dd",
                inline: true,
                defaultDate: new Date()
            });
        });

        $.expr[':'].textEquals = $.expr.createPseudo(function (arg) {
            return function (elem) {
                return $(elem).text().match("^" + arg + "$");
            };
        });

    </script>
</asp:Content>


