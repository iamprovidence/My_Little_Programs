<?php
$URI = explode("=", trim($_SERVER['REQUEST_URI']));
$active = '';

$_GET[href][0] = '/';
$_GET[href][1] = '/cam.php?option=0';
$_GET[href][2] = '/about.php';
$_GET[href][3] = '/feedback.php';

$menu = array('Головна' , 'Камери' , 'Про проект' , 'Контакти');
$name = array('Центральна Площа' , 'Міський Автовокзал 1' , 'Міський Автовокзал 2' , 'Церква Успіння Пресвятої Богодродиці 1','Церква Успіння Пресвятої Богодродиці 2');
?>
<nav class="container">
        <ul>
            <?php
            for($index=0;$index<4;$index++){
            $ACTIVE = explode("=",$_GET[href][$index]);
            $ACTIVE[0] == $URI[0] ? $active = active : $active = '';
            ?>  
            <li><a href="<?=$_GET[href][$index]?>" <?=$active?>><?print_r ($menu[$index]);?></a>

            <?php
            if($index == 1)
            {
                $active2 = "";
            echo '<ul>';
            for($index1=0;$index1<count($name);$index1++)
            {?>  
            <li><a href="cam.php?option=<?=$index1?>"><?php print_r ($name[$index1]);?></a></li>
            <?php
            }
            echo '</ul>';
            }
            echo '</li>';
            };
            ?>
        </ul>
    </nav>
