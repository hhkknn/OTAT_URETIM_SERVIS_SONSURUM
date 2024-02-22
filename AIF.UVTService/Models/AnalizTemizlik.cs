using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class AnalizTemizlik
    {
        public string IstasyonKodu { get; set; }

        public string IstasyonTanimi { get; set; }

        public string Tarih { get; set; }

        public string YoneticiOnayi { get; set; }
        public string YoneticiOnayTarihi { get; set; }
        public string YoneticiOnayAciklama { get; set; }

        //public List<CIP1> cIP1s { get; set; }

        public List<CIP2> cIP2s { get; set; }

        public List<AletEkipmanTemizlik> aletEkipmanTemizliks { get; set; }
    }


    //public class CIP1
    //{
    //    public string KontrolNoktasiAdi { get; set; }

    //    public string DegerKod { get; set; }

    //    public string Deger { get; set; }
    //}

    public class CIP2
    {
        public string KontrolNoktasiAdi { get; set; }

        public string AlkaliBasSaat { get; set; }

        public double AlkaliSc { get; set; }

        public double AlkaliKonMs { get; set; }

        public double AlkaliKonYuzde { get; set; }

        public string AlkaliBitSaat { get; set; }

        public string AsitBasSaat { get; set; }

        public double AsitSc { get; set; }

        public double AsitKonMs { get; set; }

        public double AsitKonYuzde { get; set; }

        public string AsitBitSaat { get; set; }

        public string TemizlikPers { get; set; }

        public string OzonitKontrol { get; set; }

        public double DurulamaPh { get; set; }

        public string DezenPrsnl { get; set; }

        public double HaslamaSc { get; set; }

        public string HaslamaPrsnl { get; set; }

        public string KontrolPersonel { get; set; }

        public string Yapildi { get; set; }

        public string Yapilmadi { get; set; }
    }

    public class AletEkipmanTemizlik
    {
        public string TemizlenenAlanMakine { get; set; }

        public string TemizlikYapilmaSikligi { get; set; }

        public string KullanilanKimysalAdi { get; set; }

        public string KullanilanKimysalMiktar { get; set; }

        public string TemizlikYapildi { get; set; }

        public string TemizlikYapilmadi { get; set; }

        public string TemizlikYapanPersonel { get; set; }

        public string TemizlikKontrolEdenPersonel { get; set; } 
    }
}