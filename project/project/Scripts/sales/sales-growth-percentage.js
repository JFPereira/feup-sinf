$("document").ready(function () {
    setTimeout(function () {
        $("input#sales-growth-per-button").trigger('click');
    }, 10);
});

var growthChart = null;
function updateSalesGrowthPer() {

    var year1 = $("select#sgp-year1").find(":selected").val();
    var month1 = $("select#sgp-month1").find(":selected").val();
    var year2 = $("select#sgp-year2").find(":selected").val();
    var month2 = $("select#sgp-month2").find(":selected").val();

    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/sales/sg/" + year1 + "/" + month1 + "/" + year2 + "/" + month2,
        success: function (sales) {
            sales = JSON.parse(sales);

            var s = document.getElementById('sales-growth-per');
            s.innerHTML = sales[0].Percentagem;

            $("#salesGrowthPerAnimation").remove();
        }
    })
   
}
