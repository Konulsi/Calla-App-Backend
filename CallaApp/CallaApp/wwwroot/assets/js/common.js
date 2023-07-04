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






    function getProductsById(clickedElem, url) {
        $(document).on("click", clickedElem, function (e) {
            e.preventDefault();
            debugger
            let id = $(this).attr("data-id");
            let data = { id: id };
            let parent = $(".product-grid-view")
            $.ajax({
                url: url,
                type: "Get",
                data: data,
                success: function (res) {
                    debugger
                    $(parent).html(res);
                }
            })
        })

    }

    getProducts(".all", "/Shop/GetProducts")

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

    getProductsById(".prod-category", "/Shop/GetProductsByCategory")
    getProductsById(".prod-color", "/Shop/GetProductsByColor")
    getProductsById(".prod-tag", "/Shop/GetProductsByTag")
    getProductsById(".prod-size", "/Shop/GetProductsBySize")
    getProductsById(".prod-brand", "/Shop/GetProductsByBrand")









    //delete product from basket
    $(document).on("click", ".delete-product", function () {

        let id = $(this).parent().parent().attr("data-id");
        let prod = $(this).parent().parent();
        let tbody = $(".tbody").children();
        let data = { id: id };

        $.ajax({
            type: "Post",
            url: `Cart/DeleteDataFromBasket`,
            data: data,
            success: function () {
                if ($(tbody).length == 1) {
                    $(".product-table").addClass("d-none");
                    //$(".footer-alert").removeClass("d-none")
                }
                $(prod).remove();
                //$(".rounded-circle").text(res)
                grandTotal();
            }
        })
        return false;
    })

    //change product count
    $(document).on("click", ".inc", function () {
        let id = $(this).parent().parent().parent().attr("data-id");
        let nativePrice = parseFloat($(this).parent().parent().prev().children().eq(1).text());
        let total = $(this).parent().parent().next().children().eq(1);
        let count = $(this).prev().prev();

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
        let count = $(this).next();

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
        let tbody = $(".tbody").children()
        let sum = 0;
        for (var prod of tbody) {
            let price = parseFloat($(prod).children().eq(5).children().eq(1).text())
            sum += price
        }
        $(".grand-total").text(sum);
    }

    function subTotal(res, nativePrice, total, count) {
        $(count).val(res);
        let subtotal = parseFloat(nativePrice * $(count).val());
        $(total).text(subtotal);
    }

    $(document).on("submit", ".hm-searchbox", function () {
        let value = $(".input-search").val();
        let url = `/Shop/Search?searchText=${value}`;
        window.location.assign(url);
        return false;
    })



})

$(function () {


    //add cart
    addToCart(".add-btn-second", "/Shop/AddToCart");

    function addToCart(clickedElem, url) {
        $(document).on("click", clickedElem, function (e) {
            let id = $(this).attr("data-id");
            console.log(id)
            let data = { id: id };
            let count = (".basket-count");
            $.ajax({
                type: "Post",
                url: url,
                data: data,
                success: function (res) {
                    $(count).text(res);
                }
            })
            return false;
        })
    }
})