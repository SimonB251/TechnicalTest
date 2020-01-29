using ApiAccessInterfaces.AbstractEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiAccessInterfaces.Abstractions
{
    public interface IApiRepository
    {
        public IInsertResult SendReadingsAsync(IEnumerable<IMeterReading> meterReadings);
    }
}
