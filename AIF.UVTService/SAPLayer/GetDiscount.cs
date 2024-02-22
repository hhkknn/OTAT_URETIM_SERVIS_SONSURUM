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
    public class GetDiscount
    {
        public Response getDiscount(int docentry, string dbName, string mKodValue)
        {
            #region old 30.03.2022
            //SqlCommand cmd = new SqlCommand("select T0.\"DocEntry\" as DocNo,T1.\"U_DiscRate\" as LineDiscRate,* From \"@AIF_SALES_DISC\" as T0 INNER JOIN \"@AIF_SALES_DISC1\" as T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" where T0.\"DocEntry\" ='" + docentry + "'", Connection.sql);

            //if (Connection.sql.State == ConnectionState.Closed)
            //{
            //    Connection.sql.Open();
            //}

            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);

            //dt.TableName = "DiscountList";

            //Connection.sql.Close();

            //return new Response { Value = 0, Description = "", List = dt }; 
            #endregion

            GetConnectionAsString n = new GetConnectionAsString();
            string connstring = n.getConnectionAsString(dbName, mKodValue);

            DataTable dt = new DataTable();

            if (connstring != "")
            {
                var query = "select T0.\"DocEntry\" as DocNo,T1.\"U_DiscRate\" as LineDiscRate,* From \"@AIF_SALES_DISC\" as T0 WITH (NOLOCK) INNER JOIN \"@AIF_SALES_DISC1\" as T1 WITH (NOLOCK) ON T0.\"DocEntry\" = T1.\"DocEntry\" where T0.\"DocEntry\" ='" + docentry + "'";
                try
                {
                    using (SqlConnection con = new SqlConnection(connstring))
                    {
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.CommandType = CommandType.Text;
                            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                            {
                                using (dt = new DataTable())
                                {
                                    sda.Fill(dt);
                                    dt.TableName = "DiscountList";
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