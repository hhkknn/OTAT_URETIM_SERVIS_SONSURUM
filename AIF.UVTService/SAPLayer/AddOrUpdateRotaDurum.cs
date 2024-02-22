using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;

namespace UVTService.SAPLayer
{
    public class AddOrUpdateRotaDurum
    {
        public Response addOrUpdateRotaDurum(RotaDurum rotaDurum, string dbName, string mKodValue)
        {
            Random rastgele = new Random();
            int ID = rastgele.Next(0, 9999);
            Logger logger = LogManager.GetCurrentClassLogger();

            //var requestJson_New = JsonConvert.SerializeObject(protocol);

            //logger.Info(" ");

            logger.Info("ID: " + ID + " addOrUpdateRotaDurum Servisine Geldi.");
            //logger.Info("ID: " + ID + " ISTEK :" + requestJson_New);

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
                    logger.Fatal("ID: " + ID + " " + "Hata Kodu - 3100 Veritabanı bağlantısı sırasında hata oluştu.");
                    LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                    return new Response { Value = -3100, Description = "Hata Kodu - 3100 Veritabanı bağlantısı sırasında hata oluştu. ", List = null };
                }

                clnum = connection.number;
                dbCode = connection.dbCode;

                Company oCompany = connection.oCompany;

                logger.Info("ID: " + ID + " Şirket bağlantısını başarıyla geçtik. Bağlantı sağladığımız DB :" + oCompany.CompanyDB + " clnum: " + clnum);

                Recordset oRS = (Recordset)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

                oRS.DoQuery("Select \"DocEntry\" from \"@AIF_ROTA_DURUM\" WITH (NOLOCK) where \"U_PartiNo\" = '" + rotaDurum.PartiNo + "' and \"U_RotaKodu\" = '" + rotaDurum.RotaKodu + "'");

                if (oRS.RecordCount == 0) //Daha önce bu partiye kayıt girilmiş mi?
                {
                    CompanyService oCompService = null;

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    oCompService = oCompany.GetCompanyService();

                    //oCompany.StartTransaction();

                    oGeneralService = oCompService.GetGeneralService("AIF_ROTA_DURUM");

                    oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                    oGeneralData.SetProperty("U_UretimFisNo", rotaDurum.UretimFisNo.ToString());

                    oGeneralData.SetProperty("U_RotaKodu", rotaDurum.RotaKodu);

                    oGeneralData.SetProperty("U_PartiNo", rotaDurum.PartiNo);

                    oGeneralData.SetProperty("U_DurumKodu", rotaDurum.DurumKodu);

                    oGeneralData.SetProperty("U_DurunAciklama", rotaDurum.DurumAciklamasi);

                    oRS.DoQuery("Select ISNULL(MAX(\"DocEntry\"),0) + 1 from \"@AIF_ROTA_DURUM\" WITH (NOLOCK)");

                    int maxdocentry = Convert.ToInt32(oRS.Fields.Item(0).Value);

                    oGeneralData.SetProperty("DocNum", maxdocentry);

                    var resp = oGeneralService.Add(oGeneralData);

                    if (resp != null)
                    {
                        //if (oCompany.InTransaction)
                        //{
                        //    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                        //}
                        logger.Info("ID: " + ID + " " + "Rota Durumu Eklendi..");
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = 0, Description = "Rota Durumu Eklendi..", List = null };
                    }
                    else 
                    {
                        logger.Fatal("ID: " + ID + " " + "Hata Kodu - 1200 Rota Durumu eklenirken oluşturulurken hata oluştu. " + oCompany.GetLastErrorDescription());
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -1200, Description = "Hata Kodu - 1200 Rota Durumu eklenirken oluşturulurken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                    }


                }
                else
                {
                    CompanyService oCompService;
                    oCompService = oCompany.GetCompanyService();

                    GeneralService oGeneralService;

                    GeneralData oGeneralData;

                    GeneralDataParams oGeneralParams;

                    oGeneralService = oCompService.GetGeneralService("AIF_ROTA_DURUM");

                    oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                    oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(oRS.Fields.Item("DocEntry").Value));
                    oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                    oGeneralData.SetProperty("U_UretimFisNo", rotaDurum.UretimFisNo.ToString());

                    oGeneralData.SetProperty("U_RotaKodu", rotaDurum.RotaKodu);

                    oGeneralData.SetProperty("U_PartiNo", rotaDurum.PartiNo);

                    oGeneralData.SetProperty("U_DurumKodu", rotaDurum.DurumKodu);

                    oGeneralData.SetProperty("U_DurunAciklama", rotaDurum.DurumAciklamasi);

                    try
                    {
                        oGeneralService.Update(oGeneralData);
                        logger.Info("ID: " + ID + " " + "Rota Kodu güncellendi.");
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = 0, Description = "Rota Kodu güncellendi.", List = null };
                    }
                    catch (Exception)
                    {
                        logger.Fatal("ID: " + ID + " " + "Hata Kodu - 1300 Rota Kodu güncellenirken hata oluştu. " + oCompany.GetLastErrorDescription());
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                        return new Response { Value = -1300, Description = "Hata Kodu - 1300 Rota Kodu güncellenirken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                    } 
                }
            }
            catch (Exception ex)
            {
                logger.Fatal("ID: " + ID + " " + "Bilinmeyen Hata oluştu. " + ex.Message);
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