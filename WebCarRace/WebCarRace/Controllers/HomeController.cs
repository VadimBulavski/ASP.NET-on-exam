using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebCarRace.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            //HttpBrowserCapabilitiesBase browser = HttpContext.Request.Browser;
            return View();
        }
    }
}
