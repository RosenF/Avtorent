﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext db = new AppDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {
            Users admin = new Users
            {
                Username = "admin",
                Password = "admin",
                Role = Models.Roles.Admin,
                Name = "admin",
                LastName = "admin",
                Adress = "admin №12",
                PhoneNumber = "0888329430",
                Email = "admin@gmail.com",
                SertificateNumber = "testadmin",
                RegisterDate = DateTime.Now
    };
            Users user = new Users
            {
                Username = "user",
                Password = "user",
                Role = Models.Roles.User,
                Name = "user",
                LastName = "user",
                Adress = "user №12",
                PhoneNumber = "0888329430",
                Email = "user@gmail.com",
                SertificateNumber = "testuser",
                RegisterDate = DateTime.Now
            };
            List<Users> users = new List<Users>() { admin, user };


            foreach (var item in users)
            {
                if (!db.Users.Where(x => x.Username == item.Username && x.Password == item.Password).Any())
                {
                    db.Users.Add(item);
                }
            }
            db.SaveChanges();

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users model, string returnUrl)
        {
            var dataItem = db.Users.Where(x => x.Username == model.Username && x.Password == model.Password).FirstOrDefault();
            if (dataItem != null)
            {
                FormsAuthentication.SetAuthCookie(model.Username, false);

                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                   && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {

                    return RedirectToLocal(returnUrl);
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid user/pass");
                return View(model);
            }


        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [Authorize]

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Users model, string confirmpassword)
        {
            
            if (ModelState.IsValid && model.Password == confirmpassword)
            {
                model.Role = Models.Roles.User;
                model.RegisterDate = DateTime.Now;
                db.Users.Add(model);
                db.SaveChanges();
                FormsAuthentication.SetAuthCookie(model.Username, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid user/pass");
                return View(model);
            }
        }
    }
}