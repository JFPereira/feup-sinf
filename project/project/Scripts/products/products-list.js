$(function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/products",
        success: function (products) {
            products = JSON.parse(products);

            var dataS = [];
            $.each(products, function (i) {
                var action = '<a id="product-button" href="/Products/Show/' + products[i].CodArtigo + '" class="btn btn-success btn-block">Details</a>';
                //if (products[i].CodArtigo != "VD")
                dataS.push([products[i].CodArtigo, products[i].DescArtigo, products[i].Tipo, products[i].Custo, products[i].PrecoMedio, products[i].Stock, action]);
            });

            $('#products').dataTable({
                data: dataS,
                columns: [
                    { title: "Code" },
                    { title: "Description" },
                    { title: "Type" },
                    { title: "Cost" },
                    { title: "Average Cost" },
                    { title: "Stock" },
                    { title: "Action" }
                ],
                destroy: true
            });

            $("#productsListLoadingAnimation").remove();
        }
    })
});