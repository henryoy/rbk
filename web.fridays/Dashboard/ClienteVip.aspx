<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="ClienteVip.aspx.cs" Inherits="Dashboard_ClienteVip" %>

<asp:Content runat="server" ID="Css" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <div id="list_view" class=" list_subs">
        <!-- Top Bar -->
        <div id="list_name_bar">
            <h2 data-listenc="lZLrpmHIcqOvieFNdJb19AfX" data-unsubscribe-link="" data-vip-quota="3"><b class="light cat">Clientes</b></h2>
            <!-- dropdown -->
            <div id="action" class="semi_bold">
            </div>
        </div>
        <!-- Main -->
        <div id="mainWrapper">
            <!-- Tabs -->
            <div id="tabs">
                <!-- Ul that hold the tabs -->
                <ul class="clear-fix semi_bold">
                    <!-- Today tab -->
                    <li data-tab-url="today">Registros Hoy <span class="tabs_today_active">0</span>
                    </li>
                    <!-- All tab -->
                    <li data-tab-url="all" class="activedTab">Todos <span class="tabs_all_active bubble">2</span>
                    </li>
                    <!-- VIP tab -->
                    <li class="vip_tab" data-tab-url="vip">VIP <span class="tabs_vip_active">2</span>
                    </li>
                    <!-- Blocked tab -->
                    <li class="blocked_tab" data-tab-url="blocked">Bloqueados <span>1</span>
                    </li>

                </ul>

            </div>

            <!-- Indicator -->
            <div id="indicator" class="indicator_today semi_bold">

                <!-- Email Search -->
                <div class="indicator_today_email_address" style="width: 40%;">

                    <input type="text" id="search_bar" placeholder="Buscar cliente" data-search="all">

                    <div id="search_bar_result" class="semi_bold hidden"></div>

                </div>

                <!-- Indicator name -->
                <div class="indicator_today_name semi_bold" style="width: 20%;">Nombre</div>

                <!-- Indicator country -->
                <div class="indicator_today_name semi_bold" style="width: 30%;">Fecha Registro</div>

                <!-- Add Subscriber button -->
               <%-- <a href="#" id="add_subscriber" class="semi_bold"/>--%>
                
            </div>

            <!--  -->

            <!-- List that contains the subscribers -->
            <div id="row_list" class="subscriber_result">

                <ul class="subscriber_result_today regular">

                    <li data-id="21411270" data-name="Jorge Pech" data-email="jorge@codemint.com.mx" data-date="2016/12/20" data-list="Deporte" data-country="Tizimin" data-opens="3" data-clicks="0" data-referrer="Aguacates" data-os="IOS" data-browser="Safari" data-custom-1="" data-custom-2="">
                        <div class="subscriber_email_address" style="width: 40%;">
                            <div class="crown semi_bold" title="3 Visitas">3</div>
                            <div class="subscriber_email_original done">jorge@codemint.com.mx<asp:Image runat="server" ImageUrl="~/Images/default.png" /></div>
                        </div>
                        <div class="subscriber_name" style="width: 20%;">Jorge Pech</div>
                        <div class="subscriber_country" style="width: 30%;">2016/12/20</div>
                    </li>
                    <li data-id="21411269" data-name="Jorge Pech" data-email="jrpech@gmail.com" data-date="2016/12/20" data-list="Deporte" data-country="Merida" data-opens="3" data-clicks="0" data-referrer="Francisco Montejo" data-os="" data-browser="" data-custom-1="" data-custom-2="">
                        <div class="subscriber_email_address" style="width: 40%;">
                            <div class="crown semi_bold" title="3 Visitas">3</div>
                            <div class="subscriber_email_original done">jrpech@gmail.com<asp:Image runat="server" ImageUrl="~/Images/default.png" /></div>
                        </div>
                        <div class="subscriber_name" style="width: 20%;">Jorge Pech</div>
                        <div class="subscriber_country" style="width: 30%;">2016/12/20</div>
                    </li>
                </ul>

            </div>

        </div>

    </div>

</asp:Content>

<asp:Content runat="server" ID="ScriptsJS" ContentPlaceHolderID="ScriptsPages">
    <script src="<%= ResolveClientUrl("~/Scripts/js/easing.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/jquery.gravatar.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/functions.js") %>" type="text/javascript"></script>
        <script src="<%= ResolveClientUrl("~/Scripts/js/chartjs/waypoints.min.js") %>" type="text/javascript"></script>
    <script src="<%= ResolveClientUrl("~/Scripts/js/custom.js") %>" type="text/javascript"></script>
    <script>

        $(document).ready(function () {

            //variables
            row_list_width = $('#row_list').width();
            add_width = $('#add_cell_bar').width();
            total_width = add_width - row_list_width + 50;
            action = '';
            processing = '';
            vip_quota_opens = $('[data-vip-quota]').attr('data-vip-quota');
            name = '';
            list_name = 'Deporte';
            window_height = $(document).height();
            scroll_height = $('#mainWrapper').prop('scrollHeight');
            scroll_flag = 0;
            var timer;

            //initialise gravatars
            initialiseSubscriberGravatars();

            //initialise VIP members
            awardCrownToVIPMembers();

            //if processing of a file is active
            if (processing) {

                //create popup
                headline = 'We\'re processing your list';
                paragraph = 'Because the uploaded file contains quite a few e-mail addresses, we need a little while to process it. We\'ll email you shortly. You don\'t need to keep this page active.';

                btnTrue = 'Great!';
                btnTrueId = 'closePopup';

                //open popup
                openPopup();

            }

            //if name is true
            if (name == 'true') {

                //notification that the email address already exists
                setTimeout(function () {

                    notificationContent = 'That email address already exists';
                    notificationColor = "#ea5a5b";
                    notification();

                }, 500)

            }

            if (action == 'embed') {

                setTimeout(function () {

                    $('.embed_form').trigger('click');

                }, 500)

            }

            //if list contains in the address bas
            if (window.location.href.indexOf("list") > -1) {

                name = 'Deporte';

                $('#subscribers_link li[name="' + name + '"]').addClass('active');
            }

            //mouse enter sub to delete
            $('.subscriber_result').on('mouseenter', 'li', function () {

                //variables
                opens = $(this).attr('data-opens');

                //if opens is the same or more than the quota, show ungrant button
                if (opens >= vip_quota_opens) { vip_button = 'undo_vip_sub'; }
                else { vip_button = 'make_vip_sub'; }

                $(this).append('<div class="actions semi_bold" style="position: absolute; right: 0px;"><div class="block_sub"></div><div class="delete_sub"></div><div class="' + vip_button + '"></div><div class="edit_sub"></div></div>');

            }).on('mouseleave', 'li', function () {

                $('.actions').remove();

            });

            //embed form
            //$(document).on('click', '.embed_form', function () {

            //    //variables
            //    list_name = $('#list_name_bar h2').attr('data-listenc');

            //    //redirect to form editor
            //    $(location).attr('href', '../../form/index.php?token=' + list_name);

            //});

            //delete subscriber list button
            $(document).on('click', '.delete_subscriber_list', function () {

                //vars
                list_name = 'Deporte';
                sub_number = $('[data-tab-url="all"] span').text();

                //popup attr
                headline = 'You\'re about to delete <span>' + list_name + '</span>';
                paragraph = 'This list contains <span class="semi_bold" style="color: #4a4a4a;">' + sub_number + '</span> subscribers.';

                btnTrue = 'Yes, delete list';
                btnTrueId = 'delete_subscriber_list_yes';

                btnFalse = 'No, keep list';

                // invert = true;

                //open popup
                openPopup();

            });

            //Delete subscriber list script
            $(document).on('click', '#delete_subscriber_list_yes', function () {

                //vars
                list_name = 'Deporte';
                amount_subscribers = $('.tabs_all_active').text();

                //ajax
                $.ajax({
                    type: "POST",
                    dataType: "html",
                    url: "../../scripts/calls.php?func=delete_list",
                    data: {
                        list_name: list_name, amount_subscribers: amount_subscribers
                    }
                }).done(function (data) {

                    //redirect to subscribers today page
                    $(location).attr('href', '../all/index.php');

                });

            });

            //edit subscriber button
            $(document).on('click', '.edit_sub', function () {

                //vars
                el = $(this).closest('li');
                id = $(this).closest('li').attr('data-id');
                name = $(this).closest('li').attr('data-name');
                email = $(this).closest('li').attr('data-email');
                date = $(this).closest('li').attr('data-date');
                list = $(this).closest('li').attr('data-list');
                country = $(this).closest('li').attr('data-country');
                referrer = $(this).closest('li').attr('data-referrer');
                os = $(this).closest('li').attr('data-os');
                browser = $(this).closest('li').attr('data-browser');
                opens = $(this).closest('li').attr('data-opens');
                custom_1 = $(this).closest('li').attr('data-custom-1');
                custom_2 = $(this).closest('li').attr('data-custom-2');
                img_src = $(this).closest('li').find('.subscriber_email_address img').attr('src');

                //popup attr
                btnTrue = 'Update subscriber data';
                btnTrueId = 'update_subscriber_data';

                btnFalse = 'Do not change';

                //custom html
                customHtml = '<div id="sub_data_info" class="bold"><img src="' + img_src + '">Edit subscriber</div><ul class="data_change clear-fix"><li class="clear-fix"><div class="data_name semi_bold" style="background-image: url(../../img/icons/data_name_name@2x.png)">Name</div><div class="data_value"><input type="text" value="' + name + '" data-sub-val="name" class="regular"></div></li><li class="clear-fix"><div class="data_name semi_bold" style="background-image: url(../../img/icons/data_name_email@2x.png)">Email</div><div class="data_value"><input type="text" value="' + email + '" data-sub-val="email" class="regular add_email_gravatar"></div></li><li class="clear-fix"><div class="data_name semi_bold" style="background-image: url(../../img/icons/data_name_date@2x.png)">Date Added</div><div class="data_value"><input type="text" value="' + date + '" data-sub-val="date" class="regular"></div></li><li class="clear-fix"><div class="data_name semi_bold" style="background-image: url(../../img/icons/data_name_list@2x.png)">List Name</div><div class="data_value"><input type="text" value="' + list + '" data-sub-val="list" class="regular"></div></li><li class="clear-fix"><div class="data_name semi_bold" style="background-image: url(../../img/icons/data_name_country@2x.png)">Country</div><div class="data_value"><input type="text" value="' + country + '" data-sub-val="country" class="regular"></div></li><li class="clear-fix"><div class="data_name semi_bold" style="background-image: url(../../img/icons/data_name_referrer@2x.png)">Referrer</div><div class="data_value"><input type="text" value="' + referrer + '" data-sub-val="referrer" class="regular"></div></li><li class="clear-fix"><div class="data_name semi_bold" style="background-image: url(../../img/icons/data_name_os@2x.png)">OS</div><div class="data_value"><input type="text" value="' + os + '" data-sub-val="os" class="regular"></div></li><li class="clear-fix"><div class="data_name semi_bold" style="background-image: url(../../img/icons/data_name_browser@2x.png)">Browser</div><div class="data_value"><input type="text" value="' + browser + '" data-sub-val="browser" class="regular"></div></li><li class="clear-fix"><div class="data_name semi_bold" style="background-image: url(../../img/icons/data_name_custom1@2x.png)">Custom 1</div><div class="data_value"><input type="text" value="' + custom_1 + '" data-sub-val="custom_1" class="regular"></div></li><li class="clear-fix"><div class="data_name semi_bold" style="background-image: url(../../img/icons/data_name_custom2@2x.png)">Custom 2</div><div class="data_value"><input type="text" value="' + custom_2 + '" data-sub-val="custom_2" class="regular"></div></li></ul>';

                //open popup
                openPopup();

                //modify popup
                $('#popup').css('padding', '30px 50px 116px 50px')

                //little hack to unblur inpur field
                $(':focus').blur();

            });

            //update subscriber data
            $(document).on('click', '#update_subscriber_data', function () {

                //fetched vars
                name = $('[data-sub-val="name"]').val();
                email = $('[data-sub-val="email"]').val();
                date = $('[data-sub-val="date"]').val();
                list = $('[data-sub-val="list"]').val();
                country = $('[data-sub-val="country"]').val();
                referrer = $('[data-sub-val="referrer"]').val();
                os = $('[data-sub-val="os"]').val();
                browser = $('[data-sub-val="browser"]').val();
                custom_1 = $('[data-sub-val="custom_1"]').val();
                custom_2 = $('[data-sub-val="custom_2"]').val();
                img_src = $(el).find('img').attr('src');
                data_switch_thumb = $('[data-switch-thumb]');

                //AJAX call to update
                $.ajax({
                    type: "POST",
                    dataType: "html",
                    url: "../../scripts/calls.php?func=update_subscriber",
                    data: {
                        id: id, name: name, email: email, date: date, list: list, country: country, referrer: referrer, os: os, browser: browser, custom_1: custom_1, custom_2: custom_2
                    }
                }).done(function (data) {

                    //close popup
                    closePopup();

                    //wait 500 ms
                    setTimeout(function () {

                        //present notification
                        notificationContent = 'Subscriber information updated';
                        notificationColor = "#69c0af";
                        notification();

                    }, 500);

                    //add user data to list
                    gr_img = $(el).find('.subscriber_email_address img').clone();
                    $(el).find('.subscriber_email_address img').remove();
                    $(el).find('.subscriber_email_address').html('<div class="subscriber_email_original">' + email + '</div>');
                    $(el).find('.subscriber_email_address').prepend(gr_img);

                    $(el).find('.subscriber_name').text(name);
                    $(el).find('.subscriber_country').text(country);

                    $(el).attr('data-name', name);
                    $(el).attr('data-email', email);
                    $(el).attr('data-date', date);
                    $(el).attr('data-list', list);
                    $(el).attr('data-country', country);
                    $(el).attr('data-referrer', referrer);
                    $(el).attr('data-os', os);
                    $(el).attr('data-custom-1', custom_1);
                    $(el).attr('data-custom-2', custom_2);
                    $(el).attr('data-browser', browser);

                    //initialise VIP members
                    awardCrownToVIPMembers();

                });

            });

            //search through database of subs
            $(document).on('keyup blur', '#search_bar', function () {

                //if empty list exists, quit the script
                if ($('#row_list empty_list').length > 0) {

                    return false;

                }

                //else clear the timeout
                window.clearTimeout(timer);
                $('#export_csv').attr('name', 'export_list_all');
                $('#export_csv').attr('value', 'export all');

                $('#searched_value').val('');

                //vars
                input_email = $(this).val();
                list_name = 'Deporte';

                //if input email is empty
                if (input_email == '') {

                    //clear time out
                    window.clearTimeout(timer);

                    //remove  and show data
                    $('.searched, .search_loader, .search_delete, .searchedList').remove();
                    $('.subscriber_result_today li').removeClass('hideList').show();

                    $('#no_subs').remove();
                    $('#add_cell_pic').html('&nbsp;');

                    $('#search_bar_result').show().text('');

                    //stop the script
                    return false;

                }

                //timer variable
                timer = window.setTimeout(function () {

                    //show search indicator
                    $('.search_delete').remove();
                    $('.indicator_today_email_address').prepend('<div class="search_loader"><img src="../../img/icons/loader_search.gif" width="100%"></div>')

                    search_type = $('[data-search]').attr('data-search');

                    //AJAX
                    $.ajax({
                        type: "POST",
                        dataType: "html",
                        url: "../../scripts/calls.php?func=search_subscriber_analytics",
                        data: { input_email: input_email, list_name: list_name, search_type: search_type }
                    }).done(function (data) {

                        // if data returns empty
                        if (data == 'empty') {

                            //clear timeout
                            window.clearTimeout(timer);

                            //show zero results
                            $('#add_cell_pic').text('0');

                            //if input email is empty, while the script has fetched
                            if (input_email == '') {

                                return false;

                            }

                            //if subs has over 0
                            if ($('#no_subs').length > 0) {

                            }

                                //else
                            else {

                                count_subs_list = $('.tabs_all_active').text();

                                $('.subscriber_result_today').prepend('<div id="no_subs"></div>');
                                $('.searched').remove();
                                $('.subscriber_result_today li').addClass('hideList').hide();
                                $('#search_bar_result').show().text('0/' + count_subs_list);

                            }

                        }

                            //else if data reutns not empty
                        else {

                            //clear timer
                            window.clearTimeout(timer);
                            $('#export_csv').attr('name', 'export_list_search');
                            $('#export_csv').attr('value', 'export search');
                            $('#searched_value').closest('form').attr('action', '../../scripts/calls.php?func=export_list_search&list_name=' + list_name)

                            $('#searched_value').val(input_email);

                            //show/hide data
                            $('.searched, #no_subs').remove();
                            $('.subscriber_result_today li').hide();
                            $('.searchedList').remove();
                            $('.subscriber_result_today').append(data);

                            //initialise gravatars
                            initialiseSubscriberGravatars();

                            //variables
                            count_subs = $('.subscriber_result li:visible').size();
                            count_subs_list = $('.tabs_all_active').text();

                            $('#search_bar_result').show().text(count_subs + '/' + count_subs_list);

                            //initialise VIP members
                            awardCrownToVIPMembers();

                        }

                        //add delete
                        $('.indicator_today_email_address').prepend('<div class="search_delete"></div>')
                        $('.search_loader').remove();

                    });

                }, 500);

            })

            //add subscriber
            $(document).on('click', '#add_subscriber, .add_subscriber_open', function () {

                //popup attr
                btnTrue = 'Add subscriber';
                btnTrueId = 'add_subscriber_data';

                btnFalse = 'Cancel';

                //custom html
                customHtml = '<div id="sub_data_info" class="bold"><img src="https://www.stampready.net/dashboard/img/framework/default.png">Add subscriber</div><ul class="data_change clear-fix"><li class="clear-fix"><div class="data_name semi_bold" style="background-image: url(../../img/icons/data_name_name@2x.png)">Name</div><div class="data_value"><input type="text" value="" data-sub-val="name" class="regular goFocus"></div></li><li class="clear-fix"><div class="data_name semi_bold" style="background-image: url(../../img/icons/data_name_email@2x.png)">Email</div><div class="data_value"><input type="text" value="" data-sub-val="email" class="regular add_email_gravatar"></div></li><li class="clear-fix"><div class="data_name semi_bold" style="background-image: url(../../img/icons/data_name_date@2x.png)">Date Added</div><div class="data_value"><input type="text" value="' + date + '" data-sub-val="date" class="regular"></div></li><li class="clear-fix"><div class="data_name semi_bold" style="background-image: url(../../img/icons/data_name_list@2x.png)">List Name</div><div class="data_value"><input type="text" value="' + list_name + '" data-sub-val="list" class="regular"></div></li><li class="clear-fix"><div class="data_name semi_bold" style="background-image: url(../../img/icons/data_name_country@2x.png)">Country</div><div class="data_value"><input type="text" value="" data-sub-val="country" class="regular"></div></li><li class="clear-fix"><div class="data_name semi_bold" style="background-image: url(../../img/icons/data_name_referrer@2x.png)">Referrer</div><div class="data_value"><input type="text" value="" data-sub-val="referrer" class="regular"></div></li><li class="clear-fix"><div class="data_name semi_bold" style="background-image: url(../../img/icons/data_name_os@2x.png)">Os</div><div class="data_value"><input type="text" value="" data-sub-val="os" class="regular"></div></li><li class="clear-fix"><div class="data_name semi_bold" style="background-image: url(../../img/icons/data_name_browser@2x.png)">Browser</div><div class="data_value"><input type="text" value="" data-sub-val="browser" class="regular"></div></li><li class="clear-fix"><div class="data_name semi_bold" style="background-image: url(../../img/icons/data_name_custom1@2x.png)">Custom 1</div><div class="data_value"><input type="text" value="" data-sub-val="custom_1" class="regular"></div></li><li class="clear-fix"><div class="data_name semi_bold" style="background-image: url(../../img/icons/data_name_custom2@2x.png)">Custom 2</div><div class="data_value"><input type="text" value="" data-sub-val="custom_2" class="regular"></div></li></ul>';

                //open popup
                openPopup();

                //little hack to unblur inpur field
                $(':focus').blur();

                setTimeout(function () {

                    $('.goFocus').focus();

                }, 100)

            });

            $(document).on('click', '#add_subscriber_data', function () {

                //fetched vars
                name = $('[data-sub-val="name"]').val();
                email = $('[data-sub-val="email"]').val();
                date = $('[data-sub-val="date"]').val();
                list = $('[data-sub-val="list"]').val();
                country = $('[data-sub-val="country"]').val();
                referrer = $('[data-sub-val="referrer"]').val();
                os = $('[data-sub-val="os"]').val();
                browser = $('[data-sub-val="browser"]').val();
                custom_1 = $('[data-sub-val="custom_1"]').val();
                custom_2 = $('[data-sub-val="custom_2"]').val();
                data_switch_thumb = $('[data-switch-thumb]');

                $.ajax({
                    type: "POST",
                    dataType: "html",
                    url: "../../scripts/calls.php?func=add_subscriber",
                    data: {
                        name: name, email: email, date: date, list: list, country: country, referrer: referrer, os: os, browser: browser, custom_1: custom_1, custom_2: custom_2
                    }
                }).done(function (data) {

                    if (data == 1) {

                        notificationContent = email + ' already exists in your list';
                        notificationColor = "#ea5a5b";

                        notification();

                    }

                    else if (data == 2) {

                        notificationContent = list + ' does not exists on your account';
                        notificationColor = "#ea5a5b";

                        notification();

                    }

                    else if (data == 3) {

                        notificationContent = email + ' seems invalid';
                        notificationColor = "#ea5a5b";

                        notification();

                    }

                    else if (data == 4) {

                        closePopup();

                        setTimeout(function () {

                            notificationContent = email + ' added to ' + list;
                            notificationColor = "#69c0af";

                            notification();

                            setTimeout(function () {

                                location.reload();

                            }, 1250)

                        }, 250)

                    }

                    else if (data == 5) {

                        notificationContent = email + ' is a blocked subscriber';
                        notificationColor = "#ea5a5b";

                        notification();

                    }

                });

            });

            //import csv script
            $(document).on('click', '.import_csv', function (e) {

                //create popup
                headline = 'Import subscribers by <span>CSV</span>, <span>XLS</span> or <span>XLSX</span> file.';
                paragraph = 'What would you like to do with the imported file?';

                btnTrue = 'Import File';
                btnTrueId = 'import-text-file';

                btnFalse = 'Nevermind';

                dropDownItems = {
                    'import_subscribers[][false]': 'Import email addresses only',
                    'group_subscribers[][false]': 'Segment Subscribers',
                    'block_subscribers[][false]': 'Block Subscribers',
                    'remove_subscribers[][true]': 'Remove Subscribers',
                    'make_vip[][false]': 'Grant Subscribers as VIP Member',
                    'undo_vip[][true]': 'Undo Subscribers as VIP Member'
                };

                openPopup();

            });

            //change list name
            $(document).on('click', '.change_list_name', function () {

                list_name = 'Deporte';

                headline = 'You\'re about to change the name of <span>' + list_name + '</span>';
                paragraph = 'Visitors are able to see the name of your list. Think of a good name.';

                btnTrue = 'Yes, change list name';
                btnTrueId = 'change_list_name_btn';

                btnFalse = 'No, keep it';

                inputField = 'New List Name';
                inputFieldId = 'new_list_name_change';

                openPopup();

            });

            //change list name script
            $(document).on('click', '#change_list_name_btn', function () {

                current_list = 'Deporte';
                new_list = $('#new_list_name_change').val();
                section = $('.activedTab').attr('data-tab-url');

                $.ajax({
                    type: "POST",
                    dataType: "html",
                    url: "../../scripts/calls.php?func=change_list_name",
                    data: { current_list: current_list, new_list: new_list }
                }).done(function (data) {

                    if (data == 'empty') {

                        notificationContent = 'List name can\'t be empty';
                        notificationColor = "#ea5a5b";
                        notification();

                    }

                    else if (data == 'duplicate') {

                        notificationContent = 'That name already exists';
                        notificationColor = "#ea5a5b";
                        notification();
                    }

                    else {

                        new_list = new_list.replace(new RegExp("'", "g"), "");
                        $(location).attr('href', 'http://www.stampready.net/dashboard/subscribers/' + section + '/index.php?list=' + new_list);

                    }

                });

            });

            //change unsubscribe page button
            $(document).on('click', '.change_unsubscribe_page', function () {

                //vars
                list_name = 'Deporte';
                unsubscribe_url = $('#list_name_bar h2').attr('data-unsubscribe-link');

                //popup attr
                headline = 'Set the unsubscribe page for <span>' + list_name + '</span>';
                paragraph = 'When your subscriber unsubscribes from your list, you can redirect them to any page.';

                inputField = 'http://';
                inputFieldId = 'unsubscribe_link_value';
                maxLength = '1024';

                btnTrue = 'Redirect to this url';
                btnTrueId = 'redirect_to_unsubscribe_url';

                btnTrue2 = 'use standard message';
                btnTrueId2 = 'use_standard_unsubscribe_message';

                //open popup
                openPopup();

                //insert current unsubscribe value
                $('#unsubscribe_link_value').val(unsubscribe_url);

            });

            //change unsubscribe page script via AJAX
            $(document).on('click', '#redirect_to_unsubscribe_url', function () {

                //fetch unsubscribe link
                unsubscribe_url = $('#unsubscribe_link_value').val();
                list_name = 'Deporte';

                //give it AJAX
                $.ajax({
                    type: "POST",
                    dataType: "html",
                    url: "../../scripts/calls.php?func=update_unsubscribe_url",
                    data: {
                        unsubscribe_url: unsubscribe_url, list_name: list_name
                    }
                }).done(function (data) {

                    //close popup
                    closePopup();

                    //notification
                    setTimeout(function () {

                        notificationContent = 'Unsubscribe page updated';
                        notificationColor = "#69c0af";
                        notification();

                        $('[data-unsubscribe-link]').attr('data-unsubscribe-link', unsubscribe_url);

                    }, 500)

                });

            });

            //change unsubscribe link to standard SR message
            $(document).on('click', '#use_standard_unsubscribe_message', function () {

                list_name = 'Deporte';
                unsubscribe_url = '';

                //AJAX
                $.ajax({
                    type: "POST",
                    dataType: "html",
                    url: "../../scripts/calls.php?func=update_unsubscribe_url",
                    data: {
                        unsubscribe_url: unsubscribe_url, list_name: list_name
                    }
                }).done(function (data) {

                    //close popup
                    closePopup();

                    //notification
                    setTimeout(function () {

                        notificationContent = 'Reverted to standard message';
                        notificationColor = "#69c0af";
                        notification();

                        $('[data-unsubscribe-link]').attr('data-unsubscribe-link', '');

                    }, 500)

                });

            });

            //vip settings
            $(document).on('click', '.vip_settings', function () {

                vip_quota = $('[data-vip-quota]').attr('data-vip-quota');

                //popup attr
                headline = 'Change VIP quota';
                paragraph = 'How many times does your subscriber need to open your campaign, in order to reach the VIP status? Currently set to <span class="semi_bold" style="color: #4a4a4a;">' + vip_quota + ' visitas</span>';

                inputField = vip_quota;
                inputFieldId = 'vip_quota_value';

                btnTrue = 'Set Quota';
                btnTrueId = 'set_vip_quota';

                btnFalse = 'Cancel';

                //open popup
                openPopup();

            });

            $(document).on('click', '#set_vip_quota', function () {

                vip_val = $('#vip_quota_value').val();
                list_enc = $('[data-listenc]').attr('data-listenc');

                //give it AJAX
                $.ajax({
                    type: "POST",
                    dataType: "html",
                    url: "../../scripts/calls.php?func=update_vip_quota",
                    data: {
                        vip_val: vip_val, list_enc: list_enc
                    }
                }).done(function (data) {

                    if (data == 'error') {

                        notificationContent = ' Something went wrong. Please try again.';
                        notificationColor = "#ea5a5b";

                        notification();

                    }

                    else {

                        cur_url = $(location).attr('href');
                        closePopup();

                        setTimeout(function () {

                            notificationContent = 'VIP quota updated to ' + data;
                            notificationColor = "#69c0af";
                            notification();

                            setTimeout(function () {

                                $(location).attr('href', cur_url);

                            }, 2000)

                        }, 500)

                    }

                    closePopup();

                });

            });

            $(document).on('click', '#import-text-file', function () {

                //vairables
                switch_boolean = 'inactive';
                option = $('[data-dropdown-item-present]').attr('data-dropdown-item-present');
                vip_quota_opens = $('[data-vip-quota]').attr('data-vip-quota');

                if (option == 'group_subscribers') {

                    $('#upload-by-grouping').trigger('click');

                }

                else {

                    //detect if switch is on/off and available
                    if ($('.switch-holder').is(":visible")) { if ($('.switch-holder .switch_thumb').hasClass('active')) { } else { switch_boolean = 'active'; } }

                    $('#images').prop('accept', 'text/csv, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel');
                    $('#images').trigger('click');
                    e.stopPropagation();

                }

            })

            $(document).on('change', '#upload-by-grouping', function () {

                $('#import-by-grouping-form').submit();

            });

            //if scrollheight is, or is higher than the window height
            if (scroll_height > window_height) {

                //on scroll
                $('#mainWrapper').scroll(function () {

                    if (scroll_flag == 1) {

                        return false;

                    }

                    else if ($('.searchedList').length > 0) {

                        return false;

                    }

                    to = $('#mainWrapper').scrollTop();
                    h = $('#mainWrapper').height();

                    tog = to + h + 105;

                    if (tog > scroll_height) {

                        number_subs = $('#row_list li').size();
                        scroll_search = $('.activedTab').attr('data-tab-url');

                        scroll_flag = 1;

                        $.ajax({
                            type: "POST",
                            dataType: "html",
                            url: "../../scripts/calls.php?func=load_subscribers",
                            data: { number_subs: number_subs, list_name: list_name, scroll_search: scroll_search }
                        }).done(function (data) {

                            $('#row_list ul').append(data);

                            initialiseSubscriberGravatars();

                            scroll_flag = 0;

                            //reset scrollheight
                            scroll_height = $('#mainWrapper').prop('scrollHeight');

                        });

                    }

                });

            }

        });
    </script>
</asp:Content>

