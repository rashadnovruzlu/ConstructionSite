var id = 0;
$(document).ready(function () {
   
  
    $("#grid-container").load(`/Portfolio/Project/${id=0}`);
    $(".All").click(function () {
        $("#grid-container").load(`/Portfolio/Project/${id=0}`);
    });
    $(".mains").click(function () {
        id = $(this).data("id");
        $("#grid-container").load(`/Portfolio/Project/${id}`);
    });
});