using ApiAccess.Entities;
using ApiAccessInterfaces.AbstractEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiAccessFactory.Factories
{
    public class MeterReadingFactory
    {
        public static IMeterReading CreateMeterReading(int accountId, DateTime meterReadingDateTime, string meterReadValue)
        {
            return new MeterReading() { AccountId = accountId,  MeterReadingDateTime = meterReadingDateTime, MeterReadValue = meterReadValue};
        }
    }
}
