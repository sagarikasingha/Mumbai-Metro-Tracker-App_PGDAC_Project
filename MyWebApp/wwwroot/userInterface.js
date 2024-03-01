/*Designed by CodingTuting.Com*/
$(document).ready(function () {
  $(window).scroll(function () {
    if ($(this).scrollTop() >= 500) {
      $("#gotop").fadeIn();

      $("#top-btn").click(function () {
        $(window).scrollTop(0);
      });
    } else {
      $("#gotop").fadeOut();
    }
  });
});

function menuBtnFunction(menuBtn) {
  menuBtn.classList.toggle("active");
  $(".mainMenu").toggleClass("active");
  $(".mainMenu a").toggleClass("active");
}
