using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Alice1.Models
{
    public class SkillRepository
    {
        private readonly MainContext _context;

        public SkillRepository(MainContext context)
        {
            _context = context;
        }

        public IEnumerable<Skill> GetAllSkills()
        {
            return _context.Skills.ToList();
        }

        public Skill GetById(int id)
        {
            return _context.Skills.Find(id);
        }
        public Skill GetByName(string name)
        {
            return _context.Skills.Find(name);
        }
        public void AddSkill(Skill skill)
        {
            _context.Skills.Add(skill);
            _context.SaveChanges();
        }

        public void UpdateDeveloper(Skill skill)
        {
            _context.Skills.Update(skill);
            _context.SaveChanges();
        }

        public void DeleteSkill(int id)
        {
            var skill = _context.Skills.Find(id);
            if (skill != null)
            {
                _context.Skills.Remove(skill);
                _context.SaveChanges();
            }
        }

    }
}
