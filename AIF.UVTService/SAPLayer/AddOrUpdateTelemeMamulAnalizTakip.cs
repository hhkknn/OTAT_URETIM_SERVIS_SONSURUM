using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddOrUpdateTelemeMamulAnalizTakip
    {
        public Response addOrUpdateTelemeMamulTakipAnaliz(TelemeMamulTakipAnaliz telemeMamulTakipAnaliz, string dbName, string mKodValue)
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

                oRS.DoQuery("Select * from \"@AIF_TLMMML_ANALIZ\" WITH (NOLOCK) where \"U_PartiNo\" = '" + telemeMamulTakipAnaliz.PartiNo + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChildMamulOzellikleri;

                    GeneralDataCollection oChildrenMamulOzellikleri;

                    GeneralData oChildMamulOzellikleri1;

                    GeneralDataCollection oChildrenMamulOzellikleri1;

                    oCompService = oCompany.GetCompanyService();

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_TLMMML_ANALIZ");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralData.SetProperty("U_PartiNo", telemeMamulTakipAnaliz.PartiNo.ToString());

                    oGeneralData.SetProperty("U_Aciklama", telemeMamulTakipAnaliz.Aciklama.ToString());

                    if (telemeMamulTakipAnaliz.UretimTarihi != null && telemeMamulTakipAnaliz.UretimTarihi != "")
                    {
                        string tarih = telemeMamulTakipAnaliz.UretimTarihi;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_UretimTarihi", dt);

                    }

                    oChildrenMamulOzellikleri = oGeneralData.Child("AIF_TLMMML_ANALIZ1");

                    foreach (var item in telemeMamulTakipAnaliz.mamulOzellikleriDetay)
                    {
                        oChildMamulOzellikleri = oChildrenMamulOzellikleri.Add();

                        oChildMamulOzellikleri.SetProperty("U_UrtmSonrasiTelemeMik", item.UretimSonrasiTelemeMiktari);

                        oChildMamulOzellikleri.SetProperty("U_UrtmSonrasiTelemeMik1", item.BirGunSonraTelemeMiktari);

                        oChildMamulOzellikleri.SetProperty("U_UretimRandimani", item.UretimRandimani);

                        oChildMamulOzellikleri.SetProperty("U_PersonelKodu", item.PersonelKodu);

                        oChildMamulOzellikleri.SetProperty("U_PersonelAdi", item.PersonelAdi);
                    }


                    oChildrenMamulOzellikleri1 = oGeneralData.Child("AIF_TLMMML_ANALIZ2");

                    foreach (var item in telemeMamulTakipAnaliz.mamulOzellikleri1Detay)
                    {
                        oChildMamulOzellikleri1 = oChildrenMamulOzellikleri1.Add();

                        oChildMamulOzellikleri1.SetProperty("U_KuruMadde", item.KuruMadde);

                        oChildMamulOzellikleri1.SetProperty("U_YagOrani", item.YagOrani);

                        oChildMamulOzellikleri1.SetProperty("U_PH", item.PH);

                        oChildMamulOzellikleri1.SetProperty("U_SH", item.SH);

                        oChildMamulOzellikleri1.SetProperty("U_TuzOrani", item.TuzOrani);
                    }


                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_TLMMML_ANALIZ\" WITH (NOLOCK)");

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
                        return new Response { Value = 0, Description = "Teleme Mamül Analiz Takibi girişi oluşturuldu..", List = null };
                    }
                    else

                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5200, Description = "Hata Kodu - 5200 Teleme Mamül Analiz Takibi girişi oluşturulurken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                    }
                }
                else
                {
                    CompanyService oCompService;
                    oCompService = oCompany.GetCompanyService();

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralDataParams oGeneralParams;

                    GeneralData oChildMamulOzellikleri;

                    GeneralDataCollection oChildrenMamulOzellikleri;

                    GeneralData oChildMamulOzellikleri1;

                    GeneralDataCollection oChildrenMamulOzellikleri1;

                    oGeneralService = oCompService.GetGeneralService("AIF_TLMMML_ANALIZ");

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    oGeneralData.SetProperty("U_PartiNo", telemeMamulTakipAnaliz.PartiNo);

                    oGeneralData.SetProperty("U_Aciklama", telemeMamulTakipAnaliz.Aciklama);

                    if (telemeMamulTakipAnaliz.UretimTarihi != null && telemeMamulTakipAnaliz.UretimTarihi != "")
                    {
                        string tarih = telemeMamulTakipAnaliz.UretimTarihi;
                        DateTime dt = new DateTime(Convert.ToInt32(tarih.Substring(0, 4)), Convert.ToInt32(tarih.Substring(4, 2)), Convert.ToInt32(tarih.Substring(6, 2)));

                        oGeneralData.SetProperty("U_UretimTarihi", dt);

                    }


                    oChildrenMamulOzellikleri = oGeneralData.Child("AIF_TLMMML_ANALIZ1");

                    if (oChildrenMamulOzellikleri.Count > 0)
                    {
                        int drc = oChildrenMamulOzellikleri.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenMamulOzellikleri.Remove(0);
                    }

                    foreach (var item in telemeMamulTakipAnaliz.mamulOzellikleriDetay)
                    {
                        oChildMamulOzellikleri = oChildrenMamulOzellikleri.Add();

                        oChildMamulOzellikleri.SetProperty("U_UrtmSonrasiTelemeMik", item.UretimSonrasiTelemeMiktari);

                        oChildMamulOzellikleri.SetProperty("U_UrtmSonrasiTelemeMik1", item.BirGunSonraTelemeMiktari);

                        oChildMamulOzellikleri.SetProperty("U_UretimRandimani", item.UretimRandimani);

                        oChildMamulOzellikleri.SetProperty("U_PersonelKodu", item.PersonelKodu);

                        oChildMamulOzellikleri.SetProperty("U_PersonelAdi", item.PersonelAdi);
                    }



                    oChildrenMamulOzellikleri1 = oGeneralData.Child("AIF_TLMMML_ANALIZ2");

                    if (oChildrenMamulOzellikleri1.Count > 0)
                    {
                        int drc = oChildrenMamulOzellikleri1.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenMamulOzellikleri1.Remove(0);
                    }


                    foreach (var item in telemeMamulTakipAnaliz.mamulOzellikleri1Detay)
                    {
                        oChildMamulOzellikleri1 = oChildrenMamulOzellikleri1.Add();

                        oChildMamulOzellikleri1.SetProperty("U_KuruMadde", item.KuruMadde);

                        oChildMamulOzellikleri1.SetProperty("U_YagOrani", item.YagOrani);

                        oChildMamulOzellikleri1.SetProperty("U_PH", item.PH);

                        oChildMamulOzellikleri1.SetProperty("U_SH", item.SH);

                        oChildMamulOzellikleri1.SetProperty("U_TuzOrani", item.TuzOrani);
                    }


                    try
                    {
                        oGeneralService.Update(oGeneralData);
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = 0, Description = "Teleme Mamül Analiz Takibi girişi güncellendi.", List = null };
                    }
                    catch (Exception)
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5300, Description = "Hata Kodu - 5300 Teleme Mamül Analiz Takibi girişi güncellenirken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
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