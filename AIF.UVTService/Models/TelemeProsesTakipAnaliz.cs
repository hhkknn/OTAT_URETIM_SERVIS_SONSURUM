using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.Models
{

    public class TelemeProsesTakipAnaliz
    {
        public int DocEntry { get; set; }

        public string PartiNo { get; set; }

        public string Aciklama { get; set; }

        public string UretimTarihi { get; set; }

        public List<ProsesOzellikleri1> prosessOzellikleri1Detay { get; set; }

        public List<SutunOzellikleri1> SutOzellikleriDetay { get; set; }

        public List<ProsesOzellikleri2> prosesOzellikleri2Detay { get; set; }

        public List<ProsesOzellikleri2_1> prosesOzellikleri2_1_Detay { get; set; }

        public List<SarfMalzemeKullanim> sarfMalzemeKullanimDetay { get; set; }

        public List<MamulOzellikleri> mamulOzellikleriDetay { get; set; }

        public List<MamulOzellikleri1> mamulOzellikleri1Detay { get; set; }

    }

    public class ProsesOzellikleri1
    {
        public string PartiNo { get; set; }

        public string TelemeTuru { get; set; }

        public string GorevliOperator { get; set; }

        public string GorevliOperatorAdi { get; set; }

        public string SutGondermeBaslangicSaati { get; set; }

        public string SutGondermeBitisSaati { get; set; }

        public double NetSutMiktari { get; set; }

        public double SutPastorizasyonSicakligi { get; set; }

        public string EsanjorTankFiltreKontrolu { get; set; }

        public double TanktakiSuSeviyesi { get; set; }
    }

    public class SutunOzellikleri1
    {
        public double KuruMadde { get; set; }

        public double YagOrani { get; set; }

        public double PH { get; set; }

        public double SH { get; set; }

        public double SuOrani { get; set; }

        public double ProteinOrani { get; set; }
    }

    public class ProsesOzellikleri2
    {
        public string MayalamaSaati { get; set; }

        public double MayalamaSicakligi { get; set; }

        public string KirimVeCedarlamaBaslangicSaati { get; set; }

        public double CedarlamaSicakligi { get; set; }

        public string KirimVeCedarlamaBitisSaati { get; set; }

        public string IndirmeBitisSaati { get; set; }

        public string BaskiBaslangicSaati { get; set; }

        public string BaskiBitisSaati { get; set; }

        public double UretimDepoSicakligi { get; set; }
    }

    public class ProsesOzellikleri2_1
    {
        public double SutGonderimSuresiDakika { get; set; }

        public double MayalamaSuresiDakika { get; set; }

        public double KirimVeCedarlamaSuresiDakika { get; set; }

        public double IndirimSuresiDakika { get; set; }

        public double BaskiSuresiDakika { get; set; }

        public double ToplamGecenSureDakika { get; set; }
    }

    public class SarfMalzemeKullanim
    {
        public string MalzemeAdi { get; set; }

        public string MalzemeMarkasiVeTedarikcisi { get; set; }

        public string PartiNo { get; set; }

        public double Miktar { get; set; }

        public string Birim { get; set; }
    }

    public class MamulOzellikleri
    {
        public double UretimSonrasiTelemeMiktari { get; set; }

        public double BirGunSonraTelemeMiktari { get; set; }

        public double UretimRandimani { get; set; }

        public string PersonelKodu { get; set; }

        public string PersonelAdi { get; set; }
    }

    public class MamulOzellikleri1
    {

        public double KuruMadde { get; set; }

        public double YagOrani { get; set; }

        public double PH { get; set; }

        public double SH { get; set; }

        public double TuzOrani { get; set; }

    }
}