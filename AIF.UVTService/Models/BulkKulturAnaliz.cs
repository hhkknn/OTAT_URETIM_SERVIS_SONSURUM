using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class BulkKulturAnaliz
    {
        public string PartiNo { get; set; }

        public string UrunKodu { get; set; }

        public string UrunTanimi { get; set; }

        public string Aciklama { get; set; }

        public string UretimTarihi { get; set; }

        public List<BulkProsesOzellikleri1> bulkProsesOzellikleri1s { get; set; }

        public List<BulkProsesOzellikleri2> bulkProsesOzellikleri2s { get; set; } 

        public List<BulkKulturOzellikleri> bulkKulturOzellikleris { get; set; }

        public List<BulkSarfMalzemeKullanim> bulkSarfMalzemeKullanims { get; set; } 

    }

    public class BulkProsesOzellikleri1
    {
        public string OperatorAdi { get; set; }
        public string OperasyonBaslangicSaati { get; set; }
        public double PastorizasyonSicakligi { get; set; }
        public string PastorizasyonBaslangicSaati { get; set; }
        public string PastorizasyonBitisSaati { get; set; }
        public double MayalamaSicakligi { get; set; }
        public string MayalamaSaati { get; set; }
        public string InkubasyonSonlandirmaSaati { get; set; }
        public double InkubasyonSonlandirmaPh { get; set; }
        public double HazirlananKulturMiktari { get; set; }
        public string OperasyonBitisSaati { get; set; }

    }

    public class BulkProsesOzellikleri2
    {  
        public double PastorizasyonSuresi { get; set; }
        public double InkubasyonSuresi { get; set; }  
        public double ToplamGecenSure { get; set; }  
    } 

    public class BulkKulturOzellikleri
    {
        public double KullanilanHammeddeToplam{ get; set; }

        public double KuruMadde { get; set; }

        public double PhDegeri { get; set; }
    } 

    public class BulkSarfMalzemeKullanim
    {
        public string MalzemeAdi { get; set; }

        public string MalzemeMarkaTedarikcisi { get; set; }

        public string SarfMalzemePartiNo { get; set; }

        public double Miktar { get; set; }

        public string Birim { get; set; }
    }

}