using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddOrUpdateAyranProsesAnaliz
    {
        public Response addOrUpdateAyranProsesAnaliz(AyranProsesTakipAnaliz ayranProsesTakipAnaliz, string dbName,string mKodValue)
        {
            Random rastgele = new Random();
            int ID = rastgele.Next(0, 9999);

            int clnum = 0;
            string dbCode = "";
            try
            {
                ConnectionList connection = new ConnectionList();

                LoginCompany log = new LoginCompany();

                log.DisconnectSAP(dbName);

                connection = log.getSAPConnection(dbName,ID);
 
                if (connection.number == -1)
                {
                    LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                    return new Response { Value = -3100, Description = "Hata Kodu - 3100 Veritabanı bağlantısı sırasında hata oluştu. ", List = null };
                }

                clnum = connection.number;
                dbCode = connection.dbCode;

                Company oCompany = connection.oCompany;

                Recordset oRS = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

                oRS.DoQuery("Select * from \"@AIF_AYRPRSS_ANLZ\" WITH (NOLOCK) where \"U_PartiNo\" = '" + ayranProsesTakipAnaliz.PartiNo + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChildProsesOzellikleri1;

                    GeneralDataCollection oChildrenProsesOzellikleri1;

                    GeneralData oChildProsesOzellikleri2;

                    GeneralDataCollection oChildrenProsesOzellikleri2;

                    GeneralData oChildPihtiKirimOzellikleri;

                    GeneralDataCollection oChildrenPihtiKirimOzellikleri;

                    GeneralData oChildBirinciKulturOzellikleri;

                    GeneralDataCollection oChildrenBirinciKulturOzellikleri;

                    GeneralData oChildIkinciKulturOzellikleri;

                    GeneralDataCollection oChildrenIkinciKulturOzellikleri;

                    GeneralData oChildSutunOzellikleri;

                    GeneralDataCollection oChildrenSutunOzellikleri;

                    GeneralData oChildTuzOzellikleri;

                    GeneralDataCollection oChildrenTuzOzellikleri;

                    GeneralData oChildYariMamulKaliteOzellikleri;

                    GeneralDataCollection oChildrenYariMamulKaliteOzellikleri;

                    GeneralData oChildInkubasyonTakipOzellikleri;

                    GeneralDataCollection oChildrenInkubasyonTakipOzellikleri;

                    oCompService = oCompany.GetCompanyService();

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_AYRPRSS_ANLZ");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralData.SetProperty("U_PartiNo", ayranProsesTakipAnaliz.PartiNo.ToString());

                    oGeneralData.SetProperty("U_KalemKodu", ayranProsesTakipAnaliz.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_KalemTanimi", ayranProsesTakipAnaliz.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Aciklama", ayranProsesTakipAnaliz.Aciklama.ToString());

                    if (ayranProsesTakipAnaliz.UretimTarihi != null && ayranProsesTakipAnaliz.UretimTarihi != "")
                    {
                        string tarih = ayranProsesTakipAnaliz.UretimTarihi;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_UretimTarihi", dt);

                    }

                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    //oGeneralData.SetProperty("U_Tarih", dt);

                    oChildrenProsesOzellikleri1 = oGeneralData.Child("AIF_AYRPRSS_ANLZ1");

                    foreach (var item in ayranProsesTakipAnaliz.ayranProsesOzellikleri1s)
                    {
                        oChildProsesOzellikleri1 = oChildrenProsesOzellikleri1.Add();

                        oChildProsesOzellikleri1.SetProperty("U_UrunGrubu", item.UrunGrubu);

                        oChildProsesOzellikleri1.SetProperty("U_ProsesBasSaat", item.ProsesBaslangicSaati);

                        oChildProsesOzellikleri1.SetProperty("U_YagliSutMik", item.YagliSutMiktari);

                        oChildProsesOzellikleri1.SetProperty("U_YagsizSutMik", item.YagsizSutMiktari);

                        oChildProsesOzellikleri1.SetProperty("U_ToplamSuMik", item.ToplamSuMiktari);

                        oChildProsesOzellikleri1.SetProperty("U_TuzMiktari", item.TuzMiktari);

                        oChildProsesOzellikleri1.SetProperty("U_AyranTuzOrani", item.AyranTuzOrani);

                        oChildProsesOzellikleri1.SetProperty("U_TopAyranYarMamulMik", item.ToplamAyranYariMamulMiktari);
                    }

                    oChildrenProsesOzellikleri2 = oGeneralData.Child("AIF_AYRPRSS_ANLZ2");

                    foreach (var item in ayranProsesTakipAnaliz.ayranProsesOzellikleri2s)
                    {
                        oChildProsesOzellikleri2 = oChildrenProsesOzellikleri2.Add();

                        oChildProsesOzellikleri2.SetProperty("U_PastorizasyonSicak", item.PastorizasyonSicakligi);

                        oChildProsesOzellikleri2.SetProperty("U_PastorizasyonSuresi", item.PastorizasyonSuresi);

                        oChildProsesOzellikleri2.SetProperty("U_SutVakum", item.SutVakum);

                        oChildProsesOzellikleri2.SetProperty("U_MayalamaSaati", item.MayalamaSaati);

                        oChildProsesOzellikleri2.SetProperty("U_MayalamaSicakligi", item.MayalamaSicakligi);

                        oChildProsesOzellikleri2.SetProperty("U_SorumluPersonel", item.SorumluPersonel);

                    }

                    oChildrenPihtiKirimOzellikleri = oGeneralData.Child("AIF_AYRPRSS_ANLZ3");

                    foreach (var item in ayranProsesTakipAnaliz.ayranPihtiKirims)
                    {
                        oChildPihtiKirimOzellikleri = oChildrenPihtiKirimOzellikleri.Add();

                        oChildPihtiKirimOzellikleri.SetProperty("U_PompaBasinci", item.PompaBasinci);

                        oChildPihtiKirimOzellikleri.SetProperty("U_BaslamaSaati", item.BaslamaSaati);

                        oChildPihtiKirimOzellikleri.SetProperty("U_BitisSaati", item.BitisSaati);

                        oChildPihtiKirimOzellikleri.SetProperty("U_ToplamGecenSure", item.ToplamGecenSure);
                    }



                    oChildrenBirinciKulturOzellikleri = oGeneralData.Child("AIF_AYRPRSS_ANLZ4");

                    foreach (var item in ayranProsesTakipAnaliz.ayranBirinciKulturs)
                    {
                        oChildBirinciKulturOzellikleri = oChildrenBirinciKulturOzellikleri.Add();

                        oChildBirinciKulturOzellikleri.SetProperty("U_KullanilanMik", item.KullanilanMiktar);

                        oChildBirinciKulturOzellikleri.SetProperty("U_TedAdiVeKultKod", item.TedarikciAdiveKulturKodu);

                        oChildBirinciKulturOzellikleri.SetProperty("U_LotNo", item.LotNo);
                    }


                    oChildrenIkinciKulturOzellikleri = oGeneralData.Child("AIF_AYRPRSS_ANLZ5");

                    foreach (var item in ayranProsesTakipAnaliz.ayranIkinciKulturs)
                    {
                        oChildIkinciKulturOzellikleri = oChildrenIkinciKulturOzellikleri.Add();

                        oChildIkinciKulturOzellikleri.SetProperty("U_KullanilanMik", item.KullanilanMiktar);

                        oChildIkinciKulturOzellikleri.SetProperty("U_TedAdiVeKultKod", item.TedarikciAdiveKulturKodu);

                        oChildIkinciKulturOzellikleri.SetProperty("U_LotNo", item.LotNo);
                    }


                    oChildrenTuzOzellikleri = oGeneralData.Child("AIF_AYRPRSS_ANLZ6");

                    foreach (var item in ayranProsesTakipAnaliz.ayranTuzOzellikleris)
                    {
                        oChildTuzOzellikleri = oChildrenTuzOzellikleri.Add();

                        oChildTuzOzellikleri.SetProperty("U_TedarikciAdi", item.TedarikciAdi);

                        oChildTuzOzellikleri.SetProperty("U_PartiNo", item.PartiNo);
                    }

                    oChildrenSutunOzellikleri = oGeneralData.Child("AIF_AYRPRSS_ANLZ7");

                    foreach (var item in ayranProsesTakipAnaliz.ayranSutOzellikleris)
                    {
                        oChildSutunOzellikleri = oChildrenSutunOzellikleri.Add();

                        oChildSutunOzellikleri.SetProperty("U_SutunKaynagi", item.SutunKaynagi);

                        oChildSutunOzellikleri.SetProperty("U_SutTuru", item.SutTuru);

                        oChildSutunOzellikleri.SetProperty("U_Brix", item.Brix);

                        oChildSutunOzellikleri.SetProperty("U_PH", item.PH);

                        oChildSutunOzellikleri.SetProperty("U_SH", item.SH);

                        oChildSutunOzellikleri.SetProperty("U_Yag", item.Yag);

                    }



                    oChildrenYariMamulKaliteOzellikleri = oGeneralData.Child("AIF_AYRPRSS_ANLZ8");

                    foreach (var item in ayranProsesTakipAnaliz.ayranYariMamulKaliteOzellikleris)
                    {
                        oChildYariMamulKaliteOzellikleri = oChildrenYariMamulKaliteOzellikleri.Add();

                        oChildYariMamulKaliteOzellikleri.SetProperty("U_PartiNo", item.PartiNo);

                        oChildYariMamulKaliteOzellikleri.SetProperty("U_TKM", item.TKM);

                        //oChildYariMamulKaliteOzellikleri.SetProperty("U_Protein", item.Protein);

                        //oChildYariMamulKaliteOzellikleri.SetProperty("U_Tuz", item.Tuz);

                        oChildYariMamulKaliteOzellikleri.SetProperty("U_Brix", item.Brix);

                        oChildYariMamulKaliteOzellikleri.SetProperty("U_PH", item.PH);

                        oChildYariMamulKaliteOzellikleri.SetProperty("U_SH", item.SH);

                        oChildYariMamulKaliteOzellikleri.SetProperty("U_Yag", item.Yag);

                        //oChildYariMamulKaliteOzellikleri.SetProperty("U_TatKokuKivam", item.TatKokuKivam);

                    }



                    oChildrenInkubasyonTakipOzellikleri = oGeneralData.Child("AIF_AYRPRSS_ANLZ9");

                    foreach (var item in ayranProsesTakipAnaliz.ayranInkubasyonOzellikleris)
                    {
                        oChildInkubasyonTakipOzellikleri = oChildrenInkubasyonTakipOzellikleri.Add();

                        oChildInkubasyonTakipOzellikleri.SetProperty("U_KontrolNo", item.KontrolNo);

                        oChildInkubasyonTakipOzellikleri.SetProperty("U_Saat", item.Saat);

                        //oChildInkubasyonTakipOzellikleri.SetProperty("U_Sicaklik", item.Sicaklik);

                        oChildInkubasyonTakipOzellikleri.SetProperty("U_UrunSicakligi", item.UrunSicakligi);

                        oChildInkubasyonTakipOzellikleri.SetProperty("U_OdaSicakligi", item.OdaSicakligi);

                        oChildInkubasyonTakipOzellikleri.SetProperty("U_PH", item.PH);

                        oChildInkubasyonTakipOzellikleri.SetProperty("U_KontrolEdenPers", item.KontrolEdenPersonel);

                    }


                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_AYRPRSS_ANLZ\" WITH (NOLOCK)");

                    int maxdocentry = Convert.ToInt32(oRS.Fields.Item(0).Value);

                    oGeneralData.SetProperty("DocNum", maxdocentry);

                    var resp = oGeneralService.Add(oGeneralData);

                    if (resp != null)
                    {
                        //if (oCompany.InTransaction)
                        //{
                        //    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                        //}
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = 0, Description = "Ayran Proses Analiz Takibi girişi oluşturuldu..", List = null };
                    }
                    else
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5200, Description = "Hata Kodu - 5200 Ayran Proses Analiz Takibi girişi oluşturulurken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                    }
                }
                else
                {


                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChildProsesOzellikleri1;

                    GeneralDataCollection oChildrenProsesOzellikleri1;

                    GeneralData oChildProsesOzellikleri2;

                    GeneralDataCollection oChildrenProsesOzellikleri2;

                    GeneralData oChildPihtiKirimOzellikleri;

                    GeneralDataCollection oChildrenPihtiKirimOzellikleri;

                    GeneralData oChildBirinciKulturOzellikleri;

                    GeneralDataCollection oChildrenBirinciKulturOzellikleri;

                    GeneralData oChildIkinciKulturOzellikleri;

                    GeneralDataCollection oChildrenIkinciKulturOzellikleri;

                    GeneralData oChildSutunOzellikleri;

                    GeneralDataCollection oChildrenSutunOzellikleri;

                    GeneralData oChildTuzOzellikleri;

                    GeneralDataCollection oChildrenTuzOzellikleri;

                    GeneralData oChildYariMamulKaliteOzellikleri;

                    GeneralDataCollection oChildrenYariMamulKaliteOzellikleri;

                    GeneralData oChildInkubasyonTakipOzellikleri;

                    GeneralDataCollection oChildrenInkubasyonTakipOzellikleri;

                    oCompService = oCompany.GetCompanyService();

                    GeneralDataParams oGeneralParams;

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_AYRPRSS_ANLZ");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    oGeneralData.SetProperty("U_PartiNo", ayranProsesTakipAnaliz.PartiNo.ToString());

                    oGeneralData.SetProperty("U_KalemKodu", ayranProsesTakipAnaliz.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_KalemTanimi", ayranProsesTakipAnaliz.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Aciklama", ayranProsesTakipAnaliz.Aciklama.ToString());

                    if (ayranProsesTakipAnaliz.UretimTarihi != null && ayranProsesTakipAnaliz.UretimTarihi != "")
                    {
                        string tarih = ayranProsesTakipAnaliz.UretimTarihi;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));
                        oGeneralData.SetProperty("U_UretimTarihi", dt);
                    }


                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    //oGeneralData.SetProperty("U_Tarih", dt);

                    oChildrenProsesOzellikleri1 = oGeneralData.Child("AIF_AYRPRSS_ANLZ1");

                    if (oChildrenProsesOzellikleri1.Count > 0)
                    {
                        int drc = oChildrenProsesOzellikleri1.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenProsesOzellikleri1.Remove(0);
                    }

                    foreach (var item in ayranProsesTakipAnaliz.ayranProsesOzellikleri1s)
                    {
                        oChildProsesOzellikleri1 = oChildrenProsesOzellikleri1.Add();

                        oChildProsesOzellikleri1.SetProperty("U_UrunGrubu", item.UrunGrubu);

                        oChildProsesOzellikleri1.SetProperty("U_ProsesBasSaat", item.ProsesBaslangicSaati);

                        oChildProsesOzellikleri1.SetProperty("U_YagliSutMik", item.YagliSutMiktari);

                        oChildProsesOzellikleri1.SetProperty("U_YagsizSutMik", item.YagsizSutMiktari);

                        oChildProsesOzellikleri1.SetProperty("U_ToplamSuMik", item.ToplamSuMiktari);

                        oChildProsesOzellikleri1.SetProperty("U_TuzMiktari", item.TuzMiktari);

                        oChildProsesOzellikleri1.SetProperty("U_AyranTuzOrani", item.AyranTuzOrani);

                        oChildProsesOzellikleri1.SetProperty("U_TopAyranYarMamulMik", item.ToplamAyranYariMamulMiktari);
                    }

                    oChildrenProsesOzellikleri2 = oGeneralData.Child("AIF_AYRPRSS_ANLZ2");

                    if (oChildrenProsesOzellikleri2.Count > 0)
                    {
                        int drc = oChildrenProsesOzellikleri2.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenProsesOzellikleri2.Remove(0);
                    }

                    foreach (var item in ayranProsesTakipAnaliz.ayranProsesOzellikleri2s)
                    {
                        oChildProsesOzellikleri2 = oChildrenProsesOzellikleri2.Add();

                        oChildProsesOzellikleri2.SetProperty("U_PastorizasyonSicak", item.PastorizasyonSicakligi);

                        oChildProsesOzellikleri2.SetProperty("U_PastorizasyonSuresi", item.PastorizasyonSuresi);

                        oChildProsesOzellikleri2.SetProperty("U_SutVakum", item.SutVakum);

                        oChildProsesOzellikleri2.SetProperty("U_MayalamaSaati", item.MayalamaSaati);

                        oChildProsesOzellikleri2.SetProperty("U_MayalamaSicakligi", item.MayalamaSicakligi);

                        oChildProsesOzellikleri2.SetProperty("U_SorumluPersonel", item.SorumluPersonel);

                    }


                    oChildrenPihtiKirimOzellikleri = oGeneralData.Child("AIF_AYRPRSS_ANLZ3");

                    if (oChildrenPihtiKirimOzellikleri.Count > 0)
                    {
                        int drc = oChildrenPihtiKirimOzellikleri.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenPihtiKirimOzellikleri.Remove(0);
                    }

                    foreach (var item in ayranProsesTakipAnaliz.ayranPihtiKirims)
                    {
                        oChildPihtiKirimOzellikleri = oChildrenPihtiKirimOzellikleri.Add();

                        oChildPihtiKirimOzellikleri.SetProperty("U_PompaBasinci", item.PompaBasinci);

                        oChildPihtiKirimOzellikleri.SetProperty("U_BaslamaSaati", item.BaslamaSaati);

                        oChildPihtiKirimOzellikleri.SetProperty("U_BitisSaati", item.BitisSaati);

                        oChildPihtiKirimOzellikleri.SetProperty("U_ToplamGecenSure", item.ToplamGecenSure);
                    }



                    oChildrenBirinciKulturOzellikleri = oGeneralData.Child("AIF_AYRPRSS_ANLZ4");


                    if (oChildrenBirinciKulturOzellikleri.Count > 0)
                    {
                        int drc = oChildrenBirinciKulturOzellikleri.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenBirinciKulturOzellikleri.Remove(0);
                    }

                    foreach (var item in ayranProsesTakipAnaliz.ayranBirinciKulturs)
                    {
                        oChildBirinciKulturOzellikleri = oChildrenBirinciKulturOzellikleri.Add();

                        oChildBirinciKulturOzellikleri.SetProperty("U_KullanilanMik", item.KullanilanMiktar);

                        oChildBirinciKulturOzellikleri.SetProperty("U_TedAdiVeKultKod", item.TedarikciAdiveKulturKodu);

                        oChildBirinciKulturOzellikleri.SetProperty("U_LotNo", item.LotNo);
                    }


                    oChildrenIkinciKulturOzellikleri = oGeneralData.Child("AIF_AYRPRSS_ANLZ5");

                    if (oChildrenIkinciKulturOzellikleri.Count > 0)
                    {
                        int drc = oChildrenIkinciKulturOzellikleri.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenIkinciKulturOzellikleri.Remove(0);
                    }

                    foreach (var item in ayranProsesTakipAnaliz.ayranIkinciKulturs)
                    {
                        oChildIkinciKulturOzellikleri = oChildrenIkinciKulturOzellikleri.Add();

                        oChildIkinciKulturOzellikleri.SetProperty("U_KullanilanMik", item.KullanilanMiktar);

                        oChildIkinciKulturOzellikleri.SetProperty("U_TedAdiVeKultKod", item.TedarikciAdiveKulturKodu);

                        oChildIkinciKulturOzellikleri.SetProperty("U_LotNo", item.LotNo);
                    }


                    oChildrenTuzOzellikleri = oGeneralData.Child("AIF_AYRPRSS_ANLZ6");

                    if (oChildrenTuzOzellikleri.Count > 0)
                    {
                        int drc = oChildrenTuzOzellikleri.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenTuzOzellikleri.Remove(0);
                    }

                    foreach (var item in ayranProsesTakipAnaliz.ayranTuzOzellikleris)
                    {
                        oChildTuzOzellikleri = oChildrenTuzOzellikleri.Add();

                        oChildTuzOzellikleri.SetProperty("U_TedarikciAdi", item.TedarikciAdi);

                        oChildTuzOzellikleri.SetProperty("U_PartiNo", item.PartiNo);
                    }


                    oChildrenSutunOzellikleri = oGeneralData.Child("AIF_AYRPRSS_ANLZ7");

                    if (oChildrenSutunOzellikleri.Count > 0)
                    {
                        int drc = oChildrenSutunOzellikleri.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenSutunOzellikleri.Remove(0);
                    }

                    foreach (var item in ayranProsesTakipAnaliz.ayranSutOzellikleris)
                    {
                        oChildSutunOzellikleri = oChildrenSutunOzellikleri.Add();

                        oChildSutunOzellikleri.SetProperty("U_SutTuru", item.SutTuru);

                        oChildSutunOzellikleri.SetProperty("U_SutunKaynagi", item.SutunKaynagi);

                        oChildSutunOzellikleri.SetProperty("U_Brix", item.Brix);

                        oChildSutunOzellikleri.SetProperty("U_PH", item.PH);

                        oChildSutunOzellikleri.SetProperty("U_SH", item.SH);

                        oChildSutunOzellikleri.SetProperty("U_Yag", item.Yag);

                    }


                    oChildrenYariMamulKaliteOzellikleri = oGeneralData.Child("AIF_AYRPRSS_ANLZ8");

                    if (oChildrenYariMamulKaliteOzellikleri.Count > 0)
                    {
                        int drc = oChildrenYariMamulKaliteOzellikleri.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenYariMamulKaliteOzellikleri.Remove(0);
                    }

                    foreach (var item in ayranProsesTakipAnaliz.ayranYariMamulKaliteOzellikleris)
                    {
                        oChildYariMamulKaliteOzellikleri = oChildrenYariMamulKaliteOzellikleri.Add();

                        oChildYariMamulKaliteOzellikleri.SetProperty("U_PartiNo", item.PartiNo);

                        oChildYariMamulKaliteOzellikleri.SetProperty("U_TKM", item.TKM);

                        //oChildYariMamulKaliteOzellikleri.SetProperty("U_Protein", item.Protein);

                        //oChildYariMamulKaliteOzellikleri.SetProperty("U_Tuz", item.Tuz);

                        oChildYariMamulKaliteOzellikleri.SetProperty("U_Brix", item.Brix);

                        oChildYariMamulKaliteOzellikleri.SetProperty("U_PH", item.PH);

                        oChildYariMamulKaliteOzellikleri.SetProperty("U_SH", item.SH);

                        oChildYariMamulKaliteOzellikleri.SetProperty("U_Yag", item.Yag);

                        //oChildYariMamulKaliteOzellikleri.SetProperty("U_TatKokuKivam", item.TatKokuKivam);

                    }


                    oChildrenInkubasyonTakipOzellikleri = oGeneralData.Child("AIF_AYRPRSS_ANLZ9");

                    if (oChildrenInkubasyonTakipOzellikleri.Count > 0)
                    {
                        int drc = oChildrenInkubasyonTakipOzellikleri.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenInkubasyonTakipOzellikleri.Remove(0);
                    }

                    foreach (var item in ayranProsesTakipAnaliz.ayranInkubasyonOzellikleris)
                    {
                        oChildInkubasyonTakipOzellikleri = oChildrenInkubasyonTakipOzellikleri.Add();

                        oChildInkubasyonTakipOzellikleri.SetProperty("U_KontrolNo", item.KontrolNo);

                        oChildInkubasyonTakipOzellikleri.SetProperty("U_Saat", item.Saat);

                        //oChildInkubasyonTakipOzellikleri.SetProperty("U_Sicaklik", item.Sicaklik);

                        oChildInkubasyonTakipOzellikleri.SetProperty("U_UrunSicakligi", item.UrunSicakligi);

                        oChildInkubasyonTakipOzellikleri.SetProperty("U_OdaSicakligi", item.OdaSicakligi);

                        oChildInkubasyonTakipOzellikleri.SetProperty("U_PH", item.PH);

                        oChildInkubasyonTakipOzellikleri.SetProperty("U_KontrolEdenPers", item.KontrolEdenPersonel);

                    }

                    try
                    {
                        oGeneralService.Update(oGeneralData);
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = 0, Description = "Ayran Proses Analiz Takibi girişi güncellendi.", List = null };
                    }
                    catch (Exception)
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5300, Description = "Hata Kodu - 5300 Ayran Proses Analiz Takibi girişi güncellenirken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                    }

                }
            }
            catch (Exception ex)
            {

                LoginCompany.ReleaseConnection(clnum, dbCode,ID);
                return new Response { Value = -9000, Description = "Bilinmeyen Hata oluştu. " + ex.Message, List = null };
            }
            finally
            {
                LoginCompany.ReleaseConnection(clnum, dbCode, ID);
            }
        }
    }
}