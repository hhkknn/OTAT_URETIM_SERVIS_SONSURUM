using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddOrUpdateKaliteSonuc
    {
        public Response addOrUpdateKaliteSonuc(KaliteListesi kaliteListesi, string dbName, string mKodValue)
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

                connection = log.getSAPConnection(dbName, ID);

                if (connection.number == -1)
                {
                    LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
                    return new Response { Value = -3100, Description = "Hata Kodu - 3100 Veritabanı bağlantısı sırasında hata oluştu. ", List = null };
                }

                clnum = connection.number;
                dbCode = connection.dbCode;

                Company oCompany = connection.oCompany;

                Recordset oRS = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                string sql = "Select * from \"@AIF_KALITESONUC\" WITH (NOLOCK) where \"U_RaporTarihi\" = '" + kaliteListesi.RaporTarihi + "' and \"U_FormBelgeNo\" ='" + kaliteListesi.DetayFormBelgeNo + "' "; 
                oRS.DoQuery(sql);

                if (oRS.RecordCount == 0) //Daha önce bu tarihte kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChild_kaliteListesi;

                    GeneralDataCollection oChildren_kaliteListesi; 

                    oCompService = oCompany.GetCompanyService();

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_KALITESONUC");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralData.SetProperty("U_FormAckl", kaliteListesi.FormAciklamasi.ToString());

                    if (kaliteListesi.RaporTarihi != null && kaliteListesi.RaporTarihi != "")
                    {
                        string tarih = kaliteListesi.RaporTarihi;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_RaporTarihi", dt);
                    }

                    oGeneralData.SetProperty("U_FormBelgeNo", kaliteListesi.DetayFormBelgeNo.ToString());

                    oChildren_kaliteListesi = oGeneralData.Child("AIF_KALITESONUC1");

                    foreach (var item in kaliteListesi.kaliteListesiDetays)
                    {
                        oChild_kaliteListesi = oChildren_kaliteListesi.Add();

                        oChild_kaliteListesi.SetProperty("U_IstKodu", item.IstasyonKodu);

                        oChild_kaliteListesi.SetProperty("U_IstAdi", item.IstasyonAdi);

                        oChild_kaliteListesi.SetProperty("U_Aciklama", item.Aciklama);

                        oChild_kaliteListesi.SetProperty("U_Aciklama2", item.Aciklama2);

                        oChild_kaliteListesi.SetProperty("U_Tur", item.Tur);

                        oChild_kaliteListesi.SetProperty("U_UygnUygnDgl", item.UygunUygunDegil);

                        oChild_kaliteListesi.SetProperty("U_Deger1", item.Deger1);

                        oChild_kaliteListesi.SetProperty("U_Deger2", item.Deger2);

                        oChild_kaliteListesi.SetProperty("U_Deger3", item.Deger3);

                        oChild_kaliteListesi.SetProperty("U_Deger4", item.Deger4);
                        oChild_kaliteListesi.SetProperty("U_Deger5", item.Deger5);
                        oChild_kaliteListesi.SetProperty("U_Deger6", item.Deger6);
                        oChild_kaliteListesi.SetProperty("U_Deger7", item.Deger7);
                        oChild_kaliteListesi.SetProperty("U_Deger8", item.Deger8);
                        oChild_kaliteListesi.SetProperty("U_Deger9", item.Deger9);
                        oChild_kaliteListesi.SetProperty("U_Deger10", item.Deger10);
                        oChild_kaliteListesi.SetProperty("U_Deger11", item.Deger11);
                        oChild_kaliteListesi.SetProperty("U_Deger12", item.Deger12);
                        oChild_kaliteListesi.SetProperty("U_Deger13", item.Deger13);
                        oChild_kaliteListesi.SetProperty("U_Deger14", item.Deger14);
                        oChild_kaliteListesi.SetProperty("U_Deger15", item.Deger15);
                        oChild_kaliteListesi.SetProperty("U_Deger16", item.Deger16);
                        oChild_kaliteListesi.SetProperty("U_Deger17", item.Deger17);
                        oChild_kaliteListesi.SetProperty("U_Deger18", item.Deger18);
                        oChild_kaliteListesi.SetProperty("U_Deger19", item.Deger19);
                        oChild_kaliteListesi.SetProperty("U_Deger20", item.Deger20);

                        oChild_kaliteListesi.SetProperty("U_SaatAraligi", item.SaatAraligi);
                        oChild_kaliteListesi.SetProperty("U_KalPerAcik", item.KalitePersonelAciklama);
                    }

                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_KALITESONUC\" WITH (NOLOCK)");

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
                        return new Response { Value = 0, Description = "Kalite girişi oluşturuldu..", List = null };
                    }
                    else
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
                        return new Response { Value = -5200, Description = "Hata Kodu - 5200 Kalite girişi oluşturulurken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                    }
                }
                else
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChild_kaliteListesi;

                    GeneralDataCollection oChildren_kaliteListesi;

                    oCompService = oCompany.GetCompanyService();

                    GeneralDataParams oGeneralParams;

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_KALITESONUC");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    //oGeneralData.SetProperty("U_FormAckl", kaliteListesi.FormAciklamasi.ToString());

                    //if (kaliteListesi.RaporTarihi != null && kaliteListesi.RaporTarihi != "")
                    //{
                    //    string tarih = kaliteListesi.RaporTarihi;
                    //    DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                    //    oGeneralData.SetProperty("U_RaporTarihi", dt);
                    //}

                    oChildren_kaliteListesi = oGeneralData.Child("AIF_KALITESONUC1");
                     
                    if (oChildren_kaliteListesi.Count > 0)
                    {
                        int drc = oChildren_kaliteListesi.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_kaliteListesi.Remove(0);
                    }
                     
                    foreach (var item in kaliteListesi.kaliteListesiDetays)
                    {
                        oChild_kaliteListesi = oChildren_kaliteListesi.Add();

                        oChild_kaliteListesi.SetProperty("U_IstKodu", item.IstasyonKodu);

                        oChild_kaliteListesi.SetProperty("U_IstAdi", item.IstasyonAdi);

                        oChild_kaliteListesi.SetProperty("U_Aciklama", item.Aciklama);

                        oChild_kaliteListesi.SetProperty("U_Aciklama2", item.Aciklama2);

                        oChild_kaliteListesi.SetProperty("U_Tur", item.Tur);

                        oChild_kaliteListesi.SetProperty("U_UygnUygnDgl", item.UygunUygunDegil);

                        oChild_kaliteListesi.SetProperty("U_Deger1", item.Deger1);

                        oChild_kaliteListesi.SetProperty("U_Deger2", item.Deger2);

                        oChild_kaliteListesi.SetProperty("U_Deger3", item.Deger3);

                        oChild_kaliteListesi.SetProperty("U_Deger4", item.Deger4);
                        oChild_kaliteListesi.SetProperty("U_Deger5", item.Deger5);
                        oChild_kaliteListesi.SetProperty("U_Deger6", item.Deger6);
                        oChild_kaliteListesi.SetProperty("U_Deger7", item.Deger7);
                        oChild_kaliteListesi.SetProperty("U_Deger8", item.Deger8);
                        oChild_kaliteListesi.SetProperty("U_Deger9", item.Deger9);
                        oChild_kaliteListesi.SetProperty("U_Deger10", item.Deger10);
                        oChild_kaliteListesi.SetProperty("U_Deger11", item.Deger11);
                        oChild_kaliteListesi.SetProperty("U_Deger12", item.Deger12);
                        oChild_kaliteListesi.SetProperty("U_Deger13", item.Deger13);
                        oChild_kaliteListesi.SetProperty("U_Deger14", item.Deger14);
                        oChild_kaliteListesi.SetProperty("U_Deger15", item.Deger15);
                        oChild_kaliteListesi.SetProperty("U_Deger16", item.Deger16);
                        oChild_kaliteListesi.SetProperty("U_Deger17", item.Deger17);
                        oChild_kaliteListesi.SetProperty("U_Deger18", item.Deger18);
                        oChild_kaliteListesi.SetProperty("U_Deger19", item.Deger19);
                        oChild_kaliteListesi.SetProperty("U_Deger20", item.Deger20);

                        oChild_kaliteListesi.SetProperty("U_SaatAraligi", item.SaatAraligi);
                        oChild_kaliteListesi.SetProperty("U_KalPerAcik", item.KalitePersonelAciklama);

                    }

                    try
                    {
                        oGeneralService.Update(oGeneralData);
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
                        return new Response { Value = 0, Description = "Kalite girişi güncellendi.", List = null };
                    }
                    catch (Exception)
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
                        return new Response { Value = -5300, Description = "Hata Kodu - 5300 Kalite girişi güncellenirken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
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
    }
}