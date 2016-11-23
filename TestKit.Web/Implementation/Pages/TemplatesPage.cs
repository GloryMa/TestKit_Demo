using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using Selenium.WebDriver.WaitExtensions;
using System;
using System.Linq;
using TestKit.Web.Implementation.Utility;

namespace TestKit.Web.Implementation.Pages
{
    public class TemplatesPage
    {
        private IWebDriver _driver;

        //public TemplatesPage()
        //{
        //    _driver = DriverSetUp.Driver;
        //}

        //[FindsBy(How = How.Id, Using = "run")]
        //private IWebElement RunButton { get; set; }

        //[FindsBy(How = How.Id, Using = "btnLogout")]
        //private IWebElement LogoutButton { get; set; }

        public void SelectTemplate(string templateName)
        {
            _driver = DriverSetUp.Driver;
            _driver.Wait(2500).ForPage().ReadyStateComplete();
            string xpathString = ".//*[contains(text(), '" + templateName + "')]";
            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            var templates = _driver.FindElements(By.XPath(xpathString));
            var template = from t in templates
                           where t.Text.Split('(')[0] == templateName
                           select t;
            template.First().Click();
        }
        public void Run()
        {
            _driver = DriverSetUp.Driver;
            _driver.FindElement(By.Id("run")).Click();
           // RunButton.Click();
        }

        public void RunTemplate(string item)
        {
            SelectTemplate(item);
            Run();
        }
        public void Logout()
        {
            _driver = DriverSetUp.Driver;
            _driver.FindElement(By.Id("btnLogout")).Click();
            //LogoutButton.Click();           
            if (DriverExtensions.isAlertPresent(_driver))
            {
                _driver.SwitchTo().Alert().Accept();
            }
        }
    }
}
