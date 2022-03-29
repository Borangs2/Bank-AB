// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(function () {
    $(".navbar-toggler").on("click", function (e) {
        $(".tm-header").toggleClass("show");
        e.stopPropagation();
    });

    $("html").click(function (e) {
        var header = document.getElementById("tm-header");

        if (!header.contains(e.target)) {
            $(".tm-header").removeClass("show");
        }
    });

    $("#tm-nav .nav-link").click(function (e) {
        $(".tm-header").removeClass("show");
    });
});