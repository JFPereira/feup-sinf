$(function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/clients/top",
        success: function (clients) {
            clients = JSON.parse(clients);

            var data = [];
            $.each(clients, function (i) {
                data.push({ label: clients[i].entity + " - " + clients[i].name, data: clients[i].percentage });
            });

            var plot = $.plot("#placeholderA", data, {
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
                        var entity_name = label.split(' - ');

                        return '<a class="pie-legend" href="/Clients/Show/' + entity_name[0] + '">' + entity_name[1] + '</a>';
                    }
                }
            });

            $("#placeholderA").bind("plotclick", function (event, pos, item) {
                if (item) {
                    // split the string label in entity
                    var entity = item.series.label.split(' - ')[0];
                    $(location).attr('href', '/Clients/Show/' + entity);
                }
            });

            $("#placeholderALoadingAnimation").remove();
        }
    })
});
