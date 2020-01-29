using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayerAccess.Utility
{
    public static class ExtensionClass
    {
        //Generic method that allows you to specify which object you want a row to be converted into, and which column. 
        //this is also helpful because if the indexes of the columns change (select statement changes or a new column is added for example)
        //it will still get them correctly. 
        public static T GetOrdinalValue<T>(this DataRow dr, string columnName) =>
            dr[dr.Table.Columns[columnName].Ordinal] == DBNull.Value
            ? default(T)  // Can't return null as it allows value types, so return default val.
            : (T)dr[dr.Table.Columns[columnName].Ordinal];
    }
}
