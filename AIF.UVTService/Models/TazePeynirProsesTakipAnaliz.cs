using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class TazePeynirProsesTakipAnaliz
    {
        public string PartiNo { get; set; }

        public string KalemKodu { get; set; }

        public string KalemTanimi { get; set; }

        public string Aciklama { get; set; }

        public string UretimTarihi { get; set; }

        public List<TazePeynirProssesOzellikleri1> tazePeynirProssesOzellikleri1_Detay { get; set; }

        public List<TazePeynirProssesOzellikleri2> tazePeynirProssesOzellikleri2_Detay { get; set; }

        public List<TazePeynirHammaddeMiktarVeOzellik> tazePeynirHammaddeMiktarVeOzellik_Detay { get; set; }

        public List<TazePeynirSarfMalzemeKullanim> tazePeynirSarfMalzemeKullanim_Detay { get; set; }

        public List<TazePeynirMamulOzellikleri> tazePeynirMamulOzellikleri_Detay { get; set; }
    }

    public class TazePeynirProssesOzellikleri1
    {
        public string PartiNo { get; set; }

        //public string YagTuruKodu { get; set; }

        public string HamurTuru { get; set; }

        public string GorevliOperator { get; set; }

        public string GorevliOperatorAdi { get; set; }

        public string PisirmeMakinesindeHammaddeYuklemeBaslangicSaati { get; set; }

        public string PisirmeMakinesindeHammaddeYuklemeBitisSaati { get; set; }

        public double KarisimPastorizasyonSicakligi { get; set; }

        public string PisirmeMakinesiFiltreKontrolu { get; set; }

        public string HamurunPisirmeMakinesindenIndirilmeSaati { get; set; }

        public string HamurunGramajlamaBasSaati { get; set; }

        public string HamurunGramajlamaBitisSaati { get; set; }

    }

    public class TazePeynirProssesOzellikleri2
    {
        public double HammaddeYuklemeSuresiDk { get; set; }

        public double HamurunPismeSuresiDk { get; set; }

        public double HamurunGramajlamaSuresiDk { get; set; }

        public double ToplamGecenSureDk { get; set; }
    }

    public class TazePeynirHammaddeMiktarVeOzellik
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

    public class TazePeynirSarfMalzemeKullanim
    {
        public string MalzemeAdi { get; set; }

        public string MalzemeMarkaTedarikcisi { get; set; }

        public string SarfMalzemePartiNo { get; set; }

        public double Miktar { get; set; }

        public string Birim { get; set; }
    }

    public class TazePeynirMamulOzellikleri
    {
        public double HammaddeVeSarfMalzemeToplami { get; set; }

        public double UretilenUrunMiktariKg { get; set; }

        public string UretilenUrunAdi { get; set; }

        public double KuruMadde { get; set; }

        public double HamurYagOrani { get; set; }

        public double HamurunPHDegeri { get; set; }
        public double UretilenHamur { get; set; }
    }
}