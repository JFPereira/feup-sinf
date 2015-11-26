$("document").ready(function () {
    setTimeout(function () {
        $("input#daily-purchases-button").trigger('click');
    }, 10);
});

function doPlot(purchases, days) {
    $.plot("#clientDailyPurchasesPlaceholder", [
        { label: "Purchases", data: purchases }
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
            tickFormatter: function (val, axis) { return val < axis.max ? val : val + "  Volume"; }
        },
        grid: {
            backgroundColor: { colors: ["#fff", "#eee"] },
            borderWidth: {
                top: 1,
                right: 1,
                bottom: 2,
                left: 2
            }
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

            doPlot(purchases, days);

            $("button").click(function () {
                doPlot($(this).text());
            });
        }
    });
}
