<?php
    if(isset($_POST["done"]))
    {
        $name = test_input($_POST["name"]);
        $to = "greedisgood@gmail.com";
        $from = test_input($_POST["email"]);
        $subject = "Повідомлення від сайту OnlineKozova";
        $message = " Повідомлення від — ".$name.". Вміст повідомлення — ".test_input($_POST["message"]);
                
        if(empty($name))
        {
            $error = true;
        }
        if(empty($from) || !preg_match("/@/",$from))
        {
            $error = true;
        }
        if(empty($message))
        {
            $error = true;
        }
        
        if(!$error)
        {
            $subject = "=?utf-8?B?".base64_encode($subject)."?=";
            $headers = "From: $from\r\nReply-to: $from\r\nContent-type: text/plain; charset: utf-8\r\n";
            mail($to, $subject,$message,$headers);
            $error_message = "Повідомлення відправлено";
        }
        else
        {
            $error_message = "Відбулась помилка";
        }
    }

    function test_input($data)
    {
        $data = trim($data);
        $data = stripslashes($data);
        $data = htmlspecialchars($data);
        return $data;
    }
?>
<!DOCTYPE html>
<html lang="uk">
<head>
<?php $title = "Зворотній зв'язок";?>
    <?php require "parts/head.php";?>
</head>
<body>
    <?php require "parts/header.php";?>
    <?php require "parts/info.php";?>
    <?php require "parts/nav.php";?>
    <article class="container form">
    <h2>Форма зворотнього зв'язку</h2>
        <p>Щоб відправити нам своє повідомлення, заповніть форму нижче</p>
        <form autocomplete="on" method="post" action="">
            <input type="text" id="name" name="name" placeholder="Введіть ваше ім'я" required>
            <input type="email" id="email" name="email" placeholder="Введіть ваш email" required>
            <textarea id="message" name="message" placeholder="Введіть ваше повідомлення" required></textarea>
            <input type="submit" class="btn" id="send" name="done" value="Відправити"/>
            <span class="send_message"><?=$error_message?></span>
        </form>    
    </article>
    <?php require "parts/footer.php";?>
</body>
</html>