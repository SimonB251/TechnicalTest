using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAccessInterfaces.Abstractions;
using ApiAccessInterfaces.AbstractEntity;

namespace ApiAccess.Entities
{
    public class InsertResult : IInsertResult
    {
        //DataContractJsonSerializer fails to serialise objects that have different case to the JSON fields.... Temporary fix?
        public int successfulInsertions { get; set; }
        public int failedInsertions { get; set; }
    }
}
