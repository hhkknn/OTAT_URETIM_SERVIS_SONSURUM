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
    public class GetCustomers
    {
        public Response getCustomers(string dbName, string mKodValue)
        {
            #region old 03.30.2022
            //SqlCommand cmd = new SqlCommand("select \"CardCode\" as \"Müşteri Kodu\", \"CardName\" as \"Müşteri Adı\", \"Balance\" as \"Bakiye\" FROM OCRD", Connection.sql);

            //if (Connection.sql.State == ConnectionState.Closed)
            //{
            //    Connection.sql.Open();
            //}

            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);

            //dt.TableName = "CustomerList"; 
            #endregion

            GetConnectionAsString n = new GetConnectionAsString();
            string connstring = n.getConnectionAsString(dbName, mKodValue);

            DataTable dt = new DataTable();

            if (connstring != "")
            {
                var query = "select \"CardCode\" as \"Müşteri Kodu\", \"CardName\" as \"Müşteri Adı\", \"Balance\" as \"Bakiye\" FROM OCRD WITH (NOLOCK)";
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
                                    dt.TableName = "CustomerList";
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

        public Response getCustomersWithAddressByCardCode(string cardCode, string dbName, string mKodValue)
        {
            #region old 30.03.2022
            //SqlCommand cmd = new SqlCommand("select T1.Address as CRD1Adress,T1.Street as CRD1Street,T1.Block as CRD1Block,T1.ZipCode as CRD1ZipCode,T1.County as CRD1County,T1.City as CRD1City,* FROM OCRD as T0 INNER JOIN CRD1 as T1 ON T0.\"CardCode\" = T1.\"CardCode\" where T0.\"CardCode\" = '" + cardCode + "'", Connection.sql);

            //if (Connection.sql.State == ConnectionState.Closed)
            //{
            //    Connection.sql.Open();
            //}

            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);

            //dt.TableName = "CustomerListWithAddress";

            //return new Response { Value = 0, Description = "", List = dt }; 
            #endregion

            GetConnectionAsString n = new GetConnectionAsString();
            string connstring = n.getConnectionAsString(dbName, mKodValue);

            DataTable dt = new DataTable();

            if (connstring != "")
            {
                var query = "select T1.Address as CRD1Adress,T1.Street as CRD1Street,T1.Block as CRD1Block,T1.ZipCode as CRD1ZipCode,T1.County as CRD1County,T1.City as CRD1City,* FROM OCRD as T0 WITH (NOLOCK) INNER JOIN CRD1 as T1 WITH (NOLOCK) ON T0.\"CardCode\" = T1.\"CardCode\" where T0.\"CardCode\" = '" + cardCode + "'";
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
                                    dt.TableName = "CustomerListWithAddress";
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

        public Response getCustomersContactByCardCode(string cardCode, string dbName, string mKodValue)
        {
            #region old 30.03.2022
            //SqlCommand cmd = new SqlCommand("select T0.CntctCode as CntctCode1,* from OCPR as T0 where T0.\"CardCode\" = '" + cardCode + "'", Connection.sql);

            //if (Connection.sql.State == ConnectionState.Closed)
            //{
            //    Connection.sql.Open();
            //}

            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);

            //dt.TableName = "CustomerContact";

            //return new Response { Value = 0, Description = "", List = dt }; 
            #endregion

            GetConnectionAsString n = new GetConnectionAsString();
            string connstring = n.getConnectionAsString(dbName, mKodValue);

            DataTable dt = new DataTable();

            if (connstring != "")
            {
                var query = "select T0.CntctCode as CntctCode1,* from OCPR as T0 WITH (NOLOCK) where T0.\"CardCode\" = '" + cardCode + "'";
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
                                    dt.TableName = "CustomerContact";
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