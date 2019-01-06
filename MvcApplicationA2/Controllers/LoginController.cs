using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplicationA2.Models;
using System.Web.Security;

namespace MvcApplicationA2.Controllers
{
    public class LoginController : Controller
    {
        private s3301214Entities db = new s3301214Entities();

        // GET: Login

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Login user)
        {
            var v = db.Logins.Where(a => a.UserName.Equals(user.UserName)
                    && a.PassWord.Equals(user.PassWord)).FirstOrDefault();

            if (v != null)
            {
                MigrateShoppingCart(v.UserName);
                FormsAuthentication.SetAuthCookie(v.UserName, false);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Message = "Incorrect Username and Password!";
            return View(user);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }    

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Login user)
        {
            if (ModelState.IsValid)
            {
                MigrateShoppingCart(user.UserName);
                FormsAuthentication.SetAuthCookie(user.UserName, false);
                db.Logins.Add(user);
                db.SaveChanges();
                ModelState.Clear();
                user = null;
                ViewBag.Message = "Success Registration Done!";
            }
            return RedirectToAction("Index", "Home");
        }

        private void MigrateShoppingCart(string UserName)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);             
            cart.MigrateCart(UserName);
            Session[ShoppingCart.CartSessionKey] = UserName;
        }
    }
}