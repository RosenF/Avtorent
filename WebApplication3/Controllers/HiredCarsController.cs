using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HiredCarsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: HiredCars
        public async Task<ActionResult> Index()
        {
            var hiredCars = db.HiredCars.Include(h => h.Users);
            return View(await hiredCars.ToListAsync());
        }

        // GET: HiredCars/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HiredCars hiredCars = await db.HiredCars.FindAsync(id);
            if (hiredCars == null)
            {
                return HttpNotFound();
            }
            return View(hiredCars);
        }

        // GET: HiredCars/Create
        public ActionResult Create()
        {
            ViewBag.UsersId = new SelectList(db.Users, "Id", "Username");
            return View();
        }

        // POST: HiredCars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,DateStart,DateEnd,CarId,UsersId")] HiredCars hiredCars)
        {
            if (ModelState.IsValid)
            {
                db.HiredCars.Add(hiredCars);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.UsersId = new SelectList(db.Users, "Id", "Username", hiredCars.UsersId);
            return View(hiredCars);
        }

        // GET: HiredCars/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HiredCars hiredCars = await db.HiredCars.FindAsync(id);
            if (hiredCars == null)
            {
                return HttpNotFound();
            }
            ViewBag.UsersId = new SelectList(db.Users, "Id", "Username", hiredCars.UsersId);
            return View(hiredCars);
        }

        // POST: HiredCars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,DateStart,DateEnd,CarId,UsersId")] HiredCars hiredCars)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hiredCars).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UsersId = new SelectList(db.Users, "Id", "Username", hiredCars.UsersId);
            return View(hiredCars);
        }

        // GET: HiredCars/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HiredCars hiredCars = await db.HiredCars.FindAsync(id);
            if (hiredCars == null)
            {
                return HttpNotFound();
            }
            return View(hiredCars);
        }

        // POST: HiredCars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HiredCars hiredCars = await db.HiredCars.FindAsync(id);
            db.HiredCars.Remove(hiredCars);
            await db.SaveChangesAsync();
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
