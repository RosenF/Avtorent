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
    public class CarsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Cars
        public async Task<ActionResult> Index()
        {
            return View(await db.Cars.ToListAsync());
        }

        // GET: Cars/Details/5
        public async Task<ActionResult> HireCars(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cars d = await db.Cars.FindAsync(id);
            HiredCars model = new HiredCars();
            if (d == null)
            {
                return HttpNotFound();
            }
            model.CarId =Convert.ToInt32(id);

            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> HireCarCreate(HiredCars model)
        {
            model.UsersId = db.Users.Where(x => x.Username == ControllerContext.HttpContext.User.Identity.Name).FirstOrDefault().Id;
            db.HiredCars.Add(model);
            await db.SaveChangesAsync();
            return RedirectToAction("Index","HiredCars");
        }


        // GET: Cars/Create 
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,RegistrationNumber,EngineNumber,Brand,Model,SeatsNumber,Power,CreationYear,Color,Price")] Cars cars)
        {
            if (ModelState.IsValid)
            {
                db.Cars.Add(cars);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cars);
        }

        // GET: Cars/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cars cars = await db.Cars.FindAsync(id);
            if (cars == null)
            {
                return HttpNotFound();
            }
            return View(cars);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,RegistrationNumber,EngineNumber,Brand,Model,SeatsNumber,Power,CreationYear,Color,Price")] Cars cars)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cars).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cars);
        }

        // GET: Cars/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cars cars = await db.Cars.FindAsync(id);
            if (cars == null)
            {
                return HttpNotFound();
            }
            return View(cars);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Cars cars = await db.Cars.FindAsync(id);
            db.Cars.Remove(cars);
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
