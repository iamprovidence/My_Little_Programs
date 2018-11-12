var slideIndex = 0;
var timeout;

function showSlides(n, time = 5000)// змінювати кожні 5 сек
{
    var slides = document.getElementsByClassName("mySlides");
    var dots = document.getElementsByClassName("dot");
    
    for (var i = 0; i < dots.length; i++) 
    {
        dots[i].className = dots[i].className.replace(" active", "");
    }
    for (var i = 0; i < slides.length; i++) 
    {
        slides[i].style.display = "none"; 
    }
    slideIndex++;
    if (slideIndex > slides.length) 
    {
        slideIndex = 1;
    }
    dots[slideIndex-1].className += " active";
    slides[slideIndex-1].style.display = "block";
    
    clearTimeout(timeout);
    timeout = setTimeout(showSlides, time);
}
 
showSlides(slideIndex);
 
function currentSlide(n) 
{
   showSlides(slideIndex = n-1);
}