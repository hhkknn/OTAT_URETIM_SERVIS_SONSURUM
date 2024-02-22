using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddOrUpdateLorProsessAnaliz
    {
        public Response addOrUpdateLorProsessAnaliz(LorProsesTakipAnaliz LorProsesTakipAnaliz, string dbName, string mKodValue)
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

                oRS.DoQuery("Select * from \"@AIF_LORPRSS_ANLZ\" WITH (NOLOCK) where \"U_PartiNo\" = '" + LorProsesTakipAnaliz.PartiNo + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChildProsesOzellikleri1;

                    GeneralDataCollection oChildrenProsesOzellikleri1;

                    GeneralData oChildProsesOzellikleri2;

                    GeneralDataCollection oChildrenProsesOzellikleri2;

                    GeneralData oChildSarfMalzemeKullanim;

                    GeneralDataCollection oChildrenSarfMalzemeKullanim;

                    GeneralData oChildMamulOzellikleri1;

                    GeneralDataCollection oChildrenMamulOzellikleri1;

                    GeneralData oChildMamulOzellikleri2;

                    GeneralDataCollection oChildrenMamulOzellikleri2;

                    oCompService = oCompany.GetCompanyService();

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_LORPRSS_ANLZ");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralData.SetProperty("U_PartiNo", LorProsesTakipAnaliz.PartiNo.ToString());

                    oGeneralData.SetProperty("U_KalemKodu", LorProsesTakipAnaliz.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_KalemTanimi", LorProsesTakipAnaliz.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Aciklama", LorProsesTakipAnaliz.Aciklama.ToString());


                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    //oGeneralData.SetProperty("U_Tarih", dt);

                    oChildrenProsesOzellikleri1 = oGeneralData.Child("AIF_LORPRSS_ANLZ1");

                    foreach (var item in LorProsesTakipAnaliz.LorProsesOzellikleri1s)
                    {
                        oChildProsesOzellikleri1 = oChildrenProsesOzellikleri1.Add();

                        oChildProsesOzellikleri1.SetProperty("U_PartiNo", item.PartiNo);

                        oChildProsesOzellikleri1.SetProperty("U_LorTuru", item.LorTuru);

                        oChildProsesOzellikleri1.SetProperty("U_PasGonderimBasSaat", item.PasGonderimBaslangicSaati);

                        oChildProsesOzellikleri1.SetProperty("U_PasGonderimBitSaat", item.PasGonderimBitisSaati);

                        oChildProsesOzellikleri1.SetProperty("U_NetPasMiktari", item.NetPasMiktari);

                        oChildProsesOzellikleri1.SetProperty("U_PasPastSicak", item.PasPastorizasyonSicakligi);

                        oChildProsesOzellikleri1.SetProperty("U_EsanjorTankFiltre", item.EsanjorTankFiltreVeKontrol);

                        oChildProsesOzellikleri1.SetProperty("U_PasTankGelSicak", item.PasinTankaGelisSicakligi);

                        oChildProsesOzellikleri1.SetProperty("U_PasPH", item.PasinPHDegeri);

                        oChildProsesOzellikleri1.SetProperty("U_PasinSeksenDereceSaati", item.PasinSeksenDereceyeGelmeSaati);

                        oChildProsesOzellikleri1.SetProperty("U_PasSeksenSekizDerece", item.PasinSeksenSekizDereceyeGelmeSaati);

                        oChildProsesOzellikleri1.SetProperty("U_PasinBosaltmaBasSaat", item.PasinTanktanBosaltilmaBaslangicSaati);

                        oChildProsesOzellikleri1.SetProperty("U_PasinBosaltmaBitSaat", item.PasinTanktanBosaltilmaBitisSaati);

                        oChildProsesOzellikleri1.SetProperty("U_BaskiBasSaat", item.BaskiBaslangicSaati);

                        oChildProsesOzellikleri1.SetProperty("U_BaskiBitSaat", item.BaskiBitisSaati);
                    }

                    oChildrenProsesOzellikleri2 = oGeneralData.Child("AIF_LORPRSS_ANLZ2");

                    foreach (var item in LorProsesTakipAnaliz.LorProsesOzellikleri2s)
                    {
                        oChildProsesOzellikleri2 = oChildrenProsesOzellikleri2.Add();

                        oChildProsesOzellikleri2.SetProperty("U_PasGonderimSuresi", item.PasinGonderimSuresi);

                        oChildProsesOzellikleri2.SetProperty("U_PasSeksenSekizDerece", item.PasinSeksenSekizDereceyeGelmeSuresi);

                        oChildProsesOzellikleri2.SetProperty("U_PeynirIndirilmeSuresi", item.PeynirinIndirilmeSuresi);

                        oChildProsesOzellikleri2.SetProperty("U_BaskiSuresi", item.BaskiSuresi);

                        oChildProsesOzellikleri2.SetProperty("U_ToplamGecenSure", item.ToplamGecenSure);
                    }



                    oChildrenSarfMalzemeKullanim = oGeneralData.Child("AIF_LORPRSS_ANLZ3");

                    foreach (var item in LorProsesTakipAnaliz.LorSarfMalzemeKullanims)
                    {
                        oChildSarfMalzemeKullanim = oChildrenSarfMalzemeKullanim.Add();

                        oChildSarfMalzemeKullanim.SetProperty("U_MalzemeAdi", item.MalzemeAdi);

                        oChildSarfMalzemeKullanim.SetProperty("U_MalMarkaTedarikci", item.MalzemeMarkaTedarikcisi);

                        oChildSarfMalzemeKullanim.SetProperty("U_PartiNo", item.PartiNo);

                        oChildSarfMalzemeKullanim.SetProperty("U_Miktar", item.Miktar);

                        oChildSarfMalzemeKullanim.SetProperty("U_Birim", item.Birim);
                    }


                    oChildrenMamulOzellikleri1 = oGeneralData.Child("AIF_LORPRSS_ANLZ4");

                    foreach (var item in LorProsesTakipAnaliz.LorMamulOzellikleri1s)
                    {
                        oChildMamulOzellikleri1 = oChildrenMamulOzellikleri1.Add();

                        oChildMamulOzellikleri1.SetProperty("U_UretilenMik", item.LorPeynirMiktari);

                        oChildMamulOzellikleri1.SetProperty("U_UretimRandimani", item.LorUretimRandimani);

                        oChildMamulOzellikleri1.SetProperty("U_KontrolEdenPers", item.KontrolEdenPersonel);
                    }

                    oChildrenMamulOzellikleri2 = oGeneralData.Child("AIF_LORPRSS_ANLZ5");

                    foreach (var item in LorProsesTakipAnaliz.LorMamulOzellikleri2s)
                    {
                        oChildMamulOzellikleri2 = oChildrenMamulOzellikleri2.Add();

                        oChildMamulOzellikleri2.SetProperty("U_KuruMadde", item.KuruMadde);

                        oChildMamulOzellikleri2.SetProperty("U_YagOrani", item.YagOrani);

                        oChildMamulOzellikleri2.SetProperty("U_PH", item.PH);

                        oChildMamulOzellikleri2.SetProperty("U_SH", item.SH);

                        oChildMamulOzellikleri2.SetProperty("U_TuzOrani", item.TuzOrani);
                    }


                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_LORPRSS_ANLZ\" WITH (NOLOCK)");

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
                        return new Response { Value = 0, Description = "Lor Proses Analiz Takibi girişi oluşturuldu..", List = null };
                    }
                    else

                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5200, Description = "Hata Kodu - 5200 Lor Proses Analiz Takibi girişi oluşturulurken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
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

                    GeneralData oChildSarfMalzemeKullanim;

                    GeneralDataCollection oChildrenSarfMalzemeKullanim;

                    GeneralData oChildMamulOzellikleri1;

                    GeneralDataCollection oChildrenMamulOzellikleri1;

                    GeneralData oChildMamulOzellikleri2;

                    GeneralDataCollection oChildrenMamulOzellikleri2;

                    oCompService = oCompany.GetCompanyService();

                    GeneralDataParams oGeneralParams;

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_LORPRSS_ANLZ");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    oGeneralData.SetProperty("U_PartiNo", LorProsesTakipAnaliz.PartiNo.ToString());

                    oGeneralData.SetProperty("U_KalemKodu", LorProsesTakipAnaliz.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_KalemTanimi", LorProsesTakipAnaliz.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Aciklama", LorProsesTakipAnaliz.Aciklama.ToString());


                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    //oGeneralData.SetProperty("U_Tarih", dt);

                    oChildrenProsesOzellikleri1 = oGeneralData.Child("AIF_LORPRSS_ANLZ1");

                    if (oChildrenProsesOzellikleri1.Count > 0)
                    {
                        int drc = oChildrenProsesOzellikleri1.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenProsesOzellikleri1.Remove(0);
                    }

                    foreach (var item in LorProsesTakipAnaliz.LorProsesOzellikleri1s)
                    {
                        oChildProsesOzellikleri1 = oChildrenProsesOzellikleri1.Add();

                        oChildProsesOzellikleri1.SetProperty("U_PartiNo", item.PartiNo);

                        oChildProsesOzellikleri1.SetProperty("U_LorTuru", item.LorTuru);

                        oChildProsesOzellikleri1.SetProperty("U_PasGonderimBasSaat", item.PasGonderimBaslangicSaati);

                        oChildProsesOzellikleri1.SetProperty("U_PasGonderimBitSaat", item.PasGonderimBitisSaati);

                        oChildProsesOzellikleri1.SetProperty("U_NetPasMiktari", item.NetPasMiktari);

                        oChildProsesOzellikleri1.SetProperty("U_PasPastSicak", item.PasPastorizasyonSicakligi);

                        oChildProsesOzellikleri1.SetProperty("U_EsanjorTankFiltre", item.EsanjorTankFiltreVeKontrol);

                        oChildProsesOzellikleri1.SetProperty("U_PasTankGelSicak", item.PasinTankaGelisSicakligi);

                        oChildProsesOzellikleri1.SetProperty("U_PasPH", item.PasinPHDegeri);

                        oChildProsesOzellikleri1.SetProperty("U_PasinSeksenDereceSaati", item.PasinSeksenDereceyeGelmeSaati);

                        oChildProsesOzellikleri1.SetProperty("U_PasSeksenSekizDerece", item.PasinSeksenSekizDereceyeGelmeSaati);

                        oChildProsesOzellikleri1.SetProperty("U_PasinBosaltmaBasSaat", item.PasinTanktanBosaltilmaBaslangicSaati);

                        oChildProsesOzellikleri1.SetProperty("U_PasinBosaltmaBitSaat", item.PasinTanktanBosaltilmaBitisSaati);

                        oChildProsesOzellikleri1.SetProperty("U_BaskiBasSaat", item.BaskiBaslangicSaati);

                        oChildProsesOzellikleri1.SetProperty("U_BaskiBitSaat", item.BaskiBitisSaati);
                    }

                    oChildrenProsesOzellikleri2 = oGeneralData.Child("AIF_LORPRSS_ANLZ2");

                    if (oChildrenProsesOzellikleri2.Count > 0)
                    {
                        int drc = oChildrenProsesOzellikleri2.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenProsesOzellikleri2.Remove(0);
                    }

                    foreach (var item in LorProsesTakipAnaliz.LorProsesOzellikleri2s)
                    {
                        oChildProsesOzellikleri2 = oChildrenProsesOzellikleri2.Add();

                        oChildProsesOzellikleri2.SetProperty("U_PasGonderimSuresi", item.PasinGonderimSuresi);

                        oChildProsesOzellikleri2.SetProperty("U_PasSeksenSekizDerece", item.PasinSeksenSekizDereceyeGelmeSuresi);

                        oChildProsesOzellikleri2.SetProperty("U_PeynirIndirilmeSuresi", item.PeynirinIndirilmeSuresi);

                        oChildProsesOzellikleri2.SetProperty("U_BaskiSuresi", item.BaskiSuresi);

                        oChildProsesOzellikleri2.SetProperty("U_ToplamGecenSure", item.ToplamGecenSure);
                    }


                    oChildrenSarfMalzemeKullanim = oGeneralData.Child("AIF_LORPRSS_ANLZ3");


                    if (oChildrenSarfMalzemeKullanim.Count > 0)
                    {
                        int drc = oChildrenSarfMalzemeKullanim.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenSarfMalzemeKullanim.Remove(0);
                    }

                    foreach (var item in LorProsesTakipAnaliz.LorSarfMalzemeKullanims)
                    {
                        oChildSarfMalzemeKullanim = oChildrenSarfMalzemeKullanim.Add();

                        oChildSarfMalzemeKullanim.SetProperty("U_MalzemeAdi", item.MalzemeAdi);

                        oChildSarfMalzemeKullanim.SetProperty("U_MalMarkaTedarikci", item.MalzemeMarkaTedarikcisi);

                        oChildSarfMalzemeKullanim.SetProperty("U_PartiNo", item.PartiNo);

                        oChildSarfMalzemeKullanim.SetProperty("U_Miktar", item.Miktar);

                        oChildSarfMalzemeKullanim.SetProperty("U_Birim", item.Birim);
                    }



                    oChildrenMamulOzellikleri1 = oGeneralData.Child("AIF_LORPRSS_ANLZ4");

                    if (oChildrenMamulOzellikleri1.Count > 0)
                    {
                        int drc = oChildrenMamulOzellikleri1.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenMamulOzellikleri1.Remove(0);
                    }

                    foreach (var item in LorProsesTakipAnaliz.LorMamulOzellikleri1s)
                    {
                        oChildMamulOzellikleri1 = oChildrenMamulOzellikleri1.Add();

                        oChildMamulOzellikleri1.SetProperty("U_UretilenMik", item.LorPeynirMiktari);

                        oChildMamulOzellikleri1.SetProperty("U_UretimRandimani", item.LorUretimRandimani);

                        oChildMamulOzellikleri1.SetProperty("U_KontrolEdenPers", item.KontrolEdenPersonel);
                    }


                    oChildrenMamulOzellikleri2 = oGeneralData.Child("AIF_LORPRSS_ANLZ5");

                    if (oChildrenMamulOzellikleri2.Count > 0)
                    {
                        int drc = oChildrenMamulOzellikleri2.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenMamulOzellikleri2.Remove(0);
                    }


                    foreach (var item in LorProsesTakipAnaliz.LorMamulOzellikleri2s)
                    {
                        oChildMamulOzellikleri2 = oChildrenMamulOzellikleri2.Add();

                        oChildMamulOzellikleri2.SetProperty("U_KuruMadde", item.KuruMadde);

                        oChildMamulOzellikleri2.SetProperty("U_YagOrani", item.YagOrani);

                        oChildMamulOzellikleri2.SetProperty("U_PH", item.PH);

                        oChildMamulOzellikleri2.SetProperty("U_SH", item.SH);

                        oChildMamulOzellikleri2.SetProperty("U_TuzOrani", item.TuzOrani);
                    }

                    try
                    {
                        oGeneralService.Update(oGeneralData);
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = 0, Description = "Lor Proses Analiz Takibi girişi güncellendi.", List = null };
                    }
                    catch (Exception)
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5300, Description = "Hata Kodu - 5300 Lor Proses Analiz Takibi girişi güncellenirken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
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