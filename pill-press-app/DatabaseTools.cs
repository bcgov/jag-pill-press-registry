using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Jag.PillPressRegistry.Public
{
    public static class DatabaseTools
    {
        /// <summary>
        /// Logic required to generate a connection string.  If no environment variables exists, defaults to a local sql instance.
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionString(IConfiguration Configuration)
        {
            string result = "Server=";

            if (!string.IsNullOrEmpty(Configuration["DATABASE_SERVICE_NAME"]))
            {
                result += Configuration["DATABASE_SERVICE_NAME"];
            }
            else // default to a local connection.
            {
                result += "127.0.0.1";
            }

            result += ";Database=";

            result += GetDatabaseName(Configuration);

            if (!string.IsNullOrEmpty(Configuration["DB_USER"]) && !string.IsNullOrEmpty(Configuration["DB_PASSWORD"]))
            {
                result += ";User Id=" + Configuration["DB_USER"] + ";Password=" + Configuration["DB_PASSWORD"] + ";";
            }

            return result;
        }

        public static string GetSaConnectionString(IConfiguration Configuration)
        {
            string result = "Server=tcp:";

            if (!string.IsNullOrEmpty(Configuration["DATABASE_SERVICE_NAME"]))
            {
                result += Configuration["DATABASE_SERVICE_NAME"];
            }
            else // default to a local connection.
            {
                result += "127.0.0.1";
            }

            //result += ";Database=";

            //result += GetDatabaseName(Configuration);

            result += ";Database=master;User Id=SA";

            if (!string.IsNullOrEmpty(Configuration["DB_ADMIN_PASSWORD"]))
            {
                result += ";Password=" + Configuration["DB_ADMIN_PASSWORD"] + ";";
            }

            return result;
        }

        
        /// <summary>
        /// Returns the name of the database, as set in the environment.
        /// </summary>
        /// <returns></returns>
        public static string GetDatabaseName(IConfiguration Configuration)
        {
            string result = "";
            if (!string.IsNullOrEmpty(Configuration["DB_DATABASE"]))
            {
                result += Configuration["DB_DATABASE"];
            }
            else // default to a local connection.
            {
                result += "Surveys";
            }

            return result;
        }
    }
}
