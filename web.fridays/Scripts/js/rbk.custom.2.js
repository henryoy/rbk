$(document).ready(function () {


    datepicker_flag = 0;
    switch_flag = 0;
    id = '';
    i = 0;
    action = '';
    amount = '';
    plan = '';

    result_sent = 12;
    result_opens = 12;
    result_clicks = 12;
    result_hard_bounces = 0;
    result_soft_bounces = 1;
    result_rejects = 2;
    result_complaints = 12;


    result_delivered = result_sent - result_hard_bounces - result_soft_bounces - result_rejects;
    result_difference = result_sent - result_delivered;


    chart_sent = document.getElementById('chart_sent');
    chart_delivered = document.getElementById('chart_delivered');
    chart_hard_bounces = document.getElementById('chart_hard_bounces');
    chart_soft_bounces = document.getElementById('chart_soft_bounces');
    chart_rejects = document.getElementById('chart_rejects');
    chart_spam_complaints = document.getElementById('chart_spam_complaints');

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


function fetchMandrillAnalytics() {

    var urlLog = "/Dashboard/index.aspx/GetGeneral";

    $.ajax({
        type: "POST",
        url: urlLog,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (data) {
        /*
        bounced
        clicks
        delivered
        forwarded
        impressions
        optouts
        reported_spam
        sent
        unique_clicks
        unique_impressions
        */

        result_sent = data.d.sent;
        result_opens = data.d.unique_clicks;
        result_clicks = data.d.clicks;
        result_hard_bounces = data.d.bounced;
        result_soft_bounces = 0;
        result_rejects = 0;
        result_complaints = data.d.reported_spam;

        result_delivered = result_sent - result_hard_bounces - result_soft_bounces - result_rejects;
        result_difference = result_sent - result_delivered;
        result_delivered = data.d.delivered;
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

    }).fail(function (data) {

    });
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
var urlLog = "/Dashboard/index.aspx/GetClickInfo";

$.ajax({
    type: "POST",
    url: urlLog,
    contentType: "application/json; charset=utf-8",
    dataType: "json"
}).done(function (data) {


    opensArray = JSON.stringify('[]');
    clicksArray = JSON.stringify(data.d.clicksArray);
    days_array = JSON.stringify(data.d.daysArray);

    opens_array = JSON.parse(opensArray);

    clicks_array = JSON.parse(clicksArray);
    days_array = JSON.parse(days_array);

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

}).fail(function (data) {

});;


window.onload = function () {

    setTimeout(function () {

        //fetchMandrillAnalytics();

    }, 500)

    setTimeout(function () {

        $('.preventChartHack').remove();

    }, 500);

};