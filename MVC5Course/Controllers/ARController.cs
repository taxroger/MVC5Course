using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : Controller
    {
        // GET: AR
        public ActionResult Index()
        {
            return View("123");
        }

        public ActionResult File1()
        {
            return File(Server.MapPath("~/Content/kbs_NBzhk_1200x0.png"), "image/png");
        }

        public ActionResult File2()
        {
            return File(Server.MapPath("~/Content/kbs_NBzhk_1200x0.png"), "image/png", "picxxx.png");
        }
    }

}