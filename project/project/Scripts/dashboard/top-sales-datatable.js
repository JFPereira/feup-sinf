$(function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/sales/top",
        success: function (sales) {
            sales = JSON.parse(sales);

            var dataS = [];
            $.each(sales, function (i) {
                var a = '<a href="/Clients/Show/' + sales[i].entity + '">' + sales[i].entity + '</a>';
                dataS.push([i + 1, a, sales[i].purchaseValue + "€", sales[i].date, sales[i].numPurchases]);
            });

            $('#topsales').dataTable({
                data: dataS,
                columns: [
                    { title: "#" },
                    { title: "Client" },
                    { title: "Sale Volume" },
                    { title: "Date" },
                    { title: "Client Nr Sales" }
                ],
                destroy: true,
                "bFilter": false,
                "paging": false,
                "info": false
            });

            $("#topsalesLoadingAnimation").remove();
        }
    })
});
