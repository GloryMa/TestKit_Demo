using Gauge.CSharp.Lib;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestKit.Web.Implementation.Utility
{
    public class ScreenGrabber : IScreenGrabber
    {
        //public static bool IsCompare { get; set; } = false;
        //public static byte[] Differ { get; set; } = null;
        //public static byte[] expect { get; set; } = null;
        //public static byte[] actual { get; set; } = null;

        public static bool IsCompare = false;
        public static byte[] Differ = null;
        //public static string TempFolder = string.Empty;

        public byte[] TakeScreenShot()
        {
            try
            {
                if (IsCompare)
                {
                    return Differ;
                }
                else
                {
                    //return ScreenShot When Anything Failed
                    var screenshot = DriverSetUp.Driver as ITakesScreenshot;
                    return screenshot == null ? Enumerable.Empty<byte>().ToArray() : screenshot.GetScreenshot().AsByteArray;
                }
            }            
            finally
            {
                IsCompare = false;
            }
        }           
    }
}
