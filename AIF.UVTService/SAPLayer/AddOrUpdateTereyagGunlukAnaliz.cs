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
    public class AddOrUpdateTereyagGunlukAnaliz
    {
        public Response addOrUpdateTereyagGunlukAnaliz(TereyagGunlukAnaliz tereyagGunlukAnaliz, string dbName, string mKodValue)
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

                SAPbobsCOM.Company oCompany = connection.oCompany;

                Recordset oRS = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

                oRS.DoQuery("Select * from \"@AIF_TRYGGUN_ANLZ\" WITH (NOLOCK) where \"U_UretimTarihi\" = '" + tereyagGunlukAnaliz.UretimTarihi + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChildMamulOzellikleri;

                    GeneralDataCollection oChildrenMamulOzellikleri;

                    GeneralData oChildDinlendirmeVePaketleme;

                    GeneralDataCollection oChildrenDinlendirmeVePaketleme;

                    oCompService = oCompany.GetCompanyService();

                    oGeneralService = oCompService.GetGeneralService("AIF_TRYGGUN_ANLZ");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);
                      
                    oGeneralData.SetProperty("U_Aciklama", tereyagGunlukAnaliz.Aciklama.ToString());

                    DateTime dt = new DateTime(Convert.ToInt32(tereyagGunlukAnaliz.UretimTarihi.Substring(0, 4)), Convert.ToInt32(tereyagGunlukAnaliz.UretimTarihi.Substring(4, 2)), Convert.ToInt32(tereyagGunlukAnaliz.UretimTarihi.Substring(6, 2)));

                    oGeneralData.SetProperty("U_UretimTarihi", dt);

                    oGeneralData.SetProperty("U_PaketlemeTarihi", dt);


                    oChildrenMamulOzellikleri = oGeneralData.Child("AIF_TRYGGUN_ANLZ1");

                    foreach (var item in tereyagGunlukAnaliz.tereyagGunlukMamulOzelliks)
                    {
                        oChildMamulOzellikleri = oChildrenMamulOzellikleri.Add();

                        oChildMamulOzellikleri.SetProperty("U_UretilenUrunler", item.UretilenUrun);

                        oChildMamulOzellikleri.SetProperty("U_PaketlemeOncesiSicakik", item.PaketlemeOncesiSicaklik);

                        oChildMamulOzellikleri.SetProperty("U_UretimMiktari", item.UretimMiktari);

                        oChildMamulOzellikleri.SetProperty("U_PaketlenenUrunMiktari", item.PaketlenenUrunMiktari);

                        oChildMamulOzellikleri.SetProperty("U_FireUrunMiktari", item.FireUrunMiktari);

                        oChildMamulOzellikleri.SetProperty("U_NumuneUrunMiktari", item.NumuneUrunMiktari);

                        oChildMamulOzellikleri.SetProperty("U_DepoyaGirenUrunMik", item.DepoyaGirenUrunMiktari);

                        oChildMamulOzellikleri.SetProperty("U_KuruMadde", item.KuruMadde);

                        oChildMamulOzellikleri.SetProperty("U_YagOrani", item.YagOrani);

                        oChildMamulOzellikleri.SetProperty("U_PH", item.PH);

                        oChildMamulOzellikleri.SetProperty("U_SH", item.SH);

                        oChildMamulOzellikleri.SetProperty("U_TuzOrani", item.TuzOrani);
                    }

                    oChildrenDinlendirmeVePaketleme = oGeneralData.Child("AIF_TRYGGUN_ANLZ2");

                    foreach (var item in tereyagGunlukAnaliz.tereyagGunlukDinlendirmeVePaketlemes)
                    {
                        oChildDinlendirmeVePaketleme = oChildrenDinlendirmeVePaketleme.Add();

                        oChildDinlendirmeVePaketleme.SetProperty("U_AlanAdi", item.AlanAdi);

                        oChildDinlendirmeVePaketleme.SetProperty("U_SifirSekizSicaklik", item.SifirSekizSicaklik);

                        oChildDinlendirmeVePaketleme.SetProperty("U_SifirSekizNem", item.SifirSekizNem);

                        oChildDinlendirmeVePaketleme.SetProperty("U_OnikiSicaklik", item.OnikiSicaklik);

                        oChildDinlendirmeVePaketleme.SetProperty("U_OnikiNem", item.OnikiNem);

                        oChildDinlendirmeVePaketleme.SetProperty("U_OnBesSicaklik", item.OnBesSicaklik);

                        oChildDinlendirmeVePaketleme.SetProperty("U_OnBesNem", item.OnBesNem);

                        oChildDinlendirmeVePaketleme.SetProperty("U_OnSekizSicaklik", item.OnSekizSicaklik);

                        oChildDinlendirmeVePaketleme.SetProperty("U_OnSekizNem", item.OnSekizNem);
                    } 
                     
                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_TRYGGUN_ANLZ\" WITH (NOLOCK)");

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
                        return new Response { Value = 0, Description = "Tereyağ Günlük Analiz girişi oluşturuldu.", List = null };
                    }
                    else

                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5200, Description = "Hata Kodu - 5200 Tereyağ Günlük Analiz girişi oluşturulurken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                    }
                }
                else
                {


                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralDataParams oGeneralParams;

                    GeneralData oChildMamulOzellikleri;

                    GeneralDataCollection oChildrenMamulOzellikleri;

                    GeneralData oChildDinlendirmeVePaketleme;

                    GeneralDataCollection oChildrenDinlendirmeVePaketleme;

                    oCompService = oCompany.GetCompanyService();

                    oGeneralService = oCompService.GetGeneralService("AIF_TRYGGUN_ANLZ");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);
                      
                    oGeneralData.SetProperty("U_Aciklama", tereyagGunlukAnaliz.Aciklama.ToString());

                    DateTime dt = new DateTime(Convert.ToInt32(tereyagGunlukAnaliz.UretimTarihi.Substring(0, 4)), Convert.ToInt32(tereyagGunlukAnaliz.UretimTarihi.Substring(4, 2)), Convert.ToInt32(tereyagGunlukAnaliz.UretimTarihi.Substring(6, 2)));

                    oGeneralData.SetProperty("U_UretimTarihi", dt);

                    oGeneralData.SetProperty("U_PaketlemeTarihi", dt);


                    oChildrenMamulOzellikleri = oGeneralData.Child("AIF_TRYGGUN_ANLZ1");

                    if (oChildrenMamulOzellikleri.Count > 0)
                    {
                        int drc = oChildrenMamulOzellikleri.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenMamulOzellikleri.Remove(0);
                    }

                    foreach (var item in tereyagGunlukAnaliz.tereyagGunlukMamulOzelliks)
                    {
                        oChildMamulOzellikleri = oChildrenMamulOzellikleri.Add();

                        oChildMamulOzellikleri.SetProperty("U_UretilenUrunler", item.UretilenUrun);

                        oChildMamulOzellikleri.SetProperty("U_PaketlemeOncesiSicakik", item.PaketlemeOncesiSicaklik);

                        oChildMamulOzellikleri.SetProperty("U_UretimMiktari", item.UretimMiktari);

                        oChildMamulOzellikleri.SetProperty("U_PaketlenenUrunMiktari", item.PaketlenenUrunMiktari);

                        oChildMamulOzellikleri.SetProperty("U_FireUrunMiktari", item.FireUrunMiktari);

                        oChildMamulOzellikleri.SetProperty("U_NumuneUrunMiktari", item.NumuneUrunMiktari);

                        oChildMamulOzellikleri.SetProperty("U_DepoyaGirenUrunMik", item.DepoyaGirenUrunMiktari);

                        oChildMamulOzellikleri.SetProperty("U_KuruMadde", item.KuruMadde);

                        oChildMamulOzellikleri.SetProperty("U_YagOrani", item.YagOrani);

                        oChildMamulOzellikleri.SetProperty("U_PH", item.PH);

                        oChildMamulOzellikleri.SetProperty("U_SH", item.SH);

                        oChildMamulOzellikleri.SetProperty("U_TuzOrani", item.TuzOrani);
                    }

                    oChildrenDinlendirmeVePaketleme = oGeneralData.Child("AIF_TRYGGUN_ANLZ2");

                    if (oChildrenDinlendirmeVePaketleme.Count > 0)
                    {
                        int drc = oChildrenDinlendirmeVePaketleme.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenDinlendirmeVePaketleme.Remove(0);
                    }

                    foreach (var item in tereyagGunlukAnaliz.tereyagGunlukDinlendirmeVePaketlemes)
                    {
                        oChildDinlendirmeVePaketleme = oChildrenDinlendirmeVePaketleme.Add();

                        oChildDinlendirmeVePaketleme.SetProperty("U_AlanAdi", item.AlanAdi);

                        oChildDinlendirmeVePaketleme.SetProperty("U_SifirSekizSicaklik", item.SifirSekizSicaklik);

                        oChildDinlendirmeVePaketleme.SetProperty("U_SifirSekizNem", item.SifirSekizNem);

                        oChildDinlendirmeVePaketleme.SetProperty("U_OnikiSicaklik", item.OnikiSicaklik);

                        oChildDinlendirmeVePaketleme.SetProperty("U_OnikiNem", item.OnikiNem);

                        oChildDinlendirmeVePaketleme.SetProperty("U_OnBesSicaklik", item.OnBesSicaklik);

                        oChildDinlendirmeVePaketleme.SetProperty("U_OnBesNem", item.OnBesNem);

                        oChildDinlendirmeVePaketleme.SetProperty("U_OnSekizSicaklik", item.OnSekizSicaklik);

                        oChildDinlendirmeVePaketleme.SetProperty("U_OnSekizNem", item.OnSekizNem);
                    } 
                     
                    try
                    {
                        oGeneralService.Update(oGeneralData);
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = 0, Description = "Tereyağ Günlük Analiz girişi güncellendi.", List = null };
                    }
                    catch (Exception)
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5300, Description = "Hata Kodu - 5300 Tereyağ Günlük Analiz girişi güncellenirken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
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