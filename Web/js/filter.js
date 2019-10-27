function filter() 
{
    var input = document.getElementById("filter");// Поле поиска
    var filter = input.value.toUpperCase();// Значение в поле поиска
    var tr = document.querySelectorAll("table tr + tr");// РядЫ элементов
    
    for (var i = 0; i < tr.length; i++) 
    {
        // Имя по которому ищем
        var name = tr[i].querySelector("td:nth-child(2)");
        
        if (name.innerHTML.toUpperCase().indexOf(filter) > -1) 
        {
            // Если есть совпадение показать
            tr[i].style.display = "";
        } 
        else 
        {
            // Скрыть не подходящие
            tr[i].style.display = "none";
        }
    }
    
}