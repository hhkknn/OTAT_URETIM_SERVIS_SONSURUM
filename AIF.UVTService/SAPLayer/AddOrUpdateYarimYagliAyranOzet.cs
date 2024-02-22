using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddOrUpdateYarimYagliAyranOzet
    {
        public Response addOrUpdateYarimYagliAyranOzet(YarimYagliAyranGunlukOzet yarimYagliAyranGunlukOzet, string dbName, string mKodValue)
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

                oRS.DoQuery("Select * from \"@AIF_AYRNOZTYRMYGL\" WITH (NOLOCK) where \"U_UretimTarihi\" = '" + yarimYagliAyranGunlukOzet.UretimTarihi + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChildPaketlemeBilgieri1;

                    GeneralDataCollection oChildrenPaketlemeBilgieri1;

                    GeneralData oChildPaketlemeBilgileri2;

                    GeneralDataCollection oChildrenPaketlemeBilgierli2;

                    GeneralData oChildGunSonuOzeti;

                    GeneralDataCollection oChildrenGunSonuOzeti;




                    oCompService = oCompany.GetCompanyService();

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_AYRNOZTYRMYGL");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    DateTime dt = new DateTime(Convert.ToInt32(yarimYagliAyranGunlukOzet.UretimTarihi.Substring(0, 4)), Convert.ToInt32(yarimYagliAyranGunlukOzet.UretimTarihi.Substring(4, 2)), Convert.ToInt32(yarimYagliAyranGunlukOzet.UretimTarihi.Substring(6, 2)));

                    oGeneralData.SetProperty("U_UretimTarihi", dt);

                    oGeneralData.SetProperty("U_UretimVardiyasi", yarimYagliAyranGunlukOzet.UretimVardiyasi);

                    oGeneralData.SetProperty("U_Aciklama", yarimYagliAyranGunlukOzet.Aciklama);


                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    //oGeneralData.SetProperty("U_Tarih", dt);

                    oChildrenPaketlemeBilgieri1 = oGeneralData.Child("AIF_AYRNOZTYRMYGL1");

                    foreach (var item in yarimYagliAyranGunlukOzet.yarimYagliPaketlemeBilgileri1s)
                    {
                        oChildPaketlemeBilgieri1 = oChildrenPaketlemeBilgieri1.Add();

                        oChildPaketlemeBilgieri1.SetProperty("U_UrunKodu", item.UrunKodu);

                        oChildPaketlemeBilgieri1.SetProperty("U_UrunAdi", item.UrunAdi);

                        oChildPaketlemeBilgieri1.SetProperty("U_PaketAyranMikAd", item.PaketlenenAyranMiktariAdet);

                        oChildPaketlemeBilgieri1.SetProperty("U_PaketAyranMikLt", item.PaketlenenAyranMiktariLitre);
                    }

                    oChildrenPaketlemeBilgierli2 = oGeneralData.Child("AIF_AYRNOZTYRMYGL2");

                    foreach (var item in yarimYagliAyranGunlukOzet.yarimYagliPaketlemeBilgileri2s)
                    {
                        oChildPaketlemeBilgileri2 = oChildrenPaketlemeBilgierli2.Add();

                        oChildPaketlemeBilgileri2.SetProperty("U_ToplamSutMik", item.ToplamSutMiktari);

                        oChildPaketlemeBilgileri2.SetProperty("U_ToplamSuMik", item.ToplamSuMiktari);

                        oChildPaketlemeBilgileri2.SetProperty("U_TuzMiktari", item.TuzMiktari);

                        oChildPaketlemeBilgileri2.SetProperty("U_AyranTuzOrani", item.AyranTuzOrani);

                        oChildPaketlemeBilgileri2.SetProperty("U_ToplamAyranMik", item.ToplamAyranMiktari);
                    }

                    oChildrenGunSonuOzeti = oGeneralData.Child("AIF_AYRNOZTYRMYGL3");

                    foreach (var item in yarimYagliAyranGunlukOzet.yarimYagliGunlukOzets)
                    {
                        oChildGunSonuOzeti = oChildrenGunSonuOzeti.Add();

                        oChildGunSonuOzeti.SetProperty("U_MayalananAyranMik", item.MayalananAyranMiktari);

                        oChildGunSonuOzeti.SetProperty("U_OncekiGundenDevMik", item.OncekiGundenDevredenAyranMiktari);

                        oChildGunSonuOzeti.SetProperty("U_TanktakiToplamMik", item.TanktakiToplamAyranMiktari);

                        oChildGunSonuOzeti.SetProperty("U_PaketTankFarki", item.PaketlenenVeTanktakiAyranFarki);

                        oChildGunSonuOzeti.SetProperty("U_SonrakiGuneDevMik", item.SonrakiGuneDevredenAyranMiktari);

                        oChildGunSonuOzeti.SetProperty("U_GunSonuAyranFarki", item.GunSonuAyranFarki);

                        oChildGunSonuOzeti.SetProperty("U_StandartRandiman", item.StandartRandimanDegeri);

                        oChildGunSonuOzeti.SetProperty("U_GerceklesenRandiman", item.GerceklesenRandimanDegeri);
                    }


                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_AYRNOZTYRMYGL\" WITH (NOLOCK)");

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
                        return new Response { Value = 0, Description = "Yarım Yağlı Ayran Günlük Özet Girişi Oluşturuldu..", List = null };
                    }
                    else
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5200, Description = "Hata Kodu - 5200 Tam Yarım Ayran Günlük Özet Girişi Oluşturulurken Hata Oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                    }
                }
                else
                {


                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChildPaketlemeBilgieri1;

                    GeneralDataCollection oChildrenPaketlemeBilgieri1;

                    GeneralData oChildPaketlemeBilgileri2;

                    GeneralDataCollection oChildrenPaketlemeBilgierli2;

                    GeneralData oChildGunSonuOzeti;

                    GeneralDataCollection oChildrenGunSonuOzeti;

                    oCompService = oCompany.GetCompanyService();

                    GeneralDataParams oGeneralParams;

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_AYRNOZTYRMYGL");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    DateTime dt = new DateTime(Convert.ToInt32(yarimYagliAyranGunlukOzet.UretimTarihi.Substring(0, 4)), Convert.ToInt32(yarimYagliAyranGunlukOzet.UretimTarihi.Substring(4, 2)), Convert.ToInt32(yarimYagliAyranGunlukOzet.UretimTarihi.Substring(6, 2)));

                    oGeneralData.SetProperty("U_UretimTarihi", dt);

                    oGeneralData.SetProperty("U_UretimVardiyasi", yarimYagliAyranGunlukOzet.UretimVardiyasi);

                    oGeneralData.SetProperty("U_Aciklama", yarimYagliAyranGunlukOzet.Aciklama);


                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    //oGeneralData.SetProperty("U_Tarih", dt);

                    oChildrenPaketlemeBilgieri1 = oGeneralData.Child("AIF_AYRNOZTYRMYGL1");

                    if (oChildrenPaketlemeBilgieri1.Count > 0)
                    {
                        int drc = oChildrenPaketlemeBilgieri1.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenPaketlemeBilgieri1.Remove(0);
                    }

                    foreach (var item in yarimYagliAyranGunlukOzet.yarimYagliPaketlemeBilgileri1s)
                    {
                        oChildPaketlemeBilgieri1 = oChildrenPaketlemeBilgieri1.Add();

                        oChildPaketlemeBilgieri1.SetProperty("U_UrunKodu", item.UrunKodu);

                        oChildPaketlemeBilgieri1.SetProperty("U_UrunAdi", item.UrunAdi);

                        oChildPaketlemeBilgieri1.SetProperty("U_PaketAyranMikAd", item.PaketlenenAyranMiktariAdet);

                        oChildPaketlemeBilgieri1.SetProperty("U_PaketAyranMikLt", item.PaketlenenAyranMiktariLitre);
                    }

                    oChildrenPaketlemeBilgierli2 = oGeneralData.Child("AIF_AYRNOZTYRMYGL2");

                    if (oChildrenPaketlemeBilgierli2.Count > 0)
                    {
                        int drc = oChildrenPaketlemeBilgierli2.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenPaketlemeBilgierli2.Remove(0);
                    }

                    foreach (var item in yarimYagliAyranGunlukOzet.yarimYagliPaketlemeBilgileri2s)
                    {
                        oChildPaketlemeBilgileri2 = oChildrenPaketlemeBilgierli2.Add();

                        oChildPaketlemeBilgileri2.SetProperty("U_ToplamSutMik", item.ToplamSutMiktari);

                        oChildPaketlemeBilgileri2.SetProperty("U_ToplamSuMik", item.ToplamSuMiktari);

                        oChildPaketlemeBilgileri2.SetProperty("U_TuzMiktari", item.TuzMiktari);

                        oChildPaketlemeBilgileri2.SetProperty("U_AyranTuzOrani", item.AyranTuzOrani);

                        oChildPaketlemeBilgileri2.SetProperty("U_ToplamAyranMik", item.ToplamAyranMiktari);
                    }


                    oChildrenGunSonuOzeti = oGeneralData.Child("AIF_AYRNOZTYRMYGL3");

                    if (oChildrenGunSonuOzeti.Count > 0)
                    {
                        int drc = oChildrenGunSonuOzeti.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenGunSonuOzeti.Remove(0);
                    }

                    foreach (var item in yarimYagliAyranGunlukOzet.yarimYagliGunlukOzets)
                    {
                        oChildGunSonuOzeti = oChildrenGunSonuOzeti.Add();

                        oChildGunSonuOzeti.SetProperty("U_MayalananAyranMik", item.MayalananAyranMiktari);

                        oChildGunSonuOzeti.SetProperty("U_OncekiGundenDevMik", item.OncekiGundenDevredenAyranMiktari);

                        oChildGunSonuOzeti.SetProperty("U_TanktakiToplamMik", item.TanktakiToplamAyranMiktari);

                        oChildGunSonuOzeti.SetProperty("U_PaketTankFarki", item.PaketlenenVeTanktakiAyranFarki);

                        oChildGunSonuOzeti.SetProperty("U_SonrakiGuneDevMik", item.SonrakiGuneDevredenAyranMiktari);

                        oChildGunSonuOzeti.SetProperty("U_GunSonuAyranFarki", item.GunSonuAyranFarki);

                        oChildGunSonuOzeti.SetProperty("U_StandartRandiman", item.StandartRandimanDegeri);

                        oChildGunSonuOzeti.SetProperty("U_GerceklesenRandiman", item.GerceklesenRandimanDegeri);
                    }


                    try
                    {
                        oGeneralService.Update(oGeneralData);
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = 0, Description = "Yarım Yağlı Ayran Günlük Özet Girişi Güncellendi.", List = null };
                    }
                    catch (Exception)
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5300, Description = "Hata Kodu - 5300 Yarım Yağlı Ayran Günlük Özet Girişi Güncellenirken Hata Oluştu. " + oCompany.GetLastErrorDescription(), List = null };
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