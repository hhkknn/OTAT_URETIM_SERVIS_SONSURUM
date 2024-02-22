using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class UpdateProductionOrders
    {
        private int clnum = 0;

        public Response updateProductionOrders(string dbName, int docnum, string duraklama,string mKodValue)
        {
            Random rastgele = new Random();
            int ID = rastgele.Next(0, 9999);

            ConnectionList connection = new ConnectionList();

            try
            {
                LoginCompany log = new SAPLayer.LoginCompany();

                log.DisconnectSAP(dbName);

                connection = log.getSAPConnection(dbName,ID);

                if (connection.number == -1)
                {
                    LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                    return new Response { Value = -3100, Description = "Hata Kodu - 3100 Veritabanı bağlantısı sırasında hata oluştu. ", List = null };
                }

                clnum = connection.number;

                Company oCompany = connection.oCompany;
                SAPbobsCOM.ProductionOrders oProductionOrders = (SAPbobsCOM.ProductionOrders)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oProductionOrders);

                oProductionOrders.GetByKey(Convert.ToInt32(docnum));

                oProductionOrders.UserFields.Fields.Item("U_DuraklamaSebep").Value = duraklama;

                int ret = oProductionOrders.Update();
                 
                if (ret == 0)
                { 

                    LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                    return new Response { Value = 0, Description = "Üretim siparişi başarıyla güncellendi.", List = null, DocEntry = Convert.ToInt32(oCompany.GetNewObjectKey()) };
                }
                else
                {
                    LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
                    return new Response { Value = -3100, Description = "Hata Kodu - 9100 Üretim siparişi güncellenirken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                }
            }
            catch (Exception ex)
            {
                LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                return new Response { Value = -9000, Description = "Bilinmeyen Hata oluştu. " + ex.Message, List = null };
            }
            finally
            {
                LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
            }
        }
    }
}