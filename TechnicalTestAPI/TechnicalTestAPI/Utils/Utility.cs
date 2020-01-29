using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalTestAPI.Utils
{
    public static class Utility
    {
        //https://stackoverflow.com/a/7461097
        //Apparently the fastest! I changed it around to be an extension method so it looks nice syntactically.
        public static bool IsDigitsOnly(this string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }
}
