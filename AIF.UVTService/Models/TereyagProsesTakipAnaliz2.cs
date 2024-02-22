using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class TereyagProsesTakipAnaliz2
    {
        public string PartiNo { get; set; }

        public string KalemKodu { get; set; }

        public string KalemTanimi { get; set; }

        public string Aciklama { get; set; }

        public string UretimTarihi { get; set; }

        public string PaketlemeTarihi { get; set; }

        //public List<TereyagMamul2Ozellikleri1> tereyagMamul2Ozellikleri1_Detay { get; set; }

        //public List<TereyagProsses2DinlendirmeVePaketleme> tereyagProsses2DinlendirmeVePaketlemes_Detay { get; set; }

        public List<Tereyag2SarfMalzemeKullanim> tereyag2SarfMalzemeKullanims_Detay { get; set; }

        //public List<TereyagMamulOzellikleri> tereyag2MamulOzellikleri_Detay { get; set; }

        public List<Tereyag2DedektorGecirilmeKontrol> tereyag2DedektorGecirilmeKontrols { get; set; }

        public List<Tereyag2GramajKontrol> tereyag2GramajKontrols { get; set; }
    }

    //public class TereyagMamul2Ozellikleri1
    //{
    //    public string UretilenUrun { get; set; }

    //    public double PaketlemeOncesiSicaklik { get; set; }

    //    public double UretimMiktari { get; set; }

    //    public double PaketlenenUrunMiktari { get; set; }

    //    public double FireUrunMiktari { get; set; }

    //    public double NumuneUrunMiktari { get; set; }

    //    public double DepoyaGirenUrunMiktari { get; set; }

    //    public double KuruMadde { get; set; }

    //    public double YagOrani { get; set; }

    //    public double PH { get; set; }

    //    public double SH { get; set; }

    //    public double TuzOrani { get; set; }
    //}

    //public class TereyagProsses2DinlendirmeVePaketleme
    //{
    //    public string AlanAdi { get; set; }

    //    public string UretimTarihi { get; set; }

    //    public double SifirSekizSicaklik { get; set; }

    //    public double SifirSekizNem { get; set; }

    //    public double OnikiSicaklik { get; set; }

    //    public double OnikiNem { get; set; }

    //    public double OnBesSicaklik { get; set; }

    //    public double OnBesNem { get; set; }

    //    public double OnSekizSicaklik { get; set; }

    //    public double OnSekizNem { get; set; }
    //}

    public class Tereyag2SarfMalzemeKullanim
    {
        public string MalzemeAdi { get; set; }

        public string MalzemeMarkaTedarikcisi { get; set; }

        public string SarfMalzemePartiNo { get; set; }

        public double Miktar { get; set; }

        public string Birim { get; set; }
    }

    public class Tereyag2DedektorGecirilmeKontrol
    {
        public string UretilenMetalDedektördenGecirilmeKontrolu { get; set; }
    }

    public class Tereyag2GramajKontrol
    {
        public string UrunCesidi { get; set; }

        public string PartiNo { get; set; }

        public double BirinciOrnek { get; set; }

        public double IkinciOrnek { get; set; }

        public double UcuncuOrnek { get; set; }

        public double DorduncuOrnek { get; set; }

        public double BesinciOrnek { get; set; }

        public double AltinciOrnek { get; set; }

        public double YedinciOrnek { get; set; }
    }
}