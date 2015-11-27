$(function () {
    var product = document.getElementById("productID").getAttribute("value");

    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/products/shipments/" + product,
        success: function (shipments) {
            shipments = JSON.parse(shipments);

            var s = document.getElementById('productshipments');
            s.innerHTML = shipments;

            $("#productshipmentsLoadingAnimation").remove();
        }
    })
});
