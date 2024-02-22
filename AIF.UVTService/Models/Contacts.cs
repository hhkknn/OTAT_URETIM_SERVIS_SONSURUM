using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class Contacts
    {
        public string ContactType { get; set; }
        public string ContactSubType { get; set; }
        public string ContactId { get; set; }
        public string Status { get; set; }
        public string RotaKodu { get; set; }
        public string PartiNo { get; set; }
        public string ClgCode { get; set; }
        public string Closed { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string StartDate { get; set; }
        public string UserId { get; set; }
    }
}