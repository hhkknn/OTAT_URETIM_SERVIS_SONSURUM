using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddOrUpdateFSAPKY007_2
    {
        public Response addOrUpdateFSAPKY007_2(FSAPKY007_2 fSAPKY007_2, string dbName, string mKodValue)
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

                oRS.DoQuery("Select * from \"@AIF_FSAPKY007_2_1\" WITH (NOLOCK) where \"U_PartiNo\" = '" + fSAPKY007_2.PartiNo + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChild_AIF_FSAPKY007_2_1_1;

                    GeneralDataCollection oChildren_AIF_FSAPKY007_2_1_1;

                    GeneralData oChild_AIF_FSAPKY007_2_1_2;

                    GeneralDataCollection oChildren_AIF_FSAPKY007_2_1_2;

                    GeneralData oChild_AIF_FSAPKY007_2_2_1;

                    GeneralDataCollection oChildren_AIF_FSAPKY007_2_2_1;

                    GeneralData oChild_AIF_FSAPKY007_2_2_2;

                    GeneralDataCollection oChildren_AIF_FSAPKY007_2_2_2;

                    oCompService = oCompany.GetCompanyService();

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_FSAPKY007_2_1");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralData.SetProperty("U_PartiNo", fSAPKY007_2.PartiNo);

                    oGeneralData.SetProperty("U_UrunKodu", fSAPKY007_2.UrunKodu);

                    oGeneralData.SetProperty("U_UrunTanimi", fSAPKY007_2.UrunTanimi);

                    oGeneralData.SetProperty("U_Tarih", fSAPKY007_2.Tarih);

                    //oGeneralData.SetProperty("U_Aciklama", fSAPKY007_2.Aciklama);

                    oGeneralData.SetProperty("U_DgrKltKontrol", fSAPKY007_2.DigerKaliteKontroller);

                    

                    oChildren_AIF_FSAPKY007_2_1_1 = oGeneralData.Child("AIF_FSAPKY007_2_1_1");

                    foreach (var item in fSAPKY007_2.fSAPKY007_2_1_1s)
                    {
                        oChild_AIF_FSAPKY007_2_1_1 = oChildren_AIF_FSAPKY007_2_1_1.Add();

                        oChild_AIF_FSAPKY007_2_1_1.SetProperty("U_BasSaati", item.BaslangicSaati);

                        oChild_AIF_FSAPKY007_2_1_1.SetProperty("U_PastSic", item.PastorizasyonSicakligi);

                        oChild_AIF_FSAPKY007_2_1_1.SetProperty("U_PastCikis", item.PastorizasyonCikis);

                        oChild_AIF_FSAPKY007_2_1_1.SetProperty("U_HolderSur", item.HolderSuresi);

                        oChild_AIF_FSAPKY007_2_1_1.SetProperty("U_HmjBasinc", item.HomojenizasyonBasinc);

                        oChild_AIF_FSAPKY007_2_1_1.SetProperty("U_SuluSutMik", item.SuluAyranSutuMiktari);

                        oChild_AIF_FSAPKY007_2_1_1.SetProperty("U_CekKreMik", item.CekilenKremaMiktari);

                        oChild_AIF_FSAPKY007_2_1_1.SetProperty("U_StanSutMik", item.StandartAyranSutuMiktari);

                        oChild_AIF_FSAPKY007_2_1_1.SetProperty("U_BitisSaat", item.BitisSaati);

                        oChild_AIF_FSAPKY007_2_1_1.SetProperty("U_OprtAdi", item.OperatorAdi);
                    }

                     oChildren_AIF_FSAPKY007_2_1_2 = oGeneralData.Child("AIF_FSAPKY007_2_1_2");

                    foreach (var item in fSAPKY007_2.fSAPKY007_2_1_2s)
                    {
                        oChild_AIF_FSAPKY007_2_1_2 = oChildren_AIF_FSAPKY007_2_1_2.Add();

                        oChild_AIF_FSAPKY007_2_1_2.SetProperty("U_ProsTnkNo", item.AyranProsessTankNo);

                        oChild_AIF_FSAPKY007_2_1_2.SetProperty("U_AyrSutMik", item.StandartAyranSutuMiktari);

                        oChild_AIF_FSAPKY007_2_1_2.SetProperty("U_KulturSic", item.KulturlemeSicakligi);

                        oChild_AIF_FSAPKY007_2_1_2.SetProperty("U_KulturSaat", item.KulturlemeSaati);

                        oChild_AIF_FSAPKY007_2_1_2.SetProperty("U_KulturPH", item.KulturlemePH);

                        oChild_AIF_FSAPKY007_2_1_2.SetProperty("U_KirimSaat", item.KirimSaati);

                        oChild_AIF_FSAPKY007_2_1_2.SetProperty("U_KirimPH", item.KirimPH);

                        oChild_AIF_FSAPKY007_2_1_2.SetProperty("U_KirimSH", item.KirimSH);

                        oChild_AIF_FSAPKY007_2_1_2.SetProperty("U_SogCikSic", item.SogutmaEsanjoruCikisSicakligi);

                        oChild_AIF_FSAPKY007_2_1_2.SetProperty("U_LabPersAdi", item.LabPersonelAdi);
                    }
                     
                    oChildren_AIF_FSAPKY007_2_2_1 = oGeneralData.Child("AIF_FSAPKY007_2_2_1");

                    foreach (var item in fSAPKY007_2.fSAPKY007_2_2_1s)
                    {
                        oChild_AIF_FSAPKY007_2_2_1 = oChildren_AIF_FSAPKY007_2_2_1.Add();

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_TankNo", item.TankNo);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_PaletNo", item.PaletNo);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_AnlzSaat1", item.BirinciAnalizSaat);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_AnlzSaat2", item.IkinciAnalizSaat);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_AnlzSaat3", item.UcuncuAnalizSaat);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_AnlzSaat4", item.DorduncuAnalizSaat);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_AnlzSaat5", item.BesinciAnalizSaat);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_AnlzPH1", item.BirinciAnalizPH);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_AnlzPH2", item.IkinciAnalizPH);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_AnlzPH3", item.UcuncuAnalizPH);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_AnlzPH4", item.DorduncuAnalizPH);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_AnlzPH5", item.BesinciAnalizPH);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_AnlzSic1", item.BirinciAnalizSH);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_AnlzSic2", item.IkinciAnalizSH);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_AnlzSic3", item.UcuncuAnalizSH);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_AnlzSic4", item.DorduncuAnalizSH);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_AnlzSic5", item.BesinciAnalizSH);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_OprtAdi", item.OperatorAdi); 
                    }




                    oChildren_AIF_FSAPKY007_2_2_2 = oGeneralData.Child("AIF_FSAPKY007_2_2_2");

                    foreach (var item in fSAPKY007_2.fSAPKY007_2_2_2s)
                    {
                        oChild_AIF_FSAPKY007_2_2_2 = oChildren_AIF_FSAPKY007_2_2_2.Add();

                        oChild_AIF_FSAPKY007_2_2_2.SetProperty("U_TankNo", item.TankNo);

                        oChild_AIF_FSAPKY007_2_2_2.SetProperty("U_Saat", item.Saat);

                        oChild_AIF_FSAPKY007_2_2_2.SetProperty("U_PH", item.PH);

                        oChild_AIF_FSAPKY007_2_2_2.SetProperty("U_SH", item.SH);

                        oChild_AIF_FSAPKY007_2_2_2.SetProperty("U_OprtAdi", item.OperatorAdi); 
                    }

                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_FSAPKY007_2_1\" WITH (NOLOCK)");

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
                        return new Response { Value = 0, Description = "Analiz Girişi Oluşturuldu..", List = null };
                    }
                    else

                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -82000, Description = "Hata Kodu - 8200 Analiz girişi oluşturulurken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                    }
                }
                else
                {
                    CompanyService oCompService;
                    oCompService = oCompany.GetCompanyService();

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralDataParams oGeneralParams;

                    GeneralData oChild_AIF_FSAPKY007_2_1_1;

                    GeneralDataCollection oChildren_AIF_FSAPKY007_2_1_1;

                    GeneralData oChild_AIF_FSAPKY007_2_1_2;

                    GeneralDataCollection oChildren_AIF_FSAPKY007_2_1_2;

                    GeneralData oChild_AIF_FSAPKY007_2_2_1;

                    GeneralDataCollection oChildren_AIF_FSAPKY007_2_2_1;

                    GeneralData oChild_AIF_FSAPKY007_2_2_2;

                    GeneralDataCollection oChildren_AIF_FSAPKY007_2_2_2;

                    oGeneralService = oCompService.GetGeneralService("AIF_FSAPKY007_2_1");

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    oGeneralData.SetProperty("U_PartiNo", fSAPKY007_2.PartiNo);

                    oGeneralData.SetProperty("U_UrunKodu", fSAPKY007_2.UrunKodu);

                    oGeneralData.SetProperty("U_UrunTanimi", fSAPKY007_2.UrunTanimi);

                    oGeneralData.SetProperty("U_Tarih", fSAPKY007_2.Tarih);

                    //oGeneralData.SetProperty("U_Aciklama", fSAPKY007_2.Aciklama);

                    oGeneralData.SetProperty("U_DgrKltKontrol", fSAPKY007_2.DigerKaliteKontroller);

                    oChildren_AIF_FSAPKY007_2_1_1 = oGeneralData.Child("AIF_FSAPKY007_2_1_1");

                    if (oChildren_AIF_FSAPKY007_2_1_1.Count > 0)
                    {
                        int drc = oChildren_AIF_FSAPKY007_2_1_1.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_AIF_FSAPKY007_2_1_1.Remove(0);
                    }
                     
                    foreach (var item in fSAPKY007_2.fSAPKY007_2_1_1s)
                    {
                        oChild_AIF_FSAPKY007_2_1_1 = oChildren_AIF_FSAPKY007_2_1_1.Add();

                        oChild_AIF_FSAPKY007_2_1_1.SetProperty("U_BasSaati", item.BaslangicSaati);

                        oChild_AIF_FSAPKY007_2_1_1.SetProperty("U_PastSic", item.PastorizasyonSicakligi);

                        oChild_AIF_FSAPKY007_2_1_1.SetProperty("U_PastCikis", item.PastorizasyonCikis);

                        oChild_AIF_FSAPKY007_2_1_1.SetProperty("U_HolderSur", item.HolderSuresi);

                        oChild_AIF_FSAPKY007_2_1_1.SetProperty("U_HmjBasinc", item.HomojenizasyonBasinc);

                        oChild_AIF_FSAPKY007_2_1_1.SetProperty("U_SuluSutMik", item.SuluAyranSutuMiktari);

                        oChild_AIF_FSAPKY007_2_1_1.SetProperty("U_CekKreMik", item.CekilenKremaMiktari);

                        oChild_AIF_FSAPKY007_2_1_1.SetProperty("U_StanSutMik", item.StandartAyranSutuMiktari);

                        oChild_AIF_FSAPKY007_2_1_1.SetProperty("U_BitisSaat", item.BitisSaati);

                        oChild_AIF_FSAPKY007_2_1_1.SetProperty("U_OprtAdi", item.OperatorAdi);
                    }


                    oChildren_AIF_FSAPKY007_2_1_2 = oGeneralData.Child("AIF_FSAPKY007_2_1_2");

                    if (oChildren_AIF_FSAPKY007_2_1_2.Count > 0)
                    {
                        int drc = oChildren_AIF_FSAPKY007_2_1_2.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_AIF_FSAPKY007_2_1_2.Remove(0);
                    } 

                    foreach (var item in fSAPKY007_2.fSAPKY007_2_1_2s)
                    {
                        oChild_AIF_FSAPKY007_2_1_2 = oChildren_AIF_FSAPKY007_2_1_2.Add();

                        oChild_AIF_FSAPKY007_2_1_2.SetProperty("U_ProsTnkNo", item.AyranProsessTankNo);

                        oChild_AIF_FSAPKY007_2_1_2.SetProperty("U_AyrSutMik", item.StandartAyranSutuMiktari);

                        oChild_AIF_FSAPKY007_2_1_2.SetProperty("U_KulturSic", item.KulturlemeSicakligi);

                        oChild_AIF_FSAPKY007_2_1_2.SetProperty("U_KulturSaat", item.KulturlemeSaati);

                        oChild_AIF_FSAPKY007_2_1_2.SetProperty("U_KulturPH", item.KulturlemePH);

                        oChild_AIF_FSAPKY007_2_1_2.SetProperty("U_KirimSaat", item.KirimSaati);

                        oChild_AIF_FSAPKY007_2_1_2.SetProperty("U_KirimPH", item.KirimPH);

                        oChild_AIF_FSAPKY007_2_1_2.SetProperty("U_KirimSH", item.KirimSH);

                        oChild_AIF_FSAPKY007_2_1_2.SetProperty("U_SogCikSic", item.SogutmaEsanjoruCikisSicakligi);

                        oChild_AIF_FSAPKY007_2_1_2.SetProperty("U_LabPersAdi", item.LabPersonelAdi);
                    }


                    oChildren_AIF_FSAPKY007_2_2_1 = oGeneralData.Child("AIF_FSAPKY007_2_2_1");

                    if (oChildren_AIF_FSAPKY007_2_2_1.Count > 0)
                    {
                        int drc = oChildren_AIF_FSAPKY007_2_2_1.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_AIF_FSAPKY007_2_2_1.Remove(0);
                    }

                    foreach (var item in fSAPKY007_2.fSAPKY007_2_2_1s)
                    {
                        oChild_AIF_FSAPKY007_2_2_1 = oChildren_AIF_FSAPKY007_2_2_1.Add();

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_TankNo", item.TankNo);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_PaletNo", item.PaletNo);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_AnlzSaat1", item.BirinciAnalizSaat);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_AnlzSaat2", item.IkinciAnalizSaat);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_AnlzSaat3", item.UcuncuAnalizSaat);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_AnlzSaat4", item.DorduncuAnalizSaat);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_AnlzSaat5", item.BesinciAnalizSaat);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_AnlzPH1", item.BirinciAnalizPH);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_AnlzPH2", item.IkinciAnalizPH);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_AnlzPH3", item.UcuncuAnalizPH);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_AnlzPH4", item.DorduncuAnalizPH);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_AnlzPH5", item.BesinciAnalizPH);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_AnlzSic1", item.BirinciAnalizSH);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_AnlzSic2", item.IkinciAnalizSH);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_AnlzSic3", item.UcuncuAnalizSH);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_AnlzSic4", item.DorduncuAnalizSH);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_AnlzSic5", item.BesinciAnalizSH);

                        oChild_AIF_FSAPKY007_2_2_1.SetProperty("U_OprtAdi", item.OperatorAdi);
                    }


                    oChildren_AIF_FSAPKY007_2_2_2 = oGeneralData.Child("AIF_FSAPKY007_2_2_2");

                    if (oChildren_AIF_FSAPKY007_2_2_2.Count > 0)
                    {
                        int drc = oChildren_AIF_FSAPKY007_2_2_2.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_AIF_FSAPKY007_2_2_2.Remove(0);
                    }

                    foreach (var item in fSAPKY007_2.fSAPKY007_2_2_2s)
                    {
                        oChild_AIF_FSAPKY007_2_2_2 = oChildren_AIF_FSAPKY007_2_2_2.Add();

                        oChild_AIF_FSAPKY007_2_2_2.SetProperty("U_TankNo", item.TankNo);

                        oChild_AIF_FSAPKY007_2_2_2.SetProperty("U_Saat", item.Saat);

                        oChild_AIF_FSAPKY007_2_2_2.SetProperty("U_PH", item.PH);

                        oChild_AIF_FSAPKY007_2_2_2.SetProperty("U_SH", item.SH);

                        oChild_AIF_FSAPKY007_2_2_2.SetProperty("U_OprtAdi", item.OperatorAdi);
                    }

                    try
                    {
                        oGeneralService.Update(oGeneralData);
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = 0, Description = "Analiz Girişi güncellendi.", List = null };
                    }
                    catch (Exception)
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -8300, Description = "Hata Kodu - 8300 Analiz Girişi güncellenirken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
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