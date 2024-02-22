using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddOrUpdateFSAPKY001
    {
        //commt
        public Response addOrUpdateFSAPKY001(FSAPKY001 fSAPKY001, string dbName, string mKodValue)
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

                oRS.DoQuery("Select * from \"@AIF_FSAPKY001\" WITH (NOLOCK) where \"U_PartiNo\" = '" + fSAPKY001.PartiNo + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChild_FSAPKY001_1;

                    GeneralDataCollection oChildren_FSAPKY001_1;

                    GeneralData oChild_FSAPKY001_2;

                    GeneralDataCollection oChildren_FSAPKY001_2;

                    oCompService = oCompany.GetCompanyService();

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_FSAPKY001");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralData.SetProperty("U_PartiNo", fSAPKY001.PartiNo.ToString());

                    oGeneralData.SetProperty("U_UrunKodu", fSAPKY001.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_UrunTanimi", fSAPKY001.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Aciklama", fSAPKY001.Aciklama.ToString());

                    if (fSAPKY001.Tarih != null && fSAPKY001.Tarih != "")
                    {
                        string tarih = fSAPKY001.Tarih;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_Tarih", dt);
                    }

                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    //oGeneralData.SetProperty("U_Tarih", dt);

                    oChildren_FSAPKY001_1 = oGeneralData.Child("AIF_FSAPKY001_1");

                    foreach (var item in fSAPKY001.fSAPKY001_1s)
                    {
                        oChild_FSAPKY001_1 = oChildren_FSAPKY001_1.Add();

                        oChild_FSAPKY001_1.SetProperty("U_BasSaati", item.BaslangicSaati);

                        oChild_FSAPKY001_1.SetProperty("U_PasSicaklik", item.PastorizasyonSicaklik);

                        oChild_FSAPKY001_1.SetProperty("U_PasCikis", item.PastorizasyonCikis);

                        oChild_FSAPKY001_1.SetProperty("U_HolderSuresi", item.HolderSuresi);

                        oChild_FSAPKY001_1.SetProperty("U_HmjBasinc", item.HomojenizasyonBasinc);

                        oChild_FSAPKY001_1.SetProperty("U_CigSutMik", item.CigSutMiktari);

                        oChild_FSAPKY001_1.SetProperty("U_CekilenKrema", item.CekilenKrema);

                        oChild_FSAPKY001_1.SetProperty("U_EklnKrema", item.EklenenKrema);

                        oChild_FSAPKY001_1.SetProperty("U_StandSutMik", item.StandartSutMiktari);

                        oChild_FSAPKY001_1.SetProperty("U_BitSaat", item.BitisSaati);

                        oChild_FSAPKY001_1.SetProperty("U_OprtAdi", item.OperatorAdi);
                    }

                    oChildren_FSAPKY001_2 = oGeneralData.Child("AIF_FSAPKY001_2");

                    foreach (var item in fSAPKY001.fSAPKY001_2s)
                    {
                        oChild_FSAPKY001_2 = oChildren_FSAPKY001_2.Add();

                        oChild_FSAPKY001_2.SetProperty("U_CigSutDepTnk", item.CigSutDepoTanki);

                        oChild_FSAPKY001_2.SetProperty("U_CigSutMik", item.CigSutMiktari);

                        oChild_FSAPKY001_2.SetProperty("U_CigSutPH", item.CigSutPH);

                        oChild_FSAPKY001_2.SetProperty("U_CigSutSH", item.CigSutSH);

                        oChild_FSAPKY001_2.SetProperty("U_CigSutKM", item.CigSutKM);

                        oChild_FSAPKY001_2.SetProperty("U_CigSutYag", item.CigSutYag);

                        oChild_FSAPKY001_2.SetProperty("U_OprtAdi", item.OperatorAdi);
                    }

                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_FSAPKY001\" WITH (NOLOCK)");

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
                        return new Response { Value = 0, Description = "Analiz girişi oluşturuldu..", List = null };
                    }
                    else
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5200, Description = "Hata Kodu - 5200 Analiz girişi oluşturulurken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                    }
                }
                else
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChild_FSAPKY001_1;

                    GeneralDataCollection oChildren_FSAPKY001_1;

                    GeneralData oChild_FSAPKY001_2;

                    GeneralDataCollection oChildren_FSAPKY001_2;

                    oCompService = oCompany.GetCompanyService();

                    GeneralDataParams oGeneralParams;

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_FSAPKY001");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    oGeneralData.SetProperty("U_PartiNo", fSAPKY001.PartiNo.ToString());

                    oGeneralData.SetProperty("U_UrunKodu", fSAPKY001.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_UrunTanimi", fSAPKY001.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Aciklama", fSAPKY001.Aciklama.ToString());

                    if (fSAPKY001.Tarih != null && fSAPKY001.Tarih != "")
                    {
                        string tarih = fSAPKY001.Tarih;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_Tarih", dt);
                    }

                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    //oGeneralData.SetProperty("U_Tarih", dt);

                    oChildren_FSAPKY001_1 = oGeneralData.Child("AIF_FSAPKY001_1");

                    if (oChildren_FSAPKY001_1.Count > 0)
                    {
                        int drc = oChildren_FSAPKY001_1.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY001_1.Remove(0);
                    }

                    foreach (var item in fSAPKY001.fSAPKY001_1s)
                    {
                        oChild_FSAPKY001_1 = oChildren_FSAPKY001_1.Add();

                        oChild_FSAPKY001_1.SetProperty("U_BasSaati", item.BaslangicSaati);

                        oChild_FSAPKY001_1.SetProperty("U_PasSicaklik", item.PastorizasyonSicaklik);

                        oChild_FSAPKY001_1.SetProperty("U_PasCikis", item.PastorizasyonCikis);

                        oChild_FSAPKY001_1.SetProperty("U_HolderSuresi", item.HolderSuresi);

                        oChild_FSAPKY001_1.SetProperty("U_HmjBasinc", item.HomojenizasyonBasinc);

                        oChild_FSAPKY001_1.SetProperty("U_CigSutMik", item.CigSutMiktari);

                        oChild_FSAPKY001_1.SetProperty("U_CekilenKrema", item.CekilenKrema);

                        oChild_FSAPKY001_1.SetProperty("U_EklnKrema", item.EklenenKrema);

                        oChild_FSAPKY001_1.SetProperty("U_StandSutMik", item.StandartSutMiktari);

                        oChild_FSAPKY001_1.SetProperty("U_BitSaat", item.BitisSaati);

                        oChild_FSAPKY001_1.SetProperty("U_OprtAdi", item.OperatorAdi);
                    }

                    oChildren_FSAPKY001_2 = oGeneralData.Child("AIF_FSAPKY001_2");

                    if (oChildren_FSAPKY001_2.Count > 0)
                    {
                        int drc = oChildren_FSAPKY001_2.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY001_2.Remove(0);
                    }

                    foreach (var item in fSAPKY001.fSAPKY001_2s)
                    {
                        oChild_FSAPKY001_2 = oChildren_FSAPKY001_2.Add();

                        oChild_FSAPKY001_2.SetProperty("U_CigSutDepTnk", item.CigSutDepoTanki);

                        oChild_FSAPKY001_2.SetProperty("U_CigSutMik", item.CigSutMiktari);

                        oChild_FSAPKY001_2.SetProperty("U_CigSutPH", item.CigSutPH);

                        oChild_FSAPKY001_2.SetProperty("U_CigSutSH", item.CigSutSH);

                        oChild_FSAPKY001_2.SetProperty("U_CigSutKM", item.CigSutKM);

                        oChild_FSAPKY001_2.SetProperty("U_CigSutYag", item.CigSutYag);

                        oChild_FSAPKY001_2.SetProperty("U_OprtAdi", item.OperatorAdi);
                    }

                    try
                    {
                        oGeneralService.Update(oGeneralData);
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = 0, Description = "Analiz girişi güncellendi.", List = null };
                    }
                    catch (Exception)
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5300, Description = "Hata Kodu - 5300 Analiz girişi güncellenirken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
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