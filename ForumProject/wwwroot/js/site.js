// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var coll = document.getElementsByClassName("collapsible");
var i;

for (i = 0; i < coll.length; i++) {
    coll[i].addEventListener("click", function () {
        this.classList.toggle("active");
        var content = this.nextElementSibling;
        if (content.style.maxHeight) {
            content.style.maxHeight = null;
        } else {
            content.style.maxHeight = content.scrollHeight + "px";
        }
    });
}

let collapseBox = document.querySelector('collapsible');

Array.from(collapseBox).forEach(box => {
    box.addEventListener('keydown', e => {
        //32 == spacebar
        //13 == enter
        if (e.which === 32 || e.which === 13) {
            e.preventDefault();
            box.click();
        };
    });
});