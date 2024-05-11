using System.ComponentModel.DataAnnotations;
namespace Alice1.Models
{
    public class User
    {
        public int Id { get; set; }
        public string name { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string request { get; set; }
        public Skill skill { get; set; }    
        public string date { get; set; }
    }
}
