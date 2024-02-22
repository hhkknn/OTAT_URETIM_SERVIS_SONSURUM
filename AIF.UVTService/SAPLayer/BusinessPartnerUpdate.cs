using UVTService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVTService.SAPLayer
{
    public class BusinessPartnerUpdate
    {
        public Response updateCustomerByCardCode(BusinessPartners data, string dbName)
        {
            Random rastgele = new Random();
            int ID = rastgele.Next(0, 9999);

            int clnum = 0;
            string dbCode = "";
            try
            {
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

                SAPbobsCOM.BusinessPartners oBP = (SAPbobsCOM.BusinessPartners)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);

                oBP.GetByKey(data.CardCode);

                oBP.CardName = data.CardName;
                int currentline = 0;
                foreach (var item in data.Address.OrderBy(x => x.LineNum))
                {
                    if (item.LineNum != "-1")
                    {
                        oBP.Addresses.SetCurrentLine(Convert.ToInt32(currentline));
                        oBP.Addresses.AddressName = item.Address;
                        if (item.Status == "D")
                        {
                            oBP.Addresses.Delete();
                            continue;
                        }
                        currentline += 1;
                    }
                    else
                    {
                        oBP.Addresses.Add();
                        oBP.Addresses.AddressName = item.Address;
                        oBP.Addresses.AddressType = item.AdresType == "S" ? SAPbobsCOM.BoAddressType.bo_ShipTo : SAPbobsCOM.BoAddressType.bo_BillTo;
                        //oBP.Addresses.SetCurrentLine(oBP.Addresses.Count - 1);
                    }
                    oBP.Addresses.Street = item.Street;
                    oBP.Addresses.Block = item.Block;
                    oBP.Addresses.ZipCode = item.ZipCode;
                    oBP.Addresses.County = item.County;
                    oBP.City = item.City;

                    //oBP.Update();
                    //oBP.GetByKey(data.CardCode);
                }

                currentline = 0;

                foreach (var item in data.Contacts.OrderBy(x => x.CnctCode))
                {
                    if (item.CnctCode != 0)
                    {
                        oBP.ContactEmployees.SetCurrentLine(currentline);
                        if (item.Status == "D")
                        {
                            oBP.ContactEmployees.Delete();
                            continue;
                        }
                        currentline += 1;
                    }
                    else
                    {
                        oBP.ContactEmployees.Add();
                    }

                    oBP.ContactEmployees.Name = item.CnctName;
                    oBP.ContactEmployees.FirstName = item.FirstName;
                    oBP.ContactEmployees.LastName = item.LastName;
                }

                var aa = oBP.Update();

                if (aa == 0)
                {
                    LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                    return new Response { Value = 0, Description = "Müşteri kaydı başarıyla güncellendi.", List = null };
                }
                else
                {
                    LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);
                    return new Response { Value = -1100, Description = "Hata Kodu - 1100 Müşteri kaydı güncellenirken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
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