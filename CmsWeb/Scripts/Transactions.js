﻿$(document).ready(function () {
    $.preptable = function () {
        $('table.grid > tbody > tr:even').addClass('alt');
        $(".bt").button();
        $(".datepicker").datepicker();
        $('.tip').tooltip({
            delay: 0,
            showURL: false,
            showBody: "|"
        });
    };
    $.preptable();
    $(".filterbatch").live("click", function (e) {
        e.preventDefault();
        $("#name").val($(this).text());
        $('#filter').click();
    });
    $.getTable = function (f, q) {
        q = q || f.serialize();
        $.blockUI();
        $.post("/Manage/Transactions/List", q, function (ret) {
            $('#Transactions').html(ret);
            $.preptable();
            $.unblockUI();
        });
        return false;
    };
    $('#filter').live('click', function (ev) {
        ev.preventDefault();
        var f = $(this).closest('form');
        $('#Page', f).val(1);
        $.getTable(f);
        return false;
    });
    $('.report').live('click', function (ev) {
        ev.preventDefault();
        var sdt = $('#startdt').val();
        var edt = $('#enddt').val();
        if (!sdt || !edt) {
            $.growlUI("error", 'must set date range');
            return false;
        }
        $.blockUI();
        var args = "sdt=" + sdt + "&edt=" + edt;
        $.post($(this).attr("href"), args, function (ret) {
            $('#Transactions').html(ret);
            $.preptable();
            $.unblockUI();
        });
        return false;
    });
    $('#export').live('click', function (ev) {
        ev.preventDefault();
        var f = $(this).closest('form');
        f.attr("action", "/Manage/Transactions/Export");
        f.submit();
        f.attr("action", "/Manage/Transactions/List");
        return false;
    });
    $("a.voidcredit").live("click", function (ev) {
        ev.preventDefault();
        var a = $(this);
        if (a.hasClass("noadmin")) {
            alert("must be admin");
            return false;
        }
        var f = $(this).closest('form');
        var q = f.serialize();
        if (confirm("are you sure?")) {
            if (a.text() === "Credit") {
                var amt = prompt("Amount to credit", "");
                amt = parseFloat(amt);
                if (isNaN(amt))
                    return false;
                q += "&amt=" + amt;
            }
            $.post(a.attr("href"), q, function (ret) {
                if (ret.substring(5, 0) == "error")
                    alert(ret);
                else {
                    $(f).html(ret);
                    $.preptable();
                }
            });
        }
        return false;
    });
    $("a.adjust").live("click", function (ev) {
        ev.preventDefault();
        var a = $(this);
        if (a.hasClass("noadmin")) {
            alert("must be admin");
            return false;
        }
        $("#voidurl").val(a.attr("href"));
        $.blockUI({ message: $('#AdjustForm'), css: { width: '275px'} });
        return false;
    });
    $("#post").click(function (ev) {
        ev.preventDefault();
        var amt = parseFloat($("#amt").val());
        if (isNaN(amt)) {
            $.unblockUI();
            return false;
        }
        var q = $("#form").serialize();
        q += "&amt=" + amt;
        q += "&desc=" + $("#desc").val();
        $.post($("#voidurl").val(), q, function (ret) {
            $.unblockUI();
            if (ret.substring(5, 0) == "error")
                alert(ret);
            else {
                $(f).html(ret);
                $.preptable();
            }
        });
        return false;
    });

    $('#cancel').click(function () {
        $.unblockUI();
        return false;
    });
    $('table.grid > tbody > tr:even').addClass('alt');
    $(".bt").button();
});