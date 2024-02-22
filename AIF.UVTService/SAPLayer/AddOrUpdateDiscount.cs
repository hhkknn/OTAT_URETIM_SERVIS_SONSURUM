using UVTService.Models;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;

namespace UVTService.SAPLayer
{
    public class AddOrUpdateDiscount
    {
        private int clnum;

        public Response addDiscount(Discount discount, string dbName, string mKodValue)
        {
            Random rastgele = new Random();
            int ID = rastgele.Next(0, 9999);

            string dbCode = "";
            string docentry = "";
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
                SAPbobsCOM.Recordset orec = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                int templateCode = 0;
                orec.DoQuery("Select * from OSPP WITH (NOLOCK) where CardCode = '" + discount.CardCode + "' and ItemCode = '" + discount.ItemCode + "'");

                if (orec.RecordCount == 0)
                {
                    LoginCompany.ReleaseConnection(clnum, connection.dbCode,ID);
                    return new Response { Value = -7777, Description = "Müşteriye veya ürün koduna ait indirim şablon bulunamamıştır.", List = null };
                }

                templateCode = orec.Fields.Item("U_TemplateCode").Value.ToString() == "" ? 0 : Convert.ToInt32(orec.Fields.Item("U_TemplateCode").Value);

                if (orec.RecordCount == 0)
                {
                    LoginCompany.ReleaseConnection(clnum, connection.dbCode,ID);
                    return new Response { Value = -7788, Description = "Ürün koduna ait indirim şablon bulunamamıştır.", List = null };
                }


                orec.DoQuery("Select * from \"@AIF_TMP_SLS_DISC1\" WITH (NOLOCK) where \"DocEntry\" = '" + templateCode + "'");

                XNamespace ns = "http://www.sap.com/SBO/SDK/DI";
                XDocument xDoc = XDocument.Parse(orec.GetFixedXML(SAPbobsCOM.RecordsetXMLModeEnum.rxmData));

                var rows = (from t in xDoc.Descendants(ns + "Row")
                            select new
                            {
                                DiscType = (from k in t.Descendants(ns + "Field") where k.Element(ns + "Alias").Value == "U_DiscType" select new XElement(k.Element(ns + "Value"))).First().Value,
                                Discount = (from k in t.Descendants(ns + "Field") where k.Element(ns + "Alias").Value == "U_DiscRate" select new XElement(k.Element(ns + "Value"))).First().Value
                            }).ToList();


                orec.DoQuery("Select * from \"@AIF_TMP_SLS_DISC\" WITH (NOLOCK) where \"DocEntry\" = '" + templateCode + "'");

                ns = "http://www.sap.com/SBO/SDK/DI";
                xDoc = XDocument.Parse(orec.GetFixedXML(SAPbobsCOM.RecordsetXMLModeEnum.rxmData));

                var header = (from t in xDoc.Descendants(ns + "Row")
                              select new
                              {
                                  PriceBefDisc = Helper.parseNumber.parservalues<double>((from k in t.Descendants(ns + "Field") where k.Element(ns + "Alias").Value == "U_PriceBefDisc" select new XElement(k.Element(ns + "Value"))).First().Value),
                                  TotalDisc = Helper.parseNumber.parservalues<double>((from k in t.Descendants(ns + "Field") where k.Element(ns + "Alias").Value == "U_TotalDisc" select new XElement(k.Element(ns + "Value"))).First().Value),
                                  DiscRate = Helper.parseNumber.parservalues<double>((from k in t.Descendants(ns + "Field") where k.Element(ns + "Alias").Value == "U_DiscRate" select new XElement(k.Element(ns + "Value"))).First().Value),
                                  PriceAfterDisc = Helper.parseNumber.parservalues<double>((from k in t.Descendants(ns + "Field") where k.Element(ns + "Alias").Value == "U_PriceAfterDisc" select new XElement(k.Element(ns + "Value"))).First().Value),
                              }).ToList();

                var oCompanyServiceData = oCompany.GetCompanyService();
                var oGeneralServiceData = oCompanyServiceData.GetGeneralService("AIF_SALES_DISC");
                SAPbobsCOM.GeneralDataCollection oChildren = null;
                SAPbobsCOM.GeneralData oChild = null;
                var oGeneralDataData = ((SAPbobsCOM.GeneralData)(oGeneralServiceData.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData)));

                orec.DoQuery("Select MAX(\"DocEntry\")+1 from \"@AIF_SALES_DISC\" WITH (NOLOCK)");

                docentry = orec.Fields.Item(0).Value.ToString();

                if (docentry == "")
                {
                    docentry = "1";
                }
                oGeneralDataData.SetProperty("DocNum", Convert.ToInt32(docentry));
                oGeneralDataData.SetProperty("U_PriceBefDisc", Helper.parseNumber.parservalues<double>(discount.PriceBefDisc.ToString()).ToString());

                oChildren = oGeneralDataData.Child("AIF_SALES_DISC1");
                int i = 0;
                double price = 0;
                double discountRateLine = 0;
                double sumtotaldiscount = 0;
                double sumpriceafterdisc = 0;
                double subtotal = 0;
                foreach (var item in rows)
                {
                    oChild = oChildren.Add();

                    discountRateLine = Helper.parseNumber.parservalues<double>(item.Discount.ToString());
                    if (i == 0)
                    {
                        oChild.SetProperty("U_SubTotal", discount.PriceBefDisc);
                    }
                    else
                    {
                        oChild.SetProperty("U_SubTotal", price);
                    }
                    oChild.SetProperty("U_DiscType", item.DiscType);
                    oChild.SetProperty("U_DiscRate", item.Discount);

                    if (item.DiscType == "1")
                    {
                        price = discount.PriceBefDisc;
                    }

                    price = (price / 100) * discountRateLine;
                    sumtotaldiscount += price;
                    oChild.SetProperty("U_DiscTotal", price);

                    if (i == 0)
                    {
                        price = discount.PriceBefDisc - price;
                    }
                    else
                    {
                        price = subtotal - price;
                    }

                    subtotal = price;
                    sumpriceafterdisc = subtotal;


                    oChild.SetProperty("U_SubTotal2", price);
                    i++;
                }


                oGeneralDataData.SetProperty("U_TotalDisc", sumtotaldiscount.ToString());
                oGeneralDataData.SetProperty("U_DiscRate", header.Select(x => x.DiscRate).FirstOrDefault());
                oGeneralDataData.SetProperty("U_PriceAfterDisc", subtotal);

                SAPbobsCOM.GeneralDataParams result = oGeneralServiceData.Add(oGeneralDataData);

                DataTable dt = new DataTable();
                dt.Columns.Add("DocEntry");
                dt.Rows.Add();
                dt.Rows[dt.Rows.Count - 1].SetField("DocEntry", docentry);

                dt.TableName = "BelgeNo";

                LoginCompany.ReleaseConnection(clnum, connection.dbCode,ID);
                return new Response { Value = 0, Description = "Indirim detayları eklendi.", List = dt };
            }
            catch (Exception ex)
            {
                LoginCompany.ReleaseConnection(clnum, dbCode,ID);
                return new Response { Value = -9999, Description = "Bilinmeyen bir hata oluştu." + ex.Message, List = null };
            }

            finally
            {
                LoginCompany.ReleaseConnection(clnum, dbCode, ID);
            }
        }
        public Response addDiscountManually(Discount discount, string dbName, string mKodValue)
        {
            Random rastgele = new Random();
            int ID = rastgele.Next(0, 9999);

            string dbCode = "";
            string docentry = "";
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
                SAPbobsCOM.Recordset orec = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);


                var oCompanyServiceData = oCompany.GetCompanyService();
                var oGeneralServiceData = oCompanyServiceData.GetGeneralService("AIF_SALES_DISC");
                SAPbobsCOM.GeneralDataCollection oChildren = null;
                SAPbobsCOM.GeneralData oChild = null;
                var oGeneralDataData = ((SAPbobsCOM.GeneralData)(oGeneralServiceData.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData)));

                orec.DoQuery("Select MAX(\"DocEntry\")+1 from \"@AIF_SALES_DISC\" WITH (NOLOCK)");

                docentry = orec.Fields.Item(0).Value.ToString();

                if (docentry == "")
                {
                    docentry = "1";
                }
                oGeneralDataData.SetProperty("DocNum", Convert.ToInt32(docentry));
                oGeneralDataData.SetProperty("U_PriceBefDisc", Helper.parseNumber.parservalues<double>(discount.PriceBefDisc.ToString()).ToString());

                oChildren = oGeneralDataData.Child("AIF_SALES_DISC1");
                foreach (var item in discount.DiscountDetails)
                {
                    oChild = oChildren.Add();


                    oChild.SetProperty("U_SubTotal", item.SubTotal);
                    oChild.SetProperty("U_DiscType", item.DiscType);
                    oChild.SetProperty("U_DiscRate", item.DiscRate);
                    oChild.SetProperty("U_DiscTotal", item.DiscTotal);
                    oChild.SetProperty("U_SubTotal2", item.SubTotal2);
                }


                oGeneralDataData.SetProperty("U_TotalDisc", discount.TotalDisc);
                oGeneralDataData.SetProperty("U_DiscRate", discount.DiscRate);
                oGeneralDataData.SetProperty("U_PriceAfterDisc", discount.PriceAfterDisc);

                SAPbobsCOM.GeneralDataParams result = oGeneralServiceData.Add(oGeneralDataData);

                DataTable dt = new DataTable();
                dt.Columns.Add("DocEntry");
                dt.Rows.Add();
                dt.Rows[dt.Rows.Count - 1].SetField("DocEntry", docentry);

                dt.TableName = "BelgeNo";

                LoginCompany.ReleaseConnection(clnum, connection.dbCode,ID);
                return new Response { Value = 0, Description = "Indirim detayları eklendi.", List = dt };
            }
            catch (Exception ex)
            {
                LoginCompany.ReleaseConnection(clnum, dbCode,ID);
                return new Response { Value = -9999, Description = "Bilinmeyen bir hata oluştu." + ex.Message, List = null };
            }

            finally
            {
                LoginCompany.ReleaseConnection(clnum, dbCode, ID);
            }
        }
        public Response updateDiscount(Discount discount, string dbName, string mKodValue)
        {
            Random rastgele = new Random();
            int ID = rastgele.Next(0, 9999);

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


                var oCompanyServiceData = oCompany.GetCompanyService();
                var oGeneralService = oCompanyServiceData.GetGeneralService("AIF_SALES_DISC");
                SAPbobsCOM.GeneralDataCollection oChildren = null;
                var oGeneralData = ((SAPbobsCOM.GeneralData)(oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData)));
                SAPbobsCOM.GeneralDataParams oGeneralParams;
                oGeneralParams = (SAPbobsCOM.GeneralDataParams)oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);
                oGeneralParams.SetProperty("DocEntry", discount.DiscountNo);
                oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                oGeneralData.SetProperty("U_PriceBefDisc", discount.PriceBefDisc);
                oGeneralData.SetProperty("U_TotalDisc", discount.TotalDisc);
                oGeneralData.SetProperty("U_DiscRate", discount.DiscRate);
                oGeneralData.SetProperty("U_PriceAfterDisc", discount.PriceAfterDisc);
                int i = 0;
                oChildren = oGeneralData.Child("AIF_SALES_DISC1");
                foreach (var item in discount.DiscountDetails)
                {
                    if (i >= oChildren.Count)
                    {
                        oChildren.Add();
                    }

                    oChildren.Item(i).SetProperty("U_SubTotal", item.SubTotal);
                    oChildren.Item(i).SetProperty("U_DiscType", item.DiscType);
                    oChildren.Item(i).SetProperty("U_DiscRate", item.DiscRate);
                    oChildren.Item(i).SetProperty("U_DiscTotal", item.DiscTotal);
                    oChildren.Item(i).SetProperty("U_SubTotal2", item.SubTotal2);
                    i++;
                }

                oCompany.StartTransaction();
                oGeneralService.Update(oGeneralData);

                int retval = 0;
                if (discount.DocEntry != "0")
                {
                    SAPbobsCOM.Documents oDocuments = (SAPbobsCOM.Documents)oCompany.GetBusinessObject((SAPbobsCOM.BoObjectTypes)Convert.ToInt32(discount.DocType));
                    oDocuments.GetByKey(Convert.ToInt32(discount.DocEntry));


                    //foreach (var item in discount.DiscountDetails)
                    //{
                    oDocuments.Lines.SetCurrentLine(discount.DiscountDetails[0].DocLine);
                    if (discount.DiscRate > 0)
                    {
                        oDocuments.Lines.DiscountPercent = discount.DiscRate;
                        oDocuments.Lines.UserFields.Fields.Item("U_AIF_DiscountNo").Value = discount.DiscountNo;
                    }
                    //} 
                    retval = oDocuments.Update();
                }


                if (oCompany.InTransaction)
                {
                    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                }
                LoginCompany.ReleaseConnection(connection.number, connection.dbCode,ID);

                if (retval != 0)
                {
                    LoginCompany.ReleaseConnection(clnum, connection.dbCode,ID);
                    return new Response { Value = -8889, Description = "Sap Belgesi güncellenirken hata oluştu.", List = null };
                }

                LoginCompany.ReleaseConnection(clnum, dbCode,ID);
                return new Response { Value = 0, Description = "İndirimler güncellendi.", List = null };
            }
            catch (Exception ex)
            {
                LoginCompany.ReleaseConnection(clnum, dbCode,ID);
                return new Response { Value = -8888, Description = "İndirimler güncellenirken hata oluştu." + ex.Message, List = null };
            }
            finally
            {
                LoginCompany.ReleaseConnection(clnum, dbCode, ID);
            }

        }
    }
}