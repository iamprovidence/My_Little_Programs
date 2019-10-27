//получаем кнопки табов
var button = document.getElementById("instruction").querySelectorAll("div.tab button");
//получаем контент вкладок, все элементы с class="tabcontent"
var tabcontent = document.getElementsByClassName("tabcontent");

//в цикле присваиваем каждой кнопке, функцию при нажатии
for (var j = 0; j < button.length; ++j)
{
    button[j].onclick = (function (k) 
    {
        //используется замыкания, чтобы отследить какая кнопка была нажата
        return function()
        {
            for (var i = 0; i <  button.length; ++i) 
            {
                //скрыть контент, кроме того, что принадлежит нажатой кнопке
                if(i != k)
                {
                    tabcontent[i].style.display = "none";    
                }
                else
                {
                    tabcontent[i].style.display = "block";  
                }
                //подсвечивать нажать кнопку
                if(i != k)
                {
                    button[i].classList.remove("active");
                }
                else
                {
                    button[i].className = "active";
                }
            }    
        }
    })(j);
}

// по умолчанию открыта первая вкладка
document.getElementById("instruction").querySelector("div.tab button").click();