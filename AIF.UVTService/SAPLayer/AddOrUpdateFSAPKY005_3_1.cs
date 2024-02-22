using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddOrUpdateFSAPKY005_3_1
    {
        public Response addOrUpdateFSAPKY005_3_1(FSAPKY005_3_1 fSAPKY005_3_1, string dbName, string mKodValue)
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

                oRS.DoQuery("Select * from \"@AIF_FSAPKY005_3_1\" WITH (NOLOCK) where \"U_PartiNo\" = '" + fSAPKY005_3_1.PartiNo + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChild_FSAPKY005_3_1_1;

                    GeneralDataCollection oChildren_FSAPKY005_3_1_1;

                    GeneralData oChild_FSAPKY005_3_1_2;

                    GeneralDataCollection oChildren_FSAPKY005_3_1_2;

                    GeneralData oChild_FSAPKY005_3_2_1;

                    GeneralDataCollection oChildren_FSAPKY005_3_2_1;

                    GeneralData oChild_FSAPKY005_3_2_2;

                    GeneralDataCollection oChildren_FSAPKY005_3_2_2; 

                    oCompService = oCompany.GetCompanyService();

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_FSAPKY005_3_1");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralData.SetProperty("U_PartiNo", fSAPKY005_3_1.PartiNo.ToString());

                    oGeneralData.SetProperty("U_UrunKodu", fSAPKY005_3_1.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_UrunTanimi", fSAPKY005_3_1.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Aciklama", fSAPKY005_3_1.Aciklama.ToString());

                    oGeneralData.SetProperty("U_Kontrol1", fSAPKY005_3_1.Kontrol1.ToString());

                    oGeneralData.SetProperty("U_Kontrol2", fSAPKY005_3_1.Kontrol2.ToString());

                    if (fSAPKY005_3_1.Tarih != null && fSAPKY005_3_1.Tarih != "")
                    {
                        string tarih = fSAPKY005_3_1.Tarih;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_Tarih", dt);
                    }

                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    //oGeneralData.SetProperty("U_Tarih", dt);

                    oChildren_FSAPKY005_3_1_1 = oGeneralData.Child("AIF_FSAPKY005_3_1_1");

                    foreach (var item in fSAPKY005_3_1.fSAPKY005_3_1_1s)
                    {
                        oChild_FSAPKY005_3_1_1 = oChildren_FSAPKY005_3_1_1.Add();

                        oChild_FSAPKY005_3_1_1.SetProperty("U_UrunAdi", item.UrunAdi);

                        oChild_FSAPKY005_3_1_1.SetProperty("U_DolBasPH1", item.DolBasPH1);

                        oChild_FSAPKY005_3_1_1.SetProperty("U_DolBasPH2", item.DolBasPH2);

                        oChild_FSAPKY005_3_1_1.SetProperty("U_DolBasPH3", item.DolBasPH3);

                        oChild_FSAPKY005_3_1_1.SetProperty("U_DolBasPH4", item.DolBasPH4);

                        oChild_FSAPKY005_3_1_1.SetProperty("U_DolBasPH5", item.DolBasPH5);

                        oChild_FSAPKY005_3_1_1.SetProperty("U_DolBasPH6", item.DolBasPH6);

                        oChild_FSAPKY005_3_1_1.SetProperty("U_DolBasPH7", item.DolBasPH7);

                        oChild_FSAPKY005_3_1_1.SetProperty("U_DolBasPH8", item.DolBasPH8);

                        oChild_FSAPKY005_3_1_1.SetProperty("U_OprtAdi", item.OperatorAdi);
                    }

                    oChildren_FSAPKY005_3_1_2 = oGeneralData.Child("AIF_FSAPKY005_3_1_2");

                    foreach (var item in fSAPKY005_3_1.fSAPKY005_3_1_2s)
                    {
                        oChild_FSAPKY005_3_1_2 = oChildren_FSAPKY005_3_1_2.Add();

                        oChild_FSAPKY005_3_1_2.SetProperty("U_UrunAdi", item.UrunAdi);
                        oChild_FSAPKY005_3_1_2.SetProperty("U_Ornek1", item.Ornek1);
                        oChild_FSAPKY005_3_1_2.SetProperty("U_Ornek2", item.Ornek2);
                        oChild_FSAPKY005_3_1_2.SetProperty("U_Ornek3", item.Ornek3);
                        oChild_FSAPKY005_3_1_2.SetProperty("U_Ornek4", item.Ornek4);
                        oChild_FSAPKY005_3_1_2.SetProperty("U_Ornek5", item.Ornek5);
                        oChild_FSAPKY005_3_1_2.SetProperty("U_Ornek6", item.Ornek6);
                        oChild_FSAPKY005_3_1_2.SetProperty("U_Ornek7", item.Ornek7);
                        oChild_FSAPKY005_3_1_2.SetProperty("U_Ornek8", item.Ornek8);
                        oChild_FSAPKY005_3_1_2.SetProperty("U_Ornek9", item.Ornek9);
                        oChild_FSAPKY005_3_1_2.SetProperty("U_Ornek10", item.Ornek10);
                        oChild_FSAPKY005_3_1_2.SetProperty("U_OprtAdi", item.OperatorAdi);
                    }

                    oChildren_FSAPKY005_3_2_1 = oGeneralData.Child("AIF_FSAPKY005_3_2_1");

                    foreach (var item in fSAPKY005_3_1.fSAPKY005_3_2_1s)
                    {
                        oChild_FSAPKY005_3_2_1 = oChildren_FSAPKY005_3_2_1.Add();

                        oChild_FSAPKY005_3_2_1.SetProperty("U_TankNo", item.TankNumarasi);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_PaletNo", item.PaletNumarasi);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_AnlzSaat1", item.BirinciAnalizSaat);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_AnlzSaat2", item.IkinciAnalizSaat);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_AnlzSaat3", item.UcuncuAnalizSaat);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_AnlzSaat4", item.DorduncuAnalizSaat);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_AnlzSaat5", item.BesinciAnalizSaat);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_AnlzPH1", item.BirinciAnalizPH);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_AnlzPH2", item.IkinciAnalizPH);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_AnlzPH3", item.UcuncuAnalizPH);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_AnlzPH4", item.DorduncuAnalizPH);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_AnlzPH5", item.BesinciAnalizPH);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_AnlzSic1", item.BirinciAnalizSicaklik);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_AnlzSic2", item.IkinciAnalizSicaklik);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_AnlzSic3", item.UcuncuAnalizSicaklik);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_AnlzSic4", item.DorduncuAnalizSicaklik);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_AnlzSic5", item.BesinciAnalizSicaklik);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_OprtAdi", item.OperatorAdi);
                    }

                    oChildren_FSAPKY005_3_2_2 = oGeneralData.Child("AIF_FSAPKY005_3_2_2");

                    foreach (var item in fSAPKY005_3_1.fSAPKY005_3_2_2s)
                    {
                        oChild_FSAPKY005_3_2_2 = oChildren_FSAPKY005_3_2_2.Add();

                        oChild_FSAPKY005_3_2_2.SetProperty("U_TankNo", item.TankNo);

                        oChild_FSAPKY005_3_2_2.SetProperty("U_PaletNo", item.PaletNo);

                        oChild_FSAPKY005_3_2_2.SetProperty("U_Saat", item.Saat);

                        oChild_FSAPKY005_3_2_2.SetProperty("U_PH", item.PH);

                        oChild_FSAPKY005_3_2_2.SetProperty("U_SH", item.SH);

                        oChild_FSAPKY005_3_2_2.SetProperty("U_OprtAdi", item.OperatorAdi);
                    }


                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_FSAPKY005_3_1\" WITH (NOLOCK)");

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

                    GeneralData oChild_FSAPKY005_3_1_1;

                    GeneralDataCollection oChildren_FSAPKY005_3_1_1;

                    GeneralData oChild_FSAPKY005_3_1_2;

                    GeneralDataCollection oChildren_FSAPKY005_3_1_2;

                    GeneralData oChild_FSAPKY005_3_2_1;

                    GeneralDataCollection oChildren_FSAPKY005_3_2_1;

                    GeneralData oChild_FSAPKY005_3_2_2;

                    GeneralDataCollection oChildren_FSAPKY005_3_2_2;

                    oCompService = oCompany.GetCompanyService();

                    GeneralDataParams oGeneralParams;

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_FSAPKY005_3_1");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    oGeneralData.SetProperty("U_PartiNo", fSAPKY005_3_1.PartiNo.ToString());

                    oGeneralData.SetProperty("U_UrunKodu", fSAPKY005_3_1.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_UrunTanimi", fSAPKY005_3_1.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Aciklama", fSAPKY005_3_1.Aciklama.ToString());

                    oGeneralData.SetProperty("U_Kontrol1", fSAPKY005_3_1.Kontrol1.ToString());

                    oGeneralData.SetProperty("U_Kontrol2", fSAPKY005_3_1.Kontrol2.ToString());

                    if (fSAPKY005_3_1.Tarih != null && fSAPKY005_3_1.Tarih != "")
                    {
                        string tarih = fSAPKY005_3_1.Tarih;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_Tarih", dt);
                    }

                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    //oGeneralData.SetProperty("U_Tarih", dt);

                    oChildren_FSAPKY005_3_1_1 = oGeneralData.Child("AIF_FSAPKY005_3_1_1");

                    if (oChildren_FSAPKY005_3_1_1.Count > 0)
                    {
                        int drc = oChildren_FSAPKY005_3_1_1.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY005_3_1_1.Remove(0);
                    }

                    foreach (var item in fSAPKY005_3_1.fSAPKY005_3_1_1s)
                    {
                        oChild_FSAPKY005_3_1_1 = oChildren_FSAPKY005_3_1_1.Add();

                        oChild_FSAPKY005_3_1_1.SetProperty("U_UrunAdi", item.UrunAdi);

                        oChild_FSAPKY005_3_1_1.SetProperty("U_DolBasPH1", item.DolBasPH1);

                        oChild_FSAPKY005_3_1_1.SetProperty("U_DolBasPH2", item.DolBasPH2);

                        oChild_FSAPKY005_3_1_1.SetProperty("U_DolBasPH3", item.DolBasPH3);

                        oChild_FSAPKY005_3_1_1.SetProperty("U_DolBasPH4", item.DolBasPH4);

                        oChild_FSAPKY005_3_1_1.SetProperty("U_DolBasPH5", item.DolBasPH5);

                        oChild_FSAPKY005_3_1_1.SetProperty("U_DolBasPH6", item.DolBasPH6);

                        oChild_FSAPKY005_3_1_1.SetProperty("U_DolBasPH7", item.DolBasPH7);

                        oChild_FSAPKY005_3_1_1.SetProperty("U_DolBasPH8", item.DolBasPH8);

                        oChild_FSAPKY005_3_1_1.SetProperty("U_OprtAdi", item.OperatorAdi);
                    }

                    oChildren_FSAPKY005_3_1_2 = oGeneralData.Child("AIF_FSAPKY005_3_1_2");

                    if (oChildren_FSAPKY005_3_1_2.Count > 0)
                    {
                        int drc = oChildren_FSAPKY005_3_1_2.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY005_3_1_2.Remove(0);
                    }

                    foreach (var item in fSAPKY005_3_1.fSAPKY005_3_1_2s)
                    {
                        oChild_FSAPKY005_3_1_2 = oChildren_FSAPKY005_3_1_2.Add();

                        oChild_FSAPKY005_3_1_2.SetProperty("U_UrunAdi", item.UrunAdi);
                        oChild_FSAPKY005_3_1_2.SetProperty("U_Ornek1", item.Ornek1);
                        oChild_FSAPKY005_3_1_2.SetProperty("U_Ornek2", item.Ornek2);
                        oChild_FSAPKY005_3_1_2.SetProperty("U_Ornek3", item.Ornek3);
                        oChild_FSAPKY005_3_1_2.SetProperty("U_Ornek4", item.Ornek4);
                        oChild_FSAPKY005_3_1_2.SetProperty("U_Ornek5", item.Ornek5);
                        oChild_FSAPKY005_3_1_2.SetProperty("U_Ornek6", item.Ornek6);
                        oChild_FSAPKY005_3_1_2.SetProperty("U_Ornek7", item.Ornek7);
                        oChild_FSAPKY005_3_1_2.SetProperty("U_Ornek8", item.Ornek8);
                        oChild_FSAPKY005_3_1_2.SetProperty("U_Ornek9", item.Ornek9);
                        oChild_FSAPKY005_3_1_2.SetProperty("U_Ornek10", item.Ornek10);
                        oChild_FSAPKY005_3_1_2.SetProperty("U_OprtAdi", item.OperatorAdi);
                    }

                    oChildren_FSAPKY005_3_2_1 = oGeneralData.Child("AIF_FSAPKY005_3_2_1");

                    if (oChildren_FSAPKY005_3_2_1.Count > 0)
                    {
                        int drc = oChildren_FSAPKY005_3_2_1.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY005_3_2_1.Remove(0);
                    }

                    foreach (var item in fSAPKY005_3_1.fSAPKY005_3_2_1s)
                    {
                        oChild_FSAPKY005_3_2_1 = oChildren_FSAPKY005_3_2_1.Add();

                        oChild_FSAPKY005_3_2_1.SetProperty("U_TankNo", item.TankNumarasi);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_PaletNo", item.PaletNumarasi);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_AnlzSaat1", item.BirinciAnalizSaat);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_AnlzSaat2", item.IkinciAnalizSaat);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_AnlzSaat3", item.UcuncuAnalizSaat);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_AnlzSaat4", item.DorduncuAnalizSaat);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_AnlzSaat5", item.BesinciAnalizSaat);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_AnlzPH1", item.BirinciAnalizPH);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_AnlzPH2", item.IkinciAnalizPH);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_AnlzPH3", item.UcuncuAnalizPH);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_AnlzPH4", item.DorduncuAnalizPH);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_AnlzPH5", item.BesinciAnalizPH);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_AnlzSic1", item.BirinciAnalizSicaklik);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_AnlzSic2", item.IkinciAnalizSicaklik);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_AnlzSic3", item.UcuncuAnalizSicaklik);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_AnlzSic4", item.DorduncuAnalizSicaklik);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_AnlzSic5", item.BesinciAnalizSicaklik);

                        oChild_FSAPKY005_3_2_1.SetProperty("U_OprtAdi", item.OperatorAdi);
                    }

                    oChildren_FSAPKY005_3_2_2 = oGeneralData.Child("AIF_FSAPKY005_3_2_2");

                    if (oChildren_FSAPKY005_3_2_2.Count > 0)
                    {
                        int drc = oChildren_FSAPKY005_3_2_2.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY005_3_2_2.Remove(0);
                    }

                    foreach (var item in fSAPKY005_3_1.fSAPKY005_3_2_2s)
                    {
                        oChild_FSAPKY005_3_2_2 = oChildren_FSAPKY005_3_2_2.Add();

                        oChild_FSAPKY005_3_2_2.SetProperty("U_TankNo", item.TankNo);

                        oChild_FSAPKY005_3_2_2.SetProperty("U_PaletNo", item.PaletNo);

                        oChild_FSAPKY005_3_2_2.SetProperty("U_Saat", item.Saat);

                        oChild_FSAPKY005_3_2_2.SetProperty("U_PH", item.PH);

                        oChild_FSAPKY005_3_2_2.SetProperty("U_SH", item.SH);

                        oChild_FSAPKY005_3_2_2.SetProperty("U_OprtAdi", item.OperatorAdi);
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