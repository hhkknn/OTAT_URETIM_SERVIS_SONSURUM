using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class TereyagGunlukAnaliz
    { 

        public string Aciklama { get; set; }

        public string UretimTarihi { get; set; }

        public string PaketlemeTarihi { get; set; }

        public List<TereyagGunlukMamulOzellik> tereyagGunlukMamulOzelliks { get; set; }

        public List<TereyagGunlukDinlendirmeVePaketleme> tereyagGunlukDinlendirmeVePaketlemes { get; set; } 
    }

    public class TereyagGunlukMamulOzellik
    {
        public string UretilenUrun { get; set; }

        public double PaketlemeOncesiSicaklik { get; set; }

        public double UretimMiktari { get; set; }

        public double PaketlenenUrunMiktari { get; set; }

        public double FireUrunMiktari { get; set; }

        public double NumuneUrunMiktari { get; set; }

        public double DepoyaGirenUrunMiktari { get; set; }

        public double KuruMadde { get; set; }

        public double YagOrani { get; set; }

        public double PH { get; set; }

        public double SH { get; set; }

        public double TuzOrani { get; set; }
    }

    public class TereyagGunlukDinlendirmeVePaketleme
    {
        public string AlanAdi { get; set; }

        public string UretimTarihi { get; set; }

        public double SifirSekizSicaklik { get; set; }

        public double SifirSekizNem { get; set; }

        public double OnikiSicaklik { get; set; }

        public double OnikiNem { get; set; }

        public double OnBesSicaklik { get; set; }

        public double OnBesNem { get; set; }

        public double OnSekizSicaklik { get; set; }

        public double OnSekizNem { get; set; }
    } 
}