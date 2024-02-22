using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class FSAPKY012_2
    {
        public string PartiNo { get; set; }

        public string UrunKodu { get; set; }

        public string UrunTanimi { get; set; }

        public string Aciklama { get; set; }

        public string Tarih { get; set; }

        public string Kontrol1 { get; set; }

        public List<FSAPKY012_2_1> fSAPKY012_2_1s { get; set; }

        public List<FSAPKY012_2_2> fSAPKY012_2_2s { get; set; }
    }

    public class FSAPKY012_2_1
    {
        public string UrunAdi { get; set; }

        public double PaketlemeSicakligi { get; set; }

        public DateTime UretimTarihi { get; set; }

        public DateTime SonTuketimTarihi { get; set; }

        public string PartiNo { get; set; }

        public string OperatorAdi { get; set; }
    }
    public class FSAPKY012_2_2
    {
        public string UrunAdi { get; set; }

        public double PHOlcum1 { get; set; }
        public double PHOlcum2 { get; set; }
        public double PHOlcum3 { get; set; }
        public double PHOlcum4 { get; set; }
        public double PHOlcum5 { get; set; }
        public double PHOlcum6 { get; set; }
        public double PHOlcum7 { get; set; }
        public double PHOlcum8 { get; set; }
        public double PHOlcum9 { get; set; }
        public double PHOlcum10 { get; set; }

        public double PHMiktar1 { get; set; } 
        public double PHMiktar2 { get; set; } 
        public double PHMiktar3 { get; set; } 
        public double PHMiktar4 { get; set; } 
        public double PHMiktar5 { get; set; } 
        public double PHMiktar6 { get; set; } 
        public double PHMiktar7 { get; set; } 
        public double PHMiktar8 { get; set; } 
        public double PHMiktar9 { get; set; } 
        public double PHMiktar10 { get; set; } 

        public string PHSaat1 { get; set; }
        public string PHSaat2 { get; set; }
        public string PHSaat3 { get; set; }
        public string PHSaat4 { get; set; }
        public string PHSaat5 { get; set; }
        public string PHSaat6 { get; set; }
        public string PHSaat7 { get; set; }
        public string PHSaat8 { get; set; }
        public string PHSaat9 { get; set; }
        public string PHSaat10 { get; set; }
         
        public string OperatorAdi { get; set; }
    }

}