$("document").ready(function () {
    $("select#dp-month").trigger('change');
});

function doSalesPlot(sales, days) {
    var data = sales;

    var plot = $.plot("#clientDailySalesPlaceholder", [{ label: "Sales", data: data }], {
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
                tickFormatter: function (val, axis) { return val < axis.max ? val : val + " Sales"; }
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

    $("<div id='tooltip'></div>").css({
        position: "absolute",
        display: "none",
        border: "1px solid #fdd",
        padding: "2px",
        "background-color": "#fee",
        opacity: 0.80
    }).appendTo("body");

    $("#clientDailySalesPlaceholder").bind("plothover", function (event, pos, item) {
        if (item) {
            var day = item.datapoint[0],
                value = item.datapoint[1];
            
                if (value > 1 || value == 0)
                    $("#tooltip").html(item.series.label + " of day " + day + ": " + value + " sales").css({ top: item.pageY + 5, left: item.pageX + 5 }).fadeIn(200);
                else
                    $("#tooltip").html(item.series.label + " of day " + day + ": " + value + " sale").css({ top: item.pageY + 5, left: item.pageX + 5 }).fadeIn(200);
        } else {
            $("#tooltip").hide();
        }
    });
}

function doSalesVolumePlot(salesVolume, days) {
    var data = salesVolume;

    var plot = $.plot("#clientDailySalesVolumePlaceholder", [{ label: "Sales Volume", data: data }], {
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

    $("<div id='tooltip'></div>").css({
        position: "absolute",
        display: "none",
        border: "1px solid #fdd",
        padding: "2px",
        "background-color": "#fee",
        opacity: 0.80
    }).appendTo("body");

    $("#clientDailySalesVolumePlaceholder").bind("plothover", function (event, pos, item) {
        if (item) {
            var day = item.datapoint[0], value = item.datapoint[1];
            $("#tooltip").html(item.series.label + " of day " + day + ": " + value + "€").css({ top: item.pageY + 5, left: item.pageX + 5 }).fadeIn(200);
        } else {
            $("#tooltip").hide();
        }
    });
}

function updateDailySales() { 
    var entity = document.getElementById("client-id").getAttribute("value");

    var year = $("input[name='yearPivotSpinner']").val();
    var month = $("select#dp-month").find(":selected").val();

    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/clients/" + entity + "/daily-purchases/" + month + "/" + year,
        success: function (item) {
            item = JSON.parse(item);

            var sales = [], days = [], salesVolume = [];

            $.each(item, function (i) {
                days.push(item[i].day);
                sales.push([item[i].day, item[i].numPurchase]);
                salesVolume.push([item[i].day, item[i].salesVolume]);
            });

            doSalesPlot(sales, days);

            doSalesVolumePlot(salesVolume, days);

            $("#dailySalesLoadingAnimation").remove();

            $("#dailySalesVolumeLoadingAnimation").remove();
        }
    });
}

$('#dp-month').on('change', function () {
    updateDailySales();
});
