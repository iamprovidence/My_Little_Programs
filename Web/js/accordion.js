/*
    Внимание!
В определенный момент времени может быть открыта только одна вкладка аккордеона
*/

//получаем кнопки аккордеона
var acc = document.getElementsByClassName("accordion");

//каждая кнопка при нажатии на нее выполняет функцию
for (var i = 0; i < acc.length; ++i)
{   
    acc[i].onclick = function() 
    {
        for(var j = 0; j < acc.length; ++j)
        {
            //переключать класс active
            if(acc[j] == this)
            {
                acc[j].classList.toggle("active");//в нажатой кнопке включить
            }
            else
            {
                acc[j].classList.remove("active");//в остальных выключить
            }
            
            // показывать/скрывать элемент активной панели
            if (acc[j].classList.contains('active'))
            {
                acc[j].nextElementSibling.style.maxHeight = acc[j].nextElementSibling.scrollHeight + "px";
                acc[j].nextElementSibling.style.paddingTop = acc[j].nextElementSibling.style.paddingBottom = 5 +"px"; 
            } 
            else
            {
                acc[j].nextElementSibling.style.maxHeight = null;
                acc[j].nextElementSibling.style.paddingTop = acc[j].nextElementSibling.style.paddingBottom = null; 
            } 
        }
    }                    
}
