$(document).ready(function () {
    var id = 0;
    $("#grid-container").load(`/Portfolio/Project/${id}`);
    $(".All").click(function () {
       

        $("#grid-container").load(`/Portfolio/Project/${id}`);
    });
    $(".mains").click(function () {
        id = $(this).data("id");
        $("#grid-container").load(`/Portfolio/Project/${id}`);
    });




});