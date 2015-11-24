$(function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/sales/top/2015",
        success: function (sales) {
            sales = JSON.parse(sales);

            var dataS = [];
            $.each(sales, function (i) {
                dataS.push([i + 1, sales[i].entity, sales[i].numPurchases, sales[i].purchaseValue ]);
            });

            $('#top-purchases').dataTable({
                data: dataS,
                columns: [
                    { title: "#"},
                    { title: "Entity" },
                    { title: "Units" },
                    { title: "Purchase Value" }
                ]
            });
        }
    })
});
