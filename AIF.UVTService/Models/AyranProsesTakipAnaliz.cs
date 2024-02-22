using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class AyranProsesTakipAnaliz
    {
        public string PartiNo { get; set; }

        public string UrunKodu { get; set; }

        public string UrunTanimi { get; set; }

        public string Aciklama { get; set; }

        public string UretimTarihi { get; set; }

        public List<AyranProsesOzellikleri1> ayranProsesOzellikleri1s { get; set; }

        public List<AyranProsesOzellikleri2> ayranProsesOzellikleri2s { get; set; }

        public List<AyranPihtiKirim> ayranPihtiKirims { get; set; }

        public List<AyranBirinciKultur> ayranBirinciKulturs { get; set; }

        public List<AyranIkinciKultur> ayranIkinciKulturs { get; set; }

        public List<AyranTuzOzellikleri> ayranTuzOzellikleris { get; set; }

        public List<AyranSutOzellikleri> ayranSutOzellikleris { get; set; }

        public List<AyranYariMamulKaliteOzellikleri> ayranYariMamulKaliteOzellikleris { get; set; }

        public List<AyranInkubasyonOzellikleri> ayranInkubasyonOzellikleris { get; set; }

    }

    public class AyranProsesOzellikleri1
    {
        public string UrunGrubu { get; set; }

        public string ProsesBaslangicSaati { get; set; }

        public double YagliSutMiktari { get; set; }

        public double YagsizSutMiktari { get; set; }

        public double ToplamSuMiktari { get; set; }

        public double TuzMiktari { get; set; }

        public double AyranTuzOrani { get; set; }

        public double ToplamAyranYariMamulMiktari { get; set; }

    }

    public class AyranProsesOzellikleri2
    {
        public double PastorizasyonSicakligi { get; set; }

        public double PastorizasyonSuresi { get; set; }

        public string SutVakum { get; set; }

        public string MayalamaSaati { get; set; }

        public double MayalamaSicakligi { get; set; }

        public string SorumluPersonel { get; set; }

    }

    public class AyranPihtiKirim
    {
        public double PompaBasinci { get; set; }

        public string BaslamaSaati { get; set; }

        public string BitisSaati { get; set; }

        public double ToplamGecenSure { get; set; }
    }

    public class AyranBirinciKultur
    {
        public double KullanilanMiktar { get; set; }

        public string TedarikciAdiveKulturKodu { get; set; }

        public string LotNo { get; set; }
    }

    public class AyranIkinciKultur
    {
        public double KullanilanMiktar { get; set; }

        public string TedarikciAdiveKulturKodu { get; set; }

        public string LotNo { get; set; }
    }

    public class AyranTuzOzellikleri
    {
        public string TedarikciAdi { get; set; }

        public string PartiNo { get; set; }
    }

    public class AyranSutOzellikleri
    {
        //public string YagliSutunKaynagi { get; set; }

        //public string YagsizSutunKaynagi { get; set; }

        public string SutunKaynagi { get; set; }

        public string SutTuru { get; set; }

        public double Brix { get; set; }

        public double PH { get; set; }

        public double SH { get; set; }

        public double Yag { get; set; }
    }

    public class AyranYariMamulKaliteOzellikleri
    {
        public string PartiNo { get; set; }

        public double TKM { get; set; }

        //public double Protein { get; set; }

        //public double Tuz { get; set; }

        public double Brix { get; set; }

        public double PH { get; set; }

        public double SH { get; set; }

        public double Yag { get; set; }

        //public string TatKokuKivam { get; set; }
    }

    public class AyranInkubasyonOzellikleri
    {
        public string KontrolNo { get; set; }

        public string Saat { get; set; }

        //public double Sicaklik { get; set; }

        public double UrunSicakligi { get; set; }

        public double OdaSicakligi { get; set; }

        public double PH { get; set; }

        public string KontrolEdenPersonel { get; set; }
    }
}