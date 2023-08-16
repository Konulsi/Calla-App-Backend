$(function () {

    //delete

    $(document).on("click", ".delete-blog", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id };

        $.ajax({
            url: "blog/Delete",
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


    $(document).on("click", ".delete-blogComment", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id };

        $.ajax({
            url: "blogComment/Delete",
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
    $(document).on("click", ".delete-contact", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id };

        $.ajax({
            url: "contact/Delete",
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

    $(document).on("click", ".delete-subscribe", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id };

        $.ajax({
            url: "subscribe/Delete",
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

    $(document).on("click", ".delete-team", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id };

        $.ajax({
            url: "team/Delete",
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


    $(document).on("click", ".delete-about", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id };

        $.ajax({
            url: "about/Delete",
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
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id };

        $.ajax({
            url: "MiniImage/Delete",
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


    $(document).on("click", ".delete-tag", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id };

        $.ajax({
            url: "Tag/Delete",
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

    $(document).on("click", ".delete-size", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id };

        $.ajax({
            url: "Size/Delete",
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

    $(document).on("click", ".delete-color", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id };

        $.ajax({
            url: "Color/Delete",
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


    $(document).on("click", ".delete-brand", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id };

        $.ajax({
            url: "Brand/Delete",
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


    $(document).on("click", ".delete-category", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id };

        $.ajax({
            url: "Category/Delete",
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


    $(document).on("click", ".delete-author", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id };

        $.ajax({
            url: "Author/Delete",
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

    $(document).on("click", ".delete-product", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id };

        $.ajax({
            url: "Product/Delete",
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

    $(document).on("click", ".delete-productComment", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id };

        $.ajax({
            url: "productComment/Delete",
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

    $(document).on("click", ".delete-user", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id };

        $.ajax({
            url: "user/Delete",
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

    SetStatus("/Admin/Blog/SetStatus");
    SetStatus("/Admin/Product/SetStatus");

    RemoveImage("/Admin/Blog/DeleteBlogImage");
    RemoveImage("/Admin/Product/DeleteProductImage");


    function RemoveItem(clickedElem, url) {
        $(document).on("click", clickedElem, function (e) {
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
                    if ($(tbody).length == 1) {
                        $(".table").remove();
                        (".paginate-area").remove();
                    }
                    for (let item of deleteElem) {
                        if ($(item).attr("data-id") == id) {
                            $(item).remove();
                            $(".tooltip-inner").remove()
                            $(".arrow").remove()
                        }
                    }
                }
            })
        })
    }

    function RemoveImage(url) {
        $(document).on("click", ".delete-image", function (e) {
            e.preventDefault()
            let deleteItem = $(this).parent().parent();
            let imageId = $(this).parent().attr("data-id");
            let data = { id: imageId }

            $.ajax({
                url: url,
                type: "Post",
                data: data,
                success: function (res) {
                    if (res.result) {
                        $(deleteItem).remove();
                        let imagesId = $(".images").children().eq(0).attr("data-id");
                        let data = $(".images").children().eq(0);
                        let changeElem = $(data).children().eq(1).children().eq(1);

                        if (res.id == imagesId) {
                            if ($(changeElem).children().hasClass("de-active")) {
                                $(changeElem).children().eq(0).addClass("active-status");
                                $(changeElem).children().eq(0).removeClass("de-active");
                            }
                        }
                    }
                    else {
                        Swal.fire({
                            position: 'top-end',
                            icon: 'success',
                            title: 'Image must be minimum one',
                            showConfirmButton: false,
                            timer: 1500
                        })
                    }


                }
            })
        })
    }
    function SetStatus(url) {
        $(document).on("click", ".statuses .image-status", function () {
            let imageId = $(this).parent().parent().attr("data-id");
            let elems = $(".image-status")
            let changeElem = $(this);
            let data = { id: imageId }
            $.ajax({
                url: url,
                type: "Post",
                data: data,
                success: function (res) {
                    if (res) {
                        for (var elem of elems) {
                            if ($(elem).hasClass("active-status")) {
                                $(elem).removeClass("active-status")
                                $(elem).addClass("de-active")
                            }
                            if ($(changeElem).hasClass("active-status")) {
                                Swal.fire({
                                    position: 'top-end',
                                    icon: 'success',
                                    title: 'One picture must be the main',
                                    showConfirmButton: false,
                                    timer: 1500
                                })
                            }
                        }
                        if ($(changeElem).hasClass("de-active")) {
                            $(changeElem).removeClass("de-active");
                            $(changeElem).addClass("active-status");
                        }
                    }
                }
            })
        })
    }
})