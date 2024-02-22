using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddOrUpdateTelemeAnalizTakibi
    {
        public Response addOrUpdateTelemeAnalizTakibi(TelemeAnalizTakibi telemeAnalizTakibi, string dbName, string mKodValue)
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

                oRS.DoQuery("Select * from \"@AIF_TLM_ANALIZ\" WITH (NOLOCK) where \"U_PartiNo\" = '" + telemeAnalizTakibi.PartiNo + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChildSutOzellikleri;

                    GeneralDataCollection oChildrenSutOzellikleri;

                    GeneralData oChildProsesOzellikleri;

                    GeneralDataCollection oChildrenProsesOzellikleri;

                    GeneralData oChildSonUrunOzellikleri;

                    GeneralDataCollection oChildrenSonUrunOzellikleri;

                    oCompService = oCompany.GetCompanyService();

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_TLM_ANALIZ");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralData.SetProperty("U_PartiNo", telemeAnalizTakibi.PartiNo);

                    oGeneralData.SetProperty("U_UrunKodu", telemeAnalizTakibi.UrunKodu);

                    oGeneralData.SetProperty("U_UrunTanimi", telemeAnalizTakibi.UrunTanimi);

                    oGeneralData.SetProperty("U_Aciklama", telemeAnalizTakibi.Aciklama);

                    DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    oGeneralData.SetProperty("U_Tarih", dt);

                    oChildrenSutOzellikleri = oGeneralData.Child("AIF_TLM_ANALIZ1");

                    foreach (var item in telemeAnalizTakibi.SutOzellikleri)
                    {
                        oChildSutOzellikleri = oChildrenSutOzellikleri.Add();

                        oChildSutOzellikleri.SetProperty("U_TelemeTuru", item.TelemeTuru);

                        oChildSutOzellikleri.SetProperty("U_GorevliOperator", item.GorevliOperator);

                        oChildSutOzellikleri.SetProperty("U_GorevliOperatorAdi", item.GorevliOperatorAdi);

                        oChildSutOzellikleri.SetProperty("U_NetSutMiktar", item.NetSutMiktari);

                        oChildSutOzellikleri.SetProperty("U_SutKaynagi", item.SutunKaynagi);

                        oChildSutOzellikleri.SetProperty("U_SutBicSevFark", item.SutunBicakSeviyesindenFarki);

                        oChildSutOzellikleri.SetProperty("U_PastorSicakligi", item.PastorSicakligi);

                        oChildSutOzellikleri.SetProperty("U_KuruMadde", item.KuruMadde);

                        oChildSutOzellikleri.SetProperty("U_Yag", item.Yag);

                        oChildSutOzellikleri.SetProperty("U_PH", item.PH);

                        oChildSutOzellikleri.SetProperty("U_SH", item.SH);

                        oChildSutOzellikleri.SetProperty("U_Protein", item.Protein);
                    }

                    oChildrenProsesOzellikleri = oGeneralData.Child("AIF_TLM_ANALIZ2");

                    foreach (var item in telemeAnalizTakibi.ProsesOzellikleri)
                    {
                        oChildProsesOzellikleri = oChildrenProsesOzellikleri.Add();

                        oChildProsesOzellikleri.SetProperty("U_MayalamaSicakligi", item.MayalamaSicakigi);

                        oChildProsesOzellikleri.SetProperty("U_MayalamaSuresi", item.MayalamaSuresi);

                        oChildProsesOzellikleri.SetProperty("U_CedarlamaSicakligi", item.CedarlamaSicakligi);

                        oChildProsesOzellikleri.SetProperty("U_CedarlamaSuresi", item.CedarlamaSuresi);

                        oChildProsesOzellikleri.SetProperty("U_TelemeIndPH", item.TelemeIndirildigindekiPH);
                    }

                    oChildrenSonUrunOzellikleri = oGeneralData.Child("AIF_TLM_ANALIZ3");

                    foreach (var item in telemeAnalizTakibi.SonUrunOzellikleri)
                    {
                        oChildSonUrunOzellikleri = oChildrenSonUrunOzellikleri.Add();

                        oChildSonUrunOzellikleri.SetProperty("U_KuruMadde", item.KuruMadde);

                        oChildSonUrunOzellikleri.SetProperty("U_Yag", item.Yag);

                        oChildSonUrunOzellikleri.SetProperty("U_PH", item.PH);

                        oChildSonUrunOzellikleri.SetProperty("U_SH", item.SH);

                        oChildSonUrunOzellikleri.SetProperty("U_PasBrix", item.PasBrix);

                        oChildSonUrunOzellikleri.SetProperty("U_TelemeMiktari", item.UretilenTLMMiktari);

                        oChildSonUrunOzellikleri.SetProperty("U_RandimanDegeri", item.RandimanDegeri);
                    }

                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_TLM_ANALIZ\" WITH (NOLOCK)");

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
                        return new Response { Value = 0, Description = "Teleme Analiz Takibi girişi oluşturuldu..", List = null };
                    }
                    else

                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5200, Description = "Hata Kodu - 5200 Teleme Analiz Takibi girişi oluşturulurken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                    }
                }
                else
                {
                    CompanyService oCompService;
                    oCompService = oCompany.GetCompanyService();

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralDataParams oGeneralParams;

                    GeneralData oChildSutOzellikleri;

                    GeneralDataCollection oChildrenSutOzellikleri;

                    GeneralData oChildProsesOzellikleri;

                    GeneralDataCollection oChildrenProsesOzellikleri;

                    GeneralData oChildSonUrunOzellikleri;

                    GeneralDataCollection oChildrenSonUrunOzellikleri;

                    oGeneralService = oCompService.GetGeneralService("AIF_TLM_ANALIZ");

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    oGeneralData.SetProperty("U_PartiNo", telemeAnalizTakibi.PartiNo);

                    oGeneralData.SetProperty("U_UrunKodu", telemeAnalizTakibi.UrunKodu);

                    oGeneralData.SetProperty("U_UrunTanimi", telemeAnalizTakibi.UrunTanimi);

                    oGeneralData.SetProperty("U_Aciklama", telemeAnalizTakibi.Aciklama);

                    DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    oGeneralData.SetProperty("U_Tarih", dt);

                    oChildrenSutOzellikleri = oGeneralData.Child("AIF_TLM_ANALIZ1");

                    if (oChildrenSutOzellikleri.Count > 0)
                    {
                        int drc = oChildrenSutOzellikleri.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenSutOzellikleri.Remove(0);
                    }

                    foreach (var item in telemeAnalizTakibi.SutOzellikleri)
                    {
                        oChildSutOzellikleri = oChildrenSutOzellikleri.Add();

                        oChildSutOzellikleri.SetProperty("U_TelemeTuru", item.TelemeTuru);

                        oChildSutOzellikleri.SetProperty("U_GorevliOperator", item.GorevliOperator);

                        oChildSutOzellikleri.SetProperty("U_GorevliOperatorAdi", item.GorevliOperatorAdi);

                        oChildSutOzellikleri.SetProperty("U_NetSutMiktar", item.NetSutMiktari);

                        oChildSutOzellikleri.SetProperty("U_SutKaynagi", item.SutunKaynagi);

                        oChildSutOzellikleri.SetProperty("U_SutBicSevFark", item.SutunBicakSeviyesindenFarki);

                        oChildSutOzellikleri.SetProperty("U_PastorSicakligi", item.PastorSicakligi);

                        oChildSutOzellikleri.SetProperty("U_KuruMadde", item.KuruMadde);

                        oChildSutOzellikleri.SetProperty("U_Yag", item.Yag);

                        oChildSutOzellikleri.SetProperty("U_PH", item.PH);

                        oChildSutOzellikleri.SetProperty("U_SH", item.SH);

                        oChildSutOzellikleri.SetProperty("U_Protein", item.Protein);
                    }

                    oChildrenProsesOzellikleri = oGeneralData.Child("AIF_TLM_ANALIZ2");

                    if (oChildrenProsesOzellikleri.Count > 0)
                    {
                        int drc = oChildrenProsesOzellikleri.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenProsesOzellikleri.Remove(0);
                    }

                    foreach (var item in telemeAnalizTakibi.ProsesOzellikleri)
                    {
                        oChildProsesOzellikleri = oChildrenProsesOzellikleri.Add();

                        oChildProsesOzellikleri.SetProperty("U_MayalamaSicakligi", item.MayalamaSicakigi);

                        oChildProsesOzellikleri.SetProperty("U_MayalamaSuresi", item.MayalamaSuresi);

                        oChildProsesOzellikleri.SetProperty("U_CedarlamaSicakligi", item.CedarlamaSicakligi);

                        oChildProsesOzellikleri.SetProperty("U_CedarlamaSuresi", item.CedarlamaSuresi);

                        oChildProsesOzellikleri.SetProperty("U_TelemeIndPH", item.TelemeIndirildigindekiPH);
                    }

                    oChildrenSonUrunOzellikleri = oGeneralData.Child("AIF_TLM_ANALIZ3");

                    if (oChildrenSonUrunOzellikleri.Count > 0)
                    {
                        int drc = oChildrenSonUrunOzellikleri.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenSonUrunOzellikleri.Remove(0);
                    }

                    foreach (var item in telemeAnalizTakibi.SonUrunOzellikleri)
                    {
                        oChildSonUrunOzellikleri = oChildrenSonUrunOzellikleri.Add();

                        oChildSonUrunOzellikleri.SetProperty("U_KuruMadde", item.KuruMadde);

                        oChildSonUrunOzellikleri.SetProperty("U_Yag", item.Yag);

                        oChildSonUrunOzellikleri.SetProperty("U_PH", item.PH);

                        oChildSonUrunOzellikleri.SetProperty("U_SH", item.SH);

                        oChildSonUrunOzellikleri.SetProperty("U_PasBrix", item.PasBrix);

                        oChildSonUrunOzellikleri.SetProperty("U_TelemeMiktari", item.UretilenTLMMiktari);

                        oChildSonUrunOzellikleri.SetProperty("U_RandimanDegeri", item.RandimanDegeri);
                    }

                    try
                    {
                        oGeneralService.Update(oGeneralData);
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = 0, Description = "Teleme Analiz Takibi girişi güncellendi.", List = null };
                    }
                    catch (Exception)
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5300, Description = "Hata Kodu - 5300 Teleme Analiz Takibi girişi güncellenirken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
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