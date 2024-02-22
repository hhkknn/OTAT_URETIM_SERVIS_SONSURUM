using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class YogurtMamulAnaliz
    {
        public string PartiNo { get; set; }

        public string UrunKodu { get; set; }

        public string UrunTanimi { get; set; }

        public string Aciklama { get; set; }

        public string UretimTarihi { get; set; } 
        public List<YogurtMamulInkubasyon> YogurtMamulInkubasyons { get; set; }
        public List<YogurtMamulGramajKontrol> YogurtMamulGramajKontrols { get; set; }

    }

    public class YogurtMamulInkubasyon
    {
        public string KontrolNo { get; set; }

        public string Saat { get; set; }

        public double UrunSicakligi { get; set; }

        public double PH { get; set; }

        public double OdaSicakligi { get; set; }
        public string KontrolEdenPersonel { get; set; }
    }
    public class YogurtMamulGramajKontrol
    {
        public double Ornek1 { get; set; }
        public double Ornek2 { get; set; }
        public double Ornek3 { get; set; }
        public double Ornek4 { get; set; }
        public double Ornek5 { get; set; }
        public double Ornek6 { get; set; }
        public double Ornek7 { get; set; }
        public double Ornek8 { get; set; }
        public double Ornek9 { get; set; }
        public double Ornek10 { get; set; }
        public double Ornek11 { get; set; }
        public double Ornek12 { get; set; }
        public double Ornek13 { get; set; }
        public double Ornek14 { get; set; }
        public double Ornek15 { get; set; }
    }
}