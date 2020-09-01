$(document).ready(function () {
    var returnUrl = $(".contexts").data("id");
    $(".datas_item").click(function () {
        var id = $(this).data("id");
        //localStorage.clear();
        //localStorage.setItem('name',id);
        //DeActive();

       
      
        $.ajax({
            method: "POST",
            url: "/Language/SetLanguage/" + id,
            success: function () {

                window.location.replace(returnUrl);
                //$(window).on('load', function () {
                //    Active();
                //});

             }
        });

       
    });
   
});
function DeActive() {
    $(".datas_item").css("color","white");
}
function Active() {
    var name = localStorage.getItem('name');
    $("#" + name).css("color", "red");
}
