$(function () {
    var currentYear = new Date().getFullYear();

    // append year spinner
    $('#pivotSpinnerPlaceholder').append(
        '<input id="yearPivotSpinner" class="text-center" type="text" value="' + currentYear + '" name="yearPivotSpinner">');

    var spinner = $("input[name='yearPivotSpinner']");

    spinner.TouchSpin({
        min: 1900,
        max: currentYear,
        prefix: 'Year',
        verticalbuttons: true,
        verticalupclass: 'glyphicon glyphicon-plus',
        verticaldownclass: 'glyphicon glyphicon-minus'
    });

    spinner.on("touchspin.on.stopspin", function () {
        
        renderPivot(spinner.val());
    });
});

function renderPivot(year) {
    var spinner1 = $("input[name='salesGrowthSpinner1']");
    updateSalesGrowth(spinner1.val(), year);
    updateYSalesBooking(year);
    updateTopPurchases(year);
    updateRSS(year);
}

function removeAllChildrenOfNode(elementId) {
    var node = document.getElementById(elementId);

    while (node.firstChild)
        node.removeChild(node.firstChild);
}
