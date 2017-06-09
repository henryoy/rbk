$(document).ready(function(){

	test_email = 'hency.oy@gmail.com';
	demo_id = '';

	//detect if the customer is using an outdated computer
	if(window.localStorage!==undefined){

	    //works

	}

	else{

	    headline = 'You\'re browser feels a little old.';
		paragraph = 'It seems you are using an outdated internet browser. Therefore it\'s impossible to render a few neat editing features!';

		btnTrue = 'Download Chrome';
		btnTrueId = 'downloadChrome';

		btnFalse = 'I understand. Continue.';

		setTimeout(function(){

			openPopup();

		}, 2000);

	}

	//redirect to download chrome
	$(document).on('click', '#downloadChrome', function(){

		$(location).attr('href','https://www.google.nl/chrome/browser/desktop/');

	});

	$(document).ready(function(){

		$('#font_colorpicker').farbtastic('#font_colorpicker_value');

	});


	$(document).on('mousedown', 'body', function(){

		closeEditor();

	});

	$(document).on('mousedown', '.moduleCodeButton, .moduleCode', function(e){

		e.stopPropagation();

	});

	$(document).on('keypress', '.moduleCode', function(e){

		if ( e.metaKey && ( e.which === 13 ) ) {

			closeEditor();

		}

	})

	$(document).on('click', '.codeButton', function(e){

		e.stopPropagation();

		theMod = $(this).closest('[data-module]');
		$('.codeButton, .dragButton, .deleteButton, .duplicateButton').animate({

			width: '0%'

		}, 200)

		if ($('.moduleCode').length > 0) {

		    // it exists
		    $('.moduleCode').animate({

				height: '0px'

			}, 250, 'easeOutQuad', function () {

				openEditor();

			});
		}

		else {

			openEditor();

		}

	})

	$('#frame').on('mouseenter', '[data-module]', function(e){

		if($(this).next().is('.moduleCode')) {

			return false;

		}

		e.stopPropagation();

		table = $(this);

		mod_h = parseInt($(this).height()) / 2;

		$(table).first('td').prepend('<div class="moduleCodeButton preventSelection" style="top: '+mod_h+'px"><div class="codeButton"></div></div><div class="moduleDuplicateButton preventSelection" style="top: '+mod_h+'px"><div class="duplicateButton"></div></div><div class="moduleDragButton preventSelection" style="top: '+mod_h+'px"><div class="dragButton"></div></div><div class="moduleDeleteButton preventSelection" style="top: '+mod_h+'px"><div class="deleteButton"></div></div>');

		$("#frame").sortable('enable');
		$('#frame').sortable({
	        items: 'table[data-module]',
	        axis: 'y',
	        distance: 5,
	        handle: '.dragButton',
	        opacity: 0.85,
	        cursor: '-webkit-grabbing',
	        start: function(event, ui){

		        if ($('.ui-draggable.ui-draggable-dragging').length > 0) {



				}

				else {

					 h_module = $('.dragButton').closest('table').height();

					 $('.ui-sortable-placeholder').css('height','3px');

				}

	        },
	        stop: function(event, ui ){

	        	$('#frame').css('-webkit-transform','scale(1)');

	        	allowSave();

	        }
		});

		$('.codeButton, .dragButton, .deleteButton, .duplicateButton').delay(500).animate({

			width: '100%'

		}, { duration: 400, easing: 'easeOutBack' });

	}).on('mouseleave', '[data-module]', function(e){

		$('.moduleCodeButton, .moduleDragButton, .moduleDeleteButton, .moduleDuplicateButton').remove();

	});

	//Delete Module
	$('#frame').on('click', '.deleteButton', function(ev) {

		$(this).parent().parent('table[data-module]').remove();
		$('.moduleCode').remove();

		count = $('#frame table[data-module]').size();

		if ($(count).length > 0) {

			$('#frame').removeClass('empty').css('min-height','250px');

			if ($('#clear_template').length > 0) {


			}

			else {

				$('#frame').prepend('<input type="button" id="clear_template" class="semi_bold" value="Clear Template">');

			}

		}

		else {

			$('#frame').addClass('empty').css('min-height','250px');
			$('#frame #clear_template').remove();

		}

		checkAttributes();
		allowSave()

	});



	if (document.images) {
    	img1 = new Image();
		img1.src = "img/framework/wheel.png";
	}

	$(document).on('mouseleave', '#sidebar', function() {

		$('#menu_btn').removeClass('active').removeClass('closed');
		$('#user_info').css('box-shadow','none');

		$('#menu').slideUp({
                    duration: 300,
                    easing: "easeInBack"
                });

        $('#sidebar').animate({
	        paddingTop: '81px',
        }, { duration: 300, easing: 'easeInBack' });

		$('#menu_btn .stroke_1').animate({
		  transform: 'rotate(0deg)',
		  top: '0px'
		}, 200);

		$('#menu_btn .stroke_3').animate({
		  transform: 'rotate(0deg)',
		  top: '8px'
		}, 200);

		$('#menu_btn .stroke_2').animate({
		 	opacity: 1
		}, 200);

	});

	$(document).on('mouseup', '#menu_btn', function(){

		el = (this);

		if($(this).hasClass('closed')){

			$(el).removeClass('closed');

			$('#menu').slideUp({
	                    duration: 300,
	                    easing: "easeInBack"
	                });

	        $('#sidebar').animate({
		        paddingTop: '45px',
	        }, { duration: 300, easing: 'easeInBack' });

			$('#menu_btn .stroke_1').animate({
			  transform: 'rotate(0deg)',
			  top: '0px'
			}, 200);

			$('#menu_btn .stroke_3').animate({
			  transform: 'rotate(0deg)',
			  top: '8px'
			}, 200);

			$('#menu_btn .stroke_2').animate({
			 	opacity: 1
			}, 200);

		}

		else {

			$(el).addClass('closed');

			$('#menu_btn .stroke_1').animate({
			  transform: 'rotate(45deg)',
			  top: '4px'
			}, 200);

			$('#menu_btn .stroke_3').animate({
			  transform: 'rotate(-45deg)',
			  top: '4px'
			}, 200);

			$('#menu_btn .stroke_2').animate({
			 	opacity: 0
			}, 200);

			$('#menu').slideDown({
	                    duration: 300,
	                    easing: "easeOutBack"
	                });

	        $('#sidebar').animate({
		        paddingTop: '368px',
	        }, { duration: 300, easing: 'easeOutBack' });

		}

	});

	$(document).on('click', '#mirror_mobile', function(){

		customHtml = '<div id="mirror_mobile_popup"><h4 class="font-bold">See your changes live on mobile</h4><p class="regular" style="padding-bottom: 0px!important;">Open <a href="http://www.stampready.net/mirror/" class="regular" style="color: #69c0af;" target="_blank">stampready.net/mirror/</a> on your device and use the code below to see the changes reflect live.<br/><br/><span class="font-bold mirror_token">&nbsp;</span></p></div>';

		btnFalse = 'Got it!';

		invert = 'true';

		openPopup();

		$('#save').trigger('click');

		createMirrorCode();

	})

	$(document).on('click', '#saveFromCodeEditor', function(){

		editorGetHtml = editor.getValue();

		$tmp = $('<div>'+editorGetHtml+'</div>');
		$tmp.find('style').remove();
		src = $tmp.html();

		$('#frame').html(src);

		$('#coderWrapper').css('transform','scale(0.9)').css('opacity','0');

		$('.stackSR').css('transform','scale(1)').css('opacity','1');

		$('#popupOverlay').css({
			'opacity': '0',
		});

		setTimeout(function(){

			$('html, body').css('overflow','');
			$('#popupOverlay').remove();
			$('#coderWrapper').addClass('hidden');

			editor.setValue('');

		}, 400);

	});


	$(document).on('click', '#save', function() {

		//variables
		plain_text = '';

		if($(this).hasClass('de')){

		}

		else {

			$(this).addClass('ani');

	   	   $tmp = $("<div></div>").html($("#frame").html());
	   	   $tmp.find('*[contenteditable]').each(function() { $(this).removeAttr('contenteditable') });
	   	   $tmp.find('.editable').each(function() { $(this).removeClass('editable') });
	   	   $tmp.find('.delete, .handle, .moduleDeleteButton, .moduleDragButton, .moduleCodeButton, .moduleDuplicateButton, .moduleCode').each(function() { $(this).remove(); });
	   	   $tmp.find('.last-table').each(function() { $(this).removeClass('last-table'); });
	   	   $tmp.find('.last-table').removeClass('last-table');
	   	   $tmp.find('.image_target').removeClass('image_target');
	   	   $tmp.find('tr').unwrap('<tbody></tbody>');
	   	   $tmp.find('.elementIndicator').removeClass('elementIndicator');
	   	   $tmp.find('.currentTable').each(function() { $(this).removeClass('currentTable'); });
	   	   $tmp.find('#edit_link').each(function() { $(this).remove(); });
	   	   $tmp.find('.parentOfBg').contents().unwrap();
	   	   $tmp.find('.parentOfBg, .highlighter-container').remove();
	   	   $tmp.find('#clear_template, grammarly-btn, grammarly').each(function() { $(this).remove(); });
	   	   $tmp.find('[class=""]').each(function() { $(this).removeAttr('class') });
	   	   campaign_html = $tmp.html().replace(/sr_name/g, '*|name|*').replace(/sr_first_name/g, '*|first_name|*').replace(/sr_email/g, '*|email|*').replace(/sr_unsubscribe/g, '*|unsubscribe|*').replace(/sr_date/g, '*|date|*').replace(/sr_view_online/g, '*|view_online|*').replace(/sr_country/g, '*|country|*').replace(/sr_browser/g, '*|browser|*').replace(/sr_os/g, '*|os|*').replace(/sr_referrer/g, '*|referrer|*').replace(/sr_custom_1/g, '*|custom_1|*').replace(/sr_custom_2/g, '*|custom_2|*').replace(/zip:uploads/g, 'zip_uploads');

	   	   //create plain text version
	   	   $('#frame').find('td').each(function(){

				var el = $(this);

				if($(el).children('table').length) {}

				else {

					var text = $(el).text();
					var text = text.replace(/<br\/>/g, '').replace(/\n/g, '').replace(/	/g, '').replace(/  /g, '').replace(/VIEW ONLINE/g, '').replace(/view online/g, '');
					var count = text.split(' ').length;

					if($.trim($(el).html()) == '' || $.trim($(el).html()) == ' '){}

					else if($(el).is(':empty')) {}

					else if(count == 1){}

					else if(text == ' '){}

					else {

						plain_text = plain_text+text;
						plain_text = plain_text+'\r';

					}

				}

			})

	   	    campaign_id = "180087";

	   	   $.ajax({
	            type: "POST",
	            dataType: "html",
	            url: "../scripts/calls.php?func=save_campaign",
	            data: {
		            campaign_id: campaign_id, campaign_html: campaign_html, plain_text: plain_text
	            }
	        }).done(function(data) {

	        	if(data == '2'){

	        		$('.ani').removeClass('ani');

		        	headline = 'Due to inactivity, you\'ve been logged out.';
					paragraph = 'Just enter your password so we know it\'s you again';

					inputField = '';
					inputFieldId = 'confirmAccountPassword';

					btnTrue = 'Confirm Account';
					btnTrueId = 'confirmAccountSession';

					btnFalse = 'Close without saving';

					openPopup();

					$('#confirmAccountPassword').attr('type','password')

	        	}

	        	else {

	        		$('.ani').removeClass('ani').addClass('de');

	        	}

			});

		}

	   });

	   $(document).on('click', '#confirmAccountSession', function(){

		   user_email = $('[data-useremail]').attr('data-useremail');
		   user_pass = $('#confirmAccountPassword').val();

		    $.ajax({
	            type: "POST",
	            dataType: "html",
	            url: "../scripts/calls.php?func=confirm_account_session",
	            data: {
		            user_email: user_email, user_pass: user_pass
	            }
	        }).done(function(data) {

	        	if(data == '1'){

		        	closePopup();

					setTimeout(function(){

						$('#save').trigger('click');

					}, 500)

	        	}

	        	else {

		        	notificationContent = 'Wrong Password';
					notificationColor = "#ea5a5b";

					notification();

					$('##confirmAccountPassword').focus();

	        	}

	        });

	   });

	   $('#logo').each(function(){

			//vars
			email = $(this).attr('data-avatar');

			//find data and add gravatar
			$(this).append($.gravatar(email));

			//fetch img src
			a = $(this).find('img').attr('src');

			//add stampready default icon
			$(this).find('img').attr('src', a+'d=http%3A%2F%2Fwww.stampready.net%2Fdashboard%2Fimg%2Fframework%2Favatar_default_ready.png');

		});

});

//Generate Template
function downloadTemplate() {

	//Hide overlay
	$('.overlay').fadeOut(200);

	campaign_id = '180087';

	$('#campaign_id').val(campaign_id);

}

function createMirrorCode(){

	campaign_id = '180087';

	$.ajax({
        type: "POST",
        dataType: "html",
        url: "scripts/create_mirror_token.php",
        data: {
            campaign_id: campaign_id
        }
    }).done(function(data) {

    	$('.mirror_token').text(data);

    });

   }
