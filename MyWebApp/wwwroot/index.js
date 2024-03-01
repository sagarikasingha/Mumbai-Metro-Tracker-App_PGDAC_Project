/* When the user clicks on the button, 
toggle between hiding and showing the dropdown content */
function myFunction() {
  document.getElementById("myDropdown").classList.toggle("show");
}

// Close the dropdown if the user clicks outside of it
window.onclick = function (event) {
  if (!event.target.matches(".dropbtn")) {
    var dropdowns = document.getElementsByClassName("dropdown-content");
    var i;
    for (i = 0; i < dropdowns.length; i++) {
      var openDropdown = dropdowns[i];
      if (openDropdown.classList.contains("show")) {
        openDropdown.classList.remove("show");
      }
    }
  }
};


// Carousel Div Sliding
window.onload = function () {
  let slides = document.getElementsByClassName("carousel-item");

  function addActive(slide) {
    slide.classList.add("active");
  }

  function removeActive(slide) {
    slide.classList.remove("active");
  }

  addActive(slides[0]);
  setInterval(function () {
    for (let i = 0; i < slides.length; i++) {
      if (i + 1 == slides.length) {
        addActive(slides[0]);
        setTimeout(removeActive, 350, slides[i]);
        break;
      }
      if (slides[i].classList.contains("active")) {
        setTimeout(removeActive, 350, slides[i]);
        addActive(slides[i + 1]);
        break;
      }
    }
  }, 1500);
};


// Second Footer

/* Code by CodingTuting.Com */
function footerToggle(footerBtn) {
    $(footerBtn).toggleClass("btnActive");
    $(footerBtn).next().toggleClass("active");
}