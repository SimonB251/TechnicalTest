using ApiAccess.Concretions;
using ApiAccessInterfaces.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiAccessFactory.Factories
{
    public class ApiRepostioryFactory
    {
        public static IApiRepository GetRepository() {
            return new ApiRepository();
        }
    }
}
