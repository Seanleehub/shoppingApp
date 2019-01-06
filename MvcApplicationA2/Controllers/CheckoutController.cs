using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplicationA2.Models;

namespace MvcApplicationA2.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private s3301214Entities db = new s3301214Entities();

        bool set = false;
        // GET: Checkout
        public ActionResult Index()
        {
            string userName = User.Identity.Name;           
            if (!checkIfCartEmpty(userName))
            {
                return RedirectToAction("Index","Home");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Index(OrderDetail order)
        {                  
            if (ModelState.IsValid)
            {
                set = true;
                string userName = User.Identity.Name;
                addPaidView(userName);                
                db.OrderDetails.Add(order);
                db.SaveChanges();
                return RedirectToAction("Completed", "Checkout",
                new { id = order.OrderDetailID });
            }
            return View(order);
        }
             

        public void addPaidView(string userName)
        {
            decimal total = 0;
            var ShoppingCart = db.Carts.Where(
                            c => c.CartId == userName
                            );

            foreach (Cart item in ShoppingCart)
            {
                var paidItem = new PaidOrder
                {
                    CustomerId = item.CartId,
                    ProductTitle = item.Product.Title,
                    Count = item.Count,
                    Price = item.Product.Price
                };
                db.PaidOrders.Add(paidItem);
            }

            total = GetTotal(userName);
            db.SaveChanges();
        }
        public decimal GetTotal(string userName)
        {
            decimal? total = (from c in db.PaidOrders
                              where c.CustomerId == userName
                              select (int?)c.Count * c.Price).Sum();
            return total ?? decimal.Zero;
        }
        public bool checkIfCartEmpty(string userName)
        {
            var shoppingCart = db.Carts.FirstOrDefault(c => c.CartId == userName);
            if (shoppingCart == null)
            {
                return false;
            }
            else 
            {
                return true;
            }
        }
        public List<PaidOrder> GetPaidItems(string userName)
        {
            return db.PaidOrders.Where(c => c.CustomerId == userName).ToList();
        }

        public ActionResult Completed(int? id)
        {
            string userName = User.Identity.Name;
            
            if (checkIfCartEmpty(userName) && id !=null)
            {
                ViewBag.Message = id;
                var viewModel = new PaidView
                {
                    paidItems = GetPaidItems(userName),
                    Total = GetTotal(userName),
                    CustomerName = userName
                };
                var cart = ShoppingCart.GetCart(this.HttpContext);
                cart.EmptyCart();
                return View(viewModel);                
            }
            else
            {
              return RedirectToAction("Index", "Home");               
            }           
        }
    }
}