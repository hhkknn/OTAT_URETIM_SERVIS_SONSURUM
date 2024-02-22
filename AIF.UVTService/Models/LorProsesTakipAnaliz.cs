using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class LorProsesTakipAnaliz
    {
        public string PartiNo { get; set; }

        public string UrunKodu { get; set; }

        public string UrunTanimi { get; set; }

        public string Aciklama { get; set; }

        public List<LorProsesOzellikleri1> LorProsesOzellikleri1s { get; set; }

        public List<LorProsesOzellikleri2> LorProsesOzellikleri2s { get; set; }

        public List<LorMamulOzellikleri1> LorMamulOzellikleri1s { get; set; }

        public List<LorMamulOzellikleri2> LorMamulOzellikleri2s { get; set; }

        public List<LorSarfMalzemeKullanim> LorSarfMalzemeKullanims { get; set; }

    }

    public class LorProsesOzellikleri1
    {
        public string PartiNo { get; set; }

        public string LorTuru { get; set; }

        public string OperatorAdi { get; set; }

        public string PasGonderimBaslangicSaati { get; set; }

        public string PasGonderimBitisSaati { get; set; }

        public double NetPasMiktari { get; set; }

        public double PasPastorizasyonSicakligi { get; set; }

        public string EsanjorTankFiltreVeKontrol { get; set; }

        public double PasinTankaGelisSicakligi { get; set; }

        public double PasinPHDegeri { get; set; }

        public string PasinSeksenDereceyeGelmeSaati { get; set; }

        public string PasinSeksenSekizDereceyeGelmeSaati { get; set; }

        public string PasinTanktanBosaltilmaBaslangicSaati { get; set; }

        public string PasinTanktanBosaltilmaBitisSaati { get; set; }

        public string BaskiBaslangicSaati { get; set; }

        public string BaskiBitisSaati { get; set; }

    }

    public class LorProsesOzellikleri2
    {
        public double PasinGonderimSuresi { get; set; }

        public double PasinSeksenSekizDereceyeGelmeSuresi { get; set; }

        public double PeynirinIndirilmeSuresi { get; set; }

        public double BaskiSuresi { get; set; }

        public double ToplamGecenSure { get; set; }

    }

    public class LorSarfMalzemeKullanim
    {
        public string MalzemeAdi { get; set; }

        public string MalzemeMarkaTedarikcisi { get; set; }

        public string PartiNo { get; set; }

        public double Miktar { get; set; }

        public string Birim { get; set; }
    }
    public class LorMamulOzellikleri1
    {
        public double LorPeynirMiktari { get; set; }

        public double LorUretimRandimani { get; set; }

        public string KontrolEdenPersonel { get; set; }
    }

    public class LorMamulOzellikleri2
    {
        public double KuruMadde { get; set; }

        public double YagOrani { get; set; }

        public double PH { get; set; }

        public double SH { get; set; }

        public double TuzOrani { get; set; }
    }
}