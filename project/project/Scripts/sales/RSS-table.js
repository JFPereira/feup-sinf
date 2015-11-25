$(function () {

    var year = document.getElementById("year").getAttribute("value");
    var month = document.getElementById("month").getAttribute("value");
    var day = document.getElementById("day").getAttribute("value");
    
    if (month == "February" || month == "february") {
        if (parseInt(day) > 29) {
            day = "29";
        }
    }
    if (year == null) {
        $.ajax({
            dataType: "json",
            url: "http://localhost:49328/api/sales/rss/2015",
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
                    ]
                });
            }
        })
    }
    else if (month == null) {
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
                    ]
                });
            }
        })
    }
    else if (day == null) {
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
                    ]
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
                    ]
                });
            }
        })
    }
});
