using ApiAccessInterfaces.AbstractEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalTestWeb.ViewModels
{
    public class InsertResult
    {
        //This isn't good to have a duplicate here, but MVC cannot bind to abstractions and I don't have time to write something which uses the factory!
        public int SuccessfulInsertions { get; set; }
        public int FailedInsertions { get; set; }
    }
}
