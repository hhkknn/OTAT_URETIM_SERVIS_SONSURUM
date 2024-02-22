using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class TazePeynirTakipAnaliz2
    {
        public string PartiNo { get; set; }

        public string KalemKodu { get; set; }

        public string KalemTanimi { get; set; }

        public string Aciklama { get; set; }

        public string UretimTarihi { get; set; }

        public string PaketlemeTarihi { get; set; } 

        public List<TazePeynir2SarfMalzemeKullanim> tazePeynir2SarfMalzemeKullanims { get; set; } 

        public List<TazePeynir2GramajKontrol> tazePeynir2GramajKontrols { get; set; }
    } 

    public class TazePeynir2SarfMalzemeKullanim
    {
        public string MalzemeAdi { get; set; }

        public string MalzemeMarkaTedarikcisi { get; set; }

        public string SarfMalzemePartiNo { get; set; }

        public double Miktar { get; set; }

        public string Birim { get; set; }
    }

    public class TazePeynir2GramajKontrol
    {
        public string UrunCesidi { get; set; }

        public string PartiNo { get; set; }

        public double BirinciOrnek { get; set; }

        public double IkinciOrnek { get; set; }

        public double UcuncuOrnek { get; set; }

        public double DorduncuOrnek { get; set; }

        public double BesinciOrnek { get; set; }

        public double AltinciOrnek { get; set; }

        public double YedinciOrnek { get; set; }
    }
}