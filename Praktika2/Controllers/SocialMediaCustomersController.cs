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
    public class SocialMediaCustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SocialMediaCustomers
        public ActionResult Index()
        {
            var socialMediaCustomers = db.SocialMediaCustomers.Include(s => s.Customers).Include(s => s.SocialMedias);
            return View(socialMediaCustomers.ToList());
        }

        // GET: SocialMediaCustomers/Details/5
        public ActionResult Details(int? userid, int? socid)
        {
            if (userid == null || socid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialMediaCustomers socialMediaCustomers = db.SocialMediaCustomers.Find(userid, socid);
            if (socialMediaCustomers == null)
            {
                return HttpNotFound();
            }
            return View(socialMediaCustomers);
        }

        // GET: SocialMediaCustomers/Create
        public ActionResult Create()
        {
            //ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "UserId");
            ViewBag.SocialMediaID = new SelectList(db.SocialMedias, "SocialMediaID", "SocialMediaName");
            return View();
        }

        // POST: SocialMediaCustomers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,SocialMediaID,SocialMediaReference")] SocialMediaCustomers socialMediaCustomers)
        {
            string userId = User.Identity.GetUserId();
            var customer = db.Customers.Where(c => userId == c.UserId.ToString()).ToList();
            socialMediaCustomers.CustomerID = customer[0].CustomerID;
            if (ModelState.IsValid)
            {
                db.SocialMediaCustomers.Add(socialMediaCustomers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "UserId", socialMediaCustomers.CustomerID);
            ViewBag.SocialMediaID = new SelectList(db.SocialMedias, "SocialMediaID", "SocialMediaName", socialMediaCustomers.SocialMediaID);
            return View(socialMediaCustomers);
        }

        // GET: SocialMediaCustomers/Edit/5
        public ActionResult Edit(int? userid, int? socid)
        {
            if (userid == null || socid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialMediaCustomers socialMediaCustomers = db.SocialMediaCustomers.Find(userid, socid);
            if (socialMediaCustomers == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "UserId", socialMediaCustomers.CustomerID);
            ViewBag.SocialMediaID = new SelectList(db.SocialMedias, "SocialMediaID", "SocialMediaName", socialMediaCustomers.SocialMediaID);
            return View(socialMediaCustomers);
        }

        // POST: SocialMediaCustomers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,SocialMediaID,SocialMediaReference")] SocialMediaCustomers socialMediaCustomers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(socialMediaCustomers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "UserId", socialMediaCustomers.CustomerID);
            ViewBag.SocialMediaID = new SelectList(db.SocialMedias, "SocialMediaID", "SocialMediaName", socialMediaCustomers.SocialMediaID);
            return View(socialMediaCustomers);
        }

        // GET: SocialMediaCustomers/Delete/5
        public ActionResult Delete(int? userid, int? socid)
        {
            if (userid == null || socid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialMediaCustomers socialMediaCustomers = db.SocialMediaCustomers.Find(userid, socid);
            if (socialMediaCustomers == null)
            {
                return HttpNotFound();
            }
            return View(socialMediaCustomers);
        }

        // POST: SocialMediaCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int userid, int socid)
        {
            SocialMediaCustomers socialMediaCustomers = db.SocialMediaCustomers.Find(userid, socid);
            db.SocialMediaCustomers.Remove(socialMediaCustomers);
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
