using System;
using System.Collections.Generic;
using System.Text;

namespace ApiAccessInterfaces.AbstractEntity
{
    public interface IInsertResult
    {
        //See concretion for comments
        int successfulInsertions { get; set; }
        int failedInsertions { get; set; }
    }
}
