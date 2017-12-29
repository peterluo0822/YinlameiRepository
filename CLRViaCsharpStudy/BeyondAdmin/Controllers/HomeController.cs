using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeyondAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _Home()
        {
            return View();
        }

        public ActionResult _Menu()
        {
            ViewBag.listFunc = null;
            ViewBag.listGroup = null;
            return PartialView();
        }

        public ActionResult _Head()
        {
            ViewBag.UserInfoModel = null;
            return PartialView(null);
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
    }
}