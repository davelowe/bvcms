$(document).ready(function(){$.getTable=function(){var n=$("#form"),t=n.serialize();return $.post(n.attr("action"),t,function(n){$("#Checkins > tbody").html(n).ready(function(){$.formatTable(),$("span.membercount").text($("#membercount").val()),$("span.guestcount").text($("#guestcount").val())})}),!1},$("#filter").click(function(n){n.preventDefault(),$.getTable()}),$("#clear").click(function(n){n.preventDefault(),$("input:text").val(""),$("input:checkbox").removeAttr("checked"),$("select").val(0),$.getTable()}),$.formatTable=function(){$("table.grid > tbody > tr:even").addClass("alt")},$.formatTable(),$(".bt").button()})