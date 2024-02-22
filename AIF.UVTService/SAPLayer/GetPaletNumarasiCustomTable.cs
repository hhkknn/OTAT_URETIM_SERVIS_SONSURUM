using AIF.UVTService.DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UVTService.Models;

namespace AIF.UVTService.SAPLayer
{
    public class GetPaletNumarasiCustomTable
    {

        public Response getPaletNumarasiCustomTable(string dbName, string mKod)
        {
            DataTable dt = new DataTable();
            try
            {
                GetConnectionAsString n = new GetConnectionAsString();
                string connstring = n.getConnectionAsString(dbName, mKod);

                if (connstring != "")
                {
                    var query = "SELECT TOP 1 T0.\"DocEntry\", T0.\"U_BaslangicNo\", T0.\"U_SiradakiNo\" FROM \"@AIF_WMS_PLTNO\"  T0";

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
                                        dt.TableName = "PaletNumarasiGetir";

                                        if (dt.Rows.Count == 0)
                                        {
                                            return new Response { List = null, Value = -555, Description = "PALET NUMARASI GİRİŞİ YAPILMAMIŞTIR." };
                                        }
                                    }

                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return new Response { List = null, Value = -9999, Description = "BİLİNMEYEN HATA OLUŞTU." + ex.Message };
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response { List = null, Value = -9998, Description = "BİLİNMEYEN HATA OLUŞTU." + ex.Message };
            }
            return new Response { List = dt, Value = 0 };
        }

        public Response updatePaletNumarasi(string dbName, int docentry, int siraNumarasi, string mKod)
        {
            DataTable dt = new DataTable();
            try
            {
                GetConnectionAsString n = new GetConnectionAsString();
                string connstring = n.getConnectionAsString(dbName, mKod);

                if (connstring != "")
                {
                    var query = "UPDATE \"@AIF_WMS_PLTNO\" SET \"U_SiradakiNo\" = " + (siraNumarasi + 1) + " where \"DocEntry\" = " + docentry + "";

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
                                        dt.TableName = "UpdatePaletNumarasi";

                                        //if (dt.Rows.Count == 0)
                                        //{
                                        //    return new Response { _list = null, Val = -555, Desc = "PARTİ NUMARASI GÜNCELLENİRKEN HATA OLUŞTU." };
                                        //}
                                    }

                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return new Response { List = null, Value = -9999, Description = "BİLİNMEYEN HATA OLUŞTU." + ex.Message };
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response { List = null, Value = -9998, Description = "BİLİNMEYEN HATA OLUŞTU." + ex.Message };
            }
            return new Response { List = dt, Value = 0 };
        }
    }
}