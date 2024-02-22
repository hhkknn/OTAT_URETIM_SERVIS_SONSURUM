using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class Orders
    {
        public string DocEntry { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string DocDate { get; set; }
        public string DocDueDate { get; set; }
        public List<OrdersDetail> Lines { get; set; }
    }


    public class OrdersDetail
    {
        public string ItemCode { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double DiscountPercent { get; set; }
        public int DiscountNo { get; set; }
    }
}