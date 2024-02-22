using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class Discount
    {
        public string CardCode { get; set; }
        public string ItemCode { get; set; }
        public double PriceBefDisc { get; set; }
        public double TotalDisc { get; set; }
        public double DiscRate { get; set; }
        public double PriceAfterDisc { get; set; }
        public int DiscountNo { get; set; }
        public string DocType { get; set; }
        public string DocEntry { get; set; }
        public List<DiscountDetails> DiscountDetails { get; set; }
    }

    public class DiscountDetails
    {
        public double SubTotal { get; set; }
        public string DiscType { get; set; }
        public double DiscRate { get; set; }
        public double DiscTotal { get; set; }
        public double SubTotal2 { get; set; }
        public int DocLine { get; set; }
    }
}