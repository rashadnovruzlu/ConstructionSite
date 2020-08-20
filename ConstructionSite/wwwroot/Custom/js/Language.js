$(document).ready(function () {
   
    var returnUrl = $(".contexts").data("id");
    $(".datas_item").click(function () {
        var id = $(this).data("id");
        alert(id);
        $.ajax({
            method: "POST",
            url: "/Language/SetLanguage/" + id,
            success: function () {

                window.location.replace(returnUrl);

               
               
            }
 });
    });
   
});
//function test () {
//    $(".langss").load(function () {
//        var name = $(this).data("name");
//        removeLang();
//        add(name);
//        });
//};
function add(Name) {
    alert(Name);
    $("#" + Name)
        .removeClass("langss")
        .addClass("langss current-menu-item");
}
function removeLang() {
    $(".langss").removeClass("langss current-menu-item")
        .addClass("langss");
}
