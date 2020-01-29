using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ApiAccessInterfaces.AbstractEntity
{
    public interface IMeterReading
    {

        int AccountId { get; set; }
        DateTime MeterReadingDateTime { get; set; }
        string MeterReadValue { get; set; }
    }
}
