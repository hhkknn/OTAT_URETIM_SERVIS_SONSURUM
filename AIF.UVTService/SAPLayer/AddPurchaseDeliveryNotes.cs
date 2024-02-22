using UVTService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class AddPurchaseDeliveryNotes
    {
        public Response addPurchaseDeliveryNotes(PurchaseDeliveryNotes purchaseOrder, string dbName, string mKodValue)
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

                SAPbobsCOM.Documents oDocuments = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseDeliveryNotes);

                oDocuments.BPL_IDAssignedToInvoice = purchaseOrder.BPLId;
                oDocuments.CardCode = purchaseOrder.CardCode;
                oDocuments.DocDate = new DateTime(Convert.ToInt32(purchaseOrder.DocDate.Substring(0, 4)) + Convert.ToInt32(purchaseOrder.DocDate.Substring(4, 2)) + Convert.ToInt32(purchaseOrder.DocDate.Substring(6, 2)));
                oDocuments.DocDueDate = new DateTime(Convert.ToInt32(purchaseOrder.DocDate.Substring(0, 4)) + Convert.ToInt32(purchaseOrder.DocDate.Substring(4, 2)) + Convert.ToInt32(purchaseOrder.DocDate.Substring(6, 2)));
                oDocuments.TaxDate = new DateTime(Convert.ToInt32(purchaseOrder.DocDate.Substring(0, 4)) + Convert.ToInt32(purchaseOrder.DocDate.Substring(4, 2)) + Convert.ToInt32(purchaseOrder.DocDate.Substring(6, 2)));
                oDocuments.UserFields.Fields.Item("U_SutKabulId").Value = purchaseOrder.sutKabulId.ToString();
                oDocuments.UserFields.Fields.Item("U_KullaniciId").Value = purchaseOrder.userId.ToString();
                int i = 0;
                foreach (var item in purchaseOrder.Lines)
                {
                    oDocuments.Lines.ItemCode = item.ItemCode;
                    oDocuments.Lines.Quantity = item.Quantity;

                    if (item.WareHouse != null || item.WareHouse != "")
                    {
                        oDocuments.Lines.WarehouseCode = item.WareHouse;
                    }

                    foreach (var itemPrt in item.PartiDetails)
                    {
                        oDocuments.Lines.BatchNumbers.Add();
                        oDocuments.Lines.BatchNumbers.SetCurrentLine(i);
                        oDocuments.Lines.BatchNumbers.BatchNumber = itemPrt.BatchNumber;
                        oDocuments.Lines.BatchNumbers.Quantity = itemPrt.Quantity;
                        i++;
                    }

                    i = 0;

                    oDocuments.Lines.Add();
                }

                int retval = oDocuments.Add();

                if (retval == 0)
                {
                    LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                    return new Response { Value = 0, Description = "Satınalma siparişi başarıyla oluşturuldu.", List = null, DocEntry = Convert.ToInt32(oCompany.GetNewObjectKey()) };
                }
                else
                {
                    LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                    return new Response { Value = -1200, Description = "Hata Kodu : -1200 Satınalma siparişi oluşturulurken hata oluştu." + oCompany.GetLastErrorDescription(), List = null };
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

            return null;
        }
    }
}