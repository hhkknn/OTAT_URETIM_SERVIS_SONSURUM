using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class RotaDurum
    {
        public int UretimFisNo { get; set; }

        public string RotaKodu { get; set; }

        public string PartiNo { get; set; }

        public string DurumKodu { get; set; }

        public string DurumAciklamasi { get; set; }
    }
}