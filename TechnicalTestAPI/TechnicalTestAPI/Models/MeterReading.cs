using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalTestAPI.Models
{
        //Ideally would encapsulate in a factory that handles instantiation - 
        //however requires some custom code that would enable .NET to automatically serailise the parameter into the correct obj.
        public class MeterReading
        {
            public int AccountId { get; set; }
            public DateTime MeterReadingDateTime { get; set; }
            public string MeterReadValue { get; set; }
        }
}
