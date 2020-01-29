using DataLayerInterfaces.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalTestAPI.Models;
using TechnicalTestAPI.Utils;

namespace TechnicalTestAPI.Engine
{
    public class CsvEngine
    {
        public InsertResult InputMeterReadings(IEnumerable<MeterReading> readings, IDataRepository dataRepo)
        {
            //Create the single insertResults
            InsertResult insertResult = new InsertResult();

            //Validate the readings before submitting them to the DB (where further validation is implemented).
            ValidateMeterReadings(ref readings, dataRepo, ref insertResult);

            //Submit these validated values to the DB.
            SubmitValidatedMeterReadings(ref readings, dataRepo, ref insertResult);

            //Return for display to the user.
            return insertResult;
        }
        private void ValidateMeterReadings(ref IEnumerable<MeterReading> readings, IDataRepository dataRepo, ref InsertResult insertResult)
        {
            //I cast this so i can use the Remove method below.
            List<MeterReading> readingsList = readings.ToList();

            foreach (MeterReading meterReading in readings)
            {
                if (meterReading.MeterReadValue.Length > 5
                 || !meterReading.MeterReadValue.IsDigitsOnly())
                {
                    //Invalid, thus failed
                    insertResult.FailedInsertions++;

                    //Remove so it isn't inserted into the database.
                    readingsList.Remove(meterReading);
                }
                //You can probably simply do this in SQL, but I thought I'd do it here incase the DB script doesn't include that configuration, in real life situation it would likely be a better option.
                else if (meterReading.MeterReadValue.Length < 5)
                { 
                    //Don't add to successful insertions yet as it may fail during submission to the DB
                    meterReading.MeterReadValue = $"{meterReading.MeterReadValue:D5}";
                }
            }

            readings = readingsList;
        }

        private void SubmitValidatedMeterReadings(ref IEnumerable<MeterReading> readings, IDataRepository dataRepo, ref InsertResult insertResult)
        {
            foreach (MeterReading meterReading in readings)
            {
                //Method returns a boolean if it fails to validate (such as a missing accountId). 
                bool isSuccessful = dataRepo.UploadMeterReading(meterReading.AccountId, meterReading.MeterReadingDateTime, meterReading.MeterReadValue);

                //Both the Successful and Failed insertion numbers are incremented by this line. 
                var assignment = isSuccessful == true ? insertResult.SuccessfulInsertions++ : insertResult.FailedInsertions++;
            }
        }
    }
}
