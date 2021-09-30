// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function click(btn) {
    let attribute = btn.getAttribute('formaction');
    let element = document.getElementById('deleteConfirmationSubmit');
    element.setAttribute('href', attribute);
}



$(document).ready(function () {
    $('#dropDownList').multiSelect();
});
