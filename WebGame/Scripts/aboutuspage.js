jQuery(document).ready(function () {

  jQuery('#our-story-wrap').slick({
    centerMode: true,
    centerPadding: '0',
    slidesToShow: 3,
    slidesToScroll: 1,
    swipeToSlide: true,
    touchThreshold: 20,
    arrows: true,
    infinite: false,
    responsive: [{
      breakpoint: 640,
      settings: {
        centerMode: false,
        slidesToShow: 1
      }
    }]
  });


  // On before slide change
  jQuery('#our-story-wrap').on('beforeChange', function (event, slick, currentSlide, nextSlide) {

    var slideto = nextSlide + 1;
    var sliderow = document.getElementById('our-story-row');

    switch (slideto) {
      case 1:
        sliderow.style.backgroundColor = "#e3a927";
        break;
      case 2:
        sliderow.style.backgroundColor = "#3db5a8";
        break;
      case 3:
        sliderow.style.backgroundColor = "#f0658b";
        break;
      case 4:
        sliderow.style.backgroundColor = "#eb3945";
        break;
      case 5:
        sliderow.style.backgroundColor = "#222222";
        break;
      case 6:
        sliderow.style.backgroundColor = "#e7266a";
        break;
      case 7:
        sliderow.style.backgroundColor = "#773897";
        break;
      case 8:
        sliderow.style.backgroundColor = "#144da1";
        break;
      default:
        sliderow.style.backgroundColor = "#e3a927";
    }

  });

});
