$(function () {
    var entity = document.getElementById("client-id").getAttribute("value");

    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/clients/" + entity + "/ce",
        success: function (item) {
            item = JSON.parse(item);

            Morris.Donut({
                element: 'ce-graph',
                data: [
                  { value: item.totalCost, label: 'Costs' },
                  { value: item.totalEarning, label: 'Earnings' }
                ],
                formatter: function (x, data) { return data.value + "€ (" + Math.round(data.value / item.total * 1000) / 10 + "%)"; }
            }).on('click', function (i, row) {
                console.log(i, row);
            });

            $("div#ce").append(
                '<div class="col-lg-3 col-md-6">' +
                    '<div class="panel panel-primary">' +
                        '<div class="panel-heading">' +
                            '<div class="row">' +
                                '<div class="col-xs-3">' +
                                    '<i class="fa fa-eur fa-5x"></i>' +
                                '</div>' +
                                '<div class="col-xs-9 text-right">' +
                                    '<div id="profit" class="huge">' + item.profit + ' €</div>' +
                                    '<div>Total Profit</div>' +
                                '</div>' +
                            '</div>' +
                        '</div>' +
                    '</div>' +
                '</div>');
        }
    });
});