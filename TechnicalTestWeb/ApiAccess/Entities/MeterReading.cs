using ApiAccessInterfaces.AbstractEntity;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ApiAccess.Entities
{
    [DataContract]
    public class MeterReading : IMeterReading
    {
        [DataMember]
        public int AccountId { get; set; }
        [DataMember]
        public DateTime MeterReadingDateTime { get; set; }
        [DataMember]
        public string MeterReadValue { get; set; }
    }
}