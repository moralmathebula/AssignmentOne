using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssignmentOne.Models;

namespace AssignmentOne.Controllers
{
    public class BenefitController : Controller
    {
        private AssignmentsEntities db = new AssignmentsEntities();

        // GET: Benefit
        public ActionResult Index()
        {
            var benefits = db.Benefits.Include(b => b.Category).Include(b => b.Duration);
            return View(benefits.ToList());
        }

       

        // GET: Benefit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Benefit benefit = db.Benefits.Find(id);
            if (benefit == null)
            {
                return HttpNotFound();
            }
            return View(benefit);
        }

        // GET: Benefit/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "MembershipName");
            ViewBag.DurationId = new SelectList(db.Durations, "DurationId", "DurationName");
            return View();
        }

        // POST: Benefit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BenefitId,CategoryId,MemberPrefixCode,CategoryType,DurationId,Fees,CreditLimit,Max_Adult,MaxChild")] Benefit benefit)
        {
            if (ModelState.IsValid)
            {
                db.Benefits.Add(benefit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "MembershipName", benefit.CategoryId);
            ViewBag.DurationId = new SelectList(db.Durations, "DurationId", "DurationName", benefit.DurationId);
            return View(benefit);
        }

        // GET: Benefit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Benefit benefit = db.Benefits.Find(id);
            if (benefit == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "MembershipName", benefit.CategoryId);
            ViewBag.DurationId = new SelectList(db.Durations, "DurationId", "DurationName", benefit.DurationId);
            return View(benefit);
        }

        // POST: Benefit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BenefitId,CategoryId,MemberPrefixCode,CategoryType,DurationId,Fees,CreditLimit,Max_Adult,MaxChild")] Benefit benefit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(benefit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "MembershipName", benefit.CategoryId);
            ViewBag.DurationId = new SelectList(db.Durations, "DurationId", "DurationName", benefit.DurationId);
            return View(benefit);
        }

        // GET: Benefit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Benefit benefit = db.Benefits.Find(id);
            if (benefit == null)
            {
                return HttpNotFound();
            }
            return View(benefit);
        }

        // POST: Benefit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Benefit benefit = db.Benefits.Find(id);
            db.Benefits.Remove(benefit);
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
