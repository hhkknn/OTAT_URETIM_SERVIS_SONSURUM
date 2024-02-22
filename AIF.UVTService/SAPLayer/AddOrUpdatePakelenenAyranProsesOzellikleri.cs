using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddOrUpdatePakelenenAyranProsesOzellikleri
    {
        public Response addOrUpdatePakelenenAyranProsesOzellikleri(PaketlenenAyranProsesOzellikleri paketlenenAyranProsesOzellikleri, string dbName, string mKodValue)
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

                oRS.DoQuery("Select * from \"@AIF_AYRNPKTPRSS\" WITH (NOLOCK) where \"U_Tarih\" = '" + paketlenenAyranProsesOzellikleri.Tarih + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralData oChildDetay;

                    GeneralDataCollection oChildrenDetay;

                    oCompService = oCompany.GetCompanyService();

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_AYRNPKTPRSS");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    DateTime dt = new DateTime(Convert.ToInt32(paketlenenAyranProsesOzellikleri.Tarih.Substring(0, 4)), Convert.ToInt32(paketlenenAyranProsesOzellikleri.Tarih.Substring(4, 2)), Convert.ToInt32(paketlenenAyranProsesOzellikleri.Tarih.Substring(6, 2)));

                    oGeneralData.SetProperty("U_Tarih", dt);

                    oGeneralData.SetProperty("U_Aciklama", paketlenenAyranProsesOzellikleri.Aciklama);


                    oChildrenDetay = oGeneralData.Child("AIF_AYRNPKTPRSS1");

                    foreach (var item in paketlenenAyranProsesOzellikleri.paketlenenAyranProsesOzellikleri1s)
                    {
                        oChildDetay = oChildrenDetay.Add();

                        oChildDetay.SetProperty("U_KontrolNo", item.KontrolNo);

                        oChildDetay.SetProperty("U_1", item.bir);

                        oChildDetay.SetProperty("U_2", item.iki);

                        oChildDetay.SetProperty("U_3", item.uc);

                        oChildDetay.SetProperty("U_4", item.dort);

                        oChildDetay.SetProperty("U_5", item.bes);

                        oChildDetay.SetProperty("U_6", item.alti);

                        oChildDetay.SetProperty("U_7", item.yedi);

                        oChildDetay.SetProperty("U_8", item.sekiz);

                        oChildDetay.SetProperty("U_9", item.dokuz);

                        oChildDetay.SetProperty("U_10", item.on);

                        oChildDetay.SetProperty("U_11", item.onbir);

                        oChildDetay.SetProperty("U_12", item.oniki);

                        oChildDetay.SetProperty("U_13", item.onuc);

                        oChildDetay.SetProperty("U_14", item.ondort);

                        oChildDetay.SetProperty("U_15", item.onbes);

                        oChildDetay.SetProperty("U_16", item.onalti);
                    }


                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_AYRNPKTPRSS\" WITH (NOLOCK)");

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
                        return new Response { Value = 0, Description = "Paketlenen Ayran Proses Özellikleri Girişi Oluşturuldu..", List = null };
                    }
                    else

                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -82000, Description = "Hata Kodu - 8200 Paketlenen Ayran Proses Özellikleri Girişi Oluşturulurken Hata Oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                    }
                }
                else
                {
                    CompanyService oCompService;
                    oCompService = oCompany.GetCompanyService();

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralDataParams oGeneralParams;

                    GeneralData oChildDetay;

                    GeneralDataCollection oChildrenDetay;

                    oGeneralService = oCompService.GetGeneralService("AIF_AYRNPKTPRSS");

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    DateTime dt = new DateTime(Convert.ToInt32(paketlenenAyranProsesOzellikleri.Tarih.Substring(0, 4)), Convert.ToInt32(paketlenenAyranProsesOzellikleri.Tarih.Substring(4, 2)), Convert.ToInt32(paketlenenAyranProsesOzellikleri.Tarih.Substring(6, 2)));

                    oGeneralData.SetProperty("U_Tarih", dt);

                    oGeneralData.SetProperty("U_Aciklama", paketlenenAyranProsesOzellikleri.Aciklama);


                    oChildrenDetay = oGeneralData.Child("AIF_AYRNPKTPRSS1");

                    if (oChildrenDetay.Count > 0)
                    {
                        int drc = oChildrenDetay.Count;
                        for (int rmv = 0; rmv < drc; rmv++)
                            oChildrenDetay.Remove(0);
                    }

                    foreach (var item in paketlenenAyranProsesOzellikleri.paketlenenAyranProsesOzellikleri1s)
                    {
                        oChildDetay = oChildrenDetay.Add();

                        oChildDetay.SetProperty("U_KontrolNo", item.KontrolNo);

                        oChildDetay.SetProperty("U_1", item.bir);

                        oChildDetay.SetProperty("U_2", item.iki);

                        oChildDetay.SetProperty("U_3", item.uc);

                        oChildDetay.SetProperty("U_4", item.dort);

                        oChildDetay.SetProperty("U_5", item.bes);

                        oChildDetay.SetProperty("U_6", item.alti);

                        oChildDetay.SetProperty("U_7", item.yedi);

                        oChildDetay.SetProperty("U_8", item.sekiz);

                        oChildDetay.SetProperty("U_9", item.dokuz);

                        oChildDetay.SetProperty("U_10", item.on);

                        oChildDetay.SetProperty("U_11", item.onbir);

                        oChildDetay.SetProperty("U_12", item.oniki);

                        oChildDetay.SetProperty("U_13", item.onuc);

                        oChildDetay.SetProperty("U_14", item.ondort);

                        oChildDetay.SetProperty("U_15", item.onbes);

                        oChildDetay.SetProperty("U_16", item.onalti);
                    }

                    try
                    {
                        oGeneralService.Update(oGeneralData);
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = 0, Description = "Paketlenen Ayran Proses Özellikleri Girişi Güncellendi.", List = null };
                    }
                    catch (Exception)
                    {
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -8300, Description = "Hata Kodu - 8300 Paketlenen Ayran Proses Özellikleri Girişi Güncellenirken Hata Oluştu. " + oCompany.GetLastErrorDescription(), List = null };
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