using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class PastorizasyonProsesTakipAnaliz
    {
        public string PartiNo { get; set; }

        public string UretimSiparisNo { get; set; }

        public string UretilenUrunTanimi { get; set; }

        public List<PastorizasyonProsesOzellikleri1> PastorizasyonProsesOzellikleri1s { get; set; }

        public List<PastorizasyonProsesOzellikleri2> PastorizasyonProsesOzellikleri2s { get; set; }

        public List<PastorizasyonProsesOzellikleri3> PastorizasyonProsesOzellikleri3s { get; set; }

        public List<PastorizasyonGunluk> PastorizasyonGunluk1s { get; set; }

        public List<PastorizasyonGunluk2> PastorizasyonGunluk2s { get; set; }

    }

    public class PastorizasyonProsesOzellikleri1
    {
        public string SutunAlinanTankAdi { get; set; }

        public string AlinanSutunPartiNo { get; set; }

        public string ProsesBaslangicSaati { get; set; }

        public double YagCekilecekSutMik { get; set; }

        public double SutYagOrani { get; set; }

        public double SutunPh { get; set; }

        public double KremaYogunlugu { get; set; }

        public double KremaPh { get; set; }

        public double KremaYagOrani { get; set; }

        public double CekilenKremaMikKG { get; set; }

        public double CekilenKremaMikLT { get; set; }

        public double KalanSutMik { get; set; }

        public double YagAlnmsSutYagOr { get; set; }

        public double YagAlnmsSutPH { get; set; }

        public string SutunGondTankAdi { get; set; }

    }

    public class PastorizasyonProsesOzellikleri2
    {
        public string UretilenKremaParti { get; set; }

        public string KremaPastBasSaat { get; set; }

        public double KremaPastSicakligi { get; set; }

        public string KremaPastBitSaat { get; set; }

        public string KremaMayalamaSaati { get; set; }

        public double KremaMayalamaSicak { get; set; }

        public double KremaMayalamaPh { get; set; }

        public string MayalamaKazanFiltTem { get; set; }

        public string KremaDolumBasSaat { get; set; }

        public string KremaDolumBitSaat { get; set; }

        public string KremaDolumYapan { get; set; }

        public double DolumSicakligi { get; set; }

        public string UretimYapan { get; set; }

        public string KontrolEdenMuh { get; set; }

    }

    public class PastorizasyonProsesOzellikleri3
    {
        public string UretilenKremaParti { get; set; }

        public string KullanilanKulturVeKodu { get; set; }

        public double KulturMiktari { get; set; }

        public string UretilenKremaMiktari { get; set; }

        public double KremaYagOrani { get; set; }

        public string DolumYapilanAmbalaj { get; set; }

        public double KullanimYapilanAmbalajMiktari { get; set; }

        public string BirAmbOrtMiktar { get; set; }

        public double KremaDepoPh { get; set; }

        public double KremaninDepoSicakligi { get; set; }

        public string UretimYapanOperator { get; set; }

        public string KontrolEdenMuhendis { get; set; }


    }

    public class PastorizasyonGunluk
    {
        public string VeriAdi { get; set; }

        public double Kova { get; set; }

        public double Teneke { get; set; }

        public double ToplamveyaOrt { get; set; }
    }

    public class PastorizasyonGunluk2
    {
        public string VeriAdi { get; set; }

        public double Deger { get; set; }
    }
}