﻿using UVTService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Compilation;
using Newtonsoft.Json;
using NLog;
using AIF.UVTService.DatabaseLayer;
using System.Data.SqlClient;

namespace UVTService.SAPLayer
{
    public class AddContacts
    {
        //commit
        public Response addContacts(List<Contacts> contacts, string dbName, string mKodValue)
        {

            var json = JsonConvert.SerializeObject(contacts);
            object MaxCount = 0;

            string ss = "";

            GetConnectionAsString s = new GetConnectionAsString();
            string connectionString = s.getConnectionAsString(dbName, mKodValue);

            SqlConnection myConnection = new SqlConnection(connectionString);


            string ss1 = "Select MAX(Cast(\"Code\" as int)) + 1 from \"@AIF_UVTDATA\" ";

            using (SqlCommand cmd = new SqlCommand(ss1, myConnection))
            {
                myConnection.Open();

                try
                {
                    MaxCount = cmd.ExecuteScalar();

                    if (myConnection.State == System.Data.ConnectionState.Open)
                        myConnection.Close();

                    //myConnection.Open();

                    //cmd.Connection = myConnection;
                    //cmd.CommandText = "Select \"Code\" from \"@AIF_UVTDATA\" where \"U_Deger1\" = '" + inventoryGenExits[0].Id + "' order by cast(\"Code\" as int) desc";
                    //object id = cmd.ExecuteScalar();

                    if (myConnection.State == System.Data.ConnectionState.Open)
                        myConnection.Close();
                }
                catch (Exception ex)
                {
                }
            }

            int sonnumara = Convert.ToInt32(MaxCount);

            System.Data.DataTable newTable = new System.Data.DataTable("Contactss");
            newTable.Columns.Add("Code", Type.GetType("System.String"));
            newTable.Columns.Add("Name", Type.GetType("System.String"));
            newTable.Columns.Add("U_Tip", Type.GetType("System.String"));
            newTable.Columns.Add("U_TipAciklama", Type.GetType("System.String"));
            newTable.Columns.Add("U_IstekJson", Type.GetType("System.String"));
            newTable.Columns.Add("U_Deger1", Type.GetType("System.String"));
            newTable.Columns.Add("U_IstekTarihi", Type.GetType("System.DateTime"));
            newTable.Columns.Add("U_IstekSaati", Type.GetType("System.String"));
            newTable.Columns.Add("U_Durum", Type.GetType("System.String"));
            newTable.Columns.Add("U_DurumAciklama", Type.GetType("System.String"));


            string istekSaati = DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0');
            foreach (var item in contacts)
            {
                json = JsonConvert.SerializeObject(item);

                istekSaati = DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0');
                System.Data.DataRow newTableRows = newTable.NewRow();
                newTableRows["Code"] = Convert.ToInt32(sonnumara);
                newTableRows["Name"] = sonnumara.ToString();
                newTableRows["U_Tip"] = "1";
                newTableRows["U_TipAciklama"] = "AktiviteGirisi";
                newTableRows["U_IstekJson"] = json.Replace("'", "").Replace(" & ", "");
                newTableRows["U_Deger1"] = item.UserId;
                newTableRows["U_IstekTarihi"] = DateTime.Now;
                newTableRows["U_IstekSaati"] = istekSaati;
                newTableRows["U_Durum"] = "P";
                newTableRows["U_DurumAciklama"] = "Beklemede";

                newTable.Rows.Add(newTableRows);
                sonnumara++;
            }
            newTable.AcceptChanges();
             
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connectionString))
            {
                try
                {
                    //oRSIntegrationParameters.DoQuery("DELETE FROM \"@DON_EINVCUSTLIST\"");
                    bulkCopy.DestinationTableName =
                    "dbo.[@AIF_UVTDATA]";
                    bulkCopy.WriteToServer(newTable, System.Data.DataRowState.Unchanged);

                    return new Response { Value = 0, Description = "Aktivite oluşturmak için sıraya alındı.", List = null };
                }
                catch (Exception ex)
                {
                    return new Response { Value = 0, Description = "Aktivite oluşturmak için sıraya alınırken hata oluştu." + ex.Message, List = null };

                }
            }


            #region Old
            //ss = "INSERT INTO \"@AIF_UVTDATA\" (\"Code\",\"Name\",\"U_Tip\",\"U_TipAciklama\",\"U_IstekJson\",\"U_Deger1\",\"U_IstekTarihi\",\"U_IstekSaati\",\"U_Durum\",\"U_DurumAciklama\")VALUES(CASE WHEN(SELECT Count(\"Code\") FROM \"@AIF_UVTDATA\") > 0 THEN(Select Cast(tb.\"AA\" as int) + 1 from (SELECT(MAX(cast(\"Code\" as int))) as aa FROM \"@AIF_UVTDATA\") as tb) ELSE 1 END,CASE WHEN(SELECT Count(\"Code\") FROM \"@AIF_UVTDATA\") > 0 THEN(Select Cast(tb.\"AA\" as int) + 1 from (SELECT(MAX(cast(\"Code\" as int))) as aa FROM \"@AIF_UVTDATA\") as tb) ELSE 1 END, '1', 'AktiviteGirisi', '" + json.Replace("'", "").Replace("&", "") + "','" + contacts.UserId + "','" + DateTime.Now.ToString("yyyyMMdd") + "', '" + istekSaati + "' ,'P', 'Beklemede')";

            ////foreach (var item in inventoryGenExits)
            ////{
            //using (SqlCommand cmd = new SqlCommand(ss, myConnection))
            //{
            //    myConnection.Open();

            //    try
            //    {
            //        int asd = cmd.ExecuteNonQuery();

            //        if (myConnection.State == System.Data.ConnectionState.Open)
            //            myConnection.Close();

            //        //myConnection.Open();

            //        //cmd.Connection = myConnection;
            //        //cmd.CommandText = "Select \"Code\" from \"@AIF_UVTDATA\" where \"U_Deger1\" = '" + inventoryGenExits[0].Id + "' order by cast(\"Code\" as int) desc";
            //        //object id = cmd.ExecuteScalar();

            //        if (myConnection.State == System.Data.ConnectionState.Open)
            //            myConnection.Close();

            //        return new Response { Value = 0, Description = "Aktivite oluşturmak için sıraya alındı.", List = null };
            //    }
            //    catch (Exception ex)
            //    {
            //        return new Response { Value = 0, Description = "Aktivite oluşturmak için sıraya alınırken hata oluştu." + ex.Message, List = null };
            //        //response_2.Id = "-121";
            //        //response_2.Status = "E";
            //        //response_2.StatusDescription = "INSERT ERROR " + ex.Message;
            //        //response_2.StatusCode = -2232;
            //    }
            //} 
            #endregion

            //return null;

            #region Old
            //int clnum = 0;
            //string companyCodeDb = "";

            //Random rastgele = new Random();
            //int ID = rastgele.Next(0, 9999);
            //Logger logger = LogManager.GetCurrentClassLogger();

            ////var requestJson_New = JsonConvert.SerializeObject(protocol);

            ////logger.Info(" ");

            //logger.Info("ID: " + ID + " addContacts Servisine Geldi.");
            ////logger.Info("ID: " + ID + " ISTEK :" + requestJson_New);

            //try
            //{
            //    ConnectionList connection = new ConnectionList();

            //    SAPLayer.LoginCompany log = new SAPLayer.LoginCompany();

            //    log.DisconnectSAP(dbName);

            //    connection = log.getSAPConnection(dbName, ID);

            //    ////var json = JsonConvert.SerializeObject(LoginCompany.Connlist);

            //    ////return new Response { Value = -3100, Description = "Json" + json, List = null };

            //    //if (connection.number == -1)
            //    //{
            //    //    for (int i = 1; i <= 3; i++)
            //    //    {
            //    //        connection = log.getSAPConnection(dbName,ID);

            //    //        if (connection.number > -1)
            //    //        {
            //    //            break;
            //    //        }
            //    //    }
            //    //}

            //    if (connection.number == -1)
            //    {
            //        logger.Fatal("ID: " + ID + " " + "Hata Kodu - 3100 Veritabanı bağlantısı sırasında hata oluştu.");
            //        LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
            //        return new Response { Value = -3100, Description = "Hata Kodu - 3100 Veritabanı bağlantısı sırasında hata oluştu. ", List = null };
            //    }

            //    clnum = connection.number;
            //    companyCodeDb = connection.dbCode;

            //    SAPbobsCOM.Company oCompany = connection.oCompany;

            //    logger.Info("ID: " + ID + " Şirket bağlantısını başarıyla geçtik. Bağlantı sağladığımız DB :" + oCompany.CompanyDB + " clnum: " + clnum);

            //    SAPbobsCOM.CompanyService companyService = null;
            //    SAPbobsCOM.ActivitiesService activitiesService = null;
            //    SAPbobsCOM.Activity activity = null;

            //    companyService = oCompany.GetCompanyService();
            //    activitiesService = (SAPbobsCOM.ActivitiesService)companyService.GetBusinessService(SAPbobsCOM.ServiceTypes.ActivitiesService);

            //    activity = (SAPbobsCOM.Activity)activitiesService.GetDataInterface(SAPbobsCOM.ActivitiesServiceDataInterfaces.asActivity);

            //    activity.HandledByEmployee = Convert.ToInt32(contacts.ContactId);
            //    activity.Activity = (SAPbobsCOM.BoActivities)Convert.ToInt32(contacts.ContactType);
            //    activity.ActivityType = Convert.ToInt32(contacts.ContactSubType);
            //    activity.StartDate = Convert.ToDateTime(new DateTime(Convert.ToInt32(contacts.StartDate.Substring(0, 4)), Convert.ToInt32(contacts.StartDate.Substring(4, 2)), Convert.ToInt32(contacts.StartDate.Substring(6, 2))));
            //    activity.StartTime = contacts.StartTime;
            //    activity.Status = Convert.ToInt32(contacts.Status);
            //    activity.Personalflag = SAPbobsCOM.BoYesNoEnum.tYES;
            //    activity.UserFields.Item("U_RotaCode").Value = contacts.RotaKodu;
            //    activity.UserFields.Item("U_PartiNo").Value = contacts.PartiNo;
            //    activity.UserFields.Item("U_KullaniciId").Value = contacts.UserId;

            //    var aa = activitiesService.AddActivity(activity);

            //    if (aa.ActivityCode != 0)
            //    {
            //        logger.Info("ID: " + ID + " " + "Aktivite başarıyla oluşturuldu.");
            //        LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
            //        return new Response { Value = 0, Description = "Aktivite başarıyla oluşturuldu.", List = null };
            //    }
            //    else

            //    {
            //        logger.Fatal("ID: " + ID + " " + "Hata Kodu - 8100 Aktivite oluşturulurken hata oluştu. " + oCompany.GetLastErrorDescription());
            //        LoginCompany.ReleaseConnection(connection.number, connection.dbCode, ID);
            //        return new Response { Value = -2100, Description = "Hata Kodu - 8100 Aktivite oluşturulurken hata oluştu. " + oCompany.GetLastErrorDescription(), List = null };
            //    }
            //}
            //catch (Exception ex)
            //{
            //    logger.Fatal("ID: " + ID + " " + "Bilinmeyen hata oluştu. " + ex.Message);
            //    LoginCompany.ReleaseConnection(clnum, companyCodeDb, ID);
            //    return new Response { Value = 9000, Description = "Bilinmeyen hata oluştu. " + ex.Message, List = null };
            //}
            //finally
            //{
            //    LoginCompany.ReleaseConnection(clnum, companyCodeDb, ID);
            //} 
            #endregion
        }
    }
}