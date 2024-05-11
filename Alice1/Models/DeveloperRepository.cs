using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Alice1.Models
{
    public class DeveloperRepository
    {
        private readonly MainContext _context;

        public DeveloperRepository(MainContext context)
        {
            _context = context;
        }

        public IEnumerable<Developer> GetAllDevelopers()
        {
            return _context.Developers.ToList();
        }

        public Developer GetById(int id)
        {
            return _context.Developers.Find(id);
        }

        public void AddDeveloper(Developer developer)
        {
            _context.Developers.Add(developer);
            _context.SaveChanges();
        }

        public void UpdateDeveloper(Developer developer)
        {
            _context.Developers.Update(developer);
            _context.SaveChanges();
        }

        public void DeleteDeveloper(int id)
        {
            var developer = _context.Developers.Find(id);
            if (developer != null)
            {
                _context.Developers.Remove(developer);
                _context.SaveChanges();
            }
        }

    }
}
