using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class PurchaseDeliveryNotes
    {
        public string CardCode { get; set; }

        public string DocDate { get; set; }

        public int BPLId { get; set; }

        public int sutKabulId { get; set; }
        public int userId { get; set; }

        public List<PurchaseDeliveryNotesDetail> Lines = new List<PurchaseDeliveryNotesDetail>();
    }

    public class PurchaseDeliveryNotesDetail
    {
        public string ItemCode { get; set; }

        public double Quantity { get; set; }

        public string WareHouse { get; set; }

        public List<PurchaseDeliveryNotesDetailParti> PartiDetails = new List<PurchaseDeliveryNotesDetailParti>();
    }

    public class PurchaseDeliveryNotesDetailParti
    {
        public string BatchNumber { get; set; }

        public double Quantity { get; set; }
    }
}