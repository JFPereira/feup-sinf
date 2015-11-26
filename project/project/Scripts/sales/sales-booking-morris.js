$("document").ready(function () {
    setTimeout(function () {
        $("input#sales-booking-button").trigger('click');
    }, 10);
});

function updateSalesBooking() {



    var year = $("select#sb-year").find(":selected").val();
    var month = $("select#sb-month").find(":selected").val();
    var day = $("select#sb-day").find(":selected").val();


    if (month == "None") {
        $.ajax({
            dataType: "json",
            url: "http://localhost:49328/api/sales/psb/" + year,
            success: function (sales) {
                sales = JSON.parse(sales);

                var dataS = [];
                $.each(sales, function (i) {
                    dataS.push({ name: sales[i].nome, value: sales[i].valorVendas });
                });

                new Morris.Bar({
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
    else if (day == "None") {
        $.ajax({
            dataType: "json",
            url: "http://localhost:49328/api/sales/psb/" + year + "/" + month,
            success: function (sales) {
                sales = JSON.parse(sales);

                var dataS = [];
                $.each(sales, function (i) {
                    dataS.push({ name: sales[i].nome, value: sales[i].valorVendas });
                });

                new Morris.Bar({
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

                new Morris.Bar({
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
}
