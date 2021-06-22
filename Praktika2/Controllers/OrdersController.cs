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
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Customers).Include(o => o.Illustrations).Include(o => o.Illustrators);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.IllustratorID = new SelectList(db.Illustrators, "IllustratorID", "IllustratorNickname");
            return View();
        }

        // POST: Orders/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,IllustratorID,CustomerID,Commentary,Feedback,OrderStatus,Price,OrderDate")] Orders orders)
        {
            string userId = User.Identity.GetUserId();
            var customer = db.Customers.Where(c => userId == c.UserId.ToString()).ToList();
            orders.CustomerID = customer[0].CustomerID;
            orders.OrderDate = DateTime.Today;
            if (ModelState.IsValid)
            {
                db.Orders.Add(orders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerNickname", orders.CustomerID);
            //ViewBag.IllustratorID = new SelectList(db.Illustrators, "IllustratorID", "IllustratorNickname", orders.IllustratorID);
            return View(orders);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerNickname", orders.CustomerID);
            ViewBag.IllustrationID = new SelectList(db.Illustrations, "IllustrationID", "Name", orders.IllustrationID);
            //ViewBag.IllustratorID = new SelectList(db.Illustrators, "IllustratorID", "IllustratorNickname", orders.IllustratorID);
            return View(orders);
        }

        // POST: Orders/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,IllustratorID,CustomerID,IllustrationID,Commentary,Feedback,OrderStatus,Price,OrderDate")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerNickname", orders.CustomerID);
            //ViewBag.IllustrationID = new SelectList(db.Illustrations, "IllustrationID", "Name", orders.IllustrationID);
            //ViewBag.IllustratorID = new SelectList(db.Illustrators, "IllustratorID", "IllustratorNickname", orders.IllustratorID);
            return View(orders);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orders orders = db.Orders.Find(id);
            db.Orders.Remove(orders);
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
