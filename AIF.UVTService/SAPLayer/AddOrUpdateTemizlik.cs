using UVTService.Models;
using SAPbobsCOM;
using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddOrUpdateTemizlik
    {
        public Response addOrUpdateTemizlik(AnalizTemizlik analizTemizlik, string dbName, string mKodValue)
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

                SAPbobsCOM.Company oCompany = connection.oCompany;

                Recordset oRS = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

                oRS.DoQuery("Select * from \"@AIF_TEMIZLIK\" WITH (NOLOCK) where Convert(varchar,\"U_Tarih\",112) = '" + analizTemizlik.Tarih + "' and \"U_IstasyonKodu\" = '" + analizTemizlik.IstasyonKodu + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChildCIP1;

                    GeneralDataCollection oChildrenCIP1;

                    GeneralData oChildCIP2;

                    GeneralDataCollection oChildrenCIP2;

                    GeneralData oChildAletEkipman;

                    GeneralDataCollection oChildrenAletEkipman;

                    oCompService = oCompany.GetCompanyService();

                    oGeneralService = oCompService.GetGeneralService("AIF_TEMIZLIK");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);


                    oGeneralData.SetProperty("U_IstasyonKodu", analizTemizlik.IstasyonKodu.ToString());

                    oGeneralData.SetProperty("U_IstasyonTanimi", analizTemizlik.IstasyonTanimi.ToString());


                    DateTime dt = new DateTime(Convert.ToInt32(analizTemizlik.Tarih.Substring(0, 4)), Convert.ToInt32(analizTemizlik.Tarih.Substring(4, 2)), Convert.ToInt32(analizTemizlik.Tarih.Substring(6, 2)));


                    oGeneralData.SetProperty("U_Tarih", dt.ToString());
                    //if (analizTemizlik.YoneticiOnayi != null)
                    //{
                    //    oGeneralData.SetProperty("U_YoneticiOnay", analizTemizlik.YoneticiOnayi);
                    //    DateTime dt2 = new DateTime(Convert.ToInt32(analizTemizlik.YoneticiOnayTarihi.Substring(0, 4)), Convert.ToInt32(analizTemizlik.YoneticiOnayTarihi.Substring(4, 2)), Convert.ToInt32(analizTemizlik.YoneticiOnayTarihi.Substring(6, 2)));
                    //    oGeneralData.SetProperty("U_YoneticiOnyTar", dt2.ToString()); 
                    //    oGeneralData.SetProperty("U_YoneticiAciklama", analizTemizlik.YoneticiOnayAciklama);
                    //}



                    //oChildrenCIP1 = oGeneralData.Child("AIF_TEMIZLIK1");

                    //foreach (var item in analizTemizlik.cIP1s)
                    //{
                    //    oChildCIP1 = oChildrenCIP1.Add();

                    //    oChildCIP1.SetProperty("U_KontrolAdi", item.KontrolNoktasiAdi);

                    //    oChildCIP1.SetProperty("U_DegerKod", item.DegerKod);

                    //    oChildCIP1.SetProperty("U_Deger", item.Deger);
                    //}

                    oChildrenCIP2 = oGeneralData.Child("AIF_TEMIZLIK2");

                    foreach (var item in analizTemizlik.cIP2s)
                    {
                        oChildCIP2 = oChildrenCIP2.Add();

                        oChildCIP2.SetProperty("U_KontrolAdi", item.KontrolNoktasiAdi);

                        oChildCIP2.SetProperty("U_AlkaliBasSaat", item.AlkaliBasSaat);

                        oChildCIP2.SetProperty("U_AlkaliSc", item.AlkaliSc);

                        oChildCIP2.SetProperty("U_AlkaliKonMs", item.AlkaliKonMs);

                        oChildCIP2.SetProperty("U_AlkaliKonYuzde", item.AlkaliKonYuzde);

                        oChildCIP2.SetProperty("U_AlkaliBitSaat", item.AlkaliBitSaat);

                        oChildCIP2.SetProperty("U_AsitBasSaat", item.AsitBasSaat);

                        oChildCIP2.SetProperty("U_AsitSc", item.AsitSc);

                        oChildCIP2.SetProperty("U_AsitKonMs", item.AsitKonMs);

                        oChildCIP2.SetProperty("U_AsitKonYuzde", item.AsitKonYuzde);

                        oChildCIP2.SetProperty("U_AsitBitSaat", item.AsitBitSaat);

                        oChildCIP2.SetProperty("U_TemizlikPers", item.TemizlikPers);

                        oChildCIP2.SetProperty("U_OzonitKontrol", item.OzonitKontrol);

                        oChildCIP2.SetProperty("U_DurulamaPh", item.DurulamaPh);

                        oChildCIP2.SetProperty("U_DezenPrsnl", item.DezenPrsnl);

                        oChildCIP2.SetProperty("U_HaslamaSc", item.HaslamaSc);

                        oChildCIP2.SetProperty("U_HaslamaPrsnl", item.HaslamaPrsnl);

                        oChildCIP2.SetProperty("U_KontrolPersonel", item.KontrolPersonel);

                        oChildCIP2.SetProperty("U_Yapildi", item.Yapildi);

                        oChildCIP2.SetProperty("U_Yapilmadi", item.Yapilmadi);

                    }

                    oChildrenAletEkipman = oGeneralData.Child("AIF_TEMIZLIK3");

                    foreach (var item in analizTemizlik.aletEkipmanTemizliks)
                    {
                        oChildAletEkipman = oChildrenAletEkipman.Add();

                        oChildAletEkipman.SetProperty("U_AlanMakine", item.TemizlenenAlanMakine);
                        oChildAletEkipman.SetProperty("U_YapilmaSikligi", item.TemizlikYapilmaSikligi);
                        oChildAletEkipman.SetProperty("U_KimyasalAdi", item.KullanilanKimysalAdi);
                        oChildAletEkipman.SetProperty("U_KimyasalMiktari", item.KullanilanKimysalMiktar);
                        oChildAletEkipman.SetProperty("U_KontrolYapildi", item.TemizlikYapildi);
                        oChildAletEkipman.SetProperty("U_KontrolYapilmadi", item.TemizlikYapilmadi);
                        oChildAletEkipman.SetProperty("U_TemizlikYapanPrsnl", item.TemizlikYapanPersonel);
                        oChildAletEkipman.SetProperty("U_TemizlikKontPrsnl", item.TemizlikKontrolEdenPersonel);
                    }


                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_TEMIZLIK\" WITH (NOLOCK)");

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
                        return new Response { Value = 0, Description = "Temizlik girişi oluşturuldu..", List = null };
                    }
                    else

                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
                        return new Response { Value = -5200, Description = "Hata Kodu - 5200  Temizlik girişi oluşturulurken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                    }
                }
                else
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChildCIP1;

                    GeneralDataCollection oChildrenCIP1;

                    GeneralData oChildCIP2;

                    GeneralDataCollection oChildrenCIP2;

                    GeneralData oChildAletEkipman;

                    GeneralDataCollection oChildrenAletEkipman;

                    GeneralDataParams oGeneralParams;

                    oCompService = oCompany.GetCompanyService();

                    oGeneralService = oCompService.GetGeneralService("AIF_TEMIZLIK");


                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    if (analizTemizlik.YoneticiOnayi == null)
                    {
                        //oGeneralData.SetProperty("U_IstasyonKodu", analizTemizlik.IstasyonKodu.ToString());

                        //oGeneralData.SetProperty("U_IstasyonTanimi", analizTemizlik.IstasyonTanimi.ToString());


                        DateTime dt = new DateTime(Convert.ToInt32(analizTemizlik.Tarih.Substring(0, 4)), Convert.ToInt32(analizTemizlik.Tarih.Substring(4, 2)), Convert.ToInt32(analizTemizlik.Tarih.Substring(6, 2)));


                        //oGeneralData.SetProperty("U_Tarih", dt.ToString());

                        //oChildrenCIP1 = oGeneralData.Child("AIF_TEMIZLIK1");

                        //if (oChildrenCIP1.Count > 0)
                        //{
                        //    int drc = oChildrenCIP1.Count;
                        //    for (int rmv = 0; rmv < drc; rmv++)
                        //        oChildrenCIP1.Remove(0);
                        //}

                        //foreach (var item in analizTemizlik.cIP1s)
                        //{
                        //    oChildCIP1 = oChildrenCIP1.Add();

                        //    oChildCIP1.SetProperty("U_KontrolAdi", item.KontrolNoktasiAdi);

                        //    oChildCIP1.SetProperty("U_DegerKod", item.DegerKod);

                        //    oChildCIP1.SetProperty("U_Deger", item.Deger);
                        //}

                        oChildrenCIP2 = oGeneralData.Child("AIF_TEMIZLIK2");


                        if (oChildrenCIP2.Count > 0)
                        {
                            int drc = oChildrenCIP2.Count;
                            for (int rmv = 0; rmv < drc; rmv++)
                                oChildrenCIP2.Remove(0);
                        }

                        foreach (var item in analizTemizlik.cIP2s)
                        {
                            oChildCIP2 = oChildrenCIP2.Add();

                            oChildCIP2.SetProperty("U_KontrolAdi", item.KontrolNoktasiAdi);

                            oChildCIP2.SetProperty("U_AlkaliBasSaat", item.AlkaliBasSaat);

                            oChildCIP2.SetProperty("U_AlkaliSc", item.AlkaliSc);

                            oChildCIP2.SetProperty("U_AlkaliKonMs", item.AlkaliKonMs);

                            oChildCIP2.SetProperty("U_AlkaliKonYuzde", item.AlkaliKonYuzde);

                            oChildCIP2.SetProperty("U_AlkaliBitSaat", item.AlkaliBitSaat);

                            oChildCIP2.SetProperty("U_AsitBasSaat", item.AsitBasSaat);

                            oChildCIP2.SetProperty("U_AsitSc", item.AsitSc);

                            oChildCIP2.SetProperty("U_AsitKonMs", item.AsitKonMs);

                            oChildCIP2.SetProperty("U_AsitKonYuzde", item.AsitKonYuzde);

                            oChildCIP2.SetProperty("U_AsitBitSaat", item.AsitBitSaat);

                            oChildCIP2.SetProperty("U_TemizlikPers", item.TemizlikPers);

                            oChildCIP2.SetProperty("U_OzonitKontrol", item.OzonitKontrol);

                            oChildCIP2.SetProperty("U_DurulamaPh", item.DurulamaPh);

                            oChildCIP2.SetProperty("U_DezenPrsnl", item.DezenPrsnl);

                            oChildCIP2.SetProperty("U_HaslamaSc", item.HaslamaSc);

                            oChildCIP2.SetProperty("U_HaslamaPrsnl", item.HaslamaPrsnl);

                            oChildCIP2.SetProperty("U_KontrolPersonel", item.KontrolPersonel);

                            oChildCIP2.SetProperty("U_Yapildi", item.Yapildi);

                            oChildCIP2.SetProperty("U_Yapilmadi", item.Yapilmadi);

                        }

                        oChildrenAletEkipman = oGeneralData.Child("AIF_TEMIZLIK3");

                        if (oChildrenAletEkipman.Count > 0)
                        {
                            int drc = oChildrenAletEkipman.Count;
                            for (int rmv = 0; rmv < drc; rmv++)
                                oChildrenAletEkipman.Remove(0);
                        }

                        foreach (var item in analizTemizlik.aletEkipmanTemizliks)
                        {
                            oChildAletEkipman = oChildrenAletEkipman.Add();

                            oChildAletEkipman.SetProperty("U_AlanMakine", item.TemizlenenAlanMakine);
                            oChildAletEkipman.SetProperty("U_YapilmaSikligi", item.TemizlikYapilmaSikligi);
                            oChildAletEkipman.SetProperty("U_KimyasalAdi", item.KullanilanKimysalAdi);
                            oChildAletEkipman.SetProperty("U_KimyasalMiktari", item.KullanilanKimysalMiktar.ToString());
                            oChildAletEkipman.SetProperty("U_KontrolYapildi", item.TemizlikYapildi);
                            oChildAletEkipman.SetProperty("U_KontrolYapilmadi", item.TemizlikYapilmadi);
                            oChildAletEkipman.SetProperty("U_TemizlikYapanPrsnl", item.TemizlikYapanPersonel);
                            oChildAletEkipman.SetProperty("U_TemizlikKontPrsnl", item.TemizlikKontrolEdenPersonel);

                        } 
                    }
                    else
                    {
                        oGeneralData.SetProperty("U_YoneticiOnay", analizTemizlik.YoneticiOnayi);
                        DateTime dt2 = new DateTime(Convert.ToInt32(analizTemizlik.YoneticiOnayTarihi.Substring(0, 4)), Convert.ToInt32(analizTemizlik.YoneticiOnayTarihi.Substring(4, 2)), Convert.ToInt32(analizTemizlik.YoneticiOnayTarihi.Substring(6, 2)));
                        oGeneralData.SetProperty("U_YoneticiOnyTar", dt2.ToString());
                        oGeneralData.SetProperty("U_YoneticiAciklama", analizTemizlik.YoneticiOnayAciklama);
                    }


                    try
                    {
                        oGeneralService.Update(oGeneralData);
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
                        return new Response { Value = 0, Description = "Temizlik girişi güncellendi.", List = null };
                    }
                    catch (Exception ex)
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
                        return new Response { Value = -5300, Description = "Hata Kodu - 5300 Temizlik girişi güncellenirken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
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