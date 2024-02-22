using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class AyranYardimciMalzemeGunlukOzet
    {
        public string UretimTarihi { get; set; }

        public string UretimVardiyasi { get; set; }

        public List<AyranYardimciMalzemeGunlukOzet1> ayranYardimciMalzemeGunlukOzet1s { get; set; }

    }

    public class AyranYardimciMalzemeGunlukOzet1
    {
        public string UrunKodu { get; set; }

        public string UrunAdi { get; set; }

        public string BardakTedarikciAdi { get; set; }

        public string BardakPartiNo { get; set; }

        public string FolyoTedarikciAdi { get; set; }

        public string FolyoPartiNo { get; set; }

        public string ViyolTedarikciAdi { get; set; }

        public string ViyolPartiNo { get; set; }

    }
}