using Gauge.CSharp.Lib;
using Gauge.CSharp.Lib.Attribute;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestKit.Web.Implementation.Actions
{
    public class NotPageSpec
    {
        protected static readonly string BaselineFolder = Environment.GetEnvironmentVariable("BaselinePath");
        protected static readonly string TempFolder = Path.Combine(BaselineFolder, "Temp");
        protected static readonly string DataServerFolder = Environment.GetEnvironmentVariable("DataServerPath");
        [Step("Delete All Temp Files")]
        public void DeleteTempFiles()
        {
            try
            {
                Directory.Delete(TempFolder, true);
                GaugeMessages.WriteMessage("Delete Temp Files Success.");
            }
            catch
            {
                GaugeMessages.WriteMessage("Delete Temp Files Fail.");
            }
        }

        //[BeforeSpec]
        [Step("Restart DataServer")]
        public void RestartServer()
        {
            try
            {                
                Process[] dataServers = Process.GetProcessesByName("FMDataServerConsole");
                foreach (var item in dataServers)
                {
                    item.Kill();
                    Thread.Sleep(1000);
                }
                //Process[] dataEngine = Process.GetProcessesByName("FMDataServerEngine");
                //foreach (var item in dataEngine)
                //{
                //    item.Kill();
                //    Thread.Sleep(1000);
                //}

                Thread.Sleep(5000);

                Process dsProcess;
                string dataServerConsoleExe = Path.Combine(DataServerFolder, "FMDataServerConsole.exe");
                if (File.Exists(dataServerConsoleExe))
                {
                    dsProcess = Process.Start(dataServerConsoleExe);
                }
                else
                {
                    GaugeMessages.WriteMessage("Could not find file: {0}", dataServerConsoleExe);
                }
            }
            catch
            {
                GaugeMessages.WriteMessage("Restart DataServer File Fail.");
            }
            Thread.Sleep(5000);
        }


    }
}
