using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcApplicationA2.Models;


namespace MvcApplicationA2.Models
{
    public class PaidView
    {
        public List<PaidOrder> paidItems { get; set; }
        public decimal Total { get; set; }
        public string CustomerName { get; set; }
    }
}