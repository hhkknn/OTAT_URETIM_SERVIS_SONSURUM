using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class FSAPKY003_1
    {
        public string PartiNo { get; set; }

        public string UrunKodu { get; set; }

        public string UrunTanimi { get; set; }

        public string Aciklama { get; set; }

        public string Tarih { get; set; }
        public string Kontrol1 { get; set; }

        public string Kontrol2 { get; set; }

        public List<FSAPKY003_1_1> fSAPKY003_1_1s { get; set; }
        public List<FSAPKY003_1_2> fSAPKY003_1_2s { get; set; }
        public List<FSAPKY003_1_3> fSAPKY003_1_3s { get; set; }
    }

    public class FSAPKY003_1_1
    {
        public double UniteSicakligi { get; set; }

        public string TekneNumarasi { get; set; }

        public string UretilenUrunAdi { get; set; }

        public double AlinanSutMiktari { get; set; }

        public double TekneSutuSicakligi { get; set; }

        public string MayalamaSaati { get; set; }

        public string KirimSaati { get; set; }

        public string BaskilamaBasSaat { get; set; } 
        public string BaskilamaSonSaat { get; set; }

        public string KesmeSaati { get; set; }

        public double KesmePH { get; set; }

        public string SalamuraIlaveSaati { get; set; }

        public string PeynirTopSaat { get; set; }

        public double PeynirTopPH { get; set; }

        public string OperatorAdi { get; set; }
    }

    public class FSAPKY003_1_2
    {
        public double TekneSalamuraPH { get; set; }

        public double TekneSalamuraBolme { get; set; }

        public double TekneSalamuraSicakligi { get; set; }
    }

    public class FSAPKY003_1_3
    {
        public string NumuneNumarasi { get; set; }

        public double BeyazPeynirSutPH { get; set; }

        public double BeyazPeynirSutSH { get; set; }

        public double BeyazPeynirSutKM { get; set; }

        public double BeyazPeynirSutYagi { get; set; }

        public string OperatorAdi { get; set; }

    }

    
}