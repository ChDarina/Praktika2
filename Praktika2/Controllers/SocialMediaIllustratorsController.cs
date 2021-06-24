using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Praktika2.Models;
using Microsoft.AspNet.Identity;

namespace Praktika2.Controllers
{
    public class SocialMediaIllustratorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SocialMediaIllustrators
        public ActionResult Index()
        {
            var socialMediaIllustrators = db.SocialMediaIllustrators.Include(s => s.Illustrators).Include(s => s.SocialMedias);
            return View(socialMediaIllustrators.ToList());
        }

        // GET: SocialMediaIllustrators/Details/5
        public ActionResult Details(int? userid, int? socid)
        {
            if (userid == null || socid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialMediaIllustrators socialMediaIllustrators = db.SocialMediaIllustrators.Find(userid, socid);
            if (socialMediaIllustrators == null)
            {
                return HttpNotFound();
            }
            return View(socialMediaIllustrators);
        }

        // GET: SocialMediaIllustrators/Create
        public ActionResult Create()
        {
            //ViewBag.IllustratorID = new SelectList(db.Illustrators, "IllustratorID", "UserId");
            ViewBag.SocialMediaID = new SelectList(db.SocialMedias, "SocialMediaID", "SocialMediaName");
            return View();
        }

        // POST: SocialMediaIllustrators/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IllustratorID,SocialMediaID,IllustratorReference")] SocialMediaIllustrators socialMediaIllustrators)
        {
            string userId = User.Identity.GetUserId();
            var illustrator = db.Illustrators.Where(c => userId == c.UserId.ToString()).ToList();
            socialMediaIllustrators.IllustratorID = illustrator[0].IllustratorID;
            if (ModelState.IsValid)
            {
                db.SocialMediaIllustrators.Add(socialMediaIllustrators);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.IllustratorID = new SelectList(db.Illustrators, "IllustratorID", "UserId", socialMediaIllustrators.IllustratorID);
            ViewBag.SocialMediaID = new SelectList(db.SocialMedias, "SocialMediaID", "SocialMediaName", socialMediaIllustrators.SocialMediaID);
            return View(socialMediaIllustrators);
        }

        // GET: SocialMediaIllustrators/Edit/5
        public ActionResult Edit(int? userid, int? socid)
        {
            if (userid == null || socid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialMediaIllustrators socialMediaIllustrators = db.SocialMediaIllustrators.Find(userid, socid);
            if (socialMediaIllustrators == null)
            {
                return HttpNotFound();
            }
            //ViewBag.IllustratorID = new SelectList(db.Illustrators, "IllustratorID", "UserId", socialMediaIllustrators.IllustratorID);
            ViewBag.SocialMediaID = new SelectList(db.SocialMedias, "SocialMediaID", "SocialMediaName", socialMediaIllustrators.SocialMediaID);
            return View(socialMediaIllustrators);
        }

        // POST: SocialMediaIllustrators/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IllustratorID,SocialMediaID,IllustratorReference")] SocialMediaIllustrators socialMediaIllustrators)
        {
            if (ModelState.IsValid)
            {
                db.Entry(socialMediaIllustrators).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.IllustratorID = new SelectList(db.Illustrators, "IllustratorID", "UserId", socialMediaIllustrators.IllustratorID);
            ViewBag.SocialMediaID = new SelectList(db.SocialMedias, "SocialMediaID", "SocialMediaName", socialMediaIllustrators.SocialMediaID);
            return View(socialMediaIllustrators);
        }

        // GET: SocialMediaIllustrators/Delete/5
        public ActionResult Delete(int? userid, int? socid)
        {
            if (userid == null || socid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialMediaIllustrators socialMediaIllustrators = db.SocialMediaIllustrators.Find(userid, socid);
            if (socialMediaIllustrators == null)
            {
                return HttpNotFound();
            }
            return View(socialMediaIllustrators);
        }

        // POST: SocialMediaIllustrators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int userid, int socid)
        {
            SocialMediaIllustrators socialMediaIllustrators = db.SocialMediaIllustrators.Find(userid, socid);
            db.SocialMediaIllustrators.Remove(socialMediaIllustrators);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
