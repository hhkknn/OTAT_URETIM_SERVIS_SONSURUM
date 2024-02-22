using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class FSAPKY004_2
    {
        public string PartiNo { get; set; }

        public string UrunKodu { get; set; }

        public string UrunTanimi { get; set; }

        public string Aciklama { get; set; }

        public string Tarih { get; set; }

        public List<FSAPKY004_2_1> fSAPKY004_2_1s { get; set; }
        public List<FSAPKY004_2_2> fSAPKY004_2_2s { get; set; }
        public List<FSAPKY004_2_3> fSAPKY004_2_3s { get; set; }
    }

    public class FSAPKY004_2_1
    {
        public string ProsesNo { get; set; }

        public double YariMamulMiktari { get; set; }

        public double EklenenLorMiktari { get; set; }

        public double HomojenBasinci { get; set; }

        public double PastorizasyonSicakligi { get; set; }

        public string UFOperatorAdi { get; set; }
    }

    public class FSAPKY004_2_2
    {
        public double YariMamulPH { get; set; }

        public double YariMamulSH { get; set; }

        public double YariMamulKM { get; set; }

        public double YariMamulYag { get; set; }

        public double LorKM { get; set; }

        public double LorYagi { get; set; }

        public string LabPersonelAdi { get; set; }
    }

    public class FSAPKY004_2_3
    {
        public string UrunKodu { get; set; }
        public string UrunAdi { get; set; }

        public string DuyusalAnalizOnayi { get; set; } 

        public string SorumluMuhendis { get; set; } 
    }
}