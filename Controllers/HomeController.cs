using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WillThisWork.Data;
using WillThisWork.Models;
using WillThisWork.ViewModels;

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

        public ActionResult Page(int? id)
        {
            
           /* if (page == null)
            {

                return HttpNotFound();
            }*/



            HateLikeModel model = _hateRepository.GetListPage(id);
            List<ApplicationUser> users = context.Users.ToList();
            model.Page = _hateRepository.GetList().Hates.Count;
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

            HateAddModel model = new HateAddModel();
            model.Init(repository);

            return View(model);
        }


        [HttpPost]
        public ActionResult Add(HateAddModel model)
        {



            if (ModelState.IsValid)
            {


                var hate = model.Hate;

                string fileName = Path.GetFileNameWithoutExtension(model.Image.FileName);
                string extension = Path.GetExtension(model.Image.FileName);
                fileName = fileName + Guid.NewGuid() + extension;
                hate.ImagePath = "~/Images/" + fileName;

                fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                model.Image.SaveAs(fileName);
                hate.fileExtension = extension;
                hate.Likes = 0;
                hate.Dislikes = 0;
                if (System.Web.HttpContext.Current != null &&
                    System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    hate.UserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                }


               
                _hateRepository.Add(hate);

                model.Init(repository);

                return RedirectToAction("Detail", new { id = hate.Id });
            }
            return View(model);
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

            if (User.Identity.IsAuthenticated == true)
            {
                _hateRepository.Like(id.Value);
                result.Data = new { Success = true };
            }
            else
            {
                result.Data = new { Success = false };
                return result;
            }

            return result;

        }
        [HttpPost]
        public JsonResult AddDislike(int? id)
        {

            JsonResult result = new JsonResult();

            if (User.Identity.IsAuthenticated == true)
            {
                _hateRepository.Dislike(id.Value);
                result.Data = new { Success = true };
            }
            else
            {
                result.Data = new { Success = false };
                return result;
            }
            return result;

        }
        public ActionResult Update(int? id)
        {
            Hate hate = _hateRepository.Get(id);
            UpdateHateViewModel model = 
                new UpdateHateViewModel() { Id = hate.Id,
                    Description = hate.Description,
                    HatedSummoner = hate.HatedSummoner,
                    Title = hate.Title };
            model.Init(repository);
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(UpdateHateViewModel hate)
        {

            _hateRepository.Update(hate);

            return RedirectToAction("Detail", new { id = hate.Id });

        }

        public ActionResult WaitingRoom()
        {
            List<ApplicationUser> users = context.Users.ToList();

            ViewBag.data = users;

            return View(_hateRepository.GetListWR());
        }

        [HttpPost]
        public ActionResult AddToMainPage(int? id)
        {


            _hateRepository.AddToMain(id);

            return RedirectToAction("Index", "Home");

        }


        public ActionResult Champions()
        {
            return View(context.Champions.ToList());
        }

        public ActionResult ProfileCabine(string id)
        {
            var model = new ProfileModel();


            model.ChampHateDict = _hateRepository.getChampHates(id);

            var hates = _hateRepository.GetListWR().Hates.Where(h => h.UserId == id).ToList();
            model.Hates = hates;
            model.Champions = context.Champions.ToList();
            

            return View(model);

        }

    }
}