<?php
$address = array (
    "https://open.ivideon.com/embed/v2/?server=100-38314619cca842964ef2502a16753b0b&amp;camera=131072&amp;width=&amp;height=&amp;lang=ru",
    "https://open.ivideon.com/embed/v2/?server=100-38314619cca842964ef2502a16753b0b&amp;camera=196608&amp;width=&amp;height=&amp;lang=ru",
    "https://open.ivideon.com/embed/v2/?server=100-38314619cca842964ef2502a16753b0b&amp;camera=262144&amp;width=&amp;height=&amp;lang=ru",
    "https://open.ivideon.com/embed/v2/?server=100-9165c6890b4c8b29dbe74fa1feaedb6b&amp;camera=65536&amp;width=&amp;height=&amp;lang=ru",
    "https://open.ivideon.com/embed/v2/?server=100-9165c6890b4c8b29dbe74fa1feaedb6b&amp;camera=0&amp;width=&amp;height=&amp;lang=ru"
)
?>
<!DOCTYPE html>
<html lang="uk">
<head>
<?php $title = "Камери";?>
    <?php require "parts/head.php";?>

</head>
<body>
    <?php require "parts/header.php";?>
    <?php require "parts/info.php";?>
    <?php require "parts/nav.php";?>
    <article class="container">
        <h1><?php print_r($name[$_GET['option']])?></h1>
        <div class="aspect-ratio">
            <iframe src="<?php print_r($address[$_GET['option']])?>"  allowfullscreen></iframe>
        </div>      
    </article> 
    <?php require "parts/footer.php";?>
</body>
</html>