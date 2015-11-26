$(function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/products/shipments",
        success: function (shipments) {
            shipments = JSON.parse(shipments);

            var s = document.getElementById('lateshipments');
            s.innerHTML = shipments;

            $("#lateshipmentsLoadingAnimation").remove();
        }
    })
});
