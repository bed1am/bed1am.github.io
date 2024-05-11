using Alice1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;

namespace Alice1.Views
{
    public class IndexModel : PageModel
    {
        private readonly MainContext _mainContext;

        public List<Skill> NewSkillList { get; set; } = new List<Skill>();
        public int LoggedDeveloperId { get; set; } // ѕеременна€ дл€ хранени€ Id авторизованного разработчика



        [BindProperty]
        public Skill NewSkill { get; set; }
        

        public IndexModel(MainContext mainContext)
        {
            _mainContext = mainContext;

		}

        public IActionResult OnGet()
        {
            int loggedDeveloperId = HttpContext.Session.GetInt32("LoggedDeveloperId") ?? 0;

            // ѕровер€ем, если LoggedDeveloperId равен 0 или отсутствует в сессии, перенаправл€ем на страницу входа
            if (loggedDeveloperId == 0)
            {
                return RedirectToPage("/Account/Login");
            }

            // ѕродолжаем выполнение метода, если LoggedDeveloperId не равен 0
           
            NewSkillList = _mainContext.Skills.Where(s => s.developer.Id == loggedDeveloperId).ToList();

            // «десь возможно есть логика, св€занна€ с загрузкой данных, котора€ не требует перенаправлени€

            return Page();
        }
        public IActionResult OnPost()
        {
            DeveloperRepository developerRepository = new DeveloperRepository(_mainContext);


            //ReqRes developer1 = _mainContext.Developers
            //        .Where(Developers=> Developers.Id == 1)
            //        .First();
            // »спользуем Id авторизованного разработчика дл€ добавлени€ навыка
            int loggedDeveloperId = HttpContext.Session.GetInt32("LoggedDeveloperId") ?? 0;
            NewSkill.developer = developerRepository.GetById(loggedDeveloperId);
            _mainContext.Skills.Add(NewSkill);
            _mainContext.SaveChanges();
            return RedirectToPage();
        }
        public IActionResult OnPostDelete(int id)
        {
            Skill skillToDelete = _mainContext.Skills.Find(id);
            // Ќайдите элемент дл€ удалени€ (например, по Id) и выполните удаление
            if (skillToDelete != null)
            {
                // ”дал€ем элемент из контекста данных
                _mainContext.Skills.Remove(skillToDelete);

                // —охран€ем изменени€ в базе данных
                _mainContext.SaveChanges();
            }


            // ѕосле удалени€ перенаправьте пользовател€ обратно на страницу или обновите данные
            return RedirectToPage();
        }

    }
}
