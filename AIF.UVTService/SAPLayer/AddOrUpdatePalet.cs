using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AIF.UVTService.Models;

namespace UVTService.SAPLayer
{
    public class AddOrUpdatePalet
    {
        public Response addOrUpdatePalet(string dbName, PaletYapma paletYapma, string mKodValue)
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

                oRS.DoQuery("Select \"DocEntry\" from \"@AIF_WMS_PALET\" as T0 where T0.\"U_PaletNo\" = '" + paletYapma.PaletNumarasi + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChildSatir1;

                    GeneralDataCollection oChildrenSatir1;

                    GeneralData oChildSatirParti;

                    GeneralDataCollection oChildrenSatirParti;

                    oCompService = oCompany.GetCompanyService();

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_WMS_PALET");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralData.SetProperty("U_UretimFisNo", paletYapma.UretimFisNo == null ? "" : paletYapma.UretimFisNo);

                    oGeneralData.SetProperty("U_PaletNo", paletYapma.PaletNumarasi == null ? "" : paletYapma.PaletNumarasi);

                    oGeneralData.SetProperty("U_Durum", paletYapma.Durum == null ? "" : paletYapma.Durum.ToString());

                    oGeneralData.SetProperty("U_ToplamKap", paletYapma.ToplamKap == null ? 0 : paletYapma.ToplamKap);

                    oGeneralData.SetProperty("U_NetKilo", paletYapma.NetKilo == null ? 0 : paletYapma.NetKilo);

                    oGeneralData.SetProperty("U_BrutKilo", paletYapma.BrutKilo == null ? 0 : paletYapma.BrutKilo);

                    oGeneralData.SetProperty("U_SonDepKod", paletYapma.SonGorulenDepoKodu == null ? "" : paletYapma.SonGorulenDepoKodu.ToString());

                    oGeneralData.SetProperty("U_SonDepAd", paletYapma.SonGorulenDepoAdi == null ? "" : paletYapma.SonGorulenDepoAdi.ToString());

                    oGeneralData.SetProperty("U_SonDepYeri", paletYapma.SonGorulenDepoYeriId == null ? "" : paletYapma.SonGorulenDepoYeriId.ToString());

                    oGeneralData.SetProperty("U_SonDepYeriAd", paletYapma.SonGorulenDepoYeriAdi == null ? "" : paletYapma.SonGorulenDepoYeriAdi.ToString());

                    oGeneralData.SetProperty("U_MvctDepKod", paletYapma.MevcutDepoKodu == null ? "" : paletYapma.MevcutDepoKodu.ToString());

                    oGeneralData.SetProperty("U_MvctDepAd", paletYapma.MevcutDepoAdi == null ? "" : paletYapma.MevcutDepoAdi.ToString());

                    oGeneralData.SetProperty("U_MvctDepYeri", paletYapma.MevcutDepoYeriId == null ? "" : paletYapma.MevcutDepoYeriId.ToString());

                    oGeneralData.SetProperty("U_MvctDepYeriAd", paletYapma.MevcutDepoYeriAdi == null ? "" : paletYapma.MevcutDepoYeriAdi.ToString());

                    oChildrenSatir1 = oGeneralData.Child("AIF_WMS_PALET1");

                    foreach (var item in paletYapma.paletYapmaDetays)
                    {
                        oChildSatir1 = oChildrenSatir1.Add();

                        oChildSatir1.SetProperty("U_Barkod", item.Barkod == null ? "" : item.Barkod);

                        oChildSatir1.SetProperty("U_MuhKatalogNo", item.MuhatapKatalogNo == null ? "" : item.MuhatapKatalogNo);

                        oChildSatir1.SetProperty("U_KalemKodu", item.KalemKodu == null ? "" : item.KalemKodu);

                        oChildSatir1.SetProperty("U_Tanim", item.KalemTanimi == null ? "" : item.KalemTanimi);

                        oChildSatir1.SetProperty("U_Miktar", item.Quantity == null ? 0 : item.Quantity);

                        if (item.SiparisNumarasi != null && item.SiparisNumarasi != -1)
                        {
                            oChildSatir1.SetProperty("U_SiparisNo", item.SiparisNumarasi);
                        }

                        if (item.SiparisSatirNo != null && item.SiparisSatirNo != -1)
                        {
                            oChildSatir1.SetProperty("U_SipSatirNo", item.SiparisSatirNo);
                        }


                        oChildSatir1.SetProperty("U_CekmeNo", item.CekmeNo == null ? -1 : item.CekmeNo);

                        oChildSatir1.SetProperty("U_Kaynak", item.Kaynak == null ? "" : item.Kaynak);

                        oChildSatir1.SetProperty("U_DepoKodu", item.DepoKodu == null ? "" : item.DepoKodu);

                        oChildSatir1.SetProperty("U_DepoAdi", item.DepoAdi == null ? "" : item.DepoAdi);

                        oChildSatir1.SetProperty("U_DepoYeriId", item.DepoYeriId == null ? "" : item.DepoYeriId);

                        oChildSatir1.SetProperty("U_DepoYeriAdi", item.DepoYeriAdi == null ? "" : item.DepoYeriAdi);

                        oChildSatir1.SetProperty("U_Guid", item.guid == null ? "" : item.guid);

                        oChildSatir1.SetProperty("U_Partili", item.partili == null ? "" : item.partili);

                        oChildSatir1.SetProperty("U_Serili", item.serili == null ? "" : item.serili);

                        oChildSatir1.SetProperty("U_Transfer", item.Transfer == null ? "" : item.Transfer);


                        //if (paletYapmadaDepoYeriSecilsin == "Y")
                        //{
                        oChildrenSatirParti = oGeneralData.Child("AIF_WMS_PALET2");

                        foreach (var itemParti in item.PaletYapmaPartilers)
                        {
                            oChildSatirParti = oChildrenSatirParti.Add();

                            oChildSatirParti.SetProperty("U_Barkod", itemParti.Barkod == null ? "" : itemParti.Barkod);

                            oChildSatirParti.SetProperty("U_KalemKodu", itemParti.KalemKodu == null ? "" : itemParti.KalemKodu);

                            oChildSatirParti.SetProperty("U_Tanim", itemParti.KalemTanimi == null ? "" : itemParti.KalemTanimi);

                            oChildSatirParti.SetProperty("U_PartiNo", itemParti.PartiNumarasi == null ? "" : itemParti.PartiNumarasi);

                            oChildSatirParti.SetProperty("U_Miktar", itemParti.Miktar == null ? 0 : itemParti.Miktar);

                            oChildSatirParti.SetProperty("U_DepoKodu", itemParti.DepoKodu == null ? "" : itemParti.DepoKodu);

                            oChildSatirParti.SetProperty("U_DepoAdi", itemParti.DepoAdi == null ? "" : itemParti.DepoAdi);

                            oChildSatirParti.SetProperty("U_DepoYeriId", itemParti.DepoYeriId == null ? "" : itemParti.DepoYeriId);

                            oChildSatirParti.SetProperty("U_DepoYeriAdi", itemParti.DepoYeriAdi == null ? "" : itemParti.DepoYeriAdi);

                            oChildSatirParti.SetProperty("U_Guid", itemParti.guid == null ? "" : itemParti.guid);
                        }
                        //}
                    }


                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_WMS_PALET\"");

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
                        return new Response { Value = 0, Description = "Analiz girişi oluşturuldu..", List = null };
                    }
                    else
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
                        return new Response { Value = -5200, Description = "Hata Kodu - 5200 Analiz girişi oluşturulurken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                    }
                }
                else
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChildSatir1;

                    GeneralDataCollection oChildrenSatir1;

                    GeneralData oChildSatirParti;

                    GeneralDataCollection oChildrenSatirParti;

                    oCompService = oCompany.GetCompanyService();

                    GeneralDataParams oGeneralParams;

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_WMS_PALET");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    oGeneralData.SetProperty("U_PaletNo", paletYapma.PaletNumarasi == null ? "" : paletYapma.PaletNumarasi);

                    oGeneralData.SetProperty("U_Durum", paletYapma.Durum == null ? "" : paletYapma.Durum.ToString());

                    oGeneralData.SetProperty("U_ToplamKap", paletYapma.ToplamKap == null ? 0 : paletYapma.ToplamKap);

                    oGeneralData.SetProperty("U_NetKilo", paletYapma.NetKilo == null ? 0 : paletYapma.NetKilo);

                    oGeneralData.SetProperty("U_BrutKilo", paletYapma.BrutKilo == null ? 0 : paletYapma.BrutKilo);

                    oGeneralData.SetProperty("U_SonDepKod", paletYapma.SonGorulenDepoKodu == null ? "" : paletYapma.SonGorulenDepoKodu.ToString());

                    oGeneralData.SetProperty("U_SonDepAd", paletYapma.SonGorulenDepoAdi == null ? "" : paletYapma.SonGorulenDepoAdi.ToString());

                    oGeneralData.SetProperty("U_SonDepYeri", paletYapma.SonGorulenDepoYeriId == null ? "" : paletYapma.SonGorulenDepoYeriId.ToString());

                    oGeneralData.SetProperty("U_SonDepYeriAd", paletYapma.SonGorulenDepoYeriAdi == null ? "" : paletYapma.SonGorulenDepoYeriAdi.ToString());

                    oGeneralData.SetProperty("U_MvctDepKod", paletYapma.MevcutDepoKodu == null ? "" : paletYapma.MevcutDepoKodu.ToString());

                    oGeneralData.SetProperty("U_MvctDepAd", paletYapma.MevcutDepoAdi == null ? "" : paletYapma.MevcutDepoAdi.ToString());

                    oGeneralData.SetProperty("U_MvctDepYeri", paletYapma.MevcutDepoYeriId == null ? "" : paletYapma.MevcutDepoYeriId.ToString());

                    oGeneralData.SetProperty("U_MvctDepYeriAd", paletYapma.MevcutDepoYeriAdi == null ? "" : paletYapma.MevcutDepoYeriAdi.ToString());
                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    oChildrenSatir1 = oGeneralData.Child("AIF_WMS_PALET1");

                    if (oChildrenSatir1.Count > 0)
                    {
                        int drc = oChildrenSatir1.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenSatir1.Remove(0);
                    }

                    oChildrenSatirParti = oGeneralData.Child("AIF_WMS_PALET2");

                    if (oChildrenSatirParti.Count > 0)
                    {
                        int drc = oChildrenSatirParti.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenSatirParti.Remove(0);
                    }



                    foreach (var item in paletYapma.paletYapmaDetays)
                    {
                        oChildSatir1 = oChildrenSatir1.Add();

                        oChildSatir1.SetProperty("U_Barkod", item.Barkod == null ? "" : item.Barkod);

                        oChildSatir1.SetProperty("U_MuhKatalogNo", item.MuhatapKatalogNo == null ? "" : item.MuhatapKatalogNo);

                        oChildSatir1.SetProperty("U_KalemKodu", item.KalemKodu == null ? "" : item.KalemKodu);

                        oChildSatir1.SetProperty("U_Tanim", item.KalemTanimi == null ? "" : item.KalemTanimi);

                        oChildSatir1.SetProperty("U_Miktar", item.Quantity == null ? 0 : item.Quantity);

                        if (item.SiparisNumarasi != null && item.SiparisNumarasi != -1)
                        {
                            oChildSatir1.SetProperty("U_SiparisNo", item.SiparisNumarasi);
                        }

                        if (item.SiparisSatirNo != null && item.SiparisSatirNo != -1)
                        {
                            oChildSatir1.SetProperty("U_SipSatirNo", item.SiparisSatirNo);
                        }


                        oChildSatir1.SetProperty("U_CekmeNo", item.CekmeNo == null ? -1 : item.CekmeNo);

                        oChildSatir1.SetProperty("U_Kaynak", item.Kaynak == null ? "" : item.Kaynak);

                        oChildSatir1.SetProperty("U_DepoKodu", item.DepoKodu == null ? "" : item.DepoKodu);

                        oChildSatir1.SetProperty("U_DepoAdi", item.DepoAdi == null ? "" : item.DepoAdi);

                        oChildSatir1.SetProperty("U_DepoYeriId", item.DepoYeriId == null ? "" : item.DepoYeriId);

                        oChildSatir1.SetProperty("U_DepoYeriAdi", item.DepoYeriAdi == null ? "" : item.DepoYeriAdi);

                        oChildSatir1.SetProperty("U_Guid", item.guid == null ? "" : item.guid);

                        oChildSatir1.SetProperty("U_Partili", item.partili == null ? "" : item.partili);

                        oChildSatir1.SetProperty("U_Serili", item.serili == null ? "" : item.serili);

                        oChildSatir1.SetProperty("U_Transfer", item.Transfer == null ? "" : item.Transfer);

                        //if (paletYapmadaDepoYeriSecilsin == "Y")
                        //{

                        foreach (var itemParti in item.PaletYapmaPartilers)
                        {
                            oChildSatirParti = oChildrenSatirParti.Add();


                            oChildSatirParti.SetProperty("U_Barkod", itemParti.Barkod == null ? "" : itemParti.Barkod);

                            oChildSatirParti.SetProperty("U_KalemKodu", itemParti.KalemKodu == null ? "" : itemParti.KalemKodu);

                            oChildSatirParti.SetProperty("U_Tanim", itemParti.KalemTanimi == null ? "" : itemParti.KalemTanimi);

                            oChildSatirParti.SetProperty("U_PartiNo", itemParti.PartiNumarasi == null ? "" : itemParti.PartiNumarasi);

                            oChildSatirParti.SetProperty("U_Miktar", itemParti.Miktar == null ? 0 : itemParti.Miktar);

                            oChildSatirParti.SetProperty("U_DepoKodu", itemParti.DepoKodu == null ? "" : itemParti.DepoKodu);

                            oChildSatirParti.SetProperty("U_DepoAdi", itemParti.DepoAdi == null ? "" : itemParti.DepoAdi);

                            oChildSatirParti.SetProperty("U_DepoYeriId", itemParti.DepoYeriId == null ? "" : itemParti.DepoYeriId);

                            oChildSatirParti.SetProperty("U_DepoYeriAdi", itemParti.DepoYeriAdi == null ? "" : itemParti.DepoYeriAdi);

                            oChildSatirParti.SetProperty("U_Guid", itemParti.guid == null ? "" : itemParti.guid);

                            oChildSatirParti.SetProperty("U_Transfer", item.Transfer == null ? "" : item.Transfer); //SAtır transfer ediliyorsa partisi de transfer edilmiş olur.
                        }
                    }
                    try
                    {
                        oGeneralService.Update(oGeneralData);
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
                        return new Response { Value = 0, Description = "Analiz girişi güncellendi.", List = null };
                    }
                    catch (Exception)
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
                        return new Response { Value = -5300, Description = "Hata Kodu - 5300 Analiz girişi güncellenirken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
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