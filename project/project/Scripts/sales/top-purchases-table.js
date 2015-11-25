$(function () {

    var year = document.getElementById("year").getAttribute("value");
    var month = document.getElementById("month").getAttribute("value");
    var day = document.getElementById("day").getAttribute("value");
    

    if (year == null) {
        $.ajax({
            dataType: "json",
            url: "http://localhost:49328/api/sales/top/2015",
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
                    ]
                });
            }
        })
    }
    else if (month == null) {
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
                    ]
                });
            }
        })
    }
    else if (day == null) {
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
                    ]
                });
            }
        })
    }
    else {
        $.ajax({
            dataType: "json",
            url: "http://localhost:49328/api/sales/top/" + year + "/" + month + "/" + day,
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
                    ]
                });
            }
        })
    }
});
