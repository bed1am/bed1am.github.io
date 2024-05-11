using System.ComponentModel.DataAnnotations;
namespace Alice1.Models
{
    public class ReqRes
    {
        public int Id { get; set; }
        public Skill skill { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string Request { get; set; }
        public string Response { get; set; }

    }
}
