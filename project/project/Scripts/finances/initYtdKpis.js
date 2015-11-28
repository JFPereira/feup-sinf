$(function () {
    var currentYear = new Date().getFullYear()

    renderYtdKpis(currentYear);
});

function renderYtdKpis(year) {
    clearKpisContentAndShowLoadingAnimation()

    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/financial/ytd/" + year,
        success: function (content) {
            content = JSON.parse(content)

            // sales
            removeAllChildrenOfNode('salesYtdPlaceholderLoadingAnimation')
            $('#salesYtdPlaceholder').append(content.sales)

            // purchases
            removeAllChildrenOfNode('purchasesYtdPlaceholderLoadingAnimation')
            $('#purchasesYtdPlaceholder').append(content.purchases)

            // revenue
            removeAllChildrenOfNode('revenueYtdPlaceholderLoadingAnimation')
            $('#revenueYtdPlaceholder').append(content.revenue)

            $('#revenueYtdPlaceholder').parents().eq(3)
                .removeClass('panel-default panel-green panel-red')
                .addClass(content.revenue > 0 ? 'panel-green' : 'panel-red')
        }
    })
}

function clearKpisContentAndShowLoadingAnimation() {
    removeAllChildrenOfNode('salesYtdPlaceholder')
    $('#salesYtdPlaceholderLoadingAnimation').append('<i class="fa fa-cog fa-spin fa-3x"></i>')

    removeAllChildrenOfNode('purchasesYtdPlaceholder')
    $('#purchasesYtdPlaceholderLoadingAnimation').append('<i class="fa fa-cog fa-spin fa-3x"></i>')

    removeAllChildrenOfNode('revenueYtdPlaceholder')
    $('#revenueYtdPlaceholderLoadingAnimation').append('<i class="fa fa-cog fa-spin fa-3x"></i>')
}
