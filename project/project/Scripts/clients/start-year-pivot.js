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
    updateMonthlySales(year);
    updateDailySales();
}
