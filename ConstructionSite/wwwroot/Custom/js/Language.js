$(document).ready(function () {
   
    var returnUrl = $(".contexts").data("id");
    $(".datas_item").click(function () {
        var id = $(this).data("id");
       
        $.ajax({
            method: "POST",
            url: "/Language/SetLanguage/" + id,
            success: function () {

                window.location.replace(returnUrl);
                document.cookie = `name=${id}`;
                test(document.cookie);

               
               
            }
 });
    });
   
});
function test(id) {


   
    removeLang(id);
    add(id);
}

function add(Name) {
    alert(Name);
    $("#" + Name)
        .removeClass(Name)
        .addClass("current-menu-item");
}
function removeLang(Name) {
    $("." + Name).removeClass("langss current-menu-item")
        .addClass(Name);
}
function setCookie(name,value) {
   
    document.cookie = cname + "=" + cvalue;
}
