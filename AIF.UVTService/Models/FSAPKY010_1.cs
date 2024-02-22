using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class FSAPKY010_1
    {
        public string PartiNo { get; set; }

        public string UrunKodu { get; set; }

        public string UrunTanimi { get; set; }

        public string Aciklama { get; set; }

        public string Tarih { get; set; }

        public List<FSAPKY010_1_1> fSAPKY010_1_1s { get; set; } 
    }

    public class FSAPKY010_1_1
    {
        public double KullanilanKrema { get; set; }

        public double KremaYagOrani { get; set; }

        public double KremaSicakligi { get; set; }

        public string YayiklamaBaslangicSaati { get; set; }

        public string YayiklamaBitisSaati { get; set; }

        public double TereyagRandimani { get; set; }

        public string OperatorAdi { get; set; } 
    }
     
}