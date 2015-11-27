$("document").ready(function () {
    setTimeout(function () {
        $("input#sales-booking-button").trigger('click');
    }, 10);
});

var myChart = null;
function updateSalesBooking() {



    var year = $("select#sb-year").find(":selected").val();
    var month = $("select#sb-month").find(":selected").val();


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

                
                if (myChart == null) {
                    myChart = new Morris.Bar({
                        element: 'salesbooking',
                        data: dataS,
                        xkey: 'name',
                        ykeys: ['value'],
                        labels: ['Sales Volume'],
                        hidehover: 'auto'
                    }).on('click', function (i, row) {
                        $(location).attr('href', '/Products/Show/' + sales[i].codArtigo)
                    });
                }
                else {
                    myChart.setData(dataS);
                }
                $("#salesBookingAnimation").remove();
            }
        })
    }
    else {
        $.ajax({
            dataType: "json",
            url: "http://localhost:49328/api/sales/psb/" + year + "/" + month,
            success: function (sales) {
                sales = JSON.parse(sales);

                var dataS = [];
                $.each(sales, function (i) {
                    dataS.push({ name: sales[i].nome, value: sales[i].valorVendas });
                });

                if (myChart == null) {
                    myChart = new Morris.Bar({
                        element: 'salesbooking',
                        data: dataS,
                        xkey: 'name',
                        ykeys: ['value'],
                        labels: ['Sales Volume'],
                        hidehover: 'auto'
                    }).on('click', function (i, row) {
                        $(location).attr('href', '/Products/Show/' + sales[i].codArtigo)
                    });
                }
                else {
                    myChart.setData(dataS);
                }

                $("#salesBookingAnimation").remove();
            }
        })
    }
}
