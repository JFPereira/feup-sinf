$(function () {
    var currentYear = new Date().getFullYear();

    // append year spinner
    $('#purchasesYoySpinnerPlaceholder').append(
        '<input id="purchasesYoySpinner" class="text-center" type="text" value="' + currentYear + '" name="purchasesYoySpinner">');

    var spinner = $("input[name='purchasesYoySpinner']");

    spinner.TouchSpin({
        min: 1900,
        max: currentYear,
        prefix: 'Year',
        verticalbuttons: true,
        verticalupclass: 'glyphicon glyphicon-plus',
        verticaldownclass: 'glyphicon glyphicon-minus'
    });

    renderPurchasesYoy(currentYear);

    spinner.on("touchspin.on.stopspin", function () {
        renderPurchasesYoy(spinner.val());
    });
});

function renderPurchasesYoy(year) {
    // clear previous morris bar chart
    removeAllChildrenOfNode('purchasesYoyPlaceholder');

    // remove any existing animated loading cog
    removeAllChildrenOfNode('purchasesYoyPlaceholderLoadingAnimation');

    // show animated loading cog
    $('#purchasesYoyPlaceholderLoadingAnimation').append('<i class="fa fa-cog fa-spin fa-3x"></i>');

    // get and append morris bar chart
    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/financial/purchases/yoy/" + year,
        success: function (content) {
            content = JSON.parse(content);

            Morris.Bar({
                element: 'purchasesYoyPlaceholder',
                data: [
                    {
                        y: 'January',
                        a: content[0][0],
                        b: content[0][1]
                    }, {
                        y: 'February',
                        a: content[1][0],
                        b: content[1][1]
                    }, {
                        y: 'March',
                        a: content[2][0],
                        b: content[2][1]
                    }, {
                        y: 'April',
                        a: content[3][0],
                        b: content[3][1]
                    }, {
                        y: 'May',
                        a: content[4][0],
                        b: content[4][1]
                    }, {
                        y: 'June',
                        a: content[5][0],
                        b: content[5][1]
                    }, {
                        y: 'July',
                        a: content[6][0],
                        b: content[6][1]
                    }, {
                        y: 'August',
                        a: content[7][0],
                        b: content[7][1]
                    }, {
                        y: 'September',
                        a: content[8][0],
                        b: content[8][1]
                    }, {
                        y: 'October',
                        a: content[9][0],
                        b: content[9][1]
                    }, {
                        y: 'November',
                        a: content[10][0],
                        b: content[10][1]
                    }, {
                        y: 'December',
                        a: content[11][0],
                        b: content[11][1]
                    }
                ],
                xkey: 'y',
                ykeys: ['a', 'b'],
                labels: ['Previous year', 'This year'],
                hideHover: 'auto',
                resize: true
            });

            // remove animated loading cog
            removeAllChildrenOfNode('purchasesYoyPlaceholderLoadingAnimation');
        }
    });
}

function removeAllChildrenOfNode(elementId) {
    var node = document.getElementById(elementId);

    while (node.firstChild)
        node.removeChild(node.firstChild);
}
