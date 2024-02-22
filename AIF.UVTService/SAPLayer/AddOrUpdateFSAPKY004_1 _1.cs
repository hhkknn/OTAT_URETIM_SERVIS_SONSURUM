using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddOrUpdateFSAPKY004_1_1
    {
        public Response addOrUpdateFSAPKY004_1_1(FSAPKY004_1_1 fSAPKY004_1_1, string dbName, string mKodValue)
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

                oRS.DoQuery("Select * from \"@AIF_FSAPKY004_1_1\" WITH (NOLOCK) where \"U_PartiNo\" = '" + fSAPKY004_1_1.PartiNo + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChild_FSAPKY004_1_1_1;

                    GeneralDataCollection oChildren_FSAPKY004_1_1_1;

                    GeneralData oChild_FSAPKY004_1_1_2;

                    GeneralDataCollection oChildren_FSAPKY004_1_1_2;

                    GeneralData oChild_FSAPKY004_1_2_1;

                    GeneralDataCollection oChildren_FSAPKY004_1_2_1;

                    GeneralData oChild_FSAPKY004_1_2_2;

                    GeneralDataCollection oChildren_FSAPKY004_1_2_2;

                    GeneralData oChild_FSAPKY004_1_2_3;

                    GeneralDataCollection oChildren_FSAPKY004_1_2_3;

                    oCompService = oCompany.GetCompanyService();

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_FSAPKY004_1_1");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralData.SetProperty("U_PartiNo", fSAPKY004_1_1.PartiNo.ToString());

                    oGeneralData.SetProperty("U_UrunKodu", fSAPKY004_1_1.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_UrunTanimi", fSAPKY004_1_1.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Kontrol1", fSAPKY004_1_1.Kontrol1.ToString());

                    oGeneralData.SetProperty("U_Kontrol2", fSAPKY004_1_1.Kontrol2.ToString());

                    //oGeneralData.SetProperty("U_Aciklama", fSAPKY004_1_1.Aciklama.ToString());

                    if (fSAPKY004_1_1.Tarih != null && fSAPKY004_1_1.Tarih != "")
                    {
                        string tarih = fSAPKY004_1_1.Tarih;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_Tarih", dt);
                    }

                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    //oGeneralData.SetProperty("U_Tarih", dt);

                    oChildren_FSAPKY004_1_1_1 = oGeneralData.Child("AIF_FSAPKY004_1_1_1");

                    foreach (var item in fSAPKY004_1_1.fSAPKY004_1_1_1s)
                    {
                        oChild_FSAPKY004_1_1_1 = oChildren_FSAPKY004_1_1_1.Add();

                        oChild_FSAPKY004_1_1_1.SetProperty("U_ProsTnkNo", item.ProsesTankNo);

                        oChild_FSAPKY004_1_1_1.SetProperty("U_UFSutMik", item.UFSutuMiktari);

                        oChild_FSAPKY004_1_1_1.SetProperty("U_ReworkMik", item.ReworkMiktari);

                        oChild_FSAPKY004_1_1_1.SetProperty("U_MayaSicaklik", item.MayalamaSicakligi);

                        oChild_FSAPKY004_1_1_1.SetProperty("U_MayaSaat", item.MayalamaSaati);

                        oChild_FSAPKY004_1_1_1.SetProperty("U_KirimSaat", item.KirimSaati);

                        oChild_FSAPKY004_1_1_1.SetProperty("U_UFOprtAdi", item.UFOperatorAdi);
                    }

                    oChildren_FSAPKY004_1_1_2 = oGeneralData.Child("AIF_FSAPKY004_1_1_2");

                    foreach (var item in fSAPKY004_1_1.fSAPKY004_1_1_2s)
                    {
                        oChild_FSAPKY004_1_1_2 = oChildren_FSAPKY004_1_1_2.Add();

                        oChild_FSAPKY004_1_1_2.SetProperty("U_UFSutPH", item.UFSutuPH);

                        oChild_FSAPKY004_1_1_2.SetProperty("U_UFSutSH", item.UFSutuSH);

                        oChild_FSAPKY004_1_1_2.SetProperty("U_UFSutKM", item.UFSutuKM);

                        oChild_FSAPKY004_1_1_2.SetProperty("U_UFSutYag", item.UFSutuYagi);

                        oChild_FSAPKY004_1_1_2.SetProperty("U_KirimPH", item.KirimPH);

                        oChild_FSAPKY004_1_1_2.SetProperty("U_KirimSH", item.KirimSH);

                        oChild_FSAPKY004_1_1_2.SetProperty("U_LabPersAdi", item.LabPersonelAdi);
                    }

                    oChildren_FSAPKY004_1_2_1 = oGeneralData.Child("AIF_FSAPKY004_1_2_1");

                    foreach (var item in fSAPKY004_1_1.fSAPKY004_1_2_1s)
                    {
                        oChild_FSAPKY004_1_2_1 = oChildren_FSAPKY004_1_2_1.Add();

                        oChild_FSAPKY004_1_2_1.SetProperty("U_UFGirisSic", item.UFGirisSicakligi);

                        oChild_FSAPKY004_1_2_1.SetProperty("U_UFGirisSaat", item.UFGirisSaati);

                        oChild_FSAPKY004_1_2_1.SetProperty("U_UFFaktor", item.UFFaktoru);

                        oChild_FSAPKY004_1_2_1.SetProperty("U_UFCikisSic", item.UFCikisSicakligi);

                        oChild_FSAPKY004_1_2_1.SetProperty("U_UFBitSaat", item.UFBitisSaati);

                        oChild_FSAPKY004_1_2_1.SetProperty("U_UFOprtAdi", item.UFOperatorAdi); 
                    }

                    oChildren_FSAPKY004_1_2_2 = oGeneralData.Child("AIF_FSAPKY004_1_2_2");

                    foreach (var item in fSAPKY004_1_1.fSAPKY004_1_2_2s)
                    {
                        oChild_FSAPKY004_1_2_2 = oChildren_FSAPKY004_1_2_2.Add();

                        oChild_FSAPKY004_1_2_2.SetProperty("U_ReworkPH", item.ReworkPH);

                        oChild_FSAPKY004_1_2_2.SetProperty("U_ReworkKM", item.ReworkKM);

                        oChild_FSAPKY004_1_2_2.SetProperty("U_ReworkYag", item.ReworkYag); 

                        oChild_FSAPKY004_1_2_2.SetProperty("U_LabPersAdi", item.LabPersonelAdi);
                    }

                    oChildren_FSAPKY004_1_2_3 = oGeneralData.Child("AIF_FSAPKY004_1_2_3");

                    foreach (var item in fSAPKY004_1_1.fSAPKY004_1_2_3s)
                    {
                        oChild_FSAPKY004_1_2_3 = oChildren_FSAPKY004_1_2_3.Add();

                        oChild_FSAPKY004_1_2_3.SetProperty("U_KnsUrunPH", item.KonsantreUrunPH);

                        oChild_FSAPKY004_1_2_3.SetProperty("U_KnsUrunSH", item.KonsantreUrunSH);

                        oChild_FSAPKY004_1_2_3.SetProperty("U_KnsUrunKM", item.KonsantreUrunKM);

                        oChild_FSAPKY004_1_2_3.SetProperty("U_KnsUrunYag", item.KonsantreUrunYag);

                        oChild_FSAPKY004_1_2_3.SetProperty("U_LabPersAdi", item.LabPersonelAdi);
                    }

                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_FSAPKY004_1_1\" WITH (NOLOCK)");

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
                        return new Response { Value = 0, Description = "Analiz girişi oluşturuldu..", List = null };
                    }
                    else
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5200, Description = "Hata Kodu - 5200 Analiz girişi oluşturulurken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                    }
                }
                else
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChild_FSAPKY004_1_1_1;

                    GeneralDataCollection oChildren_FSAPKY004_1_1_1;

                    GeneralData oChild_FSAPKY004_1_1_2;

                    GeneralDataCollection oChildren_FSAPKY004_1_1_2;

                    GeneralData oChild_FSAPKY004_1_2_1;

                    GeneralDataCollection oChildren_FSAPKY004_1_2_1;

                    GeneralData oChild_FSAPKY004_1_2_2;

                    GeneralDataCollection oChildren_FSAPKY004_1_2_2;

                    GeneralData oChild_FSAPKY004_1_2_3;

                    GeneralDataCollection oChildren_FSAPKY004_1_2_3;

                    oCompService = oCompany.GetCompanyService();

                    GeneralDataParams oGeneralParams;

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_FSAPKY004_1_1");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    oGeneralData.SetProperty("U_PartiNo", fSAPKY004_1_1.PartiNo.ToString());

                    oGeneralData.SetProperty("U_UrunKodu", fSAPKY004_1_1.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_UrunTanimi", fSAPKY004_1_1.UrunTanimi.ToString());

                    //oGeneralData.SetProperty("U_Aciklama", fSAPKY004_1_1.Aciklama.ToString());

                    oGeneralData.SetProperty("U_Kontrol1", fSAPKY004_1_1.Kontrol1.ToString());

                    oGeneralData.SetProperty("U_Kontrol2", fSAPKY004_1_1.Kontrol2.ToString());

                    if (fSAPKY004_1_1.Tarih != null && fSAPKY004_1_1.Tarih != "")
                    {
                        string tarih = fSAPKY004_1_1.Tarih;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_Tarih", dt);
                    }

                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    //oGeneralData.SetProperty("U_Tarih", dt);

                    oChildren_FSAPKY004_1_1_1 = oGeneralData.Child("AIF_FSAPKY004_1_1_1");

                    if (oChildren_FSAPKY004_1_1_1.Count > 0)
                    {
                        int drc = oChildren_FSAPKY004_1_1_1.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY004_1_1_1.Remove(0);
                    }

                    foreach (var item in fSAPKY004_1_1.fSAPKY004_1_1_1s)
                    {
                        oChild_FSAPKY004_1_1_1 = oChildren_FSAPKY004_1_1_1.Add();

                        oChild_FSAPKY004_1_1_1.SetProperty("U_ProsTnkNo", item.ProsesTankNo);

                        oChild_FSAPKY004_1_1_1.SetProperty("U_UFSutMik", item.UFSutuMiktari);

                        oChild_FSAPKY004_1_1_1.SetProperty("U_ReworkMik", item.ReworkMiktari);

                        oChild_FSAPKY004_1_1_1.SetProperty("U_MayaSicaklik", item.MayalamaSicakligi);

                        oChild_FSAPKY004_1_1_1.SetProperty("U_MayaSaat", item.MayalamaSaati);

                        oChild_FSAPKY004_1_1_1.SetProperty("U_KirimSaat", item.KirimSaati);

                        oChild_FSAPKY004_1_1_1.SetProperty("U_UFOprtAdi", item.UFOperatorAdi);
                    }

                    oChildren_FSAPKY004_1_1_2 = oGeneralData.Child("AIF_FSAPKY004_1_1_2");

                    if (oChildren_FSAPKY004_1_1_2.Count > 0)
                    {
                        int drc = oChildren_FSAPKY004_1_1_2.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY004_1_1_2.Remove(0);
                    }

                    foreach (var item in fSAPKY004_1_1.fSAPKY004_1_1_2s)
                    {
                        oChild_FSAPKY004_1_1_2 = oChildren_FSAPKY004_1_1_2.Add();

                        oChild_FSAPKY004_1_1_2.SetProperty("U_UFSutPH", item.UFSutuPH);

                        oChild_FSAPKY004_1_1_2.SetProperty("U_UFSutSH", item.UFSutuSH);

                        oChild_FSAPKY004_1_1_2.SetProperty("U_UFSutKM", item.UFSutuKM);

                        oChild_FSAPKY004_1_1_2.SetProperty("U_UFSutYag", item.UFSutuYagi);

                        oChild_FSAPKY004_1_1_2.SetProperty("U_KirimPH", item.KirimPH);

                        oChild_FSAPKY004_1_1_2.SetProperty("U_KirimSH", item.KirimSH);

                        oChild_FSAPKY004_1_1_2.SetProperty("U_LabPersAdi", item.LabPersonelAdi);
                    }

                    oChildren_FSAPKY004_1_2_1 = oGeneralData.Child("AIF_FSAPKY004_1_2_1");

                    if (oChildren_FSAPKY004_1_2_1.Count > 0)
                    {
                        int drc = oChildren_FSAPKY004_1_2_1.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY004_1_2_1.Remove(0);
                    }

                    foreach (var item in fSAPKY004_1_1.fSAPKY004_1_2_1s)
                    {
                        oChild_FSAPKY004_1_2_1 = oChildren_FSAPKY004_1_2_1.Add();

                        oChild_FSAPKY004_1_2_1.SetProperty("U_UFGirisSic", item.UFGirisSicakligi);

                        oChild_FSAPKY004_1_2_1.SetProperty("U_UFGirisSaat", item.UFGirisSaati);

                        oChild_FSAPKY004_1_2_1.SetProperty("U_UFFaktor", item.UFFaktoru);

                        oChild_FSAPKY004_1_2_1.SetProperty("U_UFCikisSic", item.UFCikisSicakligi);

                        oChild_FSAPKY004_1_2_1.SetProperty("U_UFBitSaat", item.UFBitisSaati);

                        oChild_FSAPKY004_1_2_1.SetProperty("U_UFOprtAdi", item.UFOperatorAdi);
                    }

                    oChildren_FSAPKY004_1_2_2 = oGeneralData.Child("AIF_FSAPKY004_1_2_2");

                    if (oChildren_FSAPKY004_1_2_2.Count > 0)
                    {
                        int drc = oChildren_FSAPKY004_1_2_2.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY004_1_2_2.Remove(0);
                    }

                    foreach (var item in fSAPKY004_1_1.fSAPKY004_1_2_2s)
                    {
                        oChild_FSAPKY004_1_2_2 = oChildren_FSAPKY004_1_2_2.Add();

                        oChild_FSAPKY004_1_2_2.SetProperty("U_ReworkPH", item.ReworkPH);

                        oChild_FSAPKY004_1_2_2.SetProperty("U_ReworkKM", item.ReworkKM);

                        oChild_FSAPKY004_1_2_2.SetProperty("U_ReworkYag", item.ReworkYag);

                        oChild_FSAPKY004_1_2_2.SetProperty("U_LabPersAdi", item.LabPersonelAdi);
                    }

                    oChildren_FSAPKY004_1_2_3 = oGeneralData.Child("AIF_FSAPKY004_1_2_3");

                    if (oChildren_FSAPKY004_1_2_3.Count > 0)
                    {
                        int drc = oChildren_FSAPKY004_1_2_3.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY004_1_2_3.Remove(0);
                    }

                    foreach (var item in fSAPKY004_1_1.fSAPKY004_1_2_3s)
                    {
                        oChild_FSAPKY004_1_2_3 = oChildren_FSAPKY004_1_2_3.Add();

                        oChild_FSAPKY004_1_2_3.SetProperty("U_KnsUrunPH", item.KonsantreUrunPH);

                        oChild_FSAPKY004_1_2_3.SetProperty("U_KnsUrunSH", item.KonsantreUrunSH);

                        oChild_FSAPKY004_1_2_3.SetProperty("U_KnsUrunKM", item.KonsantreUrunKM);

                        oChild_FSAPKY004_1_2_3.SetProperty("U_KnsUrunYag", item.KonsantreUrunYag);

                        oChild_FSAPKY004_1_2_3.SetProperty("U_LabPersAdi", item.LabPersonelAdi);
                    }

                    try
                    {
                        oGeneralService.Update(oGeneralData);
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = 0, Description = "Analiz girişi güncellendi.", List = null };
                    }
                    catch (Exception)
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5300, Description = "Hata Kodu - 5300 Analiz girişi güncellenirken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
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