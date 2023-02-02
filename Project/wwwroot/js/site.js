// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    $("a.delete").click(function () {
        if (!confirm("Confirm Delete")) return false;
    });
    // -----------------------------------------   
    // sorting script

    $("table#pages tbody").sortable({
        items: "tr:not(.home)",
        placeholder: "ui-state-highlight",
        update: function () {
            var ids = $("table#pages tbody").sortable("serialize");





            var url = "";



            $.post(url, id, function (data) { });
        }

    });

});