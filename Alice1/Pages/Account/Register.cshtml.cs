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
                    // ����������� �� ������, ��������, ����� ����������� �����-�� ��������
                    // ��������, ������� ��������� �� ������ �� ������� ��������
                    ModelState.AddModelError(string.Empty, "��� ������������ ������");
                }
            }
            else
            {
                // ����������� �� ������, ��������, ����� ����������� �����-�� ��������
                // ��������, ������� ��������� �� ������ �� ������� ��������
                ModelState.AddModelError(string.Empty, "������ �� ���������");
            }
            // ��������, ��� ������� ������ ������������
            if (!string.IsNullOrEmpty(DevData.Username) && !string.IsNullOrEmpty(DevData.Password))
            {
                // ����� ������������ �� ����� ������������ � ������
                NewDeveloper = _mainContext.Developers.FirstOrDefault(d => d.login == DevData.Username && d.password == DevData.Password);

                // ��������, ��� �� ������ �����������
                if (NewDeveloper != null)
                {
                    // �������� ������������� �������������������� ������������
                    int developerId = NewDeveloper.Id;

                    // ��������� ������������� � ������
                    HttpContext.Session.SetInt32("LoggedDeveloperId", developerId);

                    // ����������� ������, ��������� ������ ��������
                    // ��������, �������������� ������������ �� ������ ��������
                    return RedirectToPage("/index");
                }
               
            }

            // ���� ��� ����� �� ���� �����, ������ �������� ������ ��� ������ �� ���� ������� ���������
            // ���������� ������� �������� � ���������� ��������
            return Page();
        }

        public class regDevoloperData
        {
            [Required]
            [Display(Name = "��� ������������")]
            public string Username { get; set; }
            [Required]
            [Display(Name = "������")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            [Required]
            [Display(Name = "��������� ������")]
            [DataType(DataType.Password)]
            public string rePassword { get; set; }

        }

    }
 
}
