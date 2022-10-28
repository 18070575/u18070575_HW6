using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using u18070575_HW6.Models;

namespace u18070575_HW6.Controllers
{
    public class OrderController : Controller
    {
        private BikeStoresEntities1 db = new BikeStoresEntities1();
        public ActionResult Index(int? i, string search)
        {
            List<OrdersVM> productDatas = db.orders.Select(p => new OrdersVM
            {
                OrderId = p.order_id,
                Products = db.order_items.Where(x => x.order_id == p.order_id).Select(o => new { product_name = o.product.product_name, price = o.list_price, Quantity = o.quantity, Total = (o.list_price * o.quantity) }).ToList<dynamic>(),
              
                OrderDate = db.orders.Where(o => o.order_id == p.order_id).FirstOrDefault().order_date
            }).ToList();

            if (!string.IsNullOrEmpty(search))
            {
               
                productDatas = db.orders.Select(p => new OrdersVM
                {
                    OrderId = p.order_id,
                    Products = db.order_items.Where(x => x.product.product_name.Contains(search) && x.order_id == p.order_id).Select(o => new { product_name = o.product.product_name, price = o.list_price, Quantity = o.quantity, Total = (o.list_price * o.quantity) }).ToList<dynamic>(),

                    OrderDate = db.orders.Where(o => o.order_id == p.order_id).FirstOrDefault().order_date
                }).ToList();
            }


            return View(productDatas.ToPagedList(i ?? 1, 10));
        }

        public ActionResult Graph()
        {
            return View();
        }

        public string ChartData()
        {
            

            object orders  = db.orders.Select(p => new 
            {
                
                Products = db.order_items.Where(y => y.order_id == p.order_id).Select(o => new { category = o.product.category.category_name, Quantity = o.quantity, month = p.order_date.Month }).ToList<dynamic>(),
                
            }).ToList();

            return JsonConvert.SerializeObject(orders);
        }

    }
}