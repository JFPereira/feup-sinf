$(function () {
    var currentYear = new Date().getFullYear()

    updateMonthlySales(currentYear);
});

function doMonthlySalesPlot(sales, months) {
    var data = sales;

    plot = $.plot("#clientMonthlySalesPlaceholder", [{ label: "Sales", data: data }], {
        series: {
            lines: { show: true },
            points: { show: true }
        },
        xaxis: {
            ticks: months,
            tickFormatter: function (val, axis) {
                var result = "";

                switch (val) {
                    case 1:
                        result = "Jan";
                        break;
                    case 2:
                        result = "Feb";
                        break;
                    case 3:
                        result = "Mar";
                        break;
                    case 4:
                        result = "Apr";
                        break;
                    case 5:
                        result = "May";
                        break;
                    case 6:
                        result = "Jun";
                        break;
                    case 7:
                        result = "Jul";
                        break;
                    case 8:
                        result = "Aug";
                        break;
                    case 9:
                        result = "Sep";
                        break;
                    case 10:
                        result = "Oct";
                        break;
                    case 11:
                        result = "Nov";
                        break;
                    case 12:
                        result = "Dec";
                        break;
                }

                if (val == axis.max)
                    result = result + "  Months";

                return result;
            }
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

    $("#clientMonthlySalesPlaceholder").bind("plothover", function (event, pos, item) {
        if (item) {
            var month = item.datapoint[0],
                value = item.datapoint[1];

            switch (month) {
                case 1:
                    month = "January";
                    break;
                case 2:
                    month = "February";
                    break;
                case 3:
                    month = "March";
                    break;
                case 4:
                    month = "April";
                    break;
                case 5:
                    month = "May";
                    break;
                case 6:
                    month = "June";
                    break;
                case 7:
                    month = "July";
                    break;
                case 8:
                    month = "August";
                    break;
                case 9:
                    month = "September";
                    break;
                case 10:
                    month = "October";
                    break;
                case 11:
                    month = "November";
                    break;
                case 12:
                    month = "December";
                    break;
            }

            if (value > 1 || value == 0)
                $("#tooltip").html(item.series.label + " of " + month + ": " + value + " sales").css({ top: item.pageY + 5, left: item.pageX + 5 }).fadeIn(200);
            else
                $("#tooltip").html(item.series.label + " of " + month + ": " + value + " sale").css({ top: item.pageY + 5, left: item.pageX + 5 }).fadeIn(200);
        } else {
            $("#tooltip").hide();
        }
    });
}

function doMonthlySalesVolumePlot(salesVolume, months) {
    var data = salesVolume;

    var plot = $.plot("#clientMonthlySalesVolumePlaceholder", [{ label: "Sales Volume", data: data }], {
        series: {
            lines: { show: true },
            points: { show: true }
        },
        xaxis: {
            ticks: months,
            tickFormatter: function (val, axis) {
                var result = "";

                switch (val) {
                    case 1:
                        result = "Jan";
                        break;
                    case 2:
                        result = "Feb";
                        break;
                    case 3:
                        result = "Mar";
                        break;
                    case 4:
                        result = "Apr";
                        break;
                    case 5:
                        result = "May";
                        break;
                    case 6:
                        result = "Jun";
                        break;
                    case 7:
                        result = "Jul";
                        break;
                    case 8:
                        result = "Aug";
                        break;
                    case 9:
                        result = "Sep";
                        break;
                    case 10:
                        result = "Oct";
                        break;
                    case 11:
                        result = "Nov";
                        break;
                    case 12:
                        result = "Dec";
                        break;
                }

                if (val == axis.max)
                    result = result + "  Months";

                return result;
            }
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

    $("#clientMonthlySalesVolumePlaceholder").bind("plothover", function (event, pos, item) {
        if (item) {
            var month = item.datapoint[0],
                value = item.datapoint[1];

            switch (month) {
                case 1:
                    month = "January";
                    break;
                case 2:
                    month = "February";
                    break;
                case 3:
                    month = "March";
                    break;
                case 4:
                    month = "April";
                    break;
                case 5:
                    month = "May";
                    break;
                case 6:
                    month = "June";
                    break;
                case 7:
                    month = "July";
                    break;
                case 8:
                    month = "August";
                    break;
                case 9:
                    month = "September";
                    break;
                case 10:
                    month = "October";
                    break;
                case 11:
                    month = "November";
                    break;
                case 12:
                    month = "December";
                    break;
            }

            $("#tooltip").html(item.series.label + " of " + month + ": " + value + "€").css({ top: item.pageY + 5, left: item.pageX + 5 }).fadeIn(200);

        } else {
            $("#tooltip").hide();
        }
    });
}

function updateMonthlySales(year) {

    var entity = document.getElementById("client-id").getAttribute("value");

    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/clients/" + entity + "/monthly-purchases/" + year,
        success: function (item) {
            item = JSON.parse(item);

            var sales = [], months = [], salesVolume = [];

            $.each(item, function (i) {
                months.push(item[i].month);
                sales.push([item[i].month, item[i].numPurchase]);
                salesVolume.push([item[i].month, item[i].salesVolume]);
            });

            doMonthlySalesPlot(sales, months);

            doMonthlySalesVolumePlot(salesVolume, months);

            $("#monthlySalesLoadingAnimation").remove();

            $("#monthlySalesVolumeLoadingAnimation").remove();
        }
    });
}
