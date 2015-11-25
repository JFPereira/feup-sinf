$(function () {

    var year = document.getElementById("year").getAttribute("value");
    var month = document.getElementById("month").getAttribute("value");
    var day = document.getElementById("day").getAttribute("value");
    

    if (year == null) {
        $.ajax({
            dataType: "json",
            url: "http://localhost:49328/api/sales/psb/2015",
            success: function (sales) {
                sales = JSON.parse(sales);

                var dataS = [];
                $.each(sales, function (i) {
                    dataS.push({ name: sales[i].nome, value: sales[i].valorVendas });
                });

                Morris.Bar({
                    element: 'salesbooking',
                    data: dataS,
                    xkey: 'name',
                    ykeys: ['value'],
                    labels: ['Volume de Vendas'],
                    hidehover: 'auto'
                });
            }
        })
    }
    else if (month == null) {
        $.ajax({
            dataType: "json",
            url: "http://localhost:49328/api/sales/psb/" + year,
            success: function (sales) {
                sales = JSON.parse(sales);

                var dataS = [];
                $.each(sales, function (i) {
                    dataS.push({ name: sales[i].nome, value: sales[i].valorVendas });
                });

                Morris.Bar({
                    element: 'salesbooking',
                    data: dataS,
                    xkey: 'name',
                    ykeys: ['value'],
                    labels: ['Volume de Vendas'],
                    hidehover: 'auto'
                });
            }
        })
    }
    else if (day == null) {
        $.ajax({
            dataType: "json",
            url: "http://localhost:49328/api/sales/psb/" + year + "/" + month,
            success: function (sales) {
                sales = JSON.parse(sales);

                var dataS = [];
                $.each(sales, function (i) {
                    dataS.push({ name: sales[i].nome, value: sales[i].valorVendas });
                });

                Morris.Bar({
                    element: 'salesbooking',
                    data: dataS,
                    xkey: 'name',
                    ykeys: ['value'],
                    labels: ['Volume de Vendas'],
                    hidehover: 'auto'
                });
            }
        })
    }
    else {
        $.ajax({
            dataType: "json",
            url: "http://localhost:49328/api/sales/psb/" + year + "/" + month + "/" + day,
            success: function (sales) {
                sales = JSON.parse(sales);

                var dataS = [];
                $.each(sales, function (i) {
                    dataS.push({ name: sales[i].nome, value: sales[i].valorVendas });
                });

                Morris.Bar({
                    element: 'salesbooking',
                    data: dataS,
                    xkey: 'name',
                    ykeys: ['value'],
                    labels: ['Volume de Vendas'],
                    hidehover: 'auto'
                });
            }
        })
    }
});