$(document).ready(function () {

    //variables
    var timer;
    author_templates = 0;
    disableFlag = 0;
    nameHasChanged = false;
    subscriptionPopupFlag = false;
    subscriptionParameter = getQueryVariable('subscription');
    compare = false;
    date = new Date();
    month = date.getMonth() + 1;
    day = date.getDate();
    date = date.getFullYear() + '/' + (('' + month).length < 2 ? '0' : '') + month + '/' + (('' + day).length < 2 ? '0' : '') + day;
    var timerNav;

    if (subscriptionParameter == 'success') { checkoutPopup('subscribed', '500'); }

    //Slidedown Nav
    $('#nav h2').click(function () {

        if ($(this).next('ul').is(":visible")) { }
        else {

            $('#nav ul').slideUp(200);
            $(this).next('ul').slideDown(200);

        }

    });

    //Overlay hide
    $(document).on('click', '#popupOverlay, #closePopup', function () {
        closePopup();
    });

    //Notification Desktop
    $(document).on('click', '#popup', function (ev) {
        ev.stopPropagation();
    });

    //action dropdown
    $('#action h3').click(function () {

        if ($(this).hasClass('disable')) {

        }

        else {

            if ($(this).closest('div').hasClass('active')) {

                closeNav();

            }

            else {

                $('#action').addClass('active');
                $('#menu_drop_down').slideDown(200);

            }

        }

    });


    //actions on mouse enter and leave
    $(document).on('mouseenter', '#action', function () {

        openNav();

    }).on('mouseleave', '#action', function () {

        closeNav();

    });



    $('#templates li[data-price="STARTER_KIT"], #templates li[data-price="XMAS16"]').each(function () {

        //variables
        subscription = $('#main').attr('data-subscription');

        if (subscription !== 'SUBSCRIPTION') {

            $(this).find('.template').append('<div class="template-ribbon semi_bold">Locked</div>');

        }

    })

    //Mouse enter template
    $('#templates').on('mouseenter', '.template', function () {

        //variables
        name = $(this).parent().attr('data-name');
        price = $(this).parent().attr('data-price');
        available = $(this).parent().attr('data-available');
        demo = $(this).parent().attr('data-demo');
        checkout = $(this).parent().attr('data-checkout');
        subscription = $('#main').attr('data-subscription');
        trial = $('#credits').attr('data-trial');

        //Prepend overlay on template image */
        $(this).prepend('<div class="template_overlay"><div class="name">' + name + '</div><div class="demo">Live Preview</div><div class="buy">Purchase For <span class="price">' + price + '</span></div><div class="availability semi_bold">Available on <span>' + available + '</span></div><div class="remove-imported-template"></div></div>');
        $('.template_overlay').show();
        $(this).find('.price').css('-webkit-transform', 'scale(1)');
        $(this).find('.price').each(function () {

            if ($(this).text().indexOf('FREE') >= 0 || subscription == 'SUBSCRIPTION') {

                if (price == 'FREE' || price == 'XMAS' || price == 'PSD2HTML' || price == 'GIVEAWAY' || price == 'GIVEAWAY_MAY' || price == 'STARTER_KIT' || price == 'XMAS16') {

                    $(this).closest('li').find('.demo').remove();
                    $(this).closest('li').find('.buy').text('View Template').addClass('free');

                }

                else if (price == 'IMPORTED') {

                    $(this).closest('li').find('.demo').remove();
                    $(this).closest('li').find('.buy').text('View Template').addClass('free');
                    $('.availability').html('');
                    $('.template_overlay').find('.remove-imported-template').show();

                }

                else {

                    $(this).closest('li').find('.buy').addClass('target');

                }

            }

            else if ($(this).text().indexOf('IMPORTED') >= 0) {

                $(this).closest('li').find('.demo').remove();
                $(this).closest('li').find('.buy').text('View Template').addClass('free');
                $('.availability').html('');
                $('.template_overlay').find('.remove-imported-template').show();

            }

                // else if( $(this).text().indexOf('XMAS') >= 0) {
                //
                //
                // 	$(this).closest('li').find('.buy').text('Xmas Bundle 2016');
                // 	$('.buy').addClass('view_xmas_gift');
                // 	$('.buy').removeClass('buy');
                // 	$('.availability').html('Free in Xmas Bundle')
                // 	//$(this).parent().parent().find('.buy').removeClass('buy');
                // 	//$(this).parent().parent().find('.buy').text('Subscribe to plan');
                //
                // }

            else if ($(this).text().indexOf('GIVEAWAY_MAY') >= 0) {


                $(this).closest('li').find('.buy').text('StampReady giveaways');
                $('.buy').addClass('view_xmas_gift');
                $('.buy').removeClass('buy');
                $('.availability').html('Comes with a free trial')
                //$(this).parent().parent().find('.buy').removeClass('buy');
                //$(this).parent().parent().find('.buy').text('Subscribe to plan');

            }

            else if ($(this).text().indexOf('PSD2HTML') >= 0) {

                $(this).parent().parent().find('.demo').remove();
                $(this).parent().parent().find('.buy').text('View Template').addClass('free');
                $('.availability').html('Gift from <a href="https://www.psd2html.com" style="color:#FFFFFF;" target="_blank">PSD2HTML</a>')
                //$(this).parent().parent().find('.buy').removeClass('buy');
                //$(this).parent().parent().find('.buy').text('Subscribe to plan');

            }

            else if ($(this).text().indexOf('STARTER_KIT') >= 0) {

                $(this).closest('li').find('.demo').text('Starter Kit');
                $(this).closest('li').find('.demo').addClass('target');

                if (trial == '0') {

                    $(this).closest('li').find('.buy').text('Subscribe to plan').addClass('start_trial');

                }

                else {

                    $(this).closest('li').find('.buy').text('Start Trial').addClass('start_trial');

                }

                $('.availability').html('Available with a <span style="color: #FFFFFF!important;" target="_blank">Subscription</span>')

            }

            else if ($(this).text().indexOf('XMAS16') >= 0) {

                $(this).closest('li').find('.demo').text('Xmas Bundle 2016');
                $(this).closest('li').find('.demo').addClass('target');

                if (trial == '0') {

                    $(this).closest('li').find('.buy').text('Subscribe to plan').addClass('start_trial');

                }

                else {

                    $(this).closest('li').find('.buy').text('Start Trial').addClass('start_trial');

                }

                $('.availability').html('Available with a <span style="color: #FFFFFF!important;" target="_blank">Subscription</span>')

            }

            else {

                $(this).closest('li').find('.buy').addClass('target');

            }

        });

    }).on('mouseleave', 'li', function () {

        //Hide overlay first
        $(this).find('.price .demo').css('-webkit-transform', 'scale(1.1)');
        $('.template_overlay').remove();

    });

    //side actions
    $(document).on('mouseenter', '.actions div:not(.send, .analytics, .view_invoice)', function () {
        //console.log($(this));
        attr = $(this).attr('class');
        str = attr.replace("active", "").replace("_sub", "");
        str = str.replace('_', ' ');
        //console.log(str);
        var lnk = $(this).find("a");
        if (str == "view") {
            if (lnk.attr("href") === undefined)
                $(this).text("ver");
            else
                lnk.text("ver");
        }
        else {
            if (lnk.attr("href") === undefined)
                $(this).text(str);
            else
                lnk.text(str);
        }
        //console.log("---1");
        $(this).addClass('active');
        $('.actions .send, .actions .analytics, .actions .view_invoice').addClass('active');

    }).on('mouseleave', '.actions div', function () {
        //console.log("---2");
        var lnk = $(this).find("a");
        if (lnk.attr("href") === undefined)
            $('.actions div:not(.send, .analytics, .view_invoice)').removeClass('active').text('');
        else {
            lnk.text("");
            $('.actions div:not(.send, .analytics, .view_invoice)').removeClass('active');
        }

        $('.actions .send, .actions .analytics, .actions .view_invoice').removeClass('active');
    });

    //side actions
    $(document).on('mouseenter', '.actions div:not(.send, .analytics, .view_invoice)', function () {

        attr = $(this).attr('class');
        str = attr.replace("active", "").replace("_sub", "");
        str = str.replace('_', ' ');
        console.log(str);
        if (str == "view")
            $(this).text("ver");
        else $(this).text(str);
        console.log("---1");
        $(this).addClass('active');
        $('.actions .send, .actions .analytics, .actions .view_invoice').addClass('active');

    }).on('mouseleave', '.actions div', function () {
        console.log("---2");
        $('.actions div:not(.send, .analytics, .view_invoice)').removeClass('active').text('');
        $('.actions .send, .actions .analytics, .actions .view_invoice').removeClass('active');

    });

    //closePopup
    $(document).on('click', '.cancelPop', function () {

        closePopup();

    });

    //delete campaign
    $(document).on('click', '.delete', function () {

        $(this).parent().parent().addClass('active');

        campaign_name = $(this).parent().parent().find('input[type="text"]').val();

        headline = 'You are about to delete <span>' + campaign_name + '</span>';
        paragraph = 'Your template will be deleted. Once deleted, you can\'t recover it.';

        btnTrue = 'Yes, delete campaign';
        btnTrueId = 'delete_campaign';

        btnFalse = 'No, keep campaign';

        openPopup();


    });

    $(document).on('click', '.scheduled', function () {

        $('#mainWrapper ul li').removeClass('active');
        $(this).closest('li').addClass('active');

        headline = 'This campaign has been scheduled';
        paragraph = 'Although it is scheduled, would you like to send your campaign now?';

        btnTrue = 'Send campaign now';
        btnTrueId = 'send_campaign_now';

        btnFalse = 'Keep it scheduled';

        openPopup();

    });


    //cancel campaign
    $(document).on('click', '.cancel', function () {

        $(this).parent().parent().addClass('active');

        campaign_name = $(this).parent().parent().find('input[type="text"]').val();

        headline = 'Are you sure you want to cancel <span>' + campaign_name + '</span>';
        paragraph = 'Your campaign will be moved to your drafts.';

        btnTrue = 'Cancel Scheduled Campaign';
        btnTrueId = 'cancel_scheduled_campaign';

        btnFalse = 'Nevermind';

        openPopup();

    });

    $(document).on('click', '.duplicate', function () {

        $(this).parent().parent().addClass('active');

        previous_name = $(this).parent().parent().find('input[type="text"]').val();

        headline = 'You are about to duplicate <span>' + previous_name + '</span>';
        paragraph = 'The duplicated campaign will be added to the top of your Drafts.';

        btnTrue = 'Duplicate campaign';
        btnTrueId = 'create_duplicate_campaign';

        btnFalse = 'Cancel';

        inputField = 'Campaign Name';
        inputFieldId = 'duplicate_campaign_name';

        openPopup();

    });

    $(document).on('click', '.select_all_checkboxes span', function () {

        if ($(this).hasClass('active')) {

            $('.select_all_checkboxes span').text('Select All');
            $('.select_all_checkboxes span').removeClass('active')

            $('#drafts, #sent, #edit-urls-images, #live_previews').find('li').each(function () {

                $(this).find('input[type="checkbox"]').attr('checked', false);
                $(this).find('a').removeClass('checked');
                $('.campaigns_action, .campaigns_action h3').addClass('disable');

            })

        }

        else {

            $('.select_all_checkboxes span').text('Deselect All')
            $('.select_all_checkboxes span').addClass('active');

            $('#drafts, #sent, #edit-urls-images, #live_previews').find('li').each(function () {

                $(this).find('input[type="checkbox"]').attr('checked', true);
                $(this).find('a').addClass('checked');
                $('.campaigns_action, .campaigns_action h3').removeClass('disable');

            })

        }

    });



    $('#account_view input[type="text"], #account_view input[type="password"]').keypress(function () {

        $('#update_details').removeClass('button').addClass('button_active');

    });

    $('.delete_account a').click(function () {

        if ($('.cancel_paypal_plan').length > 0) {

            $('#info_layer h2, #info_layer p').hide();
            $('#info_layer [data-type="delete_paypal"]').show();
            $('#info_layer').fadeIn(200);
            $('.login_paypal').closest('p').hide();
            return false;

        }

        headline = 'Did we screw up?';
        paragraph = 'To delete your account, you need to verify your password.';

        btnFalse = 'No, keep my account';
        invert = true;

        customHtml = '<form action="https://www.stampready.net/dashboard/scripts/calls.php?func=delete_account" method="post"><input type="password" name="password" placeholder="Current Password"><input type="submit" name="delete_account" class="btnTrue semi_bold" value="Remove My Account" style="left: 0px;"></form>'

        openPopup();


    });

    //Enter amount of credits
    $('#define_amount').keyup(function () {

        //vars
        val = $(this).val();
        val = val.toString().replace(/\./g, '');
        original = val * 0.02;

        //If under 5000
        if (val < 5000) {

            price = (val * 0.02).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
            bare = val * 0.02;
            save = (original - bare).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');

        }

            //Else above 4999 and below 49999
        else if (val > 4999 && val < 50000) {

            price = (val * 0.015).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
            bare = val * 0.015;
            save = (original - bare).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');

        }

            //Else above 49999 and below 199999
        else if (val > 49999 && val < 200000) {

            price = (val * 0.01).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
            bare = val * 0.01;
            save = (original - bare).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');

        }

            //Else above 199999 and below 1999999
        else if (val > 199999 && val < 2000000) {

            price = (val * 0.005).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
            bare = val * 0.005;
            save = (original - bare).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');

        }

            //Else
        else {

            //vars
            price = (val * 0.002).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
            bare = val * 0.002;
            save = (original - bare).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');

        }

        $('#define_amount_price').text('$' + price);
        $('#define_amount_savings').text('$' + save);

    });


    $('.credits_list').on('mouseenter', '.clear-fix', function (e) {

        $('.cell_buy_active').attr('class', 'cell_buy semi_bold');
        $(this).find('.cell_buy').attr('class', 'cell_buy_active semi_bold');

    }).on('mouseleave', 'li', function (e) {

        $('.cell_buy_active').attr('class', 'cell_buy semi_bold');

    });

    $('#credits_bar .actions').on('click', '.purchase_plan', function (e) {

        amount = $('#define_plan3 span').text().toString().replace(/\,/g, '');

        $(location).attr('href', 'https://www.stampready.net/dashboard/checkout/index.php?plan=sr_plan' + amount);

    });

});

$(document).ready(function () {

    //variables
    templateAnimationSpeed = 100;
    flag = 0;
    windowHeight = $(window).height();
    notificationTime = 2500;
    errorPointTime = 2500;
    registerFeaturesDelay = 250;
    registerFeaturesAnimationTime = 250;
    chartFlag = false;
    entourageFlag = false;
    count = 1;

    //if there's an action
    action = getQueryVariable('action');

    //if action is new password
    if (action == 'reset_password') {

        setTimeout(function () {

            $('#popup-wrapper').css('width', '340px').css('height', '480px');

            //modify
            modifyPopup('new password');

            //open popup
            openPopup();

        }, 750);

    }

    //if action is new password
    if (action == 'login') {

        setTimeout(function () {

            //$('#popup-wrapper').css('width','340px').css('height','480px');

            //modify
            modifyPopup('login');

            //open popup
            openPopup();

        }, 750);

    }

    //if action is new password
    if (action == 'register') {

        setTimeout(function () {

            //$('#popup-wrapper').css('width','340px').css('height','480px');

            //modify
            modifyPopup('register');

            //open popup
            openPopup();

        }, 750);

    }

    //initialise starter kit templates
    setTimeout(function () {

        $('[data-temp]').each(function () {

            //variables
            temp_id = $(this).attr('data-temp');
            templateAnimationSpeed = templateAnimationSpeed + 100;

            initialiseStarterKitTemplates(temp_id, templateAnimationSpeed);

        })

    }, 1000);

    //record when the chart should animate
    $('.start_donut').waypoint(function (direction) {

        //if chart has not yet animated, animate it
        if (!chartFlag) {

            //fade in the chart
            $('#analytics .hidden').fadeIn(1000);

            //chart parameters
            var doughnutData = [
				{
				    value: 300, color: "#69c0af", highlight: "#5ca999", label: "Delivered"
				}, {
				    value: 100, color: "#cd6d67", highlight: "#b65a54", label: "Hard Bounces"
				}, {
				    value: 50, color: "#847f9f", highlight: "#767192", label: "Soft Bounces"
				}, {
				    value: 10, color: "#57a2d6", highlight: "#468ec1", label: "Rejects"
				}

            ];

            //chart 2 parameters
            var doughnutData2 = [
				{
				    value: 50, color: "#5ecce5", highlight: "#4fbcd5", label: "Opens"
				}, {
				    value: 200, color: "#50617d", highlight: "#394964", label: "Clicks"
				}
            ];

            //define charts
            var ctx = document.getElementById("chart-area").getContext("2d");
            var ctx2 = document.getElementById("chart-area2").getContext("2d");
            window.myDoughnut = new Chart(ctx).Doughnut(doughnutData, { responsive: false });
            window.myDoughnut = new Chart(ctx2).Doughnut(doughnutData2, { responsive: false });

            //set chartFlag boolean to true
            chartFlag = true;

        }

    });

    //entourage list animation trigger
    $('#developers_right').waypoint(function (direction) {

        //if it hasn't started animating yet, animate it
        if (!entourageFlag) {

            startAnimateList();

        }

    });

    $(document).on('keypress', '.input-field', function (e) {

        if (e.which == 13) {

            $('#popup .submit-btn').trigger('click');

        }

    });

    //check register form
    $(document).on('keyup', '.input-email-register, .input-username-register, .input-password-register', function () {

        checkFormRequirements();

    });

    $(document).on('click', '.terms', function () {

        setTimeout(function () {

            checkFormRequirements();

        }, 100)

    });

    //forget password
    $(document).on('click', '.forget-password', function () {

        if ($(this).hasClass('back-to-login')) {

            modifyPopup('login');

            $('.input-username-email-login').focus();

        }

        else {

            modifyPopup('forget password');

        }

    });

    $(document).on('click', '.confirm-new-password:not(.disabled)', function () {

        //variables
        var password = $('.input-new-password').val();
        var token = getQueryVariable('token');
        var email = getQueryVariable('email');
        var el = $(this);

        $(el).addClass('disabled');

        //ajax connection
        $.ajax({
            type: "POST",
            dataType: "html",
            url: "dashboard/scripts/calls.php?func=verify_password",
            data: { password: password, token: token }
        }).done(function (data) {

            if (data == 'error token') {

                message = 'The token to renew your password has been expired';
                type = 'error';

                $(el).removeClass('disabled');

            }

            else if (data == 'need more characters') {

                message = 'Your password need more characters';
                type = 'error';

                $(el).removeClass('disabled');

            }

            else if (data == 'success') {

                message = 'Your password has been updated';
                type = 'success';

                $(el).addClass('disabled');

                setTimeout(function () {

                    //set values
                    $('.input-username-email-login').val(email);
                    $('.input-password-login').val(password);

                    login();

                }, 2500)


            }

            notification(message, type);

        });
    });


    $(document).on('click', '.confirm-forget-password:not(.disabled)', function () {

        //variables
        var email = $('.input-username-email-login').val();
        var el = $(this);

        $(el).addClass('disabled');

        //ajax connection
        $.ajax({
            type: "POST",
            dataType: "html",
            url: "dashboard/scripts/calls.php?func=forget_password",
            data: { email: email }
        }).done(function (data) {

            if (data == 'success') {

                message = 'Reset email has been sent';
                type = 'success';

                $(el).text('Email sent');
                $(el).addClass('disabled');

                setTimeout(function () {

                    $(el).removeClass('disabled');
                    $(el).text('Recover password');

                }, 3500)

            }

            else {

                message = 'Email address does not exist';
                type = 'error';

                $(el).removeClass('disabled');

            }

            notification(message, type);

        });

    })

    //remember me
    $(document).on('click', '.popup-checkbox, .popup-checkbox-text', function () {

        //variables
        var el = $(this).closest('label').find('.popup-checkbox');

        if ($(el).hasClass('active')) {

            $(el).removeClass('active');

        }

        else {

            $(el).addClass('active');

        }

    });

    $(document).keyup(function (e) {

        if (e.keyCode === 27) {

            closePopup();

        }

    });

    //close popup
    $(document).on('click', '#popupoverlay', function () {

        closePopup();

    });

    //ignore close popup
    $(document).on('click', '#popup-wrapper', function (e) {

        e.stopPropagation();

    });

    //login
    $(document).on('click', '#login_btn', function () {

        openLogin();

    });

    //submit
    $(document).on('click', '.confirm-login:not(.disabled)', function () {
        console.log("-- call login");
        login();
    });

    $(document).on('click', '.confirm-register:not(.disabled)', function () {

        $(this).text('Creating account..').addClass('disabled');

        register();

    });

    $(document).on('click', '.disabled:not(.confirm-forget-password, .confirm-new-password)', function () {

        detectFormRequirements();

    });

    //register
    $(document).on('click', '#register_btn', function () {

        openRegister();

    });

    $(document).on('keyup', '.input-username-email-login', function () {

        checkForgetPasswordRequirements();

    })

    $(document).on('keyup', '.input-new-password', function () {

        checkNewPasswordRequirements();

    })


    //Enter amount of credits
    $('#define_amount').keyup(function () {

        //vars
        val = $(this).val();
        val = val.toString().replace(/\./g, '');
        original = val * 0.02;

        //If under 5000
        if (val < 5000) {

            price = (val * 0.02).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
            bare = val * 0.02;

        }

            //Else above 4999 and below 49999
        else if (val > 4999 && val < 50000) {

            price = (val * 0.015).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
            bare = val * 0.015;

        }

            //Else above 49999 and below 199999
        else if (val > 49999 && val < 200000) {

            price = (val * 0.01).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
            bare = val * 0.01;

        }

            //Else above 199999 and below 1999999
        else if (val > 199999 && val < 2000000) {

            price = (val * 0.005).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
            bare = val * 0.005;

        }

            //Else
        else {

            //vars
            price = (val * 0.002).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
            bare = val * 0.002;

        }

        $('#define_amount_price').text('$' + price);

    });

    //Enter amount of credits
    $('#define_amount_plan').keyup(function () {

        calcPrices();

    });

    $(document).on('keypress', '#define_amount', function () {

        $('.result').slideDown();

    });

    $(document).on('keypress', '#define_amount_plan', function () {

        $('.result2').slideDown();

    });

    $(window).scroll(function () {

        if ($(window).scrollTop() + $(window).height() < $(document).height() - 60) {

            $('.slogan').show();
            $('#register-logo').hide();

        } else {

            $('.slogan').hide();
            $('#register-logo').show();

        }

    });

    //Allow only numbers
    $(".numericOnly").keypress(function (event) {


        if (String.fromCharCode(event.which).match(/[^,.0-9]/g))
            return false;

    });

});


function calcPrices() {

    //vars
    if (flag == 0) {

        val = $('#list_name_bar h2 span b').text();
        val = val.toString().replace(/\./g, '').replace(/\,/g, '');

    }

    else {

        val = $('#define_amount_plan').val();
        val = val.toString().replace(/\./g, '').replace(/\,/g, '');
        $('.hide_it').show();
        $('.result2 .contact_us').hide();


    }

    original = val * 0.02;

    //subscribers
    if (val == 0) { val = 0; savings = 2500; }
    else if (val > 0 && val < 501) { val = 500; savings = 2500; }
    else if (val > 500 && val < 1001) { val = 1000; savings = 2500; }
    else if (val > 1000 && val < 2501) { val = 2500; savings = 10000; }
    else if (val > 2500 && val < 3001) { val = 3000; savings = 10000; }
    else if (val > 3000 && val < 3501) { val = 3500; savings = 10000; }
    else if (val > 3500 && val < 4001) { val = 4000; savings = 10000; }
    else if (val > 4000 && val < 4501) { val = 4500; savings = 10000; }
    else if (val > 4500 && val < 5001) { val = 5000; savings = 10000; }
    else if (val > 5000 && val < 6001) { val = 6000; savings = 10000; }
    else if (val > 6000 && val < 7001) { val = 7000; savings = 10000; }
    else if (val > 7000 && val < 10001) { val = 10000; savings = 25000; }
    else if (val > 10000 && val < 11001) { val = 11000; savings = 25000; }
    else if (val > 11000 && val < 12001) { val = 12000; savings = 25000; }
    else if (val > 12000 && val < 13001) { val = 13000; savings = 25000; }
    else if (val > 13000 && val < 14001) { val = 14000; savings = 25000; }
    else if (val > 14000 && val < 15001) { val = 15000; savings = 25000; }
    else if (val > 15000 && val < 16001) { val = 16000; savings = 25000; }
    else if (val > 16000 && val < 17001) { val = 17000; savings = 25000; }
    else if (val > 17000 && val < 25001) { val = 25000; savings = 50000; }
    else if (val > 25000 && val < 26001) { val = 26000; savings = 50000; }
    else if (val > 26000 && val < 27001) { val = 27000; savings = 50000; }
    else if (val > 27000 && val < 28001) { val = 28000; savings = 50000; }
    else if (val > 28000 && val < 29001) { val = 29000; savings = 50000; }
    else if (val > 29000 && val < 30001) { val = 30000; savings = 50000; }
    else if (val > 30000 && val < 31001) { val = 31000; savings = 50000; }
    else if (val > 31000 && val < 32001) { val = 32000; savings = 50000; }
    else if (val > 32000 && val < 33001) { val = 33000; savings = 50000; }
    else if (val > 33000 && val < 34001) { val = 34000; savings = 50000; }
    else if (val > 34000 && val < 35001) { val = 35000; savings = 50000; }
    else if (val > 35000 && val < 36001) { val = 36000; savings = 50000; }
    else if (val > 36000 && val < 37001) { val = 37000; savings = 50000; }
    else if (val > 37000 && val < 38001) { val = 38000; savings = 50000; }
    else if (val > 38000 && val < 39001) { val = 39000; savings = 50000; }
    else if (val > 39000 && val < 40001) { val = 40000; savings = 50000; }
    else if (val > 40000 && val < 41001) { val = 41000; savings = 50000; }
    else if (val > 41000 && val < 50001) { val = 50000; savings = 75000; }
    else if (val > 50000 && val < 52001) { val = 52000; savings = 75000; }
    else if (val > 52000 && val < 54001) { val = 54000; savings = 75000; }
    else if (val > 54000 && val < 56001) { val = 56000; savings = 75000; }
    else if (val > 56000 && val < 58001) { val = 58000; savings = 75000; }
    else if (val > 58000 && val < 60001) { val = 60000; savings = 75000; }
    else if (val > 60000 && val < 62001) { val = 62000; savings = 75000; }
    else if (val > 62000 && val < 64001) { val = 64000; savings = 75000; }
    else if (val > 64000 && val < 66001) { val = 66000; savings = 75000; }
    else if (val > 66000 && val < 68001) { val = 68000; savings = 75000; }
    else if (val > 68000 && val < 70001) { val = 70000; savings = 75000; }
    else if (val > 70000 && val < 75001) { val = 75000; savings = 200000; }
    else if (val > 75000 && val < 80001) { val = 80000; savings = 200000; }
    else if (val > 80000 && val < 85001) { val = 85000; savings = 200000; }
    else if (val > 85000 && val < 90001) { val = 90000; savings = 200000; }
    else if (val > 90000 && val < 95001) { val = 95000; savings = 200000; }
    else if (val > 95000 && val < 100001) { val = 100000; savings = 200000; }
    else if (val > 100000 && val < 105001) { val = 105000; savings = 200000; }
    else if (val > 105000 && val < 110001) { val = 110000; savings = 200000; }
    else if (val > 110000 && val < 115001) { val = 115000; savings = 200000; }
    else if (val > 115000 && val < 120001) { val = 120000; savings = 200000; }
    else if (val > 120000 && val < 125001) { val = 125000; savings = 200000; }
    else if (val > 125000 && val < 130001) { val = 130000; savings = 200000; }
    else if (val > 130000 && val < 135001) { val = 135000; savings = 200000; }
    else if (val > 135000 && val < 140001) { val = 140000; savings = 200000; }
    else if (val > 140000 && val < 145001) { val = 145000; savings = 200000; }
    else if (val > 145000 && val < 150001) { val = 150000; savings = 200000; }
    else if (val > 150000 && val < 155001) { val = 155000; savings = 200000; }
    else if (val > 155000 && val < 160001) { val = 160000; savings = 200000; }
    else if (val > 160000 && val < 165001) { val = 165000; savings = 200000; }
    else if (val > 165000 && val < 170001) { val = 170000; savings = 200000; }
    else if (val > 170000 && val < 175001) { val = 175000; savings = 200000; }
    else if (val > 175000 && val < 180001) { val = 180000; savings = 200000; }
    else if (val > 180000 && val < 185001) { val = 185000; savings = 200000; }
    else if (val > 185000 && val < 190001) { val = 190000; savings = 200000; }
    else if (val > 190000 && val < 200001) { val = 200000; }
    else if (val > 200000) { /*  val = 4500; */ }

    mails = (val * 10).toFixed(2);
    mails_t = mails.replace('.00', '');

    mails_savings = (savings * 10).toFixed(2);
    mails_savings_t = mails_savings.replace('.00', '');


    if (val < 1) {

        val = 500;
        save = 0;
        price_savings_one = 24;
        save_savings = 25;

        mails = (val * 10).toFixed(2);
        mails_t = mails.replace('.00', '');

        mails_savings = (savings * 10).toFixed(2);
        mails_savings_t = mails_savings.replace('.00', '');

        price = (val * 0.02).toFixed(2);
        price_one = price - 1;

    }

        //If under 5000
    else if (val > 1 && val < 2500) {

        price = (val * 0.02).toFixed(2);
        price_one = price - 1;

        price_original = (val * 0.02).toFixed(2);
        price_original_one = price_original - 1;

        price_savings = (savings * 0.01).toFixed(2);
        price_savings_one = price_savings - 1;

        price_savings_original = (savings * 0.02).toFixed(2);
        price_savings_original_one = price_savings_original - 1;
        price_savings_original_one = Math.ceil(price_savings_original_one);

        save = (price_original_one - price_one).toFixed(2);
        save = Math.ceil(save);

        save_savings = (price_savings_original_one - price_savings_one).toFixed(2);
        save_savings = Math.ceil(save_savings);

    }

        //Else above 4999 and below 49999
    else if (val > 2499 && val < 10000) {

        price = (val * 0.010).toFixed(2);
        price_one = price - 1;

        price_original = (val * 0.02).toFixed(2);
        price_original_one = price_original - 1;

        price_savings = (savings * 0.0075).toFixed(2);
        price_savings_one = price_savings - 1;

        price_savings_original = (savings * 0.02).toFixed(2);
        price_savings_original_one = price_savings_original - 1;
        price_savings_original_one = Math.ceil(price_savings_original_one);

        save = (price_original_one - price_one).toFixed(2);
        save = Math.ceil(save);

        save_savings = (price_savings_original_one - price_savings_one).toFixed(2);
        save_savings = Math.ceil(save_savings);

    }

        //Else above 49999 and below 199999
    else if (val > 9999 && val < 25000) {

        price = (val * 0.0075).toFixed(2);
        price_one = price - 1;
        price_one = Math.ceil(price_one);

        price_original = (val * 0.02).toFixed(2);
        price_original_one = price_original - 1;

        price_savings = (savings * 0.0054).toFixed(2);
        price_savings_one = price_savings - 1;
        price_savings_one = Math.ceil(price_savings_one);

        price_savings_original = (savings * 0.02).toFixed(2);
        price_savings_original_one = price_savings_original - 1;
        price_savings_original_one = Math.ceil(price_savings_original_one);

        save = (price_original_one - price_one).toFixed(2);
        save = Math.ceil(save);

        save_savings = (price_savings_original_one - price_savings_one).toFixed(2);
        save_savings = Math.ceil(save_savings);

    }

        //Else above 49999 and below 199999
    else if (val > 24999 && val < 50000) {

        price = (val * 0.0054).toFixed(2);
        price_one = price - 1;
        price_one = Math.ceil(price_one);

        price_original = (val * 0.02).toFixed(2);
        price_original_one = price_original - 1;

        price_savings = (savings * 0.0045).toFixed(2);
        price_savings_one = price_savings - 1;
        price_savings_one = Math.ceil(price_savings_one);

        price_savings_original = (savings * 0.02).toFixed(2);
        price_savings_original_one = price_savings_original - 1;
        price_savings_original_one = Math.ceil(price_savings_original_one);

        save = (price_original_one - price_one).toFixed(2);
        save = Math.ceil(save);

        save_savings = (price_savings_original_one - price_savings_one).toFixed(2);
        save_savings = Math.ceil(save_savings);

    }

        //Else above 49999 and below 199999
    else if (val > 49999 && val < 75000) {

        price = (val * 0.0045).toFixed(2);
        price_one = price - 1;
        price_one = Math.ceil(price_one);

        price_original = (val * 0.02).toFixed(2);
        price_original_one = price_original - 1;

        price_savings = (savings * 0.00427).toFixed(2);
        price_savings_one = price_savings - 1;
        price_savings_one = Math.ceil(price_savings_one);

        price_savings_original = (savings * 0.02).toFixed(2);
        price_savings_original_one = price_savings_original - 1;
        price_savings_original_one = Math.ceil(price_savings_original_one);

        save = (price_original_one - price_one).toFixed(2);
        save = Math.ceil(save);

        save_savings = (price_savings_original_one - price_savings_one).toFixed(2);
        save_savings = Math.ceil(save_savings);



    }

        //Else above 49999 and below 199999
    else if (val > 74999 && val < 190001) {

        price = (val * 0.00427).toFixed(2);
        price_one = price - 1;
        price_one = Math.ceil(price_one);

        price_original = (val * 0.02).toFixed(2);
        price_original_one = price_original - 1;

        price_savings = (savings * 0.00413).toFixed(2);
        price_savings_one = price_savings - 1;
        price_savings_one = Math.ceil(price_savings_one);

        price_savings_original = (savings * 0.02).toFixed(2);
        price_savings_original_one = price_savings_original - 1;
        price_savings_original_one = Math.ceil(price_savings_original_one);

        save = (price_original_one - price_one).toFixed(2);
        save = Math.ceil(save);

        save_savings = (price_savings_original_one - price_savings_one).toFixed(2);
        save_savings = Math.ceil(save_savings);

        // $('.orangeColor .plan_price_amount').closest('.table_footer').prepend('<div class="contact_us semi_bold"><a style="color: #ea6b3e;" href="mailto:info@stampready.net?subject=Custom plan">CONTACT US</a></div>');

    }

        //Else above 199999 and below 1999999
    else {

        $('.hide_it').hide();
        $('.define_amount_result').text('Custom plan');
        $('.result2 .contact_us').show();

        return false;


    }


    $('.plan_price_amount').text('$' + price_one);
    $('.define_amount_result').text(val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));

    flag = 1;

};

function addAvatar() {

    var formattedDate = new Date();
    var d = formattedDate.getDate();
    var m = formattedDate.getMonth();
    m += 1;  // JavaScript months are 0-11
    var y = formattedDate.getFullYear();

    if (count == 1) { sub_email = 'vinnie@chase.com'; sub_name = 'Vincent Chase'; }
    if (count == 2) { sub_email = 'ari@gold.com'; sub_name = 'Ari Gold'; }
    if (count == 3) { sub_email = 'e@suit.com'; sub_name = 'Eric Murphy'; }
    if (count == 4) { sub_email = 'turtle@avion.com'; sub_name = 'Turtle'; }
    if (count == 5) { sub_email = 'drama@victory.com'; sub_name = 'Johnny Chase'; }
    if (count == 6) { sub_email = 'mrsari@flay.com'; sub_name = 'Mrs Ari'; }
    if (count == 7) { sub_email = 'sloan@mcquewick.com'; sub_name = 'Sloan McQuewick'; }
    if (count == 8) { sub_email = 'lloyd@millergold.com'; sub_name = 'Lloyd Lee'; }
    if (count == 9) { sub_email = 'billy@walsh.com'; sub_name = 'Billy Walsh'; }
    if (count == 10) { sub_email = 'dom@shrek.com'; sub_name = 'Dom'; }

    $('#subscribers').append('<li class="hidden"><div class="subs_avatar"><img src="img/' + count + '.jpg"></div><div class="subs_email">' + sub_email + '</div><div class="subs_name">' + sub_name + '</div><div class="subs_date">' + m + "/" + d + "/" + y + '</div></li>');
    $('#subscribers li').slideDown();

    count++;

}

function isEmail(emailV) {
    if (emailV != null && emailV != undefined) {
        var pattern = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
        return pattern.test(emailV);
    }
    else {
        return false;
    }

}

function startAnimateList() {

    entourageFlag = true;

    setInterval(function () {

        if (count > 10) {

            $('#subscribers li').slideUp();

            setTimeout(function () {

                $('#subscribers li').remove();
                count = 1;

            }, 1000);

        }

        else {

            addAvatar();

        }

    }, 2000);

}

function initialiseStarterKitTemplates(temp_id, templateAnimationSpeed) {

    setTimeout(function () {

        $('[data-temp="' + temp_id + '"]').animate({

            'top': '0px'

        }, {
            duration: 700, easing: 'easeOutBack', complete: function () {

            }
        });

    }, templateAnimationSpeed)

}

function openLogin() {

    //modify popup
    modifyPopup('login');

    //open popup
    openPopup();

}

function closePopup() {

    $('#popupoverlay').css({
        'opacity': '0',
    });

    $('#popup-wrapper').css({
        'opacity': '0',
        'transform': 'translateY(-50%) scale(0.8)'
    });

    $('.stack').css({
        'transform': 'scale(1)'
    });

    setTimeout(function () {

        //empty input fields
        $('.input-field').val('');

        $('#popupoverlay').hide();

        //remove height and overlow hidden from stack
        $('.stack, body').removeAttr('style');

        $('body').css({
            'background-color': '#FFF'
        });

    }, 400);

}

//notification
/*
if(data == 'incorrect'){ 
		
			message = 'Username, email or password incorrect';
			type = 'error';
			
		}
		
		if(data == 'banned'){ 
		
			message = 'You\'ve been banned';
			type = 'error';
			
		}
		
		if(data == 'error'){ 
			
			message = 'Something went wrong';
			type = 'error';
			
		}
		
		if(data == 'success'){ 
			
			$(location).attr('href','dashboard/');
			return false;
			
		}
		
		notification(message, type);
*/
function notification(message, type) {

    console.log(message);
    console.log(type);
    //modify notification
    $('.popup-notification').text(message);
    $('.popup-notification').removeClass('error').removeClass('success');
    $('.popup-notification').addClass(type);

    $('.popup-notification').slideDown(200);

    setTimeout(function () {

        $('.popup-notification').slideUp(200);

    }, notificationTime)

}

//login
function login() {

    console.log("------->")
    //variables
    var email = $('.input-username-email-login').val();
    var password = $('.input-password-login').val();
    var keep_logged = 0;

    //fetch keep me logged
    if ($('.popup-checkbox').hasClass('active')) {

        var keep_logged = 1;

    }


    return false;
}

//open register
function openRegister() {

    //modify popup
    modifyPopup('register');

    openPopup();

}

//modify popup
function modifyPopup(type) {

    $('.popup-checkbox').removeClass('active');

    if (type == 'register') {

        $('#login-popup-information li').css({
            'opacity': '0',
            'padding': '0 0 0 30px'
        })

        $('#popup-wrapper').css('width', '670px').css('height', '544px');

        $('.input-email-register, .input-username-register, .input-password-register, .popup-checkbox, .popup-checkbox-text').show();

        $('.forget-password, .input-username-email-login, .input-password-login, .input-new-password, #popup p').hide();

        $('.submit-btn').addClass('disabled');

        $('.submit-btn').text('Register Account');

        $('.popup-checkbox-text').html('I agree with the <a href="#">terms</a>');

        $('.popup-checkbox-text, .popup-checkbox').addClass('terms');

        $('.submit-btn').removeClass('confirm-login').removeClass('confirm-forget-password').removeClass('confirm-new-password');

        $('.submit-btn').addClass('confirm-register');

        animateRegisterFeatures();

    }

    else if (type == 'login') {

        $('.forget-password span').text('Forgot password');

        $('.forget-password').removeClass('back-to-login');

        $('#popup-wrapper').css('width', '340px').css('height', '500px');

        $('.input-email-register, .input-username-register, .input-password-register, .input-new-password, #popup p').hide();

        $('.forget-password, .input-username-email-login, .input-password-login, .popup-checkbox, .popup-checkbox-text').show();

        $('.submit-btn').removeClass('disabled');

        $('.popup-checkbox-text, .popup-checkbox').removeClass('terms');

        $('.submit-btn').addClass('confirm-login');

        $('.submit-btn').removeClass('confirm-register').removeClass('confirm-forget-password').removeClass('confirm-new-password');

        $('.submit-btn').text('Login to dashboard');

        $('.popup-checkbox-text').html('Keep me logged in');

    }

    else if (type == 'forget password') {

        $('.popup-checkbox, .popup-checkbox-text, .forget-password, .input-password-login').fadeOut();

        setTimeout(function () {

            $('.forget-password').fadeIn();
            $('.forget-password span').text('Back to login');

            $('.forget-password').addClass('back-to-login');

            $('#popup-wrapper').css('width', '340px').css('height', '440px');

            $('.input-username-email-login').attr('placeholder', 'Email Address').focus();

            $('.submit-btn').removeClass('confirm-login').removeClass('confirm-register').removeClass('confirm-new-password');

            $('.submit-btn').addClass('confirm-forget-password');

            $('.submit-btn').text('Recover Password');

            //variables
            var email = $('.input-username-email-login').val();

            setTimeout(function () {

                if (!isEmail(email)) { $('.input-username-email-login').val(''); }
                else { $('.confirm-forget-password').trigger('click'); }

            }, 250)

            checkForgetPasswordRequirements();

        }, 300)

    }

    else if (type == 'back to login') {

        $('.popup-checkbox, .popup-checkbox-text, .forget-password, .input-password-login').fadeOut();

        setTimeout(function () {

            $('.forget-password').fadeIn();
            $('.forget-password span').text('Back to login');

            $('.forget-password').addClass('back-to-login');

            $('#popup-wrapper').css('width', '340px').css('height', '440px');

            $('.input-username-email-login').attr('placeholder', 'Email Address').focus();

            $('.submit-btn').removeClass('confirm-login').removeClass('confirm-register').removeClass('confirm-new-password');

            $('.submit-btn').addClass('confirm-forget-password');

            $('.submit-btn').text('Recover Password');

            //variables
            var email = $('.input-username-email-login').val();

            setTimeout(function () {

                if (!isEmail(email)) { $('.input-username-email-login').val(''); }
                else { $('.confirm-forget-password').trigger('click'); }

            }, 250)

            checkForgetPasswordRequirements();

        }, 300)

    }

    else if (type == 'new password') {

        $('.popup-checkbox, .popup-checkbox-text, .forget-password, .input-password-login, .input-username-email-login').hide();
        $('.input-new-password, #popup p').show();

        setTimeout(function () {

            $('.input-username-email-login').attr('placeholder', 'New Password').focus();

            $('.submit-btn').removeClass('confirm-login').removeClass('confirm-register').removeClass('confirm-new-password');

            $('.submit-btn').addClass('confirm-new-password').addClass('disabled');

            $('.submit-btn').removeClass('confirm-login');

            $('.submit-btn').text('Set New Password');

            $('.input-new-password').focus();

        }, 300)

    }
}

function openPopup() {

    setTimeout(function () {

        $('#popupoverlay').show();

        $('body').css({
            'background-color': '#757575',
            'overflow': 'hidden'
        });

        $('.stack').css({
            'height': windowHeight + 'px',
            'overflow': 'hidden'
        });

        setTimeout(function () {

            $('#popupoverlay').css({
                'transition': '0.5s all ease',
                'opacity': '1'
            });

            $('#popup-wrapper').css({
                'transition': '0.5s all ease',
                'opacity': '1',
                'transform': 'scale(1) translateY(-50%)'
            });

            $('.stack').css({
                'transition': '0.5s all ease',
                'transform': 'scale(0.9)'
            });

            setTimeout(function () {

                //focus on username/email input
                $('.input-username-email-login:visible, .input-username-register:visible').focus();

            }, 350);

        }, 20);

    }, 50);

}

//point error
function pointError(element) {

    $(element).addClass('error');
    $(element).focus();


    setTimeout(function () {

        $(element).removeClass('error');

    }, errorPointTime)

}

//check form requirement
function checkFormRequirements() {

    //variables
    var score = 4;
    var username = $('.input-username-register').val().length;;
    var email = $('.input-email-register').val();
    var password = $('.input-password-register').val().length;

    if (username < 2) { score--; }
    if (password < 7) { score--; }
    if (!$('.popup-checkbox').hasClass('active')) { score--; }
    if (!isEmail(email)) { score--; }

    if (score > 3) {

        $('.confirm-register').removeClass('disabled');

    }

    else {

        $('.confirm-register').addClass('disabled');

    }


}

function detectFormRequirements() {

    //variables
    var username = $('.input-username-register').val().length;;
    var email = $('.input-email-register').val();
    var password = $('.input-password-register').val().length;

    if (username < 2) {

        message = 'Username need more characters';
        pointError('.input-username-register');

    }
    else if (!isEmail(email)) {

        message = 'Email address looks invalid';
        pointError('.input-email-register');

    }
    else if (password < 7) {

        message = 'Password need more characters';
        pointError('.input-password-register');

    }
    else if (!$('.popup-checkbox').hasClass('active')) {

        message = 'You need to agree with the terms of condition';

    }

    type = 'error';
    notification(message, type);

}

function animateRegisterFeatures() {

    registerFeaturesDelay = 250;

    $('#login-popup-information li').each(function () {

        var el = $(this);
        registerFeaturesDelay = registerFeaturesDelay + 500;

        setTimeout(function () {

            $(el).animate({
                'opacity': '1',
                'padding': '0 0 0 0px'
            }, registerFeaturesAnimationTime)

        }, registerFeaturesDelay)

    });

}

function register() {

    //variables
    var username = $('.input-username-register').val();
    var email = $('.input-email-register').val();
    var password = $('.input-password-register').val();

    //ajax connection
    $.ajax({
        type: "POST",
        dataType: "html",
        url: "dashboard/scripts/calls.php?func=register",
        data: { username: username, email: email, password: password }
    }).done(function (data) {

        if (data == 'invalid') {

            alert('Email looks invalid');

        }

        if (data == 'banned') {

            alert('You\'ve been banned');

        }

        if (data == 'exists') {

            alert('That email exists already');

        }

        if (data == 'password') {

            alert('Password is not safe enough');

        }

        if (data == 'error') {

            alert('Something went wrong');

        }

        if (data == 'success') {

            $(location).attr('href', 'dashboard/new_campaign/?gift=true');
            return false;

        }

        $('.submit-btn').text('Register account');

        checkFormRequirements();

    });

}

function checkForgetPasswordRequirements() {

    //variables
    var email = $('.input-username-email-login').val();

    setTimeout(function () {

        if (isEmail(email)) { $('.confirm-forget-password').removeClass('disabled'); }
        else { $('.confirm-forget-password').addClass('disabled'); }

    }, 100)

}

function getQueryVariable(variable) {
    var query = window.location.search.substring(1);
    var vars = query.split("&");
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split("=");
        if (pair[0] == variable) { return pair[1]; }
    }
    return (false);
}

function checkNewPasswordRequirements() {

    //variables
    var password_length = $('.input-new-password').val().length;

    if (password_length > 5) {

        $('.confirm-new-password').removeClass('disabled');

    }

    else {

        $('.confirm-new-password').addClass('disabled');

    }

}

function SetCookie(cookieName, cookieValue, nDays) {

    var today = new Date();
    var expire = new Date();
    if (nDays == null || nDays == 0) nDays = 1;
    expire.setTime(today.getTime() + 3600000 * 24 * nDays);
    document.cookie = cookieName + "=" + escape(cookieValue) + ";path=/"
	             + ";expires=" + expire.toGMTString();
}