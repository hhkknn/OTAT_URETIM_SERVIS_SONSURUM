using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class TamYagliAyranGunlukOzet
    {
        public string UretimTarihi { get; set; }

        public string UretimVardiyasi { get; set; }

        public string Aciklama { get; set; }

        public List<TamYagliPaketlemeBilgileri1> tamYagliPaketlemeBilgileri1s { get; set; }

        public List<TamYagliPaketlemeBilgileri2> tamYagliPaketlemeBilgileri2s { get; set; }

        public List<TamYagliGunlukOzet> tamYagliGunlukOzets { get; set; }

    }

    public class TamYagliPaketlemeBilgileri1
    {
        public string UrunKodu { get; set; }

        public string UrunAdi { get; set; }

        public double PaketlenenAyranMiktariAdet { get; set; }

        public double PaketlenenAyranMiktariLitre { get; set; }
    }

    public class TamYagliPaketlemeBilgileri2
    {
        public double ToplamSutMiktari { get; set; }

        public double ToplamSuMiktari { get; set; }

        public double TuzMiktari { get; set; }

        public double AyranTuzOrani { get; set; }

        public double ToplamAyranMiktari { get; set; }

    }

    public class TamYagliGunlukOzet
    {
        public double MayalananAyranMiktari { get; set; }

        public double OncekiGundenDevredenAyranMiktari { get; set; }

        public double TanktakiToplamAyranMiktari { get; set; }

        public double PaketlenenVeTanktakiAyranFarki { get; set; }

        public double SonrakiGuneDevredenAyranMiktari { get; set; }

        public double GunSonuAyranFarki { get; set; }

        public double StandartRandimanDegeri { get; set; }

        public double GerceklesenRandimanDegeri { get; set; }
    }
}