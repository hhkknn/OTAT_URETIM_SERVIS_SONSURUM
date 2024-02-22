using AIF.UVTService.DatabaseLayer;
using UVTService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace UVTService.SAPLayer
{
    public class GetCompanyList
    {
        public Response getCompanyList(string dbName,string mKodValue)
        {
            GetConnectionAsString n = new GetConnectionAsString();
            string connstring = n.getConnectionAsString(dbName,mKodValue);

            DataTable dt = new DataTable();
            try
            {
                //GetConnectionString n = new GetConnectionString();
                //connstring = n.getConnectionString();

                if (connstring != "")
                {

                    var query = string.Format(@"select '' as 'dbName', '' as 'cmpName' UNION ALL select dbName,cmpName from [SBO-COMMON].dbo.SRGC WITH (NOLOCK) ");
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
                                        dt.TableName = "CompanyList";
                                    }
                                }
                            }
                            con.Close();
                        }
                    }
                    catch (Exception ex)
                    {

                    } 
                }
            }
            catch (Exception ex)
            {

            }
            return new Response { List = dt };
        }
    }
}