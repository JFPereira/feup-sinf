$("document").ready(function () {
    setTimeout(function () {
        $("input#sales-growth-button").trigger('click');
    }, 10);
});

var growthChart = null;
function updateSalesGrowth() {

    var year1 = $("select#sg-year1").find(":selected").val();
    var month1 = $("select#sg-month1").find(":selected").val();
    var year2 = $("select#sg-year2").find(":selected").val();
    var month2 = $("select#sg-month2").find(":selected").val();

    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/sales/sg/" + year1 + "/" + month1 + "/" + year2 + "/" + month2,
        success: function (sales) {
            sales = JSON.parse(sales);

            var s = document.getElementById('sales-growth-value');
            s.innerHTML = sales[0].Valor;

            $("#salesGrowthValueAnimation").remove();
        }
    })
   
}
