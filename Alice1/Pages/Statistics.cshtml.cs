using Alice1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Globalization;

namespace Alice1.Views
{
    public class StatisticModel : PageModel
    {
        private readonly MainContext _mainContext;
        public List<DataPoint> DataPointsUsers { get; set; }
        public List<DataPoint> DataPointsReques { get; set; }
        public List<User> NewUserList { get; set; } = new List<User>();
        

        [BindProperty]
        public User NewUser { get; set; }
        public int skillid;
        public int user_count;
        public StatisticModel(MainContext mainContext)
        {
            _mainContext = mainContext;

        }
        public void OnGet(int skillId)
        {
            user_count = 0;
            SkillRepository skillRepository = new SkillRepository(_mainContext);
            var skillData = skillRepository.GetById(skillId);
            if (skillId != 0) skillid = skillId;
            NewUserList = _mainContext.Users.Where(Users => Users.skill.Id == skillid).ToList();

            DateTime currentDate = DateTime.Now;

            // Получаем текущую дату и время
            

            // Формируем список дат за последний месяц
            List<DateTime> last30Days = Enumerable.Range(0, 30)
                .Select(offset => currentDate.Date.AddDays(-offset))
                .ToList();

            // Отфильтровываем пользователей по дате за последний месяц
            List<User> usersLast30Days = NewUserList
                .Where(u =>
                {
                    if (DateTime.TryParseExact(u.date, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime userDate))
                    {
                        return last30Days.Contains(userDate.Date); // Фильтруем по датам за последний месяц
                    }
                    else
                    {
                        return false;
                    }
                })
                .ToList();

            // Подготавливаем данные для диаграммы
            DataPointsUsers = last30Days
     .Select(day => new DataPoint
     {
         Day = day.ToString("yyyy-MM-dd"), // Преобразуем дату в строку для метки на диаграмме
         Count = usersLast30Days.Count(u =>
             DateTime.TryParseExact(u.date, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime userDate)
             && userDate.Date == day.Date
         )
     })
     .ToList();

            Console.WriteLine("DataPoints: " + JsonConvert.SerializeObject(DataPointsUsers));
            for(int i = 0; i < DataPointsUsers.Count; i++)
            {

                user_count = user_count + DataPointsUsers[i].Count;

            }
            TempData["user_count"] = user_count;
            TempData["skill_id"] = skillid;
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
    public class DataPoint
    {
        public string Day { get; set; }
        public int Count { get; set; }
    }
}
