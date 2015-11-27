$(function () {
    var entity = document.getElementById("client-id").getAttribute("value");

    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/clients/" + entity + "/apc",
        success: function (content) {
            content = JSON.parse(content);

            var apc = document.getElementById('average-purchase-cost');
            var numAPC = document.getElementById('num-purchases');

            apc.innerHTML = content.averagePurchaseCost + " €";

            if(content.numPurchases > 1)
                numAPC.innerHTML = content.numPurchases + " purchases";
            else
                numAPC.innerHTML = content.numPurchases + " purchase";

            $('#averagePurchaseCostLoadingAnimation').remove();
        }
    })
});