using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class YogurtProsesTakipAnaliz
    {
        public string PartiNo { get; set; }

        public string UrunKodu { get; set; }

        public string UrunTanimi { get; set; }

        public string Aciklama { get; set; }

        public string UretimTarihi { get; set; }

        public List<YogurtProsesOzellikleri1> YogurtProsesOzellikleri1s { get; set; }

        public List<YogurtProsesOzellikleri2> YogurtProsesOzellikleri2s { get; set; }

        public List<YogurtProsesOzellikleri3> YogurtProsesOzellikleri3s { get; set; }

        public List<YogurtBirinciKultur> YogurtBirinciKulturs { get; set; }

        public List<YogurtIkinciKultur> YogurtIkinciKulturs { get; set; }

        public List<YogurtTuzOzellikleri> YogurtTuzOzellikleris { get; set; }

        public List<YogurtSutOzellikleri> YogurtSutOzellikleris { get; set; }

        public List<YogurtYariMamulKaliteOzellikleri> YogurtYariMamulKaliteOzellikleris { get; set; }

        public List<YogurtInkubasyonOzellikleri> YogurtInkubasyonOzellikleris { get; set; }

    }

    public class YogurtProsesOzellikleri1
    {
        public string UrunGrubu { get; set; }

        public string InkubasyonNo { get; set; }

        public string ProsesBaslangicSaati { get; set; }

        public double YagliSutMiktari { get; set; }

        public double YagsizSutMiktari { get; set; }

        public double KullanilanKremaMiktari { get; set; }

    }

    public class YogurtProsesOzellikleri2
    {
        public string VakumBaslangicSaati { get; set; }

        public string VakumBitisSaati { get; set; }

        public double VakumSonuSutBrixDegeri { get; set; }

        public double PastorizasyonSicakligi { get; set; }

        public double PastorizasyonSuresi { get; set; }

        public string MayalamaSaati { get; set; }

        public double MayalamaPHDegeri { get; set; }

        public double MayalamaSicakligi { get; set; }

    }

    public class YogurtProsesOzellikleri3
    {
        public string DolumBaslangicSaati { get; set; }

        public double DolumBaslangicindakiPHDegeri { get; set; }

        public double DolumBaslangicindakiSutSicakligi { get; set; }

        public string DolumBitisSaati { get; set; }

        public double DolumBittigindekiPHDegeri { get; set; }

        public double DolumBittigindekiSutSicakligi { get; set; }

        public string SorumluPersonel { get; set; }

        public double ToplamGecenSure { get; set; }

    }

    public class YogurtBirinciKultur
    {
        public double KullanilanMiktar { get; set; }

        public string TedarikciAdiveKulturKodu { get; set; }

        public string LotNo { get; set; }
    }

    public class YogurtIkinciKultur
    {
        public double KullanilanMiktar { get; set; }

        public string TedarikciAdiveKulturKodu { get; set; }

        public string LotNo { get; set; }
    }

    public class YogurtTuzOzellikleri
    {
        public string TedarikciAdi { get; set; }

        public string PartiNo { get; set; }
    }

    public class YogurtSutOzellikleri
    {
        public string SutTuru { get; set; }

        public string SutunKaynagi { get; set; }

        public double Brix { get; set; }

        public double PH { get; set; }

        public double SH { get; set; }

        public double Yag { get; set; }
    }

    public class YogurtYariMamulKaliteOzellikleri
    {
        public string PartiNo { get; set; }

        public double TKM { get; set; }

        public double Protein { get; set; }

        public double Tuz { get; set; }

        public double Brix { get; set; }

        public double PH { get; set; }

        public double SH { get; set; }

        public double Yag { get; set; }

        public string TatKokuKivam { get; set; }
    }

    public class YogurtInkubasyonOzellikleri
    {
        public string KontrolNo { get; set; }

        public string Saat { get; set; }

        public double Sicaklik { get; set; }

        public double PH { get; set; }

        public string KontrolEdenPersonel { get; set; }
    }
}