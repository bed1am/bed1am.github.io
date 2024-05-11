using Alice1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq; 


namespace Alice1.Views
{
    public class TrainingModel : PageModel
    {
        private readonly MainContext _mainContext;

        public List<User> NewUserList { get; set; } = new List<User>();
        public List<ReqRes> NewReqResList { get; set; } = new List<ReqRes>();
        public List<User> usersWithUnansweredQuestions { get; set; } = new List<User>();

        [BindProperty]
        public User NewUser { get; set; }
        public ReqRes NewReqRes { get; set; }
        public int skillid;
        public TrainingModel(MainContext mainContext)
        {
            _mainContext = mainContext;

        }
        public void OnGet(int skillId)
        {
            SkillRepository skillRepository = new SkillRepository(_mainContext);
            var skillData = skillRepository.GetById(skillId);
            if (skillId != 0) skillid = skillId;
            NewReqResList = _mainContext.ReqRess.Where(ReqRess => ReqRess.skill.Id == skillid).ToList();


            var requestList = NewReqResList.Select(rr => rr.Request);
            //NewUserList = _mainContext.Users.Where(Users => Users.skill.Id == skillid && !NewReqResList.Where(reqRes => reqRes.Request.Equals(Users.request)).Any()).ToList();
            NewUserList = _mainContext.Users
    .Where(u => u.skill.Id == skillid )
    .ToList();
            NewUserList = NewUserList
    .Where(u => !requestList.Contains(u.request))
    .ToList();
            //        List<User> NewUserList = _mainContext.Users
            //.Include(u => u.request) // ��������� ������ �������� ��� ������� ������������
            //.Where(u => u.skill == skillData) // ��������� �� ID ������
            //.ToList();

            //        // �������� ������ �������� �� ������� ���� ������
            //        List<string> answeredQuestions = NewReqResList.Select(rr => rr.Response).ToList();

            //        // ��������� �������������, ��������� ������ ���, � ������� ���� ������� ��� �������

            //List<User> usersWithUnansweredQuestions = NewUserList
            //.Where(u => !NewReqResList.Where(reqRes => reqRes.Request.Equals(u.request)).Any())
            //.ToList();

            TempData["skill_id"] = skillid;
            //NewUserList = _mainContext.Users.ToList();
        }
        public async Task<IActionResult> OnPostAsync(int skillId1, string request, string response)
        {
            SkillRepository skillRepository = new SkillRepository(_mainContext);
            // ������� ����� ������ ReqRes � ��������� ��� ������� �� �����
            var reqRes = new ReqRes
            {
                
            

            //ReqRes developer1 = _mainContext.Developers
            //        .Where(Developers=> Developers.Id == 1)
            //        .First();

            
                skill =  skillRepository.GetById(skillId1),
                Request = request,
                Response = response,
                // �����������, ��� � ReqRes ���� ���� ��� ���� � �����, ������� ����� ���������
                //Date = DateTime.Now
            };

            // ��������� ������ ReqRes � �������� ���� ������ � ��������� ���������
            _mainContext.ReqRess.Add(reqRes);
            await _mainContext.SaveChangesAsync();

            // �������������� ������� �� �������� Training
            return RedirectToPage("/Training", new { skillId = skillId1 });
        }
    }
}
