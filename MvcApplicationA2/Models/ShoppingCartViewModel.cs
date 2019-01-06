using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcApplicationA2.Models;


namespace MvcApplicationA2.Models
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}