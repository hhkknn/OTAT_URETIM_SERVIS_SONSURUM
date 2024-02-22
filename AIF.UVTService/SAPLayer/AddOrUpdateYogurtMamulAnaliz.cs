using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddOrUpdateYogurtMamulAnaliz
    {
        public Response addOrUpdateYogurtMamulAnaliz(YogurtMamulAnaliz yogurtMamulAnaliz, string dbName, string mKodValue)
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

                oRS.DoQuery("Select * from \"@AIF_YGRMML_ANLZ\" WITH (NOLOCK) where \"U_PartiNo\" = '" + yogurtMamulAnaliz.PartiNo + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChildInkubasyon;

                    GeneralDataCollection oChildrenInkubasyon;

                    GeneralData oChildGramajKontrol;

                    GeneralDataCollection oChildrenGramajKontrol; 

                    oCompService = oCompany.GetCompanyService();

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_YGRMML_ANLZ");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralData.SetProperty("U_PartiNo", yogurtMamulAnaliz.PartiNo.ToString());

                    oGeneralData.SetProperty("U_KalemKodu", yogurtMamulAnaliz.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_KalemTanimi", yogurtMamulAnaliz.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Aciklama", yogurtMamulAnaliz.Aciklama.ToString());

                    if (yogurtMamulAnaliz.UretimTarihi != null && yogurtMamulAnaliz.UretimTarihi != "")
                    {
                        string tarih = yogurtMamulAnaliz.UretimTarihi;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_UretimTarihi", dt);

                    }

                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    //oGeneralData.SetProperty("U_Tarih", dt);

                    oChildrenInkubasyon = oGeneralData.Child("AIF_YGRMML_ANLZ1");

                    foreach (var item in yogurtMamulAnaliz.YogurtMamulInkubasyons)
                    {
                        oChildInkubasyon = oChildrenInkubasyon.Add();

                        oChildInkubasyon.SetProperty("U_KontrolNo", item.KontrolNo);

                        oChildInkubasyon.SetProperty("U_Saat", item.Saat);

                        oChildInkubasyon.SetProperty("U_UrunSicaklik", item.UrunSicakligi);

                        oChildInkubasyon.SetProperty("U_PH", item.PH);

                        oChildInkubasyon.SetProperty("U_OdaSicaklik", item.OdaSicakligi);

                        oChildInkubasyon.SetProperty("U_KontrolEdenPers", item.KontrolEdenPersonel);
                    }

                    oChildrenGramajKontrol = oGeneralData.Child("AIF_YGRMML_ANLZ2");

                    foreach (var item in yogurtMamulAnaliz.YogurtMamulGramajKontrols)
                    {
                        oChildGramajKontrol = oChildrenGramajKontrol.Add();

                        oChildGramajKontrol.SetProperty("U_Ornek1", item.Ornek1); 
                        oChildGramajKontrol.SetProperty("U_Ornek2", item.Ornek2); 
                        oChildGramajKontrol.SetProperty("U_Ornek3", item.Ornek3); 
                        oChildGramajKontrol.SetProperty("U_Ornek4", item.Ornek4); 
                        oChildGramajKontrol.SetProperty("U_Ornek5", item.Ornek5); 
                        oChildGramajKontrol.SetProperty("U_Ornek6", item.Ornek6); 
                        oChildGramajKontrol.SetProperty("U_Ornek7", item.Ornek7); 
                        oChildGramajKontrol.SetProperty("U_Ornek8", item.Ornek8); 
                        oChildGramajKontrol.SetProperty("U_Ornek9", item.Ornek9); 
                        oChildGramajKontrol.SetProperty("U_Ornek10", item.Ornek10); 
                        oChildGramajKontrol.SetProperty("U_Ornek11", item.Ornek11); 
                        oChildGramajKontrol.SetProperty("U_Ornek12", item.Ornek12); 
                        oChildGramajKontrol.SetProperty("U_Ornek13", item.Ornek13); 
                        oChildGramajKontrol.SetProperty("U_Ornek14", item.Ornek14);
                        oChildGramajKontrol.SetProperty("U_Ornek15", item.Ornek15);
                    }

                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_YGRMML_ANLZ\" WITH (NOLOCK)");

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
                        return new Response { Value = 0, Description = "Yoğurt Mamül Analiz girişi oluşturuldu.", List = null };
                    }
                    else
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5200, Description = "Hata Kodu - 5200 Yoğurt Mamül Analiz girişi oluşturulurken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                    }
                }
                else
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChildInkubasyon;

                    GeneralDataCollection oChildrenInkubasyon;

                    GeneralData oChildGramajKontrol;

                    GeneralDataCollection oChildrenGramajKontrol;

                    oCompService = oCompany.GetCompanyService();

                    GeneralDataParams oGeneralParams;

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_YGRMML_ANLZ");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    oGeneralData.SetProperty("U_PartiNo", yogurtMamulAnaliz.PartiNo.ToString());

                    oGeneralData.SetProperty("U_KalemKodu", yogurtMamulAnaliz.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_KalemTanimi", yogurtMamulAnaliz.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Aciklama", yogurtMamulAnaliz.Aciklama.ToString());

                    if (yogurtMamulAnaliz.UretimTarihi != null && yogurtMamulAnaliz.UretimTarihi != "")
                    {
                        string tarih = yogurtMamulAnaliz.UretimTarihi;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_UretimTarihi", dt);

                    }
                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    //oGeneralData.SetProperty("U_Tarih", dt);

                    oChildrenInkubasyon = oGeneralData.Child("AIF_YGRMML_ANLZ1");

                    if (oChildrenInkubasyon.Count > 0)
                    {
                        int drc = oChildrenInkubasyon.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenInkubasyon.Remove(0);
                    } 

                    foreach (var item in yogurtMamulAnaliz.YogurtMamulInkubasyons)
                    {
                        oChildInkubasyon = oChildrenInkubasyon.Add();

                        oChildInkubasyon.SetProperty("U_KontrolNo", item.KontrolNo);

                        oChildInkubasyon.SetProperty("U_Saat", item.Saat);

                        oChildInkubasyon.SetProperty("U_UrunSicaklik", item.UrunSicakligi);

                        oChildInkubasyon.SetProperty("U_PH", item.PH);

                        oChildInkubasyon.SetProperty("U_OdaSicaklik", item.OdaSicakligi);

                        oChildInkubasyon.SetProperty("U_KontrolEdenPers", item.KontrolEdenPersonel);
                    }

                    oChildrenGramajKontrol = oGeneralData.Child("AIF_YGRMML_ANLZ2");

                    if (oChildrenGramajKontrol.Count > 0)
                    {
                        int drc = oChildrenGramajKontrol.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenGramajKontrol.Remove(0);
                    }

                    foreach (var item in yogurtMamulAnaliz.YogurtMamulGramajKontrols)
                    {
                        oChildGramajKontrol = oChildrenGramajKontrol.Add();

                        oChildGramajKontrol.SetProperty("U_Ornek1", item.Ornek1);
                        oChildGramajKontrol.SetProperty("U_Ornek2", item.Ornek2);
                        oChildGramajKontrol.SetProperty("U_Ornek3", item.Ornek3);
                        oChildGramajKontrol.SetProperty("U_Ornek4", item.Ornek4);
                        oChildGramajKontrol.SetProperty("U_Ornek5", item.Ornek5);
                        oChildGramajKontrol.SetProperty("U_Ornek6", item.Ornek6);
                        oChildGramajKontrol.SetProperty("U_Ornek7", item.Ornek7);
                        oChildGramajKontrol.SetProperty("U_Ornek8", item.Ornek8);
                        oChildGramajKontrol.SetProperty("U_Ornek9", item.Ornek9);
                        oChildGramajKontrol.SetProperty("U_Ornek10", item.Ornek10);
                        oChildGramajKontrol.SetProperty("U_Ornek11", item.Ornek11);
                        oChildGramajKontrol.SetProperty("U_Ornek12", item.Ornek12);
                        oChildGramajKontrol.SetProperty("U_Ornek13", item.Ornek13);
                        oChildGramajKontrol.SetProperty("U_Ornek14", item.Ornek14);
                        oChildGramajKontrol.SetProperty("U_Ornek15", item.Ornek15);
                    } 

                    try
                    {
                        oGeneralService.Update(oGeneralData);
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = 0, Description = "Yoğurt Mamül Analiz girişi güncellendi.", List = null };
                    }
                    catch (Exception)
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5300, Description = "Hata Kodu - 5300 Yoğurt Mamül Analiz girişi güncellenirken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
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