$(function () {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/clients/top",
        success: function (clients) {
            clients = JSON.parse(clients);

            var data = [];
            $.each(clients, function (i) {
                data.push({ label: clients[i].name, data: clients[i].percentage });
            });

            console.log(data);

            $.plot("#placeholder", data, {
                series: {
                    pie: {
                        show: true,
                        radius: 1,
                        label: {
                            show: true,
                            radius: 1,
                            formatter: labelFormatter,
                            background: {
                                opacity: 0.8
                            }
                        }
                    }
                },
                grid: {
                    hoverable: true,
                    clickable: false
                },
                legend: {
                    show: true
                }
            });
        }
    })
});

// A custom label formatter used by several of the plots

function labelFormatter(label, series) {
    return "<div style='font-size:8pt; text-align:center; padding:2px; color:white;'>" + label + "<br />" + Math.round(series.percent) + "%</div>";
}

//

function setCode(lines) {
    $("#code").text(lines.join("\n"));
}
