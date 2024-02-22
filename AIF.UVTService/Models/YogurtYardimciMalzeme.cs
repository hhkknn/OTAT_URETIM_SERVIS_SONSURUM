using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class YogurtYardimciMalzeme
    {
        public string UretimTarihi { get; set; }

        public string Aciklama { get; set; }

        public List<YogurtYardimciMalzemeKontrolu> yogurtYardimciMalzemeKontrolus { get; set; }

        public List<YogurtYardimciMalzemeGorevliPersonel> yogurtYardimciMalzemeGorevliPersonels { get; set; }

        public List<YogurtYardimciMalzemeGramajKontrol> yogurtYardimciMalzemeGramajKontrols { get; set; }

    }

    public class YogurtYardimciMalzemeKontrolu
    {
        public string UrunKodu { get; set; }

        public string UrunAdi { get; set; }

        public string KovaKaseTedarikciAdi { get; set; }

        public string KovaKasePartiNo { get; set; }

        public string KapakFolyoTedarikciAdi { get; set; }

        public string KapakFolyoPartiNo { get; set; }

        public string ViyolTedarikciAdi { get; set; }

        public string ViyolPartiNo { get; set; }
    }

    public class YogurtYardimciMalzemeGorevliPersonel
    {
        public string Aciklama { get; set; }

        public string Deger { get; set; }

    }

    public class YogurtYardimciMalzemeGramajKontrol
    {
        public string Aciklama { get; set; }

        public string Deger { get; set; }

    }
}