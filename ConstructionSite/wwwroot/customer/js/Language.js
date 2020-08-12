$(document).ready(function () {
   
    var returnUrl = $(".contexts").data("id");
    $(".datas_item").click(function () {
       
        var id = $(this).data("id");
        $.ajax({
            method: "POST",
            url: "/Language/SetLanguage/" + id,
            success: function () {
               
            window.location.replace(returnUrl);
               
            }


        });
    });

});
