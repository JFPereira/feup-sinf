$(function () {
    var currentYear = new Date().getFullYear();

    // append year spinner
    $('#salesYBookingSpinnerPlaceholder').append(
        '<input id="salesYBookingSpinner" class="text-center" type="text" value="' + currentYear + '" name="salesYBookingSpinner">');

    var spinner1 = $("input[name='salesYBookingSpinner']");

    spinner1.TouchSpin({
        min: 1900,
        max: currentYear,
        prefix: 'Year',
        verticalbuttons: true,
        verticalupclass: 'glyphicon glyphicon-plus',
        verticaldownclass: 'glyphicon glyphicon-minus'
    });

    spinner1.on("touchspin.on.stopspin", function () {
        updateYSalesBooking(spinner1.val());
    });

    updateYSalesBooking(currentYear);
});

var myChartY = null;

function updateYSalesBooking(year) {

    $('#salesYBookingSpinner').val(year);

    // clear previous morris bar chart
    removeAllChildrenOfNode('salesYBookingPlaceholder');

    // remove any existing animated loading cog
    removeAllChildrenOfNode('salesYBookingLoadingAnimation');
    myChartY = null;

    // show animated loading cog
    $('#salesYBookingLoadingAnimation').append('<i class="fa fa-cog fa-spin fa-3x"></i>');

    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/sales/psb/" + year,
        success: function (sales) {
            sales = JSON.parse(sales);

            var dataS = [];
            $.each(sales, function (i) {
                dataS.push({ name: sales[i].nome, value: sales[i].valorVendas , quantity: sales[i].quantidade});
            });


            if (myChartY == null) {
                myChartY = new Morris.Bar({
                    element: 'salesYBookingPlaceholder',
                    data: dataS,
                    xkey: 'name',
                    ykeys: ['value', 'quantity'],
                    labels: ['Sales Volume', 'Quantity'],
                    hidehover: 'auto'
                }).on('click', function (i, row) {
                    $(location).attr('href', '/Products/Show/' + sales[i].codArtigo)
                });
            }
            else {
                myChartY.setData(dataS);
            }

            // remove animated loading cog
            removeAllChildrenOfNode('salesYBookingLoadingAnimation');
        }
    });

}