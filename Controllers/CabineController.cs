using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WillThisWork.Data;

namespace WillThisWork.Controllers
{
    public class CabineController : BaseController
    {

        public CabineRepository _cabineRepository = null;

        public CabineController()
        {
            _cabineRepository = new CabineRepository(context);
        }


        // GET: Cabine
        [HttpGet]
        public ActionResult MyCabine()
        {

            return View();
        
        }

        public ActionResult MyHates()
        {

            string userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var hates = _cabineRepository.GetHates(userId);
            return View(hates);
        }
    }
}