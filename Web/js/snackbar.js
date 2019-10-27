// Показать панель
function show_snackbar(message, color = "#333") 
{
    // Получаем DIV панели
    var snackbar = document.getElementById("snackbar")
    // Скрыть панель, если пользователь нажал на нее
    snackbar.onclick = function()
    {
        hide_snackber(snackbar)
    };
    // Предоставить панели передан цвет и вписать текст
    snackbar.style.backgroundColor = color;
    snackbar.textContent = message;
    // Добавить класс "show" к DIV-панели
    snackbar.className = "show";
    
    // После 3 секунд удалить "show" класс у DIV-панели
    setTimeout(function(){hide_snackber(snackbar)}, 3000);
}
// Скрыть панель
function hide_snackber(bar)
{ 
    bar.classList.remove("show"); 
}
