(function ($) {


    var methods = {

        init: function (options) {

            var settings = $.extend({
                opensArray: '{}',
                clicksArray: '[]',
                daysArray: '[]',
                RStatistics: '[]'
            }, options);

            return this.each(function () {

                console.log(settings);

                opens_array = JSON.parse(settings.opensArray);
                clicks_array = JSON.parse(settings.clicksArray);
                days_array = JSON.parse(settings.daysArray);
                R_Statistics = JSON.parse(settings.RStatistics);

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


                /****************************/



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


                if (R_Statistics != null) {

                    result_sent = R_Statistics.sent;
                    result_opens = R_Statistics.unique_clicks;
                    result_clicks = R_Statistics.clicks;
                    result_hard_bounces = R_Statistics.bounced;
                    result_soft_bounces = 0;
                    result_rejects = 0;
                    result_complaints = R_Statistics.reported_spam;

                    result_delivered = result_sent - result_hard_bounces - result_soft_bounces - result_rejects;
                    result_difference = result_sent - result_delivered;
                    result_delivered = R_Statistics.delivered;
                    //calculate values
                    methods.calculateValues();

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

            });
        },
        calculateValues: function () {

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

        },
        closeAllPickers: function () {

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

        },
        DestroyData: function () {

            var ctx1 = document.getElementById("chart1").getContext("2d");

            console.log(ctx1);

            myLineChart = new Chart(ctx1);

            console.log(myLineChart);
            
            /*lineChartData.data.labels.splice(-1, 1); // remove the label first
            lineChartData.data.datasets.forEach(function (dataset, datasetIndex) {
                dataset.data.pop();
            });*/

        }
    }
    $.fn.rbk = function (methodOrOptions) {
        if (methods[methodOrOptions]) {
            return methods[methodOrOptions].apply(this, Array.prototype.slice.call(arguments, 1));
        } else if (typeof methodOrOptions === 'object' || !methodOrOptions) {
            return methods.init.apply(this, arguments);
        } else {
            $.error('El metodo ' + methodOrOptions + ' no existe en jquery.rbk');
        }
    };

})(jQuery);