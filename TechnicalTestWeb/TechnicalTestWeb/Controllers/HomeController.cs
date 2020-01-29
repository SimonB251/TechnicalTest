using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApiAccessFactory.Factories;
using ApiAccessInterfaces.AbstractEntity;
using ApiAccessInterfaces.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TechnicalTestWeb.Models;

namespace TechnicalTestWeb.Controllers
{
    public class HomeController : Controller
    {
        public IApiRepository ApiRepo { get; set; }

        //Temp workaround(?), read here: https://stackoverflow.com/questions/14270082/connection-string-using-windows-authentication/46430792 
        [ActivatorUtilitiesConstructor]
        public HomeController() : this(ApiRepostioryFactory.GetRepository()) { }

        public HomeController(IApiRepository _apiRepo)
        {
            ApiRepo = _apiRepo;
        }


        public IActionResult Index(HomeModel model)
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult Upload(HomeModel model)
        {
            List<IMeterReading> meterReadings = new List<IMeterReading>();

            using var reader = new StreamReader(model.CsvUpload.OpenReadStream());

            //Ignores headers
            reader.ReadLine();

            //https://stackoverflow.com/questions/40045147/how-to-read-into-memory-the-lines-of-a-text-file-from-an-iformfile-in-asp-net-co/40045456
            while (reader.Peek() >= 0)
            {
                //You would validate this better, incase values had commas or something else. I don't have time to improve this.
                string[] commaDelimitedList = reader.ReadLine().Split(',');

                //This is not a good way to do this and is prone to failures, I would clean this up given the time.
                meterReadings.Add(MeterReadingFactory.CreateMeterReading(
                    int.Parse(commaDelimitedList[0]), 
                    DateTime.Parse(commaDelimitedList[1]), 
                    commaDelimitedList[2])
                );
            }

            //I wouldn't need to do this without duplicate objects which i don't have time to fix. I have to deliver in 30 mins!!
            IInsertResult result = ApiRepo.SendReadingsAsync(meterReadings);

            model.InsertResult.FailedInsertions = result.failedInsertions;
            model.InsertResult.SuccessfulInsertions = result.successfulInsertions;

            return View("Index", model);
        }
    }
}
