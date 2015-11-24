$(function () {
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
                hidehover:'auto'
            });
        }
    })
});