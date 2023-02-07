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


/*    $("#SelectCategory").on("change", function () {
        var url = $(this).val();
        if (url) {
            window.location = "Index1?catId="+url;}
            return false;

        

    });*/




    $("#newcata").click(function (e) {
        var newCatTextInput = $("#newcatname"); /*Класс текстового поля ввода*/
       var temp= $(this).attr('entity')
       var url = "/"+temp+"/Create";
        
        var catName = newCatTextInput.val();

        if (catName == "") { $.get(url); }

        else { e.preventDefault();
                                 
            $.post(url, { name: catName, salary: 0 },function(data) {});
        }
       






    });

     
     








});