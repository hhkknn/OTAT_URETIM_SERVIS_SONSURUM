using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class FSAPKY012_1
    {
        public string PartiNo { get; set; }

        public string UrunKodu { get; set; }

        public string UrunTanimi { get; set; }

        public string Aciklama { get; set; }

        public string Tarih { get; set; }

        public string DigerKontrol { get; set; }

        public List<FSAPKY012_1_1> fSAPKY012_1_1s { get; set; }

        public List<FSAPKY012_1_2> fSAPKY012_1_2s { get; set; }
    }

    public class FSAPKY012_1_1
    {
        public double CigKremeMiktari { get; set; }

        public double KremaPastorizasyon { get; set; }

        public string PastorizasyonBaslangicSaati { get; set; }

        public string PastorizasyonBitisSaati { get; set; }

        public double MayalamaSicakligi { get; set; }

        public double KulturEklemeOrani { get; set; }

        public string TereyagOperatorAdi { get; set; }
    }
    public class FSAPKY012_1_2
    {
        public double CigKremaninYagOrani { get; set; }

        public double CigKremaPH { get; set; }

        public double PastorizeKremaninYagOrani { get; set; }

        public double PastorizeKremaPH { get; set; }

        public string LabPersonelAdi { get; set; }
    }

}