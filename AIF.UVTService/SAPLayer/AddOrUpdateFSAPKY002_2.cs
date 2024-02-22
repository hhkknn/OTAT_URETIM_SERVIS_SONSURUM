using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddOrUpdateFSAPKY002_2
    {
        public Response addOrUpdateFSAPKY002_2(FSAPKY002_2 fSAPKY002_2, string dbName, string mKodValue)
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

                oRS.DoQuery("Select * from \"@AIF_FSAPKY002_2\" WITH (NOLOCK) where \"U_PartiNo\" = '" + fSAPKY002_2.PartiNo + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    //GeneralData oChild_AIF_FSAPKY002_1;

                    //GeneralDataCollection oChildren_AIF_FSAPKY002_1;

                    GeneralData oChild_FSAPKY002_2_1;

                    GeneralDataCollection oChildren_FSAPKY002_2_1;

                    GeneralData oChild_FSAPKY002_2_2;

                    GeneralDataCollection oChildren_FSAPKY002_2_2; 
                     
                    oCompService = oCompany.GetCompanyService();

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_FSAPKY002_2");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralData.SetProperty("U_PartiNo", fSAPKY002_2.PartiNo.ToString());

                    oGeneralData.SetProperty("U_UrunKodu", fSAPKY002_2.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_UrunTanimi", fSAPKY002_2.UrunTanimi.ToString());
                     
                    oGeneralData.SetProperty("U_Kontrol1", fSAPKY002_2.Kontrol1.ToString());

                    oGeneralData.SetProperty("U_Kontrol2", fSAPKY002_2.Kontrol2.ToString());
                    //oGeneralData.SetProperty("U_Aciklama", fSAPKY002_1.Aciklama.ToString());

                    if (fSAPKY002_2.Tarih != null && fSAPKY002_2.Tarih != "")
                    {
                        string tarih = fSAPKY002_2.Tarih;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_Tarih", dt);
                    } 

                    oChildren_FSAPKY002_2_1 = oGeneralData.Child("AIF_FSAPKY002_2_1");

                    foreach (var item in fSAPKY002_2.fSAPKY002_2_1s)
                    {
                        oChild_FSAPKY002_2_1 = oChildren_FSAPKY002_2_1.Add();

                        oChild_FSAPKY002_2_1.SetProperty("U_TelemePros", item.TelemeProsesi);

                        oChild_FSAPKY002_2_1.SetProperty("U_TelemeMik", item.TelemeMiktari);

                        oChild_FSAPKY002_2_1.SetProperty("U_TelemePros2", item.TelemeProsesi2);

                        oChild_FSAPKY002_2_1.SetProperty("U_TelemeMik2", item.TelemeMiktari2);

                        oChild_FSAPKY002_2_1.SetProperty("U_HaslamaBasPH", item.HaslamaBaslangicPH);

                        oChild_FSAPKY002_2_1.SetProperty("U_BaslangicSaat", item.BaslangicSaati);

                        oChild_FSAPKY002_2_1.SetProperty("U_BitisSaati", item.BitisSaati);

                        oChild_FSAPKY002_2_1.SetProperty("U_HaslamaOprt", item.HaslamaOperatoru);
                    }

                    oChildren_FSAPKY002_2_2 = oGeneralData.Child("AIF_FSAPKY002_2_2");

                    foreach (var item in fSAPKY002_2.fSAPKY002_2_2s)
                    {
                        oChild_FSAPKY002_2_2 = oChildren_FSAPKY002_2_2.Add();

                        oChild_FSAPKY002_2_2.SetProperty("U_UrunKodu", item.UrunKodu);

                        oChild_FSAPKY002_2_2.SetProperty("U_UrunAdi", item.UrunAdi);

                        oChild_FSAPKY002_2_2.SetProperty("U_Ornek1", item.Ornek1);

                        oChild_FSAPKY002_2_2.SetProperty("U_Ornek2", item.Ornek2);

                        oChild_FSAPKY002_2_2.SetProperty("U_Ornek3", item.Ornek3);

                        oChild_FSAPKY002_2_2.SetProperty("U_Ornek4", item.Ornek4);

                        oChild_FSAPKY002_2_2.SetProperty("U_Ornek5", item.Ornek5);

                        oChild_FSAPKY002_2_2.SetProperty("U_Ornek6", item.Ornek6);

                        oChild_FSAPKY002_2_2.SetProperty("U_Ornek7", item.Ornek7);

                        oChild_FSAPKY002_2_2.SetProperty("U_GramOlcOprt", item.GramajOlcumOprt);
                    }
                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_FSAPKY002_2\" WITH (NOLOCK)");

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

                    GeneralData oChild_FSAPKY002_2_1;

                    GeneralDataCollection oChildren_FSAPKY002_2_1;

                    GeneralData oChild_FSAPKY002_2_2;

                    GeneralDataCollection oChildren_FSAPKY002_2_2;

                    oCompService = oCompany.GetCompanyService();

                    GeneralDataParams oGeneralParams;

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_FSAPKY002_2");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    oGeneralData.SetProperty("U_PartiNo", fSAPKY002_2.PartiNo.ToString());

                    oGeneralData.SetProperty("U_UrunKodu", fSAPKY002_2.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_UrunTanimi", fSAPKY002_2.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Kontrol1" , fSAPKY002_2.Kontrol1.ToString());

                    oGeneralData.SetProperty("U_Kontrol2", fSAPKY002_2.Kontrol2.ToString());

                    //oGeneralData.SetProperty("U_Aciklama", fSAPKY002_1.Aciklama.ToString());

                    if (fSAPKY002_2.Tarih != null && fSAPKY002_2.Tarih != "")
                    {
                        string tarih = fSAPKY002_2.Tarih;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_Tarih", dt);
                    } 

                    oChildren_FSAPKY002_2_1 = oGeneralData.Child("AIF_FSAPKY002_2_1");

                    if (oChildren_FSAPKY002_2_1.Count > 0)
                    {
                        int drc = oChildren_FSAPKY002_2_1.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY002_2_1.Remove(0);
                    }


                    foreach (var item in fSAPKY002_2.fSAPKY002_2_1s)
                    {
                        oChild_FSAPKY002_2_1 = oChildren_FSAPKY002_2_1.Add();

                        oChild_FSAPKY002_2_1.SetProperty("U_TelemePros", item.TelemeProsesi);

                        oChild_FSAPKY002_2_1.SetProperty("U_TelemeMik", item.TelemeMiktari);

                        oChild_FSAPKY002_2_1.SetProperty("U_TelemePros2", item.TelemeProsesi2);

                        oChild_FSAPKY002_2_1.SetProperty("U_TelemeMik2", item.TelemeMiktari2);

                        oChild_FSAPKY002_2_1.SetProperty("U_HaslamaBasPH", item.HaslamaBaslangicPH);

                        oChild_FSAPKY002_2_1.SetProperty("U_BaslangicSaat", item.BaslangicSaati);

                        oChild_FSAPKY002_2_1.SetProperty("U_BitisSaati", item.BitisSaati);

                        oChild_FSAPKY002_2_1.SetProperty("U_HaslamaOprt", item.HaslamaOperatoru);
                    }

                    oChildren_FSAPKY002_2_2 = oGeneralData.Child("AIF_FSAPKY002_2_2");

                    if (oChildren_FSAPKY002_2_2.Count > 0)
                    {
                        int drc = oChildren_FSAPKY002_2_2.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY002_2_2.Remove(0);
                    }

                    foreach (var item in fSAPKY002_2.fSAPKY002_2_2s)
                    {
                        oChild_FSAPKY002_2_2 = oChildren_FSAPKY002_2_2.Add();

                        oChild_FSAPKY002_2_2.SetProperty("U_UrunKodu", item.UrunKodu);

                        oChild_FSAPKY002_2_2.SetProperty("U_UrunAdi", item.UrunAdi);

                        oChild_FSAPKY002_2_2.SetProperty("U_Ornek1", item.Ornek1);

                        oChild_FSAPKY002_2_2.SetProperty("U_Ornek2", item.Ornek2);

                        oChild_FSAPKY002_2_2.SetProperty("U_Ornek3", item.Ornek3);

                        oChild_FSAPKY002_2_2.SetProperty("U_Ornek4", item.Ornek4);

                        oChild_FSAPKY002_2_2.SetProperty("U_Ornek5", item.Ornek5);

                        oChild_FSAPKY002_2_2.SetProperty("U_Ornek6", item.Ornek6);

                        oChild_FSAPKY002_2_2.SetProperty("U_Ornek7", item.Ornek7);

                        oChild_FSAPKY002_2_2.SetProperty("U_GramOlcOprt", item.GramajOlcumOprt);
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