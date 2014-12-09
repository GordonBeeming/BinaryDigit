namespace BinaryDigit
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.EntityClient;

    public static class Configuration
    {
        #region Public Properties

        public static List<string> BeginRequest_IgnoreRawUrlsToLower
        {
            get
            {
                return new List<string>
                       {
                           "/bundles/",
                           "/scriptresource.axd",
                           "/webresource.axd",
                           "/content/",
                           "/ckeditor/",
                           "/images/",
                           "/scripts/",
                           "/favicon.ico",
                           "/robots.txt"
                       };
            }
        }

        public static string DefaultConnectionString
        {
            get
            {
                if (ConfigurationManager.ConnectionStrings.Count > 0)
                {
                    return ConfigurationManager.ConnectionStrings[0].ConnectionString;
                }
                return null;
            }
        }

        public static bool SqlDebugLogging
        {
            get
            {
                return ConfigurationManager.AppSettings["BinaryDigit.SQL.Logging"] == "1";
            }
        }

        #endregion

        #region Public Methods and Operators

        public static string CreateDefaultEntityConnectionString(string fullModelFileNameWithoutExtension, string fullModelPathName = @"res://*/")
        {
            var entityBuilder = new EntityConnectionStringBuilder();
            entityBuilder.Provider = "System.Data.SqlClient";
            entityBuilder.ProviderConnectionString = DefaultConnectionString;
            entityBuilder.Metadata = fullModelPathName + fullModelFileNameWithoutExtension + ".csdl|" + fullModelPathName + fullModelFileNameWithoutExtension + ".ssdl|" + fullModelPathName + fullModelFileNameWithoutExtension + ".msl";
            var conn = new EntityConnection(entityBuilder.ToString());
            return conn.ConnectionString;
        }

        #endregion

        public static class DataAccess
        {
            public static class Base
            {
                #region Public Properties

                public static string BD_TimeStamp_SqlType_Name
                {
                    get
                    {
                        return "BD_TimeStamp";
                    }
                }

                #endregion
            }
        }

        public static class IO
        {
            #region Public Properties

            public static string SmartFileOutputHandlerAshx
            {
                get
                {
                    return "/SmartFileOutputHandler.ashx";
                }
            }

            #endregion
        }
    }
}