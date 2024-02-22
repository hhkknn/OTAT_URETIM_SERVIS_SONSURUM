using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class FSAPKY011_1
    {
        public string PartiNo { get; set; }

        public string UrunKodu { get; set; }

        public string UrunTanimi { get; set; }

        public string Aciklama { get; set; }

        public string Tarih { get; set; }

        public List<FSAPKY011_1_1> fSAPKY011_1_1s { get; set; }

        public List<FSAPKY011_1_2> fSAPKY011_1_2s { get; set; }
    }

    public class FSAPKY011_1_1
    {
        public double KullanilanKrema { get; set; }

        public double KullanilanSutMiktari { get; set; }

        public double KremaSonYagi { get; set; }

        public string PastorizasyonBaslangicSaati { get; set; }

        public double PastrizasyonSicakligi { get; set; }

        public string PastorizasyonBitisSaati { get; set; }

        public string OperatorAdi { get; set; }
    }
    public class FSAPKY011_1_2
    {
        public double KremaPH { get; set; }

        public double KremaYagOrani { get; set; }

        public double KremaSonYagOrani { get; set; }

        public string LabPersonelAdi { get; set; } 
    }

}