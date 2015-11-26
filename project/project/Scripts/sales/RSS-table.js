$("document").ready(function () {
    setTimeout(function () {
        $("input#rss-button").trigger('click');
    }, 10);
});

function updateRSS() {

    var year = $("select#rss-year").find(":selected").val();
    var month = $("select#rss-month").find(":selected").val();
    var day = $("select#rss-day").find(":selected").val();

    if (month == "None") {
        $.ajax({
            dataType: "json",
            url: "http://localhost:49328/api/sales/rss/" + year,
            success: function (sales) {
                sales = JSON.parse(sales);

                var dataS = [];
                $.each(sales, function (i) {
                    dataS.push([i + 1, sales[i].pais, sales[i].valor, sales[i].percentagem]);
                });

                $('#salesrss').dataTable({
                    data: dataS,
                    columns: [
                        { title: "#" },
                        { title: "Country" },
                        { title: "Sales Volume" },
                        { title: "Percentage" }
                    ],
                    destroy: true
                });
            }
        })
    }
    else if (day == "None") {
        $.ajax({
            dataType: "json",
            url: "http://localhost:49328/api/sales/rss/" + year + "/" + month,
            success: function (sales) {
                sales = JSON.parse(sales);

                var dataS = [];
                $.each(sales, function (i) {
                    dataS.push([i + 1, sales[i].pais, sales[i].valor, sales[i].percentagem]);
                });

                $('#salesrss').dataTable({
                    data: dataS,
                    columns: [
                        { title: "#" },
                        { title: "Country" },
                        { title: "Sales Volume" },
                        { title: "Percentage" }
                    ],
                    destroy: true
                });
            }
        })
    }
    else {
        $.ajax({
            dataType: "json",
            url: "http://localhost:49328/api/sales/rss/" + year + "/" + month + "/" + day,
            success: function (sales) {
                sales = JSON.parse(sales);

                var dataS = [];
                $.each(sales, function (i) {
                    dataS.push([i + 1, sales[i].pais, sales[i].valor, sales[i].percentagem]);
                });

                $('#salesrss').dataTable({
                    data: dataS,
                    columns: [
                        { title: "#" },
                        { title: "Country" },
                        { title: "Sales Volume" },
                        { title: "Percentage" }
                    ],
                    destroy: true
                });
            }
        })
    }
}
