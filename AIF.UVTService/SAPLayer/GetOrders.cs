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
    public class GetOrders
    {
        public Response getOrders(string dbName, string mKodValue)
        {
            #region old 30.03.2022
            //SqlCommand cmd = new SqlCommand("select * From ORDR", Connection.sql);

            //if (Connection.sql.State == ConnectionState.Closed)
            //{
            //    Connection.sql.Open();
            //}

            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);

            //dt.TableName = "OrderList";

            //Connection.sql.Close();

            //return new Response { Value = 0, Description = "", List = dt }; 
            #endregion

            GetConnectionAsString n = new GetConnectionAsString();
            string connstring = n.getConnectionAsString(dbName, mKodValue);

            DataTable dt = new DataTable();
            string sql = "";

            if (connstring != "")
            {
                sql = "select * From ORDR WITH (NOLOCK)";

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
                                    dt.TableName = "OrderList";
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

        public Response getOrdersWithDetailsByDocEntry(int DocEntry, string dbName, string mKodValue)
        {
            #region old 30.03.2022
            //SqlCommand cmd = new SqlCommand("select T1.DiscPrcnt as LineDiscount, * From ORDR as T0 INNER JOIN RDR1 as T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" where T0.\"DocEntry\" ='" + DocEntry + "'", Connection.sql);

            //if (Connection.sql.State == ConnectionState.Closed)
            //{
            //    Connection.sql.Open();
            //}

            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);

            //dt.TableName = "OrderListWithDetails";

            //Connection.sql.Close();

            //return new Response { Value = 0, Description = "", List = dt }; 
            #endregion

            GetConnectionAsString n = new GetConnectionAsString();
            string connstring = n.getConnectionAsString(dbName, mKodValue);

            DataTable dt = new DataTable();
            string sql = "";

            if (connstring != "")
            {
                sql = "select T1.DiscPrcnt as LineDiscount, * From ORDR as T0 WITH (NOLOCK) INNER JOIN RDR1 as T1 WITH (NOLOCK) ON T0.\"DocEntry\" = T1.\"DocEntry\" where T0.\"DocEntry\" ='" + DocEntry + "'"; 

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
                                    dt.TableName = "OrderListWithDetails";
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

        public Response addOrders(Orders orders)
        {
            return null;
        }

    }
}