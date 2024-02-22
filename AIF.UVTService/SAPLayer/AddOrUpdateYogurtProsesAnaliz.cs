using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddOrUpdateYogurtProsesAnaliz
    {
        public Response addOrUpdateYogurtProsesAnaliz(YogurtProsesTakipAnaliz yogurtProsesTakipAnaliz, string dbName, string mKodValue)
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

                oRS.DoQuery("Select * from \"@AIF_YGRPRSS_ANLZ\" WITH (NOLOCK) where \"U_PartiNo\" = '" + yogurtProsesTakipAnaliz.PartiNo + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChildProsesOzellikleri1;

                    GeneralDataCollection oChildrenProsesOzellikleri1;

                    GeneralData oChildProsesOzellikleri2;

                    GeneralDataCollection oChildrenProsesOzellikleri2;

                    GeneralData oChildProsesOzellikleri3;

                    GeneralDataCollection oChildrenProsesOzellikleri3;

                    GeneralData oChildBirinciKulturOzellikleri;

                    GeneralDataCollection oChildrenBirinciKulturOzellikleri;

                    GeneralData oChildIkinciKulturOzellikleri;

                    GeneralDataCollection oChildrenIkinciKulturOzellikleri;

                    GeneralData oChildSutunOzellikleri;

                    GeneralDataCollection oChildrenSutunOzellikleri;

                    GeneralData oChildYariMamulKaliteOzellikleri;

                    GeneralDataCollection oChildrenYariMamulKaliteOzellikleri;

                    GeneralData oChildInkubasyonTakipOzellikleri;

                    GeneralDataCollection oChildrenInkubasyonTakipOzellikleri;

                    oCompService = oCompany.GetCompanyService();

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_YGRPRSS_ANLZ");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralData.SetProperty("U_PartiNo", yogurtProsesTakipAnaliz.PartiNo.ToString());

                    oGeneralData.SetProperty("U_KalemKodu", yogurtProsesTakipAnaliz.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_KalemTanimi", yogurtProsesTakipAnaliz.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Aciklama", yogurtProsesTakipAnaliz.Aciklama.ToString());

                    if (yogurtProsesTakipAnaliz.UretimTarihi != null && yogurtProsesTakipAnaliz.UretimTarihi != "")
                    {
                        string tarih = yogurtProsesTakipAnaliz.UretimTarihi;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_UretimTarihi", dt);

                    }

                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    //oGeneralData.SetProperty("U_Tarih", dt);

                    oChildrenProsesOzellikleri1 = oGeneralData.Child("AIF_YGRPRSS_ANLZ1");

                    foreach (var item in yogurtProsesTakipAnaliz.YogurtProsesOzellikleri1s)
                    {
                        oChildProsesOzellikleri1 = oChildrenProsesOzellikleri1.Add();

                        oChildProsesOzellikleri1.SetProperty("U_UrunGrubu", item.UrunGrubu);

                        oChildProsesOzellikleri1.SetProperty("U_InkubasyonOdaNo", item.InkubasyonNo);

                        oChildProsesOzellikleri1.SetProperty("U_ProsesBasSaat", item.ProsesBaslangicSaati);

                        oChildProsesOzellikleri1.SetProperty("U_YagliSutMik", item.YagliSutMiktari);

                        oChildProsesOzellikleri1.SetProperty("U_YagsizSutMik", item.YagsizSutMiktari);

                        oChildProsesOzellikleri1.SetProperty("U_KremaMik", item.KullanilanKremaMiktari);
                    }

                    oChildrenProsesOzellikleri2 = oGeneralData.Child("AIF_YGRPRSS_ANLZ2");

                    foreach (var item in yogurtProsesTakipAnaliz.YogurtProsesOzellikleri2s)
                    {
                        oChildProsesOzellikleri2 = oChildrenProsesOzellikleri2.Add();

                        oChildProsesOzellikleri2.SetProperty("U_VakumBasSaat", item.VakumBaslangicSaati);

                        oChildProsesOzellikleri2.SetProperty("U_VakumBitSaat", item.VakumBitisSaati);

                        oChildProsesOzellikleri2.SetProperty("U_Brix", item.VakumSonuSutBrixDegeri);

                        oChildProsesOzellikleri2.SetProperty("U_PastorizasyonSicak", item.PastorizasyonSicakligi);

                        oChildProsesOzellikleri2.SetProperty("U_PastorizasyonSure", item.PastorizasyonSuresi);

                        oChildProsesOzellikleri2.SetProperty("U_MayalamaSaati", item.MayalamaSaati);

                        oChildProsesOzellikleri2.SetProperty("U_MayalamaPH", item.MayalamaPHDegeri);

                        oChildProsesOzellikleri2.SetProperty("U_MayalamaSicakligi", item.MayalamaSicakligi);
                    }

                    oChildrenProsesOzellikleri3 = oGeneralData.Child("AIF_YGRPRSS_ANLZ3");

                    foreach (var item in yogurtProsesTakipAnaliz.YogurtProsesOzellikleri3s)
                    {
                        oChildProsesOzellikleri3 = oChildrenProsesOzellikleri3.Add();

                        oChildProsesOzellikleri3.SetProperty("U_DolumBasSaat", item.DolumBaslangicSaati);

                        oChildProsesOzellikleri3.SetProperty("U_DolumBasPH", item.DolumBaslangicindakiPHDegeri);

                        oChildProsesOzellikleri3.SetProperty("U_DolumBasSicak", item.DolumBaslangicindakiSutSicakligi);

                        oChildProsesOzellikleri3.SetProperty("U_DolumBitSaat", item.DolumBitisSaati);

                        oChildProsesOzellikleri3.SetProperty("U_DolumBitPH", item.DolumBittigindekiPHDegeri);

                        oChildProsesOzellikleri3.SetProperty("U_DolumBitSicak", item.DolumBittigindekiSutSicakligi);

                        oChildProsesOzellikleri3.SetProperty("U_SorumluPersonel", item.SorumluPersonel);

                        oChildProsesOzellikleri3.SetProperty("U_ToplamGecenSure", item.ToplamGecenSure);
                    }

                    oChildrenBirinciKulturOzellikleri = oGeneralData.Child("AIF_YGRPRSS_ANLZ4");

                    foreach (var item in yogurtProsesTakipAnaliz.YogurtBirinciKulturs)
                    {
                        oChildBirinciKulturOzellikleri = oChildrenBirinciKulturOzellikleri.Add();

                        oChildBirinciKulturOzellikleri.SetProperty("U_KullanilanMik", item.KullanilanMiktar);

                        oChildBirinciKulturOzellikleri.SetProperty("U_TedAdiVeKultKod", item.TedarikciAdiveKulturKodu);

                        oChildBirinciKulturOzellikleri.SetProperty("U_LotNo", item.LotNo);
                    }

                    oChildrenIkinciKulturOzellikleri = oGeneralData.Child("AIF_YGRPRSS_ANLZ5");

                    foreach (var item in yogurtProsesTakipAnaliz.YogurtIkinciKulturs)
                    {
                        oChildIkinciKulturOzellikleri = oChildrenIkinciKulturOzellikleri.Add();

                        oChildIkinciKulturOzellikleri.SetProperty("U_KullanilanMik", item.KullanilanMiktar);

                        oChildIkinciKulturOzellikleri.SetProperty("U_TedAdiVeKultKod", item.TedarikciAdiveKulturKodu);

                        oChildIkinciKulturOzellikleri.SetProperty("U_LotNo", item.LotNo);
                    }

                    oChildrenSutunOzellikleri = oGeneralData.Child("AIF_YGRPRSS_ANLZ6");

                    foreach (var item in yogurtProsesTakipAnaliz.YogurtSutOzellikleris)
                    {
                        oChildSutunOzellikleri = oChildrenSutunOzellikleri.Add();

                        oChildSutunOzellikleri.SetProperty("U_SutTuru", item.SutTuru);

                        oChildSutunOzellikleri.SetProperty("U_SutunKaynagi", item.SutunKaynagi);

                        oChildSutunOzellikleri.SetProperty("U_Brix", item.Brix);

                        oChildSutunOzellikleri.SetProperty("U_PH", item.PH);

                        oChildSutunOzellikleri.SetProperty("U_SH", item.SH);

                        oChildSutunOzellikleri.SetProperty("U_Yag", item.Yag);
                    }

                    oChildrenYariMamulKaliteOzellikleri = oGeneralData.Child("AIF_YGRPRSS_ANLZ7");

                    foreach (var item in yogurtProsesTakipAnaliz.YogurtYariMamulKaliteOzellikleris)
                    {
                        oChildYariMamulKaliteOzellikleri = oChildrenYariMamulKaliteOzellikleri.Add();

                        oChildYariMamulKaliteOzellikleri.SetProperty("U_PartiNo", item.PartiNo);

                        oChildYariMamulKaliteOzellikleri.SetProperty("U_TKM", item.TKM);

                        oChildYariMamulKaliteOzellikleri.SetProperty("U_Protein", item.Protein);

                        oChildYariMamulKaliteOzellikleri.SetProperty("U_Tuz", item.Tuz);

                        oChildYariMamulKaliteOzellikleri.SetProperty("U_Brix", item.Brix);

                        oChildYariMamulKaliteOzellikleri.SetProperty("U_PH", item.PH);

                        oChildYariMamulKaliteOzellikleri.SetProperty("U_SH", item.SH);

                        oChildYariMamulKaliteOzellikleri.SetProperty("U_Yag", item.Yag);

                        oChildYariMamulKaliteOzellikleri.SetProperty("U_TatKokuKivam", item.TatKokuKivam);
                    }

                    oChildrenInkubasyonTakipOzellikleri = oGeneralData.Child("AIF_YGRPRSS_ANLZ8");

                    foreach (var item in yogurtProsesTakipAnaliz.YogurtInkubasyonOzellikleris)
                    {
                        oChildInkubasyonTakipOzellikleri = oChildrenInkubasyonTakipOzellikleri.Add();

                        oChildInkubasyonTakipOzellikleri.SetProperty("U_KontrolNo", item.KontrolNo);

                        oChildInkubasyonTakipOzellikleri.SetProperty("U_Saat", item.Saat);

                        oChildInkubasyonTakipOzellikleri.SetProperty("U_Sicaklik", item.Sicaklik);

                        oChildInkubasyonTakipOzellikleri.SetProperty("U_PH", item.PH);

                        oChildInkubasyonTakipOzellikleri.SetProperty("U_KontrolEdenPers", item.KontrolEdenPersonel);
                    }

                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_YGRPRSS_ANLZ\" WITH (NOLOCK)");

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
                        return new Response { Value = 0, Description = "Yoğurt Proses Analiz Takibi girişi oluşturuldu..", List = null };
                    }
                    else
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5200, Description = "Hata Kodu - 5200 Yoğurt Proses Analiz Takibi girişi oluşturulurken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
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

                    GeneralData oChildProsesOzellikleri3;

                    GeneralDataCollection oChildrenProsesOzellikleri3;

                    GeneralData oChildBirinciKulturOzellikleri;

                    GeneralDataCollection oChildrenBirinciKulturOzellikleri;

                    GeneralData oChildIkinciKulturOzellikleri;

                    GeneralDataCollection oChildrenIkinciKulturOzellikleri;

                    GeneralData oChildSutunOzellikleri;

                    GeneralDataCollection oChildrenSutunOzellikleri;

                    GeneralData oChildYariMamulKaliteOzellikleri;

                    GeneralDataCollection oChildrenYariMamulKaliteOzellikleri;

                    GeneralData oChildInkubasyonTakipOzellikleri;

                    GeneralDataCollection oChildrenInkubasyonTakipOzellikleri;

                    oCompService = oCompany.GetCompanyService();

                    GeneralDataParams oGeneralParams;

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_YGRPRSS_ANLZ");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    oGeneralData.SetProperty("U_PartiNo", yogurtProsesTakipAnaliz.PartiNo.ToString());

                    oGeneralData.SetProperty("U_KalemKodu", yogurtProsesTakipAnaliz.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_KalemTanimi", yogurtProsesTakipAnaliz.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Aciklama", yogurtProsesTakipAnaliz.Aciklama.ToString());

                    if (yogurtProsesTakipAnaliz.UretimTarihi != null && yogurtProsesTakipAnaliz.UretimTarihi != "")
                    {
                        string tarih = yogurtProsesTakipAnaliz.UretimTarihi;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_UretimTarihi", dt);

                    }
                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    //oGeneralData.SetProperty("U_Tarih", dt);

                    oChildrenProsesOzellikleri1 = oGeneralData.Child("AIF_YGRPRSS_ANLZ1");

                    if (oChildrenProsesOzellikleri1.Count > 0)
                    {
                        int drc = oChildrenProsesOzellikleri1.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenProsesOzellikleri1.Remove(0);
                    }

                    foreach (var item in yogurtProsesTakipAnaliz.YogurtProsesOzellikleri1s)
                    {
                        oChildProsesOzellikleri1 = oChildrenProsesOzellikleri1.Add();

                        oChildProsesOzellikleri1.SetProperty("U_UrunGrubu", item.UrunGrubu);

                        oChildProsesOzellikleri1.SetProperty("U_InkubasyonOdaNo", item.InkubasyonNo);

                        oChildProsesOzellikleri1.SetProperty("U_ProsesBasSaat", item.ProsesBaslangicSaati);

                        oChildProsesOzellikleri1.SetProperty("U_YagliSutMik", item.YagliSutMiktari);

                        oChildProsesOzellikleri1.SetProperty("U_YagsizSutMik", item.YagsizSutMiktari);

                        oChildProsesOzellikleri1.SetProperty("U_KremaMik", item.KullanilanKremaMiktari);
                    }

                    oChildrenProsesOzellikleri2 = oGeneralData.Child("AIF_YGRPRSS_ANLZ2");

                    if (oChildrenProsesOzellikleri2.Count > 0)
                    {
                        int drc = oChildrenProsesOzellikleri2.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenProsesOzellikleri2.Remove(0);
                    }

                    foreach (var item in yogurtProsesTakipAnaliz.YogurtProsesOzellikleri2s)
                    {
                        oChildProsesOzellikleri2 = oChildrenProsesOzellikleri2.Add();

                        oChildProsesOzellikleri2.SetProperty("U_VakumBasSaat", item.VakumBaslangicSaati);

                        oChildProsesOzellikleri2.SetProperty("U_VakumBitSaat", item.VakumBitisSaati);

                        oChildProsesOzellikleri2.SetProperty("U_Brix", item.VakumSonuSutBrixDegeri);

                        oChildProsesOzellikleri2.SetProperty("U_PastorizasyonSicak", item.PastorizasyonSicakligi);

                        oChildProsesOzellikleri2.SetProperty("U_PastorizasyonSure", item.PastorizasyonSuresi);

                        oChildProsesOzellikleri2.SetProperty("U_MayalamaSaati", item.MayalamaSaati);

                        oChildProsesOzellikleri2.SetProperty("U_MayalamaPH", item.MayalamaPHDegeri);

                        oChildProsesOzellikleri2.SetProperty("U_MayalamaSicakligi", item.MayalamaSicakligi);
                    }

                    oChildrenProsesOzellikleri3 = oGeneralData.Child("AIF_YGRPRSS_ANLZ3");

                    if (oChildrenProsesOzellikleri3.Count > 0)
                    {
                        int drc = oChildrenProsesOzellikleri3.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenProsesOzellikleri3.Remove(0);
                    }

                    foreach (var item in yogurtProsesTakipAnaliz.YogurtProsesOzellikleri3s)
                    {
                        oChildProsesOzellikleri3 = oChildrenProsesOzellikleri3.Add();

                        oChildProsesOzellikleri3.SetProperty("U_DolumBasSaat", item.DolumBaslangicSaati);

                        oChildProsesOzellikleri3.SetProperty("U_DolumBasPH", item.DolumBaslangicindakiPHDegeri);

                        oChildProsesOzellikleri3.SetProperty("U_DolumBasSicak", item.DolumBaslangicindakiSutSicakligi);

                        oChildProsesOzellikleri3.SetProperty("U_DolumBitSaat", item.DolumBitisSaati);

                        oChildProsesOzellikleri3.SetProperty("U_DolumBitPH", item.DolumBittigindekiPHDegeri);

                        oChildProsesOzellikleri3.SetProperty("U_DolumBitSicak", item.DolumBittigindekiSutSicakligi);

                        oChildProsesOzellikleri3.SetProperty("U_SorumluPersonel", item.SorumluPersonel);

                        oChildProsesOzellikleri3.SetProperty("U_ToplamGecenSure", item.ToplamGecenSure);
                    }

                    oChildrenBirinciKulturOzellikleri = oGeneralData.Child("AIF_YGRPRSS_ANLZ4");

                    if (oChildrenBirinciKulturOzellikleri.Count > 0)
                    {
                        int drc = oChildrenBirinciKulturOzellikleri.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenBirinciKulturOzellikleri.Remove(0);
                    }

                    foreach (var item in yogurtProsesTakipAnaliz.YogurtBirinciKulturs)
                    {
                        oChildBirinciKulturOzellikleri = oChildrenBirinciKulturOzellikleri.Add();

                        oChildBirinciKulturOzellikleri.SetProperty("U_KullanilanMik", item.KullanilanMiktar);

                        oChildBirinciKulturOzellikleri.SetProperty("U_TedAdiVeKultKod", item.TedarikciAdiveKulturKodu);

                        oChildBirinciKulturOzellikleri.SetProperty("U_LotNo", item.LotNo);
                    }

                    oChildrenIkinciKulturOzellikleri = oGeneralData.Child("AIF_YGRPRSS_ANLZ5");

                    if (oChildrenIkinciKulturOzellikleri.Count > 0)
                    {
                        int drc = oChildrenIkinciKulturOzellikleri.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenIkinciKulturOzellikleri.Remove(0);
                    }

                    foreach (var item in yogurtProsesTakipAnaliz.YogurtIkinciKulturs)
                    {
                        oChildIkinciKulturOzellikleri = oChildrenIkinciKulturOzellikleri.Add();

                        oChildIkinciKulturOzellikleri.SetProperty("U_KullanilanMik", item.KullanilanMiktar);

                        oChildIkinciKulturOzellikleri.SetProperty("U_TedAdiVeKultKod", item.TedarikciAdiveKulturKodu);

                        oChildIkinciKulturOzellikleri.SetProperty("U_LotNo", item.LotNo);
                    }

                    oChildrenSutunOzellikleri = oGeneralData.Child("AIF_YGRPRSS_ANLZ6");

                    if (oChildrenSutunOzellikleri.Count > 0)
                    {
                        int drc = oChildrenSutunOzellikleri.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenSutunOzellikleri.Remove(0);
                    }

                    foreach (var item in yogurtProsesTakipAnaliz.YogurtSutOzellikleris)
                    {
                        oChildSutunOzellikleri = oChildrenSutunOzellikleri.Add();

                        oChildSutunOzellikleri.SetProperty("U_SutTuru", item.SutTuru);

                        oChildSutunOzellikleri.SetProperty("U_SutunKaynagi", item.SutunKaynagi);

                        oChildSutunOzellikleri.SetProperty("U_Brix", item.Brix);

                        oChildSutunOzellikleri.SetProperty("U_PH", item.PH);

                        oChildSutunOzellikleri.SetProperty("U_SH", item.SH);

                        oChildSutunOzellikleri.SetProperty("U_Yag", item.Yag);
                    }

                    oChildrenYariMamulKaliteOzellikleri = oGeneralData.Child("AIF_YGRPRSS_ANLZ7");

                    if (oChildrenYariMamulKaliteOzellikleri.Count > 0)
                    {
                        int drc = oChildrenYariMamulKaliteOzellikleri.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenYariMamulKaliteOzellikleri.Remove(0);
                    }

                    foreach (var item in yogurtProsesTakipAnaliz.YogurtYariMamulKaliteOzellikleris)
                    {
                        oChildYariMamulKaliteOzellikleri = oChildrenYariMamulKaliteOzellikleri.Add();

                        oChildYariMamulKaliteOzellikleri.SetProperty("U_PartiNo", item.PartiNo);

                        oChildYariMamulKaliteOzellikleri.SetProperty("U_TKM", item.TKM);

                        oChildYariMamulKaliteOzellikleri.SetProperty("U_Protein", item.Protein);

                        oChildYariMamulKaliteOzellikleri.SetProperty("U_Tuz", item.Tuz);

                        oChildYariMamulKaliteOzellikleri.SetProperty("U_Brix", item.Brix);

                        oChildYariMamulKaliteOzellikleri.SetProperty("U_PH", item.PH);

                        oChildYariMamulKaliteOzellikleri.SetProperty("U_SH", item.SH);

                        oChildYariMamulKaliteOzellikleri.SetProperty("U_Yag", item.Yag);

                        oChildYariMamulKaliteOzellikleri.SetProperty("U_TatKokuKivam", item.TatKokuKivam);
                    }

                    oChildrenInkubasyonTakipOzellikleri = oGeneralData.Child("AIF_YGRPRSS_ANLZ8");

                    if (oChildrenInkubasyonTakipOzellikleri.Count > 0)
                    {
                        int drc = oChildrenInkubasyonTakipOzellikleri.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenInkubasyonTakipOzellikleri.Remove(0);
                    }

                    foreach (var item in yogurtProsesTakipAnaliz.YogurtInkubasyonOzellikleris)
                    {
                        oChildInkubasyonTakipOzellikleri = oChildrenInkubasyonTakipOzellikleri.Add();

                        oChildInkubasyonTakipOzellikleri.SetProperty("U_KontrolNo", item.KontrolNo);

                        oChildInkubasyonTakipOzellikleri.SetProperty("U_Saat", item.Saat);

                        oChildInkubasyonTakipOzellikleri.SetProperty("U_Sicaklik", item.Sicaklik);

                        oChildInkubasyonTakipOzellikleri.SetProperty("U_PH", item.PH);

                        oChildInkubasyonTakipOzellikleri.SetProperty("U_KontrolEdenPers", item.KontrolEdenPersonel);
                    }

                    try
                    {
                        oGeneralService.Update(oGeneralData);
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = 0, Description = "Yoğurt Proses Analiz Takibi girişi güncellendi.", List = null };
                    }
                    catch (Exception)
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5300, Description = "Hata Kodu - 5300 Yoğurt Proses Analiz Takibi girişi güncellenirken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
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