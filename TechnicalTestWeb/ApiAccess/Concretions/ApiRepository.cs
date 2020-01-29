using ApiAccessInterfaces.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using ApiAccess.Entities;
using System.Runtime.Serialization.Json;
using System.IO;
using ApiAccessInterfaces.AbstractEntity;
using System.Linq;
using System.Runtime.Serialization;

namespace ApiAccess.Concretions
{
    public class ApiRepository : IApiRepository
    {
        //Again, just putting it here for temporary convience, would change this to be inline with good standards: https://stackoverflow.com/questions/39906035/adding-settings-to-net-core-project
        public const string APIURL = @"https://localhost:44331/"; //You might need to change this if your port is different. 

        public IInsertResult SendReadingsAsync(IEnumerable<IMeterReading> meterReadings)
        {
            //HttpClient is only being used here so I can instantiate it here. Otherwise it wouldn't be worth recreating it in each class method.
            using HttpClient client = new HttpClient();

            //Converts the object to a version using a concretion due to serialisation not working with abstractions
            List<MeterReading> readingsList = meterReadings.ToList().ConvertAll(x => (MeterReading)x);

            StreamReader sr = SerialiseObject<List<MeterReading>>(readingsList);

            try
            {
                var response = client.PostAsync(APIURL + "csv", new StringContent(
                   sr.ReadToEnd(), Encoding.UTF8, "application/json")).Result;

                //Will error out in the event of a failed call to the API. 
                response.EnsureSuccessStatusCode();

                //https://stackoverflow.com/questions/24131067/deserialize-json-to-array-or-list-with-httpclient-readasasync-using-net-4-0-ta
                //Read the response data.
                var responseStream = response.Content.ReadAsStreamAsync().Result;
                
                //Deserialise and return.
                return DeserialiseObject<InsertResult>(responseStream);
            }
            catch (Exception e)
            {
                //Exception means that the success code failed. You would handle this error in some way, such as a message to the user. 
                throw e;
            }

            //If there is no exception it means that the HttpResponse succeeded.
            return new InsertResult();
        }

        //I made these generic to avoid reusing code. 
        private static StreamReader SerialiseObject<T>(object obj)
        {
            //The serialiser the converts the object to a string
            var stream = new MemoryStream();
            var serialiser = new DataContractJsonSerializer(typeof(T), new DataContractJsonSerializerSettings
            {
                //Microsofts own date formatting cannot be parsed by .NET core web api automatic parameter binding....??
                DateTimeFormat = new DateTimeFormat("yyyy-MM-dd'T'HH:mm:ssZ")
            });

            //Serialise and provide a stream of the JSON data. 
            serialiser.WriteObject(stream, obj);

            //Read the stream again from the beginning to get a string to send.
            stream.Position = 0;
            StreamReader sr = new StreamReader(stream);
            return sr;
        }

        private static T DeserialiseObject<T>(Stream objStream)
        {
            //Defining what object to deserialise into
            var serialiser = new DataContractJsonSerializer(typeof(T), new DataContractJsonSerializerSettings
            {
                DateTimeFormat = new DateTimeFormat("yyyy-MM-dd'T'HH:mm:ssZ")
            });

            //Deserialise the stream and return the object after a cast.
            return (T)serialiser.ReadObject(objStream);
        }
    }
}
