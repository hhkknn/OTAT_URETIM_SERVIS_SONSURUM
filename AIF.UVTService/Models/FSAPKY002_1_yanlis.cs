using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class FSAPKY002_1_yanlis
    {
        public string PartiNo { get; set; }

        public string UrunKodu { get; set; }

        public string UrunTanimi { get; set; }

        public string Aciklama { get; set; }

        public string Tarih { get; set; }

        public List<FSAPKY002_1_1> fSAPKY002_1_1s { get; set; }
        public List<FSAPKY002_1_2> fSAPKY002_1_2s { get; set; }
        public List<FSAPKY002_1_3> fSAPKY002_1_3s { get; set; }
        public List<FSAPKY002_1_4> fSAPKY002_1_4s { get; set; }
        public List<FSAPKY002_2_1> fSAPKY002_2_1s { get; set; }
        public List<FSAPKY002_2_2> fSAPKY002_2_2s { get; set; }

        public List<FSAPKY002_3_1> fSAPKY002_3_1s { get; set; }
        public List<FSAPKY002_3_2> fSAPKY002_3_2s { get; set; }
        public List<FSAPKY002_3_3> fSAPKY002_3_3s { get; set; }
        public List<FSAPKY002_3_4> fSAPKY002_3_4s { get; set; }
    }

    public class FSAPKY002_1_1asd
    {
        public string ProsesTankNo { get; set; }

        public double TelemeSutMiktari { get; set; }

        public double MayalamaSicakligi { get; set; }

        public double MayalamaSuresi { get; set; }

        public string KirimSaati { get; set; }

        public double KirimPH { get; set; }

        public string ProsesPersonelAdi { get; set; }
    }

    public class FSAPKY002_1_2asd
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

    public class FSAPKY002_1_3asd
    {
        public double CedarlamaSicakligi { get; set; }

        public string CedarlamaBasSaati { get; set; }

        public string CedarlamaBitSaati { get; set; }

        public string IndirmeBasSaat { get; set; }

        public string IndirmeBitSaat { get; set; }

        public double IndirmePH { get; set; }
    }

    public class FSAPKY002_1_4asd
    {
        public double PihtiSuresi { get; set; }

        public double CedarlamaSuresi { get; set; }

        public double IndirmeSuresi { get; set; }

        public double ToplamSure { get; set; }

        public double TelemeRandimani { get; set; }  
    }

    public class FSAPKY002_2_1
    {
        public double TelemeProsesi { get; set; }

        public double TelemeMiktari { get; set; }

        public double TelemeProsesi2 { get; set; }

        public double TelemeMiktari2 { get; set; }

        public double HaslamaBaslangicPH { get; set; }

        public string BaslangicSaati { get; set; }

        public string BitisSaati { get; set; }

        public string HaslamaOperatoru { get; set; } 
    }

    public class FSAPKY002_2_2
    {
        public string UrunAdi { get; set; }
        public string Ornek1 { get; set; } 
        public string Ornek2 { get; set; } 
        public string Ornek3 { get; set; } 
        public string Ornek4 { get; set; } 
        public string Ornek5 { get; set; } 
        public string Ornek6 { get; set; } 
        public string Ornek7 { get; set; }  
        public string GramajOlcumOprt { get; set; }
    }

    public class FSAPKY002_3_1
    {
        public string UrunAdi { get; set; }

        public double PaketlemeSicakligi { get; set; }

        public string UretimTarihi { get; set; }

        public string SonTuketimTarihi { get; set; }

        public string PartiNumarasi { get; set; }

        public string OperatorAdi { get; set; } 
    }

    public class FSAPKY002_3_2
    {
        public string Saat { get; set; }

        public string UrunAdi { get; set; }

        public double BirinciGozCO2 { get; set; }

        public double BirinciGozO2 { get; set; }

        public double IkinciGozCO2 { get; set; }

        public double IkinciGozO2 { get; set; }

        public double UcuncuGozCO2 { get; set; }

        public double UcuncuGozO2 { get; set; }

        public double OperatorAdi { get; set; } 
    }

    public class FSAPKY002_3_3
    {
        public string UrunAdi { get; set; }
        public string Ornek1 { get; set; }
        public string Ornek2 { get; set; }
        public string Ornek3 { get; set; }
        public string Ornek4 { get; set; }
        public string Ornek5 { get; set; }
        public string Ornek6 { get; set; }
        public string Ornek7 { get; set; }
        public string Ornek8 { get; set; }
        public string Ornek9 { get; set; }
        public string Ornek10 { get; set; }
        public string OperatorAdi { get; set; }
    }

    public class FSAPKY002_3_4
    {
        public string UrunAdi { get; set; }

        public string DuyusalAnalizOnayi { get; set; }

        public string SorumluMuhendis { get; set; } 
    }
}