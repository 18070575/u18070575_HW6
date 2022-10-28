using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u18070575_HW6.Models
{
    public class OrdersVM
    {
        public int OrderId { get; set; }
        public List<object> Products { get; set; }
        public decimal? GrandTotal { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }
       
    }
}