/*  ---------------------------------------------------
    Template Name: Ogani
    Description:  Ogani eCommerce  HTML Template
    Author: Colorlib
    Author URI: https://colorlib.com
    Version: 1.0
    Created: Colorlib
---------------------------------------------------------  */

'use strict';

(function ($) {

    /*------------------
        Preloader
    --------------------*/
    $(window).on('load', function () {
        $(".loader").fadeOut();
        $("#preloder").delay(200).fadeOut("slow");

        /*------------------
            Gallery filter
        --------------------*/
        $('.featured__controls li').on('click', function () {
            $('.featured__controls li').removeClass('active');
            $(this).addClass('active');
        });
        if ($('.featured__filter').length > 0) {
            var containerEl = document.querySelector('.featured__filter');
            var mixer = mixitup(containerEl);
        }
    });

    /*------------------
        Background Set
    --------------------*/
    $('.set-bg').each(function () {
        var bg = $(this).data('setbg');
        $(this).css('background-image', 'url(' + bg + ')');
    });

    //Humberger Menu
    $(".humberger__open").on('click', function () {
        $(".humberger__menu__wrapper").addClass("show__humberger__menu__wrapper");
        $(".humberger__menu__overlay").addClass("active");
        $("body").addClass("over_hid");
    });

    $(".humberger__menu__overlay").on('click', function () {
        $(".humberger__menu__wrapper").removeClass("show__humberger__menu__wrapper");
        $(".humberger__menu__overlay").removeClass("active");
        $("body").removeClass("over_hid");
    });

    /*------------------
		Navigation
	--------------------*/
    $(".mobile-menu").slicknav({
        prependTo: '#mobile-menu-wrap',
        allowParentLinks: true
    });

    /*-----------------------
        Categories Slider
    ------------------------*/
    $(".categories__slider").owlCarousel({
        loop: true,
        margin: 0,
        items: 4,
        dots: false,
        nav: true,
        navText: ["<span class='fa fa-angle-left'><span/>", "<span class='fa fa-angle-right'><span/>"],
        animateOut: 'fadeOut',
        animateIn: 'fadeIn',
        smartSpeed: 1200,
        autoHeight: false,
        autoplay: true,
        responsive: {

            0: {
                items: 1,
            },

            480: {
                items: 2,
            },

            768: {
                items: 3,
            },

            992: {
                items: 4,
            }
        }
    });


    $('.hero__categories__all').on('click', function () {
        $('.hero__categories ul').slideToggle(400);
    });

    /*--------------------------
        Latest Product Slider
    ----------------------------*/
    $(".latest-product__slider").owlCarousel({
        loop: true,
        margin: 0,
        items: 1,
        dots: false,
        nav: true,
        navText: ["<span class='fa fa-angle-left'><span/>", "<span class='fa fa-angle-right'><span/>"],
        smartSpeed: 1200,
        autoHeight: false,
        autoplay: true
    });

    /*-----------------------------
        Product Discount Slider
    -------------------------------*/
    $(".product__discount__slider").owlCarousel({
        loop: true,
        margin: 0,
        items: 3,
        dots: true,
        smartSpeed: 1200,
        autoHeight: false,
        autoplay: true,
        responsive: {

            320: {
                items: 1,
            },

            480: {
                items: 2,
            },

            768: {
                items: 2,
            },

            992: {
                items: 3,
            }
        }
    });
    /*****************************/
    /*Check Session---------------*/
    /*****************************/
    /*Login--------------------------------*/
    $("#submit_login").click(function () {
        var user = $("#user").val();
        var password = $("#password").val();
        if (user.length < 8 || password.length < 8) {
            alert("Vui lòng nhập tài khoản hoặc mất khẩu hợp lệ");
        } else {
            var form = new FormData;
            form.append("user", user);
            form.append("password", password);
            var xhr = new XMLHttpRequest();
            xhr.open("POST", "/Home/ActionLogin", true);
            xhr.onreadystatechange = function () {
                if (xhr.status == 200 && xhr.readyState == 4) {
                    var js = JSON.parse(xhr.responseText);
                    if (js.Data.status == "OK") {
                        location.reload();
                    } else {
                        alert("Tài khoản mất khẩu không đúng");
                    }
                }
            }
            xhr.send(form);
        }
    });



    /*---------------------------------
        Product Details Pic Slider
    ----------------------------------*/
    $(".product__details__pic__slider").owlCarousel({
        loop: true,
        margin: 20,
        items: 4,
        dots: true,
        smartSpeed: 1200,
        autoHeight: false,
        autoplay: true
    });
    /*TextSearch*/
    $("#textsearch").keyup(function () {
        var form = new FormData();
        var min = $("#minamount").val().substr(0, $("#minamount").val().length - 5);
        var max = $("#maxamount").val().substr(0, $("#minamount").val().length - 4);
        form.append("min", min);
        form.append("max", max);
        form.append("text", $("#textsearch").val());
        var xhr = new XMLHttpRequest();
        xhr.open("POST", "/Home/ProductSearch", true);
        xhr.onreadystatechange = function () {
            if (xhr.status == 200 && xhr.readyState == 4) {
                var js = JSON.parse(xhr.responseText);
                var trang = "";
                var sotrang = "";
                if (js.Data.status == "OK") {
                    for (var i = 1; i <= Math.ceil((js.Data.product).length / 9); i++) {
                        var tam = "";
                        var dem = 0;
                        (js.Data.product).forEach(item => {
                            if (dem >= i * 9 - 9 && dem < i * 9) {
                                if (js.Data.love[dem] == "not") {
                                    tam += "<div class='col-lg-4 col-md-6 col-sm-6'><div class='product__item'><div class='product__item__pic set-bg' data-setbg='/Content/img/product/" + item.Image + "' style='background-image: url(&quot;/Content/img/product/" + item.Image + "&quot;);'><ul class='product__item__pic__hover'><li><a href='javascript: love(`" + item.IDSP + "`)'><i class='fa fa-heart'></i></a></li><li><a href='javascript: addcart(`" + item.IDSP + "`)'><i class='fa fa-shopping-cart'></i></a></li></ul></div><div class='product__item__text'><h6><a href='/Home/DetailProduct/" + item.IDSP + "'>" + item.Name + "</a></h6><h5>" + (item.Cost - (item.Cost * (item.IDKM - 1) * 0.1)) + " VNĐ</h5></div></div></div>";
                                } else if (js.Data.love[dem] == "yes") {
                                    tam += "<div class='col-lg-4 col-md-6 col-sm-6'><div class='product__item'><div class='product__item__pic set-bg' data-setbg='/Content/img/product/" + item.Image + "' style='background-image: url(&quot;/Content/img/product/" + item.Image + "&quot;);'><ul class='product__item__pic__hover'><li><a style='color:red' href='javascript: love(`" + item.IDSP + "`)'><i class='fa fa-heart'></i></a></li><li><a style='color:red' href='javascript: addcart(`" + item.IDSP + "`)'><i class='fa fa-shopping-cart'></i></a></li></ul></div><div class='product__item__text'><h6><a href='/Home/DetailProduct/" + item.IDSP + "'>" + item.Name + "</a></h6><h5>" + (item.Cost - (item.Cost * (item.IDKM - 1) * 0.1)) + " VNĐ</h5></div></div></div>";
                                } else {
                                    tam += "<div class='col-lg-4 col-md-6 col-sm-6'><div class='product__item'><div class='product__item__pic set-bg' data-setbg='/Content/img/product/" + item.Image + "' style='background-image: url(&quot;/Content/img/product/" + item.Image + "&quot;);'><ul class='product__item__pic__hover'><li><a href='/Home/Login'><i class='fa fa-heart'></i></a></li><li><a href='javascript: addcart(`" + item.IDSP + "`)'><i class='fa fa-shopping-cart'></i></a></li></ul></div><div class='product__item__text'><h6><a href='/Home/DetailProduct/" + item.IDSP + "'>" + item.Name + "</a></h6><h5>" + (item.Cost - (item.Cost * (item.IDKM - 1) * 0.1)) + " VNĐ</h5></div></div></div>";
                                }
                                
                            }
                            dem++;
                        });
                        if (i == 1) {
                            trang += "<div class='row tab-pane active' id='tabs-search-" + i + "' role='tabpanel'>" + tam + "<p align='center' class='col-lg-12 col-sm-12 col-md-12'>Trang hiện tại: " + i + "</p></div>";
                            sotrang += "<li class='nav-item active'><a class='nav-link active' data-toggle='tab' href='#tabs-search-" + i + "' role='tab' aria-selected='true'>" + i + "</a></li>";
                        } else {
                            trang += "<div class='row tab-pane' id='tabs-search-" + i + "' role='tabpanel'>" + tam + "<p align='center' class='col-lg-12 col-sm-12 col-md-12'>Trang hiện tại: " + i + "</p></div>";
                            sotrang += "<li class='nav-item'><a class='nav-link' data-toggle='tab' href='#tabs-search-" + i + "' role='tab' aria-selected='true'>" + i + "</a></li>";
                        }
                    }
                    $("#foundsearch").html("<h6><span>" + (js.Data.product).length + "</span> Products found</h6>");
                    $("#productsearch").html(trang);
                    $("#trangsearch").html(sotrang);
                }
            }
        }
        xhr.send(form);
    });

    /*--------------------*/
    /*-----------------------
		Price Range Slider
	------------------------ */
    var rangeSlider = $(".price-range"),
        minamount = $("#minamount"),
        maxamount = $("#maxamount"),
        minPrice = rangeSlider.data('min'),
        maxPrice = rangeSlider.data('max');
    rangeSlider.slider({
        range: true,
        min: minPrice,
        max: maxPrice,
        values: [minPrice, maxPrice],
        slide: function (event, ui) {
            minamount.val(ui.values[0] + "K VNĐ");
            maxamount.val(ui.values[1] + "K VNĐ");
            var form = new FormData();
            form.append("min", ui.values[0]);
            form.append("max", ui.values[1]);
            form.append("text", $("#textsearch").val());
            var xhr = new XMLHttpRequest();
            xhr.open("POST", "/Home/ProductSearch", true);
            xhr.onreadystatechange = function () {
                if (xhr.status == 200 && xhr.readyState == 4) {
                    var js = JSON.parse(xhr.responseText);
                    var trang = "";
                    var sotrang = "";
                    if (js.Data.status == "OK") {
                        for (var i = 1; i <= Math.ceil((js.Data.product).length / 9); i++) {
                            var tam = "";
                            var dem = 0;
                            (js.Data.product).forEach(item => {
                                if (js.Data.love[dem] == "not") {
                                    tam += "<div class='col-lg-4 col-md-6 col-sm-6'><div class='product__item'><div class='product__item__pic set-bg' data-setbg='/Content/img/product/" + item.Image + "' style='background-image: url(&quot;/Content/img/product/" + item.Image + "&quot;);'><ul class='product__item__pic__hover'><li><a href='javascript: love(`" + item.IDSP + "`)'><i class='fa fa-heart'></i></a></li><li><a href='javascript: addcart(`" + item.IDSP + "`)'><i class='fa fa-shopping-cart'></i></a></li></ul></div><div class='product__item__text'><h6><a href='/Home/DetailProduct/" + item.IDSP + "'>" + item.Name + "</a></h6><h5>" + (item.Cost - (item.Cost * (item.IDKM - 1) * 0.1)) + " VNĐ</h5></div></div></div>";
                                } else if (js.Data.love[dem] == "yes") {
                                    tam += "<div class='col-lg-4 col-md-6 col-sm-6'><div class='product__item'><div class='product__item__pic set-bg' data-setbg='/Content/img/product/" + item.Image + "' style='background-image: url(&quot;/Content/img/product/" + item.Image + "&quot;);'><ul class='product__item__pic__hover'><li><a style='color:red' href='javascript: love(`" + item.IDSP + "`)'><i class='fa fa-heart'></i></a></li><li><a style='color:red' href='javascript: addcart(`" + item.IDSP + "`)'><i class='fa fa-shopping-cart'></i></a></li></ul></div><div class='product__item__text'><h6><a href='/Home/DetailProduct/" + item.IDSP + "'>" + item.Name + "</a></h6><h5>" + (item.Cost - (item.Cost * (item.IDKM - 1) * 0.1)) + " VNĐ</h5></div></div></div>";
                                } else {
                                    tam += "<div class='col-lg-4 col-md-6 col-sm-6'><div class='product__item'><div class='product__item__pic set-bg' data-setbg='/Content/img/product/" + item.Image + "' style='background-image: url(&quot;/Content/img/product/" + item.Image + "&quot;);'><ul class='product__item__pic__hover'><li><a href='/Home/Login'><i class='fa fa-heart'></i></a></li><li><a href='javascript: addcart(`" + item.IDSP + "`)'><i class='fa fa-shopping-cart'></i></a></li></ul></div><div class='product__item__text'><h6><a href='/Home/DetailProduct/" + item.IDSP + "'>" + item.Name + "</a></h6><h5>" + (item.Cost - (item.Cost * (item.IDKM - 1) * 0.1)) + " VNĐ</h5></div></div></div>";
                                }
                                dem++;
                            });
                            if (i == 1) {
                                trang += "<div class='row tab-pane active' id='tabs-search-" + i + "' role='tabpanel'>" + tam + "<p align='center' class='col-lg-12 col-sm-12 col-md-12'>Trang hiện tại: " + i + "</p></div>";
                                sotrang += "<li class='nav-item active'><a class='nav-link active' data-toggle='tab' href='#tabs-search-" + i + "' role='tab' aria-selected='true'>" + i + "</a></li>";
                            } else {
                                trang += "<div class='row tab-pane' id='tabs-search-" + i + "' role='tabpanel'>" + tam + "<p align='center' class='col-lg-12 col-sm-12 col-md-12'>Trang hiện tại: " + i + "</p></div>";
                                sotrang += "<li class='nav-item'><a class='nav-link' data-toggle='tab' href='#tabs-search-" + i + "' role='tab' aria-selected='true'>" + i + "</a></li>";
                            }
                        }
                        $("#foundsearch").html("<h6><span>" + (js.Data.product).length + "</span> Products found</h6>");
                        $("#productsearch").html(trang);
                        $("#trangsearch").html(sotrang);
                    }
                }
            }
            xhr.send(form);
        }
    });
    minamount.val(rangeSlider.slider("values", 0) + "K VNĐ");
    maxamount.val(rangeSlider.slider("values", 1) + "K VNĐ");

    /*--------------------------
        Select
    ----------------------------*/
    $("select").niceSelect();

    /*------------------
		Single Product
	--------------------*/
    $('.product__details__pic__slider img').on('click', function () {

        var imgurl = $(this).data('imgbigurl');
        var bigImg = $('.product__details__pic__item--large').attr('src');
        if (imgurl != bigImg) {
            $('.product__details__pic__item--large').attr({
                src: imgurl
            });
        }
    });

    /*-------------------
		Quantity change
	--------------------- */
    var proQty = $('.pro-qty');
    proQty.prepend('<span class="dec qtybtn">-</span>');
    proQty.append('<span class="inc qtybtn">+</span>');
    proQty.on('click', '.qtybtn', function () {
        var $button = $(this);
        var oldValue = $button.parent().find('input').val();
        if ($button.hasClass('inc')) {
            var newVal = parseFloat(oldValue) + 1;
        } else {
            // Don't allow decrementing below zero
            if (oldValue > 0) {
                var newVal = parseFloat(oldValue) - 1;
            } else {
                newVal = 0;
            }
        }
        $button.parent().find('input').val(newVal);
    });

})(jQuery);