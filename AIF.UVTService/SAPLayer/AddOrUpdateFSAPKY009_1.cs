using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddOrUpdateFSAPKY009_1
    {
        public Response addOrUpdateFSAPKY009_1(FSAPKY009_1 fSAPKY009_1, string dbName, string mKodValue)
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

                oRS.DoQuery("Select * from \"@AIF_FSAPKY009_1\" WITH (NOLOCK) where \"U_PartiNo\" = '" + fSAPKY009_1.PartiNo + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChild_FSAPKY009_1_1;

                    GeneralDataCollection oChildren_FSAPKY009_1_1;

                    GeneralData oChild_FSAPKY009_1_2;

                    GeneralDataCollection oChildren_FSAPKY009_1_2;


                    oCompService = oCompany.GetCompanyService();

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_FSAPKY009_1");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralData.SetProperty("U_PartiNo", fSAPKY009_1.PartiNo.ToString());

                    oGeneralData.SetProperty("U_UrunKodu", fSAPKY009_1.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_UrunTanimi", fSAPKY009_1.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Aciklama", fSAPKY009_1.Aciklama.ToString());

                    if (fSAPKY009_1.Tarih != null && fSAPKY009_1.Tarih != "")
                    {
                        string tarih = fSAPKY009_1.Tarih;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_Tarih", dt);
                    } 

                    oChildren_FSAPKY009_1_1 = oGeneralData.Child("AIF_FSAPKY009_1_1");

                    foreach (var item in fSAPKY009_1.fSAPKY009_1_1s)
                    {
                        oChild_FSAPKY009_1_1 = oChildren_FSAPKY009_1_1.Add();

                        oChild_FSAPKY009_1_1.SetProperty("U_PisirNo", item.PisirmeNo);

                        oChild_FSAPKY009_1_1.SetProperty("U_BasSaat", item.BaslangicSaati);

                        oChild_FSAPKY009_1_1.SetProperty("U_PisirSic", item.PisirimSicakligi);

                        oChild_FSAPKY009_1_1.SetProperty("U_BitSaat", item.BitisSaati);

                        oChild_FSAPKY009_1_1.SetProperty("U_UFOprtAdi", item.UFOperatorAdi);

                        oChild_FSAPKY009_1_1.SetProperty("U_PisOnYrmPH", item.PisirimOncesiYariMamulPH);

                        oChild_FSAPKY009_1_1.SetProperty("U_PisSonYrmPH", item.PisirimSonrasiYariMamulPH);

                        oChild_FSAPKY009_1_1.SetProperty("U_KuruMadde", item.Kurumadde);

                        oChild_FSAPKY009_1_1.SetProperty("U_Yag", item.Yag);

                        oChild_FSAPKY009_1_1.SetProperty("U_LabPersAdi", item.LabPersonelAdi);
                    }


                    oChildren_FSAPKY009_1_2 = oGeneralData.Child("AIF_FSAPKY009_1_2");

                    foreach (var item in fSAPKY009_1.fSAPKY009_1_2s)
                    {
                        oChild_FSAPKY009_1_2 = oChildren_FSAPKY009_1_2.Add();

                        oChild_FSAPKY009_1_2.SetProperty("U_UrunKodu", item.UrunKodu);

                        oChild_FSAPKY009_1_2.SetProperty("U_UrunAdi", item.UrunAdi);

                        oChild_FSAPKY009_1_2.SetProperty("U_DuyAnOrn", item.DuyusalAnalizOnayi);

                        oChild_FSAPKY009_1_2.SetProperty("U_SorumluMuh", item.SorumluMuhendis); 
                    }


                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_FSAPKY009_1\" WITH (NOLOCK)");

                    int maxdocentry = Convert.ToInt32(oRS.Fields.Item(0).Value);

                    oGeneralData.SetProperty("DocNum", maxdocentry);

                    var resp = oGeneralService.Add(oGeneralData);

                    if (resp != null)
                    { 
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

                    GeneralData oChild_FSAPKY009_1_1;

                    GeneralDataCollection oChildren_FSAPKY009_1_1;

                    GeneralData oChild_FSAPKY009_1_2;

                    GeneralDataCollection oChildren_FSAPKY009_1_2;

                    oCompService = oCompany.GetCompanyService();

                    GeneralDataParams oGeneralParams;

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_FSAPKY009_1");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    oGeneralData.SetProperty("U_PartiNo", fSAPKY009_1.PartiNo.ToString());

                    oGeneralData.SetProperty("U_UrunKodu", fSAPKY009_1.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_UrunTanimi", fSAPKY009_1.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Aciklama", fSAPKY009_1.Aciklama.ToString());

                    if (fSAPKY009_1.Tarih != null && fSAPKY009_1.Tarih != "")
                    {
                        string tarih = fSAPKY009_1.Tarih;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_Tarih", dt);
                    } 

                    oChildren_FSAPKY009_1_1 = oGeneralData.Child("AIF_FSAPKY009_1_1");

                    if (oChildren_FSAPKY009_1_1.Count > 0)
                    {
                        int drc = oChildren_FSAPKY009_1_1.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY009_1_1.Remove(0);
                    }

                    foreach (var item in fSAPKY009_1.fSAPKY009_1_1s)
                    {
                        oChild_FSAPKY009_1_1 = oChildren_FSAPKY009_1_1.Add();

                        oChild_FSAPKY009_1_1.SetProperty("U_PisirNo", item.PisirmeNo);

                        oChild_FSAPKY009_1_1.SetProperty("U_BasSaat", item.BaslangicSaati);

                        oChild_FSAPKY009_1_1.SetProperty("U_PisirSic", item.PisirimSicakligi);

                        oChild_FSAPKY009_1_1.SetProperty("U_BitSaat", item.BitisSaati);

                        oChild_FSAPKY009_1_1.SetProperty("U_UFOprtAdi", item.UFOperatorAdi);

                        oChild_FSAPKY009_1_1.SetProperty("U_PisOnYrmPH", item.PisirimOncesiYariMamulPH);

                        oChild_FSAPKY009_1_1.SetProperty("U_PisSonYrmPH", item.PisirimSonrasiYariMamulPH);

                        oChild_FSAPKY009_1_1.SetProperty("U_KuruMadde", item.Kurumadde);

                        oChild_FSAPKY009_1_1.SetProperty("U_Yag", item.Yag);

                        oChild_FSAPKY009_1_1.SetProperty("U_LabPersAdi", item.LabPersonelAdi);
                    }


                    oChildren_FSAPKY009_1_2 = oGeneralData.Child("AIF_FSAPKY009_1_2");

                    if (oChildren_FSAPKY009_1_2.Count > 0)
                    {
                        int drc = oChildren_FSAPKY009_1_2.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY009_1_2.Remove(0);
                    }

                    foreach (var item in fSAPKY009_1.fSAPKY009_1_2s)
                    {
                        oChild_FSAPKY009_1_2 = oChildren_FSAPKY009_1_2.Add();

                        oChild_FSAPKY009_1_2.SetProperty("U_UrunKodu", item.UrunKodu);

                        oChild_FSAPKY009_1_2.SetProperty("U_UrunAdi", item.UrunAdi);

                        oChild_FSAPKY009_1_2.SetProperty("U_DuyAnOrn", item.DuyusalAnalizOnayi);

                        oChild_FSAPKY009_1_2.SetProperty("U_SorumluMuh", item.SorumluMuhendis);
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