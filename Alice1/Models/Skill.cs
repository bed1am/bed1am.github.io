namespace Alice1.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public Developer developer { get; set; }
        public string name { get; set; }
        public string hook_url { get; set; }
    }
}
