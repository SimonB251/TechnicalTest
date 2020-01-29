using System;
using System.Collections.Generic;
using System.Text;
using DataLayerInterfaces.Abstractions;
using DataLayerAccess.Concretions;

namespace DataLayerFactories.Factories
{
    public class DataRepostioryFactory
    {
        public static IDataRepository CreateRepository() {
            return new DataRepository();
        }
    }
}
