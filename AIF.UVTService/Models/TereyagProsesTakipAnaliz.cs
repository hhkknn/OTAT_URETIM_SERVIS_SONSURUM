using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class TereyagProsesTakipAnaliz
    {
        public string PartiNo { get; set; }

        public string KalemKodu { get; set; }

        public string KalemTanimi { get; set; }

        public string Aciklama { get; set; }

        public string UretimTarihi { get; set; }

        public List<TereyagProssesOzellikleri1> tereyagProssesOzellikleri1_Detay { get; set; }

        public List<TereyagProssesOzellikleri2> tereyagProssesOzellikleri2_Detay { get; set; }

        public List<TereyagHammaddeMiktarVeOzellik> tereyagHammaddeMiktarVeOzellik_Detay { get; set; }

        public List<TereyagSarfMalzemeKullanim> tereyagSarfMalzemeKullanim_Detay { get; set; }

        public List<TereyagMamulOzellikleri> tereyagMamulOzellikleri_Detay { get; set; }
    }

    public class TereyagProssesOzellikleri1
    {
        public string PartiNo { get; set; }

        public string YagTuruKodu { get; set; }

        public string YagTuru { get; set; }

        public string GorevliOperator { get; set; }

        public string GorevliOperatorAdi { get; set; }

        public double KremaPastorizasyonSicakligi { get; set; }

        public string YayigaKremaYuklemeBaslamaSaati { get; set; }

        public string YayigaKremaYuklemeBitisSaati { get; set; }

        public string YagOlusmaSaati { get; set; }

        public int YikamaSayisi { get; set; }

        public string YikamaBitisSaati { get; set; }

        public string YaginYayiktanIndirilmeSaati { get; set; }

        public string YaginGramajBaslamaSaati { get; set; }

        public string YaginGramajlamaBitSaati { get; set; }

    }

    public class TereyagProssesOzellikleri2
    {
        public double KremaYuklemeSuresiDk { get; set; }

        public double YayiklamaSuresiDk { get; set; }

        public double YikamaSuresiDk { get; set; }

        public double YaginGramajlamaSuresiDk { get; set; }

        public double ToplamGecenSureDk { get; set; }
    }

    public class TereyagHammaddeMiktarVeOzellik
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

    public class TereyagSarfMalzemeKullanim
    {
        public string MalzemeAdi { get; set; }

        public string MalzemeMarkaTedarikcisi { get; set; }

        public string SarfMalzemePartiNo { get; set; }

        public double Miktar { get; set; }

        public string Birim { get; set; }
    }

    public class TereyagMamulOzellikleri
    {
        public double HammaddeVeSarfMalzemeToplami { get; set; }

        public double UretilenUrunMiktariKg { get; set; }

        public string UretilenUrunAdi { get; set; }

        public double KuruMadde { get; set; }

        public double YagOrani { get; set; }

        public double YayikAltiSuyuYagOrani { get; set; }

        public double YaginPHDegeri { get; set; }
        public double UretilenHamur { get; set; }
    }
}