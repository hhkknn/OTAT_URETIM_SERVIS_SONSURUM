using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class KaliteListesi
    {
        public string FormAciklamasi { get; set; }

        public string RaporTarihi { get; set; }

        public int DetayFormBelgeNo { get; set; }

        public List<KaliteListesiDetay> kaliteListesiDetays { get; set; } 
    }

    public class KaliteListesiDetay
    {
        public string IstasyonKodu { get; set; }
        public string IstasyonAdi { get; set; }
        public string Aciklama { get; set; }
        public string Aciklama2 { get; set; }
        public string Tur { get; set; }
        public string UygunUygunDegil { get; set; }
        public string Deger1 { get; set; } 
        public string Deger2 { get; set; } 
        public string Deger3 { get; set; } 
        public string Deger4 { get; set; } 
        public string Deger5 { get; set; } 
        public string Deger6 { get; set; } 
        public string Deger7 { get; set; } 
        public string Deger8 { get; set; } 
        public string Deger9 { get; set; } 
        public string Deger10 { get; set; } 
        public string Deger11 { get; set; } 
        public string Deger12 { get; set; } 
        public string Deger13 { get; set; } 
        public string Deger14 { get; set; } 
        public string Deger15 { get; set; } 
        public string Deger16 { get; set; } 
        public string Deger17 { get; set; } 
        public string Deger18 { get; set; } 
        public string Deger19 { get; set; }
        public string Deger20 { get; set; }
        public string SaatAraligi { get; set; }

        public string KalitePersonelAciklama { get; set; }
    } 
}