using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddOrUpdateAyranYardimciMalzeme
    {
        public Response addOrUpdateAyranYardimciMalzeme(AyranYardimciMalzemeGunlukOzet ayranYardimciMalzemeGunlukOzet, string dbName,string mKodValue)
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

                oRS.DoQuery("Select * from \"@AIF_AYRNOZTYRDMLZ\" WITH (NOLOCK) where \"U_UretimTarihi\" = '" + ayranYardimciMalzemeGunlukOzet.UretimTarihi + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChildYardimciMalzemeDetay;

                    GeneralDataCollection oChildrenYardimciMalzemeDetay;


                    oCompService = oCompany.GetCompanyService();

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_AYRNOZTYRDMLZ");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    DateTime dt = new DateTime(Convert.ToInt32(ayranYardimciMalzemeGunlukOzet.UretimTarihi.Substring(0, 4)), Convert.ToInt32(ayranYardimciMalzemeGunlukOzet.UretimTarihi.Substring(4, 2)), Convert.ToInt32(ayranYardimciMalzemeGunlukOzet.UretimTarihi.Substring(6, 2)));

                    oGeneralData.SetProperty("U_UretimTarihi", dt);

                    oGeneralData.SetProperty("U_UretimVardiyasi", ayranYardimciMalzemeGunlukOzet.UretimVardiyasi);


                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    //oGeneralData.SetProperty("U_Tarih", dt);

                    oChildrenYardimciMalzemeDetay = oGeneralData.Child("AIF_AYRNOZTYRDMLZ1");

                    foreach (var item in ayranYardimciMalzemeGunlukOzet.ayranYardimciMalzemeGunlukOzet1s)
                    {
                        oChildYardimciMalzemeDetay = oChildrenYardimciMalzemeDetay.Add();

                        oChildYardimciMalzemeDetay.SetProperty("U_UrunKodu", item.UrunKodu);

                        oChildYardimciMalzemeDetay.SetProperty("U_UrunAdi", item.UrunAdi);

                        oChildYardimciMalzemeDetay.SetProperty("U_BardakTedAdi", item.BardakTedarikciAdi);

                        oChildYardimciMalzemeDetay.SetProperty("U_BardakPartiNo", item.BardakPartiNo);

                        oChildYardimciMalzemeDetay.SetProperty("U_FolyoTedAdi", item.FolyoTedarikciAdi);

                        oChildYardimciMalzemeDetay.SetProperty("U_FolyoPartiNo", item.FolyoPartiNo);

                        oChildYardimciMalzemeDetay.SetProperty("U_ViyolTedAdi", item.ViyolTedarikciAdi);

                        oChildYardimciMalzemeDetay.SetProperty("U_ViyolPartiNo", item.ViyolPartiNo);
                    }

                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_AYRNOZTYRDMLZ\" WITH (NOLOCK)");

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
                        return new Response { Value = 0, Description = "Yardımcı Malzeme Günlük Özet Girişi Oluşturuldu..", List = null };
                    }
                    else
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5200, Description = "Hata Kodu - 5200 Yardımcı Malzeme Günlük Özet Girişi Oluşturulurken Hata Oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                    }
                }
                else
                {


                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChildYardimciMalzemeDetay;

                    GeneralDataCollection oChildrenYardimciMalzemeDetay;


                    oCompService = oCompany.GetCompanyService();

                    GeneralDataParams oGeneralParams;

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_AYRNOZTYRDMLZ");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    DateTime dt = new DateTime(Convert.ToInt32(ayranYardimciMalzemeGunlukOzet.UretimTarihi.Substring(0, 4)), Convert.ToInt32(ayranYardimciMalzemeGunlukOzet.UretimTarihi.Substring(4, 2)), Convert.ToInt32(ayranYardimciMalzemeGunlukOzet.UretimTarihi.Substring(6, 2)));

                    oGeneralData.SetProperty("U_UretimTarihi", dt);

                    oGeneralData.SetProperty("U_UretimVardiyasi", ayranYardimciMalzemeGunlukOzet.UretimVardiyasi);


                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    //oGeneralData.SetProperty("U_Tarih", dt);

                    oChildrenYardimciMalzemeDetay = oGeneralData.Child("AIF_AYRNOZTYRDMLZ1");

                    if (oChildrenYardimciMalzemeDetay.Count > 0)
                    {
                        int drc = oChildrenYardimciMalzemeDetay.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenYardimciMalzemeDetay.Remove(0);
                    }

                    foreach (var item in ayranYardimciMalzemeGunlukOzet.ayranYardimciMalzemeGunlukOzet1s)
                    {
                        oChildYardimciMalzemeDetay = oChildrenYardimciMalzemeDetay.Add();

                        oChildYardimciMalzemeDetay.SetProperty("U_UrunKodu", item.UrunKodu);

                        oChildYardimciMalzemeDetay.SetProperty("U_UrunAdi", item.UrunAdi);

                        oChildYardimciMalzemeDetay.SetProperty("U_BardakTedAdi", item.BardakTedarikciAdi);

                        oChildYardimciMalzemeDetay.SetProperty("U_BardakPartiNo", item.BardakPartiNo);

                        oChildYardimciMalzemeDetay.SetProperty("U_FolyoTedAdi", item.FolyoTedarikciAdi);

                        oChildYardimciMalzemeDetay.SetProperty("U_FolyoPartiNo", item.FolyoPartiNo);

                        oChildYardimciMalzemeDetay.SetProperty("U_ViyolTedAdi", item.ViyolTedarikciAdi);

                        oChildYardimciMalzemeDetay.SetProperty("U_ViyolPartiNo", item.ViyolPartiNo);
                    }

                    try
                    {
                        oGeneralService.Update(oGeneralData);
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = 0, Description = "Yardımcı Malzeme Günlük Özet Girişi Güncellendi.", List = null };
                    }
                    catch (Exception)
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5300, Description = "Hata Kodu - 5300 Yardımcı Malzeme Günlük Özet Girişi Güncellenirken Hata Oluştu. " + oCompany.GetLastErrorDescription(), List = null };
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