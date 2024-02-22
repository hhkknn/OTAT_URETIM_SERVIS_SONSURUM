using UVTService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using AIF.UVTService.DatabaseLayer;

namespace UVTService.SAPLayer
{
    public class GetProducts
    {
        public Response getProducts(string dbName, string mKodValue)
        {
            #region old 30.03.2022
            //SqlCommand cmd = new SqlCommand("select * FROM OITM", Connection.sql);

            //if (Connection.sql.State == ConnectionState.Closed)
            //{
            //    Connection.sql.Open();
            //}

            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);

            //dt.TableName = "ItemsList";

            //Connection.sql.Close();
            //return new Response { Value = 0, Description = "", List = dt }; 
            #endregion

            GetConnectionAsString n = new GetConnectionAsString();
            string connstring = n.getConnectionAsString(dbName, mKodValue);

            DataTable dt = new DataTable();
            string sql = "";

            if (connstring != "")
            {
                sql = "select * FROM OITM WITH (NOLOCK)";

                try
                {
                    using (SqlConnection con = new SqlConnection(connstring))
                    {
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            cmd.CommandType = CommandType.Text;
                            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                            {
                                using (dt = new DataTable())
                                {
                                    sda.Fill(dt);
                                    dt.TableName = "ItemsList";
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return new Response { Value = -1586, Description = "Hata Kodu - 1586 Bilinmeyen hata oluştu. " + ex.Message, List = null };
                }
            }
            return new Response { Value = 0, Description = "", List = dt };
        }



        public Response getProductsWareHouseByItemCode(string ItemCode, string dbName, string mKodValue)
        {

            #region old 30.03.2022
            //SqlCommand cmd = new SqlCommand("select (Select TOP 1 WhsName from OWHS as T1 where T1.WhsCode = T0.WhsCode) as Whs_Name,* FROM OITW as T0 where T0.ItemCode = '" + ItemCode + "'", Connection.sql);

            //if (Connection.sql.State == ConnectionState.Closed)
            //{
            //    Connection.sql.Open();
            //}

            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);

            //dt.TableName = "WarehouseList";

            //Connection.sql.Close();
            //return new Response { Value = 0, Description = "", List = dt }; 
            #endregion

            GetConnectionAsString n = new GetConnectionAsString();
            string connstring = n.getConnectionAsString(dbName, mKodValue);

            DataTable dt = new DataTable();
            string sql = "";

            if (connstring != "")
            {
                sql = "select (Select TOP 1 WhsName from OWHS as T1 WITH (NOLOCK) where T1.WhsCode = T0.WhsCode) as Whs_Name,* FROM OITW as T0 WITH (NOLOCK) where T0.ItemCode = '" + ItemCode + "'";

                try
                {
                    using (SqlConnection con = new SqlConnection(connstring))
                    {
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            cmd.CommandType = CommandType.Text;
                            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                            {
                                using (dt = new DataTable())
                                {
                                    sda.Fill(dt);
                                    dt.TableName = "WarehouseList";
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return new Response { Value = -1586, Description = "Hata Kodu - 1586 Bilinmeyen hata oluştu. " + ex.Message, List = null };
                }
            }
            return new Response { Value = 0, Description = "", List = dt };
        }

        public Response addProducts(Items items)
        {
            return null;
        }

    }
}