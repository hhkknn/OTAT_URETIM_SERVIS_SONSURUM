using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddOrUpdateTazePeynirSalamuraSu
    {
        public Response addOrUpdateTazePeynirSalamuraSu(TazePeynirSalamuraSu tazePeynirSalamuraSu, string dbName, string mKodValue)
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

                oRS.DoQuery("Select * from \"@AIF_TAZEPEYSALSUYU\" WITH (NOLOCK) where \"U_PartiNo\" = '" + tazePeynirSalamuraSu.PartiNo + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChildProsesOzellikleri1;

                    GeneralDataCollection oChildrenProsesOzellikleri1;

                    GeneralData oChildProsesOzellikleri2;

                    GeneralDataCollection oChildrenProsesOzellikleri2;

                    GeneralData oChildSalamuraOzellikleri;

                    GeneralDataCollection oChildrenSalamuraOzellikleri;

                    GeneralData oChildSarfMalzemeKullanim;

                    GeneralDataCollection oChildrenSarfMalzemeKullanim;
                     
                    oCompService = oCompany.GetCompanyService();

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_TAZEPEYSALSUYU");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralData.SetProperty("U_PartiNo", tazePeynirSalamuraSu.PartiNo.ToString());

                    oGeneralData.SetProperty("U_KalemKodu", tazePeynirSalamuraSu.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_KalemTanimi", tazePeynirSalamuraSu.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Aciklama", tazePeynirSalamuraSu.Aciklama.ToString());


                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    //oGeneralData.SetProperty("U_Tarih", dt);

                    oChildrenProsesOzellikleri1 = oGeneralData.Child("AIF_TAZEPEYSALSUYU1");

                    foreach (var item in tazePeynirSalamuraSu.salamuraProsesOzellikleri1s)
                    {
                        oChildProsesOzellikleri1 = oChildrenProsesOzellikleri1.Add();

                        //oChildProsesOzellikleri1.SetProperty("U_PartiNo", item.PartiNo);

                        oChildProsesOzellikleri1.SetProperty("U_SalHazSrmlu", item.SalamuraHazirlayanSorumlu);

                        oChildProsesOzellikleri1.SetProperty("U_OprsynBasSaat", item.OperasyonBaslangicSaati);

                        oChildProsesOzellikleri1.SetProperty("U_PastSicaklik", item.PastorizasyonSicakligi);

                        oChildProsesOzellikleri1.SetProperty("U_PastBasSaat", item.PastorizasyonBaslangicSaati);

                        oChildProsesOzellikleri1.SetProperty("U_PastBitSaat", item.PastorizasyonBitisSaati);

                        oChildProsesOzellikleri1.SetProperty("U_SalTnkFiltKnt", item.SalamuraTankFiltreKontrol);

                        oChildProsesOzellikleri1.SetProperty("U_HazSalMiktar", item.HazirlananSalamuraMiktari); 

                        oChildProsesOzellikleri1.SetProperty("U_OprsynBitSaat", item.OperasyonBitisSaati); 
                    }

                    oChildrenProsesOzellikleri2 = oGeneralData.Child("AIF_TAZEPEYSALSUYU2");

                    foreach (var item in tazePeynirSalamuraSu.salamuraProsesOzellikleri2s)
                    {
                        oChildProsesOzellikleri2 = oChildrenProsesOzellikleri2.Add();

                        oChildProsesOzellikleri2.SetProperty("U_PastSuresi", item.PastorizasyonSuresi);  
                        oChildProsesOzellikleri2.SetProperty("U_ToplamGecenSure", item.ToplamGecenSure); 
                    }

                    oChildrenSalamuraOzellikleri = oGeneralData.Child("AIF_TAZEPEYSALSUYU3");

                    foreach (var item in tazePeynirSalamuraSu.salamuraOzellikleris)
                    {
                        oChildSalamuraOzellikleri = oChildrenSalamuraOzellikleri.Add();

                        oChildSalamuraOzellikleri.SetProperty("U_HamSarfTopKg", item.KullanilanHammeddeToplam);

                        oChildSalamuraOzellikleri.SetProperty("U_BomeDegeri", item.BomeDegeri);

                        oChildSalamuraOzellikleri.SetProperty("U_PhDegeri", item.PhDegeri); 

                    }

                    oChildrenSarfMalzemeKullanim = oGeneralData.Child("AIF_TAZEPEYSALSUYU4");

                    foreach (var item in tazePeynirSalamuraSu.salamuraSarfMalzemeKullanims)
                    {
                        oChildSarfMalzemeKullanim = oChildrenSarfMalzemeKullanim.Add();

                        oChildSarfMalzemeKullanim.SetProperty("U_MalzemeAdi", item.MalzemeAdi);

                        oChildSarfMalzemeKullanim.SetProperty("U_MalMarkaTedarikci", item.MalzemeMarkaTedarikcisi);

                        oChildSarfMalzemeKullanim.SetProperty("U_PartiNo", item.SarfMalzemePartiNo);

                        oChildSarfMalzemeKullanim.SetProperty("U_Miktar", item.Miktar);

                        oChildSarfMalzemeKullanim.SetProperty("U_Birim", item.Birim);
                    }


                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_TAZEPEYSALSUYU\" WITH (NOLOCK)");

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
                        return new Response { Value = 0, Description = "Taze Peynir Salamura Suyu girişi oluşturuldu.", List = null };
                    }
                    else

                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5200, Description = "Hata Kodu - 5200 Taze Peynir Salamura Suyu girişi oluşturulurken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
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

                    GeneralData oChildSalamuraOzellikleri;

                    GeneralDataCollection oChildrenSalamuraOzellikleri;

                    GeneralData oChildSarfMalzemeKullanim;

                    GeneralDataCollection oChildrenSarfMalzemeKullanim; 

                    oCompService = oCompany.GetCompanyService();

                    GeneralDataParams oGeneralParams;

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_TAZEPEYSALSUYU");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    oGeneralData.SetProperty("U_PartiNo", tazePeynirSalamuraSu.PartiNo.ToString());

                    oGeneralData.SetProperty("U_KalemKodu", tazePeynirSalamuraSu.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_KalemTanimi", tazePeynirSalamuraSu.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Aciklama", tazePeynirSalamuraSu.Aciklama.ToString());


                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    //oGeneralData.SetProperty("U_Tarih", dt);

                    oChildrenProsesOzellikleri1 = oGeneralData.Child("AIF_TAZEPEYSALSUYU1");

                    if (oChildrenProsesOzellikleri1.Count > 0)
                    {
                        int drc = oChildrenProsesOzellikleri1.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenProsesOzellikleri1.Remove(0);
                    }

                    foreach (var item in tazePeynirSalamuraSu.salamuraProsesOzellikleri1s)
                    {
                        oChildProsesOzellikleri1 = oChildrenProsesOzellikleri1.Add();

                        //oChildProsesOzellikleri1.SetProperty("U_PartiNo", item.PartiNo);

                        oChildProsesOzellikleri1.SetProperty("U_SalHazSrmlu", item.SalamuraHazirlayanSorumlu);

                        oChildProsesOzellikleri1.SetProperty("U_OprsynBasSaat", item.OperasyonBaslangicSaati);

                        oChildProsesOzellikleri1.SetProperty("U_PastSicaklik", item.PastorizasyonSicakligi);

                        oChildProsesOzellikleri1.SetProperty("U_PastBasSaat", item.PastorizasyonBaslangicSaati);

                        oChildProsesOzellikleri1.SetProperty("U_PastBitSaat", item.PastorizasyonBitisSaati);

                        oChildProsesOzellikleri1.SetProperty("U_SalTnkFiltKnt", item.SalamuraTankFiltreKontrol);

                        oChildProsesOzellikleri1.SetProperty("U_HazSalMiktar", item.HazirlananSalamuraMiktari);

                        oChildProsesOzellikleri1.SetProperty("U_OprsynBitSaat", item.OperasyonBitisSaati);
                    }

                    oChildrenProsesOzellikleri2 = oGeneralData.Child("AIF_TAZEPEYSALSUYU2");

                    if (oChildrenProsesOzellikleri2.Count > 0)
                    {
                        int drc = oChildrenProsesOzellikleri2.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenProsesOzellikleri2.Remove(0);
                    }

                    foreach (var item in tazePeynirSalamuraSu.salamuraProsesOzellikleri2s)
                    {
                        oChildProsesOzellikleri2 = oChildrenProsesOzellikleri2.Add();

                        oChildProsesOzellikleri2.SetProperty("U_PastSuresi", item.PastorizasyonSuresi);
                        oChildProsesOzellikleri2.SetProperty("U_ToplamGecenSure", item.ToplamGecenSure);
                    }

                    oChildrenSalamuraOzellikleri = oGeneralData.Child("AIF_TAZEPEYSALSUYU3");

                    if (oChildrenSalamuraOzellikleri.Count > 0)
                    {
                        int drc = oChildrenSalamuraOzellikleri.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenSalamuraOzellikleri.Remove(0);
                    }

                    foreach (var item in tazePeynirSalamuraSu.salamuraOzellikleris)
                    {
                        oChildSalamuraOzellikleri = oChildrenSalamuraOzellikleri.Add();

                        oChildSalamuraOzellikleri.SetProperty("U_HamSarfTopKg", item.KullanilanHammeddeToplam);

                        oChildSalamuraOzellikleri.SetProperty("U_BomeDegeri", item.BomeDegeri);

                        oChildSalamuraOzellikleri.SetProperty("U_PhDegeri", item.PhDegeri);

                    }

                    oChildrenSarfMalzemeKullanim = oGeneralData.Child("AIF_TAZEPEYSALSUYU4");
                     
                    if (oChildrenSarfMalzemeKullanim.Count > 0)
                    {
                        int drc = oChildrenSarfMalzemeKullanim.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenSarfMalzemeKullanim.Remove(0);
                    }

                    foreach (var item in tazePeynirSalamuraSu.salamuraSarfMalzemeKullanims)
                    {
                        oChildSarfMalzemeKullanim = oChildrenSarfMalzemeKullanim.Add();

                        oChildSarfMalzemeKullanim.SetProperty("U_MalzemeAdi", item.MalzemeAdi);

                        oChildSarfMalzemeKullanim.SetProperty("U_MalMarkaTedarikci", item.MalzemeMarkaTedarikcisi);

                        oChildSarfMalzemeKullanim.SetProperty("U_PartiNo", item.SarfMalzemePartiNo);

                        oChildSarfMalzemeKullanim.SetProperty("U_Miktar", item.Miktar);

                        oChildSarfMalzemeKullanim.SetProperty("U_Birim", item.Birim);
                    }

                    try
                    {
                        oGeneralService.Update(oGeneralData);
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = 0, Description = "Taze Peynir Salamura Suyu girişi güncellendi.", List = null };
                    }
                    catch (Exception)
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5300, Description = "Hata Kodu - 5300 Taze Peynir Salamura Suyu girişi güncellenirken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
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