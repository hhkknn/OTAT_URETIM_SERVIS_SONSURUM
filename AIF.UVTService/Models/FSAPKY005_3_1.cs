using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class FSAPKY005_3_1
    {
        public string PartiNo { get; set; }

        public string UrunKodu { get; set; }

        public string UrunTanimi { get; set; }

        public string Aciklama { get; set; }

        public string Tarih { get; set; }
        public string Kontrol1 { get; set; }
        public string Kontrol2 { get; set; }

        public List<FSAPKY005_3_1_1> fSAPKY005_3_1_1s { get; set; } 
        public List<FSAPKY005_3_1_2> fSAPKY005_3_1_2s { get; set; } 
        public List<FSAPKY005_3_2_1> fSAPKY005_3_2_1s { get; set; } 
        public List<FSAPKY005_3_2_2> fSAPKY005_3_2_2s { get; set; }
    }

    public class FSAPKY005_3_1_1
    {
        public string UrunAdi { get; set; }

        public double DolBasPH1 { get; set; } 

        public double DolBasPH2 { get; set; } 

        public double DolBasPH3 { get; set; } 

        public double DolBasPH4 { get; set; } 

        public double DolBasPH5 { get; set; } 

        public double DolBasPH6 { get; set; } 

        public double DolBasPH7 { get; set; } 

        public double DolBasPH8 { get; set; }

        public string OperatorAdi { get; set; } 
    }

    public class FSAPKY005_3_1_2
    {
        public string UrunAdi { get; set; }

        public double Ornek1 { get; set; } 

        public double Ornek2 { get; set; } 

        public double Ornek3 { get; set; } 

        public double Ornek4 { get; set; } 

        public double Ornek5 { get; set; } 

        public double Ornek6 { get; set; } 

        public double Ornek7 { get; set; } 

        public double Ornek8 { get; set; } 

        public double Ornek9 { get; set; }

        public double Ornek10 { get; set; }

        public string OperatorAdi { get; set; } 
    }

    public class FSAPKY005_3_2_1
    {
        public string TankNumarasi { get; set; }

        public string PaletNumarasi { get; set; }

        public string BirinciAnalizSaat { get; set; }

        public string IkinciAnalizSaat { get; set; }

        public string UcuncuAnalizSaat { get; set; }

        public string DorduncuAnalizSaat { get; set; }

        public string BesinciAnalizSaat { get; set; }

        public double BirinciAnalizPH { get; set; }

        public double IkinciAnalizPH { get; set; }

        public double UcuncuAnalizPH { get; set; }

        public double DorduncuAnalizPH { get; set; }

        public double BesinciAnalizPH { get; set; }

        public double BirinciAnalizSicaklik { get; set; }

        public double IkinciAnalizSicaklik { get; set; }

        public double UcuncuAnalizSicaklik { get; set; }

        public double DorduncuAnalizSicaklik { get; set; }

        public double BesinciAnalizSicaklik { get; set; }

        public string OperatorAdi { get; set; }  
    }

    public class FSAPKY005_3_2_2
    {
        public string TankNo { get; set; }

        public string PaletNo { get; set; }

        public string Saat { get; set; }

        public double PH { get; set; }

        public double SH { get; set; } 

        public string OperatorAdi { get; set; } 
    }

}