using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AIF.UVTService.DatabaseLayer
{
    public class GetConnectionAsString
    {
        public string getConnectionAsString(string dbName,string mKodValue)
        {
            if (mKodValue == "010OTATURVT")
            {
                string connectionString = "";
                connectionString = string.Format("Server=172.55.10.20;Database={0};User Id=sa;Password=@tat2023!.", dbName);

                return connectionString;
            }

            if (mKodValue == "20URVT")
            {
                string connectionString = "";
                connectionString = string.Format("Server=192.168.2.51;Database={0};User Id=sa;Password=Yoruk@1234", dbName);

                return connectionString;
            }

            return null;
        }
    }
}