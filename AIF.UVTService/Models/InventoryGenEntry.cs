using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class InventoryGenEntry
    {
        public int UretimSiparisi { get; set; }
        public double Miktar { get; set; }
        public double FireMiktar { get; set; }
        public double NumuneMiktar { get; set; }
        public string Parti { get; set; }
        public double PartiMiktar { get; set; }
        public string SiraNo { get; set; }
        public string RotaKodu { get; set; }
        public string DepoKodu { get; set; }
        public string UrunKodu { get; set; }
        public string StokNakliHedefDepo { get; set; }
        public double RayicBedel { get; set; }
        public int SKTGun { get; set; }
        public string UretimBaslangicTarihi { get; set; }
        public string TamamlaniyorMu { get; set; }
        public DateTime SiparisTarihi { get; set; }


    }
}