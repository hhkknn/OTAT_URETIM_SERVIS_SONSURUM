using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class ProductionOrder
    {
        public string ItemCode { get; set; }
        public double Miktar { get; set; }
        public string Depo { get; set; }
    }
}