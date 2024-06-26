using Alice1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Alice1.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly MainContext _mainContext;
        [BindProperty]
        public DevoloperData DevData { get; set; }
        public List<Developer> NewDeveloperList { get; set; } = new List<Developer>();
        public LoginModel(MainContext mainContext)
        {
            _mainContext = mainContext;

        }

        [BindProperty]
        public Developer NewDeveloper { get; set; }

        public void OnGet()
        {
            
            
        }
        public IActionResult OnPostLogin()
        {
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
                else
                {
                    // ����������� �� ������, ��������, ����� ����������� �����-�� ��������
                    // ��������, ������� ��������� �� ������ �� ������� ��������
                    ModelState.AddModelError(string.Empty, "������������ ��� ������������ ��� ������");
                }
            }

            // ���� ��� ����� �� ���� �����, ������ �������� ������ ��� ������ �� ���� ������� ���������
            // ���������� ������� �������� � ���������� ��������
            return Page();
        }
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            //HttpContext.Session.SetInt32("LoggedDeveloperId", 0); // ���������� ������ � 0 ��� ������
            return RedirectToPage("/Index"); // ������������� �� ������� �������� ��� ������ ��������
        }



    }
    public class DevoloperData {
        [Required]
        [Display(Name ="��� ������������")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "������")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    
    }
   
}
