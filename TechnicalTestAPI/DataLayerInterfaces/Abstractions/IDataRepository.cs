using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayerInterfaces.Abstractions
{
    public interface IDataRepository
    {
        const string CONNECTIONSTRING = "Server=LocalDB;User ID=usrTechnical;Password=TechnicalTest;Database=Notify;";
        bool UploadMeterReading(int accountID, DateTime meterReadingDateTime, string meterReadValue);
    }
}
