using UVTService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using NLog;

namespace UVTService.SAPLayer
{
    public class AddInventoryGenExist
    {
        public Response addInventoryGenExist(List<InventoryGenExist> goodIssues, string dbName, string mKodValue)
        {
            int clnum = 0;
            string dbCode = "";

            Random rastgele = new Random();
            int ID = rastgele.Next(0, 9999);
            Logger logger = LogManager.GetCurrentClassLogger();

            //var requestJson_New = JsonConvert.SerializeObject(protocol);

            //logger.Info(" ");

            logger.Info("ID: " + ID + " addInventoryGenExist Servisine Geldi.");
            //logger.Info("ID: " + ID + " ISTEK :" + requestJson_New);

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

                SAPbobsCOM.Company oCompany = connection.oCompany;
                logger.Info("ID: " + ID + " Şirket bağlantısını başarıyla geçtik. Bağlantı sağladığımız DB :" + oCompany.CompanyDB + " clnum: " + clnum);
                SAPbobsCOM.Documents oGenExist = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInventoryGenExit);

                oGenExist.DocDate = DateTime.Now;
                oGenExist.BPL_IDAssignedToInvoice = 1;
                oGenExist.UserFields.Fields.Item("U_RotaCode").Value = goodIssues[0].RotaKodu == null ? "" : goodIssues[0].RotaKodu;
                oGenExist.UserFields.Fields.Item("U_BatchNumber").Value = goodIssues[0].PartiNo == null ? "" : goodIssues[0].PartiNo;
                int i = 0;
                foreach (var item in goodIssues)
                {
                    oGenExist.Lines.BaseType = 202;
                    oGenExist.Lines.BaseEntry = item.UretimSiparisi;
                    oGenExist.Lines.BaseLine = item.SatirNumarasi;
                    oGenExist.Lines.Quantity = item.Miktar;

                    if (item.DepoKodu != null || item.DepoKodu != "")
                    {
                        oGenExist.Lines.WarehouseCode = item.DepoKodu;
                    }

                    foreach (var itemx in item.Parti)
                    {
                        if (i != 0)
                        {
                            oGenExist.Lines.BatchNumbers.Add();
                        }
                        oGenExist.Lines.BatchNumbers.SetCurrentLine(i);
                        oGenExist.Lines.BatchNumbers.BatchNumber = itemx.PartiNo;
                        oGenExist.Lines.BatchNumbers.Quantity = itemx.PartikMiktar;
                        i++;
                    }

                    oGenExist.Lines.Add();

                    i = 0;
                }

                var aa = oGenExist.Add();

                if (aa == 0)
                {
                    logger.Info("ID: " + ID + " " + "Üretim için çıkış başarıyla oluşturuldu.");
                    LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                    return new Response { Value = 0, Description = "Üretim için çıkış başarıyla oluşturuldu.", List = null, DocEntry = Convert.ToInt32(oCompany.GetNewObjectKey()) };
                }
                else

                {
                    logger.Fatal("ID: " + ID + " " + "Hata Kodu - 4100 Üretim için çıkış oluşturulurken hata oluştu. " + oCompany.GetLastErrorDescription());
                    LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                    return new Response { Value = -4100, Description = "Hata Kodu - 4100 Üretim için çıkış oluşturulurken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                }
            }
            catch (Exception ex)
            {
                logger.Fatal("ID: " + ID + " " + "Bilinmeyen hata oluştu. " + ex.Message);
                LoginCompany.ReleaseConnection(clnum, dbCode,ID);
                return new Response { Value = 9000, Description = "Bilinmeyen hata oluştu. " + ex.Message, List = null };
            }

            finally
            {
                LoginCompany.ReleaseConnection(clnum, dbCode, ID);
            }
        }
    }
}