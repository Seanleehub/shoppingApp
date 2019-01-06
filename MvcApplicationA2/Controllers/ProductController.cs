using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcApplicationA2.Models;
using System.ComponentModel.DataAnnotations;

namespace MvcApplicationA2.Controllers
{
    public class ProductsController : Controller
    {
        private s3301214Entities db = new s3301214Entities();

        // GET: Products

        public ActionResult Index(int? ddlcategory)
        {
            ViewBag.ddlcategory = new SelectList(db.Categories, "CategoryID", "Title");

            var product = from m in db.Products
                          select m;
            if (ddlcategory != null)
            {
                product = product.Where(x => x.CategoryID == ddlcategory);
            }

            return View(product);
        }


        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        
        public ActionResult EnterAmount(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index","Products");
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);

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
