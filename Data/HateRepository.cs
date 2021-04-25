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
            model.Likes = _context.Likes.ToList();
            model.Dislikes = _context.Dislikes.ToList();

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
            Likes likes = new Likes { HateId = id, UserName = System.Web.HttpContext.Current.User.Identity.Name };
            var userLikesOfHate = _context.Likes.
                Where(u => u.HateId == id).
                Where(a => a.UserName == likes.UserName).ToList();


            if (userLikesOfHate.Count == 0)
            {
                hate.Likes++;
                hate.LikeList.Add(likes);
                _context.SaveChanges();

            }
            else
            {
                hate.Likes--;
                _context.Likes.Remove(userLikesOfHate.SingleOrDefault());
                _context.SaveChanges();
            }

        }

        public void Dislike(int id)
        {
            var hate = _context.Hates.Where(a => a.Id == id).SingleOrDefault();
            Dislikes dislikes = new Dislikes { HateId = id, UserName = System.Web.HttpContext.Current.User.Identity.Name };
            var userLikesOfHate = _context.Dislikes.
                 Where(u => u.HateId == id).
                 Where(a => a.UserName == dislikes.UserName).ToList();

            if (userLikesOfHate.Count == 0)
            {
                hate.Dislikes++;
                hate.DislikeList.Add(dislikes);
                _context.SaveChanges();

            }
            else
            {
                hate.Dislikes--;
                _context.Dislikes.Remove(userLikesOfHate.SingleOrDefault());
                _context.SaveChanges();
            }
        }

        public void Update(UpdateHateViewModel hate)
        {
            Hate hate2 = _context.Hates.Find(hate.Id);
            _context.Entry(hate2).CurrentValues.SetValues(hate);
            _context.SaveChanges();
        }

  
    }
}