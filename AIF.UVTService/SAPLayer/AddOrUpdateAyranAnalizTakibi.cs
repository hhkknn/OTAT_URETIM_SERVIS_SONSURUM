using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddOrUpdateAyranAnalizTakibi
    {
        public Response addOrUpdateAyranAnalizTakibi(AyranAnalizTakibi ayranAnalizTakibi, string dbName, string mKodValue)
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

                //if (connection.number == -1)
                //{

                //    for (int ix = 1; ix <= 3; ix++)
                //    {
                //        connection = log.getSAPConnection(dbName,ID);

                //        if (connection.number > -1)
                //        {
                //            break;
                //        }
                //    }

                //}

                if (connection.number == -1)
                {
                    LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
                    return new Response { Value = -3100, Description = "Hata Kodu - 3100 Veritabanı bağlantısı sırasında hata oluştu. ", List = null };
                }

                clnum = connection.number;
                dbCode = connection.dbCode;

                Company oCompany = connection.oCompany;

                Recordset oRS = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

                oRS.DoQuery("Select * from \"@AIF_AYR_ANALIZ\" WITH (NOLOCK) where \"U_PartiNo\" = '" + ayranAnalizTakibi.PartiNo + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChildMayalama;

                    GeneralDataCollection oChildrenMayalama;

                    GeneralData oChildInkubasyon;

                    GeneralDataCollection oChildrenInkubasyon;

                    oCompService = oCompany.GetCompanyService();

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_AYR_ANALIZ");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralData.SetProperty("U_PartiNo", ayranAnalizTakibi.PartiNo);

                    oGeneralData.SetProperty("U_Sicaklik", ayranAnalizTakibi.PastorizasyonSicaklik);

                    oGeneralData.SetProperty("U_SicaklikSure", ayranAnalizTakibi.PastorizasyonSure);

                    oGeneralData.SetProperty("U_SutSicakligi", ayranAnalizTakibi.OnAktiflestirmeSutSicakligi);

                    oGeneralData.SetProperty("U_OnAktifSure", ayranAnalizTakibi.OnAktifSuresi);

                    oGeneralData.SetProperty("U_OnAktifPH", ayranAnalizTakibi.OnAktifPH);

                    oGeneralData.SetProperty("U_SutVakum", ayranAnalizTakibi.SutVakum);

                    oGeneralData.SetProperty("U_TankAdi", ayranAnalizTakibi.TankAdi);

                    oGeneralData.SetProperty("U_MayalamaSicaklik", ayranAnalizTakibi.MayalamaTankiSicaklik);

                    oGeneralData.SetProperty("U_Sorumlu", ayranAnalizTakibi.Sorumlu);

                    oGeneralData.SetProperty("U_SorumluAdi", ayranAnalizTakibi.SorumluAdi);

                    oGeneralData.SetProperty("U_PompaBasinci", ayranAnalizTakibi.PompaBasinci);

                    oGeneralData.SetProperty("U_BaslangicSaati", ayranAnalizTakibi.BaslangicSaati);

                    oGeneralData.SetProperty("U_BitisSaati", ayranAnalizTakibi.BitisSaati);

                    oGeneralData.SetProperty("U_TKM", ayranAnalizTakibi.TKM);

                    oGeneralData.SetProperty("U_Protein", ayranAnalizTakibi.Protein);

                    oGeneralData.SetProperty("U_Tuz", ayranAnalizTakibi.TKM);

                    oGeneralData.SetProperty("U_Brix", ayranAnalizTakibi.Brix);

                    oGeneralData.SetProperty("U_PH", ayranAnalizTakibi.PH);

                    oGeneralData.SetProperty("U_SH", ayranAnalizTakibi.SH);

                    oGeneralData.SetProperty("U_Yag", ayranAnalizTakibi.Yag);

                    oGeneralData.SetProperty("U_TatKokuKivam", ayranAnalizTakibi.TatKokuKivam);

                    oChildrenMayalama = oGeneralData.Child("AIF_AYR_ANALIZ1");

                    foreach (var item in ayranAnalizTakibi.MayalanacakSutOzellikleri)
                    {
                        oChildMayalama = oChildrenMayalama.Add();

                        oChildMayalama.SetProperty("U_Kaynak", item.Kaynak);

                        oChildMayalama.SetProperty("U_KaynakAdi", item.KaynakAdi);

                        oChildMayalama.SetProperty("U_Brix", item.Brix);

                        oChildMayalama.SetProperty("U_PH", item.PH);

                        oChildMayalama.SetProperty("U_SH", item.SH);

                        oChildMayalama.SetProperty("U_Yag", item.Yag);
                    }

                    oChildrenInkubasyon = oGeneralData.Child("AIF_AYR_ANALIZ2");

                    foreach (var item in ayranAnalizTakibi.Inkubasyon)
                    {
                        oChildInkubasyon = oChildrenInkubasyon.Add();

                        oChildInkubasyon.SetProperty("U_InkubasyonNo", item.InkubasyonNo.ToString());

                        oChildInkubasyon.SetProperty("U_Saat", item.Saat);

                        oChildInkubasyon.SetProperty("U_Sicaklik", item.Sicaklik);

                        oChildInkubasyon.SetProperty("U_PH", item.PH);

                        oChildInkubasyon.SetProperty("U_KontrolEden", item.KontrolEdenNo);

                        oChildInkubasyon.SetProperty("U_KontrolEdenAdi", item.KontrolEdenAdi.ToString());
                    }

                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_AYR_ANALIZ\" WITH (NOLOCK)");

                    int maxdocentry = Convert.ToInt32(oRS.Fields.Item(0).Value);

                    oGeneralData.SetProperty("DocNum", maxdocentry);

                    var resp = oGeneralService.Add(oGeneralData);

                    if (resp != null)
                    {
                        //if (oCompany.InTransaction)
                        //{
                        //    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                        //}
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
                        return new Response { Value = 0, Description = "Ayran Analiz Takibi girişi oluşturuldu..", List = null };
                    }
                    else

                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
                        return new Response { Value = -82000, Description = "Hata Kodu - 8200 Ayran Analiz Takibi girişi oluşturulurken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                    }
                }
                else
                {
                    CompanyService oCompService;
                    oCompService = oCompany.GetCompanyService();

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralDataParams oGeneralParams;

                    GeneralData oChildMayalama;

                    GeneralDataCollection oChildrenMayalama;

                    GeneralData oChildInkubasyon;

                    GeneralDataCollection oChildrenInkubasyon;

                    oGeneralService = oCompService.GetGeneralService("AIF_AYR_ANALIZ");

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    oGeneralData.SetProperty("U_Sicaklik", ayranAnalizTakibi.PastorizasyonSicaklik);

                    oGeneralData.SetProperty("U_SicaklikSure", ayranAnalizTakibi.PastorizasyonSure);

                    oGeneralData.SetProperty("U_SutSicakligi", ayranAnalizTakibi.OnAktiflestirmeSutSicakligi);

                    oGeneralData.SetProperty("U_OnAktifSure", ayranAnalizTakibi.OnAktifSuresi);

                    oGeneralData.SetProperty("U_OnAktifPH", ayranAnalizTakibi.OnAktifPH);

                    oGeneralData.SetProperty("U_SutVakum", ayranAnalizTakibi.SutVakum);

                    oGeneralData.SetProperty("U_TankAdi", ayranAnalizTakibi.TankAdi);

                    oGeneralData.SetProperty("U_MayalamaSicaklik", ayranAnalizTakibi.MayalamaTankiSicaklik);

                    oGeneralData.SetProperty("U_Sorumlu", ayranAnalizTakibi.Sorumlu);

                    oGeneralData.SetProperty("U_SorumluAdi", ayranAnalizTakibi.SorumluAdi);

                    oGeneralData.SetProperty("U_PompaBasinci", ayranAnalizTakibi.PompaBasinci);

                    oGeneralData.SetProperty("U_BaslangicSaati", ayranAnalizTakibi.BaslangicSaati);

                    oGeneralData.SetProperty("U_BitisSaati", ayranAnalizTakibi.BitisSaati);

                    oGeneralData.SetProperty("U_TKM", ayranAnalizTakibi.TKM);

                    oGeneralData.SetProperty("U_Protein", ayranAnalizTakibi.Protein);

                    oGeneralData.SetProperty("U_Tuz", ayranAnalizTakibi.TKM);

                    oGeneralData.SetProperty("U_Brix", ayranAnalizTakibi.Brix);

                    oGeneralData.SetProperty("U_PH", ayranAnalizTakibi.PH);

                    oGeneralData.SetProperty("U_SH", ayranAnalizTakibi.SH);

                    oGeneralData.SetProperty("U_Yag", ayranAnalizTakibi.Yag);

                    oGeneralData.SetProperty("U_TatKokuKivam", ayranAnalizTakibi.TatKokuKivam);

                    oChildrenMayalama = oGeneralData.Child("AIF_AYR_ANALIZ1");

                    if (oChildrenMayalama.Count > 0)
                    {
                        int drc = oChildrenMayalama.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenMayalama.Remove(0);
                    }

                    foreach (var item in ayranAnalizTakibi.MayalanacakSutOzellikleri)
                    {
                        oChildMayalama = oChildrenMayalama.Add();

                        oChildMayalama.SetProperty("U_Kaynak", item.Kaynak);

                        oChildMayalama.SetProperty("U_KaynakAdi", item.KaynakAdi);

                        oChildMayalama.SetProperty("U_Brix", item.Brix);

                        oChildMayalama.SetProperty("U_PH", item.PH);

                        oChildMayalama.SetProperty("U_SH", item.SH);

                        oChildMayalama.SetProperty("U_Yag", item.Yag);
                    }

                    oChildrenInkubasyon = oGeneralData.Child("AIF_AYR_ANALIZ2");

                    if (oChildrenInkubasyon.Count > 0)
                    {
                        int drc = oChildrenInkubasyon.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenInkubasyon.Remove(0);
                    }

                    oChildrenInkubasyon = oGeneralData.Child("AIF_AYR_ANALIZ2");

                    foreach (var item in ayranAnalizTakibi.Inkubasyon)
                    {
                        oChildInkubasyon = oChildrenInkubasyon.Add();

                        oChildInkubasyon.SetProperty("U_InkubasyonNo", item.InkubasyonNo.ToString());

                        oChildInkubasyon.SetProperty("U_Saat", item.Saat);

                        oChildInkubasyon.SetProperty("U_Sicaklik", item.Sicaklik);

                        oChildInkubasyon.SetProperty("U_PH", item.PH);

                        oChildInkubasyon.SetProperty("U_KontrolEden", item.KontrolEdenNo);

                        oChildInkubasyon.SetProperty("U_KontrolEdenAdi", item.KontrolEdenAdi.ToString());
                    }

                    try
                    {
                        oGeneralService.Update(oGeneralData);
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
                        return new Response { Value = 0, Description = "Ayran Analiz Takibi girişi güncellendi.", List = null };
                    }
                    catch (Exception)
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
                        return new Response { Value = -8300, Description = "Hata Kodu - 8300 Ayran Analiz Takibi girişi güncellenirken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                    }
                }
            }
            catch (Exception ex)
            {
                LoginCompany.ReleaseConnection(clnum, dbCode, ID);
                return new Response { Value = -9000, Description = "Bilinmeyen Hata oluştu. " + ex.Message, List = null };
            }
            finally
            {
                LoginCompany.ReleaseConnection(clnum, dbCode, ID);
            }
        }

        public Response updateAyranAnalizTakibi(AyranAnalizTakibi ayranAnalizTakibi, string dbName, string mKodValue)
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

                CompanyService oCompService;
                oCompService = oCompany.GetCompanyService();

                GeneralService oGeneralService;

                GeneralData oGeneralData;

                GeneralDataParams oGeneralParams;

                GeneralData oChildMayalama;

                GeneralDataCollection oChildrenMayalama;

                GeneralData oChildInkubasyon;

                GeneralDataCollection oChildrenInkubasyon;

                oGeneralService = oCompService.GetGeneralService("AIF_AYR_ANALIZ");

                oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                oGeneralParams.SetProperty("DocEntry", ayranAnalizTakibi.DocEntry);
                oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                oChildrenMayalama = oGeneralData.Child("AIF_AYR_ANALIZ1");

                if (oChildrenMayalama.Count > 0)
                {
                    int drc = oChildrenMayalama.Count;
                    for (int rmv = 0; rmv < drc; rmv++)
                        oChildrenMayalama.Remove(0);
                }

                foreach (var item in ayranAnalizTakibi.MayalanacakSutOzellikleri)
                {
                    oChildMayalama = oChildrenMayalama.Add();

                    oChildMayalama.SetProperty("U_Kaynak", item.Kaynak);

                    oChildMayalama.SetProperty("U_KaynakAdi", item.KaynakAdi);

                    oChildMayalama.SetProperty("U_Brix", item.Brix);

                    oChildMayalama.SetProperty("U_PH", item.PH);

                    oChildMayalama.SetProperty("U_SH", item.SH);

                    oChildMayalama.SetProperty("U_Yag", item.Yag);
                }

                oChildrenInkubasyon = oGeneralData.Child("AIF_AYR_ANALIZ1");

                if (oChildrenInkubasyon.Count > 0)
                {
                    int drc = oChildrenInkubasyon.Count;
                    for (int rmv = 0; rmv < drc; rmv++)
                        oChildrenInkubasyon.Remove(0);
                }

                oChildrenInkubasyon = oGeneralData.Child("AIF_AYR_ANALIZ2");

                foreach (var item in ayranAnalizTakibi.Inkubasyon)
                {
                    oChildInkubasyon = oChildrenInkubasyon.Add();

                    oChildInkubasyon.SetProperty("U_InkubasyonNo", item.InkubasyonNo);

                    oChildInkubasyon.SetProperty("U_Saat", item.Saat);

                    oChildInkubasyon.SetProperty("U_Sicaklik", item.Sicaklik);

                    oChildInkubasyon.SetProperty("U_PH", item.PH);

                    oChildInkubasyon.SetProperty("U_KontrolEden", item.KontrolEdenNo);

                    oChildInkubasyon.SetProperty("U_KontrolEdenAdi", item.KontrolEdenAdi);
                }

                try
                {
                    oGeneralService.Update(oGeneralData);
                    LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                    return new Response { Value = 0, Description = "Ayran Analiz Takibi girişi güncellendi.", List = null };
                }
                catch (Exception)
                {
                    LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                    return new Response { Value = -8300, Description = "Hata Kodu - 8300 Ayran Analiz Takibi girişi güncellenirken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                }
            }
            catch (Exception ex)
            {
                LoginCompany.ReleaseConnection(clnum, dbCode,ID);
                return new Response { Value = 9000, Description = "Bilinmeyen hata oluştu. " + ex.Message, List = null };
            }

            finally
            {
                LoginCompany.ReleaseConnection(clnum, dbCode, ID);
            }
        }
    }
}