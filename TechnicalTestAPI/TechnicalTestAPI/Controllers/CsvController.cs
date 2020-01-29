using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DataLayerFactories.Factories;
using DataLayerInterfaces.Abstractions;
using TechnicalTestAPI.Models;
using TechnicalTestAPI.Engine;
using Microsoft.Extensions.DependencyInjection;

//Renamed namespace from Technical_Test_Simon_Brown if there are namespace errors do a find and replace with the old namespace.
namespace TechnicalTestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CsvController : ControllerBase
    {
        public IDataRepository DataRepo { get; set; }

        //Temp workaround(?), read here: https://stackoverflow.com/questions/14270082/connection-string-using-windows-authentication/46430792 
        [ActivatorUtilitiesConstructor]
        public CsvController() : this(DataRepostioryFactory.CreateRepository()){}

        public CsvController(IDataRepository _dataRepo)
        {
            DataRepo = _dataRepo;
        }

        [HttpPost]
        public InsertResult Post([FromBody]List<MeterReading> readings)
        {
            CsvEngine csvEngine = new CsvEngine();

            return csvEngine.InputMeterReadings(readings, DataRepo);
        }
    }
}
