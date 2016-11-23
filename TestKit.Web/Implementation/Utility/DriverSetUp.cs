using Gauge.CSharp.Lib.Attribute;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TestKit.Web.Implementation.Utility
{
    public class DriverSetUp
    {
        internal static IWebDriver Driver;

        //[BeforeSpec]
        [BeforeSuite]
        public void StartTestServer()
        {
            InitData.InitParameters();
            //Driver = new TestDriverFactory().CreateDriver();
            var browser = Environment.GetEnvironmentVariable("Browser");
            switch (browser)
            {
                case "chrome":
                    Driver = new ChromeDriver();
                    break;
                case "ie":
                    Driver = new InternetExplorerDriver();
                    break;
                default:
                    Driver = new FirefoxDriver();
                    break;
            }
            Driver.Manage().Cookies.DeleteAllCookies();
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        }

        //[AfterSpec]
        [AfterSuite]
        public void StopTestServer()
        {
            Driver.Close();
            Driver.Quit();
            //Process[] chromeDriverProcesses = Process.GetProcessesByName("chromedriver");

            //foreach (var chromeDriverProcess in chromeDriverProcesses)
            //{
            //    chromeDriverProcess.Kill();
            //}
            //Process[] chromeProcesses = Process.GetProcessesByName("chrome");

            //foreach (var chrome in chromeProcesses)
            //{
            //    chrome.Kill();
            //}
        }

        //public static IWebDriver Driver { get; private set; }

        //[BeforeSpec]
        ////[Step("Setup WebDriver")]
        //public void Setup()
        //{
        //    var browser = Environment.GetEnvironmentVariable("Browser");
        //    switch (browser)
        //    {
        //        case "chrome":                  
        //            Driver = new ChromeDriver();
        //            break;
        //        case "ie":
        //            Driver = new InternetExplorerDriver();
        //            break;
        //        default:
        //            Driver = new FirefoxDriver();
        //            break;
        //    }
        //    //Driver.Manage().Cookies.DeleteAllCookies();
        //    //Driver.Manage().Window.Maximize();
        //    //Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        //}

        //[AfterSpec]
        ////[Step("TearDown WebDriver")]
        //public void TearDown()
        //{
        //    Driver.Close();
        //    Driver.Quit();
        //    //Driver.Dispose();
        //    //Process[] chromeDriverProcesses = Process.GetProcessesByName("chromedriver");

        //    //foreach (var chromeDriverProcess in chromeDriverProcesses)
        //    //{
        //    //    chromeDriverProcess.Kill();
        //    //}
        //    //Driver Kill
        //}
    }
    //public class TestDriverFactory
    //{
    //    public IWebDriver CreateDriver()
    //    {
    //        bool IsRemote = false;
    //        if (IsRemote)
    //        {
    //            return new WebDriverFactory().Create(
    //                new RemoteDriverConfiguration(
    //                        "Chrome",
    //                        PlatformType.Windows,
    //                        "54",
    //                        "http://10.86.0.102",
    //                        5555,
    //                        "Maximize"));
    //        }

    //        //Else (false) create a LocalDriverConfig and pass this to the factory
    //        return new WebDriverFactory().Create(
    //            new LocalDriverConfiguration(
    //                "Chrome",
    //                "Maximize"));
    //    }

    //}
}
