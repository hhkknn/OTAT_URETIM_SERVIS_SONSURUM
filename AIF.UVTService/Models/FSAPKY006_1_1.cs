using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class FSAPKY006_1_1
    {
        public string PartiNo { get; set; }

        public string UrunKodu { get; set; }

        public string UrunTanimi { get; set; }

        public string Aciklama { get; set; }

        public string Tarih { get; set; }

        public string Kontrol1 { get; set; }

        public string Kontrol2 { get; set; }
        public string Kontrol3 { get; set; }

        public List<FSAPKY006_1_1_1> fSAPKY006_1_1_1s { get; set; } 
        public List<FSAPKY006_1_1_2> fSAPKY006_1_1_2s { get; set; } 
        public List<FSAPKY006_1_2_1> fSAPKY006_1_2_1s { get; set; } 
        public List<FSAPKY006_1_2_2> fSAPKY006_1_2_2s { get; set; }
    }

    public class FSAPKY006_1_1_1
    {
        public string TankNumarasi { get; set; }

        public double StandSuMiktari { get; set; }

        public double EklenenKrema { get; set; }

        public double EklenenKremaPH { get; set; }

        public double FirinlamaSuresi { get; set; }

        public double KulturlemeSicakligi { get; set; }
        public string KulturlemeSaati { get; set; }
        public double KullanilanKulturPH { get; set; }
        public double KullanilanKulturOrani { get; set; }

        public string OperatorPersonelAdi { get; set; } 
    }

    public class FSAPKY006_1_1_2
    {
        public double CigKremaYagOrani { get; set; }

        public double CigKremaPH { get; set; }

        public double PastKremaYagOrani { get; set; }

        public double PastKremaPH { get; set; }

        //değişen alanlar
        public double StandSutYag { get; set; }

        public double StandSutPH { get; set; }

        public double PastSutYag{ get; set; }

        public double PastSutPH { get; set; }
        public string LabPersonelAdi { get; set; } 
    }

    public class FSAPKY006_1_2_1
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
        public string SogukOdaGirisSaati { get; set; }
        public string OperatorAdi { get; set; }
    }

    public class FSAPKY006_1_2_2
    {
        public string TankNumarasi { get; set; }

        public string InkubasyonOdaNo { get; set; }

        public string Saat { get; set; } 

        public double PH { get; set; } 

        public double Sicaklik { get; set; }

        public string SogOdGrSaat { get; set; } 

        public string OperatorAdi { get; set; } 
    } 
}