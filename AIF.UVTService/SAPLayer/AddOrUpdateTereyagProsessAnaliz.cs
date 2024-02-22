using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddOrUpdateTereyagProsessAnaliz
    {
        public Response addOrUpdateTereyagProsessAnaliz(TereyagProsesTakipAnaliz tereyagProsesTakipAnaliz, string dbName, string mKodValue)
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

                oRS.DoQuery("Select * from \"@AIF_TRYGPRSS_ANLZ\" WITH (NOLOCK) where \"U_PartiNo\" = '" + tereyagProsesTakipAnaliz.PartiNo + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChildProsesOzellikleri1;

                    GeneralDataCollection oChildrenProsesOzellikleri1;

                    GeneralData oChildProsesOzellikleri2;

                    GeneralDataCollection oChildrenProsesOzellikleri2;

                    GeneralData oChildHamMaddeMiktarOzellikleri;

                    GeneralDataCollection oChildrenHammaddeMiktarOzellikleri;

                    GeneralData oChildSarfMalzemeKullanim;

                    GeneralDataCollection oChildrenSarfMalzemeKullanim;

                    GeneralData oChildMamulOzellikleri;

                    GeneralDataCollection oChildrenMamulOzellikleri;

                    oCompService = oCompany.GetCompanyService();

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_TRYGPRSS_ANLZ");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralData.SetProperty("U_PartiNo", tereyagProsesTakipAnaliz.PartiNo.ToString());

                    oGeneralData.SetProperty("U_KalemKodu", tereyagProsesTakipAnaliz.KalemKodu.ToString());

                    oGeneralData.SetProperty("U_KalemTanimi", tereyagProsesTakipAnaliz.KalemTanimi.ToString());

                    oGeneralData.SetProperty("U_Aciklama", tereyagProsesTakipAnaliz.Aciklama.ToString());

                    if (tereyagProsesTakipAnaliz.UretimTarihi != null && tereyagProsesTakipAnaliz.UretimTarihi != "")
                    {
                        string tarih = tereyagProsesTakipAnaliz.UretimTarihi;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_UretimTarihi", dt);
                    }

                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    //oGeneralData.SetProperty("U_Tarih", dt);

                    oChildrenProsesOzellikleri1 = oGeneralData.Child("AIF_TRYGPRSS_ANLZ1");

                    foreach (var item in tereyagProsesTakipAnaliz.tereyagProssesOzellikleri1_Detay)
                    {
                        oChildProsesOzellikleri1 = oChildrenProsesOzellikleri1.Add();

                        oChildProsesOzellikleri1.SetProperty("U_PartiNo", item.PartiNo);

                        oChildProsesOzellikleri1.SetProperty("U_YagTuru", item.YagTuru);

                        //oChildProsesOzellikleri1.SetProperty("U_YagTuruKod", item.YagTuruKodu);

                        oChildProsesOzellikleri1.SetProperty("U_GorevliOperator", item.GorevliOperator);

                        oChildProsesOzellikleri1.SetProperty("U_GorevliOperatorAdi", item.GorevliOperatorAdi);

                        oChildProsesOzellikleri1.SetProperty("U_KremaPastorizasyonSicak", item.KremaPastorizasyonSicakligi);

                        oChildProsesOzellikleri1.SetProperty("U_YayigaKremaYukBasSaat", item.YayigaKremaYuklemeBaslamaSaati);

                        oChildProsesOzellikleri1.SetProperty("U_YayigaKremaYukBitSaat", item.YayigaKremaYuklemeBitisSaati);

                        oChildProsesOzellikleri1.SetProperty("U_YagOlusmaSaati", item.YagOlusmaSaati);

                        oChildProsesOzellikleri1.SetProperty("U_YikamaSayisi", item.YikamaSayisi);

                        oChildProsesOzellikleri1.SetProperty("U_YikamaBitisSaati", item.YikamaBitisSaati);

                        oChildProsesOzellikleri1.SetProperty("U_YaginYayiktanIndSaat", item.YaginYayiktanIndirilmeSaati);

                        oChildProsesOzellikleri1.SetProperty("U_YaginGramajlamaBasSaat", item.YaginGramajBaslamaSaati);

                        oChildProsesOzellikleri1.SetProperty("U_YaginGramajlamaBitSaat", item.YaginGramajlamaBitSaati);
                    }

                    oChildrenProsesOzellikleri2 = oGeneralData.Child("AIF_TRYGPRSS_ANLZ2");

                    foreach (var item in tereyagProsesTakipAnaliz.tereyagProssesOzellikleri2_Detay)
                    {
                        oChildProsesOzellikleri2 = oChildrenProsesOzellikleri2.Add();

                        oChildProsesOzellikleri2.SetProperty("U_KremaYuklemeSuresiDk", item.KremaYuklemeSuresiDk);

                        oChildProsesOzellikleri2.SetProperty("U_YayiklamaSuresiDk", item.YayiklamaSuresiDk);

                        oChildProsesOzellikleri2.SetProperty("U_YikamaSuresiDk", item.YikamaSuresiDk);

                        oChildProsesOzellikleri2.SetProperty("U_YaginGramajlamaSuresiDk", item.YaginGramajlamaSuresiDk);

                        oChildProsesOzellikleri2.SetProperty("U_ToplamGecenSureDk", item.ToplamGecenSureDk);
                    }

                    oChildrenHammaddeMiktarOzellikleri = oGeneralData.Child("AIF_TRYGPRSS_ANLZ3");

                    foreach (var item in tereyagProsesTakipAnaliz.tereyagHammaddeMiktarVeOzellik_Detay)
                    {
                        oChildHamMaddeMiktarOzellikleri = oChildrenHammaddeMiktarOzellikleri.Add();

                        oChildHamMaddeMiktarOzellikleri.SetProperty("U_MalzemeAdi", item.MalzemeAdi);

                        oChildHamMaddeMiktarOzellikleri.SetProperty("U_KuruMadde", item.KuruMadde);

                        oChildHamMaddeMiktarOzellikleri.SetProperty("U_YagOrani", item.YagOrani);

                        oChildHamMaddeMiktarOzellikleri.SetProperty("U_PH", item.PH);

                        oChildHamMaddeMiktarOzellikleri.SetProperty("U_SH", item.SH);

                        oChildHamMaddeMiktarOzellikleri.SetProperty("U_HammaddePartiNo", item.HammaddePartiNo);

                        oChildHamMaddeMiktarOzellikleri.SetProperty("U_Miktar", item.Miktar);

                        oChildHamMaddeMiktarOzellikleri.SetProperty("U_Birim", item.Birim);
                    }

                    oChildrenSarfMalzemeKullanim = oGeneralData.Child("AIF_TRYGPRSS_ANLZ4");

                    foreach (var item in tereyagProsesTakipAnaliz.tereyagSarfMalzemeKullanim_Detay)
                    {
                        oChildSarfMalzemeKullanim = oChildrenSarfMalzemeKullanim.Add();

                        oChildSarfMalzemeKullanim.SetProperty("U_MalzemeAdi", item.MalzemeAdi);

                        oChildSarfMalzemeKullanim.SetProperty("U_MalMarkaTedarikci", item.MalzemeMarkaTedarikcisi);

                        oChildSarfMalzemeKullanim.SetProperty("U_SarfMalzemePartiNo", item.SarfMalzemePartiNo);

                        oChildSarfMalzemeKullanim.SetProperty("U_Miktar", item.Miktar);

                        oChildSarfMalzemeKullanim.SetProperty("U_Birim", item.Birim);
                    }

                    oChildrenMamulOzellikleri = oGeneralData.Child("AIF_TRYGPRSS_ANLZ5");

                    foreach (var item in tereyagProsesTakipAnaliz.tereyagMamulOzellikleri_Detay)
                    {
                        oChildMamulOzellikleri = oChildrenMamulOzellikleri.Add();

                        oChildMamulOzellikleri.SetProperty("U_HammaddeSarfMalzTop", item.HammaddeVeSarfMalzemeToplami);

                        oChildMamulOzellikleri.SetProperty("U_UretilenUrunMik", item.UretilenUrunMiktariKg);

                        oChildMamulOzellikleri.SetProperty("U_UretilenUrunAdi", item.UretilenUrunAdi);

                        oChildMamulOzellikleri.SetProperty("U_KuruMadde", item.KuruMadde);

                        oChildMamulOzellikleri.SetProperty("U_YagOrani", item.YagOrani);

                        oChildMamulOzellikleri.SetProperty("U_YayikAltiSuyuOran", item.YayikAltiSuyuYagOrani);

                        oChildMamulOzellikleri.SetProperty("U_YaginPH", item.YaginPHDegeri);

                        oChildMamulOzellikleri.SetProperty("U_UretilenHamur", item.UretilenHamur);

                    }

                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_TRYGPRSS_ANLZ\" WITH (NOLOCK)");

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
                        return new Response { Value = 0, Description = "Tereyağ Proses Analiz Takibi girişi oluşturuldu..", List = null };
                    }
                    else

                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5200, Description = "Hata Kodu - 5200 Tereyağ Proses Analiz Takibi girişi oluşturulurken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                    }
                }
                else
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChildProsesOzellikleri1;

                    GeneralDataCollection oChildrenProsesOzellikleri1;

                    GeneralData oChildProsesOzellikleri2;

                    GeneralDataCollection oChildrenProsesOzellikleri2;

                    GeneralData oChildHamMaddeMiktarOzellikleri;

                    GeneralDataCollection oChildrenHammaddeMiktarOzellikleri;

                    GeneralData oChildSarfMalzemeKullanim;

                    GeneralDataCollection oChildrenSarfMalzemeKullanim;

                    GeneralData oChildMamulOzellikleri;

                    GeneralDataCollection oChildrenMamulOzellikleri;

                    oCompService = oCompany.GetCompanyService();

                    GeneralDataParams oGeneralParams;

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_TRYGPRSS_ANLZ");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    oGeneralData.SetProperty("U_PartiNo", tereyagProsesTakipAnaliz.PartiNo.ToString());

                    oGeneralData.SetProperty("U_KalemKodu", tereyagProsesTakipAnaliz.KalemKodu.ToString());

                    oGeneralData.SetProperty("U_KalemTanimi", tereyagProsesTakipAnaliz.KalemTanimi.ToString());

                    oGeneralData.SetProperty("U_Aciklama", tereyagProsesTakipAnaliz.Aciklama.ToString());

                    if (tereyagProsesTakipAnaliz.UretimTarihi != null && tereyagProsesTakipAnaliz.UretimTarihi != "")
                    {
                        string tarih = tereyagProsesTakipAnaliz.UretimTarihi;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_UretimTarihi", dt);
                    }
                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    //oGeneralData.SetProperty("U_Tarih", dt);

                    oChildrenProsesOzellikleri1 = oGeneralData.Child("AIF_TRYGPRSS_ANLZ1");

                    if (oChildrenProsesOzellikleri1.Count > 0)
                    {
                        int drc = oChildrenProsesOzellikleri1.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenProsesOzellikleri1.Remove(0);
                    }

                    foreach (var item in tereyagProsesTakipAnaliz.tereyagProssesOzellikleri1_Detay)
                    {
                        oChildProsesOzellikleri1 = oChildrenProsesOzellikleri1.Add();

                        oChildProsesOzellikleri1.SetProperty("U_PartiNo", item.PartiNo);

                        oChildProsesOzellikleri1.SetProperty("U_YagTuru", item.YagTuru);

                        //oChildProsesOzellikleri1.SetProperty("U_YagTuruKod", item.YagTuruKodu);

                        oChildProsesOzellikleri1.SetProperty("U_GorevliOperator", item.GorevliOperator);

                        oChildProsesOzellikleri1.SetProperty("U_GorevliOperatorAdi", item.GorevliOperatorAdi);

                        oChildProsesOzellikleri1.SetProperty("U_KremaPastorizasyonSicak", item.KremaPastorizasyonSicakligi);

                        oChildProsesOzellikleri1.SetProperty("U_YayigaKremaYukBasSaat", item.YayigaKremaYuklemeBaslamaSaati);

                        oChildProsesOzellikleri1.SetProperty("U_YayigaKremaYukBitSaat", item.YayigaKremaYuklemeBitisSaati);

                        oChildProsesOzellikleri1.SetProperty("U_YagOlusmaSaati", item.YagOlusmaSaati);

                        oChildProsesOzellikleri1.SetProperty("U_YikamaSayisi", item.YikamaSayisi);

                        oChildProsesOzellikleri1.SetProperty("U_YikamaBitisSaati", item.YikamaBitisSaati);

                        oChildProsesOzellikleri1.SetProperty("U_YaginYayiktanIndSaat", item.YaginYayiktanIndirilmeSaati);

                        oChildProsesOzellikleri1.SetProperty("U_YaginGramajlamaBasSaat", item.YaginGramajBaslamaSaati);

                        oChildProsesOzellikleri1.SetProperty("U_YaginGramajlamaBitSaat", item.YaginGramajlamaBitSaati);
                    }

                    oChildrenProsesOzellikleri2 = oGeneralData.Child("AIF_TRYGPRSS_ANLZ2");

                    if (oChildrenProsesOzellikleri2.Count > 0)
                    {
                        int drc = oChildrenProsesOzellikleri2.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenProsesOzellikleri2.Remove(0);
                    }

                    foreach (var item in tereyagProsesTakipAnaliz.tereyagProssesOzellikleri2_Detay)
                    {
                        oChildProsesOzellikleri2 = oChildrenProsesOzellikleri2.Add();

                        oChildProsesOzellikleri2.SetProperty("U_KremaYuklemeSuresiDk", item.KremaYuklemeSuresiDk);

                        oChildProsesOzellikleri2.SetProperty("U_YayiklamaSuresiDk", item.YayiklamaSuresiDk);

                        oChildProsesOzellikleri2.SetProperty("U_YikamaSuresiDk", item.YikamaSuresiDk);

                        oChildProsesOzellikleri2.SetProperty("U_YaginGramajlamaSuresiDk", item.YaginGramajlamaSuresiDk);

                        oChildProsesOzellikleri2.SetProperty("U_ToplamGecenSureDk", item.ToplamGecenSureDk);
                    }

                    oChildrenHammaddeMiktarOzellikleri = oGeneralData.Child("AIF_TRYGPRSS_ANLZ3");

                    if (oChildrenHammaddeMiktarOzellikleri.Count > 0)
                    {
                        int drc = oChildrenHammaddeMiktarOzellikleri.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenHammaddeMiktarOzellikleri.Remove(0);
                    }

                    foreach (var item in tereyagProsesTakipAnaliz.tereyagHammaddeMiktarVeOzellik_Detay)
                    {
                        oChildHamMaddeMiktarOzellikleri = oChildrenHammaddeMiktarOzellikleri.Add();

                        oChildHamMaddeMiktarOzellikleri.SetProperty("U_MalzemeAdi", item.MalzemeAdi);

                        oChildHamMaddeMiktarOzellikleri.SetProperty("U_KuruMadde", item.KuruMadde);

                        oChildHamMaddeMiktarOzellikleri.SetProperty("U_YagOrani", item.YagOrani);

                        oChildHamMaddeMiktarOzellikleri.SetProperty("U_PH", item.PH);

                        oChildHamMaddeMiktarOzellikleri.SetProperty("U_SH", item.SH);

                        oChildHamMaddeMiktarOzellikleri.SetProperty("U_HammaddePartiNo", item.HammaddePartiNo);

                        oChildHamMaddeMiktarOzellikleri.SetProperty("U_Miktar", item.Miktar);

                        oChildHamMaddeMiktarOzellikleri.SetProperty("U_Birim", item.Birim);
                    }

                    oChildrenSarfMalzemeKullanim = oGeneralData.Child("AIF_TRYGPRSS_ANLZ4");

                    if (oChildrenSarfMalzemeKullanim.Count > 0)
                    {
                        int drc = oChildrenSarfMalzemeKullanim.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenSarfMalzemeKullanim.Remove(0);
                    }

                    foreach (var item in tereyagProsesTakipAnaliz.tereyagSarfMalzemeKullanim_Detay)
                    {
                        oChildSarfMalzemeKullanim = oChildrenSarfMalzemeKullanim.Add();

                        oChildSarfMalzemeKullanim.SetProperty("U_MalzemeAdi", item.MalzemeAdi);

                        oChildSarfMalzemeKullanim.SetProperty("U_MalMarkaTedarikci", item.MalzemeMarkaTedarikcisi);

                        oChildSarfMalzemeKullanim.SetProperty("U_SarfMalzemePartiNo", item.SarfMalzemePartiNo);

                        oChildSarfMalzemeKullanim.SetProperty("U_Miktar", item.Miktar);

                        oChildSarfMalzemeKullanim.SetProperty("U_Birim", item.Birim);
                    }

                    oChildrenMamulOzellikleri = oGeneralData.Child("AIF_TRYGPRSS_ANLZ5");

                    if (oChildrenMamulOzellikleri.Count > 0)
                    {
                        int drc = oChildrenMamulOzellikleri.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenMamulOzellikleri.Remove(0);
                    }

                    foreach (var item in tereyagProsesTakipAnaliz.tereyagMamulOzellikleri_Detay)
                    {
                        oChildMamulOzellikleri = oChildrenMamulOzellikleri.Add();

                        oChildMamulOzellikleri.SetProperty("U_HammaddeSarfMalzTop", item.HammaddeVeSarfMalzemeToplami);

                        oChildMamulOzellikleri.SetProperty("U_UretilenUrunMik", item.UretilenUrunMiktariKg);

                        oChildMamulOzellikleri.SetProperty("U_UretilenUrunAdi", item.UretilenUrunAdi);

                        oChildMamulOzellikleri.SetProperty("U_KuruMadde", item.KuruMadde);

                        oChildMamulOzellikleri.SetProperty("U_YagOrani", item.YagOrani);

                        oChildMamulOzellikleri.SetProperty("U_YayikAltiSuyuOran", item.YayikAltiSuyuYagOrani);

                        oChildMamulOzellikleri.SetProperty("U_YaginPH", item.YaginPHDegeri);

                        oChildMamulOzellikleri.SetProperty("U_UretilenHamur", item.UretilenHamur);

                    }

                    try
                    {
                        oGeneralService.Update(oGeneralData);
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = 0, Description = "Tereyağ Proses Analiz Takibi girişi güncellendi.", List = null };
                    }
                    catch (Exception)
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5300, Description = "Hata Kodu - 5300 Tereyağ Proses Analiz Takibi girişi güncellenirken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
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