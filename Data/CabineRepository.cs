using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WillThisWork.Models;
using WillThisWork.ViewModels;

namespace WillThisWork.Data
{
    public class CabineRepository : Repository
    {

        public CabineRepository(ApplicationDbContext context) : base(context)
        {
           
        }
        
        public List<ApplicationUser> GetApplicationUsers()
        {
            
            List<ApplicationUser> users = _context.Users.ToList();

            return users;
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

        public void Update(UpdateUserRoleViewModel model)
        {
            var user = _context.Users.Find(model.Id);
            user.Roles.Clear();

            IdentityUserRole role = new IdentityUserRole();
            role.UserId = model.Id;
            role.RoleId = model.RoleId;
            user.Roles.Add(role);
            _context.SaveChanges();
        }

    }
}