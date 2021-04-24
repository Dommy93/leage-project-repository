using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WillThisWork.Data;
using WillThisWork.Models;

namespace WillThisWork.Controllers
{
    public class HomeController : BaseController
    {

        private HateRepository _hateRepository = null;

        public HomeController()
        {
            _hateRepository = new HateRepository(context);
        }


        public ActionResult Index()
        {

            //List<Hate> hates = _hateRepository.GetList();
            HateLikeModel model = _hateRepository.GetList();
            List<ApplicationUser> users = context.Users.ToList();

            ViewBag.data = users;

            return View(model);
        }
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Hate hate = _hateRepository.Get(id);
            

            hate.Comments = _hateRepository.GetComments(23, hate.Id);


            if (hate == null)
            {
                return HttpNotFound();
            }

            return View(hate);

        }


        public ActionResult Add()
        {

            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Index");
            }

            Hate hate = new Hate();
           

            return View(hate);
        }


        [HttpPost]
        public ActionResult Add(Hate hate)
        {
            if (ModelState.IsValid)
            {

                string fileName = Path.GetFileNameWithoutExtension(hate.Image.FileName);
                string extension = Path.GetExtension(hate.Image.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                hate.ImagePath = "~/Images/" + fileName;

                fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                hate.Image.SaveAs(fileName);
                hate.fileExtension = extension;
                hate.Likes = 0;
                hate.Dislikes = 0;
                if (System.Web.HttpContext.Current != null &&
                    System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    hate.UserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                }


               
                _hateRepository.Add(hate);

                return RedirectToAction("Detail", new { id = hate.Id });
            }
            return View(hate);
        }
   

        public ActionResult Ranking()
        {

            //List<Hate> sortedHates = _hateRepository.GetList().OrderByDescending(h => h.Likes).ToList();


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public JsonResult LeaveComment(CommentViewModel model)
        {
            JsonResult result = new JsonResult();

            try
            {
                var comment = new Comment();
                comment.Text = model.Text;
                comment.EntityId = model.EntityId;
                comment.HateId = model.RecordId;
                comment.UserId = User.Identity.GetUserId();
                comment.UserName = User.Identity.GetUserName();

                comment.TimeStamp = DateTime.UtcNow;


                var res = _hateRepository.LeaveComment(comment);
                

                result.Data = new { Success = res};
                
            }
            catch(Exception ex)
            {
                result.Data = new { Success = false, ex.Message };
            }




            return result;

        }
        [HttpPost]
        public JsonResult AddLike(int? id)
        {

            JsonResult result = new JsonResult();

            _hateRepository.Like(id.Value);
            result.Data = new { Success = true };

            return result;

        }
        [HttpPost]
        public JsonResult AddDislike(int? id)
        {

            JsonResult result = new JsonResult();

            _hateRepository.Dislike(id.Value);
            result.Data = new { Success = true };

            return result;

        }
        public ActionResult Update(int? id)
        {
            Hate hate = _hateRepository.Get(id);
            return View(hate);
        }

        [HttpPost]
        public ActionResult Update(Hate hate)
        {

            _hateRepository.Update(hate);

            return RedirectToAction("Detail", new { id = hate.Id });

        }

    }
}