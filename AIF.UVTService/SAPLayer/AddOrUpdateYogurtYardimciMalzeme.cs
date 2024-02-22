using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddOrUpdateYogurtYardimciMalzeme
    {
        public Response addOrUpdateYogurtYardimciMalzeme(YogurtYardimciMalzeme yogurtYardimciMalzeme, string dbName, string mKodValue)
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

                oRS.DoQuery("Select * from \"@AIF_YGRTYRDMLZ\" WITH (NOLOCK) where \"U_UretimTarihi\" = '" + yogurtYardimciMalzeme.UretimTarihi + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChildYardimciMalzemeDetay;

                    GeneralDataCollection oChildrenYardimciMalzemeDetay;

                    GeneralData oChildGorevliPersonel;

                    GeneralDataCollection oChildrenGorevliPersonel;

                    GeneralData oChildGramajKontrol;

                    GeneralDataCollection oChildrenGramajKontrol;


                    oCompService = oCompany.GetCompanyService();

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_YGRTYRDMLZ");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    DateTime dt = new DateTime(Convert.ToInt32(yogurtYardimciMalzeme.UretimTarihi.Substring(0, 4)), Convert.ToInt32(yogurtYardimciMalzeme.UretimTarihi.Substring(4, 2)), Convert.ToInt32(yogurtYardimciMalzeme.UretimTarihi.Substring(6, 2)));

                    oGeneralData.SetProperty("U_UretimTarihi", dt);

                    oGeneralData.SetProperty("U_Aciklama", yogurtYardimciMalzeme.Aciklama);

                    oChildrenYardimciMalzemeDetay = oGeneralData.Child("AIF_YGRTYRDMLZ1");

                    foreach (var item in yogurtYardimciMalzeme.yogurtYardimciMalzemeKontrolus)
                    {
                        oChildYardimciMalzemeDetay = oChildrenYardimciMalzemeDetay.Add();

                        oChildYardimciMalzemeDetay.SetProperty("U_UrunKodu", item.UrunKodu);

                        oChildYardimciMalzemeDetay.SetProperty("U_UrunAdi", item.UrunAdi);

                        oChildYardimciMalzemeDetay.SetProperty("U_KovaKaseTedAdi", item.KovaKaseTedarikciAdi);

                        oChildYardimciMalzemeDetay.SetProperty("U_KovaKasePartiNo", item.KovaKasePartiNo);

                        oChildYardimciMalzemeDetay.SetProperty("U_KapakFolyoTedAdi", item.KovaKasePartiNo);

                        oChildYardimciMalzemeDetay.SetProperty("U_KapakFolyoPartiNo", item.KapakFolyoPartiNo);

                        oChildYardimciMalzemeDetay.SetProperty("U_ViyolTedAdi", item.ViyolTedarikciAdi);

                        oChildYardimciMalzemeDetay.SetProperty("U_ViyolPartiNo", item.ViyolPartiNo);
                    }

                    oChildrenGorevliPersonel = oGeneralData.Child("AIF_YGRTYRDMLZ2");

                    foreach (var item in yogurtYardimciMalzeme.yogurtYardimciMalzemeGorevliPersonels)
                    {
                        oChildGorevliPersonel = oChildrenGorevliPersonel.Add();

                        oChildGorevliPersonel.SetProperty("U_Aciklama", item.Aciklama);

                        oChildGorevliPersonel.SetProperty("U_Deger", item.Deger);
                    }



                    oChildrenGramajKontrol = oGeneralData.Child("AIF_YGRTYRDMLZ3");

                    foreach (var item in yogurtYardimciMalzeme.yogurtYardimciMalzemeGramajKontrols)
                    {
                        oChildGramajKontrol = oChildrenGramajKontrol.Add();

                        oChildGramajKontrol.SetProperty("U_Aciklama", item.Aciklama);

                        oChildGramajKontrol.SetProperty("U_Deger", item.Deger);
                    }

                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_YGRTYRDMLZ\" WITH (NOLOCK)");

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
                        return new Response { Value = 0, Description = "Yoğurt Yardımcı Malzeme Girişi Oluşturuldu..", List = null };
                    }
                    else
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5200, Description = "Hata Kodu - 5200 Yoğurt Yardımcı Malzeme Girişi Oluşturulurken Hata Oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                    }
                }
                else
                {


                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChildYardimciMalzemeDetay;

                    GeneralDataCollection oChildrenYardimciMalzemeDetay;

                    GeneralData oChildGorevliPersonel;

                    GeneralDataCollection oChildrenGorevliPersonel;

                    GeneralData oChildGramajKontrol;

                    GeneralDataCollection oChildrenGramajKontrol;


                    oCompService = oCompany.GetCompanyService();

                    GeneralDataParams oGeneralParams;

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_YGRTYRDMLZ");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    DateTime dt = new DateTime(Convert.ToInt32(yogurtYardimciMalzeme.UretimTarihi.Substring(0, 4)), Convert.ToInt32(yogurtYardimciMalzeme.UretimTarihi.Substring(4, 2)), Convert.ToInt32(yogurtYardimciMalzeme.UretimTarihi.Substring(6, 2)));

                    oGeneralData.SetProperty("U_UretimTarihi", dt);

                    oGeneralData.SetProperty("U_Aciklama", yogurtYardimciMalzeme.Aciklama);

                    oChildrenYardimciMalzemeDetay = oGeneralData.Child("AIF_YGRTYRDMLZ1");

                    if (oChildrenYardimciMalzemeDetay.Count > 0)
                    {
                        int drc = oChildrenYardimciMalzemeDetay.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenYardimciMalzemeDetay.Remove(0);
                    }

                    foreach (var item in yogurtYardimciMalzeme.yogurtYardimciMalzemeKontrolus)
                    {
                        oChildYardimciMalzemeDetay = oChildrenYardimciMalzemeDetay.Add();

                        oChildYardimciMalzemeDetay.SetProperty("U_UrunKodu", item.UrunKodu);

                        oChildYardimciMalzemeDetay.SetProperty("U_UrunAdi", item.UrunAdi);

                        oChildYardimciMalzemeDetay.SetProperty("U_KovaKaseTedAdi", item.KovaKaseTedarikciAdi);

                        oChildYardimciMalzemeDetay.SetProperty("U_KovaKasePartiNo", item.KovaKasePartiNo);

                        oChildYardimciMalzemeDetay.SetProperty("U_KapakFolyoTedAdi", item.KovaKasePartiNo);

                        oChildYardimciMalzemeDetay.SetProperty("U_KapakFolyoPartiNo", item.KapakFolyoPartiNo);

                        oChildYardimciMalzemeDetay.SetProperty("U_ViyolTedAdi", item.ViyolTedarikciAdi);

                        oChildYardimciMalzemeDetay.SetProperty("U_ViyolPartiNo", item.ViyolPartiNo);
                    }


                    oChildrenGorevliPersonel = oGeneralData.Child("AIF_YGRTYRDMLZ2");


                    if (oChildrenGorevliPersonel.Count > 0)
                    {
                        int drc = oChildrenGorevliPersonel.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenGorevliPersonel.Remove(0);
                    }

                    foreach (var item in yogurtYardimciMalzeme.yogurtYardimciMalzemeGorevliPersonels)
                    {
                        oChildGorevliPersonel = oChildrenGorevliPersonel.Add();

                        oChildGorevliPersonel.SetProperty("U_Aciklama", item.Aciklama);

                        oChildGorevliPersonel.SetProperty("U_Deger", item.Deger);
                    }


                    oChildrenGramajKontrol = oGeneralData.Child("AIF_YGRTYRDMLZ3");


                    if (oChildrenGramajKontrol.Count > 0)
                    {
                        int drc = oChildrenGramajKontrol.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenGramajKontrol.Remove(0);
                    }

                    foreach (var item in yogurtYardimciMalzeme.yogurtYardimciMalzemeGramajKontrols)
                    {
                        oChildGramajKontrol = oChildrenGramajKontrol.Add();

                        oChildGramajKontrol.SetProperty("U_Aciklama", item.Aciklama);

                        oChildGramajKontrol.SetProperty("U_Deger", item.Deger);
                    }

                    try
                    {
                        oGeneralService.Update(oGeneralData);
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = 0, Description = "Yoğurt Yardımcı Malzeme Girişi Güncellendi.", List = null };
                    }
                    catch (Exception)
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5300, Description = "Hata Kodu - 5300 Yoğurt Yardımcı Malzeme Girişi Güncellenirken Hata Oluştu. " + oCompany.GetLastErrorDescription(), List = null };
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