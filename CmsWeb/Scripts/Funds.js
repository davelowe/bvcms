﻿$(function () {
    $(".clickEdit").editable("/Setup/Fund/EditOrder/", {
        tooltip: "Click to edit...",
        style: 'display: inline',
        width: '60px',
        submit: "OK",
        height: 25
    });
    $(".clickSelect").editable("/Setup/Fund/EditStatus/", {
        tooltip: "Click to edit...",
        data: " {'1':'Open','2':'Closed'}",
        loadtype: "POST",
        type: "select",
        submit: "OK",
        style: 'display: inline'
    });
    $("a.sortable").click(function (ev) {
        ev.preventDefault();
        window.location = "/Setup/Fund?sort=" + $(this).text();
    });
    $(".bt").button();
});