using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddOrUpdateFSAPKY012_2
    {
        public Response addOrUpdateFSAPKY012_2(FSAPKY012_2 fSAPKY012_2, string dbName, string mKodValue)
        {
            Random rastgele = new Random();
            int ID = rastgele.Next(0, 9999);

            int clnum = 0;
            string dbCode = "";
            try
            {
                ConnectionList connection = new ConnectionList();

                LoginCompany log = new LoginCompany();

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

                oRS.DoQuery("Select * from \"@AIF_FSAPKY012_2\" WITH (NOLOCK) where \"U_PartiNo\" = '" + fSAPKY012_2.PartiNo + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChild_FSAPKY012_2_1;

                    GeneralDataCollection oChildren_FSAPKY012_2_1;

                    GeneralData oChild_FSAPKY012_2_2;

                    GeneralDataCollection oChildren_FSAPKY012_2_2;


                    oCompService = oCompany.GetCompanyService();

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_FSAPKY012_2");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralData.SetProperty("U_PartiNo", fSAPKY012_2.PartiNo.ToString());

                    oGeneralData.SetProperty("U_UrunKodu", fSAPKY012_2.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_UrunTanimi", fSAPKY012_2.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Aciklama", fSAPKY012_2.Aciklama.ToString());

                    oGeneralData.SetProperty("U_Kontrol1", fSAPKY012_2.Kontrol1.ToString());

                    if (fSAPKY012_2.Tarih != null && fSAPKY012_2.Tarih != "")
                    {
                        string tarih = fSAPKY012_2.Tarih;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_Tarih", dt);
                    } 

                    oChildren_FSAPKY012_2_1 = oGeneralData.Child("AIF_FSAPKY012_2_1");

                    foreach (var item in fSAPKY012_2.fSAPKY012_2_1s)
                    {
                        oChild_FSAPKY012_2_1 = oChildren_FSAPKY012_2_1.Add();

                        oChild_FSAPKY012_2_1.SetProperty("U_UrunAdi", item.UrunAdi);

                        oChild_FSAPKY012_2_1.SetProperty("U_PaketSic", item.PaketlemeSicakligi);

                        if (item.UretimTarihi.Year != 1900)
                        {
                            oChild_FSAPKY012_2_1.SetProperty("U_UretimTar", item.UretimTarihi);

                        }

                        if (item.SonTuketimTarihi.Year != 1900)
                        {
                            oChild_FSAPKY012_2_1.SetProperty("U_SonTukTar", item.SonTuketimTarihi);

                        } 

                        oChild_FSAPKY012_2_1.SetProperty("U_PartiNo", item.PartiNo);

                        oChild_FSAPKY012_2_1.SetProperty("U_OprtAdi", item.OperatorAdi); 
                    }


                    oChildren_FSAPKY012_2_2 = oGeneralData.Child("AIF_FSAPKY012_2_2");

                    foreach (var item in fSAPKY012_2.fSAPKY012_2_2s)
                    {
                        oChild_FSAPKY012_2_2 = oChildren_FSAPKY012_2_2.Add();

                        oChild_FSAPKY012_2_2.SetProperty("U_UrunAdi", item.UrunAdi);

                        oChild_FSAPKY012_2_2.SetProperty("U_pHOlcum1", item.PHOlcum1); 
                        oChild_FSAPKY012_2_2.SetProperty("U_pHOlcum2", item.PHOlcum2); 
                        oChild_FSAPKY012_2_2.SetProperty("U_pHOlcum3", item.PHOlcum3); 
                        oChild_FSAPKY012_2_2.SetProperty("U_pHOlcum4", item.PHOlcum4); 
                        oChild_FSAPKY012_2_2.SetProperty("U_pHOlcum5", item.PHOlcum5); 
                        oChild_FSAPKY012_2_2.SetProperty("U_pHOlcum6", item.PHOlcum6); 
                        oChild_FSAPKY012_2_2.SetProperty("U_pHOlcum7", item.PHOlcum7); 
                        oChild_FSAPKY012_2_2.SetProperty("U_pHOlcum8", item.PHOlcum8); 
                        oChild_FSAPKY012_2_2.SetProperty("U_pHOlcum9", item.PHOlcum9); 
                        oChild_FSAPKY012_2_2.SetProperty("U_pHOlcum10", item.PHOlcum10);

                        oChild_FSAPKY012_2_2.SetProperty("U_pHMiktar1", item.PHMiktar1);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHMiktar2", item.PHMiktar2);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHMiktar3", item.PHMiktar3);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHMiktar4", item.PHMiktar4);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHMiktar5", item.PHMiktar5);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHMiktar6", item.PHMiktar6);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHMiktar7", item.PHMiktar7);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHMiktar8", item.PHMiktar8);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHMiktar9", item.PHMiktar9);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHMiktar10", item.PHMiktar10);

                        oChild_FSAPKY012_2_2.SetProperty("U_pHSaat1", item.PHSaat1);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHSaat2", item.PHSaat2);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHSaat3", item.PHSaat3);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHSaat4", item.PHSaat4);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHSaat5", item.PHSaat5);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHSaat6", item.PHSaat6);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHSaat7", item.PHSaat7);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHSaat8", item.PHSaat8);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHSaat9", item.PHSaat9);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHSaat10", item.PHSaat10);

                        oChild_FSAPKY012_2_2.SetProperty("U_OprtAdi", item.OperatorAdi);
                    }


                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_FSAPKY012_2\" WITH (NOLOCK)");

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

                    GeneralData oChild_FSAPKY012_2_1;

                    GeneralDataCollection oChildren_FSAPKY012_2_1;

                    GeneralData oChild_FSAPKY012_2_2;

                    GeneralDataCollection oChildren_FSAPKY012_2_2;

                    oCompService = oCompany.GetCompanyService();

                    GeneralDataParams oGeneralParams;

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_FSAPKY012_2");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    oGeneralData.SetProperty("U_PartiNo", fSAPKY012_2.PartiNo.ToString());

                    oGeneralData.SetProperty("U_UrunKodu", fSAPKY012_2.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_UrunTanimi", fSAPKY012_2.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Aciklama", fSAPKY012_2.Aciklama.ToString());

                    oGeneralData.SetProperty("U_Kontrol1", fSAPKY012_2.Kontrol1.ToString());

                    if (fSAPKY012_2.Tarih != null && fSAPKY012_2.Tarih != "")
                    {
                        string tarih = fSAPKY012_2.Tarih;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_Tarih", dt);
                    } 

                    oChildren_FSAPKY012_2_1 = oGeneralData.Child("AIF_FSAPKY012_2_1");

                    if (oChildren_FSAPKY012_2_1.Count > 0)
                    {
                        int drc = oChildren_FSAPKY012_2_1.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY012_2_1.Remove(0);
                    }

                    foreach (var item in fSAPKY012_2.fSAPKY012_2_1s)
                    {
                        oChild_FSAPKY012_2_1 = oChildren_FSAPKY012_2_1.Add();

                        oChild_FSAPKY012_2_1.SetProperty("U_UrunAdi", item.UrunAdi);

                        oChild_FSAPKY012_2_1.SetProperty("U_PaketSic", item.PaketlemeSicakligi);

                        if (item.UretimTarihi.Year != 1900)
                        {
                            oChild_FSAPKY012_2_1.SetProperty("U_UretimTar", item.UretimTarihi);

                        }

                        if (item.SonTuketimTarihi.Year != 1900)
                        {
                            oChild_FSAPKY012_2_1.SetProperty("U_SonTukTar", item.SonTuketimTarihi);

                        }

                        oChild_FSAPKY012_2_1.SetProperty("U_PartiNo", item.PartiNo);

                        oChild_FSAPKY012_2_1.SetProperty("U_OprtAdi", item.OperatorAdi);
                    }


                    oChildren_FSAPKY012_2_2 = oGeneralData.Child("AIF_FSAPKY012_2_2");

                    if (oChildren_FSAPKY012_2_2.Count > 0)
                    {
                        int drc = oChildren_FSAPKY012_2_2.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY012_2_2.Remove(0);
                    }

                    foreach (var item in fSAPKY012_2.fSAPKY012_2_2s)
                    {
                        oChild_FSAPKY012_2_2 = oChildren_FSAPKY012_2_2.Add();

                        oChild_FSAPKY012_2_2.SetProperty("U_UrunAdi", item.UrunAdi);

                        oChild_FSAPKY012_2_2.SetProperty("U_pHOlcum1", item.PHOlcum1);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHOlcum2", item.PHOlcum2);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHOlcum3", item.PHOlcum3);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHOlcum4", item.PHOlcum4);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHOlcum5", item.PHOlcum5);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHOlcum6", item.PHOlcum6);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHOlcum7", item.PHOlcum7);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHOlcum8", item.PHOlcum8);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHOlcum9", item.PHOlcum9);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHOlcum10", item.PHOlcum10);

                        oChild_FSAPKY012_2_2.SetProperty("U_pHMiktar1", item.PHMiktar1);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHMiktar2", item.PHMiktar2);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHMiktar3", item.PHMiktar3);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHMiktar4", item.PHMiktar4);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHMiktar5", item.PHMiktar5);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHMiktar6", item.PHMiktar6);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHMiktar7", item.PHMiktar7);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHMiktar8", item.PHMiktar8);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHMiktar9", item.PHMiktar9);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHMiktar10", item.PHMiktar10);

                        oChild_FSAPKY012_2_2.SetProperty("U_pHSaat1", item.PHSaat1);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHSaat2", item.PHSaat2);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHSaat3", item.PHSaat3);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHSaat4", item.PHSaat4);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHSaat5", item.PHSaat5);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHSaat6", item.PHSaat6);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHSaat7", item.PHSaat7);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHSaat8", item.PHSaat8);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHSaat9", item.PHSaat9);
                        oChild_FSAPKY012_2_2.SetProperty("U_pHSaat10", item.PHSaat10);

                        oChild_FSAPKY012_2_2.SetProperty("U_OprtAdi", item.OperatorAdi);
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