$(function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/financial/purchases",
        success: function (content) {
            content = JSON.parse(content);

            var domElem = document.getElementById('purchasesYtdPlaceholder');
            domElem.innerHTML = content;
        }
    })
});
