using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddProductionOrders
    {
        private int clnum = 0;

        public Response addProductionOrders(ProductionOrder productionOrder, string dbName,string mKodValue)
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
                ProductionOrders wo = (ProductionOrders)oCompany.GetBusinessObject(BoObjectTypes.oProductionOrders);

                wo.ItemNo = productionOrder.ItemCode;
                wo.DueDate = DateTime.Now;
                wo.ProductionOrderType = BoProductionOrderTypeEnum.bopotStandard;
                wo.PlannedQuantity = productionOrder.Miktar;
                int retVal = wo.Add();

                if (retVal == 0)
                {
                    wo.GetByKey(Convert.ToInt32(oCompany.GetNewObjectKey()));
                    wo.ProductionOrderStatus = BoProductionOrderStatusEnum.boposReleased;
                    wo.Update();

                    LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                    return new Response { Value = 0, Description = "Üretim siparişi başarıyla oluşturuldu.", List = null, DocEntry = Convert.ToInt32(oCompany.GetNewObjectKey()) };
                }
                else
                {
                    LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                    return new Response { Value = -7200, Description = "Hata Kodu : 7200 Üretim siparişi oluşturulurken hata oluştu." + oCompany.GetLastErrorDescription(), List = null };
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