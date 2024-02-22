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
    public class GetPaletDetay
    {
        public Response getUretimPaletDetay(string dbName, string uretimFisNo, string mKodValue)
        {

            GetConnectionAsString n = new GetConnectionAsString();
            string connstring = n.getConnectionAsString(dbName, mKodValue);

            DataTable dt = new DataTable();
            string sql = "";

            if (connstring != "")
            {
                sql = "Select T0.\"U_PaletNo\" as \"PaletNo\",T1.\"U_Miktar\" as \"Miktar\" from \"@AIF_WMS_PALET\" as T0 INNER JOIN \"@AIF_WMS_PALET1\" AS T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" where T0.\"U_UretimFisNo\" = '" + uretimFisNo + "'";

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
                                    dt.TableName = "UretimFisPaletDetay";
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