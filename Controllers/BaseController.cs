using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WillThisWork.Data;
using WillThisWork.Models;

namespace WillThisWork.Controllers
{
    public class BaseController : Controller
    {

        protected ApplicationDbContext context { get; private set; }

        private bool _dispose = false;

        public Repository repository { get; private set; }


        public BaseController()
        {
            context = new ApplicationDbContext();
            repository = new Repository(context);
        }

        protected override void Dispose(bool disposing)
        {

            if (_dispose)
            {
                return;
            }
            if (disposing)
            {
                context.Dispose();
            }

            _dispose = true;

            base.Dispose(disposing);
        }

    }
}