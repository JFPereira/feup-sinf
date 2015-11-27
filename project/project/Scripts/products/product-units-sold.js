$(function () {
    var product = document.getElementById("productID").getAttribute("value");


    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/products/" + product + "/sales",
        success: function (sales) {
            sales = JSON.parse(sales);
            sales = sales.Vendidos;

            var s = document.getElementById('productunitssold');
            s.innerHTML = sales;

            $("#productunitssoldLoadingAnimation").remove();
        }
    })
});