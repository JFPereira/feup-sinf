$(function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/financial/global",
        success: function (global) {
            global = JSON.parse(global);

            var dataG = [];
            $.each(global, function (i) {
                var mes = global[i].Ano + "-" + global[i].Mes;
                dataG.push({ month: mes, sales: global[i].Vendas, purchases: global[i].Compras });
            });

            new Morris.Line({
                // ID of the element in which to draw the chart.
                element: 'globalfinancial',
                // Chart data records -- each entry in this array corresponds to a point on
                // the chart.
                data: dataG,
                // The name of the data record attribute that contains x-values.
                xkey: 'month',
                // A list of names of data record attributes that contain y-values.
                ykeys: ['sales', 'purchases'],
                // Labels for the ykeys -- will be displayed when you hover over the
                // chart.
                labels: ['Sales', 'Purchases'],
            });
        }
    })
});
