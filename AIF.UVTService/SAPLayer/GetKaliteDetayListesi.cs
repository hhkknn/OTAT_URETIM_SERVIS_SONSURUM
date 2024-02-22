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
    public class GetKaliteDetayListesi
    {
        public Response getKaliteDetayListesi(string dbName, string mKodValue)
        { 
            GetConnectionAsString n = new GetConnectionAsString();
            string connstring = n.getConnectionAsString(dbName, mKodValue);

            DataTable dt = new DataTable();
            string sql = "";

            if (connstring != "")
            {
                sql = "select * From \"@AIF_KALITEDETAY\" WITH (NOLOCK)";

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
                                    dt.TableName = "KaliteDetay";
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return new Response { Value = -1586, Description = "Hata Kodu - 1436 Bilinmeyen hata oluştu. " + ex.Message, List = null };
                }
            }
            return new Response { Value = 0, Description = "", List = dt };
        }
         

    }
}