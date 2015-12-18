$(function () {
    var currentYear = new Date().getFullYear();

    renderProductUnitsSold(currentYear);
});

function renderProductUnitsSold(year) {
    // clear previous morris bar chart
    removeAllChildrenOfNode('productunitssold');

    // remove any existing animated loading cog
    removeAllChildrenOfNode('productunitssoldLoadingAnimation');

    // show animated loading cog
    $('#productunitssoldLoadingAnimation').append('<i class="fa fa-cog fa-spin fa-3x"></i>');
    var product = document.getElementById("productID").getAttribute("value");

    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/products/" + product + "/sales/" + year,
        success: function (sales) {
            sales = JSON.parse(sales);
            sales = sales.Vendidos;

            var s = document.getElementById('productunitssold');
            s.innerHTML = sales;

            // remove animated loading cog
            removeAllChildrenOfNode('productunitssoldLoadingAnimation');
        }
    })
}
