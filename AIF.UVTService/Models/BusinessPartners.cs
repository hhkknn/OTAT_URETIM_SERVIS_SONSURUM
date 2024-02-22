using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class BusinessPartners
    {
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public List<SAPAddress> Address { get; set; }
        public List<SAPContact> Contacts { get; set; }
    }


    public class SAPAddress
    {
        public string AdresType { get; set; }
        public string Address { get; set; }
        public string Street { get; set; }
        public string Block { get; set; }
        public string ZipCode { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string LineNum { get; set; }
        public string Status { get; set; } //A - ADD - U - UPDATE - D - DELETE
    }

    public class SAPContact
    {
        public int CnctCode { get; set; }
        public string CnctName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string ZipCode { get; set; }
        //public string County { get; set; }
        //public string City { get; set; }
        //public string LineNum { get; set; }
        public string Status { get; set; } //A - ADD - U - UPDATE - D - DELETE
    }
}