$("document").ready(function () {
    setTimeout(function () {
        $("input#daily-purchases-button").trigger('click');
    }, 10);
});

function doPlot(salesVolume, purchases, days) {
    var plot = $.plot("#clientDailyPurchasesPlaceholder", [
        { label: "Sales Volume", data: salesVolume }
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

            $("#tooltip").html(item.series.label + " of day " + day + ": " + value + "€")
                .css({ top: item.pageY + 5, left: item.pageX + 5 })
                .fadeIn(200);
        } else {
            $("#tooltip").hide();
        }
    });

};

function updateDailyPurchases() {

    alert("entrei");

    var entity = document.getElementById("client-id").getAttribute("value");

    var year = $("select#dp-year").find(":selected").val();
    var month = $("select#dp-month").find(":selected").val();
    console.log("Year: " + year + "       Month: " + month);

    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/clients/" + entity + "/daily-purchases/" + month + "/" + year,
        success: function (item) {
            item = JSON.parse(item);

            var purchases = [], days = [], salesVolume = [];

            $.each(item, function (i) {
                days.push(item[i].day);
                purchases.push([item[i].day, item[i].purchases]);
                salesVolume.push([item[i].day, item[i].salesVolume]);
            });

            doPlot(salesVolume, purchases, days);

            $("button").click(function () {
                doPlot($(this).text());
            });
        }
    });
}
