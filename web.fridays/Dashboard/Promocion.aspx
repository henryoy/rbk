<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="Promocion.aspx.cs" Inherits="Dashboard_Promocion" %>

<asp:Content runat="server" ID="Css" ContentPlaceHolderID="HeadContent">    
    <link rel="stylesheet" type="text/css" href="../Content/css/jquery-ui.css" />
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <style>
        .back_btn {
            height: 43px;
            position: absolute;
            left: 50px;
            top: 20px;
            color: #919191;
            font-size: 12px;
            text-transform: uppercase;
            cursor: pointer;
            background-image: url(../content/img/icons/arrow_left.png);
            background-position: 1px center;
            background-repeat: no-repeat;
            background-color: #FFF;
            line-height: 43px;
            padding-left: 15px;
            padding-right: 20px;
        }

            .back_btn:hover {
                color: #69c0af;
                background-image: url(../content/img/icons/arrow_left_hover.png);
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

        #send_view li {
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
        }

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
            color:#fff;
            margin-left: 25%;
        }

        .scheduleCampaignWrapper {
            position: relative;
            display: none;
        }

        /*.scheduleCampaignWrapperLeft {
            float: left;
            width: 60%;
            padding-right: 25px;
            box-sizing: border-box;
            -moz-box-sizing: border-box;
            height: 45px;
        }*/

        .scheduleCampaignWrapperRight {
            float: left;
            width: 40%;
            height: 45px;
        }

        .send_schedule_campaign_day {
            width: 100% !important;
            cursor: pointer;
        }

        #datepicker,#datepicker_init {
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

        .uploadFile {
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
        }

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

        .filter_button {
            position: absolute;
            right: 0px;
            top: 0px;
            cursor: pointer;
            font-size: 13px;
            color: #70cce5;
        }

        .viewList {
            right: 0px;
            top: 0px;
            height: 100%;
            width: 32px;
        }

        .back_btn {
            height: 43px;
            position: absolute;
            left: 38px;
            top: 27px;
            color: #919191;
            font-size: 12px;
            text-transform: uppercase;
            cursor: pointer;
            background-image: url(../img/icons/arrow_left.png);
            background-position: 1px center;
            background-repeat: no-repeat;
            background-color: transparent;
            line-height: 46px;
            padding-left: 15px;
            padding-right: 20px;
            z-index: 99999;
            display: none;
        }

            .back_btn:hover {
                color: #69c0af;
                background-image: url(../img/icons/arrow_left_hover.png);
            }
    </style>

    <div id="campaigns" class="disable_selection semi_bold">
        <!-- Top Bar -->
        <div id="list_name_bar">
            <div class="back_btn semi_bold">Regresar</div>
        </div>
        <div id="mainWrapper">
            <div id="send_view">
                <div id="send_sidebar">
                    <!-- Form -->
                    <div id="send_form" data-campaign="171862">

                        <h4 class="semi_bold">Título <span class="errorSenderName"></span></h4>
                        <asp:TextBox runat="server" ID="txtTitulo" placeholder="Título"></asp:TextBox>                        
                        <h4 class="semi_bold">Descripción <span class="errorSenderEmailAddress"></span></h4>
                        <asp:TextBox runat="server" ID="txtDescripcion" placeholder="Descripción" ></asp:TextBox>  
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
                            <asp:DropDownList class="subscriber_lists" runat="server" ID="dpTipoPromocion">
                                <asp:ListItem Value="VISITA">VISITA</asp:ListItem>
                                <asp:ListItem Value="EVENTO">EVENTO</asp:ListItem>
                                <asp:ListItem Value="AMBOS">AMBOS</asp:ListItem>
                            </asp:DropDownList>                   
                        </label>
                        <h4 class="semi_bold" style="padding-bottom: 24px!important; margin-top: 30px;">
                            <a href="../account/signature/index.php" target="_blank" style="color: #4a4a4a;">
                                Include Email Signature
                            </a>
                            <div class="switch disabled" name="signature_switch" style="right: -1px; top: -4px;">
                                <div class="switch_thumb active" style="right: 19px;"></div>
                                <input type="hidden" value="0" name="signature" id="signature">
                            </div>
                        </h4>
                        <h4 class="semi_bold" style="padding-bottom: 18px!important; margin-top: 6px;">
                             Configurar Fecha de promoción					
							<div class="switch disabled" name="schedule_switch" style="right: -1px; top: -4px;">
                                <div class="switch_thumb active" style="right: 19px;"></div>                                
                                <input type="hidden" value="0" name="schedule" id="schedule_campaign">
                            </div>
                        </h4>                        
                        <div class="scheduleCampaignWrapper clear-fix">
                            <div class="scheduleCampaignWrapperLeft">
                                <!--<input type="text" placeholder="" class="send_schedule_campaign_day_init" value="" readonly="">-->
                                <asp:TextBox runat="server" ID="txtFechaInicio" CssClass="send_schedule_campaign_day_init" ViewStateMode="Enabled"></asp:TextBox>
                            </div>
                            <%--<div class="scheduleCampaignWrapperRight">
                                <input type="text" placeholder="" class="send_schedule_campaign_time">
                            </div>--%>
                        </div>
                        <div id="datepicker_init" class="ll-skin-melon clear-fix"></div>
                        <div class="scheduleCampaignWrapper clear-fix">
                            <div class="scheduleCampaignWrapperLeft">                                
                                <asp:TextBox runat="server" ID="txtFechaFinal" CssClass="send_schedule_campaign_day" ViewStateMode="Enabled"></asp:TextBox>
                            </div>
                            <%--<div class="scheduleCampaignWrapperRight">
                                <asp:TextBox runat="server" ID="txtFechaInicio" CssClass="send_schedule_campaign_time"  />
                            </div>--%>
                        </div>                        
                        <div id="datepicker" class="ll-skin-melon clear-fix"></div>
                    </div>
                    <div id="generateChecklist" class="semi_bold">
                        <asp:LinkButton runat="server" ID="lnkGuardarPromocion" CssClass="generateChecklistName" OnClick="lnkGuardarPromocion_Click">Guardar promoción</asp:LinkButton>
                       <%-- <div class="generateChecklistName">
                            Guardar promoción
                        </div>--%>

                        <!-- test -->

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content runat="server" ID="ScriptsJS" ContentPlaceHolderID="ScriptsPages">
    
    <script src="<%= ResolveClientUrl("~/Scripts/js/easing.js") %>" type="text/javascript"></script>
   <script src="<%= ResolveClientUrl("~/Scripts/js/jquery.color.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/jquery-ui.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/functions.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/custom.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/chartjs/waypoints.min.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
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

                       

            $(document).on('click', '.send_schedule_campaign_day', function (e) {
                e.stopPropagation();
                
                $('#datepicker').show();
                $('#send_form .selected').removeClass('selected');
            });

            $(document).on('click', '.send_schedule_campaign_day_init', function (e) {
                e.stopPropagation();

                $('#datepicker_init').show();
                $('#send_form .selected').removeClass('selected');
            });

           
            if (getQueryVariable('callback') == 'success') {

                analytics_id = getQueryVariable('id');

                notificationContent = "Campaign successfully sent!"; notificationColor = "#7ebbad"; notification(); setTimeout(function () { $(location).attr("href", "../analytics/index.php?id=" + analytics_id); }, 3000);

            }

            else if (getQueryVariable('callback') == 'credits') {


                amount = getQueryVariable('amount');
                save = getQueryVariable('save');
                stripe = getQueryVariable('stripe');
                headline = 'You need to buy more credits'
                paragraph = 'It seems you don\'t have enough credits to send out your campaign. You just need ' + amount + ' more.';

                btnTrue = 'Buy ' + amount + ' credits';
                btnTrueId = 'buy_more_credits';

                openPopup();

            }

            else if (getQueryVariable('callback') == 'lowplan') {

                notificationContent = "You have to many subscribers in order to sent this newsletter. Please upgrade"; notificationColor = "#ea5a5b"; notification(); setTimeout(function () { $(location).attr("href", "../credits/index.php?"); }, 3000);

            }


            else if (getQueryVariable('callback') == 'verify') {

                email = getQueryVariable('email');
                name = getQueryVariable('name');
                name = decodeURIComponent((name).replace(/\+/g, '%20'));
                subject = getQueryVariable('subject');
                subject = decodeURIComponent((subject).replace(/\+/g, '%20'));
                list = getQueryVariable('list');
                list = decodeURIComponent((list).replace(/\+/g, '%20'));

                headline = 'Please verify ' + email
                paragraph = 'Due to security measures, we need to make sure you have control over <span class="brandColor">' + email + '</span>. This is a one time process. Please, check your email.';

                inputField = 'Verification Code';
                inputFieldId = 'verification_value';

                btnTrue = 'Verify address and send campaign';
                btnTrueId = 'verify_and_send';

                openPopup();

                subject = decodeURIComponent((subject).replace(/\+/g, '%20'));
                subject = subject.replace(/\\/g, '');
                name = decodeURIComponent((name).replace(/\+/g, '%20'));
                $('#send_from_name').val(name);
                $('#send_from_email').val(email);
                $('#send_subject_line').val(subject);

                setTimeout(function () {

                    $('[data-list-name="' + list + '"]').trigger('click');

                }, 250)


            }

            else if (getQueryVariable('redirect') == '1') {

                email = getQueryVariable('email');
                name = getQueryVariable('name');
                subject = getQueryVariable('subject');
                list = getQueryVariable('list');

                subject = decodeURIComponent((subject).replace(/\+/g, '%20'));
                subject = subject.replace(/\\/g, '');
                list = decodeURIComponent((list).replace(/\+/g, '%20'));
                name = decodeURIComponent((name).replace(/\+/g, '%20'));

                $('#send_from_name').val(name);
                $('#send_from_email').val(email);
                $('#send_subject_line').val(subject);

                setTimeout(function () {

                    $('[data-list-name="' + list + '"]').trigger('click');

                }, 250)

            }

            $(document).on('click', '#verify_and_send', function () {

                if (disableFlag == '1') {

                    return false;

                }

                disableFlag = 1;

                $(this).val('Verifying..')

                verification_code = $('#verification_value').val();

                $.ajax({
                    type: "POST",
                    dataType: "html",
                    url: "../scripts/calls.php?func=verify_address",
                    data: { verification_code: verification_code }
                }).done(function (data) {

                    if (data == 0) {

                        closePopup();

                        setTimeout(function () {

                            $('#generateChecklist').trigger('click');

                        }, 500);

                    }

                    else {

                        notificationContent = "Wrong verification code"; notificationColor = "#ea5a5b"; notification();
                        $('#verify_and_send').val('Verify Email Address');

                    }

                    disableFlag = 0;

                });

            });

            $(document).on('click', 'body', function () {

                $('#datepicker').hide();

            });

            $(document).on('click', '#continue_to_analytics', function () {

                $(location).attr('href', '../analytics/campaign/index.php?id=' + campaignId);

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

            $(document).on('click', '#buy_more_credits', function () {

                $(location).attr('href', '../checkout/index.php?credits=' + creditsShort);

            })

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

                editableListsOff();

                //hide edit button
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

        function editableListsOff() {

            $('.edit_lists_btn').text('edit');

            $('.removeListBtn').remove();
            $('#send_sidebar label li').css('padding-left', '10px');

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

        function getSelectionList() {

            selection_type = $('#filter_form .selected').attr('data-selection-type');
            selection_list = $('#send_form .selected').attr('data-l-name');

            $.ajax({
                type: "POST",
                dataType: "html",
                url: "../scripts/calls.php?func=fetch_selection_lists",
                data: { selection_type: selection_type, selection_list: selection_list }
            }).done(function (data) {

                $('#selection_form').html(data);
                restoreSelectionBar();

                //get the count
                selection_count = $('#selection_form .selection_item').size();

                if (selection_count < 2) {

                    //detect if it reads 'Other';
                    selection_text = $('#selection_form .selection_item').text();

                    if (selection_text == 'Other') {

                        $('#selection_form .selection_item').after('<div id="embed_form_notification" class="semi_bold">Install our form on your website<br/> to show more filters</div>')

                    }


                }

            }).fail(function () {

                setTimeout(function () {

                    getSelectionList();

                }, 1000)

            });

        }

        function sendEmailCampaign() {

            var filter_array = [];

            $('#send_form li[data-id]').each(function () {

                var $this = $(this);
                var data = {};
                var country = $(this).attr('data-selected-country');
                var os = $(this).attr('data-selected-os');
                var browser = $(this).attr('data-selected-browser');
                var referrer = $(this).attr('data-selected-referrer');
                var vip = $(this).attr('data-selected-vip');
                var list_name = $(this).attr('data-id');

                data.country = country;
                data.os = os;
                data.browser = browser;
                data.referrer = referrer;
                data.vip = vip;
                data.list_name = list_name;

                filter_array.push(data);

            });

            campaign_id = $('[data-campaign]').attr('data-campaign');
            send_from_name = $('#send_from_name').val();
            send_from_email = $('#send_from_email').val();
            send_reply_to_email = $('#send_reply_to_email').val();
            send_subject_line = $('#send_subject_line').val();
            send_preview_message = $('#send_preview_message').val();
            signature = $('#signature').val();
            schedule = $('#schedule_campaign').val();
            user_given_date = $('#send_schedule_campaign_day').val();

            if (send_from_email == send_reply_to_email) {

                send_reply_to_email = '';

            }

            if (schedule == '0') {

                user_given_date = '';
                user_given_time = '';
                user_current_time = '';
                user_current_date = '';

            }

            $.ajax({
                type: "POST",
                dataType: "html",
                url: "../scripts/calls.php?func=prepare_campaign",
                data: {
                    filter_array: filter_array,
                    campaign_id: campaign_id,
                    send_from_name: send_from_name,
                    send_from_email: send_from_email,
                    send_reply_to_email: send_reply_to_email,
                    send_subject_line: send_subject_line,
                    send_preview_message: send_preview_message,
                    signature: signature,
                    user_given_date: user_given_date,
                    user_given_time: user_given_time,
                    user_current_time: user_current_time,
                    user_current_date: user_current_date

                }
            }).done(function (data) {

                sentFlag = 1;

                if (data == 'state_success') {

                    data_state = 'success';
                    data_headline = 'Nice work! We got it..';
                    data_message = 'Looks good! We\'re processing your campaign, it\'ll be in your subscribers inboxes any minute, unless you\'ve scheduled it.';

                }

                else if (data == 'state_hold') {

                    data_state = 'hold';
                    data_headline = 'Great! But we need to review this first..';
                    data_message = 'We\'ve noticed your campaign holds one or more words that got caught by our spam filter. Don\'t worry, but we\'ll need to review your campaign first.';

                }

                else if (data == 'error_1') {

                    alert('Fetched data is empty. Do not resend. Please, notify our support, as they will make sure this error will never happen again. help@stampready.net. Thank you.');
                    return false;

                }

                else if (data == 'error_2') {

                    alert('User id is empty. Do not resend. Please, notify our support, as they will make sure this error will never happen again. help@stampready.net. Thank you.');
                    return false;

                }

                else {

                    console.log('"' + data + '"');

                    data_state = 'error';
                    data_headline = 'Woops.. Something went wrong';
                    data_message = 'The browser sent your campaign to us, but we could not establish the connection. Please, check your Sent page to see if your campaign has been sent out, or contact help@stampready.net';

                }

                $('.sendHeader h1, .sendOverviewBottom').fadeOut(400);

                $('.sendHeader').animate({

                    'height': '100%',
                    'background-color': '#dbdbdb'

                }, { duration: 450, easing: 'easeOutCubic' });

                setTimeout(function () {

                    $('#sendPopup').css({

                        'margin-top': '20px'

                    });

                    $('.sendOverviewTop').animate({

                        'height': '400px'

                    }, 200)

                    setTimeout(function () {

                        $('#sendPopup').css({

                            'margin-top': '0px'

                        });

                        $('.sendHeader').append('<div id="splash"><div id="send_output_icon" class="' + data_state + '"></div><h4 class="semi_bold">' + data_headline + '</h4><p class="regular">' + data_message + '</p><div id="continue_to_analytics" class="semi_bold">View Campaign Analytics</div></div>');

                        setTimeout(function () {

                            $('#splash').addClass('animated');

                            setTimeout(function () {

                                $('#sendPopup').css({
                                    'transform': 'scale(1.03) translateY(-50%)',
                                    '-webkit-transform': 'scale(1.03) translateY(-50%)'
                                });

                                setTimeout(function () {

                                    $('#sendPopup').css({
                                        'transform': 'scale(1) translateY(-50%)',
                                        '-webkit-transform': 'scale(1) translateY(-50%)'
                                    });

                                }, 250)

                            }, 300)

                        }, 100);


                    }, 350)


                }, 200)

            }).fail(function () {

                setTimeout(function () {

                    sendEmailCampaign();

                }, 2000);

            });

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

            $('.send_schedule_campaign_time').attr('placeholder', _time);

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
                    $("#<%=txtFechaInicio.ClientID%>").val(date);
                    //$('.send_schedule_campaign_day_init').val(date);
                    $('#datepicker_init').hide();

                },
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


