$(function () {
    var currentYear = new Date().getFullYear();

    // append year spinner
    $('#TPSpinnerPlaceholder').append(
        '<input id="TPSpinner" class="text-center" type="text" value="' + currentYear + '" name="TPSpinner">');

    var spinner1 = $("input[name='TPSpinner']");

    spinner1.TouchSpin({
        min: 1900,
        max: currentYear,
        prefix: 'Year',
        verticalbuttons: true,
        verticalupclass: 'glyphicon glyphicon-plus',
        verticaldownclass: 'glyphicon glyphicon-minus'
    });

    spinner1.on("touchspin.on.stopspin", function () {
        updateTopPurchases(spinner1.val());
    });

    updateTopPurchases(currentYear);
});

function updateTopPurchases(year) {

    $('#TPSpinner').val(year);
    // clear previous morris bar chart
    removeAllChildrenOfNode('top-purchases');

    // remove any existing animated loading cog
    removeAllChildrenOfNode('TPLoadingAnimation');

    // show animated loading cog
    $('#TPLoadingAnimation').append('<i class="fa fa-cog fa-spin fa-3x"></i>');


    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/sales/top/" + year,
        success: function (sales) {
            sales = JSON.parse(sales);

            var dataS = [];
            $.each(sales, function (i) {
                var a = '<a href="/Clients/Show/' + sales[i].entity + '">' + sales[i].entity + '</a>';
                dataS.push([i + 1, a, sales[i].numPurchases, sales[i].purchaseValue + "€", sales[i].date]);
            });

            $('#top-purchases').dataTable({
                data: dataS,
                columns: [
                    { title: "#" },
                    { title: "Client" },
                    { title: "Units Sold" },
                    { title: "Sale Volume" },
                    { title: "Date" }
                ],
                destroy: true,
                "bFilter": false,
                "paging": false,
                "info": false
            });

            // remove animated loading cog
            removeAllChildrenOfNode('TPLoadingAnimation');
        }
    });
}