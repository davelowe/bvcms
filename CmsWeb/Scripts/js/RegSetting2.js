﻿$(function () {
    CKEDITOR.replace('editor', {
        height: 200,
        filebrowserUploadUrl: '/Account/CKEditorUpload/',
        filebrowserImageUploadUrl: '/Account/CKEditorUpload/' 
    });
    $("body").on("click", 'ul.enablesort div.newitem > a', function (ev) {
        if (!$(this).attr("href"))
            return false;
        ev.preventDefault();
        var a = $(this);
        var f = a.closest("form");
        $.post(a.attr("href"), null, function (ret) {
            a.parent().prev().append(ret);
            a.parent().prev().find(".tip").tooltip({ opacity: 0, showBody: "|" });
            $.initDatePicker(f);
        });
    });

    $.regsettingeditclick = function (f) {
        $(".tip", f).tooltip({ opacity: 0, showBody: "|" });
        $("ul.enablesort.sort, ul.enablesort ul.sort", f).sortable();
        $("ul.noedit input", f).attr("disabled", "disabled");
        $("ul.noedit select", f).attr("disabled", "disabled");
        $("ul.noedit a", f).not('[target="otherorg"]').removeAttr("href");
        $("ul.noedit a", f).not('[target="otherorg"]').css("color", "grey");
        $("ul.noedit a", f).not('[target="otherorg"]').unbind("click");
        $('ul.edit a.notifylist').SearchUsers({
            UpdateShared: function(topid) {
                $.post("/Org/UpdateNotifyIds", { id: $("#OrganizationId").val(), topid: topid }, function (ret) {
                    $("a.notifylist").html(ret);
                });
            }
        });
    };
    $.regsettingeditclick();
    $("a.editor").live("click", function (ev) {
        if (!$(this).attr("href"))
            return false;
        var name = $(this).attr("tb");
        ev.preventDefault();
        CKEDITOR.instances['editor'].setData($("#" + name).val());
        dimOn();
        $("#EditorDialog").center().show();
        $("#saveedit").off("click").on("click", function (ev) {
            ev.preventDefault();
            var v = CKEDITOR.instances['editor'].getData();
            $("#" + name).val(v);
            $("#" + name + "_ro").html(v);
            CKEDITOR.instances['editor'].setData('');
            $('#EditorDialog').hide("close");
            dimOff();
            return false;
        });
        return false;
    });
    $("#canceledit").live("click", function (ev) {
        ev.preventDefault();
        $('#EditorDialog').hide("close");
        dimOff();
        return false;
    });
});
CKEDITOR.on('dialogDefinition', function (ev) {
    var dialogName = ev.data.name;
    var dialogDefinition = ev.data.definition;
    if (dialogName == 'link') {
        var advancedTab = dialogDefinition.getContents('advanced');
	advancedTab.label = "SpecialLinks";
        advancedTab.remove('advCSSClasses');
        advancedTab.remove('advCharset');
        advancedTab.remove('advContentType');
        advancedTab.remove('advStyles');
        advancedTab.remove('advAccessKey');
        advancedTab.remove('advName');
        advancedTab.remove('advId');
        advancedTab.remove('advTabIndex');

        var relField = advancedTab.get('advRel');
        relField.label = "SmallGroup";
        var titleField = advancedTab.get('advTitle');
        titleField.label = "Message";
        var idField = advancedTab.get('advLangCode');
        idField.label = "OrgId/MeetingId";
        var langdirField = advancedTab.get('advLangDir');
        langdirField.label = "Confirmation";
	langdirField.items[1][0] = "Yes, send confirmation";
	langdirField.items[2][0] = "No, do not send confirmation";
    }
});