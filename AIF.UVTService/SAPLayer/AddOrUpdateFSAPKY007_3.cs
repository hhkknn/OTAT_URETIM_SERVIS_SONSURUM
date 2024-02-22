using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddOrUpdateFSAPKY007_3
    {
        public Response addOrUpdateFSAPKY007_3(FSAPKY007_3 fSAPKY007_3, string dbName, string mKodValue)
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

                oRS.DoQuery("Select * from \"@AIF_FSAPKY007_3\" WITH (NOLOCK) where \"U_PartiNo\" = '" + fSAPKY007_3.PartiNo + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChild_FSAPKY007_3_1;

                    GeneralDataCollection oChildren_FSAPKY007_3_1;

                    GeneralData oChild_FSAPKY007_3_2;

                    GeneralDataCollection oChildren_FSAPKY007_3_2;

                    oCompService = oCompany.GetCompanyService();

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_FSAPKY007_3");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralData.SetProperty("U_PartiNo", fSAPKY007_3.PartiNo.ToString());

                    oGeneralData.SetProperty("U_UrunKodu", fSAPKY007_3.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_UrunTanimi", fSAPKY007_3.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Kontrol1", fSAPKY007_3.Kontrol1.ToString());

                    oGeneralData.SetProperty("U_Kontrol2", fSAPKY007_3.Kontrol2.ToString());

                    //oGeneralData.SetProperty("U_Aciklama", fSAPKY007_3.Aciklama.ToString());

                    if (fSAPKY007_3.Tarih != null && fSAPKY007_3.Tarih != "")
                    {
                        string tarih = fSAPKY007_3.Tarih;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_Tarih", dt);
                    }

                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    //oGeneralData.SetProperty("U_Tarih", dt);

                    oChildren_FSAPKY007_3_1 = oGeneralData.Child("AIF_FSAPKY007_3_1");

                    foreach (var item in fSAPKY007_3.fSAPKY007_3_1s)
                    {


                        oChild_FSAPKY007_3_1 = oChildren_FSAPKY007_3_1.Add();

                        oChild_FSAPKY007_3_1.SetProperty("U_UrunKodu", item.UrunKodu);

                        oChild_FSAPKY007_3_1.SetProperty("U_UrunAdi", item.UrunAdi);

                        oChild_FSAPKY007_3_1.SetProperty("U_PaketSicak", item.PaketlemeSicakligi);

                        if (item.UretimTarihi.Year != 1900)
                        {
                            oChild_FSAPKY007_3_1.SetProperty("U_UretimTarih", item.UretimTarihi);
                        }

                        if (item.SonTuketimTarihi.Year != 1900)
                        {
                            oChild_FSAPKY007_3_1.SetProperty("U_SonTukTarih", item.SonTuketimTarihi);
                        }

                        oChild_FSAPKY007_3_1.SetProperty("U_PartiNo", item.PartiNo);

                        oChild_FSAPKY007_3_1.SetProperty("U_OprtAdi", item.OperatorAdi);
                    }

                    oChildren_FSAPKY007_3_2 = oGeneralData.Child("AIF_FSAPKY007_3_2");

                    foreach (var item in fSAPKY007_3.fSAPKY007_3_2s)
                    {

                        oChild_FSAPKY007_3_2 = oChildren_FSAPKY007_3_2.Add();

                        oChild_FSAPKY007_3_2.SetProperty("U_UrunKodu", item.UrunKodu);

                        oChild_FSAPKY007_3_2.SetProperty("U_UrunAdi", item.UrunAdi);

                        oChild_FSAPKY007_3_2.SetProperty("U_Ornek1", item.Ornek1);

                        oChild_FSAPKY007_3_2.SetProperty("U_Ornek2", item.Ornek2);

                        oChild_FSAPKY007_3_2.SetProperty("U_Ornek3", item.Ornek3);

                        oChild_FSAPKY007_3_2.SetProperty("U_Ornek4", item.Ornek4);

                        oChild_FSAPKY007_3_2.SetProperty("U_Ornek5", item.Ornek5);

                        oChild_FSAPKY007_3_2.SetProperty("U_Ornek6", item.Ornek6);

                        oChild_FSAPKY007_3_2.SetProperty("U_Ornek7", item.Ornek7);

                        oChild_FSAPKY007_3_2.SetProperty("U_Ornek8", item.Ornek8);

                        oChild_FSAPKY007_3_2.SetProperty("U_Ornek9", item.Ornek9);

                        oChild_FSAPKY007_3_2.SetProperty("U_Ornek10", item.Ornek10); 

                        oChild_FSAPKY007_3_2.SetProperty("U_OprtAdi", item.OperatorAdi);
                    }

                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_FSAPKY007_3\" WITH (NOLOCK)");

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

                    GeneralData oChild_FSAPKY007_3_1;

                    GeneralDataCollection oChildren_FSAPKY007_3_1;

                    GeneralData oChild_FSAPKY007_3_2;

                    GeneralDataCollection oChildren_FSAPKY007_3_2;


                    oCompService = oCompany.GetCompanyService();

                    GeneralDataParams oGeneralParams;

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_FSAPKY007_3");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);


                    oGeneralData.SetProperty("U_PartiNo", fSAPKY007_3.PartiNo.ToString());

                    oGeneralData.SetProperty("U_UrunKodu", fSAPKY007_3.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_UrunTanimi", fSAPKY007_3.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Kontrol1", fSAPKY007_3.Kontrol1.ToString());

                    oGeneralData.SetProperty("U_Kontrol2", fSAPKY007_3.Kontrol2.ToString());

                    //oGeneralData.SetProperty("U_Aciklama", fSAPKY007_3.Aciklama.ToString());

                    if (fSAPKY007_3.Tarih != null && fSAPKY007_3.Tarih != "")
                    {
                        string tarih = fSAPKY007_3.Tarih;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_Tarih", dt);
                    }


                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    //oGeneralData.SetProperty("U_Tarih", dt);

                    oChildren_FSAPKY007_3_1 = oGeneralData.Child("AIF_FSAPKY007_3_1");

                    if (oChildren_FSAPKY007_3_1.Count > 0)
                    {
                        int drc = oChildren_FSAPKY007_3_1.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY007_3_1.Remove(0);
                    }

                    foreach (var item in fSAPKY007_3.fSAPKY007_3_1s)
                    { 
                        oChild_FSAPKY007_3_1 = oChildren_FSAPKY007_3_1.Add();

                        oChild_FSAPKY007_3_1.SetProperty("U_UrunKodu", item.UrunKodu);

                        oChild_FSAPKY007_3_1.SetProperty("U_UrunAdi", item.UrunAdi);

                        oChild_FSAPKY007_3_1.SetProperty("U_PaketSicak", item.PaketlemeSicakligi);

                        if (item.UretimTarihi.Year != 1900)
                        {
                            oChild_FSAPKY007_3_1.SetProperty("U_UretimTarih", item.UretimTarihi);
                        }

                        if (item.SonTuketimTarihi.Year != 1900)
                        {
                            oChild_FSAPKY007_3_1.SetProperty("U_SonTukTarih", item.SonTuketimTarihi);
                        }

                        oChild_FSAPKY007_3_1.SetProperty("U_PartiNo", item.PartiNo);

                        oChild_FSAPKY007_3_1.SetProperty("U_OprtAdi", item.OperatorAdi);
                    }


                    oChildren_FSAPKY007_3_2 = oGeneralData.Child("AIF_FSAPKY007_3_2");

                    if (oChildren_FSAPKY007_3_2.Count > 0)
                    {
                        int drc = oChildren_FSAPKY007_3_2.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY007_3_2.Remove(0);
                    }

                    foreach (var item in fSAPKY007_3.fSAPKY007_3_2s)
                    {

                        oChild_FSAPKY007_3_2 = oChildren_FSAPKY007_3_2.Add();

                        oChild_FSAPKY007_3_2.SetProperty("U_UrunAdi", item.UrunAdi);

                        oChild_FSAPKY007_3_2.SetProperty("U_Ornek1", item.Ornek1);

                        oChild_FSAPKY007_3_2.SetProperty("U_Ornek2", item.Ornek2);

                        oChild_FSAPKY007_3_2.SetProperty("U_Ornek3", item.Ornek3);

                        oChild_FSAPKY007_3_2.SetProperty("U_Ornek4", item.Ornek4);

                        oChild_FSAPKY007_3_2.SetProperty("U_Ornek5", item.Ornek5);

                        oChild_FSAPKY007_3_2.SetProperty("U_Ornek6", item.Ornek6);

                        oChild_FSAPKY007_3_2.SetProperty("U_Ornek7", item.Ornek7);

                        oChild_FSAPKY007_3_2.SetProperty("U_Ornek8", item.Ornek8);

                        oChild_FSAPKY007_3_2.SetProperty("U_Ornek9", item.Ornek9);

                        oChild_FSAPKY007_3_2.SetProperty("U_Ornek10", item.Ornek10);

                        oChild_FSAPKY007_3_2.SetProperty("U_OprtAdi", item.OperatorAdi);
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