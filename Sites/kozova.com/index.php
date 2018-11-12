<?php
    $alt = array('Текст 1' , 'Текст 2' , 'Текст 3' , 'Текст 4');
?>
<!DOCTYPE html>
<html lang="uk">
<head>
<?php $title = "Головна";?>
    <?php require "parts/head.php";?>
</head>
<body>
    <?php require "parts/header.php";?>
    <?php require "parts/info.php";?>
    <?php require "parts/nav.php";?>   
    <article class="container">
       <h1>Головна сторінка</h1>
        <div class="slideshow-container">
<?php
for($i=0;$i<count($alt);$i++)
{?>
            <div class="mySlides fade">
                <img src="img/slider/img<?=$i+1?>.jpg" alt="<?php print_r ($alt[$i]);?>">
                <div class="text"><?php print_r ($alt[$i]);?></div>
            </div>
<?php } ?>
        </div>
        <br>
        <div class="text_center">
<?php
for($i=0;$i<count($alt);$i++)
{?>
            <span class="dot" onclick="currentSlide(<?=$i+1?>)"></span> 
<?php } ?>
        </div>
    </article>
    <?php require "parts/footer.php";?>
<script src="js/slider.js"></script>
</body>
</html>