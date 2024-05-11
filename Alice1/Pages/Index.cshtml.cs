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
        public int LoggedDeveloperId { get; set; } // ���������� ��� �������� Id ��������������� ������������



        [BindProperty]
        public Skill NewSkill { get; set; }
        

        public IndexModel(MainContext mainContext)
        {
            _mainContext = mainContext;

		}

        public IActionResult OnGet()
        {
            int loggedDeveloperId = HttpContext.Session.GetInt32("LoggedDeveloperId") ?? 0;

            // ���������, ���� LoggedDeveloperId ����� 0 ��� ����������� � ������, �������������� �� �������� �����
            if (loggedDeveloperId == 0)
            {
                return RedirectToPage("/Account/Login");
            }

            // ���������� ���������� ������, ���� LoggedDeveloperId �� ����� 0
           
            NewSkillList = _mainContext.Skills.Where(s => s.developer.Id == loggedDeveloperId).ToList();

            // ����� �������� ���� ������, ��������� � ��������� ������, ������� �� ������� ���������������

            return Page();
        }
        public IActionResult OnPost()
        {
            DeveloperRepository developerRepository = new DeveloperRepository(_mainContext);


            //ReqRes developer1 = _mainContext.Developers
            //        .Where(Developers=> Developers.Id == 1)
            //        .First();
            // ���������� Id ��������������� ������������ ��� ���������� ������
            int loggedDeveloperId = HttpContext.Session.GetInt32("LoggedDeveloperId") ?? 0;
            NewSkill.developer = developerRepository.GetById(loggedDeveloperId);
            _mainContext.Skills.Add(NewSkill);
            _mainContext.SaveChanges();
            return RedirectToPage();
        }
        public IActionResult OnPostDelete(int id)
        {
            Skill skillToDelete = _mainContext.Skills.Find(id);
            // ������� ������� ��� �������� (��������, �� Id) � ��������� ��������
            if (skillToDelete != null)
            {
                // ������� ������� �� ��������� ������
                _mainContext.Skills.Remove(skillToDelete);

                // ��������� ��������� � ���� ������
                _mainContext.SaveChanges();
            }


            // ����� �������� ������������� ������������ ������� �� �������� ��� �������� ������
            return RedirectToPage();
        }

    }
}
