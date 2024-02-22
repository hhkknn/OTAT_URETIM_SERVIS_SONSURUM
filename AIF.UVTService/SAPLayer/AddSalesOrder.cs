using UVTService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddSalesOrder
    {
        public Response addSalesOrder(Orders orders, string dbName,string mKodValue)
        {
            Random rastgele = new Random();
            int ID = rastgele.Next(0, 9999);

            int clnum = 0;
            string dbCode = "";
            try
            {
                //SAPbobsCOM.GeneralData oGeneralData;
                //SAPbobsCOM.GeneralService oGeneralService;
                //SAPbobsCOM.CompanyService oCompService = null;

                ConnectionList connection = new ConnectionList();

                SAPLayer.LoginCompany log = new SAPLayer.LoginCompany();

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

                SAPbobsCOM.Documents oDocuments = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders);

                oDocuments.CardCode = orders.CardCode;
                DateTime dt = DateTime.Now;

                if (orders.DocDate != null)
                {
                    dt = new DateTime(Convert.ToInt32(orders.DocDate.Substring(0, 4)), Convert.ToInt32(orders.DocDate.Substring(4, 2)), Convert.ToInt32(orders.DocDate.Substring(6, 2)));
                }

                DateTime dtDocDueDate = DateTime.Now;

                if (orders.DocDueDate != null)
                {
                    dtDocDueDate = new DateTime(Convert.ToInt32(orders.DocDueDate.Substring(0, 4)), Convert.ToInt32(orders.DocDueDate.Substring(4, 2)), Convert.ToInt32(orders.DocDueDate.Substring(6, 2)));
                }




                oDocuments.DocDate = dt;
                oDocuments.DocDueDate = dtDocDueDate;
                oDocuments.TaxDate = dt;
                oDocuments.BPL_IDAssignedToInvoice = 1;


                foreach (var item in orders.Lines)
                {
                    oDocuments.Lines.ItemCode = item.ItemCode;
                    oDocuments.Lines.Quantity = Convert.ToDouble(item.Quantity);
                    oDocuments.Lines.UnitPrice = Convert.ToDouble(item.Price);
                    oDocuments.Lines.DiscountPercent = Convert.ToDouble(item.DiscountPercent);
                    oDocuments.Lines.UserFields.Fields.Item("U_AIF_DiscountNo").Value = item.DiscountNo.ToString();
                    oDocuments.Lines.Add();
                }

                var aa = oDocuments.Add();

                if (aa == 0)
                {
                    LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                    return new Response { Value = 0, Description = "Satış siparişi başarıyla oluşturuldu.", List = null };
                }
                else

                {
                    LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                    return new Response { Value = -2100, Description = "Hata Kodu - 2100 Satış siparişi oluşturulurken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                }

            }
            catch (Exception ex)
            {
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