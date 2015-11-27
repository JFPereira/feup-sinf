$(function () {
    var currentYear = new Date().getFullYear()

    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/financial/ytd/" + currentYear,
        success: function (content) {
            content = JSON.parse(content)

            $('#purchasesYtdPlaceholder').append(content.purchases)
            $('#purchasesYtdPlaceholderLoadingAnimation').remove()

            $('#salesYtdPlaceholder').append(content.sales)
            $('#salesYtdPlaceholderLoadingAnimation').remove()

            $('#revenueYtdPlaceholder').append(content.revenue)
            $('#revenueYtdPlaceholder').parents().eq(3).addClass(content.revenue > 0 ? 'panel-green' : 'panel-red')
            $('#revenueYtdPlaceholderLoadingAnimation').remove()
        }
    })
});
