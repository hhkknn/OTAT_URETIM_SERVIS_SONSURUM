using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class FSAPKY002_2
    {
        public string PartiNo { get; set; }

        public string UrunKodu { get; set; }

        public string UrunTanimi { get; set; }

        public string Aciklama { get; set; }

        public string Tarih { get; set; }

        public string Kontrol1 { get; set; }

        public string Kontrol2 { get; set; }

        public List<FSAPKY002_2_1> fSAPKY002_2_1s { get; set; }
        public List<FSAPKY002_2_2> fSAPKY002_2_2s { get; set; }
    }
     
    public class FSAPKY002_2_1
    {
        public string TelemeProsesi { get; set; }

        public double TelemeMiktari { get; set; }

        public string TelemeProsesi2 { get; set; }

        public double TelemeMiktari2 { get; set; }

        public double HaslamaBaslangicPH { get; set; }

        public string BaslangicSaati { get; set; }

        public string BitisSaati { get; set; }

        public string HaslamaOperatoru { get; set; } 
    }

    public class FSAPKY002_2_2
    {
        public string UrunKodu { get; set; }
        public string UrunAdi { get; set; }
        public double Ornek1 { get; set; } 
        public double Ornek2 { get; set; } 
        public double Ornek3 { get; set; } 
        public double Ornek4 { get; set; } 
        public double Ornek5 { get; set; } 
        public double Ornek6 { get; set; } 
        public double Ornek7 { get; set; }  
        public string GramajOlcumOprt { get; set; }
    }

}