using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddOrUpdateFSAPKY004_2
    {
        public Response addOrUpdateFSAPKY004_2(FSAPKY004_2 fSAPKY004_2, string dbName, string mKodValue)
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

                oRS.DoQuery("Select * from \"@AIF_FSAPKY004_2\" WITH (NOLOCK) where \"U_PartiNo\" = '" + fSAPKY004_2.PartiNo + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChild_FSAPKY004_2_1;

                    GeneralDataCollection oChildren_FSAPKY004_2_1;

                    GeneralData oChild_FSAPKY004_2_2;

                    GeneralDataCollection oChildren_FSAPKY004_2_2;

                    GeneralData oChild_FSAPKY004_2_3;

                    GeneralDataCollection oChildren_FSAPKY004_2_3; 

                    oCompService = oCompany.GetCompanyService();

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_FSAPKY004_2");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralData.SetProperty("U_PartiNo", fSAPKY004_2.PartiNo.ToString());

                    oGeneralData.SetProperty("U_UrunKodu", fSAPKY004_2.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_UrunTanimi", fSAPKY004_2.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Aciklama", fSAPKY004_2.Aciklama.ToString());

                    if (fSAPKY004_2.Tarih != null && fSAPKY004_2.Tarih != "")
                    {
                        string tarih = fSAPKY004_2.Tarih;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_Tarih", dt);
                    }

                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    //oGeneralData.SetProperty("U_Tarih", dt);

                    oChildren_FSAPKY004_2_1 = oGeneralData.Child("AIF_FSAPKY004_2_1");

                    foreach (var item in fSAPKY004_2.fSAPKY004_2_1s)
                    {
                        oChild_FSAPKY004_2_1 = oChildren_FSAPKY004_2_1.Add();
                                       
                        oChild_FSAPKY004_2_1.SetProperty("U_ProsesNo", item.ProsesNo);

                        oChild_FSAPKY004_2_1.SetProperty("U_YariMamMik", item.YariMamulMiktari);

                        oChild_FSAPKY004_2_1.SetProperty("U_EkLorMik", item.EklenenLorMiktari);

                        oChild_FSAPKY004_2_1.SetProperty("U_HmjBasinc", item.HomojenBasinci);

                        oChild_FSAPKY004_2_1.SetProperty("U_PastSic", item.PastorizasyonSicakligi);

                        oChild_FSAPKY004_2_1.SetProperty("U_UFOprtAdi", item.UFOperatorAdi); 
                    }

                    oChildren_FSAPKY004_2_2 = oGeneralData.Child("AIF_FSAPKY004_2_2");

                    foreach (var item in fSAPKY004_2.fSAPKY004_2_2s)
                    {
                        oChild_FSAPKY004_2_2 = oChildren_FSAPKY004_2_2.Add();

                        oChild_FSAPKY004_2_2.SetProperty("U_YariMamPH", item.YariMamulPH);

                        oChild_FSAPKY004_2_2.SetProperty("U_YariMamSH", item.YariMamulSH);

                        oChild_FSAPKY004_2_2.SetProperty("U_YariMamKM", item.YariMamulKM);

                        oChild_FSAPKY004_2_2.SetProperty("U_YariMamYag", item.YariMamulYag);

                        oChild_FSAPKY004_2_2.SetProperty("U_LorKM", item.LorKM);

                        oChild_FSAPKY004_2_2.SetProperty("U_LorYagi", item.LorYagi);

                        oChild_FSAPKY004_2_2.SetProperty("U_LabPersAdi", item.LabPersonelAdi);
                    }

                    oChildren_FSAPKY004_2_3 = oGeneralData.Child("AIF_FSAPKY004_2_3");

                    foreach (var item in fSAPKY004_2.fSAPKY004_2_3s)
                    {
                        oChild_FSAPKY004_2_3 = oChildren_FSAPKY004_2_3.Add();

                        oChild_FSAPKY004_2_3.SetProperty("U_UrunKodu", item.UrunKodu);

                        oChild_FSAPKY004_2_3.SetProperty("U_UrunAdi", item.UrunAdi);

                        oChild_FSAPKY004_2_3.SetProperty("U_DuyAnlzOny", item.DuyusalAnalizOnayi);

                        oChild_FSAPKY004_2_3.SetProperty("U_SorumluMuh", item.SorumluMuhendis); 
                    }

                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_FSAPKY004_2\" WITH (NOLOCK)");

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

                    GeneralData oChild_FSAPKY004_2_1;

                    GeneralDataCollection oChildren_FSAPKY004_2_1;

                    GeneralData oChild_FSAPKY004_2_2;

                    GeneralDataCollection oChildren_FSAPKY004_2_2;

                    GeneralData oChild_FSAPKY004_2_3;

                    GeneralDataCollection oChildren_FSAPKY004_2_3;

                    oCompService = oCompany.GetCompanyService();

                    GeneralDataParams oGeneralParams;

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_FSAPKY004_2");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    oGeneralData.SetProperty("U_PartiNo", fSAPKY004_2.PartiNo.ToString());

                    oGeneralData.SetProperty("U_UrunKodu", fSAPKY004_2.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_UrunTanimi", fSAPKY004_2.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Aciklama", fSAPKY004_2.Aciklama.ToString());

                    if (fSAPKY004_2.Tarih != null && fSAPKY004_2.Tarih != "")
                    {
                        string tarih = fSAPKY004_2.Tarih;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_Tarih", dt);
                    }

                    //DateTime dt = new DateTime(Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(0, 4)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(4, 2)), Convert.ToInt32(telemeAnalizTakibi.Tarih.Substring(6, 2)));

                    //oGeneralData.SetProperty("U_Tarih", dt);

                    oChildren_FSAPKY004_2_1 = oGeneralData.Child("AIF_FSAPKY004_2_1");

                    if (oChildren_FSAPKY004_2_1.Count > 0)
                    {
                        int drc = oChildren_FSAPKY004_2_1.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY004_2_1.Remove(0);
                    }

                    foreach (var item in fSAPKY004_2.fSAPKY004_2_1s)
                    {
                        oChild_FSAPKY004_2_1 = oChildren_FSAPKY004_2_1.Add();

                        oChild_FSAPKY004_2_1.SetProperty("U_ProsesNo", item.ProsesNo);

                        oChild_FSAPKY004_2_1.SetProperty("U_YariMamMik", item.YariMamulMiktari);

                        oChild_FSAPKY004_2_1.SetProperty("U_EkLorMik", item.EklenenLorMiktari);

                        oChild_FSAPKY004_2_1.SetProperty("U_HmjBasinc", item.HomojenBasinci);

                        oChild_FSAPKY004_2_1.SetProperty("U_PastSic", item.PastorizasyonSicakligi);

                        oChild_FSAPKY004_2_1.SetProperty("U_UFOprtAdi", item.UFOperatorAdi);
                    }

                    oChildren_FSAPKY004_2_2 = oGeneralData.Child("AIF_FSAPKY004_2_2");

                    if (oChildren_FSAPKY004_2_2.Count > 0)
                    {
                        int drc = oChildren_FSAPKY004_2_2.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY004_2_2.Remove(0);
                    }

                    foreach (var item in fSAPKY004_2.fSAPKY004_2_2s)
                    {
                        oChild_FSAPKY004_2_2 = oChildren_FSAPKY004_2_2.Add();

                        oChild_FSAPKY004_2_2.SetProperty("U_YariMamPH", item.YariMamulPH);

                        oChild_FSAPKY004_2_2.SetProperty("U_YariMamSH", item.YariMamulSH);

                        oChild_FSAPKY004_2_2.SetProperty("U_YariMamKM", item.YariMamulKM);

                        oChild_FSAPKY004_2_2.SetProperty("U_YariMamYag", item.YariMamulYag);

                        oChild_FSAPKY004_2_2.SetProperty("U_LorKM", item.LorKM);

                        oChild_FSAPKY004_2_2.SetProperty("U_LorYagi", item.LorYagi);

                        oChild_FSAPKY004_2_2.SetProperty("U_LabPersAdi", item.LabPersonelAdi);
                    }

                    oChildren_FSAPKY004_2_3 = oGeneralData.Child("AIF_FSAPKY004_2_3");

                    if (oChildren_FSAPKY004_2_3.Count > 0)
                    {
                        int drc = oChildren_FSAPKY004_2_3.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY004_2_3.Remove(0);
                    }

                    foreach (var item in fSAPKY004_2.fSAPKY004_2_3s)
                    {
                        oChild_FSAPKY004_2_3 = oChildren_FSAPKY004_2_3.Add();

                        oChild_FSAPKY004_2_3.SetProperty("U_UrunKodu", item.UrunKodu);

                        oChild_FSAPKY004_2_3.SetProperty("U_UrunAdi", item.UrunAdi);

                        oChild_FSAPKY004_2_3.SetProperty("U_DuyAnlzOny", item.DuyusalAnalizOnayi);

                        oChild_FSAPKY004_2_3.SetProperty("U_SorumluMuh", item.SorumluMuhendis);
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