using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class FSAPKY002_3
    {
        public string PartiNo { get; set; }

        public string UrunKodu { get; set; }

        public string UrunTanimi { get; set; }

        public string Aciklama { get; set; }

        public string Tarih { get; set; }

        public string Kontrol1 { get; set; }

        public string Kontrol2 { get; set; }

        public string Kontrol3 { get; set; }

        public List<FSAPKY002_3_1> fSAPKY002_3_1s { get; set; }
        public List<FSAPKY002_3_2> fSAPKY002_3_2s { get; set; }
        public List<FSAPKY002_3_3> fSAPKY002_3_3s { get; set; }
        public List<FSAPKY002_3_4> fSAPKY002_3_4s { get; set; }
    }
    public class FSAPKY002_3_1
    {
        public string UrunKodu { get; set; }

        public string UrunAdi { get; set; }

        public double PaketlemeSicakligi { get; set; }

        public DateTime UretimTarihi { get; set; }

        public DateTime SonTuketimTarihi { get; set; }

        public string PartiNumarasi { get; set; }

        public string OperatorAdi { get; set; }
    }

    public class FSAPKY002_3_2
    {
        public string Saat { get; set; }

        public string UrunKodu { get; set; }

        public string UrunAdi { get; set; }

        public double BirinciGozCO2 { get; set; }

        public double BirinciGozO2 { get; set; }

        public double IkinciGozCO2 { get; set; }

        public double IkinciGozO2 { get; set; }

        public double UcuncuGozCO2 { get; set; }

        public double UcuncuGozO2 { get; set; }

        public string OperatorAdi { get; set; }
    }

    public class FSAPKY002_3_3
    {
        public string UrunKodu { get; set; }
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

    public class FSAPKY002_3_4
    {
        public string UrunKodu { get; set; }

        public string UrunAdi { get; set; }

        public string DuyusalAnalizOnayi { get; set; }

        public string SorumluMuhendis { get; set; }
    }
} 