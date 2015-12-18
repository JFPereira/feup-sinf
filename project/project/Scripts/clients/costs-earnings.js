$(function () {
    var entity = document.getElementById("client-id").getAttribute("value");

    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/clients/" + entity + "/ce",
        success: function (item) {
            item = JSON.parse(item);

            console.log("Cost: " + item.totalCost);
            console.log("Earnings: " + item.totalEarning);
            console.log("Profit: " + item.profit);

            Morris.Bar({
                element: 'ce-bar',
                data: [
                  { y: 'Values', Earnings: item.totalEarning, Costs: item.totalCost, Profit: item.profit }
                ],
                xkey: 'y',
                ykeys: ['Earnings', 'Costs', 'Profit'],
                labels: ['Earnings', 'Costs', 'Profit']
            });

            $('#ceLoadingAnimation').remove();
        }
    });
});