$(function () {
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
                    { title: "#"},
                    { title: "Country" },
                    { title: "Sales Volume" },
                    { title: "Percentage" }
                ]
            });
        }
    })
});
