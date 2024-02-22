using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class FSAPKY005_2
    {
        public string PartiNo { get; set; }

        public string UrunKodu { get; set; }

        public string UrunTanimi { get; set; }

        public string Aciklama { get; set; }

        public string Tarih { get; set; }

        public string Kontrol1 { get; set; }

        public List<FSAPKY005_2_1> fSAPKY005_2_1s { get; set; }
        public List<FSAPKY005_2_2> fSAPKY005_2_2s { get; set; } 
    }

    public class FSAPKY005_2_1
    {
        public string TankNumarasi { get; set; }

        public double StanSutMiktari { get; set; }

        public double StanSutSicakligi { get; set; }

        public double KulturlemeSicakligi { get; set; }

        public string OperatorAdi { get; set; }

        public double StanSutPH { get; set; }

        public double StanSutSH { get; set; }

        public double StanSutKM { get; set; } 

        public string TekPersAdi { get; set; } 
    }

    public class FSAPKY005_2_2
    {
        public string UrunAdi { get; set; }

        public string DolumaBaslamaOnayi { get; set; } 

        public string SorumluMuhendis { get; set; }  
    } 
}