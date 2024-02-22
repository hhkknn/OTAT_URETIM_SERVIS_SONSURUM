using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AIF.UVTService.DatabaseLayer
{
    public class GetConnectionString
    {
        string constring = "";// ConfigurationManager.ConnectionStrings["SAP"].ConnectionString;

        public string getConnectionString(string dbName = "")
        {
            if (string.IsNullOrEmpty(dbName))
            {
                constring = ConfigurationManager.ConnectionStrings["SAP"].ConnectionString;
            }
            else
            {
                if (ConfigurationManager.ConnectionStrings[dbName.ToString()] != null)
                {
                    constring = ConfigurationManager.ConnectionStrings[dbName.ToString()].ConnectionString;
                }
            }

            return constring;
        }
    }
}