$(function () {
    var currentYear = new Date().getFullYear();

    renderProductTopClients(currentYear);
});

function renderProductTopClients(year) {

    removeAllChildrenOfNode('ProductPlaceholder');

    // remove any existing animated loading cog
    removeAllChildrenOfNode('ProductPlaceholderLoadingAnimation');

    // show animated loading cog
    $('#ProductPlaceholderLoadingAnimation').append('<i class="fa fa-cog fa-spin fa-3x"></i>');
    var product = document.getElementById("productID").getAttribute("value");
    console.log(year);
    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/products/" + product + "/top-clients/" + year,
        success: function (clients) {
            console.log(clients);
            clients = JSON.parse(clients);

            console.log(clients);
            var data = [];
            $.each(clients, function (i) {
                data.push({ label: clients[i].entity + " - " + clients[i].name, data: clients[i].percentage });
            });

            console.log(data);

            var plot = $.plot("#ProductPlaceholder", data, {
                series: {
                    pie: {
                        show: true
                    }
                },
                grid: {
                    hoverable: true,
                    clickable: true
                },
                tooltip: true,
                tooltipOpts: {
                    content: "%p.0%, %s", // show percentages, rounding to 2 decimal places
                    shifts: {
                        x: 20,
                        y: 0
                    }
                },
                legend: {
                    labelFormatter: function (label, series) {
                        var clientCod_description = label.split(' - ');

                        return '<a class="pie-legend" href="/Clients/Show/' + clientCod_description[0] + '">' + clientCod_description[1] + '</a>';
                    }
                }
            });

            $("#ProductPlaceholder").bind("plotclick", function (event, pos, item) {
                if (item) {
                    // split the string label in entity
                    var clientCod = item.series.label.split(' - ')[0];
                    $(location).attr('href', '/Clients/Show/' + clientCod);
                }
            });

            $("#ProductPlaceholderLoadingAnimation").remove();
        }
    })
}