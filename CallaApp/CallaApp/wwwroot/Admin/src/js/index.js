$(function () {

    //delete
    $(document).on("click", ".delete-slider", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id };

        $.ajax({
            url: "slider/Delete",
            type: "post",
            data: data,
            success: function (res) {

                $(deletedElem).remove();
                $(".tooltip-inner").remove();
                $(".arrow").remove();
                if ($(tbody).length == 1) {
                    $(".table").remove();
                }
            }
        })
    })

    $(document).on("click", ".delete-banner", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id };

        $.ajax({
            url: "banner/Delete",
            type: "post",
            data: data,
            success: function (res) {

                $(deletedElem).remove();
                $(".tooltip-inner").remove();
                $(".arrow").remove();
                if ($(tbody).length == 1) {
                    $(".table").remove();
                }
            }
        })
    })



    $(document).on("click", ".delete-advertising", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id };

        $.ajax({
            url: "advertising/Delete",
            type: "post",
            data: data,
            success: function (res) {

                $(deletedElem).remove();
                $(".tooltip-inner").remove();
                $(".arrow").remove();
                if ($(tbody).length == 1) {
                    $(".table").remove();
                }
            }
        })
    })


    $(document).on("click", ".delete-decor", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id };

        $.ajax({
            url: "decor/Delete",
            type: "post",
            data: data,
            success: function (res) {

                $(deletedElem).remove();
                $(".tooltip-inner").remove();
                $(".arrow").remove();
                if ($(tbody).length == 1) {
                    $(".table").remove();
                }
            }
        })
    })

    $(document).on("click", ".delete-miniImage", function (e) {
        debugger
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id };

        $.ajax({
            url: "MiniImage/Delete",
            type: "post",
            data: data,
            success: function (res) {
                debugger
                $(deletedElem).remove();
                $(".tooltip-inner").remove();
                $(".arrow").remove();
                if ($(tbody).length == 1) {
                    $(".table").remove();
                }
            }
        })
    })









    //function RemoveImage(url) {
    //    $(document).on("click", ".delete-image", function (e) {
    //        e.preventDefault()
    //        let deleteItem = $(this).parent().parent();
    //        let imageId = $(this).parent().attr("data-id");
    //        let data = { id: imageId }

    //        $.ajax({
    //            url: url,
    //            type: "Post",
    //            data: data,
    //            success: function (res) {
    //                if (res.result) {
    //                    debugger
    //                    $(deleteItem).remove();
    //                    let imagesId = $(".images").children().eq(0).attr("data-id");
    //                    let data = $(".images").children().eq(0);
    //                    let changeElem = $(data).children().eq(1).children().eq(1);

    //                    if (res.id == imagesId) {
    //                        if ($(changeElem).children().hasClass("de-active")) {
    //                            $(changeElem).children().eq(0).addClass("active-status");
    //                            $(changeElem).children().eq(0).removeClass("de-active");
    //                        }
    //                    }
    //                }
    //                else {
    //                    Swal.fire({
    //                        position: 'top-end',
    //                        icon: 'success',
    //                        title: 'Image must be minimum one',
    //                        showConfirmButton: false,
    //                        timer: 1500
    //                    })
    //                }


    //            }
    //        })
    //    })
    //}
    //function SetStatus(url) {
    //    $(document).on("click", ".statuses .image-status", function () {
    //        let imageId = $(this).parent().parent().attr("data-id");
    //        let elems = $(".image-status")
    //        let changeElem = $(this);
    //        let data = { id: imageId }
    //        $.ajax({
    //            url: url,
    //            type: "Post",
    //            data: data,
    //            success: function (res) {
    //                if (res) {
    //                    for (var elem of elems) {
    //                        if ($(elem).hasClass("active-status")) {
    //                            $(elem).removeClass("active-status")
    //                            $(elem).addClass("de-active")
    //                        }
    //                        if ($(changeElem).hasClass("active-status")) {
    //                            Swal.fire({
    //                                position: 'top-end',
    //                                icon: 'success',
    //                                title: 'One picture must be the main',
    //                                showConfirmButton: false,
    //                                timer: 1500
    //                            })
    //                        }
    //                    }
    //                    if ($(changeElem).hasClass("de-active")) {
    //                        $(changeElem).removeClass("de-active");
    //                        $(changeElem).addClass("active-status");
    //                    }
    //                }
    //            }
    //        })
    //    })
    //}
})