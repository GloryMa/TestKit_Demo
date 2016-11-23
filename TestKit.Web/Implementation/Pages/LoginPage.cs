using Gauge.CSharp.Lib;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Selenium.WebDriver.WaitExtensions;
using System;
using System.Threading;
using TestKit.Web.Implementation.Utility;

namespace TestKit.Web.Implementation.Pages
{
    public class LoginPage
    {
        private IWebDriver _driver;
        //private string _HomeUrl;
        //public LoginPage()
        //{
        //    _driver = DriverSetUp.Driver;
        //    _HomeUrl = InitData.suiteStore.Get<string>("BaseUrl");
        //}

        //public static readonly string HomeUrl = Environment.GetEnvironmentVariable("BaseUrl");
        ////public static readonly string HomeUrl = D
        //[FindsBy(How = How.Id, Using = "user")]
        //private IWebElement UserName { get; set; }

        //[FindsBy(How = How.Id, Using = "pwd")]
        //private IWebElement Password { get; set; }

        //[FindsBy(How = How.Id, Using = "submit")]
        //private IWebElement Submit { get; set; }

        public void LoginFMWeb(string usename, string password)
        {
            _driver = DriverSetUp.Driver;           
            _driver.FindElement(By.Id("user")).SendKeys(usename);
            _driver.FindElement(By.Id("pwd")).SendKeys(password);
            _driver.FindElement(By.Id("submit")).Click();
            //DriverExtensions.EnterText(UserName, usename);           
            //DriverExtensions.EnterText(Password, password);
            //Submit.Click();


            if (DriverExtensions.isAlertPresent(_driver))
            {
                _driver.SwitchTo().Alert().Accept();
            }
        }

        public void NavigateToHomepage()
        {
            _driver = DriverSetUp.Driver;
            _driver.Visit(InitData.suiteStore.Get<string>("BaseUrl"));
            //TestKit.Web.Implementation.Utility.DriverSetUp.Driver.Visit(LoginPage.HomeUrl);
            GaugeMessages.WriteMessage("Navigate To Home Page.");
        }
    }
}
