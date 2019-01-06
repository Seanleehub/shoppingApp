using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplicationA2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About Us";

            return View();
        }
               
        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Page";

            return View();
        }     

        public ActionResult SiteMap()
        {
            return View();
        }
      
    }
}