using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WillThisWork.Models;

namespace WillThisWork.Data
{
    public class HateRepository : Repository
    {


        public HateRepository(ApplicationDbContext context) : base(context)
        {

        }

        public HateLikeModel GetList()
        {
            HateLikeModel model = new HateLikeModel();
            model.Hates = _context.Hates.ToList();
            model.UserLikes = _context.UserLikes.ToList();

            return model;
        }

        public Hate Get(int? id)
        {
            var hates = _context.Hates.AsQueryable();
            return hates.Where(h => h.Id == id).SingleOrDefault();
        }

        public void Add(Hate hate)
        {
            _context.Hates.Add(hate);
            _context.SaveChanges();
        }
        public void Update(Hate hate, int id)
        {
            //TO DO Update
        }
        public bool LeaveComment(Comment comment)
        {
          
            _context.Comments.Add(comment);
            
            return _context.SaveChanges() > 0;
        }
        public List<Comment> GetComments(int entityId, int hateId)
        {
            List<Comment> comments = new List<Comment>();


            comments = _context.Comments.Where(b => b.EntityId == entityId && b.HateId == hateId).ToList();

            return comments;
        }

        public void Like(int id)
        {


            var hate = _context.Hates.Where(a => a.Id == id).SingleOrDefault();
            UserLike userLike = new UserLike { HateId = id, UserName = System.Web.HttpContext.Current.User.Identity.Name };
            var userLikesOfHate = _context.UserLikes.
                Where(u => u.HateId == id).
                Where(a => a.UserName == userLike.UserName).ToList();
            
            
            if(userLikesOfHate.Count == 0 )
            {
                hate.Likes++;
                hate.UserLikes.Add(userLike);
                _context.SaveChanges();

            }

        }

        public void Dislike(int id)
        {
            var hate = _context.Hates.Where(a => a.Id == id).SingleOrDefault();
            UserLike userLike = new UserLike { HateId = id, UserName = System.Web.HttpContext.Current.User.Identity.Name };
            var userLikesOfHate = _context.UserLikes.Where(u => u.HateId == id).ToList();

            if (userLikesOfHate == null)
            {
                hate.Dislikes++;
                userLikesOfHate.Add(userLike);
                _context.SaveChanges();

            }
            else if (!userLikesOfHate.Contains(userLike))
            {
                hate.Dislikes++;
                userLikesOfHate.Add(userLike);
                _context.SaveChanges();
            }
        }


    }
}