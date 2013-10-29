﻿$(document).ready(function () {
    $(document).on("click", "a.dialog-options", function (ev) {
        ev.preventDefault();
        var $a = $(this);
        $("<div id='dialog-options' />").load($a.data("target"), function () {
            var d = $(this);
            var f = d.find("form");
            if ($a[0].title)
                f.find("h3.title").text($a[0].title);
            f.modal("show");
            f.attr("action", $a[0].href);
            f.on('hidden', function () {
                d.remove();
            });
            f.validate({
                submitHandler: function (form) {
                    if (form.method.toUpperCase() === 'GET') {
                        form.submit();
                    }
                    else {
                        var q = f.serialize();
                        $.post(form.action, q, function (ret) {
                            if (ret)
                                $.growlUI("", ret);
                            if ($a.data("callback")) {
                                $.InitFunctions[$a.data("callback")]($a);
                            }
                        });
                    }
                    f.modal("hide");
                },
                highlight: function (element) {
                    $(element).closest(".control-group").addClass("error");
                },
                unhighlight: function (element) {
                    $(element).closest(".control-group").removeClass("error");
                }
            });
        });
        return false;
    });

    $('#UnTagAll').live("click", function (ev) {
        ev.preventDefault();
        $('div.dropdown-menu').hide();
        $.block();
        $.post(this.href, null, function (ret) {
            $(".taguntag:visible").text(ret);
            $.unblock();
        });
        return false;
    });
    $(document).on("click", '#AddContact', function (ev) {
        ev.preventDefault();
        var url = this.href;
        bootbox.confirm("Are you sure you want to add a contact for all these people?", function (result) {
            if (result === true) {
                $.block();
                $.post(url, null, function (ret) {
                    $.unblock();
                    if (ret < 0)
                        $.growlUI("error", "too many people to add to a contact (max 100)");
                    else if (ret == 0)
                        $.growlUI("error", "no results");
                    else
                        window.location = ret;
                });
            }
        });
        return false;
    });
    $(document).on("click", '#AddTasks', function (ev) {
        ev.preventDefault();
        var message = "Are you sure you want to add a task for all these people?";
        if (window.location.pathname.contains("/Person"))
            message = "Are you sure you want to add a task for this person?";
        var url = this.href;
        bootbox.confirm(message, function (result) {
            if (result === true) {
                $.block();
                $.post(url, null, function (ret) {
                    $.unblock();
                    if (ret > 100)
                        $.growlUI("error", "too many people to add tasks for (max 100)");
                    else if (ret == 0)
                        $.growlUI("error", "no results");
                    else
                        window.location = "/Task";
                });
            }
        });
        return false;
    });
    $.QueryString = function (q, item) {
        var r = new Object();
        $.each(q.split('&'), function () {
            var kv = this.split('=');
            r[kv[0]] = kv[1];
        });
        return r[item];
    };
    $.block = function (message) {
        if (!message)
            message = '<h1>working on it...</h1>';
        $.blockUI({
            message: message,
            overlayCSS: { opacity: 0 },
            css: {
                border: '3px',
                padding: '15px',
                backgroundColor: '#aaa',
                '-webkit-border-radius': '10px',
                '-moz-border-radius': '10px',
                opacity: .9,
                color: '#000',
                width: '500px'
            }
        });
    };
    $.unblock = function () {
        $.unblockUI();
    };
    $.navigate = function (url, data) {
        url += (url.match(/\?/) ? "&" : "?") + data;
        window.location = url;
    };
    $.DateValid = function (d, growl) {
        var reDate = /^(0?[1-9]|1[012])[\/-](0?[1-9]|[12][0-9]|3[01])[\/-]((19|20)?[0-9]{2})$/i;
        if ($.dateFormat.startsWith('d'))
            reDate = /^(0?[1-9]|[12][0-9]|3[01])[\/-](0?[1-9]|1[012])[\/-]((19|20)?[0-9]{2})$/i;
        var v = true;
        if (!reDate.test(d)) {
            if (growl == true)
                $.growlUI("error", "enter valid date");
            v = false;
        }
        return v;
    };
    $.SortableDate = function (s) {
        var dt;
        if ($.dateFormat.startsWith('d'))
            dt = new Date(s.split('/')[2], s.split('/')[1] - 1, s.split('/')[0]);
        else
            dt = new Date(s.split('/')[2], s.split('/')[0] - 1, s.split('/')[1]);
        var dt2 = dt.getFullYear() + '-' + (dt.getMonth() + 1) + '-' + dt.getDate();
        return dt2;
    };

    jQuery.fn.center = function (parent) {
        if (parent) {
            parent = this.parent();
        } else {
            parent = window;
        }
        this.css({
            "position": "absolute",
            "top": ((($(parent).height() - this.outerHeight()) / 2) + $(parent).scrollTop() + "px"),
            "left": ((($(parent).width() - this.outerWidth()) / 2) + $(parent).scrollLeft() + "px")
        });
        return this;
    };
});