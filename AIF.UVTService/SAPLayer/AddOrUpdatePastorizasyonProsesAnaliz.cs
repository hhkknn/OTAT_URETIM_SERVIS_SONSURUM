using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddOrUpdatePastorizasyonProsesAnaliz
    {
        public Response addOrUpdatePastorizasyonProsesAnaliz(PastorizasyonProsesTakipAnaliz PastorizasyonProsesTakipAnaliz, string dbName, string mKodValue)
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

                oRS.DoQuery("Select * from \"@AIF_PASPRSS_ANLZ\" WITH (NOLOCK) where \"U_PartiNo\" = '" + PastorizasyonProsesTakipAnaliz.PartiNo + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChildProsesOzellikleri1;

                    GeneralDataCollection oChildrenProsesOzellikleri1;

                    GeneralData oChildProsesOzellikleri2;

                    GeneralDataCollection oChildrenProsesOzellikleri2;

                    GeneralData oChildProsesOzellikleri3;

                    GeneralDataCollection oChildrenProsesOzellikleri3;

                    GeneralData oChildGunlukProsesOzeti1;

                    GeneralDataCollection oChildrenGunlukProsesOzeti1;

                    GeneralData oChildGunlukProsesOzeti2;

                    GeneralDataCollection oChildrenGunlukProsesOzeti2;


                    oCompService = oCompany.GetCompanyService();

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_PASPRSS_ANLZ");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralData.SetProperty("U_PartiNo", PastorizasyonProsesTakipAnaliz.PartiNo.ToString());

                    //oGeneralData.SetProperty("U_KalemKodu", PastorizasyonProsesTakipAnaliz.uru.ToString());

                    oGeneralData.SetProperty("U_UretimSiparisNo", PastorizasyonProsesTakipAnaliz.UretimSiparisNo.ToString());

                    oGeneralData.SetProperty("U_UretilenUrunTanimi", PastorizasyonProsesTakipAnaliz.UretilenUrunTanimi.ToString());


                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    //oGeneralData.SetProperty("U_Tarih", dt);

                    oChildrenProsesOzellikleri1 = oGeneralData.Child("AIF_PASPRSS_ANLZ1");

                    foreach (var item in PastorizasyonProsesTakipAnaliz.PastorizasyonProsesOzellikleri1s)
                    {
                        oChildProsesOzellikleri1 = oChildrenProsesOzellikleri1.Add();

                        oChildProsesOzellikleri1.SetProperty("U_SutunAlinanTankAdi", item.SutunAlinanTankAdi);

                        oChildProsesOzellikleri1.SetProperty("U_AlinanSutunPartiNo", item.AlinanSutunPartiNo);

                        oChildProsesOzellikleri1.SetProperty("U_YagCekilecekSutMik", item.YagCekilecekSutMik);

                        oChildProsesOzellikleri1.SetProperty("U_SutYagOrani", item.SutYagOrani);

                        oChildProsesOzellikleri1.SetProperty("U_SutunPh", item.SutunPh);

                        oChildProsesOzellikleri1.SetProperty("U_KremaYogunlugu", item.KremaYogunlugu);

                        oChildProsesOzellikleri1.SetProperty("U_KremaPh", item.KremaPh);

                        oChildProsesOzellikleri1.SetProperty("U_KremaYagOrani", item.KremaYagOrani);

                        oChildProsesOzellikleri1.SetProperty("U_CekilenKremaMikKG", item.CekilenKremaMikKG);

                        oChildProsesOzellikleri1.SetProperty("U_CekilenKremaMikLT", item.CekilenKremaMikLT);

                        oChildProsesOzellikleri1.SetProperty("U_YagAlnmsSutYagOr", item.YagAlnmsSutYagOr);

                        oChildProsesOzellikleri1.SetProperty("U_YagAlnmsSutPH", item.YagAlnmsSutPH);

                        oChildProsesOzellikleri1.SetProperty("U_SutunGondTankAdi", item.SutunGondTankAdi);
                    }

                    oChildrenProsesOzellikleri2 = oGeneralData.Child("AIF_PASPRSS_ANLZ2");

                    foreach (var item in PastorizasyonProsesTakipAnaliz.PastorizasyonProsesOzellikleri2s)
                    {
                        oChildProsesOzellikleri2 = oChildrenProsesOzellikleri2.Add();

                        oChildProsesOzellikleri2.SetProperty("U_UretilenKremaParti", item.UretilenKremaParti);

                        oChildProsesOzellikleri2.SetProperty("U_KremaPastBasSaat", item.KremaPastBasSaat);

                        oChildProsesOzellikleri2.SetProperty("U_KremaPastSicakligi", item.KremaPastSicakligi);

                        oChildProsesOzellikleri2.SetProperty("U_KremaPastBitSaat", item.KremaPastBitSaat);

                        oChildProsesOzellikleri2.SetProperty("U_KremaMayalamaSaati", item.KremaMayalamaSaati);

                        oChildProsesOzellikleri2.SetProperty("U_KremaMayalamaSicak", item.KremaMayalamaSicak);

                        oChildProsesOzellikleri2.SetProperty("U_KremaMayalamaPh", item.KremaMayalamaPh);

                        oChildProsesOzellikleri2.SetProperty("U_MayalamaKazanFiltTem", item.MayalamaKazanFiltTem);

                        oChildProsesOzellikleri2.SetProperty("U_KremaDolumBasSaat", item.KremaDolumBasSaat);

                        oChildProsesOzellikleri2.SetProperty("U_KremaDolumBitSaat", item.KremaDolumBitSaat);

                        oChildProsesOzellikleri2.SetProperty("U_KremaDolumYapan", item.KremaDolumYapan);

                        oChildProsesOzellikleri2.SetProperty("U_DolumSicakligi", item.DolumSicakligi);

                        oChildProsesOzellikleri2.SetProperty("U_UretimYapan", item.UretimYapan);

                        oChildProsesOzellikleri2.SetProperty("U_KontrolEdenMuh", item.KontrolEdenMuh);
                    }

                    oChildrenProsesOzellikleri3 = oGeneralData.Child("AIF_PASPRSS_ANLZ3");

                    foreach (var item in PastorizasyonProsesTakipAnaliz.PastorizasyonProsesOzellikleri3s)
                    {
                        oChildProsesOzellikleri3 = oChildrenProsesOzellikleri3.Add();

                        oChildProsesOzellikleri3.SetProperty("U_UretilenKremaParti", item.UretilenKremaParti);

                        oChildProsesOzellikleri3.SetProperty("U_KulKulturAdiVeKodu", item.KullanilanKulturVeKodu);

                        oChildProsesOzellikleri3.SetProperty("U_KulturMiktari", item.KulturMiktari);

                        oChildProsesOzellikleri3.SetProperty("U_UretilenKremaMik", item.UretilenKremaMiktari);

                        oChildProsesOzellikleri3.SetProperty("U_KremaYagOrani", item.KremaYagOrani);

                        oChildProsesOzellikleri3.SetProperty("U_DolumYapilanAmbalaj", item.DolumYapilanAmbalaj);

                        oChildProsesOzellikleri3.SetProperty("U_KulAmbalajMik", item.KullanimYapilanAmbalajMiktari);

                        oChildProsesOzellikleri3.SetProperty("U_BirAmbOrtMiktar", item.BirAmbOrtMiktar);

                        oChildProsesOzellikleri3.SetProperty("U_KremaDepoPh", item.KremaDepoPh);

                        oChildProsesOzellikleri3.SetProperty("U_KremaninDepoSicakligi", item.KremaninDepoSicakligi);

                        oChildProsesOzellikleri3.SetProperty("U_UretimYapan", item.UretimYapanOperator);

                        oChildProsesOzellikleri3.SetProperty("U_KontrolEdenMuh", item.KontrolEdenMuhendis);
                    }



                    oChildrenGunlukProsesOzeti1 = oGeneralData.Child("AIF_PASPRSS_ANLZ4");

                    foreach (var item in PastorizasyonProsesTakipAnaliz.PastorizasyonGunluk1s)
                    {
                        oChildGunlukProsesOzeti1 = oChildrenGunlukProsesOzeti1.Add();

                        oChildGunlukProsesOzeti1.SetProperty("U_VeriAdi", item.VeriAdi);

                        oChildGunlukProsesOzeti1.SetProperty("U_Kova", item.Kova);

                        oChildGunlukProsesOzeti1.SetProperty("U_Teneke", item.Teneke);

                        oChildGunlukProsesOzeti1.SetProperty("U_ToplamveyaOrt", item.ToplamveyaOrt);
                    }


                    oChildrenGunlukProsesOzeti2 = oGeneralData.Child("AIF_PASPRSS_ANLZ5");

                    foreach (var item in PastorizasyonProsesTakipAnaliz.PastorizasyonGunluk2s)
                    {
                        oChildGunlukProsesOzeti2 = oChildrenGunlukProsesOzeti2.Add();

                        oChildGunlukProsesOzeti2.SetProperty("U_VeriAdi", item.VeriAdi);

                        oChildGunlukProsesOzeti2.SetProperty("U_Deger", item.Deger);
                    }

                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_PASPRSS_ANLZ\" WITH (NOLOCK)");

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
                        return new Response { Value = 0, Description = "Pastörizasyon Proses Analiz Takibi girişi oluşturuldu..", List = null };
                    }
                    else
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5200, Description = "Hata Kodu - 5200 Pastörizasyon Proses Analiz Takibi girişi oluşturulurken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
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

                    GeneralData oChildProsesOzellikleri3;

                    GeneralDataCollection oChildrenProsesOzellikleri3;

                    GeneralData oChildGunlukProsesOzeti1;

                    GeneralDataCollection oChildrenGunlukProsesOzeti1;

                    GeneralData oChildGunlukProsesOzeti2;

                    GeneralDataCollection oChildrenGunlukProsesOzeti2;

                    oCompService = oCompany.GetCompanyService();

                    GeneralDataParams oGeneralParams;

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_PASPRSS_ANLZ");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    oGeneralData.SetProperty("U_PartiNo", PastorizasyonProsesTakipAnaliz.PartiNo.ToString());

                    //oGeneralData.SetProperty("U_KalemKodu", PastorizasyonProsesTakipAnaliz.uru.ToString());

                    oGeneralData.SetProperty("U_UretimSiparisNo", PastorizasyonProsesTakipAnaliz.UretimSiparisNo.ToString());

                    oGeneralData.SetProperty("U_UretilenUrunTanimi", PastorizasyonProsesTakipAnaliz.UretilenUrunTanimi.ToString());



                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    //oGeneralData.SetProperty("U_Tarih", dt);

                    oChildrenProsesOzellikleri1 = oGeneralData.Child("AIF_PASPRSS_ANLZ1");

                    if (oChildrenProsesOzellikleri1.Count > 0)
                    {
                        int drc = oChildrenProsesOzellikleri1.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenProsesOzellikleri1.Remove(0);
                    }

                    foreach (var item in PastorizasyonProsesTakipAnaliz.PastorizasyonProsesOzellikleri1s)
                    {
                        oChildProsesOzellikleri1 = oChildrenProsesOzellikleri1.Add();

                        oChildProsesOzellikleri1.SetProperty("U_SutunAlinanTankAdi", item.SutunAlinanTankAdi);

                        oChildProsesOzellikleri1.SetProperty("U_AlinanSutunPartiNo", item.AlinanSutunPartiNo);

                        oChildProsesOzellikleri1.SetProperty("U_YagCekilecekSutMik", item.YagCekilecekSutMik);

                        oChildProsesOzellikleri1.SetProperty("U_SutYagOrani", item.SutYagOrani);

                        oChildProsesOzellikleri1.SetProperty("U_SutunPh", item.SutunPh);

                        oChildProsesOzellikleri1.SetProperty("U_KremaYogunlugu", item.KremaYogunlugu);

                        oChildProsesOzellikleri1.SetProperty("U_KremaPh", item.KremaPh);

                        oChildProsesOzellikleri1.SetProperty("U_KremaYagOrani", item.KremaYagOrani);

                        oChildProsesOzellikleri1.SetProperty("U_CekilenKremaMikKG", item.CekilenKremaMikKG);

                        oChildProsesOzellikleri1.SetProperty("U_CekilenKremaMikLT", item.CekilenKremaMikLT);

                        oChildProsesOzellikleri1.SetProperty("U_YagAlnmsSutYagOr", item.YagAlnmsSutYagOr);

                        oChildProsesOzellikleri1.SetProperty("U_YagAlnmsSutPH", item.YagAlnmsSutPH);

                        oChildProsesOzellikleri1.SetProperty("U_SutunGondTankAdi", item.SutunGondTankAdi);
                    }


                    oChildrenProsesOzellikleri2 = oGeneralData.Child("AIF_PASPRSS_ANLZ2");

                    if (oChildrenProsesOzellikleri2.Count > 0)
                    {
                        int drc = oChildrenProsesOzellikleri2.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenProsesOzellikleri2.Remove(0);
                    }

                    foreach (var item in PastorizasyonProsesTakipAnaliz.PastorizasyonProsesOzellikleri2s)
                    {
                        oChildProsesOzellikleri2 = oChildrenProsesOzellikleri2.Add();

                        oChildProsesOzellikleri2.SetProperty("U_UretilenKremaParti", item.UretilenKremaParti);

                        oChildProsesOzellikleri2.SetProperty("U_KremaPastBasSaat", item.KremaPastBasSaat);

                        oChildProsesOzellikleri2.SetProperty("U_KremaPastSicakligi", item.KremaPastSicakligi);

                        oChildProsesOzellikleri2.SetProperty("U_KremaPastBitSaat", item.KremaPastBitSaat);

                        oChildProsesOzellikleri2.SetProperty("U_KremaMayalamaSaati", item.KremaMayalamaSaati);

                        oChildProsesOzellikleri2.SetProperty("U_KremaMayalamaSicak", item.KremaMayalamaSicak);

                        oChildProsesOzellikleri2.SetProperty("U_KremaMayalamaPh", item.KremaMayalamaPh);

                        oChildProsesOzellikleri2.SetProperty("U_MayalamaKazanFiltTem", item.MayalamaKazanFiltTem);

                        oChildProsesOzellikleri2.SetProperty("U_KremaDolumBasSaat", item.KremaDolumBasSaat);

                        oChildProsesOzellikleri2.SetProperty("U_KremaDolumBitSaat", item.KremaDolumBitSaat);

                        oChildProsesOzellikleri2.SetProperty("U_KremaDolumYapan", item.KremaDolumYapan);

                        oChildProsesOzellikleri2.SetProperty("U_DolumSicakligi", item.DolumSicakligi);

                        oChildProsesOzellikleri2.SetProperty("U_UretimYapan", item.UretimYapan);

                        oChildProsesOzellikleri2.SetProperty("U_KontrolEdenMuh", item.KontrolEdenMuh);
                    }

                    oChildrenProsesOzellikleri3 = oGeneralData.Child("AIF_PASPRSS_ANLZ3");

                    if (oChildrenProsesOzellikleri3.Count > 0)
                    {
                        int drc = oChildrenProsesOzellikleri3.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenProsesOzellikleri3.Remove(0);
                    }


                    foreach (var item in PastorizasyonProsesTakipAnaliz.PastorizasyonProsesOzellikleri3s)
                    {
                        oChildProsesOzellikleri3 = oChildrenProsesOzellikleri3.Add();

                        oChildProsesOzellikleri3.SetProperty("U_UretilenKremaParti", item.UretilenKremaParti);

                        oChildProsesOzellikleri3.SetProperty("U_KulKulturAdiVeKodu", item.KullanilanKulturVeKodu);

                        oChildProsesOzellikleri3.SetProperty("U_KulturMiktari", item.KulturMiktari);

                        oChildProsesOzellikleri3.SetProperty("U_UretilenKremaMik", item.UretilenKremaMiktari);

                        oChildProsesOzellikleri3.SetProperty("U_KremaYagOrani", item.KremaYagOrani);

                        oChildProsesOzellikleri3.SetProperty("U_DolumYapilanAmbalaj", item.DolumYapilanAmbalaj);

                        oChildProsesOzellikleri3.SetProperty("U_KulAmbalajMik", item.KullanimYapilanAmbalajMiktari);

                        oChildProsesOzellikleri3.SetProperty("U_BirAmbOrtMiktar", item.BirAmbOrtMiktar);

                        oChildProsesOzellikleri3.SetProperty("U_KremaDepoPh", item.KremaDepoPh);

                        oChildProsesOzellikleri3.SetProperty("U_KremaninDepoSicakligi", item.KremaninDepoSicakligi);

                        oChildProsesOzellikleri3.SetProperty("U_UretimYapan", item.UretimYapanOperator);

                        oChildProsesOzellikleri3.SetProperty("U_KontrolEdenMuh", item.KontrolEdenMuhendis);
                    }


                    oChildrenGunlukProsesOzeti1 = oGeneralData.Child("AIF_PASPRSS_ANLZ4");

                    if (oChildrenGunlukProsesOzeti1.Count > 0)
                    {
                        int drc = oChildrenGunlukProsesOzeti1.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenGunlukProsesOzeti1.Remove(0);
                    }

                    foreach (var item in PastorizasyonProsesTakipAnaliz.PastorizasyonGunluk1s)
                    {
                        oChildGunlukProsesOzeti1 = oChildrenGunlukProsesOzeti1.Add();

                        oChildGunlukProsesOzeti1.SetProperty("U_VeriAdi", item.VeriAdi);

                        oChildGunlukProsesOzeti1.SetProperty("U_Kova", item.Kova);

                        oChildGunlukProsesOzeti1.SetProperty("U_Teneke", item.Teneke);

                        oChildGunlukProsesOzeti1.SetProperty("U_ToplamveyaOrt", item.ToplamveyaOrt);
                    }


                    oChildrenGunlukProsesOzeti2 = oGeneralData.Child("AIF_PASPRSS_ANLZ5");

                    if (oChildrenGunlukProsesOzeti2.Count > 0)
                    {
                        int drc = oChildrenGunlukProsesOzeti2.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenGunlukProsesOzeti2.Remove(0);
                    }


                    foreach (var item in PastorizasyonProsesTakipAnaliz.PastorizasyonGunluk2s)
                    {
                        oChildGunlukProsesOzeti2 = oChildrenGunlukProsesOzeti2.Add();

                        oChildGunlukProsesOzeti2.SetProperty("U_VeriAdi", item.VeriAdi);

                        oChildGunlukProsesOzeti2.SetProperty("U_Deger", item.Deger);
                    }


                    try
                    {
                        oGeneralService.Update(oGeneralData);
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = 0, Description = "Pastörizasyon Proses Analiz Takibi girişi güncellendi.", List = null };
                    }
                    catch (Exception)
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5300, Description = "Hata Kodu - 5300 Pastörizasyon Proses Analiz Takibi girişi güncellenirken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
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