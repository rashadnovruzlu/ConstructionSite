var name='';
$(document).ready(function () {
    var returnUrl = $(".contexts").data("id");
    $(".datas_item").click(function () {
        var id = $(this).data("id");
        DeActive();

        Active(id);
       
      
        $.ajax({
            method: "POST",
            url: "/Language/SetLanguage/" + id,
            success: function () {
               // mainActive(id);
               window.location.replace(returnUrl);

             }
        });
       
    });
   
});
function DeActive() {
    $(".datas_item").css("color","white");
}
function Active(name) {
    $("#" + name).css("color", "red");
}
