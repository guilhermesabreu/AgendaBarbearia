$(document).ready(function () {
    var url = window.location.href;
    var logo = $("#logo").text();

    if (url.indexOf("Agenda") > -1) {
        $(".voltar").attr("class", "hidden");
    }
});