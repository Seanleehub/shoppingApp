using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplicationA2.Models
{
    public partial class ShoppingCart
    {
        s3301214Entities db = new s3301214Entities();

        string ShoppingCartId { get; set; }

        public const string CartSessionKey = "CartId";

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Product product, int count)
        {
            var cartItem = db.Carts.FirstOrDefault(
                            c => c.CartId == ShoppingCartId &&
                            product.ProductID == c.ProductID
                            );

            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    ProductID = product.ProductID,
                    CartId = ShoppingCartId,
                    Count = count,
                };
                db.Carts.Add(cartItem);
            }
            else
            {
                cartItem.Count = count;
            }
            db.SaveChanges();
        }

        private object RedirectToAction(string p)
        {
            throw new NotImplementedException();
        }

        public void RemoveFromCart(int id)
        {
            var cartItem = db.Carts.Single(c => c.RecordId == id);

            db.Carts.Remove(cartItem);
            db.SaveChanges();
        }

        public void EmptyCart()
        {
            var cartItems = db.Carts.Where(cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                db.Carts.Remove(cartItem);
            }
            db.SaveChanges();
        }

        public List<Cart> GetCartItems()
        {
            return db.Carts.Where(c => c.CartId == ShoppingCartId).ToList();
        }
        public decimal GetTotal()
        {
            decimal? total = (from cartItems in db.Carts
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count * cartItems.Product.Price).Sum();
            return total ?? decimal.Zero;
        }       

        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }

        public void MigrateCart(string userName)
        {
            var shoppingCart = db.Carts.Where(c => c.CartId == ShoppingCartId);
            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            db.SaveChanges();
        }     
    }
}