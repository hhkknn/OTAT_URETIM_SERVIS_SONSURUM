using AIF.UVTService.DatabaseLayer;
using AIF.UVTService.Models;
using AIF.UVTService.SAPLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using UVTService.Models;
using UVTService.SAPLayer;

namespace AIF.UVTService
{
    /// <summary>
    /// Summary description for UVTService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    // [System.Web.Script.Services.ScriptService]
    public class UVTService : System.Web.Services.WebService
    {
        [WebMethod]
        public Response GetCompanyList(string dbName, string mKodValue)
        {
            GetCompanyList l = new GetCompanyList();
            Response resp = l.getCompanyList(dbName, mKodValue);

            return resp;
        }

        [WebMethod]
        public Response Login(string userName, string Password, string dbName, string mKodValue)
        {
            LoginCompany l = new LoginCompany();
            Response resp = l.Connect(userName, Password, dbName, mKodValue);
            return resp;
        } 

        [WebMethod]
        public string getConnectionAsString(string dbName, string mKodValue)
        {
            GetConnectionAsString k = new GetConnectionAsString();
            string conn = k.getConnectionAsString(dbName, mKodValue);
            return conn;
        }

        //[WebMethod]
        //public Response LoginWithSQL(string kadi, string parola)
        //{
        //    LoginCompany l = new LoginCompany()
        //    var res = l.LoginViaSQL(kadi, parola);

        //    return res;
        //}

        [WebMethod]
        public Response GetAllCustomers(string dbName, string mKodValue)
        {
            GetCustomers l = new GetCustomers();
            var res = l.getCustomers(dbName, mKodValue);

            return res;
        }

        [WebMethod]
        public Response GetCustomersWithAddressByCardCode(string cardCode, string dbName, string mKodValue)
        {
            GetCustomers l = new GetCustomers();
            var res = l.getCustomersWithAddressByCardCode(cardCode, dbName, mKodValue);

            return res;
        }

        [WebMethod]
        public Response GetCustomerContactByCardCode(string cardCode, string dbName, string mKodValue)
        {
            GetCustomers l = new GetCustomers();
            var res = l.getCustomersContactByCardCode(cardCode, dbName, mKodValue);

            return res;
        }

        [WebMethod]
        public Response GetProducts(string dbName, string mKodValue)
        {
            GetProducts l = new GetProducts();
            var res = l.getProducts(dbName, mKodValue);

            return res;
        }

        [WebMethod]
        public Response GetProductsWarehouse(string ItemCode, string dbName, string mKodValue)
        {
            GetProducts l = new GetProducts();
            var res = l.getProductsWareHouseByItemCode(ItemCode, dbName, mKodValue);

            return res;
        }

        [WebMethod]
        public Response BusinessPartnerUpdate(BusinessPartners data, string dbName)
        {
            BusinessPartnerUpdate l = new BusinessPartnerUpdate();
            var res = l.updateCustomerByCardCode(data, dbName);

            return res;
        }

        [WebMethod]
        public Response BusinessPartnerAdd(BusinessPartners data, string dbName, string mKodValue)
        {
            BusinessPartnerAdd l = new BusinessPartnerAdd();
            var res = l.businessPartnerAdd(data, dbName, mKodValue);

            return res;
        }

        [WebMethod]
        public Response AddSalesOrder(Orders orders, string dbName, string mKodValue)
        {
            AddSalesOrder n = new AddSalesOrder();
            var res = n.addSalesOrder(orders, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response GetOrders(string dbName, string mKodValue)
        {
            GetOrders l = new GetOrders();
            var res = l.getOrders(dbName, mKodValue);

            return res;
        }

        [WebMethod]
        public Response GetOrdersWithDetails(int DocEntry, string dbName, string mKodValue)
        {
            GetOrders l = new GetOrders();
            var res = l.getOrdersWithDetailsByDocEntry(DocEntry, dbName, mKodValue);

            return res;
        }

        [WebMethod]
        public Response GetDiscountList(int DocEntry, string dbName, string mKodValue)
        {
            GetDiscount l = new GetDiscount();
            var res = l.getDiscount(DocEntry, dbName, mKodValue);

            return res;
        }

        [WebMethod]
        public Response UpdateDiscount(Discount discount, string dbName, string mKodValue)
        {
            AddOrUpdateDiscount l = new AddOrUpdateDiscount();
            var res = l.updateDiscount(discount, dbName, mKodValue);

            return res;
        }

        [WebMethod]
        public Response AddDiscount(Discount discount, string dbName, string mKodValue)
        {
            AddOrUpdateDiscount l = new AddOrUpdateDiscount();
            var res = l.addDiscount(discount, dbName, mKodValue);

            return res;
        }

        [WebMethod]
        public Response AddDiscountManullay(Discount discount, string dbName, string mKodValue)
        {
            AddOrUpdateDiscount l = new AddOrUpdateDiscount();
            var res = l.addDiscountManually(discount, dbName, mKodValue);

            return res;
        }

        [WebMethod]
        public Response AddContacts(List<Contacts> contacts, string dbName, string mKodValue)
        {
            AddContacts l = new AddContacts();
            var res = l.addContacts(contacts, dbName, mKodValue);

            return res;
        }

        [WebMethod]
        public Response UpdateContacts(List<Contacts> contacts, string dbName, string mKodValue)
        {
            UpdateContacts l = new UpdateContacts();
            var res = l.updateContacts(contacts, dbName, mKodValue);

            return res;
        }

        [WebMethod]
        public Response AddInventoryGenExist(List<InventoryGenExist> inventoryGenExistList, string dbName, string mKodValue)
        {
            AddInventoryGenExist l = new AddInventoryGenExist();
            var res = l.addInventoryGenExist(inventoryGenExistList, dbName, mKodValue);

            return res;
        }

        [WebMethod]
        public Response AddInventoryGenEntry(List<InventoryGenEntry> inventoryGenEntry, string dbName, string mKodValue)
        {
            AddInventoryGenEntry l = new AddInventoryGenEntry();
            var res = l.addInventoryGenEntry(inventoryGenEntry, dbName, mKodValue);

            return res;
        }

        [WebMethod]
        public Response AddOrUpdateAyranAnalizTakibi(AyranAnalizTakibi ayranAnalizTakibi, string dbName, string mKodValue)
        {
            AddOrUpdateAyranAnalizTakibi l = new AddOrUpdateAyranAnalizTakibi();
            var res = l.addOrUpdateAyranAnalizTakibi(ayranAnalizTakibi, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response AddOrUpdateTelemeAnalizTakibi(TelemeAnalizTakibi telemeAnalizTakibi, string dbName, string mKodValue)
        {
            AddOrUpdateTelemeAnalizTakibi l = new AddOrUpdateTelemeAnalizTakibi();
            var res = l.addOrUpdateTelemeAnalizTakibi(telemeAnalizTakibi, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response AddOrUpdateRotaDurum(RotaDurum rotaDurum, string dbName, string mKodValue)
        {
            AddOrUpdateRotaDurum l = new AddOrUpdateRotaDurum();
            var res = l.addOrUpdateRotaDurum(rotaDurum, dbName, mKodValue);
            return res;
        }

        //[WebMethod]
        //public Response UpdateAyranAnalizTakibi(AyranAnalizTakibi ayranAnalizTakibi)
        //{
        //    AddOrUpdateAyranAnalizTakibi l = new AddOrUpdateAyranAnalizTakibi();
        //    var res = l.updateAyranAnalizTakibi(ayranAnalizTakibi);
        //    return res;
        //}
        [WebMethod]
        public Response AddProductionOrders(ProductionOrder productionOrder, string dbName, string mKodValue)
        {
            //ProductionOrder p = new ProductionOrder();
            //p.ItemCode = "1100";
            //p.Miktar = 1;
            AddProductionOrders l = new AddProductionOrders();
            var res = l.addProductionOrders(productionOrder, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response AddOrUpdatePurchaseDeliveryNotes(PurchaseDeliveryNotes purchaseDeliveryNotes, string dbName, string mKodValue)
        {
            AddPurchaseDeliveryNotes l = new AddPurchaseDeliveryNotes();
            var res = l.addPurchaseDeliveryNotes(purchaseDeliveryNotes, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response GetProductionbyDocEntry(int docEntry, string dbName, string mKodValue)
        {
            GetProductionOrders l = new GetProductionOrders();
            var res = l.getProductionOrdersbyDocEntry(docEntry, dbName, mKodValue);

            return res;
        }

        [WebMethod]
        public Response GetEmployess(string dbName, string mKodValue)
        {
            GetEmployess l = new GetEmployess();
            var res = l.getEmployess(dbName, mKodValue);

            return res;
        }

        [WebMethod]
        public Response AddOrUpdateTelemeProsesAnalizTakip(TelemeProsesTakipAnaliz telemeProsesTakipAnaliz, string dbName, string mKodValue)
        {
            AddOrUpdateTelemeProsesAnalizTakip l = new AddOrUpdateTelemeProsesAnalizTakip();
            var res = l.addOrUpdateTelemeProsesTakipAnaliz(telemeProsesTakipAnaliz, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response AddOrUpdateTelemeMamulAnalizTakip(TelemeMamulTakipAnaliz telemeMamulTakipAnaliz, string dbName, string mKodValue)
        {
            AddOrUpdateTelemeMamulAnalizTakip l = new AddOrUpdateTelemeMamulAnalizTakip();
            var res = l.addOrUpdateTelemeMamulTakipAnaliz(telemeMamulTakipAnaliz, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response AddOrUpdateTereyagProsesAnalizTakip(TereyagProsesTakipAnaliz tereyagProsesTakipAnaliz, string dbName, string mKodValue)
        {
            AddOrUpdateTereyagProsessAnaliz l = new AddOrUpdateTereyagProsessAnaliz();
            var res = l.addOrUpdateTereyagProsessAnaliz(tereyagProsesTakipAnaliz, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response AddOrUpdateTazePeynirProsesAnalizTakip(TazePeynirProsesTakipAnaliz tazePeynirProsesTakipAnaliz, string dbName, string mKodValue)
        {
            AddOrUpdateTazePeynirProsessAnaliz l = new AddOrUpdateTazePeynirProsessAnaliz();
            var res = l.addOrUpdateTazePeynirProsessAnaliz(tazePeynirProsesTakipAnaliz, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response AddOrUpdateTostPeynirProsesAnalizTakip(TostPeynirProsesTakipAnaliz tostPeynirProsesTakipAnaliz, string dbName, string mKodValue)
        {
            AddOrUpdateTostPeynirProsessAnaliz l = new AddOrUpdateTostPeynirProsessAnaliz();
            var res = l.addOrUpdateTostPeynirProsessAnaliz(tostPeynirProsesTakipAnaliz, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response AddOrUpdateTereyagProsesAnalizTakip2(TereyagProsesTakipAnaliz2 tereyagProsesTakipAnaliz2, string dbName, string mKodValue)
        {
            AddOrUpdateTereyagProsessAnaliz2 l = new AddOrUpdateTereyagProsessAnaliz2();
            var res = l.addOrUpdateTereyagProsessAnaliz2(tereyagProsesTakipAnaliz2, dbName, mKodValue);
            return res;
        }

        #region kapatıldı tereyag gunluk analize taşındı
        //[WebMethod]
        //public Response AddOrUpdateDinlendirmeVePaketlemeOdasi(List<TereyagProsses2DinlendirmeVePaketleme> tereyagProsses2DinlendirmeVePaketlemes, string dbName, string mKodValue)
        //{
        //    AddOrUpdateTereyagProsessAnaliz2 l = new AddOrUpdateTereyagProsessAnaliz2();
        //    var res = l.addOrUpdateDinlendirmeVePaketleme(tereyagProsses2DinlendirmeVePaketlemes, dbName, mKodValue);
        //    return res;
        //} 
        #endregion

        [WebMethod]
        public Response AddOrUpdateTostPeynirProsesAnalizTakip2(TostPeynirTakipAnaliz2 tostPeynirTakipAnaliz2, string dbName, string mKodValue)
        {
            AddOrUpdateTostPeynirProsessAnaliz2 l = new AddOrUpdateTostPeynirProsessAnaliz2();
            var res = l.addOrUpdateTostPeynirProsessAnaliz2(tostPeynirTakipAnaliz2, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response AddOrUpdateTostPeynirKurutmaVePaketlemeOdasi(List<TostPeynir2KurutmaVePaketleme> tostPeynir2KurutmaVePaketlemes, string dbName, string mKodValue)
        {
            AddOrUpdateTostPeynirProsessAnaliz2 l = new AddOrUpdateTostPeynirProsessAnaliz2();
            var res = l.addOrUpdateKurutmaVePaketleme(tostPeynir2KurutmaVePaketlemes, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response AddOrUpdateTazePeynirProsesAnalizTakip2(TazePeynirTakipAnaliz2 tazePeynirTakipAnaliz2, string dbName, string mKodValue)
        {
            AddOrUpdateTazePeynirProsessAnaliz2 l = new AddOrUpdateTazePeynirProsessAnaliz2();
            var res = l.addOrUpdateTazePeynirProsessAnaliz2(tazePeynirTakipAnaliz2, dbName, mKodValue);
            return res;
        }

        //[WebMethod]
        //public Response AddOrUpdateTazePeynirKurutmaVePaketlemeOdasi(List<TazePeynir2DinlendirmeVePaketleme> tazePeynir2DinlendirmeVePaketlemes, string dbName, string mKodValue)
        //{
        //    AddOrUpdateTazePeynirProsessAnaliz2 l = new AddOrUpdateTazePeynirProsessAnaliz2();
        //    var res = l.addOrUpdateDinlendirmeVePaketleme(tazePeynir2DinlendirmeVePaketlemes, dbName, mKodValue);
        //    return res;
        //}

        [WebMethod]
        public Response AddOrUpdateAyranProsesTakipAnaliz(AyranProsesTakipAnaliz ayranProsesTakipAnalizs, string dbName, string mKodValue)
        {
            AddOrUpdateAyranProsesAnaliz l = new AddOrUpdateAyranProsesAnaliz();
            var res = l.addOrUpdateAyranProsesAnaliz(ayranProsesTakipAnalizs, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response AddOrUpdateTemizlik(AnalizTemizlik analizTemizlik, string dbName, string mKodValue)
        {
            AddOrUpdateTemizlik l = new AddOrUpdateTemizlik();
            var res = l.addOrUpdateTemizlik(analizTemizlik, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response AddOrUpdateYogurtProsesTakipAnaliz(YogurtProsesTakipAnaliz yogurtProsesTakipAnaliz, string dbName, string mKodValue)
        {
            AddOrUpdateYogurtProsesAnaliz l = new AddOrUpdateYogurtProsesAnaliz();
            var res = l.addOrUpdateYogurtProsesAnaliz(yogurtProsesTakipAnaliz, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response AddOrUpdateLorProsesTakipAnaliz(LorProsesTakipAnaliz lorProsesTakipAnaliz, string dbName, string mKodValue)
        {
            AddOrUpdateLorProsessAnaliz l = new AddOrUpdateLorProsessAnaliz();
            var res = l.addOrUpdateLorProsessAnaliz(lorProsesTakipAnaliz, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response AddOrUpdatePastorizasyonProsesTakipAnaliz(PastorizasyonProsesTakipAnaliz pastorizasyonProsesTakipAnaliz, string dbName, string mKodValue)
        {
            AddOrUpdatePastorizasyonProsesAnaliz l = new AddOrUpdatePastorizasyonProsesAnaliz();
            var res = l.addOrUpdatePastorizasyonProsesAnaliz(pastorizasyonProsesTakipAnaliz, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response AddOrUpdateTamYagliAyranOzet(TamYagliAyranGunlukOzet tamYagliAyranGunlukOzet, string dbName, string mKodValue)
        {
            AddOrUpdateTamYagliAyranOzet l = new AddOrUpdateTamYagliAyranOzet();
            var res = l.addOrUpdateTamYagliAyranOzet(tamYagliAyranGunlukOzet, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response AddOrUpdateYarimYagliAyranOzet(YarimYagliAyranGunlukOzet yarimYagliAyranGunlukOzet, string dbName, string mKodValue)
        {
            AddOrUpdateYarimYagliAyranOzet l = new AddOrUpdateYarimYagliAyranOzet();
            var res = l.addOrUpdateYarimYagliAyranOzet(yarimYagliAyranGunlukOzet, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response AddOrUpdateAyranOzetYardimciMalzeme(AyranYardimciMalzemeGunlukOzet ayranYardimciMalzemeGunlukOzet, string dbName, string mKodValue)
        {
            AddOrUpdateAyranYardimciMalzeme l = new AddOrUpdateAyranYardimciMalzeme();
            var res = l.addOrUpdateAyranYardimciMalzeme(ayranYardimciMalzemeGunlukOzet, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response AddOrUpdatePakelenenAyranProsesOzellikleri(PaketlenenAyranProsesOzellikleri paketlenenAyranProsesOzellikleri, string dbName, string mKodValue)
        {
            AddOrUpdatePakelenenAyranProsesOzellikleri l = new AddOrUpdatePakelenenAyranProsesOzellikleri();
            var res = l.addOrUpdatePakelenenAyranProsesOzellikleri(paketlenenAyranProsesOzellikleri, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response AddOrUpdateYogurtYardimciMalzeme(YogurtYardimciMalzeme yogurtYardimciMalzeme, string dbName, string mKodValue)
        {
            AddOrUpdateYogurtYardimciMalzeme l = new AddOrUpdateYogurtYardimciMalzeme();
            var res = l.addOrUpdateYogurtYardimciMalzeme(yogurtYardimciMalzeme, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response addOrUpdateFSAPKY001(FSAPKY001 fSAPKY001, string dbName, string mKodValue)
        {
            AddOrUpdateFSAPKY001 l = new AddOrUpdateFSAPKY001();
            var res = l.addOrUpdateFSAPKY001(fSAPKY001, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response addOrUpdateFSAPKY002_1(FSAPKY002_1 fSAPKY002_1, string dbName, string mKodValue)
        {
            AddOrUpdateFSAPKY002_1 l = new AddOrUpdateFSAPKY002_1();
            var res = l.addOrUpdateFSAPKY002_1(fSAPKY002_1, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response addOrUpdateFSAPKY003_1(FSAPKY003_1 fSAPKY003_1, string dbName, string mKodValue)
        {
            AddOrUpdateFSAPKY003_1 l = new AddOrUpdateFSAPKY003_1();
            var res = l.addOrUpdateFSAPKY003_1(fSAPKY003_1, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response addOrUpdateFSAPKY004_1_1(FSAPKY004_1_1 fSAPKY004_1_1, string dbName, string mKodValue)
        {
            AddOrUpdateFSAPKY004_1_1 l = new AddOrUpdateFSAPKY004_1_1();
            var res = l.addOrUpdateFSAPKY004_1_1(fSAPKY004_1_1, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response addOrUpdateFSAPKY004_2(FSAPKY004_2 fSAPKY004_2, string dbName, string mKodValue)
        {
            AddOrUpdateFSAPKY004_2 l = new AddOrUpdateFSAPKY004_2();
            var res = l.addOrUpdateFSAPKY004_2(fSAPKY004_2, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response addOrUpdateFSAPKY005_2(FSAPKY005_2 fSAPKY005_2, string dbName, string mKodValue)
        {
            AddOrUpdateFSAPKY005_2 l = new AddOrUpdateFSAPKY005_2();
            var res = l.addOrUpdateFSAPKY005_2(fSAPKY005_2, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response addOrUpdateFSAPKY005_3_1(FSAPKY005_3_1 fSAPKY005_3_1, string dbName, string mKodValue)
        {
            AddOrUpdateFSAPKY005_3_1 l = new AddOrUpdateFSAPKY005_3_1();
            var res = l.addOrUpdateFSAPKY005_3_1(fSAPKY005_3_1, dbName, mKodValue);
            return res;
        }


        [WebMethod]
        public Response addOrUpdateFSAPKY006_1_1(FSAPKY006_1_1 fSAPKY006_1_1, string dbName, string mKodValue)
        {
            AddOrUpdateFSAPKY006_1_1 l = new AddOrUpdateFSAPKY006_1_1();
            var res = l.addOrUpdateFSAPKY006_1_1(fSAPKY006_1_1, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response addOrUpdateFSAPKY007_1(FSAPKY007_1 fSAPKY007_1, string dbName, string mKodValue)
        {
            AddOrUpdateFSAPKY007_1 l = new AddOrUpdateFSAPKY007_1();
            var res = l.addOrUpdateFSAPKY007_1(fSAPKY007_1, dbName, mKodValue);
            return res;
        }


        [WebMethod]
        public Response addOrUpdateFSAPKY007_2(FSAPKY007_2 fSAPKY007_2, string dbName, string mKodValue)
        {
            AddOrUpdateFSAPKY007_2 l = new AddOrUpdateFSAPKY007_2();
            var res = l.addOrUpdateFSAPKY007_2(fSAPKY007_2, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response addOrUpdateFSAPKY007_3(FSAPKY007_3 fSAPKY007_3, string dbName, string mKodValue)
        {
            AddOrUpdateFSAPKY007_3 l = new AddOrUpdateFSAPKY007_3();
            var res = l.addOrUpdateFSAPKY007_3(fSAPKY007_3, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response addOrUpdateFSAPKY002_2(FSAPKY002_2 fSAPKY002_2, string dbName, string mKodValue)
        {
            AddOrUpdateFSAPKY002_2 l = new AddOrUpdateFSAPKY002_2();
            var res = l.addOrUpdateFSAPKY002_2(fSAPKY002_2, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response addOrUpdateFSAPKY002_3(FSAPKY002_3 fSAPKY002_3, string dbName, string mKodValue)
        {
            AddOrUpdateFSAPKY002_3 l = new AddOrUpdateFSAPKY002_3();
            var res = l.addOrUpdateFSAPKY002_3(fSAPKY002_3, dbName, mKodValue);
            return res;
        }
        [WebMethod]
        public Response addOrUpdateFSAPKY008_1(FSAPKY008_1 fSAPKY008_1, string dbName, string mKodValue)
        {
            AddOrUpdateFSAPKY008_1 l = new AddOrUpdateFSAPKY008_1();
            var res = l.addOrUpdateFSAPKY008_1(fSAPKY008_1, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response addOrUpdateFSAPKY010_1(FSAPKY010_1 fSAPKY010_1, string dbName, string mKodValue)
        {
            AddOrUpdateFSAPKY010_1 l = new AddOrUpdateFSAPKY010_1();
            var res = l.addOrUpdateFSAPKY010_1(fSAPKY010_1, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response addOrUpdateFSAPKY011_1(FSAPKY011_1 fSAPKY011_1, string dbName, string mKodValue)
        {
            AddOrUpdateFSAPKY011_1 l = new AddOrUpdateFSAPKY011_1();
            var res = l.addOrUpdateFSAPKY011_1(fSAPKY011_1, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response addOrUpdateFSAPKY009_1(FSAPKY009_1 fSAPKY009_1, string dbName, string mKodValue)
        {
            AddOrUpdateFSAPKY009_1 l = new AddOrUpdateFSAPKY009_1();
            var res = l.addOrUpdateFSAPKY009_1(fSAPKY009_1, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response addOrUpdateFSAPKY012_1(FSAPKY012_1 fSAPKY012_1, string dbName, string mKodValue)
        {
            AddOrUpdateFSAPKY012_1 l = new AddOrUpdateFSAPKY012_1();
            var res = l.addOrUpdateFSAPKY012_1(fSAPKY012_1, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response addOrUpdateFSAPKY003_2(FSAPKY003_2 fSAPKY003_2, string dbName, string mKodValue)
        {
            AddOrUpdateFSAPKY003_2 l = new AddOrUpdateFSAPKY003_2();
            var res = l.addOrUpdateFSAPKY003_2(fSAPKY003_2, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response addOrUpdateFSAPKY012_2(FSAPKY012_2 fSAPKY012_2, string dbName, string mKodValue)
        {
            AddOrUpdateFSAPKY012_2 l = new AddOrUpdateFSAPKY012_2();
            var res = l.addOrUpdateFSAPKY012_2(fSAPKY012_2, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response getKaliteDetayListesi(string dbName, string mKodValue)
        {
            GetKaliteDetayListesi l = new GetKaliteDetayListesi();
            var res = l.getKaliteDetayListesi(dbName, mKodValue);
            return res;
        }
        [WebMethod]
        public Response addOrUpdateKaliteSonuc(KaliteListesi kaliteListesi, string dbName, string mKodValue)
        {
            AddOrUpdateKaliteSonuc l = new AddOrUpdateKaliteSonuc();
            var res = l.addOrUpdateKaliteSonuc(kaliteListesi, dbName, mKodValue);
            return res;
        }

        [WebMethod]
        public Response UpdateProductionOrders(string dbName, int docnum, string duraklama, string mKodValue)
        {
            UpdateProductionOrders l = new UpdateProductionOrders();
            var res = l.updateProductionOrders(dbName, docnum, duraklama, mKodValue);

            return res;
        }

        [WebMethod]
        public Response AddOrUpdatePaletYapma(string dbName, PaletYapma paletYapma, string mKodValue)
        {
            AddOrUpdatePalet l = new AddOrUpdatePalet();
            var res = l.addOrUpdatePalet(dbName, paletYapma, mKodValue);

            return res;
        }

        [WebMethod]
        public Response GetPaletNumarasiGetir(string dbName, string mKod)
        {
            GetPaletNumarasiCustomTable l = new GetPaletNumarasiCustomTable();
            Response resp = l.getPaletNumarasiCustomTable(dbName, mKod);
            return resp;
        }

        [WebMethod]
        public Response UpdatePaletNumarasi(string dbName, int docentry, int nextnumber, string mKod)
        {
            GetPaletNumarasiCustomTable l = new GetPaletNumarasiCustomTable();
            Response resp = l.updatePaletNumarasi(dbName, docentry, nextnumber, mKod);
            return resp;
        }

        [WebMethod]
        public Response getUretimPaletDetay(string dbName, string uretimFisNo, string mKod)
        {
            GetPaletDetay l = new GetPaletDetay();
            Response resp = l.getUretimPaletDetay(dbName, uretimFisNo, mKod);
            return resp;
        }

        [WebMethod]
        public Response AddOrUpdateBulkKulturAnaliz(BulkKulturAnaliz bulkKulturAnaliz,string dbName, string mKodValue)
        {
            AddOrUpdateBulkKulturAnaliz l = new AddOrUpdateBulkKulturAnaliz();
            Response resp = l.addOrUpdateBulkKulturAnaliz(bulkKulturAnaliz,dbName, mKodValue);
            return resp;
        }

        [WebMethod]
        public Response AddOrUpdateTazePeynirSalamuraSu(TazePeynirSalamuraSu tazePeynirSalamuraSu, string dbName, string mKodValue)
        {
            AddOrUpdateTazePeynirSalamuraSu l = new AddOrUpdateTazePeynirSalamuraSu();
            Response resp = l.addOrUpdateTazePeynirSalamuraSu(tazePeynirSalamuraSu, dbName, mKodValue);
            return resp;
        }
        [WebMethod]
        public Response AddOrUpdateYogurtMamulAnaliz(YogurtMamulAnaliz yogurtMamulAnaliz, string dbName, string mKodValue)
        {
            AddOrUpdateYogurtMamulAnaliz l = new AddOrUpdateYogurtMamulAnaliz();
            Response resp = l.addOrUpdateYogurtMamulAnaliz(yogurtMamulAnaliz, dbName, mKodValue);
            return resp;
        }
        [WebMethod]
        public Response AddOrUpdateTazePeynirGunlukAnaliz(TazePeynirGunlukAnaliz tazePeynirGunlukAnaliz, string dbName, string mKodValue)
        {
            AddOrUpdateTazePeynirGunlukAnaliz l = new AddOrUpdateTazePeynirGunlukAnaliz();
            Response resp = l.addOrUpdateTazePeynirGunlukAnaliz(tazePeynirGunlukAnaliz, dbName, mKodValue);
            return resp;
        }

        [WebMethod]
        public Response AddOrUpdateTereyagGunlukAnaliz(TereyagGunlukAnaliz tereyagGunlukAnaliz, string dbName, string mKodValue)
        {
            AddOrUpdateTereyagGunlukAnaliz l = new AddOrUpdateTereyagGunlukAnaliz();
            Response resp = l.addOrUpdateTereyagGunlukAnaliz(tereyagGunlukAnaliz, dbName, mKodValue);
            return resp;
        }
    }
}