using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplicationA2.Models;

namespace MvcApplicationA2.Controllers
{
    public class ShoppingCartController : Controller
    {
        s3301214Entities db = new s3301214Entities();

        // GET: ShoppingCart
        public ActionResult Index()
        {            
            var cart = ShoppingCart.GetCart(this.HttpContext);
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };       
            return View(viewModel);
        }

        public ActionResult AddToCart(int id, int Count)
        {
            var addProduct = db.Products.Single(p => p.ProductID == id);
            var cart = ShoppingCart.GetCart(this.HttpContext);     
            cart.AddToCart(addProduct, Count);     
            return RedirectToAction("Index", "Products");
        }

        public ActionResult RemoveFromCart(int id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.RemoveFromCart(id);
            return RedirectToAction("Index");
        }  

    }
}