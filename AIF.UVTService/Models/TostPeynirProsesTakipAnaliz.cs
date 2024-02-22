using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class TostPeynirProsesTakipAnaliz
    {
        public string PartiNo { get; set; }

        public string KalemKodu { get; set; }

        public string KalemTanimi { get; set; }

        public string Aciklama { get; set; }

        public string UretimTarihi { get; set; }

        public List<TostPeynirProssesOzellikleri1> tostPeynirProssesOzellikleri1_Detay { get; set; }

        public List<TostPeynirProssesOzellikleri2> tostPeynirProssesOzellikleri2_Detay { get; set; }

        public List<TostPeynirHammaddeMiktarVeOzellik> tostPeynirHammaddeMiktarVeOzellik_Detay { get; set; }

        public List<TostPeynirSarfMalzemeKullanim> tostPeynirSarfMalzemeKullanim_Detay { get; set; }

        public List<TostPeynirMamulOzellikleri> tostPeynirMamulOzellikleri_Detay { get; set; }
    }

    public class TostPeynirProssesOzellikleri1
    {
        public string PartiNo { get; set; }

        //public string YagTuruKodu { get; set; }

        public string HamurTuru { get; set; }

        public string GorevliOperator { get; set; }

        public string GorevliOperatorAdi { get; set; }

        public string PisirmeMakinesindeHammaddeYuklemeBaslangicSaati { get; set; }

        public string PisirmeMakinesindeHammaddeYuklemeBitisSaati { get; set; }

        public double HamurPastorizasyonSicakligi { get; set; }

        public double VakumSuresi { get; set; }

        public string HamurunPisirmeMakinesindenIndirilmeSaati { get; set; }

        public string HamurunGramajlamaBitisSaati { get; set; }

    }

    public class TostPeynirProssesOzellikleri2
    {
        public double HammaddeYuklemeSuresiDk { get; set; }

        public double HamurunPismeSuresiDk { get; set; }

        public double VakumSuresi { get; set; }

        public double HamurunGramajlamaSuresiDk { get; set; }

        public double ToplamGecenSureDk { get; set; }
    }

    public class TostPeynirHammaddeMiktarVeOzellik
    {
        public string MalzemeAdi { get; set; }

        public double KuruMadde { get; set; }

        public double YagOrani { get; set; }

        public double PH { get; set; }

        public double SH { get; set; }

        public string HammaddePartiNo { get; set; }

        public double Miktar { get; set; }

        public string Birim { get; set; }
    }

    public class TostPeynirSarfMalzemeKullanim
    {
        public string MalzemeAdi { get; set; }

        public string MalzemeMarkaTedarikcisi { get; set; }

        public string SarfMalzemePartiNo { get; set; }

        public double Miktar { get; set; }

        public string Birim { get; set; }
    }

    public class TostPeynirMamulOzellikleri
    {
        public double HammaddeVeSarfMalzemeToplami { get; set; }

        public double UretilenUrunMiktariKg { get; set; }

        public string UretilenUrunAdi { get; set; }

        public double KuruMadde { get; set; }

        public double HamurYagOrani { get; set; }

        public double HamurunPHDegeri { get; set; }
        public double KuruMaddeYagOrani { get; set; }
        public double UretilenHamur { get; set; }
    }
}