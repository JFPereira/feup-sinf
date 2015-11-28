$(function () {
    var currentYear = new Date().getFullYear();

    // append year spinner
    $('#yearPivotSpinnerPlaceholder').append(
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
    renderYtdKpis(year);
    renderSalesYoy(year);
    renderPurchasesYoy(year);
}

function removeAllChildrenOfNode(elementId) {
    var node = document.getElementById(elementId);

    while (node.firstChild)
        node.removeChild(node.firstChild);
}
