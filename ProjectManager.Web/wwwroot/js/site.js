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
    updateDropzones();
});

const drag = (event) => {
    event.dataTransfer.setData("text/plain", event.target.id);
}

const allowDrop = (ev) => {
    ev.preventDefault();
    if (hasClass(ev.target, "dropzone")) {
        addClass(ev.target, "droppable");
    }
}

const clearDrop = (ev) => {
    removeClass(ev.target, "droppable");
}

function hasClass(target, className) {
    return new RegExp('(\\s|^)' + className + '(\\s|$)').test(target.className);
}

function addClass(ele, cls) {
    if (!hasClass(ele, cls)) ele.className += " " + cls;
}

function removeClass(ele, cls) {
    if (hasClass(ele, cls)) {
        var reg = new RegExp('(\\s|^)' + cls + '(\\s|$)');
        ele.className = ele.className.replace(reg, ' ');
    }
}

function updateTaskSection(taskId, sectionId) {
    return $.ajax({
        url: `/Task/UpdateTaskSection/${taskId}?sectionId=${sectionId}`,
        type: 'GET',
        cache: false,
        data: {},
        success: function (response) {
            doSomething('success');
        },
        error: function (response) {
            doSomething(response.status + ' error');
        },
    })
}
function doSomething(response) {
    console.log(response);
}
const drop = (event) => {
    event.preventDefault();
    //card id "task id" from the drag event
    const data = event.dataTransfer.getData("text/plain");
    //
    const element = document.querySelector(`#${data}`);
    try {
        const newSectionId = event.target.parentNode.id;
        const prefix = 'task';
        const taskId = data.substring(prefix.length);
        var state = updateTaskSection(taskId, newSectionId).statusCode
        console.log(state);
        // remove the spacer content from dropzone
        event.target.removeChild(event.target.firstChild);
        // add the draggable content
        event.target.appendChild(element);
        // remove the dropzone parent
        unwrap(event.target);
    } catch (error) {
        console.warn("can't move the item to the same place")
    }
    updateDropzones();
}

const updateDropzones = () => {
    /* after dropping, refresh the drop target areas
      so there is a dropzone after each item
      using jQuery here for simplicity */

    var dz = $('<div class="dropzone rounded" ondrop="drop(event)" ondragover="allowDrop(event)" ondragleave="clearDrop(event)"> &nbsp; </div>');

    // delete old dropzones
    $('.dropzone').remove();

    // insert new dropdzone after each item
    dz.insertAfter('.card.draggable');
    dz.insertBefore('.card.draggable');

    // insert new dropzone in any empty swimlanes
    $(".items:not(:has(.card.draggable))").append(dz);
};

function unwrap(node) {
    node.replaceWith(...node.childNodes);
}

function refreshPage() {
    window.location.reload();
}
