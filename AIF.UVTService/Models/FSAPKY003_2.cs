using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class FSAPKY003_2
    {
        public string PartiNo { get; set; }

        public string UrunKodu { get; set; }

        public string UrunTanimi { get; set; }

        public string Aciklama { get; set; }

        public string Tarih { get; set; }

        public string Kontrol1 { get; set; }

        public string Kontrol2 { get; set; }

        public List<FSAPKY003_2_1> fSAPKY003_2_1s { get; set; }

        public List<FSAPKY003_2_2> fSAPKY003_2_2s { get; set; }
    }

    public class FSAPKY003_2_1
    {
        public double KapatmaPH { get; set; }

        public DateTime UretimTarihi { get; set; }

        public DateTime SonTuketimTarihi { get; set; }

        public string PartiNumarasi { get; set; }

        public double DolumSalamuraPH { get; set; }

        public double DolumSalamuraBolme { get; set; }

        public string OperatorAdi { get; set; }
    }

    public class FSAPKY003_2_2
    {
        public double Ornek1 { get; set; }
        public double Ornek2 { get; set; }
        public double Ornek3 { get; set; }
        public double Ornek4 { get; set; }
        public double Ornek5 { get; set; }
    }
}