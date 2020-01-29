using System;
using System.Collections.Generic;
using System.Text;
using DataLayerInterfaces.Abstractions;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using DataLayerAccess.Utility;

namespace DataLayerAccess.Concretions
{

    public class DataRepository : IDataRepository
    {
        //.NET Core has it's own lengthy way to retrieve configuration values, for simplicity and time constraints I'll just define my config values as and when needed.  
        //This seems a nice way https://stackoverflow.com/questions/39906035/adding-settings-to-net-core-project
        //Ordinarily it would be wise to set up a user with limited permissions for security reasons, also. 
        //You will need to change this for the appropriate connection string for your db.
        public const string CONNECTIONSTRING = "Server=DESKTOP-3HNCMM2\\SQLEXPRESS;Integrated Security=SSPI;Database=TechnicalTest;";

        //Given enough time I probably would have used an ORM like Entity Framework rather than using SqlConnections
        //Would make the bulk inserts easier to optimise, using this way you have to run the query many times to insert the various values. 
        public bool UploadMeterReading(int accountID, DateTime meterReadingDateTime, string meterReadValue)
        {
            //New C# lets you use using without specifying the scope... Automatically disposes and closes the connection.
            using SqlConnection con = new SqlConnection(CONNECTIONSTRING);

            SqlCommand cmd = Utils.AttachParams(con.CreateCommand(),
                new SqlParameter() { IsNullable = false, SqlDbType = SqlDbType.Int, Value = accountID, ParameterName = "@AccountID" },
                new SqlParameter() { IsNullable = false, SqlDbType = SqlDbType.DateTime, Value = meterReadingDateTime, ParameterName = "@MeterReadingDateTime" },
                new SqlParameter() { IsNullable = false, SqlDbType = SqlDbType.NVarChar, Value = meterReadValue, ParameterName = "@MeterReadValue" }
            );

            //Stored proc
            cmd.CommandText = @"EXECUTE [TechnicalTest].[dbo].[InsertMeterReading] @AccountID, @MeterReadingDateTime, @MeterReadValue";

            con.Open();

            //Get DataTable of results
            DataTable dt = Utils.ExecuteReaderReturnDataTable(cmd);

            return dt.Rows[0].GetOrdinalValue<bool>("Result");
        }
    }
}
