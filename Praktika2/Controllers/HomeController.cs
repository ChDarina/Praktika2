using Praktika2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Praktika2.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext context;
        public HomeController()
        {
            context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }
        public ActionResult Index()
        {
            ViewBag.IllustrationTechnics = new SelectList(context.Technics, "TechnicsID", "TechnicsName");
            var ill = context.Illustrations.Where(c => c.Privacy == false);
            List<Illustrations> ill2 = ill.ToList();
            return View("Index", ill2);
        }
        public ActionResult Details(int id)
        {
            var illustrator = context.Illustrators.SingleOrDefault(i => i.IllustratorID == id);
            return View("Details", illustrator);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Это работа на практику Чупиной Д.В., ПИ-19-1";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Контакт на случай, если программа будет работать неправильно";

            return View();
        }
    }
}