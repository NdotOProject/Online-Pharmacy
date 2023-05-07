using Online_Pharmacy__Server.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Online_Pharmacy__Server.App_Start
{
    public class AppConfig
    {
        private static readonly object _lock = new object();
        private static volatile SqlConnection _defaultConnection = null;
        private static readonly string DefaultConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        //
        public static OnlinePharmacyEntities DefaultDatabase()
        {
            return new OnlinePharmacyEntities();
        }

        // 
        public static SqlConnection DefaultConnection()
        {
            if (_defaultConnection == null)
            {
                lock(_lock )
                {
                    _defaultConnection = new SqlConnection(DefaultConnectionString);
                }
            }
            return _defaultConnection;
        }

        // 
        public const string InternalApiPrefix = "api/internal/";

        // 
        public const string PublishApiPrefix = "api/publish/";

    }
}