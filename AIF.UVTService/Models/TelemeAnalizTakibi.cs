using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class TelemeAnalizTakibi
    {
        public string PartiNo { get; set; }

        public string UrunKodu { get; set; }

        public string UrunTanimi { get; set; }

        public string Tarih { get; set; }

        public List<SutOzellikleri> SutOzellikleri { get; set; }

        public List<ProsesOzellikleri> ProsesOzellikleri { get; set; }

        public List<SonUrunOzellikleri> SonUrunOzellikleri { get; set; }

        public string Aciklama { get; set; }
    }

    public class SutOzellikleri
    {
        public string TelemeTuru { get; set; }

        public string GorevliOperator { get; set; }

        public string GorevliOperatorAdi { get; set; }

        public double NetSutMiktari { get; set; }

        public string SutunKaynagi { get; set; }

        public double SutunBicakSeviyesindenFarki { get; set; }

        public double PastorSicakligi { get; set; }

        public double KuruMadde { get; set; }

        public double Yag { get; set; }

        public double PH { get; set; }

        public double SH { get; set; }

        public double Protein { get; set; }
    }

    public class ProsesOzellikleri
    {
        public double MayalamaSicakigi { get; set; }

        public double MayalamaSuresi { get; set; }

        public double CedarlamaSicakligi { get; set; }

        public double CedarlamaSuresi { get; set; }

        public double TelemeIndirildigindekiPH { get; set; }
    }

    public class SonUrunOzellikleri
    {
        public double KuruMadde { get; set; }

        public double Yag { get; set; }

        public double PH { get; set; }

        public double SH { get; set; }

        public double PasBrix { get; set; }

        public double UretilenTLMMiktari { get; set; }

        public double RandimanDegeri { get; set; }
    }
}