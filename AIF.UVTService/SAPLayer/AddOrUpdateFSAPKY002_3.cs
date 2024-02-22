using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddOrUpdateFSAPKY002_3
    {
        public Response addOrUpdateFSAPKY002_3(FSAPKY002_3 fSAPKY002_3, string dbName, string mKodValue)
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

                oRS.DoQuery("Select * from \"@AIF_FSAPKY002_3\" WITH (NOLOCK) where \"U_PartiNo\" = '" + fSAPKY002_3.PartiNo + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChild_FSAPKY002_3_1;

                    GeneralDataCollection oChildren_FSAPKY002_3_1;

                    GeneralData oChild_FSAPKY002_3_2;

                    GeneralDataCollection oChildren_FSAPKY002_3_2;

                    GeneralData oChild_FSAPKY002_3_3;

                    GeneralDataCollection oChildren_FSAPKY002_3_3;

                    GeneralData oChild_FSAPKY002_3_4;

                    GeneralDataCollection oChildren_FSAPKY002_3_4;

                    oCompService = oCompany.GetCompanyService();

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_FSAPKY002_3");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralData.SetProperty("U_PartiNo", fSAPKY002_3.PartiNo.ToString());

                    oGeneralData.SetProperty("U_UrunKodu", fSAPKY002_3.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_UrunTanimi", fSAPKY002_3.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Kontrol1", fSAPKY002_3.Kontrol1.ToString());

                    oGeneralData.SetProperty("U_Kontrol2", fSAPKY002_3.Kontrol2.ToString());

                    oGeneralData.SetProperty("U_Kontrol3", fSAPKY002_3.Kontrol3.ToString());
                    //oGeneralData.SetProperty("U_Aciklama", fSAPKY002_1.Aciklama.ToString());

                    if (fSAPKY002_3.Tarih != null && fSAPKY002_3.Tarih != "")
                    {
                        string tarih = fSAPKY002_3.Tarih;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_Tarih", dt);
                    }

                    oChildren_FSAPKY002_3_1 = oGeneralData.Child("AIF_FSAPKY002_3_1");

                    foreach (var item in fSAPKY002_3.fSAPKY002_3_1s)
                    {
                        oChild_FSAPKY002_3_1 = oChildren_FSAPKY002_3_1.Add();

                        oChild_FSAPKY002_3_1.SetProperty("U_UrunKodu", item.UrunKodu);

                        oChild_FSAPKY002_3_1.SetProperty("U_UrunAdi", item.UrunAdi);

                        oChild_FSAPKY002_3_1.SetProperty("U_PaketSicak", item.PaketlemeSicakligi);

                        if (item.UretimTarihi.Year != 1900)
                        {
                            oChild_FSAPKY002_3_1.SetProperty("U_UretimTarih", item.UretimTarihi);
                        }

                        if (item.SonTuketimTarihi.Year != 1900)
                        {
                            oChild_FSAPKY002_3_1.SetProperty("U_SonTukTarih", item.SonTuketimTarihi);
                        }

                        oChild_FSAPKY002_3_1.SetProperty("U_PartiNo", item.PartiNumarasi);

                        oChild_FSAPKY002_3_1.SetProperty("U_OprtAdi", item.OperatorAdi);
                    }

                    oChildren_FSAPKY002_3_2 = oGeneralData.Child("AIF_FSAPKY002_3_2");

                    foreach (var item in fSAPKY002_3.fSAPKY002_3_2s)
                    {
                        oChild_FSAPKY002_3_2 = oChildren_FSAPKY002_3_2.Add();

                        oChild_FSAPKY002_3_2.SetProperty("U_Saat", item.Saat);

                        oChild_FSAPKY002_3_2.SetProperty("U_UrunKodu", item.UrunKodu);

                        oChild_FSAPKY002_3_2.SetProperty("U_UrunAdi", item.UrunAdi);

                        oChild_FSAPKY002_3_2.SetProperty("U_BirGozCO2", item.BirinciGozCO2);

                        oChild_FSAPKY002_3_2.SetProperty("U_BirGozO2", item.BirinciGozO2);

                        oChild_FSAPKY002_3_2.SetProperty("U_IkiGozCO2", item.IkinciGozCO2);

                        oChild_FSAPKY002_3_2.SetProperty("U_IkiGozO2", item.IkinciGozO2);

                        oChild_FSAPKY002_3_2.SetProperty("U_UcGozCO2", item.UcuncuGozCO2);

                        oChild_FSAPKY002_3_2.SetProperty("U_UcGozO2", item.UcuncuGozO2);

                        oChild_FSAPKY002_3_2.SetProperty("U_OprtAdi", item.OperatorAdi);
                    }

                    oChildren_FSAPKY002_3_3 = oGeneralData.Child("AIF_FSAPKY002_3_3");

                    foreach (var item in fSAPKY002_3.fSAPKY002_3_3s)
                    {
                        oChild_FSAPKY002_3_3 = oChildren_FSAPKY002_3_3.Add();

                        oChild_FSAPKY002_3_3.SetProperty("U_UrunKodu", item.UrunKodu);

                        oChild_FSAPKY002_3_3.SetProperty("U_UrunAdi", item.UrunAdi);

                        oChild_FSAPKY002_3_3.SetProperty("U_Ornek1", item.Ornek1);

                        oChild_FSAPKY002_3_3.SetProperty("U_Ornek2", item.Ornek2);

                        oChild_FSAPKY002_3_3.SetProperty("U_Ornek3", item.Ornek3);

                        oChild_FSAPKY002_3_3.SetProperty("U_Ornek4", item.Ornek4);

                        oChild_FSAPKY002_3_3.SetProperty("U_Ornek5", item.Ornek5);

                        oChild_FSAPKY002_3_3.SetProperty("U_Ornek6", item.Ornek6);

                        oChild_FSAPKY002_3_3.SetProperty("U_Ornek7", item.Ornek7);

                        oChild_FSAPKY002_3_3.SetProperty("U_Ornek8", item.Ornek8);

                        oChild_FSAPKY002_3_3.SetProperty("U_Ornek9", item.Ornek9);

                        oChild_FSAPKY002_3_3.SetProperty("U_Ornek10", item.Ornek10);

                        oChild_FSAPKY002_3_3.SetProperty("U_OprtAdi", item.OperatorAdi);
                    }

                    oChildren_FSAPKY002_3_4 = oGeneralData.Child("AIF_FSAPKY002_3_4");

                    foreach (var item in fSAPKY002_3.fSAPKY002_3_4s)
                    {
                        oChild_FSAPKY002_3_4 = oChildren_FSAPKY002_3_4.Add();

                        oChild_FSAPKY002_3_4.SetProperty("U_UrunKodu", item.UrunKodu);

                        oChild_FSAPKY002_3_4.SetProperty("U_UrunAdi", item.UrunAdi);

                        oChild_FSAPKY002_3_4.SetProperty("U_DuyAnlzOny", item.DuyusalAnalizOnayi);

                        oChild_FSAPKY002_3_4.SetProperty("U_SorumluMuh", item.SorumluMuhendis);
                    }

                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_FSAPKY002_3\" WITH (NOLOCK)");

                    int maxdocentry = Convert.ToInt32(oRS.Fields.Item(0).Value);

                    oGeneralData.SetProperty("DocNum", maxdocentry);

                    var resp = oGeneralService.Add(oGeneralData);

                    if (resp != null)
                    {
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

                    GeneralData oChild_FSAPKY002_3_1;

                    GeneralDataCollection oChildren_FSAPKY002_3_1;

                    GeneralData oChild_FSAPKY002_3_2;

                    GeneralDataCollection oChildren_FSAPKY002_3_2;

                    GeneralData oChild_FSAPKY002_3_3;

                    GeneralDataCollection oChildren_FSAPKY002_3_3;

                    GeneralData oChild_FSAPKY002_3_4;

                    GeneralDataCollection oChildren_FSAPKY002_3_4;

                    oCompService = oCompany.GetCompanyService();

                    GeneralDataParams oGeneralParams;

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_FSAPKY002_3");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    oGeneralData.SetProperty("U_PartiNo", fSAPKY002_3.PartiNo.ToString());

                    oGeneralData.SetProperty("U_UrunKodu", fSAPKY002_3.UrunKodu.ToString());

                    oGeneralData.SetProperty("U_UrunTanimi", fSAPKY002_3.UrunTanimi.ToString());

                    oGeneralData.SetProperty("U_Kontrol1", fSAPKY002_3.Kontrol1.ToString());

                    oGeneralData.SetProperty("U_Kontrol2", fSAPKY002_3.Kontrol2.ToString());

                    oGeneralData.SetProperty("U_Kontrol3", fSAPKY002_3.Kontrol3.ToString());

                    //oGeneralData.SetProperty("U_Aciklama", fSAPKY002_1.Aciklama.ToString());

                    if (fSAPKY002_3.Tarih != null && fSAPKY002_3.Tarih != "")
                    {
                        string tarih = fSAPKY002_3.Tarih;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_Tarih", dt);
                    }

                    oChildren_FSAPKY002_3_1 = oGeneralData.Child("AIF_FSAPKY002_3_1");

                    if (oChildren_FSAPKY002_3_1.Count > 0)
                    {
                        int drc = oChildren_FSAPKY002_3_1.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY002_3_1.Remove(0);
                    }


                    foreach (var item in fSAPKY002_3.fSAPKY002_3_1s)
                    {
                        oChild_FSAPKY002_3_1 = oChildren_FSAPKY002_3_1.Add();

                        oChild_FSAPKY002_3_1.SetProperty("U_UrunKodu", item.UrunKodu);

                        oChild_FSAPKY002_3_1.SetProperty("U_UrunAdi", item.UrunAdi);

                        oChild_FSAPKY002_3_1.SetProperty("U_PaketSicak", item.PaketlemeSicakligi);

                        oChild_FSAPKY002_3_1.SetProperty("U_UretimTarih", item.UretimTarihi);

                        oChild_FSAPKY002_3_1.SetProperty("U_SonTukTarih", item.SonTuketimTarihi);

                        oChild_FSAPKY002_3_1.SetProperty("U_PartiNo", item.PartiNumarasi);

                        oChild_FSAPKY002_3_1.SetProperty("U_OprtAdi", item.OperatorAdi);
                    }

                    oChildren_FSAPKY002_3_2 = oGeneralData.Child("AIF_FSAPKY002_3_2");

                    if (oChildren_FSAPKY002_3_2.Count > 0)
                    {
                        int drc = oChildren_FSAPKY002_3_2.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY002_3_2.Remove(0);
                    }

                    foreach (var item in fSAPKY002_3.fSAPKY002_3_2s)
                    {
                        oChild_FSAPKY002_3_2 = oChildren_FSAPKY002_3_2.Add();

                        oChild_FSAPKY002_3_2.SetProperty("U_Saat", item.Saat);

                        oChild_FSAPKY002_3_2.SetProperty("U_UrunKodu", item.UrunKodu);

                        oChild_FSAPKY002_3_2.SetProperty("U_UrunAdi", item.UrunAdi);

                        oChild_FSAPKY002_3_2.SetProperty("U_BirGozCO2", item.BirinciGozCO2);

                        oChild_FSAPKY002_3_2.SetProperty("U_BirGozO2", item.BirinciGozO2);

                        oChild_FSAPKY002_3_2.SetProperty("U_IkiGozCO2", item.IkinciGozCO2);

                        oChild_FSAPKY002_3_2.SetProperty("U_IkiGozO2", item.IkinciGozO2);

                        oChild_FSAPKY002_3_2.SetProperty("U_UcGozCO2", item.UcuncuGozCO2);

                        oChild_FSAPKY002_3_2.SetProperty("U_UcGozO2", item.UcuncuGozO2);

                        oChild_FSAPKY002_3_2.SetProperty("U_OprtAdi", item.OperatorAdi);
                    }


                    oChildren_FSAPKY002_3_3 = oGeneralData.Child("AIF_FSAPKY002_3_3");

                    if (oChildren_FSAPKY002_3_3.Count > 0)
                    {
                        int drc = oChildren_FSAPKY002_3_3.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY002_3_3.Remove(0);
                    }


                    foreach (var item in fSAPKY002_3.fSAPKY002_3_3s)
                    {
                        oChild_FSAPKY002_3_3 = oChildren_FSAPKY002_3_3.Add();

                        oChild_FSAPKY002_3_3.SetProperty("U_UrunKodu", item.UrunKodu);

                        oChild_FSAPKY002_3_3.SetProperty("U_UrunAdi", item.UrunAdi);

                        oChild_FSAPKY002_3_3.SetProperty("U_Ornek1", item.Ornek1);

                        oChild_FSAPKY002_3_3.SetProperty("U_Ornek2", item.Ornek2);

                        oChild_FSAPKY002_3_3.SetProperty("U_Ornek3", item.Ornek3);

                        oChild_FSAPKY002_3_3.SetProperty("U_Ornek4", item.Ornek4);

                        oChild_FSAPKY002_3_3.SetProperty("U_Ornek5", item.Ornek5);

                        oChild_FSAPKY002_3_3.SetProperty("U_Ornek6", item.Ornek6);

                        oChild_FSAPKY002_3_3.SetProperty("U_Ornek7", item.Ornek7);

                        oChild_FSAPKY002_3_3.SetProperty("U_Ornek8", item.Ornek8);

                        oChild_FSAPKY002_3_3.SetProperty("U_Ornek9", item.Ornek9);

                        oChild_FSAPKY002_3_3.SetProperty("U_Ornek10", item.Ornek10);

                        oChild_FSAPKY002_3_3.SetProperty("U_OprtAdi", item.OperatorAdi);
                    }

                    oChildren_FSAPKY002_3_4 = oGeneralData.Child("AIF_FSAPKY002_3_4");

                    if (oChildren_FSAPKY002_3_4.Count > 0)
                    {
                        int drc = oChildren_FSAPKY002_3_4.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildren_FSAPKY002_3_4.Remove(0);
                    }

                    foreach (var item in fSAPKY002_3.fSAPKY002_3_4s)
                    {
                        oChild_FSAPKY002_3_4 = oChildren_FSAPKY002_3_4.Add();

                        oChild_FSAPKY002_3_4.SetProperty("U_UrunKodu", item.UrunKodu);

                        oChild_FSAPKY002_3_4.SetProperty("U_UrunAdi", item.UrunAdi);

                        oChild_FSAPKY002_3_4.SetProperty("U_DuyAnlzOny", item.DuyusalAnalizOnayi);

                        oChild_FSAPKY002_3_4.SetProperty("U_SorumluMuh", item.SorumluMuhendis);
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