using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using Selenium.WebDriver.WaitExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestKit.Web.Implementation.Utility;

namespace TestKit.Web.Implementation.Pages
{
    public class DashboardPage
    {
        private IWebDriver _driver;
        //public DashboardPage()
        //{
        //    _driver = DriverSetUp.Driver;
        //}
        //[FindsBy(How = How.XPath, Using = ".//*[@id='wrapper']/nav/div/ul/li[10]/a/img")]
        //private IWebElement GoTemplateButton { get; set; }        



        public void GoTemplatePage()
        {
            _driver = DriverSetUp.Driver;
            if (!DriverExtensions.isOnSpecialPage(_driver, "Templates.htm"))
            {               
                _driver.FindElement(By.XPath(".//*[@id='wrapper']/nav/div/ul/li[10]/a/img"), 120).Click();
                //new WebDriverWait(_driver, TimeSpan.FromSeconds(120)).
                //    Until(ExpectedConditions.ElementExists(By.XPath(".//*[@id='wrapper']/nav/div/ul/li[10]/a/img")));
                //GoTemplateButton.Click();
            }           
        }        
    }
}
