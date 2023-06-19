$('#topbtn').click(function () {
    $('html').animate({
        scrollTop: 0
    }, 100)

})



$(window).scroll(function () {
    var header = $('.fixed-nav'),
        scroll = $(window).scrollTop();
    let searchInput =$(".search-input")

    let logoImg = $(".logo img")
            if (scroll >= 1) {
        header.css({
            'position': 'fixed',
            'top': '0',
            'left': '0',
            'right': '0',
            'z-index': '99999',
            'background-color' : 'white',
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
        $(rightIcons).css({'opacity':'0'});
        $(navMenu).css({'opacity':'0'});
        $(social).css({'opacity':'0'});
        $(searchInput).css({'opacity':'1','z-index':'5'});
    })

    $(".close-icon").on("click", function () {
        $(rightIcons).css({'opacity':'1'});
        $(navMenu).css({'opacity':'1'});
        $(social).css({'opacity':'1'});
        $(searchInput).css({'opacity':'0','z-index':'-5'});
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

    





})