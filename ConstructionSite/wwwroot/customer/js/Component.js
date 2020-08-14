var id = 0;

        $(document).ready(function () {

        $("#grid-container").load(`/Portfolio/Project/${id}`);
            $("#myfist").removeClass("cbp-filter-item  ");
            $("#myfist").addClass("cbp-filter-item   cbp-filter-item-active");
            $(".All").click(function () {
        $("#myfist").removeClass("cbp-filter-item");
                $("#myfist").addClass("cbp-filter-item cbp-filter-item-active");
                $("#grid-container").load(`/Portfolio/Project/${id}`);
            });
            $(".mains").click(function () {
        id = $(this).data("id");

                $("#myfist").removeClass("cbp-filter-item cbp-filter-item-active");
                $("#myfist").addClass("cbp-filter-item");

                $("#grid-container").load(`/Portfolio/Project/${id}`);
            });

        });
//$(document).ready(function () {
   
  
//    $("#grid-container").load(`/Portfolio/Project/${id=0}`);
//    $(".All").click(function () {
//        $("#grid-container").load(`/Portfolio/Project/${id=0}`);
//    });
//    $(".mains").click(function () {
//        id = $(this).data("id");
//        $("#grid-container").load(`/Portfolio/Project/${id}`);
//    });
//});


        //$(document).ready(function () {
        //    var id = 0;
        //    $("#grid-container").load(`/Portfolio/Project/${id}`);
        //    $("#myfist").removeClass("cbp-filter-item");
        //    $("#myfist").addClass("cbp-filter-item cbp-filter-item-active");
        //    $(".All").click(function () {
        //    $("#myfist").removeClass("cbp-filter-item");
        //        $("#myfist").addClass("cbp-filter-item cbp-filter-item-active");
        //        $("#grid-container").load(`/Portfolio/Project/${id}`);
        //    });
        //    $(".mains").click(function () {
        //    id = $(this).data("id");

        //        $("#myfist").removeClass("cbp-filter-item cbp-filter-item-active");
        //        $("#myfist").addClass("cbp-filter-item");

        //        $("#grid-container").load(`/Portfolio/Project/${id}`);
        //    });

        //});
