$(function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49328/api/financial/sales",
        success: function (content) {
            content = JSON.parse(content);

            var domElem = document.getElementById('salesYtdPlaceholder');
            domElem.innerHTML = content;
        }
    })
});
