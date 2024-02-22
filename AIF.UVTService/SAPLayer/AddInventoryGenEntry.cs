using UVTService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;

namespace UVTService.SAPLayer
{
    public class AddInventoryGenEntry
    {
        public Response addInventoryGenEntry(List<InventoryGenEntry> inventoryGenEntries, string dbName, string mKodValue)
        {
            int clnum = 0;
            SAPbobsCOM.Company oCompany = null;
            string dbCode = "";

            Random rastgele = new Random();
            int ID = rastgele.Next(0, 9999);
            Logger logger = LogManager.GetCurrentClassLogger();

            //var requestJson_New = JsonConvert.SerializeObject(protocol);

            //logger.Info(" ");

            logger.Info("ID: " + ID + " addOrUpdateInventoryGenEntry Servisine Geldi.");
            //logger.Info("ID: " + ID + " ISTEK :" + requestJson_New);

            try
            {
                ConnectionList connection = new ConnectionList();

                SAPLayer.LoginCompany log = new SAPLayer.LoginCompany();

                log.DisconnectSAP(dbName);

                connection = log.getSAPConnection(dbName, ID);

                if (connection.number == -1)
                {
                    logger.Fatal("ID: " + ID + " " + "Hata Kodu - 3100 Veritabanı bağlantısı sırasında hata oluştu.");
                    LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
                    return new Response { Value = -3100, Description = "Hata Kodu - 3100 Veritabanı bağlantısı sırasında hata oluştu. ", List = null };
                }

                clnum = connection.number;
                dbCode = connection.dbCode;

                oCompany = connection.oCompany;

                string numunedepo = "";
                string firedepo = "";
                if (inventoryGenEntries.Where(x => x.NumuneMiktar > 0).Count() > 0)
                {

                    string value = inventoryGenEntries.Where(x => x.UrunKodu != "").Select(y => y.UrunKodu).FirstOrDefault().ToString();

                    SAPbobsCOM.Recordset oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRS.DoQuery("Select \"U_UVTNumuneDepo\" from OITM WITH (NOLOCK) where \"ItemCode\" = '" + value + "'");

                    numunedepo = oRS.Fields.Item(0).Value.ToString();

                    if (numunedepo == "")
                    {
                        logger.Fatal("ID: " + ID + " " + "Hata Kodu - 5100 Numune için " + value + " için Numune depo seçimi yapılmamıştır.");
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
                        return new Response { Value = -3100, Description = "Hata Kodu - 5100 Numune için " + value + " için Numune depo seçimi yapılmamıştır. ", List = null };
                    }
                }

                if (inventoryGenEntries.Where(x => x.FireMiktar > 0).Count() > 0)
                {

                    string value = inventoryGenEntries.Where(x => x.UrunKodu != "").Select(y => y.UrunKodu).FirstOrDefault().ToString();

                    SAPbobsCOM.Recordset oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRS.DoQuery("Select \"U_UVTFireDepo\" from OITM WITH (NOLOCK) where \"ItemCode\" = '" + value + "'");

                    firedepo = oRS.Fields.Item(0).Value.ToString();

                    if (firedepo == "")
                    {
                        logger.Fatal("ID: " + ID + " " + "Hata Kodu - 5100 Numune için " + value + " için Numune depo seçimi yapılmamıştır.");
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
                        return new Response { Value = -3100, Description = "Hata Kodu - 5100 Numune için " + value + " için Numune depo seçimi yapılmamıştır. ", List = null };
                    }
                }

                logger.Info("ID: " + ID + " Şirket bağlantısını başarıyla geçtik. Bağlantı sağladığımız DB :" + oCompany.CompanyDB + " clnum: " + clnum);

                //oCompany.StartTransaction();
                SAPbobsCOM.Documents oGenEntry = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInventoryGenEntry);

                oGenEntry.DocDate = DateTime.Now;
                oGenEntry.BPL_IDAssignedToInvoice = 1;
                int i = 0;
                bool check = false;
                foreach (var item in inventoryGenEntries.Where(x => x.Miktar > 0))
                {
                    oGenEntry.Lines.BaseType = 202;
                    oGenEntry.Lines.BaseEntry = item.UretimSiparisi;
                    //oGenEntry.Lines.BaseLine = 0;// item.SatirNumarasi;
                    oGenEntry.Lines.Quantity = item.Miktar;
                    oGenEntry.Lines.TransactionType = SAPbobsCOM.BoTransactionTypeEnum.botrntComplete;

                    if (item.DepoKodu != null && item.DepoKodu != "")
                    {
                        oGenEntry.Lines.WarehouseCode = item.DepoKodu;
                    }

                    //foreach (var itemx in item.Parti)
                    //{
                    //if (i != 0)
                    //{
                    //oGenEntry.Lines.BatchNumbers.Add();
                    //}
                    oGenEntry.Lines.BatchNumbers.SetCurrentLine(i);
                    oGenEntry.Lines.BatchNumbers.BatchNumber = item.Parti;
                    oGenEntry.Lines.BatchNumbers.Quantity = item.PartiMiktar;
                    if (item.SKTGun > 0 && item.SKTGun.ToString() != "")
                    {
                        oGenEntry.Lines.BatchNumbers.ExpiryDate = DateTime.Now.AddDays(item.SKTGun);
                    }

                    #region sipariş tarihi yılı ve günü olduğu mnfserial = parti niteliği 1 alanına gönderilir.

                    //if (mKodValue == "20URVT")
                    //{
                    //    //DateTime bugun = DateTime.Now;
                    //    //string kacincigun = bugun.DayOfYear.ToString();

                    //    if (item.UretimBaslangicTarihi != "")
                    //    {
                    //        string yilveGun = item.UretimBaslangicTarihi.Substring(0, 4) + "-" + item.UretimBaslangicTarihi.Substring(6, 2);
                    //        oGenEntry.Lines.BatchNumbers.ManufacturerSerialNumber = yilveGun;
                    //    }
                    //}

                    if (mKodValue == "20URVT")
                    {
                        //DateTime bugun = DateTime.Now;

                        #region üretim sipariş tarihine göre hesaplama yapıldı

                        DateTime siparisTarihi = item.SiparisTarihi;
                        //DateTime siparisTarihi = new DateTime(Convert.ToInt32(item.SiparisTarihi.Substring(0,4)), Convert.ToInt32(item.SiparisTarihi.Substring(4, 2)), Convert.ToInt32(item.SiparisTarihi.Substring(6, 2)));

                        #endregion üretim sipariş tarihine göre hesaplama yapıldı

                        string kacincigun = siparisTarihi.DayOfYear.ToString();

                        if (item.UretimBaslangicTarihi != "")
                        {
                            string partiNiteligi = siparisTarihi.Year + "-" + kacincigun;

                            oGenEntry.Lines.BatchNumbers.ManufacturerSerialNumber = partiNiteligi;
                        }
                    }

                    #endregion sipariş tarihi yılı ve günü olduğu mnfserial = parti niteliği 1 alanına gönderilir.

                    //i++;
                    //}

                    oGenEntry.Lines.Add();

                    check = true;
                }

                int aa = 0;
                int retval = 0;

                if (check)
                {
                    oGenEntry.UserFields.Fields.Item("U_RotaCode").Value = inventoryGenEntries[0].RotaKodu;
                    oGenEntry.UserFields.Fields.Item("U_BatchNumber").Value = inventoryGenEntries[0].Parti;
                    aa = oGenEntry.Add();

                    if (aa != 0)
                    {
                        string hata = oCompany.GetLastErrorDescription();

                        //try
                        //{
                        //    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);

                        //}
                        //catch (Exception)
                        //{
                        //}
                        logger.Fatal("ID: " + ID + " " + "Hata Kodu - 4100 Üretimden giriş oluşturulurken hata oluştu." + hata);
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
                        return new Response { Value = -6100, Description = "Hata Kodu - 4100 Üretimden giriş oluşturulurken hata oluştu. " + hata, List = null };
                    }
                }
                i = 0;

                //if (inventoryGenEntries.Where(z => z.Miktar > 0).Count() > 0)
                //{
                check = false;
                oGenEntry = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInventoryGenEntry);

                oGenEntry.DocDate = DateTime.Now;
                oGenEntry.BPL_IDAssignedToInvoice = 1;

                oGenEntry.UserFields.Fields.Item("U_RotaCode").Value = inventoryGenEntries[0].RotaKodu;
                oGenEntry.UserFields.Fields.Item("U_BatchNumber").Value = inventoryGenEntries[0].Parti;
                foreach (var item in inventoryGenEntries.Where(x => x.Miktar < 0))
                {
                    oGenEntry.Lines.BaseType = 202;
                    oGenEntry.Lines.BaseEntry = item.UretimSiparisi;

                    if (item.SiraNo != "")
                    {
                        oGenEntry.Lines.BaseLine = Convert.ToInt32(item.SiraNo);
                    }
                    oGenEntry.Lines.Quantity = item.Miktar * -1;

                    #region Kalem anaverisi üzerinde Rayiç bedel alanı açıldı.üretimden giriş yapılırken bu fiyat dolu ise üretimden giriş ekranındaki Birim Fiyat alanına gönderir.

                    if (item.RayicBedel > 0 && item.RayicBedel.ToString() != "")
                    {
                        oGenEntry.Lines.UnitPrice = item.RayicBedel;
                    }

                    #endregion Kalem anaverisi üzerinde Rayiç bedel alanı açıldı.üretimden giriş yapılırken bu fiyat dolu ise üretimden giriş ekranındaki Birim Fiyat alanına gönderir.

                    //oGenEntry.Lines.TransactionType = SAPbobsCOM.BoTransactionTypeEnum.botrntComplete;

                    //foreach (var itemx in item.Parti)
                    //{
                    //if (i != 0)
                    //{
                    //
                    //}
                    oGenEntry.Lines.BatchNumbers.Add();
                    oGenEntry.Lines.BatchNumbers.SetCurrentLine(i);
                    oGenEntry.Lines.BatchNumbers.BatchNumber = item.Parti;
                    oGenEntry.Lines.BatchNumbers.Quantity = item.PartiMiktar * -1;
                    if (item.SKTGun > 0 && item.SKTGun.ToString() != "")
                    {
                        oGenEntry.Lines.BatchNumbers.ExpiryDate = DateTime.Now.AddDays(item.SKTGun);
                    }
                    i++;
                    //}

                    #region sipariş yılı ve günü olduğu mnfserial = parti niteliği 1 alanına gönderilir.

                    //if (mKodValue == "20URVT")
                    //{
                    //    //DateTime bugun = DateTime.Now;
                    //    //string kacincigun = bugun.DayOfYear.ToString();

                    //    if (item.UretimBaslangicTarihi != "")
                    //    {
                    //        string yilveGun = item.UretimBaslangicTarihi.Substring(0, 4) + "-" + item.UretimBaslangicTarihi.Substring(6, 2);
                    //        oGenEntry.Lines.BatchNumbers.ManufacturerSerialNumber = yilveGun;
                    //    }
                    //}

                    if (mKodValue == "20URVT")
                    {
                        //DateTime bugun = DateTime.Now;

                        #region üretim sipariş tarihine göre hesaplama yapıldı

                        DateTime siparisTarihi = item.SiparisTarihi;
                        //DateTime siparisTarihi = new DateTime(Convert.ToInt32(item.SiparisTarihi.Substring(0, 4)), Convert.ToInt32(item.SiparisTarihi.Substring(4, 2)), Convert.ToInt32(item.SiparisTarihi.Substring(6, 2)));

                        #endregion üretim sipariş tarihine göre hesaplama yapıldı

                        string kacincigun = siparisTarihi.DayOfYear.ToString();

                        if (item.UretimBaslangicTarihi != "")
                        {
                            string partiNiteligi = siparisTarihi.Year + "-" + kacincigun;

                            oGenEntry.Lines.BatchNumbers.ManufacturerSerialNumber = partiNiteligi;
                        }
                    }

                    #endregion sipariş yılı ve günü olduğu mnfserial = parti niteliği 1 alanına gönderilir.

                    oGenEntry.Lines.Add();

                    check = true;
                }
                if (check)
                {
                    aa = oGenEntry.Add();
                }
                //}

                if (inventoryGenEntries.Where(x => x.Miktar > 0).Count() > 0)
                {
                    if (aa == 0)
                    {
                        string value = inventoryGenEntries.Where(x => x.UrunKodu != "").Select(y => y.UrunKodu).FirstOrDefault().ToString();
                        string varsayilandepo = inventoryGenEntries.Where(y => y.StokNakliHedefDepo != "").Select(z => z.StokNakliHedefDepo).FirstOrDefault();
                        //if (value.StartsWith("MAM") && varsayilandepo != "" && varsayilandepo != null)
                        if (varsayilandepo != "" && varsayilandepo != null)
                        {
                            #region üretim depo içerisinden stoğun ilgili deposuna gönderilmesi için taslak stok nakli oluşturma.

                            SAPbobsCOM.StockTransfer oDocuments = (SAPbobsCOM.StockTransfer)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInventoryTransferRequest);
                            //oDocuments.DocObjectCode = SAPbobsCOM.BoObjectTypes.oStockTransfer;

                            oDocuments.DocDate = DateTime.Now;
                            oDocuments.UserFields.Fields.Item("U_UretimdenGonderildi").Value = "Y";

                            string uretimSiparisi = inventoryGenEntries.Where(x => x.Miktar > 0).Select(y => y.UretimSiparisi).FirstOrDefault().ToString();

                            SAPbobsCOM.Recordset oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                            oRS.DoQuery("Select \"WareHouse\" from OWOR WITH (NOLOCK) where \"DocNum\" = " + uretimSiparisi + "");

                            var stoknakiKaynakDepo = oRS.Fields.Item(0).Value.ToString();
                            var stoknakiHedefDepo = inventoryGenEntries.Where(x => x.Miktar > 0 && x.StokNakliHedefDepo != "").Select(y => y.StokNakliHedefDepo).FirstOrDefault().ToString();

                            oDocuments.FromWarehouse = stoknakiKaynakDepo;

                            if (stoknakiHedefDepo != null && stoknakiHedefDepo != "")
                            {
                                oDocuments.ToWarehouse = stoknakiHedefDepo;
                            }

                            oDocuments.DueDate = DateTime.Now;

                            #endregion üretim depo içerisinden stoğun ilgili deposuna gönderilmesi için taslak stok nakli oluşturma.

                            #region üretim depo içerisinden stoğun ilgili deposuna gönderilmesi için taslak stok nakli oluşturma.

                            oDocuments.Lines.ItemCode = inventoryGenEntries.Where(x => x.UrunKodu != "").Select(y => y.UrunKodu).FirstOrDefault().ToString();
                            oDocuments.Lines.Quantity = inventoryGenEntries[0].Miktar - inventoryGenEntries[0].FireMiktar - inventoryGenEntries[0].NumuneMiktar;

                            //foreach (var aifteam in item.PartiInventoryGenEntryLinesBatchMiktar)
                            //{
                            //oDocuments.Lines.BatchNumbers.Add();
                            //oDocuments.Lines.BatchNumbers.SetCurrentLine(i);
                            //oDocuments.Lines.BatchNumbers.BatchNumber = inventoryGenEntries[0].Parti;
                            //oDocuments.Lines.BatchNumbers.Quantity = inventoryGenEntries[0].PartiMiktar;
                            //    i++;
                            //}

                            oDocuments.Lines.Add();

                            #endregion üretim depo içerisinden stoğun ilgili deposuna gönderilmesi için taslak stok nakli oluşturma.

                            aa = oDocuments.Add();

                            if (aa != 0)
                            {
                                string hata = oCompany.GetLastErrorDescription();
                                //if (oCompany.InTransaction)
                                //{
                                //    try
                                //    {
                                //        oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                                //    }
                                //    catch (Exception)
                                //    {
                                //    }
                                //}
                                logger.Fatal("ID: " + ID + " " + "Hata Kodu - 4100 Stok nakli talebi oluşturulurken hata oluştu." + hata);
                                LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
                                return new Response { Value = -3100, Description = "Hata Kodu - 4100 Stok nakli talebi oluşturulurken hata oluştu. " + hata, List = null };
                            }
                        }
                    }
                }

                if (inventoryGenEntries.Where(x => x.FireMiktar > 0).Count() > 0)
                {
                    //if (aa == 0)
                    //{
                    string value = inventoryGenEntries.Where(x => x.UrunKodu != "").Select(y => y.UrunKodu).FirstOrDefault().ToString();

                    //if (value.StartsWith("MAM"))
                    //{

                    #region Fire girilirse SAP'de stok nakli oluşur.

                    SAPbobsCOM.StockTransfer oDocuments = (SAPbobsCOM.StockTransfer)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInventoryTransferRequest);
                    //oDocuments.DocObjectCode = SAPbobsCOM.BoObjectTypes.oStockTransfer;

                    oDocuments.DocDate = DateTime.Now;
                    oDocuments.UserFields.Fields.Item("U_UretimdenGonderildi").Value = "Y";

                    string uretimSiparisi = inventoryGenEntries.Where(x => x.Miktar > 0).Select(y => y.UretimSiparisi).FirstOrDefault().ToString();

                    SAPbobsCOM.Recordset oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRS.DoQuery("Select \"WareHouse\" from OWOR WITH (NOLOCK) where \"DocNum\" = " + uretimSiparisi + "");

                    var stoknakiKaynakDepo = oRS.Fields.Item(0).Value.ToString();

                    oDocuments.FromWarehouse = stoknakiKaynakDepo;

                    //var stoknakiHedefDepo = inventoryGenEntries.Where(x => x.Miktar > 0 && x.StokNakliHedefDepo != "").Select(y => y.StokNakliHedefDepo).FirstOrDefault().ToString();
                    #region old
                    //string stoknakiHedefDepo = "";
                    //oRS.DoQuery("Select \"U_UVTFireDepo\" from OITM WITH (NOLOCK) where \"ItemCode\" = '" + value + "'");

                    //stoknakiHedefDepo = oRS.Fields.Item(0).Value.ToString();

                    //if (stoknakiHedefDepo == "")
                    //{
                    //    //if (oCompany.InTransaction)
                    //    //{
                    //    //    try
                    //    //    {
                    //    //        oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                    //    //    }
                    //    //    catch (Exception)
                    //    //    {
                    //    //    }
                    //    //}
                    //    logger.Fatal("ID: " + ID + " " + "Hata Kodu - 5100 Fire için " + value + " için Fire depo seçimi yapılmamıştır.");
                    //    LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
                    //    return new Response { Value = -3100, Description = "Hata Kodu - 5100 Fire için " + value + " için Fire depo seçimi yapılmamıştır. ", List = null };
                    //} 
                    #endregion

                    if (firedepo != null && firedepo != "")
                    {
                        oDocuments.ToWarehouse = firedepo;
                    }

                    oDocuments.DueDate = DateTime.Now;

                    #endregion Fire girilirse SAP'de stok nakli oluşur.

                    #region üretim depo içerisinden stoğun ilgili deposuna gönderilmesi için taslak stok nakli oluşturma.

                    oDocuments.Lines.ItemCode = inventoryGenEntries.Where(x => x.UrunKodu != "").Select(y => y.UrunKodu).FirstOrDefault().ToString();
                    oDocuments.Lines.Quantity = inventoryGenEntries[0].FireMiktar;

                    //foreach (var aifteam in item.PartiInventoryGenEntryLinesBatchMiktar)
                    //{
                    //oDocuments.Lines.BatchNumbers.Add();
                    //oDocuments.Lines.BatchNumbers.SetCurrentLine(i);
                    //oDocuments.Lines.BatchNumbers.BatchNumber = inventoryGenEntries[0].Parti;
                    //oDocuments.Lines.BatchNumbers.Quantity = inventoryGenEntries[0].PartiMiktar;
                    //    i++;
                    //}

                    oDocuments.Lines.Add();

                    #endregion üretim depo içerisinden stoğun ilgili deposuna gönderilmesi için taslak stok nakli oluşturma.

                    aa = oDocuments.Add();

                    if (aa != 0)
                    {
                        string hata = oCompany.GetLastErrorDescription();
                        //if (oCompany.InTransaction)
                        //{
                        //    try
                        //    {
                        //        oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                        //    }
                        //    catch (Exception)
                        //    {
                        //    }
                        //}
                        logger.Fatal("ID: " + ID + " " + "Hata Kodu - 4100 Fire Stok nakli talebi oluşturulurken hata oluştu." + hata);
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
                        return new Response { Value = -3100, Description = "Hata Kodu - 4100 Fire Stok nakli talebi oluşturulurken hata oluştu. " + hata, List = null };
                    }

                    //}
                    //}
                }

                if (inventoryGenEntries.Where(x => x.NumuneMiktar > 0).Count() > 0)
                {
                    //if (aa == 0)
                    //{
                    string value = inventoryGenEntries.Where(x => x.UrunKodu != "").Select(y => y.UrunKodu).FirstOrDefault().ToString();

                    //if (value.StartsWith("MAM"))
                    //{

                    #region Numune girilirse SAP'de stok nakli oluşur.

                    SAPbobsCOM.StockTransfer oDocuments = (SAPbobsCOM.StockTransfer)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInventoryTransferRequest);
                    //oDocuments.DocObjectCode = SAPbobsCOM.BoObjectTypes.oStockTransfer;

                    oDocuments.DocDate = DateTime.Now;
                    oDocuments.UserFields.Fields.Item("U_UretimdenGonderildi").Value = "Y";

                    string uretimSiparisi = inventoryGenEntries.Where(x => x.Miktar > 0).Select(y => y.UretimSiparisi).FirstOrDefault().ToString();

                    SAPbobsCOM.Recordset oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRS.DoQuery("Select \"WareHouse\" from OWOR WITH (NOLOCK) where \"DocNum\" = " + uretimSiparisi + "");

                    var stoknakiKaynakDepo = oRS.Fields.Item(0).Value.ToString();

                    oDocuments.FromWarehouse = stoknakiKaynakDepo;

                    #region old

                    ////var stoknakiHedefDepo = inventoryGenEntries.Where(x => x.Miktar > 0 && x.StokNakliHedefDepo != "").Select(y => y.StokNakliHedefDepo).FirstOrDefault().ToString();
                    //string stoknakiHedefDepo = "";
                    //oRS.DoQuery("Select \"U_UVTNumuneDepo\" from OITM WITH (NOLOCK) where \"ItemCode\" = '" + value + "'");

                    //stoknakiHedefDepo = oRS.Fields.Item(0).Value.ToString();

                    //if (stoknakiHedefDepo == "")
                    //{
                    //    //if (oCompany.InTransaction)
                    //    //{
                    //    //    try
                    //    //    {
                    //    //        oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                    //    //    }
                    //    //    catch (Exception)
                    //    //    {
                    //    //    }
                    //    //}
                    //    logger.Fatal("ID: " + ID + " " + "Hata Kodu - 5100 Numune için " + value + " için Numune depo seçimi yapılmamıştır.");
                    //    LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
                    //    return new Response { Value = -3100, Description = "Hata Kodu - 5100 Numune için " + value + " için Numune depo seçimi yapılmamıştır. ", List = null };
                    //} 
                    #endregion

                    if (numunedepo != null && numunedepo != "")
                    {
                        oDocuments.ToWarehouse = numunedepo;
                    }

                    oDocuments.DueDate = DateTime.Now;

                    #endregion Numune girilirse SAP'de stok nakli oluşur.

                    #region üretim depo içerisinden stoğun ilgili deposuna gönderilmesi için taslak stok nakli oluşturma.

                    oDocuments.Lines.ItemCode = inventoryGenEntries.Where(x => x.UrunKodu != "").Select(y => y.UrunKodu).FirstOrDefault().ToString();
                    oDocuments.Lines.Quantity = inventoryGenEntries[0].NumuneMiktar;

                    //foreach (var aifteam in item.PartiInventoryGenEntryLinesBatchMiktar)
                    //{
                    //oDocuments.Lines.BatchNumbers.Add();
                    //oDocuments.Lines.BatchNumbers.SetCurrentLine(i);
                    //oDocuments.Lines.BatchNumbers.BatchNumber = inventoryGenEntries[0].Parti;
                    //oDocuments.Lines.BatchNumbers.Quantity = inventoryGenEntries[0].PartiMiktar;
                    //    i++;
                    //}

                    oDocuments.Lines.Add();

                    #endregion üretim depo içerisinden stoğun ilgili deposuna gönderilmesi için taslak stok nakli oluşturma.

                    aa = oDocuments.Add();

                    if (aa != 0)
                    {
                        string hata = oCompany.GetLastErrorDescription();
                        //if (oCompany.InTransaction)
                        //{
                        //    try
                        //    {
                        //        oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                        //    }
                        //    catch (Exception)
                        //    {
                        //    }
                        //}
                        logger.Fatal("ID: " + ID + " " + "Hata Kodu - 4100 Numune Stok nakli talebi oluşturulurken hata oluştu." + hata);
                        LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
                        return new Response { Value = -3100, Description = "Hata Kodu - 4100 Numune Stok nakli talebi oluşturulurken hata oluştu. " + hata, List = null };
                    }

                    //}
                    //}
                }
                if (aa == 0)
                {
                    try
                    {
                        SAPbobsCOM.Recordset oRS = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                        oRS.DoQuery("Select \"U_UrtPrtSekli\" as \"UrtPrtSekli\" from \"@AIF_UVT_PARAM\" WITH (NOLOCK) ");

                        string UrtPrtSekli = oRS.Fields.Item("UrtPrtSekli").Value.ToString();

                        if (UrtPrtSekli == "2")
                        {
                            if (inventoryGenEntries[0].TamamlaniyorMu != null && inventoryGenEntries[0].TamamlaniyorMu == "Evet")
                            {
                                SAPbobsCOM.ProductionOrders oProductionOrders = (SAPbobsCOM.ProductionOrders)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oProductionOrders);

                                oProductionOrders.GetByKey(Convert.ToInt32(inventoryGenEntries.Where(x => x.UretimSiparisi != null).Select(y => y.UretimSiparisi).FirstOrDefault()));

                                oProductionOrders.ProductionOrderStatus = SAPbobsCOM.BoProductionOrderStatusEnum.boposClosed;

                                int ret = oProductionOrders.Update();

                                if (ret != 0)
                                {
                                    logger.Fatal("ID: " + ID + " " + "Hata Kodu - 9100 Üretim siparişi kapatılırken hata oluştu. " + oCompany.GetLastErrorDescription());
                                    LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
                                    return new Response { Value = -3100, Description = "Hata Kodu - 9100 Üretim siparişi kapatılırken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }

                    //try
                    //{
                    //    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                    //}
                    //catch (Exception)
                    //{
                    //}
                    logger.Info("ID: " + ID + " " + "Üretim için giriş başarıyla oluşturuldu.");
                    LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
                    return new Response { Value = 0, Description = "Üretim için giriş başarıyla oluşturuldu.", List = null, DocEntry = Convert.ToInt32(oCompany.GetNewObjectKey()) };
                }
                else
                {
                    string error = "";

                    error = oCompany.GetLastErrorDescription();
                    //try
                    //{
                    //    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                    //}
                    //catch (Exception)
                    //{
                    //}
                    logger.Fatal("ID: " + ID + " " + "Hata Kodu - 4200 Üretim için giriş oluşturulurken hata oluştu. " + error);
                    LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
                    return new Response { Value = -4100, Description = "Hata Kodu - 4200 Üretim için giriş oluşturulurken hata oluştu. " + error, List = null };
                }
            }
            catch (Exception ex)
            {
                //try
                //{
                //    oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                //}
                //catch (Exception)
                //{
                //}
                logger.Fatal("ID: " + ID + " " + "Bilinmeyen hata oluştu. " + ex.Message);
                LoginCompany.ReleaseConnection(clnum, dbCode, ID);
                return new Response { Value = 9000, Description = "Bilinmeyen hata oluştu. " + ex.Message, List = null };
            }
            finally
            {
                LoginCompany.ReleaseConnection(clnum, dbCode, ID);
            }
        }
    }
}