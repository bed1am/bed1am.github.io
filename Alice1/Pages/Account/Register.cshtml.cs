using Alice1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Alice1.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly MainContext _mainContext;
        [BindProperty]
        public regDevoloperData DevData { get; set; }
        public List<Developer> NewDeveloperList { get; set; } = new List<Developer>();
        public RegisterModel(MainContext mainContext)
        {
            _mainContext = mainContext;

        }

        [BindProperty]
        public Developer NewDeveloper { get; set; }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (DevData.Password == DevData.rePassword)
            {
                NewDeveloper.login = DevData.Username;
                NewDeveloper.password = DevData.Password;
                if (_mainContext.Developers.FirstOrDefault(d => d.login == DevData.Username) == null)
                {
                    _mainContext.Developers.Add(NewDeveloper);
                    _mainContext.SaveChanges();
                }
                else
                {
                    // Разработчик не найден, возможно, нужно предпринять какие-то действия
                    // Например, вывести сообщение об ошибке на текущей странице
                    ModelState.AddModelError(string.Empty, "Имя пользователя занято");
                }
            }
            else
            {
                // Разработчик не найден, возможно, нужно предпринять какие-то действия
                // Например, вывести сообщение об ошибке на текущей странице
                ModelState.AddModelError(string.Empty, "Пароли не совпадают");
            }
            // Проверка, что введены данные пользователя
            if (!string.IsNullOrEmpty(DevData.Username) && !string.IsNullOrEmpty(DevData.Password))
            {
                // Поиск разработчика по имени пользователя и паролю
                NewDeveloper = _mainContext.Developers.FirstOrDefault(d => d.login == DevData.Username && d.password == DevData.Password);

                // Проверка, был ли найден разработчик
                if (NewDeveloper != null)
                {
                    // Получаем идентификатор аутентифицированного разработчика
                    int developerId = NewDeveloper.Id;

                    // Сохраняем идентификатор в сессии
                    HttpContext.Session.SetInt32("LoggedDeveloperId", developerId);

                    // Разработчик найден, выполняем нужные действия
                    // Например, перенаправляем пользователя на другую страницу
                    return RedirectToPage("/index");
                }
               
            }

            // Если код дошел до этой точки, значит возникла ошибка или данные не были введены корректно
            // Возвращаем текущую страницу с возможными ошибками
            return Page();
        }

        public class regDevoloperData
        {
            [Required]
            [Display(Name = "Имя пользователя")]
            public string Username { get; set; }
            [Required]
            [Display(Name = "Пароль")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            [Required]
            [Display(Name = "Повторите пароль")]
            [DataType(DataType.Password)]
            public string rePassword { get; set; }

        }

    }
 
}
