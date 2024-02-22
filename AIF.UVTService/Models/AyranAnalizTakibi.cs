using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class AyranAnalizTakibi
    {
        public int DocEntry { get; set; }

        public string PartiNo { get; set; }

        public double PastorizasyonSicaklik { get; set; }

        public double PastorizasyonSure { get; set; }

        public double OnAktiflestirmeSutSicakligi { get; set; }

        public double OnAktifSuresi { get; set; }

        public double OnAktifPH { get; set; }

        public string SutVakum { get; set; }

        public string TankAdi { get; set; }

        public double MayalamaTankiSicaklik { get; set; }

        public string Sorumlu { get; set; }

        public string SorumluAdi { get; set; }

        public double PompaBasinci { get; set; }

        public int BaslangicSaati { get; set; }

        public int BitisSaati { get; set; }

        public double TKM { get; set; }

        public double Protein { get; set; }

        public double Tuz { get; set; }

        public double Brix { get; set; }

        public double PH { get; set; }

        public double SH { get; set; }

        public double Yag { get; set; }

        public string TatKokuKivam { get; set; }

        public List<MayalanacakSutOzellikleri> MayalanacakSutOzellikleri { get; set; }

        public List<Inkubasyon> Inkubasyon { get; set; }


    }

    public class MayalanacakSutOzellikleri
    {
        public string Kaynak { get; set; }

        public string KaynakAdi { get; set; }

        public double Brix { get; set; }

        public double PH { get; set; }

        public double SH { get; set; }

        public double Yag { get; set; }
    }

    public class Inkubasyon
    {
        public int InkubasyonNo { get; set; }

        public int Saat { get; set; }

        public double Sicaklik { get; set; }

        public double PH { get; set; }

        public string KontrolEdenNo { get; set; }

        public string KontrolEdenAdi { get; set; }
    }
}