$(function () {
    var currentYear = new Date().getFullYear();

    // append year spinner
    $('#salesRSSSpinnerPlaceholder').append(
        '<input id="salesRSSSpinner" class="text-center" type="text" value="' + currentYear + '" name="salesRSSSpinner">');

    var spinner1 = $("input[name='salesRSSSpinner']");

    spinner1.TouchSpin({
        min: 1900,
        max: currentYear,
        prefix: 'Year',
        verticalbuttons: true,
        verticalupclass: 'glyphicon glyphicon-plus',
        verticaldownclass: 'glyphicon glyphicon-minus'
    });

    spinner1.on("touchspin.on.stopspin", function () {
        updateRSS(spinner1.val());
    });

    updateRSS(currentYear);
});
function updateRSS(year) {


    $('#salesRSSSpinner').val(year);

    // clear previous morris bar chart
    removeAllChildrenOfNode('salesRSSPlaceholder');

    // remove any existing animated loading cog
    removeAllChildrenOfNode('salesRSSLoadingAnimation');

    // show animated loading cog
    $('#salesRSSLoadingAnimation').append('<i class="fa fa-cog fa-spin fa-3x"></i>');

   
    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/sales/rss/" + year,
        success: function (sales) {
            sales = JSON.parse(sales);

            var dataS = [];
            $.each(sales, function (i) {
                dataS.push([i + 1, sales[i].pais, sales[i].valor + "€", sales[i].percentagem + "%"]);
            });

            $('#salesRSSPlaceholder').dataTable({
                data: dataS,
                columns: [
                    { title: "#" },
                    { title: "Country" },
                    { title: "Sales Volume" },
                    { title: "Percentage" }
                ],
                destroy: true,
                "bFilter": false,
                "info": false
            });

            // remove animated loading cog
            removeAllChildrenOfNode('salesRSSLoadingAnimation');
        }
    });
    
}
