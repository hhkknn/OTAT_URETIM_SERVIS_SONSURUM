using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddOrUpdateFSAPKY010_1
    {
        public Response addOrUpdateFSAPKY010_1(FSAPKY010_1 fSAPKY010_1, string dbName, string mKodValue)
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

                oRS.DoQuery("Select * from \"@AIF_FSAPKY010_1\" WITH (NOLOCK) where \"U_PartiNo\" = '" + fSAPKY010_1.PartiNo + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChild_FSAPKY010_1_1;

                    GeneralDataCollection oChildren_FSAPKY010_1_1;
                     

                    oCompService = oCompany.GetCompanyService();

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_FSAPKY010_1");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralData.SetProperty("U_PartiNo", fSAPKY010_1.PartiNo.ToString());

                    oGeneralData.SetProperty("U_UrunKodu", fSAPKY010_1.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_UrunTanimi", fSAPKY010_1.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Aciklama", fSAPKY010_1.Aciklama.ToString());

                    if (fSAPKY010_1.Tarih != null && fSAPKY010_1.Tarih != "")
                    {
                        string tarih = fSAPKY010_1.Tarih;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_Tarih", dt);
                    } 

                    oChildren_FSAPKY010_1_1 = oGeneralData.Child("AIF_FSAPKY010_1_1");

                    foreach (var item in fSAPKY010_1.fSAPKY010_1_1s)
                    {
                        oChild_FSAPKY010_1_1 = oChildren_FSAPKY010_1_1.Add();

                        oChild_FSAPKY010_1_1.SetProperty("U_KullKrema", item.KullanilanKrema);

                        oChild_FSAPKY010_1_1.SetProperty("U_KreYagOr", item.KremaYagOrani);

                        oChild_FSAPKY010_1_1.SetProperty("U_KreSicak", item.KremaSicakligi);

                        oChild_FSAPKY010_1_1.SetProperty("U_YayBasSaat", item.YayiklamaBaslangicSaati);

                        oChild_FSAPKY010_1_1.SetProperty("U_YayBitSaat", item.YayiklamaBitisSaati);

                        oChild_FSAPKY010_1_1.SetProperty("U_TrygRand", item.TereyagRandimani);

                        oChild_FSAPKY010_1_1.SetProperty("U_OprtAdi", item.OperatorAdi); 
                    }

                     
                     
                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_FSAPKY010_1\" WITH (NOLOCK)");

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

                    GeneralData oChild_FSAPKY010_1_1;

                    GeneralDataCollection oChildren_FSAPKY010_1_1; 

                    oCompService = oCompany.GetCompanyService();

                    GeneralDataParams oGeneralParams;

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_FSAPKY010_1");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    oGeneralData.SetProperty("U_PartiNo", fSAPKY010_1.PartiNo.ToString());

                    oGeneralData.SetProperty("U_UrunKodu", fSAPKY010_1.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_UrunTanimi", fSAPKY010_1.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Aciklama", fSAPKY010_1.Aciklama.ToString());

                    if (fSAPKY010_1.Tarih != null && fSAPKY010_1.Tarih != "")
                    {
                        string tarih = fSAPKY010_1.Tarih;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_Tarih", dt);
                    } 

                    oChildren_FSAPKY010_1_1 = oGeneralData.Child("AIF_FSAPKY010_1_1");

                    if (oChildren_FSAPKY010_1_1.Count > 0)
                    {
                        int drc = oChildren_FSAPKY010_1_1.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY010_1_1.Remove(0);
                    }

                    foreach (var item in fSAPKY010_1.fSAPKY010_1_1s)
                    {
                        oChild_FSAPKY010_1_1 = oChildren_FSAPKY010_1_1.Add();

                        oChild_FSAPKY010_1_1.SetProperty("U_KullKrema", item.KullanilanKrema);

                        oChild_FSAPKY010_1_1.SetProperty("U_KreYagOr", item.KremaYagOrani);

                        oChild_FSAPKY010_1_1.SetProperty("U_KreSicak", item.KremaSicakligi);

                        oChild_FSAPKY010_1_1.SetProperty("U_YayBasSaat", item.YayiklamaBaslangicSaati);

                        oChild_FSAPKY010_1_1.SetProperty("U_YayBitSaat", item.YayiklamaBitisSaati);

                        oChild_FSAPKY010_1_1.SetProperty("U_TrygRand", item.TereyagRandimani);

                        oChild_FSAPKY010_1_1.SetProperty("U_OprtAdi", item.OperatorAdi);
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