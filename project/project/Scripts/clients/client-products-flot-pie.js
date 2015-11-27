$(function () {

    var entity = document.getElementById("client-id").getAttribute("value");

    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/clients/" + entity + "/top-products",  
        success: function (products) {
            products = JSON.parse(products);

            var data = [];
            $.each(products, function (i) {
                data.push({ label: products[i].codArtigo + " - " + products[i].description, data: products[i].percentage });
            });

            var plot = $.plot("#clientTopProductsPlaceholder", data, {
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
                        // split the string label into an array with the entity and name of the company
                        var productCod_description = label.split(' - ');

                        return '<a class="pie-legend" href="/Products/Show/' + productCod_description[0] + '">' + productCod_description[1] + '</a>';
                    }
                }
            });

            $("#clientTopProductsPlaceholder").bind("plotclick", function (event, pos, item) {
                if (item) {
                    // split the string label in entity
                    var productCod = item.series.label.split(' - ')[0];
                    $(location).attr('href', '/Products/Show/' + productCod);
                }
            });

            $('#top10ProductsLoadingAnimation').remove();
        }
    })
});