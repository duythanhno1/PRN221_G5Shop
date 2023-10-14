    /*!
 * @update 02.10.2020
 * @author Webibazaar Template https://www.webibazaar.com
 * @contact info@webibazaar.com 
 * @license
 * Copyright  2016-2020 Winter Infotech Team
 * Dual licensed under the MIT or GPL Version 2 licenses.
 *
 */

// Quantity Cart Start
var qty = {
    'minus' : function(product_id) {
        var input = $("input[id='input-quantity-" + product_id + "']");
        var currentVal = parseInt(input.val());
        if (!isNaN(currentVal)) {
            var minValue = parseInt(input.attr('min'));
            if (!minValue) minValue = 1;
            if (currentVal > minValue) {
                input.val(currentVal - 1).change();
            }
            if (parseInt(input.val()) == minValue) {
                $(this).attr('disabled', true);
            }
        }
        else {
            input.val(1);
        }
    },
    'plus' : function(product_id) {
        var input = $("input[id='input-quantity-" + product_id + "']");
        var currentVal = parseInt(input.val());
        if (!isNaN(currentVal)) {
            var maxValue = parseInt(input.attr('max'));
            if (!maxValue) maxValue = 999;
            if (currentVal < maxValue) {
                input.val(currentVal + 1).change();
                $('.dis-' + product_id).prop('disabled', false);    
            }
            if (parseInt(input.val()) == maxValue) {
                $(this).attr('disabled', true);
            }
        }
        else {
            input.val(1);
        }
    },
    'cart' : function(product_id) {
        var qty = $('#input-quantity-' + product_id).val();
        if(qty > 0) cart.add(product_id,qty);       
        else {
            //alert($("input[name='hid-qty-msg']").val());
            $('#content').parent().before('<div class="alert alert-danger alert-dismissible"><i class="fa fa-check-circle"></i> ' + $("input[name='hid-qty-msg']").val() + ' <button type="button" class="close" data-dismiss="alert">&times;</button></div>');    
            $('html, body').animate({ scrollTop: 0 }, 'slow');
            $('#input-quantity-' + product_id).val("1");
        }
    }
}
// Quantity Cart End


/* responsive menu */
function openNav() {
    document.getElementById("myOverlay").style.display = "block";
    document.getElementById("stamenu").className = "active menu-fixed";
}

function closeNav() {
    document.getElementById("myOverlay").style.display = "none";
    document.getElementById("stamenu").className = "menu-fixed";
}

 /* loader */
$(window).load(function myFunction() {
  $(".s-panel .loader").removeClass("wrloader");
});

//go to top
$(document).ready(function () {
    $("#common-home").parent().addClass("home-page");
    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('#scroll').fadeIn();
        } else {
            $('#scroll').fadeOut();
        }
    });
    $('#scroll').click(function () {
        $("html, body").animate({scrollTop: 0}, 600);
        return false;
    });
});


$(document).ready(function () {
    if ($(window).width() <= 991) {
        $('.menusp').appendTo('.appmenu');
        $('.curr').appendTo('.lhaccount');
        $('.langg').appendTo('.lhaccount');
    }

    $('.toprightw .owl-carousel.owl-theme .owl-buttons').appendTo('.apponbtn');
      $('.leftpro').appendTo('.onsale_app');
     $('.toppro').appendTo('.latest-app');
     $('.catt-bg').appendTo('.timer');
     

});


function openSearch() {
    $('body').addClass("active-search");
    document.getElementById("search").style.height = "auto";
    $('#search').addClass("sideb");
    // $('.search_query').attr('autofocus', 'autofocus').focus();
}
function closeSearch() {
    $('body').removeClass("active-search");
    document.getElementById("search").style.height = "0";
    $('#search').addClass("siden");
    // $('.search_query').attr('autofocus', 'autofocus').focus();
}


$(document).ready(function () {
$("#ratep,#ratecount").click(function() {
    $('body,html').animate({
        scrollTop: $(".product-tab").offset().top 
    }, 1500);
});
});

/* dropdown effect of account */
$(document).ready(function () {
    if ($(window).width() <= 767) {
    $('.catfilter').appendTo('.appres');

    $('.dropdown a.account').on("click", function(e) {
        $(this).next('ul').toggle();
        e.stopPropagation();
        e.preventDefault();
    });
}
});

$(document).ready(function () {
$('.dropdown button.test').on("click", function(e)  {
    $(this).next('ul').toggle();
    e.stopPropagation();
    e.preventDefault();
});
});



/* dropdown */

/* sticky header */
  if ($(window).width() >= 992) {
 $(document).ready(function(){
      $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('.hsticky').addClass('fixed fadeInDown animated');
        } else {
            $('.hsticky').removeClass('fixed fadeInDown animated');
        }
      });
});
};

$(document).ready(function(){
    if ($(window).width() >= 992) {
var number_blocks =7;
    var count_block = $('#menu .m-menu');
    var moremenu = count_block.slice(number_blocks, count_block.length);
    moremenu.wrapAll('<li class="view_cat_menu tab-menu"><div class="more-menu sub-menu">');
    $('.view_cat_menu').prepend('<a href="#" class="submenu-title">More</a>');
    $('.view_cat_menu').mouseover(function(){
    $(this).children('div').show();
    })
    $('.view_cat_menu').mouseout(function(){
    $(this).children('div').hide();
    });
    };
});


$(document).ready(function(){
        if ($(window).width() >= 992 && $(window).width() <= 1199){
    var number_blocks =9;
    var count_block = $('#menu .m-menu');
    var moremenu = count_block.slice(number_blocks, count_block.length);
    moremenu.wrapAll('<li class="view_cat_menu tab-menu"><div class="more-menu sub-menu">');
    $('.view_cat_menu').prepend('<a href="#" class="submenu-title">More</a>');
    $('.view_cat_menu').mouseover(function(){
    $(this).children('div').show();
    })
    $('.view_cat_menu').mouseout(function(){
    $(this).children('div').hide();
    });
}
});

$(document).ready(function(){
        if ($(window).width() >= 1200 && $(window).width() <= 1409){
    var number_blocks =9;
    var count_block = $('#menu .m-menu');
    var moremenu = count_block.slice(number_blocks, count_block.length);
    moremenu.wrapAll('<li class="view_cat_menu tab-menu"><div class="more-menu sub-menu">');
    $('.view_cat_menu').prepend('<a href="#" class="submenu-title">More</a>');
    $('.view_cat_menu').mouseover(function(){
    $(this).children('div').show();
    })
    $('.view_cat_menu').mouseout(function(){
    $(this).children('div').hide();
    });
}
});

$(document).ready(function(){
    if ($(window).width() >= 1410) {
var number_blocks =5;
    var count_block = $('#menu .m-menu');
    var moremenu = count_block.slice(number_blocks, count_block.length);
    moremenu.wrapAll('<li class="view_cat_menu tab-menu"><div class="more-menu sub-menu">');
    $('.view_cat_menu').prepend('<a href="#" class="submenu-title">More</a>');
    $('.view_cat_menu').mouseover(function(){
    $(this).children('div').show();
    })
    $('.view_cat_menu').mouseout(function(){
    $(this).children('div').hide();
    });
    };
});



$(document).ready(function(){
    $('.modal').on('hide.bs.modal', function() {
      var memory = $(this).html();
      $(this).html(memory);
    })
});

//category tab
  $(document).ready(function() {
  $('.webTab').click(function(){
     $('.webTab').addClass('active');
     $('.webTab').removeClass('active');
  });
});


     $(document).ready(function(){
if ($(document).width() > 767){
     var count_block = $('.catt-bg .webTab').length;
     var number_blocks = 3;
     if(count_block < number_blocks){
          return false; 
     } else {
          $('.catt-bg .webTab').each(function(i,n){
                if(i> number_blocks) {
                    $(this).addClass('it_hide_wbtab');
                }
          })
          $('.it_hide_wbtab').hide();
          $('.catt-bg .wbview_more').click(function() {
                $(this).toggleClass('active');
                $('.it_hide_wbtab').slideToggle();
          });
     }
}
});

     $(document).ready(function() {
  $('.webTab2').click(function(){
     $('.webTab2').addClass('active');
     $('.webTab2').removeClass('active');
  });
});


     $(document).ready(function(){
if ($(document).width() > 767){
     var count_block = $('.tabpro .webTab2').length;
     var number_blocks = 7;
     if(count_block < number_blocks){
          return false; 
     } else {
          $('.tabpro .webTab2').each(function(i,n){
                if(i> number_blocks) {
                    $(this).addClass('it_hide_wbtab2');
                }
          })
          $('.it_hide_wbtab2').hide();
          $('.tabpro .wbview_more2').click(function() {
                $(this).toggleClass('active');
                $('.it_hide_wbtab2').slideToggle();
          });
     }
}
});

/* top categories */
$(document).ready(function() {
 $('.cat-img').slick({
  dots: false,
  arrows: true,
  slidesToShow: 5,
  slidesToScroll: 1,
  autoplay: false,
  autoplaySpeed: 2000,
  rows: 1,
  responsive: [
    {
      breakpoint: 1410,
      settings: {
        slidesToShow: 5
      }
    },
    {
      breakpoint: 1200,
      settings: {
        slidesToShow: 4
      }
    },
    {
      breakpoint: 991,
      settings: {
        slidesToShow: 3
      }
    },
    {
      breakpoint: 768,
      settings: {
        slidesToShow: 3
      }
    },
    {
      breakpoint: 575,
      settings: {
        slidesToShow: 2
      }
    },
    {
      breakpoint: 320,
      settings: {
        slidesToShow: 1
      }
    }
  ]
});

     $('.feature').slick({
  dots: false,
  arrows: true,
  slidesToShow: 5,
  slidesToScroll: 1,
  autoplay: false,
  autoplaySpeed: 2000,
  rows: 2,
  responsive: [
    {
      breakpoint: 1410,
      settings: {
        slidesToShow: 4
      }
    },
    {
      breakpoint: 1200,
      settings: {
        slidesToShow: 3
      }
    },
    {
      breakpoint: 991,
      settings: {
        slidesToShow: 3
      }
    },
    {
      breakpoint: 768,
      settings: {
        slidesToShow: 2
      }
    },
    {
      breakpoint: 575,
      settings: {
        slidesToShow: 2
      }
    },
    {
      breakpoint: 320,
      settings: {
        slidesToShow: 1
      }
    }
  ]
});

     $('.special,.related').slick({
  dots: false,
  arrows: true,
  slidesToShow: 5,
  slidesToScroll: 1,
  autoplay: false,
  autoplaySpeed: 2000,
  rows: 1,
  responsive: [
    {
      breakpoint: 1410,
      settings: {
        slidesToShow: 4
      }
    },
    {
      breakpoint: 1200,
      settings: {
        slidesToShow: 3
      }
    },
    {
      breakpoint: 991,
      settings: {
        slidesToShow: 3
      }
    },
    {
      breakpoint: 768,
      settings: {
        slidesToShow: 2
      }
    },
    {
      breakpoint: 575,
      settings: {
        slidesToShow: 2
      }
    },
    {
      breakpoint: 320,
      settings: {
        slidesToShow: 1
      }
    }
  ]
});

     $('.count').slick({
  dots: false,
  arrows: true,
  slidesToShow: 2,
  slidesToScroll: 1,
  autoplay: false,
  autoplaySpeed: 2000,
  rows: 1,
  responsive: [
    {
      breakpoint: 1410,
      settings: {
        slidesToShow: 2
      }
    },
    {
      breakpoint: 1200,
      settings: {
        slidesToShow: 1
      }
    },
    {
      breakpoint: 991,
      settings: {
        slidesToShow: 1
      }
    },
    {
      breakpoint: 768,
      settings: {
        slidesToShow: 1
      }
    },
    {
      breakpoint: 575,
      settings: {
        slidesToShow: 1
      }
    },
    {
      breakpoint: 320,
      settings: {
        slidesToShow: 1
      }
    }
  ]
});
       $('.blog').slick({
  dots: false,
  arrows: true,
  slidesToShow: 3,
  slidesToScroll: 1,
  autoplay: false,
  autoplaySpeed: 2000,
  rows: 1,
  responsive: [
    {
      breakpoint: 1410,
      settings: {
        slidesToShow: 3
      }
    },
    {
      breakpoint: 1200,
      settings: {
        slidesToShow: 2
      }
    },
    {
      breakpoint: 991,
      settings: {
        slidesToShow: 2
      }
    },
    {
      breakpoint: 768,
      settings: {
        slidesToShow: 2
      }
    },
    {
      breakpoint: 575,
      settings: {
        slidesToShow: 1
      }
    },
    {
      breakpoint: 320,
      settings: {
        slidesToShow: 1
      }
    }
  ]
});


    $('#gallery_01').slick({
  dots: false,
  arrows: false,
  slidesToShow: 4,
  slidesToScroll: 1,
  autoplay: false,
  autoplaySpeed: 2000,
  rows: 1,
  responsive: [
    {
      breakpoint: 1410,
      settings: {
        slidesToShow: 4
      }
    },
    {
      breakpoint: 1200,
      settings: {
        slidesToShow: 4
      }
    },
    {
      breakpoint: 991,
      settings: {
        slidesToShow: 4
      }
    },
    {
      breakpoint: 768,
      settings: {
        slidesToShow: 3
      }
    },
    {
      breakpoint: 575,
      settings: {
        slidesToShow: 3
      }
    },
    {
      breakpoint: 320,
      settings: {
        slidesToShow: 2
      }
    }
  ]
});

});





       $(document).ready(function() {
       $('.tab-content .tab-pane #slider-fore').slick({
  dots: false,
  arrows: false,
  slidesToShow: 3,
  slidesToScroll: 1,
  autoplay: false,
  autoplaySpeed: 2000,
  rows: 3,
  responsive: [
    {
      breakpoint: 1410,
      settings: {
        slidesToShow: 3
      }
    },
    {
      breakpoint: 1200,
      settings: {
        slidesToShow: 2
      }
    },
    {
      breakpoint: 991,
      settings: {
        slidesToShow: 2
      }
    },
    {
      breakpoint: 768,
      settings: {
        slidesToShow: 2
      }
    },
    {
      breakpoint: 575,
      settings: {
        slidesToShow: 1
      }
    },
    {
      breakpoint: 320,
      settings: {
        slidesToShow: 1
      }
    }
  ]
});
    });

     $(document).ready(function() {
       $('.tab-content .tab-pane #slider-fore2').slick({
  dots: false,
  arrows: true,
  slidesToShow: 3,
  slidesToScroll: 1,
  autoplay: false,
  autoplaySpeed: 2000,
  rows: 1,
  responsive: [
    {
      breakpoint: 1410,
      settings: {
        slidesToShow: 3
      }
    },
    {
      breakpoint: 1200,
      settings: {
        slidesToShow: 2
      }
    },
    {
      breakpoint: 991,
      settings: {
        slidesToShow: 2
      }
    },
    {
      breakpoint: 768,
      settings: {
        slidesToShow: 3
      }
    },
    {
      breakpoint: 600,
      settings: {
        slidesToShow: 2
      }
    },
    {
      breakpoint: 320,
      settings: {
        slidesToShow: 1
      }
    }
  ]
});
    });



   

 

