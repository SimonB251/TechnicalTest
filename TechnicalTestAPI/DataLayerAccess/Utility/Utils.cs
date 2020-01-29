using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayerAccess.Utility
{
    static class Utils
    {
        public static SqlCommand AttachParams(SqlCommand cmd, params SqlParameter[] parameters)
        {
            for (int i = 0; i < parameters.Length; i++)
                cmd.Parameters.Add(parameters[i]);

            return cmd;
        }

        public static DataTable ExecuteReaderReturnDataTable(SqlCommand cmd)
        {
            DataTable dt = new DataTable();

            dt.Load(cmd.ExecuteReader());

            return dt;
        }
    }
}
