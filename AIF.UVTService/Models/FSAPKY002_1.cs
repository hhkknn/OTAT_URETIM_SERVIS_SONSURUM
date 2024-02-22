using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class FSAPKY002_1
    {
        public string PartiNo { get; set; }

        public string UrunKodu { get; set; }

        public string UrunTanimi { get; set; }

        public string Aciklama { get; set; }

        public string Tarih { get; set; }
         
        public string Kontrol { get; set; }
        public List<FSAPKY002_1_1> fSAPKY002_1_1s { get; set; }
        public List<FSAPKY002_1_2> fSAPKY002_1_2s { get; set; }
        public List<FSAPKY002_1_3> fSAPKY002_1_3s { get; set; }
        public List<FSAPKY002_1_4> fSAPKY002_1_4s { get; set; } 
    }

    public class FSAPKY002_1_1
    {
        public string ProsesTankNo { get; set; }

        public double TelemeSutMiktari { get; set; }

        public double MayalamaSicakligi { get; set; }

        public string MayalamaSaati { get; set; }

        public string KirimSaati { get; set; }

        public double KirimPH { get; set; }

        public string ProsesPersonelAdi { get; set; }
    }

    public class FSAPKY002_1_2
    {
        public double TelemeSutPH { get; set; }

        public double TelemeSutSH { get; set; }

        public double TelemeSutKM { get; set; }

        public double TelemeSutYagi { get; set; }

        public double TelemeYagi { get; set; }

        public double TelemeKuruMadde { get; set; }

        public double TelemePH { get; set; }

        public string LabPersonelAdi { get; set; }
    }

    public class FSAPKY002_1_3
    {
        public double CedarlamaSicakligi { get; set; }

        public string CedarlamaBasSaati { get; set; }

        public string CedarlamaBitSaati { get; set; }

        public string IndirmeBasSaat { get; set; }

        public string IndirmeBitSaat { get; set; }

        public double IndirmePH { get; set; }
    }

    public class FSAPKY002_1_4
    {
        public double PihtiSuresi { get; set; }

        public double CedarlamaSuresi { get; set; }

        public double IndirmeSuresi { get; set; }

        public double ToplamSure { get; set; }

        public double TelemeRandimani { get; set; }  
    }
     
}