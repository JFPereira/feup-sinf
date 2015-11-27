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
    console.log(year1);
    console.log(year2);
    console.log(month1);
    console.log(month2);

    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/sales/sg/" + year1 + "/" + month1 + "/" + year2 + "/" + month2,
        success: function (sales) {
            sales = JSON.parse(sales);

            /*var dataS = [];
            dataS.push({ name: sales[0].Nome, value: sales[0].Percentagem });

            if (growthChart == null) {
                growthChart = new Morris.Bar({
                    element: 'salesgrowth',
                    data: dataS,
                    xkey: 'name',
                    ykeys: ['value'],
                    labels: ['Growth Percentage'],
                    hidehover: 'auto'
                });
            }
            else {
                growthChart.setData(dataS);
            }*/
            console.log(sales[0].Percentagem);
            var s = document.getElementById('sales-growth-per');
            s.innerHTML = sales[0].Percentagem;
        }
    })
   
}
