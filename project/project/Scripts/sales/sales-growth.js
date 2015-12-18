$(function () {
    var currentYear = new Date().getFullYear();
    var lastYear = new Date().getFullYear() - 1;

    // append year spinner
    $('#salesGrowthSpinnerPlaceholder1').append(
        '<input id="salesGrowthSpinner1" class="text-center" type="text" value="' + lastYear + '" name="salesGrowthSpinner1">');

    $('#salesGrowthSpinnerPlaceholder2').append(
        '<input id="salesGrowthSpinner2" class="text-center" type="text" value="' + currentYear + '" name="salesGrowthSpinner2">');

    var spinner1 = $("input[name='salesGrowthSpinner1']"); 
    var spinner2 = $("input[name='salesGrowthSpinner2']");

    spinner1.TouchSpin({
        min: 1900,
        max: currentYear,
        prefix: 'Year 1',
        verticalbuttons: true,
        verticalupclass: 'glyphicon glyphicon-plus',
        verticaldownclass: 'glyphicon glyphicon-minus'
    });

    spinner2.TouchSpin({
        min: 1900,
        max: currentYear,
        prefix: 'Year 2',
        verticalbuttons: true,
        verticalupclass: 'glyphicon glyphicon-plus',
        verticaldownclass: 'glyphicon glyphicon-minus'
    });

    spinner1.on("touchspin.on.stopspin", function () {
        updateSalesGrowth(spinner1.val(), spinner2.val());
    });
    spinner2.on("touchspin.on.stopspin", function () {
        updateSalesGrowth(spinner1.val(), spinner2.val());
    });

   updateSalesGrowth(lastYear,currentYear);
});

var myChart = null;
function updateSalesGrowth(year1, year2) {

    $('#salesGrowthSpinner1').val(year1);
    $('#salesGrowthSpinner2').val(year2);

    // clear previous morris bar chart
    removeAllChildrenOfNode('salesGrowthPlaceholder');
    myChart = null;
    // remove any existing animated loading cog
    removeAllChildrenOfNode('salesGrowthLoadingAnimation');

    // show animated loading cog
    $('#salesGrowthLoadingAnimation').append('<i class="fa fa-cog fa-spin fa-3x"></i>');

    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/sales/sg/" + year1 + "/" + year2,
        success: function (content) {
            content = JSON.parse(content);

            var myData = [
                        {
                            y: 'Q1',
                            a: content[0].Valor1,
                            b: content[0].Valor2,
                            per: content[0].Percentagem,
                            dif: content[0].Dif
                        }, {
                            y: 'Q2',
                            a: content[1].Valor1,
                            b: content[1].Valor2,
                            per: content[1].Percentagem,
                            dif: content[1].Dif
                        }, {
                            y: 'Q3',
                            a: content[2].Valor1,
                            b: content[2].Valor2,
                            per: content[2].Percentagem,
                            dif: content[2].Dif
                        }, {
                            y: 'Q4',
                            a: content[3].Valor1,
                            b: content[3].Valor2,
                            per: content[3].Percentagem,
                            dif: content[3].Dif
                        }
            ];

            
            if (myChart == null) {
                myChart = Morris.Bar({
                    element: 'salesGrowthPlaceholder',
                    data: myData,
                    xkey: 'y',
                    ykeys: ['a', 'b'],
                    labels: ['Year1', 'Year2'],
                    hideHover: 'auto',
                    resize: true,
                    hoverCallback: function (index, options, content) {
                        return "Value: " + options.data[index].dif + "€;\n" + "Percentage: " + options.data[index].per + "%.";
                    }
                });
            }
            else myChart.setData(myData);

            // remove animated loading cog
            removeAllChildrenOfNode('salesGrowthLoadingAnimation');

        }

    });

}