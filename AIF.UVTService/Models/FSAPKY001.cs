using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class FSAPKY001
    {
        public string PartiNo { get; set; }

        public string UrunKodu { get; set; }

        public string UrunTanimi { get; set; }

        public string Aciklama { get; set; }

        public string Tarih { get; set; }

        public List<FSAPKY001_1> fSAPKY001_1s { get; set; }

        public List<FSAPKY001_2> fSAPKY001_2s { get; set; }
    }

    public class FSAPKY001_1
    {
        public string BaslangicSaati { get; set; }

        public double PastorizasyonSicaklik { get; set; }

        public double PastorizasyonCikis { get; set; }

        public double HolderSuresi { get; set; }

        public double HomojenizasyonBasinc { get; set; }

        public double CigSutMiktari { get; set; }

        public double CekilenKrema { get; set; }

        public double EklenenKrema { get; set; }

        public double StandartSutMiktari { get; set; }

        public string BitisSaati { get; set; }

        public string OperatorAdi { get; set; }
    }

    public class FSAPKY001_2
    {
        public string CigSutDepoTanki { get; set; }

        public double CigSutMiktari { get; set; }

        public double CigSutPH { get; set; }

        public double CigSutSH { get; set; }

        public double CigSutKM { get; set; }

        public double CigSutYag { get; set; } 

        public string OperatorAdi { get; set; } 
    }
}