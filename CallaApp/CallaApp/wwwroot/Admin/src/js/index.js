$(function () {
    //delete 

    RemoveItem(".delete-slider", "/Admin/Slider/Delete");
    RemoveItem(".delete-banner", "/Admin/Banner/Delete");


   


    function RemoveItem(clickedElem, url) {
        debugger
        $(document).on("click", clickedElem, function (e) {
            debugger
            e.preventDefault()
            let deleteElem = $(this).parent().parent();
            let id = $(this).parent().parent().attr("data-id");
            let data = { id: id };
            let tbody = $(deleteElem).parent().children();
            $.ajax({
                url: url,
                type: "Post",
                data: data,
                success: function () {
                    debugger
                    if ($(tbody).length == 1) {
                        $(".table").remove();
                    }
                    for (let item of deleteElem) {
                        if ($(item).attr("data-id") == id) {
                            debugger
                            $(item).remove();
                            $(".tooltip-inner").remove()
                            $(".arrow").remove()
                        }
                    }
                }
            })
        })
    }

})