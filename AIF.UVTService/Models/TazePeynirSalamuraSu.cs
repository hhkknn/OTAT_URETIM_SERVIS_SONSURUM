using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class TazePeynirSalamuraSu
    {
        public string PartiNo { get; set; }

        public string UrunKodu { get; set; }

        public string UrunTanimi { get; set; }

        public string Aciklama { get; set; }

        public string UretimTarihi { get; set; }

        public List<SalamuraProsesOzellikleri1> salamuraProsesOzellikleri1s { get; set; }

        public List<SalamuraProsesOzellikleri2> salamuraProsesOzellikleri2s { get; set; } 

        public List<SalamuraOzellikleri> salamuraOzellikleris { get; set; }

        public List<SalamuraSarfMalzemeKullanim> salamuraSarfMalzemeKullanims { get; set; } 

    }

    public class SalamuraProsesOzellikleri1
    {
        public string SalamuraHazirlayanSorumlu { get; set; }
        public string OperasyonBaslangicSaati { get; set; }
        public double PastorizasyonSicakligi { get; set; }
        public string PastorizasyonBaslangicSaati { get; set; }
        public string PastorizasyonBitisSaati { get; set; }
        public string SalamuraTankFiltreKontrol { get; set; } 
        public double HazirlananSalamuraMiktari { get; set; } 
        public string OperasyonBitisSaati { get; set; }

    }

    public class SalamuraProsesOzellikleri2
    {  
        public double PastorizasyonSuresi { get; set; } 
        public double ToplamGecenSure { get; set; }  
    } 

    public class SalamuraOzellikleri
    {
        public double KullanilanHammeddeToplam{ get; set; }

        public double BomeDegeri { get; set; }

        public double PhDegeri { get; set; }
    } 

    public class SalamuraSarfMalzemeKullanim
    {
        public string MalzemeAdi { get; set; }

        public string MalzemeMarkaTedarikcisi { get; set; }

        public string SarfMalzemePartiNo { get; set; }

        public double Miktar { get; set; }

        public string Birim { get; set; }
    }

}