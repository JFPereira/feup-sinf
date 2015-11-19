$(function () {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/sales/top",
        success: function (sales) {
            sales = JSON.parse(sales);

            var dataS = [];
            $.each(sales, function (i) {
                dataS.push([sales[i].entity, sales[i].purchaseValue, sales[i].numPurchases ]);
            });

            console.log(dataS);

            $('#topsales').DataTable({
                data: dataS,
                columns: [
                    { title: "Entity" },
                    { title: "Purchase Value" },
                    { title: "Nr Purchases" }
                ]
            });
        }
    })
});
