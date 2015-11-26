$("document").ready(function () {
    setTimeout(function () {
        $("input#daily-purchases-button").trigger('click');
    }, 10);
});

function addNewMonth() {
    var inputMonths = document.getElementById("numMonths");
    var numMonths = inputMonths.getAttribute("value");

    numMonths = parseInt(numMonths) + 1;
    inputMonths.setAttribute("value", numMonths);

    alert(numMonths);

    $("#options-rows").append('<br>' +
                        '<div id="daily-purchases-' + numMonths + '" class="form-inline" role="form">' +
                            '<div class="form-group years">' +
                                '<label for="dp-year-' + numMonths + '">Year</label>' +
                                '<select id="dp-year-' + numMonths + '" class="form-control" name="month">' +
                                    '<option value="2014">2014</option>' +
                                    '<option value="2015" selected>2015</option>' +
                                    '<option value="2016">2016</option>' +
                                    '<option value="2017">2017</option>' +
                                '</select>' +
                            '</div>' +
                            '<div class="form-group months">' +
                                '<label for="dp-month-' + numMonths + '">Month</label>' +
                                '<select id="dp-month-' + numMonths + '" class="form-control" name="year">' +
                                    '<option value="01">January</option>' +
                                    '<option value="02">February</option>' +
                                    '<option value="03">March</option>' +
                                    '<option value="04">April</option>' +
                                    '<option value="05">May</option>' +
                                    '<option value="06">June</option>' +
                                    '<option value="07">July</option>' +
                                    '<option value="08">August</option>' +
                                    '<option value="09">September</option>' +
                                    '<option value="10" selected>October</option>' +
                                    '<option value="11">November</option>' +
                                    '<option value="12">December</option>' +
                                '</select>' +
                            '</div>' +
                        '</div>');
}

function doPlot(salesVolume, purchases, days, mode) {
    var data = null;

    var plot = null;

    if (mode === "sales-volume") {
        console.log("sales-volume");

        data = salesVolume;

        plot = $.plot("#clientDailyPurchasesPlaceholder", [
        { label: "Sales Volume", data: data }
        ],
    {
        series: {
            lines: { show: true },
            points: { show: true }
        },
        xaxis: {
            ticks: days,
            tickFormatter: function (val, axis) { return val < axis.max ? val : val + "  Days"; }
        },
        yaxis: {
            ticks: 1,
            min: 0,
            tickFormatter: function (val, axis) { return val < axis.max ? val + "€" : val + "€  Volume"; }
        },
        grid: {
            backgroundColor: { colors: ["#fff", "#eee"] },
            borderWidth: {
                top: 1,
                right: 1,
                bottom: 2,
                left: 2
            },
            hoverable: true,
            clickable: true
        }
    });
    } else {
        console.log("purchases");

        data = purchases;

        plot = $.plot("#clientDailyPurchasesPlaceholder", [
        { label: "Purchases", data: data }
        ],
    {
        series: {
            lines: { show: true },
            points: { show: true }
        },
        xaxis: {
            ticks: days,
            tickFormatter: function (val, axis) { return val < axis.max ? val : val + "  Days"; }
        },
        yaxis: {
            ticks: 1,
            min: 0,
            tickFormatter: function (val, axis) { return val < axis.max ? val : val + " Purchases"; }
        },
        grid: {
            backgroundColor: { colors: ["#fff", "#eee"] },
            borderWidth: {
                top: 1,
                right: 1,
                bottom: 2,
                left: 2
            },
            hoverable: true,
            clickable: true
        }
    });
    }

    $("<div id='tooltip'></div>").css({
        position: "absolute",
        display: "none",
        border: "1px solid #fdd",
        padding: "2px",
        "background-color": "#fee",
        opacity: 0.80
    }).appendTo("body");

    $("#clientDailyPurchasesPlaceholder").bind("plothover", function (event, pos, item) {
        if (item) {
            var day = item.datapoint[0],
                value = item.datapoint[1];

            $("#tooltip").html(item.series.label + " of day " + day + ": " + value)
                .css({ top: item.pageY + 5, left: item.pageX + 5 })
                .fadeIn(200);
        } else {
            $("#tooltip").hide();
        }
    });

};

function updateDailyPurchases() {

    var entity = document.getElementById("client-id").getAttribute("value");

    var year = $("select#dp-year-1").find(":selected").val();
    var month = $("select#dp-month-1").find(":selected").val();
    var mode = $("input[name=optradio]:checked").val();

    console.log("Year: " + year + "       Month: " + month + "         Mode: " + mode);

    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/clients/" + entity + "/daily-purchases/" + month + "/" + year,
        success: function (item) {
            item = JSON.parse(item);

            var purchases = [], days = [], salesVolume = [];

            $.each(item, function (i) {
                days.push(item[i].day);
                purchases.push([item[i].day, item[i].numPurchase]);
                salesVolume.push([item[i].day, item[i].salesVolume]);
            });

            doPlot(salesVolume, purchases, days, mode);

            $("button").click(function () {
                doPlot($(this).text());
            });
        }
    });
}
