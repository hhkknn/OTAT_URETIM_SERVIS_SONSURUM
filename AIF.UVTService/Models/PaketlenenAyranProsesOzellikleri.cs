using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UVTService.Models
{
    public class PaketlenenAyranProsesOzellikleri
    {
        public string Tarih { get; set; }

        public string Aciklama { get; set; }

        public List<PaketlenenAyranProsesOzellikleri1> paketlenenAyranProsesOzellikleri1s { get; set; }

    }

    public class PaketlenenAyranProsesOzellikleri1
    {
        public string KontrolNo { get; set; }

        public string bir { get; set; }

        public string iki { get; set; }

        public string uc { get; set; }

        public string dort { get; set; }

        public string bes { get; set; }

        public string alti { get; set; }

        public string yedi { get; set; }

        public string sekiz { get; set; }

        public string dokuz { get; set; }

        public string on { get; set; }

        public string onbir { get; set; }

        public string oniki { get; set; }

        public string onuc { get; set; }

        public string ondort { get; set; }

        public string onbes { get; set; }

        public string onalti { get; set; }

    }
}