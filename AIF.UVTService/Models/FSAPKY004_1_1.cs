using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class FSAPKY004_1_1
    {
        public string PartiNo { get; set; }

        public string UrunKodu { get; set; }

        public string UrunTanimi { get; set; }

        public string Aciklama { get; set; }

        public string Tarih { get; set; }

        public string Kontrol1 { get; set; }

        public string Kontrol2 { get; set; }

        public List<FSAPKY004_1_1_1> fSAPKY004_1_1_1s { get; set; } 
        public List<FSAPKY004_1_1_2> fSAPKY004_1_1_2s { get; set; } 
        public List<FSAPKY004_1_2_1> fSAPKY004_1_2_1s { get; set; } 
        public List<FSAPKY004_1_2_2> fSAPKY004_1_2_2s { get; set; }
        public List<FSAPKY004_1_2_3> fSAPKY004_1_2_3s { get; set; }
    }

    public class FSAPKY004_1_1_1
    {
        public string ProsesTankNo { get; set; }

        public double UFSutuMiktari { get; set; }

        public double ReworkMiktari { get; set; }

        public double MayalamaSicakligi { get; set; }

        public string MayalamaSaati { get; set; }

        public string KirimSaati { get; set; }

        public string UFOperatorAdi { get; set; } 
    }

    public class FSAPKY004_1_1_2
    {
        public double UFSutuPH { get; set; }

        public double UFSutuSH { get; set; }

        public double UFSutuKM { get; set; }

        public double UFSutuYagi { get; set; }

        public double KirimPH { get; set; }

        public double KirimSH { get; set; } 

        public string LabPersonelAdi { get; set; } 
    }

    public class FSAPKY004_1_2_1
    {
        public double UFGirisSicakligi { get; set; }

        public string UFGirisSaati { get; set; }

        public double UFFaktoru { get; set; }

        public double UFCikisSicakligi { get; set; }

        public string UFBitisSaati { get; set; } 

        public string UFOperatorAdi { get; set; } 
    }

    public class FSAPKY004_1_2_2
    {
        public double ReworkPH { get; set; }

        public double ReworkKM { get; set; }

        public double ReworkYag { get; set; } 

        public string LabPersonelAdi { get; set; } 
    }

    public class FSAPKY004_1_2_3
    {
        public double KonsantreUrunPH { get; set; }

        public double KonsantreUrunSH { get; set; }

        public double KonsantreUrunKM { get; set; }

        public double KonsantreUrunYag { get; set; } 

        public string LabPersonelAdi { get; set; } 
    }
}