using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAccessInterfaces.AbstractEntity;
using Microsoft.AspNetCore.Http;
using TechnicalTestWeb.ViewModels;

namespace TechnicalTestWeb.Models
{
    public class HomeModel
    {

        public HomeModel()
        {
            InsertResult = new InsertResult();
        }

        public IFormFile CsvUpload { get; set; }
        public InsertResult InsertResult { get; set; }
    }
}
