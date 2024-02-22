using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class FSAPKY009_1
    {
        public string PartiNo { get; set; }

        public string UrunKodu { get; set; }

        public string UrunTanimi { get; set; }

        public string Aciklama { get; set; }

        public string Tarih { get; set; }

        public List<FSAPKY009_1_1> fSAPKY009_1_1s { get; set; }

        public List<FSAPKY009_1_2> fSAPKY009_1_2s { get; set; }
    }

    public class FSAPKY009_1_1
    { 
        public int PisirmeNo { get; set; }

        public string BaslangicSaati { get; set; }

        public double PisirimSicakligi { get; set; }

        public string BitisSaati { get; set; }

        public string UFOperatorAdi { get; set; }

        public double PisirimOncesiYariMamulPH { get; set; }

        public double PisirimSonrasiYariMamulPH { get; set; }

        public double Kurumadde { get; set; }

        public double Yag { get; set; }

        public string LabPersonelAdi { get; set; }
    }
    public class FSAPKY009_1_2
    { 

        public string UrunKodu { get; set; }

        public string UrunAdi { get; set; }

        public string DuyusalAnalizOnayi { get; set; }

        public string SorumluMuhendis { get; set; } 
    }

}