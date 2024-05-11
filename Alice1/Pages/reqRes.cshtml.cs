using Alice1.Models;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Alice1.Views
{
    public class ReqResModel : PageModel
    {
        private readonly MainContext _mainContext;
        public List<ReqRes> NewReqResList { get; set; } = new List<ReqRes>();
        public int skillid;

        [BindProperty]
        public ReqRes NewReqRes { get; set; } 
        public ReqResModel(MainContext mainContext)
        {
            _mainContext = mainContext;

        }
        

        public IActionResult OnGet(int skillId)
        {
            
            SkillRepository skillRepository = new SkillRepository(_mainContext);
            var skillData = skillRepository.GetById(skillId);
            if(skillId != 0) skillid = skillId;
           
           
            TempData["skill_id"] = skillid;
            NewReqResList = _mainContext.ReqRess.Where(ReqRess => ReqRess.skill.Id == skillid).ToList();
            return Page();

            
        }
        //public void OnGet()
        //{

        //    NewReqResList = _mainContext.ReqRess.Where(ReqRess => ReqRess.skill.Id == skillid).ToList();
        //}
        public IActionResult OnPost(int skillId)
        {
            SkillRepository skillRepository = new SkillRepository(_mainContext);
            Console.WriteLine(skillId);

            //ReqRes developer1 = _mainContext.Developers
            //        .Where(Developers=> Developers.Id == 1)
            //        .First();

            NewReqRes.skill = skillRepository.GetById(skillId);
            _mainContext.ReqRess.Add(NewReqRes);
            _mainContext.SaveChanges();
            return RedirectToPage();
        }
        public IActionResult OnPostDelete(int id)
        {
            ReqRes reqResToDelete = _mainContext.ReqRess.Find(id);
            // Найдите элемент для удаления (например, по Id) и выполните удаление
            if (reqResToDelete != null)
            {
                // Удаляем элемент из контекста данных
                _mainContext.ReqRess.Remove(reqResToDelete);

                // Сохраняем изменения в базе данных
                _mainContext.SaveChanges();
            }
            

            // После удаления перенаправьте пользователя обратно на страницу или обновите данные
            return RedirectToPage();
        }
        
            public IActionResult OnPostChange(int skillId, string name, string webhook)
        {
            Skill skillToChange = _mainContext.Skills.Find(skillId);
            // Найдите элемент для удаления (например, по Id) и выполните удаление
            if (skillToChange != null)
            {
                // Удаляем элемент из контекста данных
                skillToChange.name = name;
                skillToChange.hook_url = webhook;
                // Сохраняем изменения в базе данных
                _mainContext.SaveChanges();
            }


            // После удаления перенаправьте пользователя обратно на страницу или обновите данные
            return RedirectToPage();
        }
    }
}
