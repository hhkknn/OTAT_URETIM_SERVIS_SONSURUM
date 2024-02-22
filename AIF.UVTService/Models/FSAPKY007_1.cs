using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class FSAPKY007_1
    {
        public string PartiNo { get; set; }

        public string UrunKodu { get; set; }

        public string UrunTanimi { get; set; }

        public string Aciklama { get; set; }

        public string Tarih { get; set; }

        public List<FSAPKY007_1_1> fSAPKY007_1_1s { get; set; }

        public List<FSAPKY007_1_2> fSAPKY007_1_2s { get; set; }
    }

    public class FSAPKY007_1_1
    {
        public string TankNo { get; set; }

        public double KullCigSut { get; set; }

        public string CigSutCekSa { get; set; }

        public double KullSu { get; set; }

        public string SuEkSaat { get; set; }

        public double TopAyrSut { get; set; }

        public string OprtAdi { get; set; }
         
    }

    public class FSAPKY007_1_2
    { 
        public double CigSutPH { get; set; }

        public double CigSutSH { get; set; }

        public double CigSutKM { get; set; }

        public double CigSutYag { get; set; }

        public double AyrSutPH { get; set; }

        public double AyrSutSH { get; set; }

        public double AyrSutKM { get; set; }

        public double AyrSutYag { get; set; }

        public string TekPersAdi { get; set; } 
    }
}