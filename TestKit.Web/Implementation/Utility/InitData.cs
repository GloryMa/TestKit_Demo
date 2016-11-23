using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gauge.CSharp.Lib;

namespace TestKit.Web.Implementation.Utility
{
    public class InitData
    {
        public static DataStore suiteStore;
        public static void InitParameters()
        {
            suiteStore = DataStoreFactory.SuiteDataStore;
            suiteStore.Add("BaseUrl", Environment.GetEnvironmentVariable("BaseUrl"));
            suiteStore.Add("BaselinePath", Environment.GetEnvironmentVariable("BaselinePath"));
        }
    }
}
