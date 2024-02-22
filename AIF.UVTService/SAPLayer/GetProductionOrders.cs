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
    public class GetProductionOrders
    {
        public Response getProductionOrders()
        {
            return null;
        }
        public Response getProductionOrdersbyDocEntry(int DocEntry, string dbName, string mKodValue)
        {
            #region old 30.03.2022
            //SqlCommand cmd = new SqlCommand("select * From OWOR as T0 INNER JOIN WOR1 as T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" where T0.\"DocEntry\" ='" + DocEntry + "'", Connection.sql);

            //if (Connection.sql.State == ConnectionState.Closed)
            //{
            //    Connection.sql.Open();
            //}

            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);

            //dt.TableName = "ProductionOrderListWithDetails";

            //Connection.sql.Close();

            //return new Response { Value = 0, Description = "", List = dt };
            //return null; 
            #endregion

            GetConnectionAsString n = new GetConnectionAsString();
            string connstring = n.getConnectionAsString(dbName, mKodValue);

            DataTable dt = new DataTable();
            string sql = "";

            if (connstring != "")
            {
                sql = "select * From OWOR as T0 WITH (NOLOCK) INNER JOIN WOR1 as T1 WITH (NOLOCK) ON T0.\"DocEntry\" = T1.\"DocEntry\" where T0.\"DocEntry\" ='" + DocEntry + "'";

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
                                    dt.TableName = "ProductionOrderListWithDetails";
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
    }
}