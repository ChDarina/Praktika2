using Praktika2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult> Index(string searchString)
        {
            var illustrations = from m in context.Illustrations
                         select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                illustrations = illustrations.Where(s => s.Name.Contains(searchString) || s.Illustrators.IllustratorNickname.Contains(searchString));
            }
            return View(await illustrations.ToListAsync());
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
            ViewBag.Message = "Пишите на данную электронную почту, если программа будет работать неправильно";
            return View();
        }
    }
}