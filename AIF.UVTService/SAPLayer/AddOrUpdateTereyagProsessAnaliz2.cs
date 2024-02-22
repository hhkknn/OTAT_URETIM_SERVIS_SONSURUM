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
    public class AddOrUpdateTereyagProsessAnaliz2
    {
        public Response addOrUpdateTereyagProsessAnaliz2(TereyagProsesTakipAnaliz2 tereyagProsesTakipAnaliz2, string dbName, string mKodValue)
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

                oRS.DoQuery("Select * from \"@AIF_TRYGPRSS2_ANLZ\" WITH (NOLOCK) where \"U_PartiNo\" = '" + tereyagProsesTakipAnaliz2.PartiNo + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    //GeneralData oChildMamulOzellikleri1;

                    //GeneralDataCollection oChildrenMamulOzellikleri1;

                    GeneralData oChildSarfMalzemeKullanim;

                    GeneralDataCollection oChildrenSarfMalzemeKullanim;

                    //GeneralData oChildDedektordenGecirilmeKontrolu;

                    //GeneralDataCollection oChildrenDedektordenGecirilmeKontrolu;

                    GeneralData oChildGramajKontrolu;

                    GeneralDataCollection oChildrenGramajKontrolu;

                    oCompService = oCompany.GetCompanyService();

                    oGeneralService = oCompService.GetGeneralService("AIF_TRYGPRSS2_ANLZ");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);


                    oGeneralData.SetProperty("U_PartiNo", tereyagProsesTakipAnaliz2.PartiNo.ToString());

                    oGeneralData.SetProperty("U_KalemKodu", tereyagProsesTakipAnaliz2.KalemKodu.ToString());

                    oGeneralData.SetProperty("U_KalemTanimi", tereyagProsesTakipAnaliz2.KalemTanimi.ToString());

                    oGeneralData.SetProperty("U_Aciklama", tereyagProsesTakipAnaliz2.Aciklama.ToString());

                    DateTime dt = new DateTime(Convert.ToInt32(tereyagProsesTakipAnaliz2.UretimTarihi.Substring(0, 4)), Convert.ToInt32(tereyagProsesTakipAnaliz2.UretimTarihi.Substring(4, 2)), Convert.ToInt32(tereyagProsesTakipAnaliz2.UretimTarihi.Substring(6, 2)));

                    oGeneralData.SetProperty("U_UretimTarihi", dt);

                    oGeneralData.SetProperty("U_PaketlemeTarihi", dt);


                    //oChildrenMamulOzellikleri1 = oGeneralData.Child("AIF_TRYGPRSS2_ANLZ1");

                    //foreach (var item in tereyagProsesTakipAnaliz2.tereyagMamul2Ozellikleri1_Detay)
                    //{
                    //    oChildMamulOzellikleri1 = oChildrenMamulOzellikleri1.Add();

                    //    oChildMamulOzellikleri1.SetProperty("U_UretilenUrunler", item.UretilenUrun);

                    //    oChildMamulOzellikleri1.SetProperty("U_PaketlemeOncesiSicakik", item.PaketlemeOncesiSicaklik);

                    //    oChildMamulOzellikleri1.SetProperty("U_UretimMiktari", item.UretimMiktari);

                    //    oChildMamulOzellikleri1.SetProperty("U_PaketlenenUrunMiktari", item.PaketlenenUrunMiktari);

                    //    oChildMamulOzellikleri1.SetProperty("U_FireUrunMiktari", item.FireUrunMiktari);

                    //    oChildMamulOzellikleri1.SetProperty("U_NumuneUrunMiktari", item.NumuneUrunMiktari);

                    //    oChildMamulOzellikleri1.SetProperty("U_DepoyaGirenUrunMik", item.DepoyaGirenUrunMiktari);

                    //    oChildMamulOzellikleri1.SetProperty("U_KuruMadde", item.KuruMadde);

                    //    oChildMamulOzellikleri1.SetProperty("U_YagOrani", item.YagOrani);

                    //    oChildMamulOzellikleri1.SetProperty("U_PH", item.PH);

                    //    oChildMamulOzellikleri1.SetProperty("U_SH", item.SH);

                    //    oChildMamulOzellikleri1.SetProperty("U_TuzOrani", item.TuzOrani);
                    //}

                    oChildrenSarfMalzemeKullanim = oGeneralData.Child("AIF_TRYGPRSS2_ANLZ2");

                    foreach (var item in tereyagProsesTakipAnaliz2.tereyag2SarfMalzemeKullanims_Detay)
                    {
                        oChildSarfMalzemeKullanim = oChildrenSarfMalzemeKullanim.Add();

                        oChildSarfMalzemeKullanim.SetProperty("U_MalzemeAdi", item.MalzemeAdi);

                        oChildSarfMalzemeKullanim.SetProperty("U_MalMarkaTedarikci", item.MalzemeMarkaTedarikcisi);

                        oChildSarfMalzemeKullanim.SetProperty("U_SarfMalzemePartiNo", item.SarfMalzemePartiNo);

                        oChildSarfMalzemeKullanim.SetProperty("U_Miktar", item.Miktar);

                        oChildSarfMalzemeKullanim.SetProperty("U_Birim", item.Birim);
                    }

                    //oChildrenDedektordenGecirilmeKontrolu = oGeneralData.Child("AIF_TRYGPRSS2_ANLZ3");

                    //foreach (var item in tereyagProsesTakipAnaliz2.tereyag2DedektorGecirilmeKontrols)
                    //{
                    //    oChildDedektordenGecirilmeKontrolu = oChildrenDedektordenGecirilmeKontrolu.Add();

                    //    oChildDedektordenGecirilmeKontrolu.SetProperty("U_DedektorGecirilmeKontrol", item.UretilenMetalDedektördenGecirilmeKontrolu);
                    //}

                    oChildrenGramajKontrolu = oGeneralData.Child("AIF_TRYGPRSS2_ANLZ4");

                    foreach (var item in tereyagProsesTakipAnaliz2.tereyag2GramajKontrols)
                    {
                        oChildGramajKontrolu = oChildrenGramajKontrolu.Add();

                        oChildGramajKontrolu.SetProperty("U_UrunCesidi", item.UrunCesidi);

                        oChildGramajKontrolu.SetProperty("U_PartiNo", item.PartiNo);

                        oChildGramajKontrolu.SetProperty("U_BirinciOrnek", item.BirinciOrnek);

                        oChildGramajKontrolu.SetProperty("U_IkinciOrnek", item.IkinciOrnek);

                        oChildGramajKontrolu.SetProperty("U_UcuncuOrnek", item.UcuncuOrnek);

                        oChildGramajKontrolu.SetProperty("U_DorduncuOrnek", item.DorduncuOrnek);

                        oChildGramajKontrolu.SetProperty("U_BesinciOrnek", item.BesinciOrnek);

                        oChildGramajKontrolu.SetProperty("U_AltinciOrnek", item.AltinciOrnek);

                        oChildGramajKontrolu.SetProperty("U_YedinciOrnek", item.YedinciOrnek);
                    }


                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_TRYGPRSS2_ANLZ\" WITH (NOLOCK)");

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
                        return new Response { Value = 0, Description = "Tereyağ Analiz 2 takibi girişi oluşturuldu.", List = null };
                    }
                    else

                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5200, Description = "Hata Kodu - 5200 Tereyağ Analiz 2 takibi girişi oluşturulurken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                    }
                }
                else
                {


                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralDataParams oGeneralParams;

                    //GeneralData oChildMamulOzellikleri1;

                    //GeneralDataCollection oChildrenMamulOzellikleri1;

                    GeneralData oChildSarfMalzemeKullanim;

                    GeneralDataCollection oChildrenSarfMalzemeKullanim;

                    //GeneralData oChildDedektordenGecirilmeKontrolu;

                    //GeneralDataCollection oChildrenDedektordenGecirilmeKontrolu;

                    GeneralData oChildGramajKontrolu;

                    GeneralDataCollection oChildrenGramajKontrolu;

                    oCompService = oCompany.GetCompanyService();

                    oGeneralService = oCompService.GetGeneralService("AIF_TRYGPRSS2_ANLZ");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    oGeneralData.SetProperty("U_PartiNo", tereyagProsesTakipAnaliz2.PartiNo.ToString());

                    oGeneralData.SetProperty("U_KalemKodu", tereyagProsesTakipAnaliz2.KalemKodu.ToString());

                    oGeneralData.SetProperty("U_KalemTanimi", tereyagProsesTakipAnaliz2.KalemTanimi.ToString());

                    oGeneralData.SetProperty("U_Aciklama", tereyagProsesTakipAnaliz2.Aciklama.ToString());

                    DateTime dt = new DateTime(Convert.ToInt32(tereyagProsesTakipAnaliz2.UretimTarihi.Substring(0, 4)), Convert.ToInt32(tereyagProsesTakipAnaliz2.UretimTarihi.Substring(4, 2)), Convert.ToInt32(tereyagProsesTakipAnaliz2.UretimTarihi.Substring(6, 2)));

                    oGeneralData.SetProperty("U_UretimTarihi", dt);

                    oGeneralData.SetProperty("U_PaketlemeTarihi", dt);


                    //oChildrenMamulOzellikleri1 = oGeneralData.Child("AIF_TRYGPRSS2_ANLZ1");

                    //if (oChildrenMamulOzellikleri1.Count > 0)
                    //{
                    //    int drc = oChildrenMamulOzellikleri1.Count;
                    //    for (int rmv = 0; rmv < drc; rmv++)
                    //        oChildrenMamulOzellikleri1.Remove(0);
                    //}

                    //foreach (var item in tereyagProsesTakipAnaliz2.tereyagMamul2Ozellikleri1_Detay)
                    //{
                    //    oChildMamulOzellikleri1 = oChildrenMamulOzellikleri1.Add();

                    //    oChildMamulOzellikleri1.SetProperty("U_UretilenUrunler", item.UretilenUrun);

                    //    oChildMamulOzellikleri1.SetProperty("U_PaketlemeOncesiSicakik", item.PaketlemeOncesiSicaklik);

                    //    oChildMamulOzellikleri1.SetProperty("U_UretimMiktari", item.UretimMiktari);

                    //    oChildMamulOzellikleri1.SetProperty("U_PaketlenenUrunMiktari", item.PaketlenenUrunMiktari);

                    //    oChildMamulOzellikleri1.SetProperty("U_FireUrunMiktari", item.FireUrunMiktari);

                    //    oChildMamulOzellikleri1.SetProperty("U_NumuneUrunMiktari", item.NumuneUrunMiktari);

                    //    oChildMamulOzellikleri1.SetProperty("U_DepoyaGirenUrunMik", item.DepoyaGirenUrunMiktari);

                    //    oChildMamulOzellikleri1.SetProperty("U_KuruMadde", item.KuruMadde);

                    //    oChildMamulOzellikleri1.SetProperty("U_YagOrani", item.YagOrani);

                    //    oChildMamulOzellikleri1.SetProperty("U_PH", item.PH);

                    //    oChildMamulOzellikleri1.SetProperty("U_SH", item.SH);

                    //    oChildMamulOzellikleri1.SetProperty("U_TuzOrani", item.TuzOrani);
                    //}

                    oChildrenSarfMalzemeKullanim = oGeneralData.Child("AIF_TRYGPRSS2_ANLZ2");

                    if (oChildrenSarfMalzemeKullanim.Count > 0)
                    {
                        int drc = oChildrenSarfMalzemeKullanim.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenSarfMalzemeKullanim.Remove(0);
                    }

                    foreach (var item in tereyagProsesTakipAnaliz2.tereyag2SarfMalzemeKullanims_Detay)
                    {
                        oChildSarfMalzemeKullanim = oChildrenSarfMalzemeKullanim.Add();

                        oChildSarfMalzemeKullanim.SetProperty("U_MalzemeAdi", item.MalzemeAdi);

                        oChildSarfMalzemeKullanim.SetProperty("U_MalMarkaTedarikci", item.MalzemeMarkaTedarikcisi);

                        oChildSarfMalzemeKullanim.SetProperty("U_SarfMalzemePartiNo", item.SarfMalzemePartiNo);

                        oChildSarfMalzemeKullanim.SetProperty("U_Miktar", item.Miktar);

                        oChildSarfMalzemeKullanim.SetProperty("U_Birim", item.Birim);
                    }

                    //oChildrenDedektordenGecirilmeKontrolu = oGeneralData.Child("AIF_TRYGPRSS2_ANLZ3");

                    //if (oChildrenDedektordenGecirilmeKontrolu.Count > 0)
                    //{
                    //    int drc = oChildrenDedektordenGecirilmeKontrolu.Count;
                    //    for (int rmv = 0; rmv < drc; rmv++)
                    //        oChildrenDedektordenGecirilmeKontrolu.Remove(0);
                    //}

                    //foreach (var item in tereyagProsesTakipAnaliz2.tereyag2DedektorGecirilmeKontrols)
                    //{
                    //    oChildDedektordenGecirilmeKontrolu = oChildrenDedektordenGecirilmeKontrolu.Add();

                    //    oChildDedektordenGecirilmeKontrolu.SetProperty("U_DedektorGecirilmeKontrol", item.UretilenMetalDedektördenGecirilmeKontrolu);
                    //}

                    oChildrenGramajKontrolu = oGeneralData.Child("AIF_TRYGPRSS2_ANLZ4");

                    if (oChildrenGramajKontrolu.Count > 0)
                    {
                        int drc = oChildrenGramajKontrolu.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenGramajKontrolu.Remove(0);
                    }

                    foreach (var item in tereyagProsesTakipAnaliz2.tereyag2GramajKontrols)
                    {
                        oChildGramajKontrolu = oChildrenGramajKontrolu.Add();

                        oChildGramajKontrolu.SetProperty("U_UrunCesidi", item.UrunCesidi);

                        oChildGramajKontrolu.SetProperty("U_PartiNo", item.PartiNo);

                        oChildGramajKontrolu.SetProperty("U_BirinciOrnek", item.BirinciOrnek);

                        oChildGramajKontrolu.SetProperty("U_IkinciOrnek", item.IkinciOrnek);

                        oChildGramajKontrolu.SetProperty("U_UcuncuOrnek", item.UcuncuOrnek);

                        oChildGramajKontrolu.SetProperty("U_DorduncuOrnek", item.DorduncuOrnek);

                        oChildGramajKontrolu.SetProperty("U_BesinciOrnek", item.BesinciOrnek);

                        oChildGramajKontrolu.SetProperty("U_AltinciOrnek", item.AltinciOrnek);

                        oChildGramajKontrolu.SetProperty("U_YedinciOrnek", item.YedinciOrnek);
                    }


                    try
                    {
                        oGeneralService.Update(oGeneralData);
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = 0, Description = "Tereyağ Analiz 2 Takibi girişi güncellendi.", List = null };
                    }
                    catch (Exception)
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -5300, Description = "Hata Kodu - 5300 Tereyağ Analiz 2 Takibi girişi güncellenirken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
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

        #region kapatılı-günlük analize taşındı 20230403
        //public Response addOrUpdateDinlendirmeVePaketleme(List<TereyagProsses2DinlendirmeVePaketleme> tereyagProsses2DinlendirmeVePaketlemes, string dbName, string mKodValue)
        //{
        //    Random rastgele = new Random();
        //    int ID = rastgele.Next(0, 9999);

        //    int clnum = 0;
        //    string dbCode = "";
        //    try
        //    {
        //        ConnectionList connection = new ConnectionList();

        //        LoginCompany log = new LoginCompany();

        //        log.DisconnectSAP(dbName);

        //        connection = log.getSAPConnection(dbName, ID);

        //        if (connection.number == -1)
        //        {
        //            LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
        //            return new Response { Value = -3100, Description = "Hata Kodu - 3100 Veritabanı bağlantısı sırasında hata oluştu. ", List = null };
        //        }

        //        clnum = connection.number;
        //        dbCode = connection.dbCode;

        //        SAPbobsCOM.Company oCompany = connection.oCompany;

        //        Recordset oRS = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

        //        DateTime dt = new DateTime(Convert.ToInt32(tereyagProsses2DinlendirmeVePaketlemes[0].UretimTarihi.Substring(0, 4)), Convert.ToInt32(tereyagProsses2DinlendirmeVePaketlemes[0].UretimTarihi.Substring(4, 2)), Convert.ToInt32(tereyagProsses2DinlendirmeVePaketlemes[0].UretimTarihi.Substring(6, 2)));

        //        //oRS.DoQuery("Select * from \"@AIF_TRYGDNPKT\" where Convert(varchar,\"U_UretimTarihi\",112) = '" + dt.ToString("yyyyMMdd") + "'");
        //        oRS.DoQuery("Delete from \"@AIF_TRYGDNPKT\" where Convert(varchar,\"U_UretimTarihi\",112) = '" + dt.ToString("yyyyMMdd") + "'");


        //        if (oRS.RecordCount == 0)
        //        {
        //            CompanyService oCompService = null;

        //            GeneralService oGeneralService;

        //            GeneralData oGeneralData;

        //            oCompService = oCompany.GetCompanyService();

        //            oGeneralService = oCompService.GetGeneralService("AIF_TRYGDNPKT");

        //            oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

        //            foreach (var item in tereyagProsses2DinlendirmeVePaketlemes)
        //            {
        //                oGeneralData.SetProperty("U_AlanAdi", item.AlanAdi);

        //                oGeneralData.SetProperty("U_UretimTarihi", dt);

        //                oGeneralData.SetProperty("U_SifirSekizSicaklik", item.SifirSekizSicaklik);

        //                oGeneralData.SetProperty("U_SifirSekizNem", item.SifirSekizNem);

        //                oGeneralData.SetProperty("U_OnikiSicaklik", item.OnikiSicaklik);

        //                oGeneralData.SetProperty("U_OnikiNem", item.OnikiNem);

        //                oGeneralData.SetProperty("U_OnBesSicaklik", item.OnBesSicaklik);

        //                oGeneralData.SetProperty("U_OnBesNem", item.OnBesNem);

        //                oGeneralData.SetProperty("U_OnSekizSicaklik", item.OnSekizSicaklik);

        //                oGeneralData.SetProperty("U_OnSekizNem", item.OnSekizNem);

        //                oGeneralService.Add(oGeneralData);

        //            }
        //        }


        //        LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
        //        return new Response { Value = 0, Description = "Dinlendirme ve paketleme odası girişi güncellendi.", List = null };
        //    }
        //    catch (Exception ex)
        //    {

        //        LoginCompany.ReleaseConnection(clnum, dbCode, ID);
        //        return new Response { Value = -9000, Description = "Bilinmeyen Hata oluştu. " + ex.Message, List = null };
        //    }

        //    finally
        //    {
        //        LoginCompany.ReleaseConnection(clnum, dbCode, ID);
        //    }
        //} 
        #endregion
    }
}