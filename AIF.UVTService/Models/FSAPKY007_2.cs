using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class FSAPKY007_2
    {
        public string PartiNo { get; set; }

        public string UrunKodu { get; set; }

        public string UrunTanimi { get; set; }

        public string Aciklama { get; set; }

        public string Tarih { get; set; }

        public string DigerKaliteKontroller { get; set; }

        public List<FSAPKY007_2_1_1> fSAPKY007_2_1_1s { get; set; }

        public List<FSAPKY007_2_1_2> fSAPKY007_2_1_2s { get; set; }

        public List<FSAPKY007_2_2_1> fSAPKY007_2_2_1s { get; set; }

        public List<FSAPKY007_2_2_2> fSAPKY007_2_2_2s { get; set; }
    }

    public class FSAPKY007_2_1_1
    {
        public string BaslangicSaati { get; set; }

        public double PastorizasyonSicakligi { get; set; }

        public double PastorizasyonCikis { get; set; }

        public int HolderSuresi { get; set; }

        public double HomojenizasyonBasinc { get; set; }

        public double SuluAyranSutuMiktari { get; set; }

        public double CekilenKremaMiktari { get; set; }

        public double StandartAyranSutuMiktari { get; set; }

        public string BitisSaati { get; set; }

        public string OperatorAdi { get; set; }

    }

    public class FSAPKY007_2_1_2
    {
        public string AyranProsessTankNo { get; set; }

        public double StandartAyranSutuMiktari { get; set; }

        public double KulturlemeSicakligi { get; set; }

        public string KulturlemeSaati { get; set; }

        public double KulturlemePH { get; set; }

        public string KirimSaati { get; set; }

        public double KirimPH { get; set; }

        public double KirimSH { get; set; }

        public double SogutmaEsanjoruCikisSicakligi { get; set; }

        public string LabPersonelAdi { get; set; }
    } 
    public class FSAPKY007_2_2_1
    {
        public string TankNo { get; set; }

        public string PaletNo { get; set; }

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

        public double BirinciAnalizSH { get; set; }

        public double IkinciAnalizSH { get; set; }

        public double UcuncuAnalizSH { get; set; }

        public double DorduncuAnalizSH { get; set; }

        public double BesinciAnalizSH { get; set; }
        public string OperatorAdi { get; set; }
    }
    public class FSAPKY007_2_2_2
    {
        public string TankNo { get; set; }

        public string Saat { get; set; }

        public double PH { get; set; }

        public double SH { get; set; } 

        public string OperatorAdi { get; set; }
    }
}