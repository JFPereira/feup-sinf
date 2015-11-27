$("document").ready(function () {
    setTimeout(function () {
        $("input#top-purchases-button").trigger('click');
    }, 10);
});

function updateTopPurchases() {

    var year = $("select#tp-year").find(":selected").val();
    var month = $("select#tp-month").find(":selected").val();
    var day = $("select#tp-day").find(":selected").val();
    
    if (month == "None") {
        $.ajax({
            dataType: "json",
            url: "http://localhost:49328/api/sales/top/" + year,
            success: function (sales) {
                sales = JSON.parse(sales);

                var dataS = [];
                $.each(sales, function (i) {
                    dataS.push([i + 1, sales[i].entity, sales[i].numPurchases, sales[i].purchaseValue]);
                });

                $('#top-purchases').dataTable({
                    data: dataS,
                    columns: [
                        { title: "#" },
                        { title: "Entity" },
                        { title: "Units" },
                        { title: "Purchase Value" }
                    ],
                    destroy: true
                });
            }
        })
    }
    else {
        $.ajax({
            dataType: "json",
            url: "http://localhost:49328/api/sales/top/" + year + "/" + month,
            success: function (sales) {
                sales = JSON.parse(sales);

                var dataS = [];
                $.each(sales, function (i) {
                    dataS.push([i + 1, sales[i].entity, sales[i].numPurchases, sales[i].purchaseValue]);
                });

                $('#top-purchases').dataTable({
                    data: dataS,
                    columns: [
                        { title: "#" },
                        { title: "Entity" },
                        { title: "Units" },
                        { title: "Purchase Value" }
                    ],
                    destroy: true
                });
            }
        })
    }
}