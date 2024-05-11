using Alice1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Alice1.Views
{
    public class DialogsModel : PageModel
    {
        private readonly MainContext _mainContext;

        public List<User> NewUserList { get; set; } = new List<User>();
        public List<ReqRes> NewReqResList { get; set; } = new List<ReqRes>();

        [BindProperty]
        public ReqRes NewReqRes { get; set; }
        public User NewUser { get; set; }

        public int skillid;
        public DialogsModel(MainContext mainContext)
        {
            _mainContext = mainContext;

        }
        public void OnGet(int skillId)
        {
            SkillRepository skillRepository = new SkillRepository(_mainContext);
            var skillData = skillRepository.GetById(skillId);
            if (skillId != 0) skillid = skillId;
            
            NewUserList = _mainContext.Users.Where(Users => Users.skill.Id == skillid).ToList();
            NewReqResList = _mainContext.ReqRess.Where(ReqRess => ReqRess.skill.Id == skillid).ToList();


            TempData["skill_id"] = skillid;
            //NewUserList = _mainContext.Users.ToList();
        }
        public IActionResult OnPost()
        {
            DeveloperRepository developerRepository = new DeveloperRepository(_mainContext);


            //ReqRes developer1 = _mainContext.Developers
            //        .Where(Developers=> Developers.Id == 1)
            //        .First();
            int uid = 1;
            //NewUser.skill = developerRepository.GetById(uid);
            _mainContext.Users.Add(NewUser);
            _mainContext.SaveChanges();
            return RedirectToPage();
        }
    }
}
