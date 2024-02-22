using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class FSAPKY008_1
    {
        public string PartiNo { get; set; }

        public string UrunKodu { get; set; }

        public string UrunTanimi { get; set; }

        public string Aciklama { get; set; }

        public string Tarih { get; set; }

        public List<FSAPKY008_1_1> fSAPKY008_1_1s { get; set; } 
    }

    public class FSAPKY008_1_1
    {
        public double PasMiktari { get; set; }

        public string BaslangicSaati { get; set; }

        public double PisirimSicakligi { get; set; }

        public string IndirmeSaati { get; set; }

        public double BaskilamaSuresi { get; set; }

        public string OperatorAdi { get; set; }

        public double PasYagi { get; set; }

        public double PasPH { get; set; }

        public string TeknikerAdi { get; set; } 
    }
     
}