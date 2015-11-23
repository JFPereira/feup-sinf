$(function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/products/shipments",
        success: function (shipments) {
            shipments = JSON.parse(shipments);

            var s = document.createElement('div');
            var text = document.createElement('div');
            s.className = "huge";
            s.innerHTML = shipments;
            text.innerHTML = "Late Shipments";
            document.getElementById('lateshipments').appendChild(s);
            document.getElementById('lateshipments').appendChild(text);
            
        }
    })
});