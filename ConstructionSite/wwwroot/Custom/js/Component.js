var id = 0;
$(document).ready(function () {
    
    $("#grid-container").load(`/Portfolio/Project/${id=0}`);
    removeMain();
    fistActive();
    
    $(".All").click(function () {
        removeMain();
        fistActive();
        $("#grid-container").load(`/Portfolio/Project/${id=0}`);
        
    });
    $(".mains").click(function () {
        id = $(this).data("id");
        var name = $(this).data("name");

        unFistActive();
        removeMain();
        mainActive(name);

        $("#grid-container").load(`/Portfolio/Project/${id}`);
        
    });
   
});

function fistActive() {
   
    $("#myfist").removeClass("cbp-filter-item All")
        .addClass("cbp-filter-item All cbp-filter-item-active");
    
}
function unFistActive() {
    $("#myfist")
        .removeClass("cbp-filter-item All cbp-filter-item-active")
        .addClass("cbp-filter-item All");
}
function mainActive(Name) {
    $('#' + Name).removeClass("cbp-filter-item mains");
    $('#' + Name).addClass("cbp-filter-item mains cbp-filter-item-active  ");
}

function removeMain() {
    $(".mains").removeClass("cbp-filter-item mains cbp-filter-item-active  ")
        .addClass("mains cbp-filter-item  mains");
}