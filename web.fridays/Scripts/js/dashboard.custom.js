//document ready
$(document).ready(function () {

	//variables and flags
	datepicker_flag = 0;
	switch_flag = 0;
	id = '';
	i = 0;
	action = '';
	amount = '';
	plan = '';
	new_member = '';
	username = 'henry';
	gift = '';

	//if action is progressing
	if (action == 'processing') {

		setTimeout(function () {

			headline = 'Thank you for the purchase!';
			paragraph = 'You\'re able to find your invoice on the Billing page. However, it may take a few minutes in order to process the credits/plan.';

			btnTrue = 'Go to invoices';
			btnTrueId = 'go_to_invoice';
			btnTrueFunction = 'goToInvoice();';

			btnFalse = 'Close';

			openPopup();

		}, 500);

	}

	//variables of results
	//default = 0
	result_sent = 0;
	result_opens = 0;
	result_clicks = 0;
	result_hard_bounces = 0;
	result_soft_bounces = 0;
	result_rejects = 0;
	result_complaints = 0;

	//calculate delivered and the delivered and difference
	result_delivered = result_sent - result_hard_bounces - result_soft_bounces - result_rejects;
	result_difference = result_sent - result_delivered;

	//initialise the charts
	chart_sent = document.getElementById('chart_sent');
	chart_delivered = document.getElementById('chart_delivered');
	chart_hard_bounces = document.getElementById('chart_hard_bounces');
	chart_soft_bounces = document.getElementById('chart_soft_bounces');
	chart_rejects = document.getElementById('chart_rejects');
	chart_spam_complaints = document.getElementById('chart_spam_complaints');

	//calculate the chart values
	calculateValues();

	//chart sent
	chart_circle_sent = new ProgressBar.Circle(chart_sent, {
		color: '#69c0af',
		trailColor: '#d8d8d8',
		trailWidth: 3,
		duration: 750,
		easing: 'easeOut',
		strokeWidth: 3,
		text: {
			value: result_sent
		},

		// Set default step function for all animate calls
		step: function (state, chart_circle_sent) {
			chart_circle_sent.path.setAttribute('stroke', state.color);
		}
	});

	//chart delivered
	chart_circle_delivered = new ProgressBar.Circle(chart_delivered, {
		color: '#fbd970',
		trailColor: '#d8d8d8',
		trailWidth: 3,
		duration: 750,
		easing: 'easeOut',
		strokeWidth: 3,
		text: {
			value: result_delivered
		},

		// Set default step function for all animate calls
		step: function (state, chart_circle_delivered) {
			chart_circle_delivered.path.setAttribute('stroke', state.color);
		}
	});

	$(document).on('click', '#start_and_enjoy, #continue_receipt_btn', function () {

		closePopup();

	});

	$(document).on('mouseenter', '#chart1', function () {

		setTimeout(function () {

			$('#chartjs-tooltip').show();

		}, 100)

	})

});

function calculateValues() {

	result_sent_animation = '1';
	result_delivered_animation = result_delivered / result_sent;
	result_opens_animation = result_delivered / result_opens;
	result_clicks_animation = result_delivered / result_clicks;
	result_hard_bounces_animation = result_hard_bounces / result_difference * 100;
	result_soft_bounces_animation = result_soft_bounces / result_difference * 100;
	result_rejects_animation = result_rejects / result_difference * 100;
	result_complaints_animation = result_complaints / result_difference * 100;
	if (result_complaints_animation > 99) { result_complaints_animation = 100; }


	if (result_sent == '') { result_sent = '0'; result_delivered = '0'; result_sent_animation = '0'; result_delivered_animation = '0'; }
	if (result_opens == '') { result_opens = '0'; result_opens_animation = '0'; }
	if (result_clicks == '') { result_clicks = '0'; result_clicks_animation = '0'; }
	if (result_hard_bounces == '') { result_hard_bounces = '0'; result_hard_bounces_animation = '0'; }
	if (result_soft_bounces == '') { result_soft_bounces = '0'; result_soft_bounces_animation = '0'; }
	if (result_rejects == '') { result_rejects = '0'; result_rejects_animation = '0'; }
	if (result_complaints == '') { result_complaints = '0'; result_complaints_animation = '0'; }

	result_open_rate = Math.round((result_opens / result_sent) * 100).toFixed(1);
	result_click_rate = Math.round((result_clicks / result_sent) * 100).toFixed(1);
	result_delivered_rate = Math.round((result_delivered / result_sent) * 100).toFixed(1);

	//if nan
	if (result_open_rate == 'NaN') { result_open_rate = '0'; }
	if (result_click_rate == 'NaN') { result_click_rate = '0'; }
	if (result_delivered_rate == 'NaN') { result_delivered_rate = '0'; }

	//change open/click rate number
	$('#open_chart').find('h2').text(result_open_rate + '%');
	$('#click_chart').find('h2').text(result_click_rate + '%');

	//change open/click number
	result_opens = parseInt(result_opens);
	result_clicks = parseInt(result_clicks);
	$('.open_chart_number').text(result_opens.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,").slice(0, -3))
	$('.click_chart_number').text(result_clicks.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,").slice(0, -3))

	if (result_open_rate > 100) { result_open_rate = 100; }

	//change loader percentage animation bar
	$('#chart_open_rate .chart_rate_percentage').text(result_open_rate + '%');
	$('#chart_click_rate .chart_rate_percentage').text(result_click_rate + '%');
	$('#chart_deliverability_rate .chart_rate_percentage').text(result_delivered_rate + '%');
	$('#chart_open_rate .dashboard_loader_filler').attr('data-percentage', result_open_rate);
	$('#chart_click_rate .dashboard_loader_filler').attr('data-percentage', result_click_rate);
	$('#chart_deliverability_rate .dashboard_loader_filler').attr('data-percentage', result_delivered_rate);
	$('.chart_meter .hard_bounces').attr('data-percentage', result_hard_bounces_animation);
	$('.chart_meter .soft_bounces').attr('data-percentage', result_soft_bounces_animation);
	$('.chart_meter .rejects').attr('data-percentage', result_rejects_animation);
	$('.chart_meter .spam_complaints').attr('data-percentage', result_complaints_animation);

	$('[data-percentage]').each(function () {

		el = $(this);
		percentage = $(this).attr('data-percentage');

		$(el).animate({

			'width': percentage + '%'

		}, 750);

	});

}

function closeAllPickers() {

	$('#datepicker').css({
		'-webkit-transform': 'scale(0.9) translate(0,0px)',
		'opacity': '0'

	});

	$('#datepicker_input').css({
		'width': '162px',
		'background-position': '126px',
		'box-shadow': '-1px 0px 0px #ebebeb, 0px 1px 0px #ebebeb, 0px -1px 0px #ebebeb'
	});

	setTimeout(function () {

		datepicker_flag = 0;
		$('#datepicker').hide();

	}, 500)

}

function refreshCampaignAnalytics() {

	$(location).attr('href', 'index.php?id=' + campaign_id);

}

function fetchMandrillAnalytics() {

	$.ajax({
		type: "POST",
		dataType: "html",
		url: "scripts/calls.php?func=fetch_user_activity",
		data: {},
		dataType: "json"
	}).done(function (data) {

		//if empty
		if (!data['sent']) {

			//alert('empty');

		}

		else {

			//updating doughnut charts
			result_sent = parseInt(data['sent']);
			result_opens = parseInt(data['u_opens']);
			result_clicks = parseInt(data['u_clicks']);
			result_hard_bounces = parseInt(data['hard_bounces']);
			result_soft_bounces = parseInt(data['soft_bounces']);
			result_rejects = parseInt(data['rejects']);
			result_complaints = parseInt(data['complaints']);

			result_delivered = result_sent - result_hard_bounces - result_soft_bounces - result_rejects;
			result_difference = result_sent - result_delivered;

			//calculate values
			calculateValues();

			if (result_clicks > 0) { result_clicks = result_clicks.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,").slice(0, -3); }
			if (result_opens > 0) { result_opens = result_opens.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,").slice(0, -3); }
			if (result_sent > 0) { result_sent = result_sent.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,").slice(0, -3); }
			if (result_delivered > 0) { result_delivered = result_delivered.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,").slice(0, -3); }
			if (result_hard_bounces > 0) { result_hard_bounces = result_hard_bounces.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,").slice(0, -3); }
			if (result_soft_bounces > 0) { result_soft_bounces = result_soft_bounces.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,").slice(0, -3); }
			if (result_rejects > 0) { result_rejects = result_rejects.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,").slice(0, -3); }
			if (result_complaints > 0) { result_complaints = result_complaints.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,").slice(0, -3); }

			$('#chart_sent p').text(result_sent);
			$('#chart_delivered p').text(result_delivered);
			$('.chart_meter_hard_bounces').text(result_hard_bounces);
			$('.chart_meter_soft_bounces').text(result_soft_bounces);
			$('.chart_meter_rejects').text(result_rejects);
			$('.chart_meter_spam_complaints').text(result_complaints);
			$('#chart_open_rate .chart_rate_amount').text(result_opens);
			$('#chart_click_rate .chart_rate_amount').text(result_clicks);
			$('#chart_deliverability_rate .chart_rate_amount').text(result_sent);


			//result_hard_bounces_animation
			chart_circle_sent.animate(result_sent_animation, {
				from: { color: '#69c0af' },
				to: { color: '#69c0af' }
			});

			chart_circle_delivered.animate(result_delivered_animation, {
				from: { color: '#fbd970' },
				to: { color: '#fbd970' }
			});


		}

	}).fail(function (data) {

		setTimeout(function () {

			//fetchMandrillAnalytics()

		}, 2500)

	});

}

function goToInvoice() {

	$(location).attr('href', 'account/billing/');

}

//process analytics
function processingPayment() {

	service = '';

	//change headline
	if (service == 'stripe') {

		$('.processing_headline').text('Payment processed successfully!');
		$('#loader').css('background-color', 'transparent');
		$('#icon img, #loader').addClass('loaded');

		setTimeout(function () {

			$('#icon img').attr('class', 'loaded2');

		}, 400);

		//vars
		credits = '';
		type = '';
		plan = '';
		price = '';
		invoice = '';
		service = 'Creditcard';

		//variables via JSON
		credits = parseInt(credits).toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,").slice(0, -3);

		//create lists
		if (type == 'subscription') {

			price = parseInt(price).toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");

			//create the data sheet
			$('.receipt_sheet').append('<li><div class="preview_send_row">Plan</div><div class="preview_send_result">' + plan + '</div></li><li><div class="preview_send_row">Price</div><div class="preview_send_result">$' + price + '</div></li><li><div class="preview_send_row">Paid With</div><div class="preview_send_result" style="text-transform: capitalize">' + service + '</div></li><li><div class="preview_send_row">Next Invoice</div><div class="preview_send_result">' + invoice + '</div></li>');

		}

		else if (type == 'single_payment') {

			//create the data sheet
			$('.receipt_sheet').append('<li><div class="preview_send_row">Credits Purchased</div><div class="preview_send_result">' + credits + '</div></li><li><div class="preview_send_row">Price</div><div class="preview_send_result">$' + price + '</div></li><li><div class="preview_send_row">Paid With</div><div class="preview_send_result" style="text-transform: capitalize">' + service + '</div></li>');

		}

		$('.receipt_sheet').delay(500).animate({

			"height": "show",
			"marginTop": "show",
			"marginBottom": "show",
			"paddingTop": "show",
			"paddingBottom": "show",
			"lineHeight": '81px'

		}, { duration: 500, easing: 'easeOutBack' });

	}

	else if (service == 'paypal') {

		$.ajax({
			type: "POST",
			dataType: "html",
			url: "scripts/calls.php?func=processing_payment",
			data: {},
			dataType: "json",
			error: function (request, status, error) {

				setTimeout(function () {

					processingPayment();

				}, 5000);

			}
		}).done(function (data) {

			if (!data['type']) {

				setTimeout(function () {

					processingPayment();

				}, 5000);

			}

			else {

				$('.processing_headline').text('Payment processed successfully!');
				$('#loader').css('background-color', 'transparent');
				$('#icon img, #loader').addClass('loaded');

				setTimeout(function () {

					$('#icon img').attr('class', 'loaded2');

				}, 400);

				//variables via JSON
				credits = parseInt(data['credits_got']).toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,").slice(0, -3);

				//create lists
				if (data['type'] == 'subscr_payment' || data['type'] == 'subscr_signup') {

					price = data['price'];
					if (data['price'] == null) { price = 0; }

					//create the data sheet
					$('.receipt_sheet').append('<li><div class="preview_send_row">Plan</div><div class="preview_send_result">' + data["plan"] + '</div></li><li><div class="preview_send_row">Price</div><div class="preview_send_result">$' + price + '</div></li><li><div class="preview_send_row">Paid With</div><div class="preview_send_result">' + data["service"] + '</div></li><li><div class="preview_send_row">Next Invoice</div><div class="preview_send_result">' + data["next_invoice"] + '</div></li>');

				}

				else if (data['type'] == 'web_accept') {

					//create the data sheet
					$('.receipt_sheet').append('<li><div class="preview_send_row">Credits Purchased</div><div class="preview_send_result">' + credits + '</div></li><li><div class="preview_send_row">Price</div><div class="preview_send_result">$' + data["price"] + '</div></li><li><div class="preview_send_row">Paid With</div><div class="preview_send_result">' + data["service"] + '</div></li>');

					fetch_credits = data['credits_now'];

					//present new credits to meter
					$('.credits_or_plan').html(fetch_credits + ' <span>Credits</span>');

				}

				$('.receipt_sheet').animate({

					"height": "show",
					"marginTop": "show",
					"marginBottom": "show",
					"paddingTop": "show",
					"paddingBottom": "show",
					"lineHeight": '81px'

				}, { duration: 500, easing: 'easeOutBack' });

			}

		});


	}
}


//create custom tooltip api
Chart.defaults.global.customTooltips = function (tooltip) {

	var tooltipEl = $('#chartjs-tooltip');

	if (!tooltip) {
		tooltipEl.css({
		});
		return;
	}

	tooltipEl.removeClass('above below');
	tooltipEl.addClass(tooltip.yAlign);

	var innerHtml = '';
	for (var i = tooltip.labels.length - 1; i >= 0; i--) {

		result = tooltip.labels[i].replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");

		innerHtml += [
			'<div class="chartjs-tooltip-section">',
			'	<span class="chartjs-tooltip-value" style="color:' + tooltip.legendColors[i].fill + '">' + result + '</span>',
			'</div>'
		].join('');
	}

	tooltipEl.html(innerHtml);

	tooltipEl.css({
		opacity: 0.9,
		left: tooltip.chart.canvas.offsetLeft + tooltip.x + 'px',
		top: tooltip.chart.canvas.offsetTop + tooltip.y + 'px',
		fontFamily: tooltip.fontFamily,
		fontSize: tooltip.fontSize,
		fontStyle: tooltip.fontStyle,
	});
};

opensArray = '[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]';
clicksArray = '[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]';
daysArray = '["01-27","01-28","01-29","01-30","01-31","02-01","02-02","02-03","02-04","02-05","02-06","02-07","02-08","02-09","02-10","02-11","02-12","02-13","02-14","02-15","02-16","02-17","02-18","02-19","02-20","02-21","02-22","02-23","02-24","02-25","02-26"]';

//parse the data from the json object
opens_array = JSON.parse(opensArray);
clicks_array = JSON.parse(clicksArray);
days_array = JSON.parse(daysArray);

//initalize the line chart
var lineChartData = {
	labels: days_array,
	datasets: [{
		label: "My Second dataset",
		fillColor: "#ffa744",
		strokeColor: "#ffa744",
		pointColor: "rgba(255,255,255,0)",
		pointStrokeColor: "rgba(255,255,255,0)",
		pointHighlightFill: "#ffa744",
		pointHighlightStroke: "#FFF",
		data: opens_array
	}, {
		label: "My First dataset",
		fillColor: "#ff6041",
		strokeColor: "#ff6041",
		pointColor: "rgba(0,0,0,0)",
		pointStrokeColor: "rgba(0,0,0,0)",
		pointHighlightFill: "#ff6041",
		pointHighlightStroke: "#FFF",
		data: clicks_array
	}]
};

//render the line chart
window.onload = function () {

	setTimeout(function () {

		//fetchMandrillAnalytics();

	}, 500)

	var ctx1 = document.getElementById("chart1").getContext("2d");
	myLineChart = new Chart(ctx1).Line(lineChartData, {
		responsive: true,
		pointDotStrokeWidth: 2,
		pointHitDetectionRadius: 8,
		bezierCurveTension: 0.4,
		scaleShowGridLines: true,
		scaleGridLineColor: "rgba(0,0,0,.02)",
		scaleFontColor: "#9a9c9e",
		scaleFontSize: 12
	});

	setTimeout(function () {

		$('.preventChartHack').remove();

	}, 500);

};