$(document).ready(function () {

    const togglePassword = document.querySelector("#register-form .password .eyes");
    const password = document.querySelector("#register-form .password input");

    togglePassword.addEventListener("click", function () {
        // toggle the type attribute
        const type = password.getAttribute("type") === "password" ? "text" : "password";
        password.setAttribute("type", type);

        // toggle the icon
        this.classList.toggle("bi-eye");
    });

    // prevent form submit
    const form = document.querySelector("form");
    form.addEventListener('submit', function (e) {
        e.preventDefault();
    });
})


$(document).ready(function () {
    const togglePassword = document.querySelector("#register-form .comfirm-password .eyes");
    const password = document.querySelector("#register-form .comfirm-password input");

    togglePassword.addEventListener("click", function () {
        // toggle the type attribute
        const type = password.getAttribute("type") === "password" ? "text" : "password";
        password.setAttribute("type", type);

        // toggle the icon
        this.classList.toggle("bi-eye");
    });

    // prevent form submit
    const form = document.querySelector("form");
    form.addEventListener('submit', function (e) {
        e.preventDefault();
    });
})


$(document).ready(function () {
    const togglePassword = document.querySelector("#login-form .password .eyes");
    const password = document.querySelector("#login-form .password input");

    togglePassword.addEventListener("click", function () {
        // toggle the type attribute
        const type = password.getAttribute("type") === "password" ? "text" : "password";
        password.setAttribute("type", type);

        // toggle the icon
        this.classList.toggle("bi-eye");
    });

    // prevent form submit
    const form = document.querySelector("form");
    form.addEventListener('submit', function (e) {
        e.preventDefault();
    });
})




//click etdikde scroll yuxari getsin.
$('#topbtn').click(function () {
    $('html').animate({
        scrollTop: 0
    }, 100)

})



$(window).scroll(function () {
    var header = $('.fixed-nav'),
        scroll = $(window).scrollTop();
    let searchInput = $(".search-input")

    let logoImg = $(".logo img")
    if (scroll >= 1) {
        header.css({
            'position': 'fixed',
            'top': '0',
            'left': '0',
            'right': '0',
            'z-index': '99999',
            'background-color': 'white',
            'box-shadow': 'rgba(149, 157, 165, 0.2) 0px 8px 24px'
            // 'backdrop-filter':'blur(10px)',
            // 'background': 'transparent'
        });
        logoImg.css({
            'margin-top': '0px',
            'width': '14%'
        });
        searchInput.css({
            'top': '78px',
        })
    } else {
        header.css({
            'position': 'relative',
            'box-shadow': 'none'
        });
        logoImg.css({
            'margin-top': '0px',
            'padding-top': '4%',
            'width': '22%'
        })
        searchInput.css({
            'top': '130px',
        })
    }


});





$(document).ready(function () {
    //search
    let searchInput = $(".search-input");
    let rightIcons = $(".right-icons");
    let navMenu = $(".nav-main-menu");
    let social = $(".social-icons");


    $(".search-icon").on("click", function (e) {
        $(rightIcons).css({ 'opacity': '0' });
        $(navMenu).css({ 'opacity': '0' });
        $(social).css({ 'opacity': '0' });
        $(searchInput).css({ 'opacity': '1', 'z-index': '5' });
    })

    $(".close-icon").on("click", function () {
        $(rightIcons).css({ 'opacity': '1' });
        $(navMenu).css({ 'opacity': '1' });
        $(social).css({ 'opacity': '1' });
        $(searchInput).css({ 'opacity': '0', 'z-index': '-5' });
        $(".search-input input").val("");
        $(".not-found").css({ 'display': 'd-none'});

    })





    //hamburger-menu
    let hamburgerIcon = document.querySelector(".hamburger-icon i");
    let hamburgerMenuList = document.querySelector(".hamburger-menu-list .nav-menu")

    hamburgerIcon.addEventListener("click", function () {
        hamburgerMenuList.classList.toggle("close")

    })

    //responsive navbar
    let userIcon = document.querySelector(".right-icons .icons i");
    let logReg = document.querySelector(".right-icons .icons .log-reg")

    userIcon.addEventListener("click", function (e) {
        e.preventDefault();
        logReg.classList.toggle("d-none");
    })


    //responsive search

    $(".searchIcon").on("click", function () {
        $(".right-icons").addClass("d-none");
        $(".search").removeClass("d-none");

    })

    $(".closeIcon").on("click", function () {
        $(".right-icons").removeClass("d-none");
        $(".search").addClass("d-none");

    })


    $(".searchIcon").on("click", function () {
        $(".right-icons .icons .log-reg").addClass("d-none");
    })

    $(".search").on("click", function (e) {
        $(".right-icons .icons .log-reg").addClass("d-none");
    })






    //sechimlere gore datalarin gosterilmesi
    function getProductsById(clickedElem, url) {
        $(document).on("click", clickedElem, function (e) {
            e.preventDefault();
            let id = $(this).attr("data-id");
            let data = { id: id };
            let parent = $(".product-grid-view")
            $.ajax({
                url: url,
                type: "Get",
                data: data,
                success: function (res) {
                    $(parent).html(res);
                }
            })
        })

    }


    getProductsById(".prod-category", "/Shop/GetProductsByCategory")
    getProductsById(".prod-color", "/Shop/GetProductsByColor")
    getProductsById(".prod-tag", "/Shop/GetProductsByTag")
    getProductsById(".prod-size", "/Shop/GetProductsBySize")
    getProductsById(".prod-brand", "/Shop/GetProductsByBrand")



    //butun datalarin gosterilmesi
    function getProducts(clickedElem, url) {
        $(document).on("click", clickedElem, function (e) {
            e.preventDefault();
            let parent = $(".product-grid-view")
            $.ajax({
                url: url,
                type: "Get",
                success: function (res) {
                    $(parent).html(res);
                }
            })
        })

    }
    getProducts(".all-products", "/Shop/GetAllProducts")



    //product detailde main imagede shekillerin gorsenmesi
    $(document).on("click", "#product-info .product-images .prod-img", function () {
        let photo = $(this).children().eq(0).attr("src")
        $(".basic-img").attr("src", photo)

    })





    //change product count 
    $(document).on("click", ".inc", function () {
        let id = $(this).parent().parent().parent().attr("data-id");
        let nativePrice = parseFloat($(this).parent().parent().prev().children().eq(1).text());
        let total = $(this).parent().parent().next().children().eq(1);
        let count = $(this).parent().prev().children().eq(0);

        $.ajax({
            type: "Post",
            url: `Cart/IncrementProductCount?id=${id}`,
            success: function (res) {
                res++;
                subTotal(res, nativePrice, total, count)
                grandTotal();
            }
        })
    })

    $(document).on("click", ".dec", function () {
        let id = $(this).parent().parent().parent().attr("data-id");
        let nativePrice = parseFloat($(this).parent().parent().prev().children().eq(1).text());
        let total = $(this).parent().parent().next().children().eq(1);
        let count = $(this).parent().next().children().eq(0);

        $.ajax({
            type: "Post",
            url: `Cart/DecrementProductCount?id=${id}`,
            success: function (res) {
                if ($(count).val() == 1) {
                    return;
                }
                res--;
                subTotal(res, nativePrice, total, count)
                grandTotal();
            }
        })
    })


    function grandTotal() {
        let tbody = $(".table-body").children()

        let sum = 0;
        for (var prod of tbody) {
            let price = parseFloat($(prod).children().eq(4).children().eq(1).text())
            sum += price
        }
        $(".grand-total").text(sum + ".00");
    }

    function subTotal(res, nativePrice, total, count) {
        $(count).val(res);
        let subtotal = parseFloat(nativePrice * $(count).val());
        $(total).text(subtotal + ".00");
    }


})

$(function () {
    //add cart (product detail)
    $(document).on("click", ".addCart-detail", function (e) {
        let id = $(this).attr("data-id");
        let data = { id: id };
        let count = (".basket-count");
        $.ajax({
            type: "Post",
            url: "/Shop/AddToCart",
            data: data,
            success: function (res) {
                $(count).text(res);
                swal("Added to cart!", "You clicked the button!", "success")
            }
        })
        return false;
    })




    ////change product count-Detail
    //$(document).on("click", ".incrementDetail", function () {
    //    let id = $(this).attr("data-id");
    //    let input = $(this).parent().prev();
    //    let inputValue = $(this).parent().prev().val();
    //    inputValue ++;
    //    $(input).val(inputValue);
    //    $.ajax({
    //        type: "Post",
    //        url: `/Cart/IncrementProductCount?id=${id}`,
    //        success: function (res) {

    //        }
    //    })
    //})
    ////change product count-Detail
    //$(document).on("click", ".decrementDetail", function () {
    //    let id = $(this).attr("data-id");
    //    let input = $(this).parent().prev();
    //    let inputValue = $(this).parent().prev().val();
    //    if (inputValue != 1) {
    //        inputValue--;
    //    }
    //    $(input).val(inputValue);
    //    $.ajax({
    //        type: "Post",
    //        url: `/Cart/DecrementProductCount?id=${id}`,
    //        success: function (res) {
    //            if ($(inputValue).val() == 1) {
    //                return;
    //            }

    //        }
    //    })
    //})

    //add cart

        $(document).on("click", ".addCart", function (e) {
            let id = $(this).attr("data-id");
            let data = { id: id };
            let count = (".basket-count");
            $.ajax({
                type: "Post",
                url: "/Shop/AddToCart",
                data: data,
                success: function (res) {
                    $(count).text(res);
                    swal("Added to cart!", "You clicked the button!", "success")
                }
            })
            return false;
        })

    //delete product from basket
    $(document).on("click", ".delete-cart", function () {
        let id = $(this).parent().parent().attr("data-id");
        let prod = $(this).parent().parent();
        let tbody = $(".table-body").children();
        let data = { id: id };
        swal({
            title: "Are you sure?",
            text: "You will not be able to recover this imaginary file!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, delete it!",
            closeOnConfirm: false
        },
            function () {
                $.ajax({
                    type: "Post",
                    url: `Cart/DeleteDataFromBasket`,
                    data: data,
                    success: function (res) {
                        if ($(tbody).length == 1) {
                            $(".basket-products").addClass("d-none");
                            $(".show-alert").removeClass("d-none")
                        }
                        $(prod).remove();
                        res--;
                        $(".basket-count").text(res)
                        grandTotal();
                        swal("Deleted!", "Your imaginary file has been deleted.", "success");
                    }

                })
            });
     
        return false;
    })

    function grandTotal() {
        let tbody = $(".table-body").children()

        let sum = 0;
        for (var prod of tbody) {
            let price = parseFloat($(prod).children().eq(4).children().eq(1).text())
            sum += price
        }
        $(".grand-total").text(sum + ".00");
    }



})


$(function () {
    //add to wishlist detail
    $(document).on("click", ".add-to-wishlist-detail", function (e) {

        let id = $(this).attr("data-id");
        let data = { id: id };
        let count = (".wishlist-count");
        $.ajax({
            type: "Post",
            url: "/Shop/AddToWishlist",
            data: data,
            success: function (res) {
                $(count).text(res);
                swal("Added to wishlist!", "You clicked the button!", "success")
            }
        })
        return false;
    })

    //add wishlist

        $(document).on("click", ".add-to-wishlist", function (e) {

            let id = $(this).attr("data-id");
            let data = { id: id };
            let count = (".wishlist-count");
            $.ajax({
                type: "Post",
                url: "/Shop/AddToWishlist",
                data: data,
                success: function (res) {
                    $(count).text(res);
                    swal("Added to wishlist!", "You clicked the button!", "success")
                }
            })
            return false;
        })

    //delete product from wishlist
    $(document).on("click", ".delete-wishlist", function () {

        let id = $(this).parent().parent().attr("data-id");

        let product = $(this).parent().parent();
        let tablebody = $(".wishlist-table-body").children();
        let data = { id: id };
        swal({
            title: "Are you sure?",
            text: "You will not be able to recover this imaginary file!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, delete it!",
            closeOnConfirm: false
        },
            function () {
                $.ajax({
                    type: "Post",
                    url: `wishlist/DeleteDataFromWishlist`,
                    data: data,
                    success: function (res) {
                        if ($(tablebody).length == 1) {
                            $(".wishlist-products").addClass("d-none");
                            $(".show-alert").removeClass("d-none")
                        }
                        $(product).remove();
                        res--;
                        $(".wishlist-count").text(res)
                        swal("Deleted!", "Your imaginary file has been deleted.", "success");
                    }
                })
            });

        return false;
    })

})