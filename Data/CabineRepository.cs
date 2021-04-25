using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WillThisWork.Models;

namespace WillThisWork.Data
{
    public class CabineRepository : Repository
    {

        public CabineRepository(ApplicationDbContext context) : base(context)
        {

        }


        public List<Hate> GetHates(string userId)
        {

            List<Hate> hates = new List<Hate>();
            hates = _context.Hates.Where(h => h.UserId == userId).ToList();


            return hates;
        }

        public void Delete(int? id)
        {
            Hate hate = _context.Hates.Where(h => h.Id == id).SingleOrDefault();

            _context.Hates.Remove(hate);
            _context.SaveChanges();
        }

    }
}