document.addEventListener("DOMContentLoaded", function () {
    const sidebar = document.querySelector(".sidebar_menu");
    const toggleBtn = document.querySelector(".toggle-btn");
    const mainContainer = document.querySelector(".main-container");

    toggleBtn.addEventListener("click", function () {
        sidebar.classList.toggle("closed");

        if (window.innerWidth <= 768) {
            if (sidebar.classList.contains("closed")) {
                mainContainer.style.marginTop = "50px";
            } else {
                mainContainer.style.marginTop = "0px";
            }
        }
    });
});
