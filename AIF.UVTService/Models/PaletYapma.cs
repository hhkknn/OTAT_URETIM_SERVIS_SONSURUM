﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIF.UVTService.Models
{
    public class PaletYapma
    {
        public string UretimFisNo { get; set; }
        public string PaletNumarasi { get; set; }
        public string Durum { get; set; }
        public double ToplamKap { get; set; }
        public double NetKilo { get; set; }
        public double BrutKilo { get; set; }
        public string Tarih { get; set; }
        public string Depo { get; set; }
        public string DepoAdi { get; set; }
        public string DepoYeriId { get; set; }
        public string DepoYeriAdi { get; set; }
        //public string HedefDepo { get; set; }
        //public string HedefDepoYeriId { get; set; }

        public string SonGorulenDepoKodu { get; set; }
        public string SonGorulenDepoAdi { get; set; }
        public string SonGorulenDepoYeriId { get; set; }
        public string SonGorulenDepoYeriAdi { get; set; }

        public string MevcutDepoKodu { get; set; }
        public string MevcutDepoAdi { get; set; }
        public string MevcutDepoYeriId { get; set; }
        public string MevcutDepoYeriAdi { get; set; }
        public string PaletDurum { get; set; }
        public List<PaletYapmaDetay> paletYapmaDetays { get; set; }

    }
    public class PaletYapmaDetay
    {
        public int DocEntry { get; set; }

        public string Barkod { get; set; }

        public string MuhatapKatalogNo { get; set; }

        public string KalemKodu { get; set; }

        public string KalemTanimi { get; set; }

        public int SiparisNumarasi { get; set; }

        public int SiparisSatirNo { get; set; }

        public double Quantity { get; set; }

        public int CekmeNo { get; set; }

        public string Kaynak { get; set; }

        public string DepoKodu { get; set; }

        public string DepoAdi { get; set; }

        public string DepoYeriId { get; set; }

        public string DepoYeriAdi { get; set; }

        public string guid { get; set; }

        public string partili { get; set; }

        public string serili { get; set; }
        public string Transfer { get; set; }


        public List<PaletYapmaPartiler> PaletYapmaPartilers { get; set; }


    }
    public class PaletYapmaPartiler
    {
        public string Barkod { get; set; }
        public string KalemKodu { get; set; }
        public string KalemTanimi { get; set; }
        public string PartiNumarasi { get; set; }
        public double Miktar { get; set; }
        public string DepoKodu { get; set; }
        public string DepoAdi { get; set; }
        public string DepoYeriId { get; set; }
        public string DepoYeriAdi { get; set; }
        public string guid { get; set; }
        public string Transfer { get; set; }
    }
}