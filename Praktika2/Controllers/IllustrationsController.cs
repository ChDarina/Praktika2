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
    public class IllustrationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Illustrations
        public ActionResult Index()
        {
            var illustrations = db.Illustrations.Include(i => i.Illustrators);
            return View(illustrations.ToList());
        }

        // GET: Illustrations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Illustrations illustrations = db.Illustrations.Find(id);
            if (illustrations == null)
            {
                return HttpNotFound();
            }
            return View(illustrations);
        }

        // GET: Illustrations/Create
        public ActionResult Create()
        {
            //ViewBag.IllustratorID = new SelectList(db.Illustrators, "IllustratorID", "IllustratorNickname");
            return View();
        }

        // POST: Illustrations/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IllustrationID,IllustratorID,Name,DLDate,Privacy,Hiding")] Illustrations illustrations)
        {
            string userId = User.Identity.GetUserId();
            var illustrator = db.Illustrators.Where(c => userId == c.UserId.ToString()).ToList();
            illustrations.IllustratorID = illustrator[0].IllustratorID;
            illustrations.DLDate = DateTime.Today;
            if (ModelState.IsValid)
            {
                db.Illustrations.Add(illustrations);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.IllustratorID = new SelectList(db.Illustrators, "IllustratorID", "IllustratorNickname", illustrations.IllustratorID);
            return View(illustrations);
        }

        // GET: Illustrations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Illustrations illustrations = db.Illustrations.Find(id);
            if (illustrations == null)
            {
                return HttpNotFound();
            }
            //ViewBag.IllustratorID = new SelectList(db.Illustrators, "IllustratorID", "IllustratorNickname", illustrations.IllustratorID);
            return View(illustrations);
        }

        // POST: Illustrations/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IllustrationID,IllustratorID,Name,DLDate,Privacy,Hiding")] Illustrations illustrations)
        {
            if (ModelState.IsValid)
            {
                db.Entry(illustrations).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.IllustratorID = new SelectList(db.Illustrators, "IllustratorID", "IllustratorNickname", illustrations.IllustratorID);
            return View(illustrations);
        }

        // GET: Illustrations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Illustrations illustrations = db.Illustrations.Find(id);
            if (illustrations == null)
            {
                return HttpNotFound();
            }
            return View(illustrations);
        }

        // POST: Illustrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Illustrations illustrations = db.Illustrations.Find(id);
            db.Illustrations.Remove(illustrations);
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
