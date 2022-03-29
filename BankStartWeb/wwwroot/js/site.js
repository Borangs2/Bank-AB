// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


var navbarBtn = document.getElementById("navbar-toggler");
var header = document.getElementById("tm-header");


navbarBtn.addEventListener("click", () => {
    header.classList.toggle("show");

    //$(".navbar-toggler").on("click", function (e) {
    //    $(".tm-header").toggleClass("show");
    //    e.stopPropagation();
    //});

    //$("html").click(function (e) {
    //    var header = document.getElementById("tm-header");

    //    if (!header.contains(e.target)) {
    //        $(".tm-header").removeClass("show");
    //    }
    //});

    //$("#tm-nav .nav-link").click(function (e) {
    //    $(".tm-header").removeClass("show");
    //});
});



var url = window.location.pathname;
var filename = url.split('/').pop();

var homeBtn = document.getElementById("homeBtn");
var custBtn = document.getElementById("custBtn");

switch (filename) {
    //Index
    case "":
        homeBtn.classList.add("active");
        break;
    case "Customers":
        custBtn.classList.add("active");
}



function RemoveActiveButton() {
    homeBtn.classList.remove("active");
    custBtn.classList.remove("active");
}