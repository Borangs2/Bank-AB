var navbarBtn = document.getElementById("navbar-toggler");
var header = document.getElementById("tm-header");


navbarBtn.addEventListener("click", () => {
    header.classList.toggle("show");
});



var url = window.location.pathname;
var filename = url.split('/').pop();

var homeBtn = document.getElementById("homeBtn");
var custBtn = document.getElementById("custBtn");
var loginBtn = document.getElementById("loginBtn");
var adminBtn = document.getElementById("adminBtn");

switch (filename) {
    //Index
    case "":
        homeBtn.classList.add("active");
        break;
    case "Customers":
        custBtn.classList.add("active");
    case "Login":
        loginBtn.classList.add("active");
    case "Admin":
        adminBtn.classList.add("active");
}