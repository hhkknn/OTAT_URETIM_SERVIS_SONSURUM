using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddOrUpdateTelemeProsesAnalizTakip
    {
        public Response addOrUpdateTelemeProsesTakipAnaliz(TelemeProsesTakipAnaliz telemeProsesTakipAnaliz, string dbName,string mKodValue)
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

                oRS.DoQuery("Select * from \"@AIF_TLMPRSS_ANALIZ\" WITH (NOLOCK) where \"U_PartiNo\" = '" + telemeProsesTakipAnaliz.PartiNo + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChildSutOzellikleri;

                    GeneralDataCollection oChildrenSutOzellikleri;

                    GeneralData oChildProsesOzellikleri;

                    GeneralDataCollection oChildrenProsesOzellikleri;

                    GeneralData oChildProsess2Ozellikleri;

                    GeneralDataCollection oChildrenProsess2Ozellikleri;

                    GeneralData oChildProsess2_1_Ozellikleri;

                    GeneralDataCollection oChildrenProsess2_1_Ozellikleri;

                    GeneralData oChildSarfMalzemeKullanim;

                    GeneralDataCollection oChildrenSarfMalzemeKullanim;

                    GeneralData oChildMamulOzellikleri;

                    GeneralDataCollection oChildrenMamulOzellikleri;

                    GeneralData oChildMamulOzellikleri1;

                    GeneralDataCollection oChildrenMamulOzellikleri1;

                    oCompService = oCompany.GetCompanyService();

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_TLMPRSS_ANALIZ");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralData.SetProperty("U_PartiNo", telemeProsesTakipAnaliz.PartiNo.ToString());

                    oGeneralData.SetProperty("U_Aciklama", telemeProsesTakipAnaliz.Aciklama.ToString());


                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    //oGeneralData.SetProperty("U_Tarih", dt);

                    if (telemeProsesTakipAnaliz.UretimTarihi != null && telemeProsesTakipAnaliz.UretimTarihi != "")
                    {
                        string tarih = telemeProsesTakipAnaliz.UretimTarihi;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_UretimTarihi", dt);

                    }

                    oChildrenProsesOzellikleri = oGeneralData.Child("AIF_TLMPRSS_ANALIZ1");

                    foreach (var item in telemeProsesTakipAnaliz.prosessOzellikleri1Detay)
                    {
                        oChildSutOzellikleri = oChildrenProsesOzellikleri.Add();

                        oChildSutOzellikleri.SetProperty("U_PartiNo", item.PartiNo);

                        oChildSutOzellikleri.SetProperty("U_TelemeTuru", item.TelemeTuru);

                        oChildSutOzellikleri.SetProperty("U_GorevliOperator", item.GorevliOperator);

                        oChildSutOzellikleri.SetProperty("U_GorevliOperatorAdi", item.GorevliOperatorAdi);

                        oChildSutOzellikleri.SetProperty("U_SutGonBaslangicSaati", item.SutGondermeBaslangicSaati);

                        oChildSutOzellikleri.SetProperty("U_SutGonBitisSaati", item.SutGondermeBitisSaati);

                        oChildSutOzellikleri.SetProperty("U_NetSutMiktari", item.NetSutMiktari);

                        oChildSutOzellikleri.SetProperty("U_SutPastSicakligi", item.SutPastorizasyonSicakligi);

                        oChildSutOzellikleri.SetProperty("U_EsanjorTankFiltre", item.EsanjorTankFiltreKontrolu);

                        oChildSutOzellikleri.SetProperty("U_TanktakiSuSeviyesi", item.TanktakiSuSeviyesi);
                    }

                    oChildrenSutOzellikleri = oGeneralData.Child("AIF_TLMPRSS_ANALIZ2");

                    foreach (var item in telemeProsesTakipAnaliz.SutOzellikleriDetay)
                    {
                        oChildProsesOzellikleri = oChildrenSutOzellikleri.Add();

                        oChildProsesOzellikleri.SetProperty("U_KuruMadde", item.KuruMadde);

                        oChildProsesOzellikleri.SetProperty("U_YagOrani", item.YagOrani);

                        oChildProsesOzellikleri.SetProperty("U_PH", item.PH);

                        oChildProsesOzellikleri.SetProperty("U_SH", item.SH);

                        oChildProsesOzellikleri.SetProperty("U_SuOrani", item.SuOrani);

                        oChildProsesOzellikleri.SetProperty("U_ProteinOrani", item.ProteinOrani);
                    }

                    oChildrenProsess2Ozellikleri = oGeneralData.Child("AIF_TLMPRSS_ANALIZ3");

                    foreach (var item in telemeProsesTakipAnaliz.prosesOzellikleri2Detay)
                    {
                        oChildProsess2Ozellikleri = oChildrenProsess2Ozellikleri.Add();

                        oChildProsess2Ozellikleri.SetProperty("U_MayalamaSaati", item.MayalamaSaati);

                        oChildProsess2Ozellikleri.SetProperty("U_MayalamaSicakligi", item.MayalamaSicakligi);

                        oChildProsess2Ozellikleri.SetProperty("U_KirimVeCedarBasSaat", item.KirimVeCedarlamaBaslangicSaati);

                        oChildProsess2Ozellikleri.SetProperty("U_CedarlamaSicakligi", item.CedarlamaSicakligi);

                        oChildProsess2Ozellikleri.SetProperty("U_KirimVeCedarBitSaat", item.KirimVeCedarlamaBitisSaati);

                        oChildProsess2Ozellikleri.SetProperty("U_IndirmeBitisSaati", item.IndirmeBitisSaati);

                        oChildProsess2Ozellikleri.SetProperty("U_BaskiBaslangicSaati", item.BaskiBaslangicSaati);

                        oChildProsess2Ozellikleri.SetProperty("U_BaskiBitisSaati", item.BaskiBitisSaati);

                        oChildProsess2Ozellikleri.SetProperty("U_UretimDepoSicakligi", item.UretimDepoSicakligi);
                    }



                    oChildrenProsess2_1_Ozellikleri = oGeneralData.Child("AIF_TLMPRSS_ANALIZ4");

                    foreach (var item in telemeProsesTakipAnaliz.prosesOzellikleri2_1_Detay)
                    {
                        oChildProsess2_1_Ozellikleri = oChildrenProsess2_1_Ozellikleri.Add();

                        oChildProsess2_1_Ozellikleri.SetProperty("U_SutGonderimSuresiDk", item.SutGonderimSuresiDakika);

                        oChildProsess2_1_Ozellikleri.SetProperty("U_MayalamaSuresiDk", item.MayalamaSuresiDakika);

                        oChildProsess2_1_Ozellikleri.SetProperty("U_KirimVeCedarSure", item.KirimVeCedarlamaSuresiDakika);

                        oChildProsess2_1_Ozellikleri.SetProperty("U_IndirmeSuresi", item.IndirimSuresiDakika);

                        oChildProsess2_1_Ozellikleri.SetProperty("U_BaskiSuresi", item.BaskiSuresiDakika);

                        oChildProsess2_1_Ozellikleri.SetProperty("U_ToplamGecenSure", item.ToplamGecenSureDakika);
                    }


                    oChildrenSarfMalzemeKullanim = oGeneralData.Child("AIF_TLMPRSS_ANALIZ5");

                    foreach (var item in telemeProsesTakipAnaliz.sarfMalzemeKullanimDetay)
                    {
                        oChildSarfMalzemeKullanim = oChildrenSarfMalzemeKullanim.Add();

                        oChildSarfMalzemeKullanim.SetProperty("U_MalzemeAdi", item.MalzemeAdi);

                        oChildSarfMalzemeKullanim.SetProperty("U_MalMarkaTedarikci", item.MalzemeMarkasiVeTedarikcisi);

                        oChildSarfMalzemeKullanim.SetProperty("U_PartiNo", item.PartiNo);

                        oChildSarfMalzemeKullanim.SetProperty("U_Miktar", item.Miktar);

                        oChildSarfMalzemeKullanim.SetProperty("U_Birim", item.Birim);
                    }

                    #region Mamul Özellikleri datagridviewleri ayrı ekran oldu.
                    //oChildrenMamulOzellikleri = oGeneralData.Child("AIF_TLMPRSS_ANALIZ6");

                    //foreach (var item in telemeProsesTakipAnaliz.mamulOzellikleriDetay)
                    //{
                    //    oChildMamulOzellikleri = oChildrenMamulOzellikleri.Add();

                    //    oChildMamulOzellikleri.SetProperty("U_UrtmSonrasiTelemeMik", item.UretimSonrasiTelemeMiktari);

                    //    oChildMamulOzellikleri.SetProperty("U_UrtmSonrasiTelemeMik1", item.BirGunSonraTelemeMiktari);

                    //    oChildMamulOzellikleri.SetProperty("U_UretimRandimani", item.UretimRandimani);

                    //    oChildMamulOzellikleri.SetProperty("U_PersonelKodu", item.PersonelKodu);

                    //    oChildMamulOzellikleri.SetProperty("U_PersonelAdi", item.PersonelAdi);
                    //}


                    //oChildrenMamulOzellikleri1 = oGeneralData.Child("AIF_TLMPRSS_ANALIZ7");

                    //foreach (var item in telemeProsesTakipAnaliz.mamulOzellikleri1Detay)
                    //{
                    //    oChildMamulOzellikleri1 = oChildrenMamulOzellikleri1.Add();

                    //    oChildMamulOzellikleri1.SetProperty("U_KuruMadde", item.KuruMadde);

                    //    oChildMamulOzellikleri1.SetProperty("U_YagOrani", item.YagOrani);

                    //    oChildMamulOzellikleri1.SetProperty("U_PH", item.PH);

                    //    oChildMamulOzellikleri1.SetProperty("U_SH", item.SH);

                    //    oChildMamulOzellikleri1.SetProperty("U_TuzOrani", item.TuzOrani);
                    //}

                    #endregion

                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_TLMPRSS_ANALIZ\" WITH (NOLOCK)");

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
                        return new Response { Value = 0, Description = "Teleme Proses Analiz Takibi girişi oluşturuldu..", List = null };
                    }
                    else

                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5200, Description = "Hata Kodu - 5200 Teleme Proses Analiz Takibi girişi oluşturulurken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                    }
                }
                else
                {
                    CompanyService oCompService;
                    oCompService = oCompany.GetCompanyService();

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralDataParams oGeneralParams;

                    GeneralData oChildSutOzellikleri;

                    GeneralDataCollection oChildrenSutOzellikleri;

                    GeneralData oChildProsesOzellikleri;

                    GeneralDataCollection oChildrenProsesOzellikleri;

                    GeneralData oChildProsess2Ozellikleri;

                    GeneralDataCollection oChildrenProsess2Ozellikleri;

                    GeneralData oChildProsess2_1_Ozellikleri;

                    GeneralDataCollection oChildrenProsess2_1_Ozellikleri;

                    GeneralData oChildSarfMalzemeKullanim;

                    GeneralDataCollection oChildrenSarfMalzemeKullanim;

                    GeneralData oChildMamulOzellikleri;

                    GeneralDataCollection oChildrenMamulOzellikleri;

                    GeneralData oChildMamulOzellikleri1;

                    GeneralDataCollection oChildrenMamulOzellikleri1;

                    oGeneralService = oCompService.GetGeneralService("AIF_TLMPRSS_ANALIZ");

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    oGeneralData.SetProperty("U_PartiNo", telemeProsesTakipAnaliz.PartiNo);

                    oGeneralData.SetProperty("U_Aciklama", telemeProsesTakipAnaliz.Aciklama);

                    if (telemeProsesTakipAnaliz.UretimTarihi != null && telemeProsesTakipAnaliz.UretimTarihi != "")
                    {
                        string tarih = telemeProsesTakipAnaliz.UretimTarihi;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_UretimTarihi", dt);

                    }


                    oChildrenProsesOzellikleri = oGeneralData.Child("AIF_TLMPRSS_ANALIZ1");

                    if (oChildrenProsesOzellikleri.Count > 0)
                    {
                        int drc = oChildrenProsesOzellikleri.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenProsesOzellikleri.Remove(0);
                    }

                    foreach (var item in telemeProsesTakipAnaliz.prosessOzellikleri1Detay)
                    {
                        oChildSutOzellikleri = oChildrenProsesOzellikleri.Add();
                        oChildSutOzellikleri.SetProperty("U_PartiNo", item.PartiNo);

                        oChildSutOzellikleri.SetProperty("U_TelemeTuru", item.TelemeTuru);

                        oChildSutOzellikleri.SetProperty("U_GorevliOperator", item.GorevliOperator);

                        oChildSutOzellikleri.SetProperty("U_GorevliOperatorAdi", item.GorevliOperatorAdi);

                        oChildSutOzellikleri.SetProperty("U_SutGonBaslangicSaati", item.SutGondermeBaslangicSaati);

                        oChildSutOzellikleri.SetProperty("U_SutGonBitisSaati", item.SutGondermeBitisSaati);

                        oChildSutOzellikleri.SetProperty("U_NetSutMiktari", item.NetSutMiktari);

                        oChildSutOzellikleri.SetProperty("U_SutPastSicakligi", item.SutPastorizasyonSicakligi);

                        oChildSutOzellikleri.SetProperty("U_EsanjorTankFiltre", item.EsanjorTankFiltreKontrolu);

                        oChildSutOzellikleri.SetProperty("U_TanktakiSuSeviyesi", item.TanktakiSuSeviyesi);
                    }


                    oChildrenSutOzellikleri = oGeneralData.Child("AIF_TLMPRSS_ANALIZ2");

                    if (oChildrenSutOzellikleri.Count > 0)
                    {
                        int drc = oChildrenSutOzellikleri.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenSutOzellikleri.Remove(0);
                    }

                    foreach (var item in telemeProsesTakipAnaliz.SutOzellikleriDetay)
                    {
                        oChildProsesOzellikleri = oChildrenSutOzellikleri.Add();

                        oChildProsesOzellikleri.SetProperty("U_KuruMadde", item.KuruMadde);

                        oChildProsesOzellikleri.SetProperty("U_YagOrani", item.YagOrani);

                        oChildProsesOzellikleri.SetProperty("U_PH", item.PH);

                        oChildProsesOzellikleri.SetProperty("U_SH", item.SH);

                        oChildProsesOzellikleri.SetProperty("U_SuOrani", item.SuOrani);

                        oChildProsesOzellikleri.SetProperty("U_ProteinOrani", item.ProteinOrani);
                    }


                    oChildrenProsess2Ozellikleri = oGeneralData.Child("AIF_TLMPRSS_ANALIZ3");


                    if (oChildrenProsess2Ozellikleri.Count > 0)
                    {
                        int drc = oChildrenProsess2Ozellikleri.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenProsess2Ozellikleri.Remove(0);
                    }

                    foreach (var item in telemeProsesTakipAnaliz.prosesOzellikleri2Detay)
                    {
                        oChildProsess2Ozellikleri = oChildrenProsess2Ozellikleri.Add();

                        oChildProsess2Ozellikleri.SetProperty("U_MayalamaSaati", item.MayalamaSaati);

                        oChildProsess2Ozellikleri.SetProperty("U_MayalamaSicakligi", item.MayalamaSicakligi);

                        oChildProsess2Ozellikleri.SetProperty("U_KirimVeCedarBasSaat", item.KirimVeCedarlamaBaslangicSaati);

                        oChildProsess2Ozellikleri.SetProperty("U_CedarlamaSicakligi", item.CedarlamaSicakligi);

                        oChildProsess2Ozellikleri.SetProperty("U_KirimVeCedarBitSaat", item.KirimVeCedarlamaBitisSaati);

                        oChildProsess2Ozellikleri.SetProperty("U_IndirmeBitisSaati", item.IndirmeBitisSaati);

                        oChildProsess2Ozellikleri.SetProperty("U_BaskiBaslangicSaati", item.BaskiBaslangicSaati);

                        oChildProsess2Ozellikleri.SetProperty("U_BaskiBitisSaati", item.BaskiBitisSaati);

                        oChildProsess2Ozellikleri.SetProperty("U_UretimDepoSicakligi", item.UretimDepoSicakligi);
                    }


                    oChildrenProsess2_1_Ozellikleri = oGeneralData.Child("AIF_TLMPRSS_ANALIZ4");


                    if (oChildrenProsess2_1_Ozellikleri.Count > 0)
                    {
                        int drc = oChildrenProsess2_1_Ozellikleri.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenProsess2_1_Ozellikleri.Remove(0);
                    }


                    foreach (var item in telemeProsesTakipAnaliz.prosesOzellikleri2_1_Detay)
                    {
                        oChildProsess2_1_Ozellikleri = oChildrenProsess2_1_Ozellikleri.Add();

                        oChildProsess2_1_Ozellikleri.SetProperty("U_SutGonderimSuresiDk", item.SutGonderimSuresiDakika);

                        oChildProsess2_1_Ozellikleri.SetProperty("U_MayalamaSuresiDk", item.MayalamaSuresiDakika);

                        oChildProsess2_1_Ozellikleri.SetProperty("U_KirimVeCedarSure", item.KirimVeCedarlamaSuresiDakika);

                        oChildProsess2_1_Ozellikleri.SetProperty("U_IndirmeSuresi", item.IndirimSuresiDakika);

                        oChildProsess2_1_Ozellikleri.SetProperty("U_BaskiSuresi", item.BaskiSuresiDakika);

                        oChildProsess2_1_Ozellikleri.SetProperty("U_ToplamGecenSure", item.ToplamGecenSureDakika);
                    }


                    oChildrenSarfMalzemeKullanim = oGeneralData.Child("AIF_TLMPRSS_ANALIZ5");

                    if (oChildrenSarfMalzemeKullanim.Count > 0)
                    {
                        int drc = oChildrenSarfMalzemeKullanim.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenSarfMalzemeKullanim.Remove(0);
                    }

                    foreach (var item in telemeProsesTakipAnaliz.sarfMalzemeKullanimDetay)
                    {
                        oChildSarfMalzemeKullanim = oChildrenSarfMalzemeKullanim.Add();

                        oChildSarfMalzemeKullanim.SetProperty("U_MalzemeAdi", item.MalzemeAdi);

                        oChildSarfMalzemeKullanim.SetProperty("U_MalMarkaTedarikci", item.MalzemeMarkasiVeTedarikcisi);

                        oChildSarfMalzemeKullanim.SetProperty("U_PartiNo", item.PartiNo);

                        oChildSarfMalzemeKullanim.SetProperty("U_Miktar", item.Miktar);

                        oChildSarfMalzemeKullanim.SetProperty("U_Birim", item.Birim);
                    }



                    #region Mamul Özellikleri datagridviewleri ayrı ekran oldu.
                    //oChildrenMamulOzellikleri = oGeneralData.Child("AIF_TLMPRSS_ANALIZ6");

                    //if (oChildrenMamulOzellikleri.Count > 0)
                    //{
                    //    int drc = oChildrenMamulOzellikleri.Count;
                    //    for (int rmv = 0; rmv < drc; rmv++)
                    //        oChildrenMamulOzellikleri.Remove(0);
                    //}

                    //foreach (var item in telemeProsesTakipAnaliz.mamulOzellikleriDetay)
                    //{
                    //    oChildMamulOzellikleri = oChildrenMamulOzellikleri.Add();

                    //    oChildMamulOzellikleri.SetProperty("U_UrtmSonrasiTelemeMik", item.UretimSonrasiTelemeMiktari);

                    //    oChildMamulOzellikleri.SetProperty("U_UrtmSonrasiTelemeMik1", item.BirGunSonraTelemeMiktari);

                    //    oChildMamulOzellikleri.SetProperty("U_UretimRandimani", item.UretimRandimani);

                    //    oChildMamulOzellikleri.SetProperty("U_PersonelKodu", item.PersonelKodu);

                    //    oChildMamulOzellikleri.SetProperty("U_PersonelAdi", item.PersonelAdi);
                    //}



                    //oChildrenMamulOzellikleri1 = oGeneralData.Child("AIF_TLMPRSS_ANALIZ7");

                    //if (oChildrenMamulOzellikleri1.Count > 0)
                    //{
                    //    int drc = oChildrenMamulOzellikleri1.Count;
                    //    for (int rmv = 0; rmv < drc; rmv++)
                    //        oChildrenMamulOzellikleri1.Remove(0);
                    //}


                    //foreach (var item in telemeProsesTakipAnaliz.mamulOzellikleri1Detay)
                    //{
                    //    oChildMamulOzellikleri1 = oChildrenMamulOzellikleri1.Add();

                    //    oChildMamulOzellikleri1.SetProperty("U_KuruMadde", item.KuruMadde);

                    //    oChildMamulOzellikleri1.SetProperty("U_YagOrani", item.YagOrani);

                    //    oChildMamulOzellikleri1.SetProperty("U_PH", item.PH);

                    //    oChildMamulOzellikleri1.SetProperty("U_SH", item.SH);

                    //    oChildMamulOzellikleri1.SetProperty("U_TuzOrani", item.TuzOrani);
                    //} 
                    #endregion


                    try
                    {
                        oGeneralService.Update(oGeneralData);
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = 0, Description = "Teleme Proses Analiz Takibi girişi güncellendi.", List = null };
                    }
                    catch (Exception)
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5300, Description = "Hata Kodu - 5300 Teleme Proses Analiz Takibi girişi güncellenirken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
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