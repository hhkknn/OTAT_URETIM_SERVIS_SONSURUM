using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class TelemeMamulTakipAnaliz
    {
        public int DocEntry { get; set; }

        public string PartiNo { get; set; }

        public string Aciklama { get; set; }

        public string UretimTarihi { get; set; }

        public List<MamulOzellikleri> mamulOzellikleriDetay { get; set; }

        public List<MamulOzellikleri1> mamulOzellikleri1Detay { get; set; }

    }

    public class TelemeMamulOzellikleri
    {
        public double UretimSonrasiTelemeMiktari { get; set; }

        public double BirGunSonraTelemeMiktari { get; set; }

        public double UretimRandimani { get; set; }

        public string PersonelKodu { get; set; }

        public string PersonelAdi { get; set; }
    }

    public class TelemeMamulOzellikleri1
    {

        public double KuruMadde { get; set; }

        public double YagOrani { get; set; }

        public double PH { get; set; }

        public double SH { get; set; }

        public double TuzOrani { get; set; }

    }
}