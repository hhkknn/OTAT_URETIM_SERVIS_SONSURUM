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
    public class GetEmployess
    {
        public Response getEmployess(string dbName, string mKodValue)
        {
            #region old 30.03.2022
            //try
            //{
            //    DataTable dt = new DataTable();
            //    SqlCommand cmd = new SqlCommand();
            //    string sql = "";
            //    sql = "SELECT TOP 1 '' as empID, '' as Name from \"OHEM\"";
            //    sql += " UNION ALL ";
            //    sql += "SELECT \"empID\",(\"firstName\" + ' ' + \"LastName\") as Name from \"OHEM\" where \"Active\" = 'Y'";
            //    cmd = new SqlCommand(sql, Connection.sql);

            //    if (Connection.sql.State != ConnectionState.Open)
            //        Connection.sql.Open();

            //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //    sda = new SqlDataAdapter(cmd);
            //    sda.Fill(dt);

            //    dt.TableName = "Employess";

            //    return new Response { Value = 0, Description = "", List = dt };
            //}
            //catch (Exception ex)
            //{
            //    return new Response { Value = -1586, Description = "Hata Kodu - 1586 Bilinmeyen hata oluştu. " + ex.Message, List = null };
            //} 
            #endregion

            GetConnectionAsString n = new GetConnectionAsString();
            string connstring = n.getConnectionAsString(dbName, mKodValue);

            DataTable dt = new DataTable();
            string sql = "";

            if (connstring != "")
            {
                sql = "SELECT TOP 1 '' as empID, '' as Name from \"OHEM\" WITH (NOLOCK)";
                sql += " UNION ALL ";
                sql += "SELECT \"empID\",(\"firstName\" + ' ' + \"LastName\") as Name from \"OHEM\" WITH (NOLOCK) where \"Active\" = 'Y'";

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
                                    dt.TableName = "Employess";
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