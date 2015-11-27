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
    console.log(year1);
    console.log(year2);
    console.log(month1);
    console.log(month2);

    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/sales/sg/" + year1 + "/" + month1 + "/" + year2 + "/" + month2,
        success: function (sales) {
            sales = JSON.parse(sales);

            console.log(sales[0].Valor);
            var s = document.getElementById('sales-growth-value');
            s.innerHTML = sales[0].Valor;
        }
    })
   
}
