$(function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/products/shipmentslist",
        success: function (shipments) {
            shipments = JSON.parse(shipments);

            var dataS = [];
            $.each(shipments, function (i) {
                var a = '<a href="/Clients/Show/' + shipments[i].entity + '">' + shipments[i].entity + '</a>';
                var p = '<a href="/Products/Show/' + shipments[i].codArtigo + '">' + shipments[i].nomeArtigo + '</a>';
                dataS.push([shipments[i].date, p, shipments[i].quantEnc, shipments[i].quantSat, a]);
            });

            $('#shipments').dataTable({
                data: dataS,
                columns: [
                    { title: "Date" },
                    { title: "Product" },
                    { title: "Order Amount" },
                    { title: "Fulfilled Amount" },
                    { title: "Entity" }
                ],
                destroy: true
            });

            $("#shipmentsLoadingAnimation").remove();
        }
    })
});