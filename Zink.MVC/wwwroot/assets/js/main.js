const toggleBtn = document.getElementsByClassName("toggle-button")[0]
const showBtn = document.getElementsByClassName("show-btn")[0]
const navLinks = document.getElementsByClassName("navbar-links")[0]
const dropBtn = document.querySelectorAll(".left-menu li")
const navLeft = document.querySelectorAll(".nav-left")
const showList = document.querySelectorAll(".show-list")
const favorite = document.querySelector(".favorite");

toggleBtn.addEventListener("click", () => {
    navLinks.classList.toggle("active")
});

showBtn.addEventListener("mouseover", (e) => {
    navLinks.classList.toggle("active")
})

navLinks.addEventListener("mouseleave", (e) => {
    navLinks.classList.remove("active")
})